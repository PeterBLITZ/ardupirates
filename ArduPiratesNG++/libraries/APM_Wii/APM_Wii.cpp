/*
	APM_Wii.cpp - Wii Motion+ and Nunchuck Library for Ardupilot Mega.
	Written by Paul Jones.

	This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

*/
extern "C" {
  // AVR LibC Includes
  #include <inttypes.h>
  #include <avr/interrupt.h>
  #include "WConstants.h"
}

#include <Wire.h>
#include "APM_Wii.h"

#define GYRO_ROLL  0
#define GYRO_PITCH 1
#define GYRO_YAW   2
#define ACCEL_X    3
#define ACCEL_Y    4
#define ACCEL_Z    5


#define TWI_FREQ 400000L


int Data[6];



// Constructors ////////////////////////////////////////////////////////////////
APM_Wii::APM_Wii()
{
}


// Public Methods //////////////////////////////////////////////////////////////
void APM_Wii::Init(void)
{
  unsigned char tmp;
  
  //Init WM+ and Nunchuk
  Wire.begin();
  Wire.beginTransmission(0x53);
  Wire.send(0xFE);
  Wire.send(0x05);
  Wire.endTransmission();
  delay(100);
  Wire.beginTransmission(0x53);
  Wire.send(0xF0);
  Wire.send(0x55);
  Wire.endTransmission();
  delay(100);
}


// Read one channel value
int APM_Wii::Ch(unsigned char ch)         
{
	//Update every time first axis of sensor is read
	if(ch == 0) FetchData();
	
	//Remap channel numbers and scale values to match DIY IMU board
	switch(ch)
	{
		case 0:	return [GYRO_YAW  ] / 6; break;
		case 1:	return [GYRO_ROLL ] / 6; break;
		case 2:	return [GYRO_PITCH] / 6; break;
		case 4:	return [ACCEL_X] * 2; break;
		case 5:	return [ACCEL_Y] * 2; break;
		case 6:	return [ACCEL_Z] * 2; break;
	}
	
	return 0;
}


//Read the data via i2c from Wii devices
void APM_Wii::FetchData(void) {
  unsigned char buffer[6];

	for(unsigned char j = 0; j < 2; j++) 
	{
    Wire.beginTransmission(0x52);
    Wire.send(0);
    Wire.endTransmission();
    Wire.requestFrom(0x52, 6);

    for(unsigned char i = 0; i < 6; i++)
	{
      buffer[i] = Wire.receive();
	}

		if (buffer[5] & 0x02) //Wii Motion+ Gyro
		{
		  Data[GYRO_ROLL] =((buffer[4] >> 2) << 8) +  buffer[1];
		  Data[GYRO_PITCH]=((buffer[5] >> 2) << 8) +  buffer[2];
		  Data[GYRO_YAW]  =((buffer[3] >> 2) << 8) +  buffer[0];
		  Data[GYRO_ROLL] = (buffer[4] & 0x02) >> 1 ? Data[GYRO_ROLL] : Data[GYRO_ROLL] * 4;  //we detect here the slow of fast mode WMP gyros values 
		  Data[GYRO_PITCH]= (buffer[3] & 0x01)      ? Data[GYRO_PITCH]: Data[GYRO_PITCH]* 4;  //the ratio 1/4 is not exactly the IDG600 or IFG650 specification 
		  Data[GYRO_YAW]  = (buffer[3] & 0x02) >> 1 ? Data[GYRO_YAW]  : Data[GYRO_YAW]  * 4;  //http://www.assembla.com/wiki/show/alicewiimotionplus/slow_and_fast_modes
		}
		else //WiiNunchuk Accelerometer
		{
		  Data[ACCEL_X] = ((buffer[2] << 2) + ((buffer[5] >> 3) & 0x2));
		  Data[ACCEL_Y] = ((buffer[3] << 2) + ((buffer[5] >> 4) & 0x2));
		  Data[ACCEL_Z] =(((buffer[4] & 0xFE) << 2) + ((buffer[5] >> 5) & 0x6));
		}
	}
}
