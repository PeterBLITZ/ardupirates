/*
 www.ArduCopter.com - www.DIYDrones.com
 Copyright (c) 2010.  All rights reserved.
 An Open Source Arduino based multicopter.
 
 File     : config.h (former names: UserDefines.pde, ArduUser.h)
 Version  : v1.0, Aug 27, 2010
 Author(s): ArduCopter Team
             Ted Carancho (aeroquad), Jose Julio, Jordi Muñoz,
             Jani Hirvinen, Ken McEwans, Roberto Navoni,          
             Sandro Benigno, Chris Anderson
             
 Author(s): ArduPirates deveopment team                                  
             Philipp Maloney, Norbert, Hein, Igor, Emile, kidogo  
 
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

* ************************************************************** *
ChangeLog:
2011/22/01 kidogo;  moved all user configurable settings from main
                    .pde file and ArduUser.h to config.h


* ************************************************************** *
TODO:

* ************************************************************** */


/* ************************************************************ */
//  H O W   T O   U S E   T H I S   F I L E :
/* ************************************************************ */
//
// Please read through this entire file and read the comments.
// These comments will guide you through the entire setup process
// of your multicopter. 
//
// This is the only file you need to edit to complete your 
// personalised setup. No editing of any other file is needed.
//
/* ************************************************************ */



/* ************************************************************ */
// 1. CHOOSE AND SETUP YOUR AIRFRAME:
/* ************************************************************ */
//
// The first and most important step is to choose your airframe,
// in other words, what are you flying ?
// Valid choices are a quadcopter (QUAD) or hexacopter (HEXA). 
// The first airframe has four motors, the second has six.
//
/* ------------------------------------------------- */
//    QUAD COPTER AIRFRAME SETUP (PWM) (4 motors)    //
/* ------------------------------------------------- */
//
// If you have a quadcopter, uncomment this next line !
// If you have a hexacopter, comment this next line !
#define AIRFRAME QUAD
//
//
//  CONFIGURATION : FLIGHT_MODE_X_45Degree 
//  - APM-front is pointing towards the front motor.
//  - You must set the DIP1 switch to OFF (up) !
//  - After changing DIP1, reboot your APM.
//
//   F  CW  0....Front....0 CCW  R        // 0 = Motors
//          ...****........               // ****  = APM (APM-front pointing towards front motor)
//          ......****.....               //    **** 
//          .........****..               //       ****
//   L CCW  0....Back.....0  CW  B        //  L = Left motor, 
//                                        //  R = Right motor, 
//                                        //  B = Back motor,
//                                        //  F = Front motor.  
//                                        // CW = Clockwise rotation,
//                                        // CCW = Counter clockwise rotation.
//
//
//  CONFIGURATION : FLIGHT_MODE_+
//  - APM-front is pointing towards the front motor. 
//  - You must set the DIP1 switch to ON (down) !
//  - After changing DIP1, reboot your APM.
//
//           F CW 0 
//          ....FRONT.....                // 0 = Motors
//          .....***......                // *** = APM 
//   L CCW 0.....***.....0 CCW R          // *** 
//          .....***......                // ***  
//          .....BACK.....                 
//          B CW  0                       // L = Left motor, 
//                                        // R = Right motor, 
//                                        // B = Back motor,
//                                        // F = Front motor.  
//                                        // CW = Clockwise rotation,
//                                        // CCW = Counter clockwise rotation.
//
//  
//
//  CONFIGURATION : FLIGHT_MODE_X
//  - APM-front is between the Front and Right motor
//  - It is not important whether the DIP1 switch is on or off.
//  - Uncomment the line #define FLIGHT_MODE_X below !!
//
//   F  CW  0....Front....0 CCW  R        // 0 = Motors
//          ......***......               // *** = APM (APM-front between Front and Right motor)
//          ......***......               // ***
//          ......***......               // *** 
//   L CCW  0....Back.....0  CW  B        // L = Left motor, 
//                                        // R = Right motor, 
//                                        // B = Back motor,
//                                        // F = Front motor.  
//                                        // CW = Clockwise rotation,
//                                        // CCW = Counter clockwise rotation.
//
// To choose FLIGHT_MODE_X, just uncommend the line below.
#define FLIGHT_MODE_X      
//
//
/* ------------------------------------------------- */
//    HEXA COPTER AIRFRAME SETUP (PWM) (6 motors)    //
/* ------------------------------------------------- */
//
// If you have a hexacopter, uncomment this next line !
// If you have a quadcopter, comment it !
// #define AIRFRAME HEXA
//
//
//           F CW 0 
//          ....FRONT....                 // 0 = Motors
//    L CCW 0....***....0 CCW R           // *** = APM 
//          .....***.....                 // *** 
//    L CW  0....***....0 CW  R           // ***
//          .....BACK....                 
//          B CCW 0                       // L = Left motors, 
//                                        // R = Right motors, 
//                                        // B = Back motor,
//                                        // F = Front motor.  
//                                        // CW = Clockwise rotation,
//                                        // CCW = Counter clockwise rotation.
//
// To make absolutely sure you are running the hexa flight mode,
// connect with the Configurator and use the serial monitor in the
// Configurator to send the command "T". It will tell you which
// flight mode is configured.



