/*
	APM_RC.cpp - Radio Control Library for Ardupilot Mega. Arduino
	Code by Jordi Muñoz and Jose Julio. DIYDrones.com

	This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

	RC Input : PPM signal on IC4 pin
	RC Output : 11 Servo outputs (standard 20ms frame)

	Methods:
		Init() : Initialization of interrupts an Timers
		OutpuCh(ch,pwm) : Output value to servos (range : 900-2100us) ch=0..10
		InputCh(ch) : Read a channel input value.  ch=0..7
		GetState() : Returns the state of the input. 1 => New radio frame to process
		             Automatically resets when we call InputCh to read channels
		
*/

//#define PPM_SUM



#include "APM_RC.h"

#include <avr/interrupt.h>
#include "WProgram.h"

#if !defined(__AVR_ATmega1280__)
# error Please check the Tools/Board menu to ensure you have selected Arduino Mega as your target.
#else

// Variable definition for Input Capture interrupt
volatile unsigned int ICR4_old;
volatile unsigned char PPM_Counter=0;
volatile unsigned int PWM_RAW[8] = {2400,2400,2400,2400,2400,2400,2400,2400};
volatile unsigned char radio_status=0;

#ifndef PPM_SUM

// ******************
// rc functions split channels
// ******************
#define MINCHECK 1100
#define MAXCHECK 1900

volatile int16_t failsafeCnt = 0;

volatile uint16_t rcPinValue[8] = {1500,1500,1500,1500,1500,1500,1500,1500}; // interval [1000;2000]
static int16_t rcData[8] ; // interval [1000;2000]
static int16_t rcCommand[4] ; // interval [1000;2000] for THROTTLE and [-500;+500] for ROLL/PITCH/YAW 
static int16_t rcHysteresis[8] ;
static int16_t rcData4Values[8][4];


// ***PPM SUM SIGNAL***
volatile uint16_t rcValue[8] = {1500,1500,1500,1500,1500,1500,1500,1500}; // interval [1000;2000]

// Configure each rc pin for PCINT
void configureReceiver() {
    for (uint8_t chan = 0; chan < 8; chan++)
      for (uint8_t a = 0; a < 4; a++)
        rcData4Values[chan][a] = 1500; //we initiate the default value of each channel. If there is no RC receiver connected, we will see those values
      // PCINT activated only for specific pin inside [A8-A15]
      DDRK = 0;  // defined PORTK as a digital port ([A8-A15] are consired as digital PINs and not analogical)
      PORTK   = (1<<0) | (1<<1) | (1<<2) | (1<<3) | (1<<4) | (1<<5) | (1<<6) | (1<<7); //enable internal pull ups on the PINs of PORTK
      PCMSK2 |= (1<<0) | (1<<1) | (1<<2) | (1<<3) | (1<<4) | (1<<5) | (1<<6) | (1<<7);
      PCICR   = 1<<2; // PCINT activated only for PORTK dealing with [A8-A15] PINs
}

