/*
 ArduCopter v1.3 - August 2010
 www.ArduCopter.com
 Copyright (c) 2010.  All rights reserved.
 An Open Source Arduino based multicopter.
 
 This program is free software: you can redistribute it and/or modify 
 it under the terms of the GNU General Public License as published by 
 the Free Software Foundation, either version 3 of the License, or 
 (at your option) any later version. 
 
 This program is distributed in the hope that it will be useful, 
 but WITHOUT ANY WARRANTY; without even the implied warranty of 
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
 GNU General Public License for more details. 
 
 You should have received a copy of the GNU General Public License 
 along with this program. If not, see <http://www.gnu.org/licenses/>.
 */

  char signRzGyro;  
  float R;
  float RxEst = 0; // init acc in stable mode
  float RyEst = 0;
  float RzEst = 1;
  float Axz,Ayz;           //angles between projection of R on XZ/YZ plane and Z axis (in Radian)
  float RxAcc,RyAcc,RzAcc;         //projection of normalized gravitation force vector on x/y/z axis, as measured by accelerometer       
  float RxGyro,RyGyro,RzGyro;        //R obtained from last estimated value and gyro movement
  float wGyro = 50.0; // gyro weight/smooting factor
  float atanx,atany;
  float gyroFactor;


// **************
// Wii Motion Plus I2C
// **************
#define ROLL 0
#define PITCH 1
#define YAW 2

static uint16_t rawADC[6];
static int16_t gyroADC[3];
static int16_t accADC[3];

// Mask prescaler bits : only 5 bits of TWSR defines the status of each I2C request
#define TW_STATUS_MASK	(1<<TWS7) | (1<<TWS6) | (1<<TWS5) | (1<<TWS4) | (1<<TWS3)
#define TW_STATUS       (TWSR & TW_STATUS_MASK)

void i2c_rep_start(uint8_t address);
void i2c_write(uint8_t data );
uint8_t i2c_readAck();
uint8_t i2c_readNak(void);
uint8_t rawIMU ();
void initIMU(void);


void i2c_init(void) {
  PORTC |= 1<<4; // activate internal pull-ups PIN A4 for twi
  PORTC |= 1<<5; // activate internal pull-ups PIN A5 for twi
  TWSR = 0;        // no prescaler => prescaler = 1
  TWBR = ((16000000L / 400000) - 16) / 2; // change the I2C clock rate
  TWCR = 1<<TWEN;  // enable twi module, no interrupt
}
void waitTransmissionI2C() {
  uint8_t count = 255;
  while (count-->0 && !(TWCR & (1<<TWINT)) );
  if (count<2) { //we are in a blocking state => we don't insist
    TWCR = 0;  //and we force a reset on TWINT register
   // neutralizeTime = micros(); //we take a timestamp here to neutralize the value during a short delay after the hard reset
  }
}

void checkStatusI2C() {
  if ( (TW_STATUS & 0xF8) == 0xF8) { //TW_NO_INFO : this I2C error status indicates a wrong I2C communication.
    // WMP does not respond anymore => we do a hard reset. I did not find another way to solve it. It takes only 13ms to reset and init to WMP or WMP+NK
    TWCR = 0;
    /*digitalWrite(POWERPIN,0);
    delay(1);  
    digitalWrite(POWERPIN,1);
    delay(10);  */
    i2c_rep_start(0xA6);
    i2c_write(0xF0);
    i2c_write(0x55);
    i2c_rep_start(0xA6);
    i2c_write(0xFE);
    i2c_write(0x05);
    //neutralizeTime = micros(); //we take a timestamp here to neutralize the WMP or WMP+NK values during a short delay (20ms) after the hard reset
  }
}
void i2c_rep_start(uint8_t address) {
  TWCR = (1<<TWINT) | (1<<TWSTA) | (1<<TWEN) | (1<<TWSTO); // send REAPEAT START condition
  waitTransmissionI2C(); // wait until transmission completed
  checkStatusI2C(); // check value of TWI Status Register
  TWDR = address; // send device address
  TWCR = (1<<TWINT) | (1<<TWEN);
  waitTransmissionI2C(); // wail until transmission completed
  checkStatusI2C(); // check value of TWI Status Register
}