/* ************************************************************ */
// 2. CHOOSE YOUR FLIGHT MODE:
/* ************************************************************ */
//
// The next step is to choose your FLIGHT MODE.
// This is done by setting switch 3 of the DIP switch on your APM
// to on or of. After switching to another flight mode you will
// need to reboot your APM by disconnecting and reconnecting power.
//
// Acrobatic mode; your multicopter will be handling as a traditional
// RC helicopter. You will have to constantly correct it's flight path
// to keep it up in the air - like balancing on a ball. On the other
// hand, this is the mode that will allow some aerobatics.
//
// Stable mode; your multicopter will be automatically piloted by the 
// APM and in an ideal configuration you'll only have to command it
// with your transmitter when you want it to change it's position in
// the air. Stability, horizontal attitude, altitude etc. will be
// controller by the autopilot functionality.
//
// DIP3 down (ON) : Acrobatic Mode.  Yellow LED is Flashing. 
// DIP3 up (OFF)  : Stable Mode.  Yellow LED stays lit. AUTOPILOT MODE status LEDs now apply.  See below.
//
/* ************************************************************ */
// AUTOPILOT MODE status LEDs and MODES: 
// (these only apply in Stable mode, see above)
/* ************************************************************ */
//
// AUX2 OFF && AUX1 OFF = Stable Mode              (AP_mode = 2)   Yellow LED: OFF    Red LED: OFF
// AUX2 OFF && AUX1 ON  = Altitude Hold only       (AP_mode = 3)   Yellow LED: ON     Red LED: OFF
// AUX2 ON  && AUX1 OFF = Position Hold only       (AP_mode = 4)   Yellow LED: OFF    Red LED: ON (if the GPS is not being logged the Red LED is flashing)
// AUX2 ON  && AUX1 ON  = Position & Altitude Hold (AP_mode = 5)   Yellow LED: ON     Red LED: ON (if the GPS is not being logged the Red LED is flashing)
//
// TIP: in the Configurator, the MODE channel is what we refer to as AUX2.
 