ISR(PCINT2_vect) { //this ISR is common to every receiver channel, it is call everytime a change state occurs on a digital pin [D2-D7]
  uint8_t mask;
  uint8_t pin;
  uint16_t cTime,dTime;
  static uint16_t edgeTime[8];
  static uint8_t PCintLast;

    pin = PINK;             // PINK indicates the state of each PIN for the arduino port dealing with [A8-A15] digital pins (8 bits variable)
  mask = pin ^ PCintLast;   // doing a ^ between the current interruption and the last one indicates wich pin changed
  sei();                    // re enable other interrupts at this point, the rest of this interrupt is not so time critical and can be interrupted safely
  PCintLast = pin;          // we memorize the current state of all PINs [D0-D7]

  cTime = micros();         // micros() return a uint32_t, but it is not usefull to keep the whole bits => we keep only 16 bits
  
  // mask is pins [D0-D7] that have changed // the principle is the same on the MEGA for PORTK and [A8-A15] PINs
  // chan = pin sequence of the port. chan begins at D2 and ends at D7
  if (mask & 1<<2)           //indicates the bit 2 of the arduino port [D0-D7], that is to say digital pin 2, if 1 => this pin has just changed
    if (!(pin & 1<<2)) {     //indicates if the bit 2 of the arduino port [D0-D7] is not at a high state (so that we match here only descending PPM pulse)
      dTime = cTime-edgeTime[2]; if (900<dTime && dTime<2200) rcPinValue[2] = dTime; // just a verification: the value must be in the range [1000;2000] + some margin
    } else edgeTime[2] = cTime;    // if the bit 2 of the arduino port [D0-D7] is at a high state (ascending PPM pulse), we memorize the time
  if (mask & 1<<4)   //same principle for other channels   // avoiding a for() is more than twice faster, and it's important to minimize execution time in ISR
    if (!(pin & 1<<4)) {
      dTime = cTime-edgeTime[4]; if (900<dTime && dTime<2200) rcPinValue[4] = dTime;
    } else edgeTime[4] = cTime;
  if (mask & 1<<5)
    if (!(pin & 1<<5)) {
      dTime = cTime-edgeTime[5]; if (900<dTime && dTime<2200) rcPinValue[5] = dTime;
    } else edgeTime[5] = cTime;
  if (mask & 1<<6)
    if (!(pin & 1<<6)) {
      dTime = cTime-edgeTime[6]; if (900<dTime && dTime<2200) rcPinValue[6] = dTime;
    } else edgeTime[6] = cTime;
  if (mask & 1<<7)
    if (!(pin & 1<<7)) {
      dTime = cTime-edgeTime[7]; if (900<dTime && dTime<2200) rcPinValue[7] = dTime;
    } else edgeTime[7] = cTime;
    if (mask & 1<<0)    
      if (!(pin & 1<<0)) {
        dTime = cTime-edgeTime[0]; if (900<dTime && dTime<2200) rcPinValue[0] = dTime; 
      } else edgeTime[0] = cTime; 
    if (mask & 1<<1)      
      if (!(pin & 1<<1)) {
        dTime = cTime-edgeTime[1]; if (900<dTime && dTime<2200) rcPinValue[1] = dTime; 
      } else edgeTime[1] = cTime;
    if (mask & 1<<3)
      if (!(pin & 1<<3)) {
        dTime = cTime-edgeTime[3]; if (900<dTime && dTime<2200) rcPinValue[3] = dTime;
      } else edgeTime[3] = cTime;
}
/* RC standard matrix (we are using analog inputs A8..A15 of MEGA board)
0	Aileron
1	Elevator
2	Throttle
3	Rudder
4	RX CH 5 Gear
5	RX CH 6 Flaps
5.bis	RX CH 6 Flaps 3 Switch
6	RX CH7
7 *)	RX CH8
*/
//MultiWii compatibility layout:
/*
THROTTLEPIN                  //PIN 62 =  PIN A8
ROLLPIN                      //PIN 63 =  PIN A9
PITCHPIN                     //PIN 64 =  PIN A10
YAWPIN                       //PIN 65 =  PIN A11
AUX1PIN                      //PIN 66 =  PIN A12
AUX2PIN                      //PIN 67 =  PIN A13
CAM1PIN                      //PIN 68 =  PIN A14
CAM2PIN                      //PIN 69 =  PIN A15
*/
static uint8_t pinRcChannel[8] = {1, 2, 0, 3, 4,5,6,7}; // mapped multiwii to APM layout

uint16_t readRawRC(uint8_t chan) {
  uint16_t data;
  uint8_t oldSREG;
  oldSREG = SREG;
  cli(); // Let's disable interrupts
    data = rcPinValue[pinRcChannel[chan]]; // Let's copy the data Atomically
  SREG = oldSREG;
  sei();// Let's enable the interrupts
  return data; // We return the value correctly copied when the IRQ's where disabled
}
  
void computeRC() {
  static uint8_t rc4ValuesIndex = 0;
  uint8_t chan,a;

  rc4ValuesIndex++;
  for (chan = 0; chan < 8; chan++) {
    rcData4Values[chan][rc4ValuesIndex%4] = readRawRC(chan);
    rcData[chan] = 0;
    for (a = 0; a < 4; a++)
      rcData[chan] += rcData4Values[chan][a];
    rcData[chan]= (rcData[chan]+2)/4;
    if ( rcData[chan] < rcHysteresis[chan] -3)  rcHysteresis[chan] = rcData[chan]+2;
    if ( rcData[chan] > rcHysteresis[chan] +3)  rcHysteresis[chan] = rcData[chan]-2;
  }
}





#else

//######################### END RC split channels






