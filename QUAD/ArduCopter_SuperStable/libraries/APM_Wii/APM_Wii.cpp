/*
	APM_Wii.cpp - Wii Motion+ and Nunchuck Library for Ardupilot Mega.
	Library Code by Paul Jones, adapted from the AeroQuad Project

    Original code written by lamarche_mathieu
    Modifications by jihlein 

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


int Data[6];



// Constructors ////////////////////////////////////////////////////////////////
APM_Wii_Class::APM_Wii_Class()
{
}


// Public Methods //////////////////////////////////////////////////////////////
void APM_Wii_Class::Init(void)
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
int APM_Wii_Class::Ch(byte ch)         
{
	//Update every time first chanel is read
	if(ch == 0) FetchData();
	
	return Data[ch];
}


//Read the data via i2c from Wii devices
void APM_Wii_Class::FetchData(void) {
  byte i, j, buffer[6];

  for(j = 0; j < 2; j++) 
  {
    Wire.beginTransmission(0x52);
    Wire.send(0);
    Wire.endTransmission();
    Wire.requestFrom(0x52, 6);

    for(i = 0; i < 6; i++)
	{
      buffer[i] = Wire.receive();
	}
    
	if (buffer[5] & 0x02) //Wii Motion+ Gyro
	{
      Data[GYRO_ROLL] = (((buffer[4] >> 2) << 8) +  buffer[1]) / 16;
      Data[GYRO_PITCH]= (((buffer[5] >> 2) << 8) +  buffer[2]) / 16;
      Data[GYRO_YAW]  =-(((buffer[3] >> 2) << 8) +  buffer[0]) / 16;
    }
    else //WiiNunchuk Accelerometer
	{
      Data[ACCEL_X] = (buffer[2] << 1) | ((buffer[5] >> 4) & 0x01);
      Data[ACCEL_Y] = (buffer[3] << 1) | ((buffer[5] >> 5) & 0x01);
      Data[ACCEL_Z] = buffer[4];
      Data[ACCEL_Z] = Data[ACCEL_Z] << 1;
      Data[ACCEL_Z] = Data[ACCEL_Z] & 0xFFFC;
      Data[ACCEL_Z] = Data[ACCEL_Z] | ((buffer[5] >> 6) & 0x03);
    }
  }
}



// make one instance for the user to use
APM_Wii_Class APM_Wii;