/* ************************************************************ */
// 3. SELECT AND SETUP YOUR SENSORS
/* ************************************************************ */
//
// The next step is to choose which sensors you have connected to
// your APM. Uncomment every sensor that you own and wish to use by
// removing the double slash (//) from the beginning of the line.
//
// If you do not wish to use a particular sensor (or if you do not
// have it connected, disable it by inserting a double slash (//) 
// at the beginning of the line
//
/* ------------------------------------------------------------ */
// Sensor(s) for: Altitude detection
/* ------------------------------------------------------------ */
// TIP: Choose either UseBMP (default) or IsSONAR but not both !
/* ------------------------------------------------------------ */
//
#define UseBMP             // Use pressure sensor for altitude hold (default) ?
//#define IsSONAR          // or are we using a Sonar for altitude hold?
//
/* ------------------------------------------------------------ */
// Sensor for: Obstacle avoidance
/* ------------------------------------------------------------ */
//
// Uncomment this next line if you wish to use a rangefinder,
// otherwise comment this line.
//#define IsRANGEFINDER    // are we using range finders for obstacle avoidance?
//
/* ------------------------------------------------------------ */
// Sensor for: Location detection (GPS)
/* ------------------------------------------------------------ */
//
// Uncomment this next line if you wish to use GPS, comment it if
// you don't want to use GPS.
#define IsGPS              
//
// if you use a GPS, please uncomment your GPS Protocol based on 
// your GPS device even if you do not have a GPS!!
//
#ifdef IsGPS  
//#define GPS_PROTOCOL GPS_PROTOCOL_NONE	// No GPS attached!!
//#define GPS_PROTOCOL GPS_PROTOCOL_NMEA	// Standard NMEA GPS.      NOT SUPPORTED (yet?)
//#define GPS_PROTOCOL GPS_PROTOCOL_IMU	        // X-Plane interface or ArduPilot IMU.
#define GPS_PROTOCOL GPS_PROTOCOL_MTK	        // MediaTek-based GPS running the DIYDrones firmware 1.4
//#define GPS_PROTOCOL GPS_PROTOCOL_MTK16	// MediaTek-based GPS running the DIYDrones firmware 1.6
//#define GPS_PROTOCOL GPS_PROTOCOL_UBLOX	// UBLOX GPS
//#define GPS_PROTOCOL GPS_PROTOCOL_SIRF	// SiRF-based GPS in Binary mode.  NOT TESTED
#endif
//
/* ------------------------------------------------------------ */
// Sensor for: Electronic compass
/* ------------------------------------------------------------ */
//
// Uncomment this next line if you wish to use the magnetometers,
// comment it if you don't want to use them.
#define IsMAG              // Do we have a magnetometer connected
//
// IMPORTANT: You must activate the magnetometer either using the
// CLI or the Configurator !
//
// If you are using the magnetometers, please set it up using the
// section below.
//
#ifdef IsMAG
// To get Magneto offsets, switch to CLI mode and run offset calibration. During calibration
// you need to roll/bank/tilt/yaw/shake etc your ArduCoptet. Don't kick like Jani always does :)
//#define MAGOFFSET -76,22.5,-55.5  // Hein's Quad calibration settings.  You have to determine your own.
//
//
// MAGCALIBRATION is the correction angle in degrees (can be + or -). You must calibrating your magnetometer to show magnetic north correctly.
// After calibration you will have to determine the declination value between Magnetic north and true north, see following link
// http://code.google.com/p/arducopter/wiki/Quad_Magnetos under additional settings. Both values have to be incorporated
// You can check Declination to your location from http://www.magnetic-declination.com/
// Example:  Magnetic north calibration show -1.2 degrees offset and declination (true north) is -5.6 then the MAGCALIBRATION should be -6.8.
// Your GPS readings is based on true north.
// For Magnetic north calibration make sure that your Magnetometer is truly showing 0 degress when your ArduQuad is looking to the North.
// Use a real compass (! not your iPhone) to point your ArduQuad to the magnetic north and then adjust this 
// value until you have a 0 dergrees reading in the configurator's artificial horizon. 
// Once you have achieved this fine tune in the configurator's serial monitor by pressing "T" (capital t).
//
#define MAGCALIBRATION 1.65      //  Quad Hein, South Africa, Centurion.  You have to determine your own.
//#define MAGCALIBRATION -15.65      //  Hexa Hein, South Africa, Centurion.  You have to determine your own.
//
//
// Next, you'll have to define how your magnetometer is mounted
// to your multicopter. Also take care of choosing the right
// kind of magnetometer that you have; DIYDrones or SparkFun.
// Uncomment the line that defines your setup. 
// Remember to uncomment only one single line !
//
// DIY Drones magnetometer;
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_UP_PINS_FORWARD
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_UP_PINS_FORWARD_RIGHT
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_UP_PINS_RIGHT
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_UP_PINS_BACK_RIGHT
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_UP_PINS_BACK
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_UP_PINS_BACK_LEFT
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_UP_PINS_LEFT
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_UP_PINS_FORWARD_LEFT
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_DOWN_PINS_FORWARD      // Hein Hexa
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_DOWN_PINS_FORWARD_RIGHT
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_DOWN_PINS_RIGHT
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_DOWN_PINS_BACK_RIGHT
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_DOWN_PINS_BACK
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_DOWN_PINS_BACK_LEFT
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_DOWN_PINS_LEFT
//#define MAGORIENTATION AP_COMPASS_COMPONENTS_DOWN_PINS_FORWARD_LEFT