/****************************************************
   Input Capture Interrupt ICP4 => PPM signal read
 ****************************************************/
ISR(TIMER4_CAPT_vect)  
{
  unsigned int Pulse;
  unsigned int Pulse_Width;
  
  Pulse=ICR4;
  if (Pulse<ICR4_old)     // Take care of the overflow of Timer4 (TOP=40000)
    Pulse_Width=(Pulse + 40000)-ICR4_old;  //Calculating pulse 
  else
    Pulse_Width=Pulse-ICR4_old;            //Calculating pulse 
  if (Pulse_Width>8000)   // SYNC pulse?
    PPM_Counter=0;
  else
    {
    PPM_Counter &= 0x07;  // For safety only (limit PPM_Counter to 7)
    PWM_RAW[PPM_Counter++]=Pulse_Width;  //Saving pulse. 
    if (PPM_Counter >= NUM_CHANNELS)
      radio_status = 1;
    }
  ICR4_old = Pulse;
}

#endif
// Constructors ////////////////////////////////////////////////////////////////

APM_RC_Class::APM_RC_Class()
{
}



// PWM 16 outputs
//2.5 ms = 8 channels per group
//Servo outputs D2..D7 group #1 (6 pcs)
/*
D	Port PWM
2	e4	0
3	e5	1
4	g5	2
5	e3	3
6	h3	4
7	h4	5
8	h5	6
9	h6	7
//2nd group
22	a0	8
23	a1	9
24	a2	10
25	a3	11
26	a4	12
27	a5	13
28	a6	14
29	a7	15
*/

// Public Methods //////////////////////////////////////////////////////////////
void APM_RC_Class::Init(void)
{
//We are using JUST 1 timer1 for 16 PPM outputs!!! (Syberian)
  // Init PWM Timer 1
  pinMode(2,OUTPUT);
  pinMode(3,OUTPUT);
  pinMode(4,OUTPUT);
  pinMode(5,OUTPUT);
  pinMode(6,OUTPUT);
  pinMode(7,OUTPUT);
  pinMode(8,OUTPUT);
  pinMode(9,OUTPUT);
  pinMode(22,OUTPUT);
  pinMode(23,OUTPUT);
  pinMode(24,OUTPUT);
  pinMode(25,OUTPUT);
  pinMode(26,OUTPUT);
  pinMode(27,OUTPUT);
  pinMode(28,OUTPUT);
  pinMode(29,OUTPUT);

  //Remember the registers not declared here remains zero by default... 
  TCCR3A =0; //standard mode with overflow at A and OC B and C interrupts
  TCCR3B = (1<<CS11)|(1<<WGM12); //Prescaler set to 8, that give us a resolution of 0.5us, read page 134 of data sheet
  TIMSK3=B00001110;
  OCR3A=5000; // 2500 us 8 slots
  OCR3B = 3000; //PB5, none
  OCR3C = 3000; //PB6, OUT2
//  ICR1 = 40000; //50hz freq...Datasheet says  (system_freq/prescaler)/target frequency. So (16000000hz/8)/50hz=40000,
  
  #ifdef PPM_SUM
  
  TIMSK4 |= (1<<ICIE4); // Enable Input Capture interrupt. Timer interrupt mask
 #else
 configureReceiver();
 #endif;
}


int OCRxx1[8]={3000,3000,3000,3000,3000,3000,3000,3000,};
int OCRxx2[8]={3000,3000,3000,3000,3000,3000,3000,3000,};
char OCRstate=7;
/*
D	Port PWM
2	e4	0
3	e5	1
4	g5	2
5	e3	3
6	h3	4
7	h4	5
8	h5	6
9	h6	7
//2nd gro6up
22	a0	8
23	a1	9
24	a2	10
25	a3	11
26	a4	12
27	a5	13
28	a6	14
29	a7	15
*/