void i2c_write(uint8_t data ) {	
  TWDR = data; // send data to the previously addressed device
  TWCR = (1<<TWINT) | (1<<TWEN);
  waitTransmissionI2C(); // wait until transmission completed
  checkStatusI2C(); // check value of TWI Status Register
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



void initI2C(void) {
  i2c_init();
  delay(250);
  i2c_rep_start(0xA6 + 0);//I2C write direction => 0
  i2c_write(0xF0); 
  i2c_write(0x55); 
  delay(250);
  i2c_rep_start(0xA6 + 0);//I2C write direction => 0
  i2c_write(0xFE); 
  i2c_write(0x05); 
  delay(250);
}

void getI2C() {
  i2c_rep_start(0xA4 + 0);//I2C write direction => 0
  i2c_write(0x00);
  i2c_rep_start(0xA4 + 1);//I2C read direction => 1
  for(uint8_t i = 0; i < 5; i++) {
    rawADC[i]=i2c_readAck();}
  rawADC[5]= i2c_readNak();
}

uint8_t updateIMU() {
  static int32_t g[3];
  static int32_t a[3];
  uint8_t axis;
  
  if (rawIMU()) { //gyro
    gyroADC[PITCH] = (rawADC[4]&0x02)>>1  ? gyroADC[PITCH] : gyroADC[PITCH] * 4;  //we detect here the slow of fast mode WMP gyros values 
    gyroADC[ROLL]  = (rawADC[3]&0x01)     ? gyroADC[ROLL]  : gyroADC[ROLL] * 4;   //the ratio 1/4 is not exactly the IDG600 or IFG650 specification 
    gyroADC[YAW]   = (rawADC[3]&0x02)>>1  ? gyroADC[YAW]   : gyroADC[YAW] * 4;    //http://www.assembla.com/wiki/show/alicewiimotionplus/slow_and_fast_modes
    return 1;
  } else { //nunchuk
    return 0;
  }
}


uint8_t rawIMU () { //if the WMP or NK are oriented differently, it can be changed here
  getI2C();
  if ( rawADC[5]&0x02 ) {// motion plus data
    gyroADC[PITCH]  =  ( ((rawADC[4]>>2)<<8) + rawADC[1] );
    gyroADC[ROLL]   =  ( ((rawADC[5]>>2)<<8) + rawADC[2] );
    gyroADC[YAW]    =  ( ((rawADC[3]>>2)<<8) + rawADC[0] );
    return 1;
  } else { //nunchuk data
    accADC[PITCH] =  ( (rawADC[2]<<2)        + ((rawADC[5]>>3)&0x2) );
    accADC[ROLL]  =   ( (rawADC[3]<<2)        + ((rawADC[5]>>4)&0x2) );
    accADC[YAW]   =  ( ((rawADC[4]&0xFE)<<2) + ((rawADC[5]>>5)&0x6) );
    return 0;
  }
}

void initIMU(void) {
  uint8_t numberAccRead = 0;
  initI2C();  
  for(uint8_t i=0;i<100;i++) {
    delay(3);
    if (rawIMU() == 0) numberAccRead++; // we detect here is nunchuk extension is available
  }
  if (numberAccRead>30)
    //nunchukPresent = 1;

  delay(10);
}
// Read accels and gyros
void Update_Sensors(void)
{
    for(char i = 0; i < 6; i++)
    {
      Sensor_Data_Raw[i] = Raw_Sensor_Read(i); //for debugging
      if (SENSOR_SIGN[i] < 0)
      {
        Sensor_Input[i] = Sensor_Offset[i] - Sensor_Data_Raw[i];
      }
      else
      {
        Sensor_Input[i] = Sensor_Data_Raw[i] - Sensor_Offset[i];
      }
    }
}





// Remap hardware functions to autopilot functions
int Raw_Sensor_Read(char n)
{
#ifdef USE_WII
  //Read values from Wii sensors
  /*if(n == GYRO_ROLL) return APM_Wii.Ch(0);
  if(n == GYRO_PITCH)return APM_Wii.Ch(1);
  if(n == GYRO_YAW)  return APM_Wii.Ch(2);
  if(n == ACCEL_X)   return APM_Wii.Ch(3);
  if(n == ACCEL_Y)   return APM_Wii.Ch(4);
  if(n == ACCEL_Z)   return APM_Wii.Ch(5);*/
  //Read values from Wii sensors
  if(n == GYRO_ROLL) 
  {
    updateIMU();
    return gyroADC[PITCH] / 6;
  }
  if(n == GYRO_PITCH)return gyroADC[ROLL]/ 6;
  if(n == GYRO_YAW)  return gyroADC[YAW] / 6;
  if(n == ACCEL_X)
  {
    updateIMU();
    return accADC[PITCH] * 2;
  }
  if(n == ACCEL_Y)   return accADC[ROLL] * 2;
  if(n == ACCEL_Z)   return accADC[YAW]  * 2;
#else
  //Read values from oilpan sensors
  if(n == GYRO_ROLL) return APM_ADC.Ch(1);
  if(n == GYRO_PITCH)return APM_ADC.Ch(2);
  if(n == GYRO_YAW)  return APM_ADC.Ch(0);
  if(n == ACCEL_X)   return APM_ADC.Ch(4);
  if(n == ACCEL_Y)   return APM_ADC.Ch(5);
  if(n == ACCEL_Z)   return APM_ADC.Ch(6);
#endif

  return 0;
}
  
  
void Calibrate_Gyro_Offsets(void)
{
  float aux_float[3];
  unsigned char i, j = 0;
  
  //Preload gyro offset filter with some rough values  
  aux_float[0] = Raw_Sensor_Read(GYRO_ROLL);    
  aux_float[1] = Raw_Sensor_Read(GYRO_PITCH);
  aux_float[2] = Raw_Sensor_Read(GYRO_YAW);

  // Take the gyro offset values
  for(i = 0; i < 255; i++)
  {
    for(char y = GYRO_ROLL; y < (GYRO_ROLL + 3); y++)
    {  
      aux_float[y] = aux_float[y] * 0.9 + Raw_Sensor_Read(y) * 0.1;
    }
  
    delay(10);
    
    // Runnings lights effect to let user know that we are taking mesurements
    if(j == 0) 
    {
      digitalWrite(LED_Green, HIGH);
      digitalWrite(LED_Yellow, LOW);
      digitalWrite(LED_Red, LOW);
    } 
    else if (j == 1) 
    {
      digitalWrite(LED_Green, LOW);
      digitalWrite(LED_Yellow, HIGH);
      digitalWrite(LED_Red, LOW);
    } 
    else 
    {
      digitalWrite(LED_Green, LOW);
      digitalWrite(LED_Yellow, LOW);
      digitalWrite(LED_Red, HIGH);
    }
    
    if((i % 5) == 0) j++;
    if(j >= 3) j = 0;
  }
  
  digitalWrite(LED_Green, LOW);
  digitalWrite(LED_Yellow, LOW);
  digitalWrite(LED_Red, LOW);

  for(char y = GYRO_ROLL; y < (GYRO_ROLL + 3); y++)
  {  
    Sensor_Offset[y] = aux_float[y];
  }
  
#ifndef CONFIGURATOR
    for(i = 0; i < 6; i++)
    {
      SerPri("Sensor_Offset[]:");
      SerPriln(Sensor_Offset[i]);
    }
    SerPri("Yaw neutral value:");
    SerPri(yaw_mid);
#endif
}


/*
#ifdef UseBMP

void read_baro(void)
{
  float tempPresAlt;
  
  tempPresAlt = float(APM_BMP085.Press)/101325.0;
  tempPresAlt = pow(tempPresAlt, 0.190295);
  if (press_alt==0)
    press_alt = (1.0 - tempPresAlt) * 4433000;      // Altitude in cm
  else
    press_alt = press_alt*0.9 + ((1.0 - tempPresAlt) * 443300);  // Altitude in cm (filtered)
}

#endif
*/
// This filter limits the max difference between readings and also aply an average filter
int Sonar_Sensor_Filter(long new_value, int old_value, int max_diff)
{
  int diff_values;
  int result;
  
  if (old_value==0)     // Filter is not initialized (no old value)
    return(new_value);
  diff_values = new_value - old_value;      // Difference with old reading
  if (diff_values>max_diff)   
    result = old_value+max_diff;    // We limit the max difference between readings
  else
    {
    if (diff_values<-max_diff)
      result = old_value-max_diff;        // We limit the max difference between readings
    else
      result = (new_value+old_value)>>1;  // Small filtering (average filter)
    }
  return(result); 
}

// This filter limits the max difference between readings and also aply an average filter
long BMP_Sensor_Filter(long new_value, long old_value, int max_diff)
{
  long diff_values;
  long result;
  
  if (old_value==0)     // Filter is not initialized (no old value)
    return(new_value);
  diff_values = new_value - old_value;      // Difference with old reading
  if (diff_values>max_diff)   
    result = old_value+max_diff;    // We limit the max difference between readings
  else
    {
    if (diff_values<-max_diff)
      result = old_value-max_diff;        // We limit the max difference between readings
    else
      result = (new_value+old_value)>>1;  // Small filtering (average filter)
    }
  return(result); 
}

void ReadSCP1000(void) {
}

/*#ifdef UseBMP
void read_airpressure(void){
  double x;

  APM_BMP085.Read(); 	//Get new data from absolute pressure sensor
  abs_press = APM_BMP085.Press;
  abs_press_filt = (abs_press); // + 2l * abs_press_filt) / 3l;		//Light filtering
  //temperature = (temperature * 9 + temp_unfilt) / 10;    We will just use the ground temp for the altitude calculation	 

  double p = (double)abs_press_gnd / (double)abs_press_filt;
  double temp = (float)ground_temperature / 10.f + 273.15f;
  x = log(p) * temp * 29271.267f;
  //x = log(p) * temp * 29.271267 * 1000;
  press_alt = (int)(x / 10) + ground_alt;		// Pressure altitude in centimeters
  //  Need to add comments for theory.....
}
#endif
*/

#ifdef UseAirspeed
void read_airspeed(void) {
#if GCS_PROTOCOL != 3 // Xplane will supply the airspeed
  airpressure_raw = ((float)analogRead(AIRSPEED_PIN) * .25) + (airpressure_raw * .75);
  airpressure = (int)airpressure_raw - airpressure_offset;
  airspeed = sqrt((float)airpressure / AIRSPEED_RATIO);
#endif
  airspeed_error = airspeed_cruise - airspeed;
}
#endif


#ifdef BATTERY_EVENT
void read_battery(void)
{
  battery_voltage = BATTERY_VOLTAGE(analogRead(BATTERY_ADC));
  
  //Check to see if voltage is below low voltage threshold,
  //but don't sound alarm if no battery is connected
  if((battery_voltage < LOW_VOLTAGE) && (battery_voltage > 3))
  {
    //Sound alarm
    digitalWrite(LOW_BATTERY_OUT, HIGH);
  }
  else
  {
    //Silence
    digitalWrite(LOW_BATTERY_OUT, LOW);
  }
}
#endif


#ifdef UseAirspeed
void zero_airspeed(void)
{
  airpressure_raw = analogRead(AIRSPEED_PIN);
  for(int c=0; c < 80; c++){
    airpressure_raw = (airpressure_raw * .90) + ((float)analogRead(AIRSPEED_PIN) * .10);	
  }
  airpressure_offset = airpressure_raw;	
}
#endif