// Sparkfun magnetometer;
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_FORWARD
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_FORWARD_RIGHT
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_RIGHT
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_BACK_RIGHT
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_BACK
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_BACK_LEFT
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_LEFT
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_FORWARD_LEFT
#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD       //Hein quad
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD_RIGHT
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_RIGHT
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK_RIGHT
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK_LEFT
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_LEFT
//#define MAGORIENTATION AP_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD_LEFT
#endif



/* ************************************************************ */
// 4. SELECT AND SETUP YOUR SPECIAL FUNCTIONS
/* ************************************************************ */
//
// This step is about enabling or disabling special functions
// that go beyond the core functionality of flying (keeping the
// multicopter in the air).
//
// If you do not wish to use a particular function (or if you do 
// not have the hardware to support it, disable it by inserting a 
// double slash (//) at the beginning of the line
//

/* ------------------------------------------------------------ */
// Function for: Motormount LED's or Attraction Mode
/* ------------------------------------------------------------ */

//#define IsAM             // Do we have motormount LED's. AM = Atraction Mode

/* ------------------------------------------------------------ */
// Functions for: Camera mounted to the multicopter
/* ------------------------------------------------------------ */
//
// You can build or buy a special mount for your photo camera which
// is attached to your multicopter. Using one or more servos you
// can have the multicopter automatically tilt and roll the camera
// so it stays horizontal, regardless of the multicopter's position
// in the air (also called attitude).
//
// In order for camera stabilization to work you will have to change
// the settings in the section below (called 'Camera related settings')
// to match your needs and hardware.
//
// If you want to enable camera stabilization, uncomment the IsCAM line
// and set the DIP2 switch on your APM to ON (down). If you wish to 
// disable camera stabilization, comment the IsCAM line by placing a 
// double slash (//) at the beginning of the line, but also set DIP2
// on your APM to OFF (up) !
//
// IMPORTANT: To finalize the configuration of camera stabilization you
// will need to use the Configurator and send the K command. Refer to 
// the Wiki for more info on the Configurator and this specific command.

//#define IsCAM              // Do we have camera stabilization in use, If you activate, check OUTPUT pins from ArduUser.h
                           // DIP2 down (ON) = Camera Stabilization enabled, DIP2 up (OFF) = Camera Stabilization disabled.
//#define UseCamShutter      // Do we want to use CH9 (Pin PL3) for camera trigger during GPS Hold or Altitude Hold.                  

/* ******************************************************** */
// Camera related settings - check these only if you are 
// planning on using camera stabilization.

#define CAM_CENT  1500          // Camera center
#define CAM_SMOOTHING 1000      // Camera movement smoothing on pitch axis
#define CAM_SMOOTHING_ROLL 1000 // Camera movement smoothing on roll axis

