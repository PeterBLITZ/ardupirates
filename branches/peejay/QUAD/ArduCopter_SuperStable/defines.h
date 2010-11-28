/*
  ArduCopter 1.3 - August 2010
  www.ArduCopter.com
  Copyright (c) 2010. All rights reserved.
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

#include "ArduPirates_Setup.h"

// Quick and easy hack to change FTDI Serial output to Telemetry port. Just activate #define IsXBEE some lines earlier
#ifndef IsXBEE
  #define SerBau  115200
  #define SerPri  Serial.print
  #define SerPriln Serial.println
  #define SerAva  Serial.available
  #define SerRea  Serial.read
  #define SerFlu  Serial.flush
  #define SerBeg  Serial.begin
  #define SerPor  "FTDI"
  #else
  #define SerBau  115200
  #define SerPri  Serial3.print
  #define SerPriln Serial3.println
  #define SerAva  Serial3.available
  #define SerRea  Serial3.read   
  #define SerFlu  Serial3.flush
  #define SerBeg  Serial3.begin
  #define SerPor  "Telemetry"
#endif


/*******************************************************************/
// ArduPilot Mega specific hardware and software settings
// 
// DO NOT EDIT unless you are absolytely sure of your doings. 
// User configurable settings are on UserConfig.h
/*******************************************************************/

/**************************************************************/
// Special features that might disapear in future releases

/* APM Hardware definitions */
#define LED_Yellow 36
#define LED_Red 35
#define LED_Green 37
#define RELE_pin 47
#define SW1_pin 41
#define SW2_pin 40

  
/* APM Hardware definitions, END */

/* General definitions */

#define TRUE 1
#define FALSE 0
#define ON 1
#define OFF 0


// ADC : Voltage reference 3.3v / 12bits(4096 steps) => 0.8mV/ADC step
// ADXL335 Sensitivity(from datasheet) => 330mV/g, 0.8mV/ADC step => 330/0.8 = 412
// Tested value : 408
#define GRAVITY 408 //this equivalent to 1G in the raw data coming from the accelerometer 
#define Accel_Scale(x) x*(GRAVITY/9.81)//Scaling the raw data of the accel to actual acceleration in meters for seconds square

#define ToRad(x) (x*0.01745329252)  // *pi/180
#define ToDeg(x) (x*57.2957795131)  // *180/pi

// IDG500 Sensitivity (from datasheet) => 2.0mV/º/s, 0.8mV/ADC step => 0.8/3.33 = 0.4
// Tested values : 
#define Gyro_Gain_X 0.4  //X axis Gyro gain
#define Gyro_Gain_Y 0.41 //Y axis Gyro gain
#define Gyro_Gain_Z 0.41 //Z axis Gyro gain
#define Gyro_Scaled_X(x) x*ToRad(Gyro_Gain_X) //Return the scaled ADC raw data of the gyro in radians for second
#define Gyro_Scaled_Y(x) x*ToRad(Gyro_Gain_Y) //Return the scaled ADC raw data of the gyro in radians for second
#define Gyro_Scaled_Z(x) x*ToRad(Gyro_Gain_Z) //Return the scaled ADC raw data of the gyro in radians for second

/*For debugging purposes*/
#define OUTPUTMODE 1  //If value = 1 will print the corrected data, 0 will print uncorrected data of the gyros (with drift), 2 Accel only data



#define RELAY_PIN        47

//Low Battery Alarm
#define BATTERY_VOLTAGE(x) (x * (INPUT_VOLTAGE / 1024.0)) * ((10000 + VOLT_DIV_OHMS) / VOLT_DIV_OHMS)

#define BATTERY_ADC      0	// ADC Channel of voltage divider
#define LOW_BATTERY_OUT  49     // Digital output pin for alarm    
#define INPUT_VOLTAGE    5.0	// (Volts) voltage your power regulator is feeding your ArduPilot to have an accurate pressure and battery level readings. (you need a multimeter to measure and set this of course)


//Airspeed
#define AIRSPEED_PIN     1      // Unused?




// Flying modes (lyagukh@gmail.com, 20101121)

#define F_MODE_ACROBATIC    0
#define F_MODE_POS_HOLD     1
#define F_MODE_SUPER_STABLE 2
#define F_MODE_ABS_HOLD     3

// Pins used for motormount LEDs (lyagukh@gmail.com, 20101121)

#define  MM_LED1 58  // AN4
#define  MM_LED2 59  // AN5