ISR(TIMER3_COMPA_vect)
{ // set the corresponding pin to 1
	OCRstate++;
	OCRstate&=7;
switch (OCRstate)
	{case 0:PORTE|=(1<<4);break;
	case 1: PORTE|=(1<<5);break;
	case 2: PORTG|=(1<<5);break;
	case 3: PORTE|=(1<<3);break;
	case 4: PORTH|=(1<<3);break;
	case 5: PORTH|=(1<<4);break;
	case 6: PORTH|=(1<<5);break;
	case 7: PORTH|=(1<<6);break;
	}
switch (OCRstate) // 2nd group
	{case 0:PORTA|=(1<<0); break;
	case 1: PORTA|=(1<<1);break;
	case 2: PORTA|=(1<<2);break;
	case 3: PORTA|=(1<<3);break;
	case 4: PORTA|=(1<<4);break;
	case 5: PORTA|=(1<<5);break;
	case 6: PORTA|=(1<<6);break;
	case 7: PORTA|=(1<<7);break;
	}
	OCR3B=OCRxx1[OCRstate];
	OCR3C=OCRxx2[OCRstate];
	
}
ISR(TIMER3_COMPB_vect) // CLEAR PORTS IN GROUP 1
{
switch (OCRstate)
	{case 0:PORTE&=(1<<4)^255; break;
	case 1: PORTE&=(1<<5)^255;break;
	case 2: PORTG&=(1<<5)^255;break;
	case 3: PORTE&=(1<<3)^255;break;
	case 4: PORTH&=(1<<3)^255;break;
	case 5: PORTH&=(1<<4)^255;break;
	case 6: PORTH&=(1<<5)^255;break;
	case 7: PORTH&=(1<<6)^255;break;
	}
}


ISR(TIMER3_COMPC_vect) //clear ports in group2
{
switch (OCRstate)
	{case 0:PORTA&=(1<<0)^255; break;
	case 1: PORTA&=(1<<1)^255;break;
	case 2: PORTA&=(1<<2)^255;break;
	case 3: PORTA&=(1<<3)^255;break;
	case 4: PORTA&=(1<<4)^255;break;
	case 5: PORTA&=(1<<5)^255;break;
	case 6: PORTA&=(1<<6)^255;break;
	case 7: PORTA&=(1<<7)^255;break;
	}
}














void APM_RC_Class::OutputCh(unsigned char ch, int pwm)
{
  pwm=constrain(pwm,MIN_PULSEWIDTH,MAX_PULSEWIDTH);
  pwm<<=1;   // pwm*2;
 
 switch(ch)
  {
    case 0:  
    case 1:  
    case 2:  
    case 3:  
    case 4:  
    case 5:  
    case 6:  
    case 7:  
			OCRxx1[ch]=pwm; break;  //ch7
    case 8: 
    case 9: 
    case 10:
    case 11:
    case 12:
    case 13:
    case 14:
    case 15: OCRxx2[ch-8]=pwm;break;
  } 
}

int APM_RC_Class::InputCh(unsigned char ch)
{
  int result;
  int result2;
  
  // Because servo pulse variables are 16 bits and the interrupts are running values could be corrupted.
  // We dont want to stop interrupts to read radio channels so we have to do two readings to be sure that the value is correct...
#ifdef PPM_SUM
  result =  PWM_RAW[ch]>>1;  // Because timer runs at 0.5us we need to do value/2
  result2 =  PWM_RAW[ch]>>1;
  if (result != result2)
    result =  PWM_RAW[ch]>>1;   // if the results are different we make a third reading (this should be fine)
#else
result=readRawRC(ch); 
#endif;
  
  // Limit values to a valid range
  result = constrain(result,MIN_PULSEWIDTH,MAX_PULSEWIDTH);
  radio_status=1; // Radio channel read
  return(result);
}

unsigned char APM_RC_Class::GetState(void)
{
#ifdef PPM_SUM
  return(radio_status);
#else
return(1);// always 1
#endif
}

// InstantPWM implementation
// This function forces the PWM output (reset PWM) on Out0 and Out1 (Timer5). For quadcopters use
void APM_RC_Class::Force_Out0_Out1(void)
{
//  if (TCNT5>5000)  // We take care that there are not a pulse in the output
//    TCNT5=39990;   // This forces the PWM output to reset in 5us (10 counts of 0.5us). The counter resets at 40000
}
// This function forces the PWM output (reset PWM) on Out2 and Out3 (Timer1). For quadcopters use
void APM_RC_Class::Force_Out2_Out3(void)
{
  }
// This function forces the PWM output (reset PWM) on Out6 and Out7 (Timer3). For quadcopters use
void APM_RC_Class::Force_Out6_Out7(void)
{
 }

// make one instance for the user to use
APM_RC_Class APM_RC;

#endif // defined(ATMega1280)