#define CAM_FOCUS 1710          // Camera trigger Servo Focus position
#define CAM_TRIGGER 1780        // Camera trigger Servo Trigger position 
#define CAM_RELEASE 1500        // Camera trigger Servo Release Trigger Button position

#define CAM_TILT_OUT   4        // OUTx pin for Tilt servo
#define CAM_ROLL_OUT   5        // OUTx pin for Roll servo
#define CAM_YAW_OUT    5        // OUTx pin for Yaw servo (often same as Roll)

#define CAM_TILT_CH  CH_7       // Channel for radio knob to control tilt 

/* ------------------------------------------------------------ */
// Function for: Battery voltage monitoring
/* ------------------------------------------------------------ */
//
// The APM is capable of monitoring the voltage level of your LiPo
// battery across each individual cell. If you have soldered the
// voltage dividers and wish to monitor the power level of your 
// battery, uncomment the next line by removing the double slash (//)
// at the beginning of the line. Refer to the ArduCopter Wiki for 
// more information on this subject:
// http://code.google.com/p/ardupirates/wiki/BatteryAlarmHowto

//#define BATTERY_EVENT 1  // (boolean) 0 = don't read battery, 1 = read battery voltage (only if you have it _wired_ up!)


// [kidogo] Please help with this one (define CONFIGURATOR), I'm unsure as to what it does.
#define CONFIGURATOR



/* ************************************************************ */
// 5. SETUP SAFETY FEATURES
/* ************************************************************ */
//
// This section helps you setup the safety features of your
// multicopter. Defaults are generally okay. We advise you to
// read through them, but not to change anything unless you are
// very sure about what you are doing.
//
#define ARM_DELAY 50                      // how long you need to keep rudder to max right for arming motors (units*0.02, 50=1second)
#define DISARM_DELAY 25                   // how long you need to keep rudder to max left for disarming motors
#define SAFETY_DELAY 25                   // how long you need to keep throttle to min before safety activates and does not allow sudden throttle increases
#define SAFETY_MAX_THROTTLE_INCREASE 100  // how much of jump in throttle (within a single cycle, 5ms) will cause motors to disarm
#define SAFETY_HOVER_THROTTLE 1300        // When we reach Hover Throttle Safely we switch of Safety Feature  



/* ************************************************************ */
// 6. MISCELLANIOUS SETTINGS
/* ************************************************************ */
//
// This section is for advanced or experienced users only. If you
// are just starting out with your multicopter you probably do not
// need to change anything here. You can leave all settings below
// at their default values.
//
/* ------------------------------------------------------------ */
// Serial communication and wireless link
/* ------------------------------------------------------------ */
//
// Serial data, do we have FTDI cable or Xbee on Telemetry port as our primary command link
// If we are using normal FTDI/USB port as our telemetry/configuration, keep next line disabled
//#define SerXbee
//
// Telemetry port speed, default is 115200
//#define SerBau  19200
//#define SerBau  38400
//#define SerBau  57600
#define SerBau  115200
//
/* ------------------------------------------------------------ */
// Finetuning your transmitter (radio) settings
/* ------------------------------------------------------------ */
//
#define ROLL_MID 1500           // Radio Roll channel mid value
#define PITCH_MID 1500          // Radio Pitch channel mid value
#define YAW_MID 1500            // Radio Yaw channel mid value
#define THROTTLE_MID 1505       // Radio Throttle channel mid value
#define AUX_MID 1500
//
/* ------------------------------------------------------------ */
// PID tuning with your transmitter (radio)
/* ------------------------------------------------------------ */
//
// To enable PID tuning, uncomment the below line.
// PID tuning uses your 3 position switch on your radio, the same
// you use to select the flightmode. You will need a radio with
// at least 7 channels.
// Your Aux1 will be your 3 position channel, and your radio has
// to be in acro (plane) mode.
//#define Use_PID_Tuning



/* ************************************************************ */
// E N D   O F   F I L E
/* ************************************************************ */





