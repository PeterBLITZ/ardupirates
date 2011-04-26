/*
	APM_ADC.cpp - ADC ADS7844 Library for Ardupilot Mega
Total rewrite by Syberian:

Full I2C sensors replacement:
ITG3200, BMA180

Integrated analog Sonar on the ADC channel 7 (in centimeters)
//D49 (PORTL.0) = input from sonar
//D47 (PORTL.2) = sonar Tx (trigger)
//The smaller altitude then lower the cycle time

	
	
	
*/
extern "C" {
  // AVR LibC Includes
  #include <inttypes.h>
  #include <avr/interrupt.h>
  #include "WConstants.h"
}

#include "APM_ADC.h"

// *********************
// I2C general functions
// *********************
  #define I2C_PULLUPS_DISABLE        PORTC &= ~(1<<4); PORTC &= ~(1<<5);

// Mask prescaler bits : only 5 bits of TWSR defines the status of each I2C request
#define TW_STATUS_MASK	(1<<TWS7) | (1<<TWS6) | (1<<TWS5) | (1<<TWS4) | (1<<TWS3)
#define TW_STATUS       (TWSR & TW_STATUS_MASK)
int neutralizeTime;
void i2c_init(void) {
    I2C_PULLUPS_DISABLE
  TWSR = 0;        // no prescaler => prescaler = 1
  TWBR = ((16000000L / 400000L) - 16) / 2; // change the I2C clock rate
  TWCR = 1<<TWEN;  // enable twi module, no interrupt
}
void waitTransmissionI2C() {
  uint8_t count = 255;
  while (count-->0 && !(TWCR & (1<<TWINT)) );
  if (count<2) { //we are in a blocking state => we don't insist
    TWCR = 0;  //and we force a reset on TWINT register
    neutralizeTime = micros(); //we take a timestamp here to neutralize the value during a short delay after the hard reset
  }
}

void i2c_rep_start(uint8_t address) {
  TWCR = (1<<TWINT) | (1<<TWSTA) | (1<<TWEN) | (1<<TWSTO); // send REAPEAT START condition
  waitTransmissionI2C(); // wait until transmission completed
 // checkStatusI2C(); // check value of TWI Status Register
  TWDR = address; // send device address
  TWCR = (1<<TWINT) | (1<<TWEN);
  waitTransmissionI2C(); // wail until transmission completed
 // checkStatusI2C(); // check value of TWI Status Register
}

void i2c_write(uint8_t data ) {	
  TWDR = data; // send data to the previously addressed device
  TWCR = (1<<TWINT) | (1<<TWEN);
  waitTransmissionI2C(); // wait until transmission completed
 // checkStatusI2C(); // check value of TWI Status Register
}

uint8_t i2c_readAck() {
  TWCR = (1<<TWINT) | (1<<TWEN) | (1<<TWEA);
  waitTransmissionI2C();
  return TWDR;
}

uint8_t i2c_readNak(void) {
  TWCR = (1<<TWINT) | (1<<TWEN);
  waitTransmissionI2C();
  return TWDR;
}
int     adc_value[8]   = { 0, 0, 0, 0, 0, 0, 0, 0 };
int gyrozero[3]={0,0,0};
int rawADC_ITG3200[6],rawADC_BMA180[6];
long adc_read_timeout=0;



// Constructors ////////////////////////////////////////////////////////////////
APM_ADC_Class::APM_ADC_Class()
{
}

// Public Methods //////////////////////////////////////////////////////////////
void APM_ADC_Class::Init(void)
{
 int i;
long gyrozeroL[3]={0,0,0};
//      Wire.begin();
//i2c_init();
//=== ITG3200 INIT

 delay(10);  
  TWBR = ((16000000L / 400000L) - 16) / 2; // change the I2C clock rate to 400kHz
 
   i2c_rep_start(0XD0+0);      // I2C write direction 
  i2c_write(0x3E);            // Power Management register
  i2c_write(0x80);            //   reset device
  i2c_write(0x16);            // register DLPF_CFG - low pass filter configuration & sample rate
  i2c_write(0x1e);            //   10Hz Low Pass Filter Bandwidth - Internal Sample Rate 1kHz
  i2c_write(0x3E);            // Power Management register
  i2c_write(0x01);            //   PLL with X Gyro reference
  delay(100);
  
  // gyro zero adjustment
i=0;  
adc_read_timeout=0;
  while(i<400)
  {while((millis()-adc_read_timeout)<3) ; // wait a pause
  adc_read_timeout=millis();
   i2c_rep_start(0XD0);     // I2C write direction // read ITG3200 400 times
  i2c_write(0X1D);         // Start multiple read
  i2c_rep_start(0XD0 +1);  // I2C read direction => 1
  for(uint8_t i = 0; i < 5; i++) {
  rawADC_ITG3200[i]=i2c_readAck();}
  rawADC_ITG3200[5]= i2c_readNak();
  gyrozeroL[2] +=  ((rawADC_ITG3200[0]<<8) | rawADC_ITG3200[1]); //g pitch
  gyrozeroL[1] +=  ((rawADC_ITG3200[2]<<8) | rawADC_ITG3200[3]); //g roll
  gyrozeroL[0] +=  ((rawADC_ITG3200[4]<<8) | rawADC_ITG3200[5]); //g yaw
  i++;}
gyrozero[2]=(gyrozeroL[2]+200)/399;
gyrozero[1]=(gyrozeroL[1]+200)/399;
gyrozero[0]=(gyrozeroL[0]+200)/399;
  

delay(10);

 //===BMA180 INIT
  i2c_rep_start(0x80+0);      // I2C write direction 
  i2c_write(0x0D);            // ctrl_reg0
  i2c_write(1<<4);            // Set bit 4 to 1 to enable writing
  i2c_rep_start(0x80+0);       
  i2c_write(0x35);            // 
  i2c_write(3<<1);            // range set to 3.  2730 1G raw data.  With /10 divisor on acc_ADC, more in line with other sensors and works with the GUI
  i2c_rep_start(0x80+0);
  i2c_write(0x20);            // bw_tcs reg: bits 4-7 to set bw
  i2c_write(1<<4);            // bw to 10Hz (low pass filter)

 delay(10);  
 
 // Sonar INIT
//=======================
//D49 (PORTL.0) = sonar input
//D47 (PORTL.2) = sonar Tx (trigger)
//The smaller altitude then lower the cycle time

 // 0.034 cm/micros
PORTL&=B11111010; 
DDRL&=B11111110;
DDRL|=B00000100;

//div64 = 4 us/bit
//resolution =0.136cm
//full range =90m 300ms
 // Using timer5
   //Remember the registers not declared here remains zero by default... 
  TCCR5A =0; //standard mode with overflow at A and OC B and C interrupts
  TCCR5B = (1<<CS11)|(1<<CS10); //Prescaler set to 64, resolution of 4us
  TIMSK5=B00100011; // ints: overflow, capture, compareA
  OCR5A=30000; // approx 40m limit, 150ms period
}

char sonar_meas=0;
int sonar_data=-1,sonic_range=-1;
ISR(TIMER5_COMPA_vect) // measurement is over, no edge detected, Set up Tx pin, offset 12 us
{if (sonar_meas==0) sonar_data=-1;PORTL|=B00000100;TCNT5=65533;}
ISR(TIMER5_OVF_vect) // next measurement, clear the Tx pin, 
{PORTL&=B11111011;sonar_meas=0;}
ISR(TIMER5_CAPT_vect) // measurement successful, wait 40ms, next measurement
{sonar_data=TCNT5;TCNT5=29990;sonar_meas=1;}




void i2c_Gyro_ACC_getADC () { // ITG3200 read data
uint8_t i;
  i2c_rep_start(0XD0);     // I2C write direction
  i2c_write(0X1D);         // Start multiple read
  i2c_rep_start(0XD0 +1);  // I2C read direction => 1
  for(uint8_t i = 0; i < 5; i++) {
  rawADC_ITG3200[i]=i2c_readAck();}
  rawADC_ITG3200[5]= i2c_readNak();


  i2c_rep_start(0x80);     // I2C write direction
  i2c_write(0x02);         // Start multiple read at reg 0x02 acc_x_lsb
  i2c_rep_start(0x80 +1);  // I2C read direction => 1
  for( i = 0; i < 5; i++) {
    rawADC_BMA180[i]=i2c_readAck();}
  rawADC_BMA180[5]= i2c_readNak();


  /*	On Ardupilot Mega Hardware, oriented as described above:
	Chennel 0 : yaw rate, r
	Channel 1 : roll rate, p
	Channel 2 : pitch rate, q
	Channel 3 : x/y gyro temperature
	Channel 4 : x acceleration, aX
	Channel 5 : y acceleration, aY
	Channel 6 : z acceleration, aZ
	Channel 7 : Differential pressure sensor port
*/
  adc_value[2] =-  (((rawADC_ITG3200[0]<<8) | rawADC_ITG3200[1])-gyrozero[2])/44; //g pitch
  adc_value[1] =  (((rawADC_ITG3200[2]<<8) | rawADC_ITG3200[3])-gyrozero[1])/44; //g roll
  adc_value[0] =  (((rawADC_ITG3200[4]<<8) | rawADC_ITG3200[5])-gyrozero[0])/44; //g yaw
  adc_value[5] = -(((rawADC_BMA180[1]<<8) | (rawADC_BMA180[0]))>>2)/10; //a roll
  adc_value[4] =  (((rawADC_BMA180[3]<<8) | (rawADC_BMA180[2]))>>2)/10; //a pitch
  adc_value[6] =  (((rawADC_BMA180[5]<<8) | (rawADC_BMA180[4]))>>2)/10; //a yaw

}



// Read one channel value
int APM_ADC_Class::Ch(unsigned char ch_num)         
{

if ( (millis()-adc_read_timeout )  > 3 )  //each read is spaced by 10ms else place old values
{  adc_read_timeout = millis();
 i2c_Gyro_ACC_getADC ();}
 else adc_read_timeout = millis();
if (ch_num==7) {
//range in centimeters=0.136 per tick
if (sonar_data==-1) return(-1);
else sonic_range=((long)(17408L*(long)sonar_data))>>7;
return(sonic_range);}
else return(adc_value[ch_num]);
}

// make one instance for the user to use
APM_ADC_Class APM_ADC;