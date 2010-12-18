/* test*/
/* ********************************************************************** */
/*             ArduCopter & ArduPirates Quad / Hexa                       */
/*                                                                        */
/* Quadcopter code from AeroQuad project and ArduIMU quadcopter project   */
/* IMU DCM code from Diydrones.com                                        */
/* (Original ArduIMU code from Jordi Muñoz and William Premerlani)        */
/* Ardupilot core code : from DIYDrones.com development team              */
/* Authors : Arducopter development team                                  */
/*           Ted Carancho (aeroquad), Jose Julio, Jordi Muñoz,            */
/*           Jani Hirvinen, Ken McEwans, Roberto Navoni,                  */
/*           Sandro Benigno, Chris Anderson.                              */
/* Authors : ArduPirates deveopment team                                  */
/*           Phil, Norbert, Hein, Igor, Dani Saez.                        */
/* Date    : December 2010                                                */
/* Version : 1.7                                                          */
/* Hardware : ArduPilot Mega + Sensor Shield (Production versions)        */
/* Mounting position : RC connectors pointing backwards                   */
/* This code use this libraries :                                         */
/*   APM_RC : Radio library (with InstantPWM)                             */
/*   APM_ADC : External ADC library                                       */
/*   DataFlash : DataFlash log library                                    */
/*   APM_BMP085 : BMP085 barometer library                                */
/*   APM_Compass : HMC5843 compass library [optional]                     */
/*   GPS_UBLOX or GPS_NMEA or GPS_MTK : GPS library    [optional]         */
/* ********************************************************************** */
/*
**** Switch Functions *****
 AUX2 OFF && GEAR OFF = StableMode (AP_mode = 0)
 AUX2 OFF && GEAR ON  = SuperStable Mode (Altitude Hold and Heading Hold if no throttle stick movement) (AP_mode = 1)
 AUX2 ON  && GEAR ON  = Position & Altitude Hold Mode (AP_mode = 2)
 
 **** LED Feedback ****
 Bootup Sequence:
 1) A, B, C LED's blinking rapidly while waiting ESCs to bootup and initial shake to end from connecting battery
 2) A, B, C LED's have running light while calibrating Gyro/Acc's
 3) Green LED Solid after initialization finished
 
 Green LED On = APM Initialization Finished
 Yellow LED On = GPS Hold Mode
 Yellow LED Off = Flight Assist Mode (No GPS)
 Red LED On = GPS Fix, 2D or 3D
 Red LED Off = No GPS Fix
 
 Green LED blink slow = Motors armed, Stable mode
 Green LED blink rapid = Motors armed, Acro mode 


/* *****************************************************************************
                  ArduPirate Configuration Setup
   ***************************************************************************** */

//GPS Config *******************************************************************

#define IsGPS               // Do we have a GPS connected?

#define IsNEWMTEK           // Do we have MTEK with new firmware?
#include <GPS_MTK.h>        // MediaTEK DIY Drones GPS

//#define UBLOX_GPS         // uBlox GPS  **
//#include <GPS_UBLOX.h>    //            **

//Flight Sensors Config ********************************************************

#define UseBMP              // Do we want to use the barometer sensor on the IMU?
#define IsMAG               // Do we have a Magnetometer connected? If have, remember to activate it from Configurator!!
//#define IsSonar             // Do we have Sonar installed - typically XL-Maxsonar EZ4 from Maxbotix (or similar) on analgue output

//Other Config ******************************************************************

#define CONFIGURATOR        // Do we use Configurator or normal text output over serial link?
//#define IsCAMERATRIGGER   // Do we want to use a servo to trigger a camera regularely?
//#define IsXBEE            // Do we have a telemetry connected, eg. XBee connected on Telemetry port?
//#define IsAM              // Do we have motormount LED's? (AM = Atraction Mode)
//#define BATTERY_EVENT     // Do we have battery alarm wired up?
//#define MOTORMOUNT_LEDS   // Do we have motormount LEDs attached to AN4 and AN5 (NOT the same as IsAM)? See bottom of the file for details
//#define RELAY_LED_LIGHTS  // Do we have LED lights attached through the relay? Turned on and off with Rx Ch7 (FIXME: should be configurable)


/**********************************************/
//    QUAD COPTER SETUP                       //

#define Quad

// Frame build configuration
// THIS FLIGHT MODE X CODE - APM FRONT BETWEEN FRONT AND RIGHT MOTOR.
// NOT LIKE THE ALPHA RELEASE !!!.

//   F  CW  0....Front....0 CCW  R        // 0 = Motor
//          ......***......               // *** = APM 
//          ......***......               // ***
//          ......***......               // *** 
//   L CCW  0....Back.....0  CW  B          L = Left motor, 
//                                          R = Right motor, 
//                                          B = Back motor,
//                                          F = Front motor.  

#define FLIGHT_MODE_X
//#define FLIGHT_MODE_+

/**********************************************/
//    HEXA COPTER SETUP                       //

//#define Hexa

// Frame build condiguration
// Hexa Diamond Mode - 6 Motor system in diamond shape

//      L  CCW 0.Front.0 CW  R           // 0 = Motor
//         ......***......               // *** = APM 
//   L  CW 0.....***......0 CCW  R       // ***
//         ......***......               // *** 
//     B  CCW  0.Back..0  CW  B          L = Left motors, R = Right motors, B = Back motors.

/**********************************************/

#ifdef IsMAG
//  Magnetometer Setup

// orientations for DIYDrones magnetometer
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_FORWARD
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_FORWARD_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_BACK_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_BACK
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_BACK_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_FORWARD_LEFT
#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_FORWARD
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_FORWARD_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_BACK_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_BACK
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_BACK_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_FORWARD_LEFT

// orientations for Sparkfun magnetometer
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_FORWARD
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_FORWARD_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_BACK_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_BACK
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_BACK_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_FORWARD_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD_LEFT

// To get Magneto offsets, switch to CLI mode and run offset calibration. During calibration
// you need to roll/bank/tilt/yaw/shake etc your ArduCoptet. Don't kick like Jani always does :)

#define MAGOFFSET -81.00,-35.00,30.50  // You will have to determine your own settings.

// MAGCALIBRATION is the correction angle in degrees (can be + or -). You must calibrating your magnetometer to show magnetic north correctly.
// After calibration you will have to determine the declination value between Magnetic north and true north, see following link
// http://code.google.com/p/arducopter/wiki/Quad_Magnetos under additional settings. Both values have to be incorporated
// Example:  Magnetic north calibration show -1.2 degrees offset and declination (true north) is -5.6 then the MAGCALIBRATION shoud be -6.8.
// Your GPS readings is based on true north.
// For Magnetic north calibration make sure that your Magnetometer is truly showing 0 degress when your ArduQuad is looking to the North.
// Use a real compass (! not your iPhone) to point your ArduQuad to the magnetic north and then adjust this 
// value until you have a 0 dergrees reading in the configurator's artificial horizon.

#define MAGCALIBRATION -6 // You have to determine your own setting.
#endif

//Low Battery Alarm
#define LOW_VOLTAGE      12.5   // Pack voltage at which to trigger alarm (Set to about 1 volt above ESC low voltage cutoff)
#define VOLT_DIV_OHMS    3690   // Value of resistor (in ohms) used on voltage divider

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

/* ****************************************************************************** */
/* ****************************** Includes ************************************** */
/* ****************************************************************************** */

#include <Wire.h>
#include <APM_ADC.h>
#include <APM_RC.h>
#include <DataFlash.h>
#include <APM_Compass.h>
#include <EEPROM.h>       // EEPROM storage for user configurable values
#include "ArduCopter.h"
#include "UserConfig.h"

#ifdef UseBMP
  #include <APM_BMP085.h>
#endif

/* Software version */
#define VER 1.7    // Current software version (only numeric values)

/* ***************************************************************************** */

// Normal users does not need to edit anything below these lines. If you have
// need, go and change them in UserConfig.h

/* ***************************************************************************** */

/* ************************************************************ *
   byte Read_AP_mode()                                          
     Desc: reads and returns the selected Flying mode           

     Moving the reading of the 5,6 channels to a funtion that 
     returns the mode makes it easier for those who want to select 
     their flying modes with other switches as they only have to modify 
     this function and not mess with the main code.

   right now we read the Quad Mode from Channel 5 & 6
   AUX2 OFF && GEAR OFF = StableMode (AP_mode = 0)
   AUX2 OFF && GEAR ON  = SuperStable Mode (Altitude Hold and Heading Hold if no throttle stick movement) (AP_mode = 1)
   AUX2 ON  && GEAR ON  = Position & Altitude Hold Mode (AP_mode = 2)
 * ************************************************************ */

byte Read_AP_mode()
{
  byte APmode;  //local var so we don't mess gob vars.
      
  if (ch_aux2 < 1250 && ch_aux > 1800)
  {
    APmode = 2;          // Stable mode & Altitude hold mode (Stabilization assist mode)
    digitalWrite(LED_Yellow,LOW); // Yellow LED off
  }
  else if (ch_aux < 1250 && ch_aux2 > 1800)
  {
    APmode = 3;           // Position & Altitude hold mode (GPS position control & Altitude control)
    digitalWrite(LED_Yellow,HIGH); // Yellow LED On
  }
  else if (ch_aux < 1250 && ch_aux2 < 1250)
  {
    APmode = 1;           // Position hold mode (GPS position control)
    digitalWrite(LED_Yellow,HIGH); // Yellow LED On
  }
  else 
  {
    APmode = 0;          // Acrobatic mode
    digitalWrite(LED_Yellow,LOW); // Yellow LED off
  }     // End reading Quad Mode from Channel 5 & 6
  return (APmode);
}


/* ************************************************************ *
   void Init_GPS()                                        
     Desc: Initializes GPS
 * ************************************************************ */
void  Init_GPS()
{
  #ifdef IsGPS  
    GPS.Init();                // GPS Initialization
  
    #ifdef IsNEWMTEK  
      delay(250);
      // DIY Drones MTEK GPS needs binary sentences activated if you upgraded to latest firmware.
      // If your GPS shows solid blue but LED C (Red) does not go on, your GPS is on NMEA mode
      Serial1.print("$PMTK220,200*2C\r\n");          // 5Hz update rate
      delay(200);
      Serial1.print("$PGCMD,16,0,0,0,0,0*6A\r\n"); 
    #endif
  #endif
}

/* ************************************************************ *
   void Clean_GPS_vars()                                        
     Desc: Sets to 0 GPS related global vars                    
 * ************************************************************ */
void Clean_GPS_vars()
{
  #ifdef IsGPS
    gps_roll_I        = 0;
    gps_pitch_I       = 0;
    gps_err_roll      = 0;
    gps_err_pitch     = 0;
    gps_roll_D        = 0;
    gps_pitch_D       = 0;
    gps_err_roll_old  = 0;
    gps_err_pitch_old = 0;
    command_gps_roll  = 0;
    command_gps_pitch = 0;
  #endif
}

/* ************************************************************ *
   void Get_Target_Position_GPS()                                        
     Desc: set GPS target_lattitude, target_longitude, target_position
 * ************************************************************ */
void Get_Target_Position_GPS()
{
  #ifdef IsGPS
    target_lattitude = GPS.Lattitude;
    target_longitude = GPS.Longitude;
    target_position = 1;
  #endif
}


/* ************************************************************ *
   void Read_GPS()                                        
     Desc: Reads GPS 
 * ************************************************************ */
void  Read_GPS()
{
  #ifdef IsGPS
    GPS_counter++;

    //Read GPS
    if (GPS_counter > 3)  // Reading GPS data at 60 Hz
    {
      GPS_counter = 0;
      GPS.Read();
    } 
    if (GPS.NewData)  // New GPS data?
    {
      GPS_timer_old = GPS_timer;   // Update GPS timer
      GPS_timer     = timer;
      GPS_Dt = (GPS_timer - GPS_timer_old) * 0.001;   // GPS_Dt
      GPS.NewData = 0;    // We Reset the flag...

      if (GPS.Fix)
        digitalWrite(LED_Red,HIGH);  // GPS Fix => Blue LED
      else
        digitalWrite(LED_Red,LOW);

      if (AP_mode == 1 || AP_mode ==3)
      {
        if ((target_position == 1) && (GPS.Fix))
        {
          Position_control(target_lattitude,target_longitude);  // Call position hold routine
        }
        else
        {
          Clean_GPS_vars();
        }
      }
    }
  #endif
}

/* ************************************************************ *
   void Init_BMP()                                        
     Desc: Initializes APM ADC
 * ************************************************************ */
void  Init_BMP()
{
  #ifdef UseBMP
    APM_BMP085.Init();   // APM ADC initialization
  #endif
}

/* ************************************************************ * 
   void Clean_BMP_vars()                                        
     Desc: Sets to 0 BMP related global vars                    
 * ************************************************************ */
void Clean_BMP_vars()
{
  BMP_altitude_I       = 0;
  BMP_altitude_D       = 0;
  BMP_err_altitude_old = 0;
  BMP_err_altitude     = 0;
  BMP_command_altitude = 0;
}


/* ************************************************************ * 
   void Read_BMP()
     Desc: Reads barometer to set BMP_Altitude 
 * ************************************************************ */
void Read_BMP()
{
  #ifdef UseBMP

    float tempPresAlt;

    BMP_counter++;
    if (BMP_counter > 10)  // Reading Barometric data at 20Hz 
    {
      BMP_counter = 0;
      APM_BMP085.Read();
      tempPresAlt = float(APM_BMP085.Press)/101325.0;
      tempPresAlt = pow(tempPresAlt, 0.190295);
      BMP_Altitude = (1.0 - tempPresAlt) * 4433000;      // Altitude in cm
    }

  #endif
}


/* ************************************************************ * 
   void Check_BMP(AP_mode)
     Desc: Reads BMP_Altitude and check throttle for 
           deactivation of Altitude Hold
     Params: Receives Flying Mode (AP_Mode)
 * ************************************************************ */
void Check_BMP(byte APmode)
{
  #ifdef UseBMP  
    // New Altitude Hold using BMP Pressure sensor.  If Trottle stick moves more then 10%, switch Altitude Hold off    
    if (APmode == 2 || APmode == 3) 
    {
      if(command_throttle >= 15 || command_throttle <= -15 || ch_throttle <= 1200)
      {
        BMP_mode = 0; //Altitude hold is switched off because of stick movement 

        Clean_BMP_vars();
        
        target_alt_position = 0;  //target altitude reset
      } 
      else 
      {
        BMP_mode = 1;  //Altitude hold is swithed on.
      }
    } 
  #endif
}

/* ************************************************************ * 
   void Init_MAG();
     Desc: Initializes Magnetometer
 * ************************************************************ */
void Init_MAG()
{
  #ifdef IsMAG
    if (MAGNETOMETER == 1) 
    {
      APM_Compass.Init();  // I2C initialization
      APM_Compass.SetOrientation(MAGORIENTATION);
      APM_Compass.SetOffsets(MAGOFFSET);
      APM_Compass.SetDeclination(ToRad(MAGCALIBRATION));
    }
  #endif
}

/* ************************************************************ * 
   void Read_MAG()
     Desc: Reads Magnetomer to calculate roll and pitch 
 * ************************************************************ */
void Read_MAG()
{
  #ifdef IsMAG
    if (MAGNETOMETER == 1) 
    {
      Magneto_counter++;
      if (Magneto_counter > 20)  // Read compass data at 10Hz... (20 loop runs)
      {
        Magneto_counter = 0;
        APM_Compass.Read();     // Read magnetometer
        APM_Compass.Calculate(roll,pitch);  // Calculate heading
      }
    }
  #endif   
}


/* ************************************************************ * 
   void Read_Channels_Commands()
     Desc: Reads Commands from radio Rx... 
 * ************************************************************ */
void Read_Channels_Commands()
{
  float aux_float;

  // Commands from radio Rx... 
  // Stick position defines the desired angle in roll, pitch and yaw
  ch_roll     = channel_filter(APM_RC.InputCh(0) * ch_roll_slope  + ch_roll_offset , ch_roll);
  ch_pitch    = channel_filter(APM_RC.InputCh(1) * ch_pitch_slope + ch_pitch_offset, ch_pitch);
  ch_yaw      = channel_filter(APM_RC.InputCh(3) * ch_yaw_slope   + ch_yaw_offset  , ch_yaw);
  ch_throttle = channel_filter(APM_RC.InputCh(2), ch_throttle); // Transmiter calibration not used on throttle
  ch_aux  = APM_RC.InputCh(4);
  ch_aux2 = APM_RC.InputCh(5);
  ch_mode = APM_RC.InputCh(6);

  // get commands based on stick position
  command_throttle = (ch_throttle - throttle_mid) / 12.0; 
  command_rx_roll  = (ch_roll     - roll_mid    ) / 12.0;
  command_rx_pitch = (ch_pitch    - pitch_mid   ) / 12.0;

  if (abs(ch_yaw-yaw_mid) < 12)   // Take into account a bit of "dead zone" on yaw
    aux_float = 0.0;
  else
    aux_float = (ch_yaw-yaw_mid) / 180.0;
  
  command_rx_yaw += aux_float;

  if (command_rx_yaw > 180)         // Normalize yaw to -180,180 degrees
    command_rx_yaw -= 360.0;
  else if (command_rx_yaw < -180)
    command_rx_yaw += 360.0;

}



/* ************************************************************ * 
   byte  Get_Arm_Disarm_Motors(byte AuxMotorArmed)
     Desc: Check Throttle down and full yaw right for more than 2 seconds
    if not modified, returns  received value
 * ************************************************************ */
byte Get_Arm_Disarm_Motors(byte AuxMotorArmed)  
{
  // Arm motor output : Throttle down and full yaw right for more than 2 seconds
  if (ch_throttle < (MIN_THROTTLE + 100)) 
  {
    control_yaw = 0;
    command_rx_yaw = ToDeg(yaw);
    if (ch_yaw > 1850) 
    {
      if (Arming_counter > ARM_DELAY)
      {
        if(ch_throttle > 800) 
        {
          AuxMotorArmed = 1;
          minThrottle = MIN_THROTTLE + 60;  // A minimun value for mantain a bit if throttle
        }
      }
      else
        Arming_counter++;
    }
    else
      Arming_counter=0;
  
    // To Disarm motor output : Throttle down and full yaw left for more than 2 seconds
    if (ch_yaw < 1150) 
    {
      if (Disarming_counter > DISARM_DELAY)
      {
        AuxMotorArmed = 0;
        minThrottle = MIN_THROTTLE;
      }
      else
        Disarming_counter++;
    }
    else
      Disarming_counter=0;
  }
  else
  {
    Arming_counter=0;
    Disarming_counter=0;
  }
  return (AuxMotorArmed);
}

/* ************************************************************ * 
   void Show_Leds();
     Desc: Shows leds status
 * ************************************************************ */
void Show_Leds()
{
  // AM and Mode status LED lights
  if(millis() - gled_timer > gled_speed) 
  {
    gled_timer = millis();
    if(gled_status == HIGH) 
    { 
      digitalWrite(LED_Green, LOW);
      #ifdef IsAM      
        digitalWrite(RE_LED, LOW);
      #endif
      gled_status = LOW;
    } 
    else 
    {
      digitalWrite(LED_Green, HIGH);
      #ifdef IsAM
        if(motorArmed) 
          digitalWrite(RE_LED, HIGH);
      #endif
      gled_status = HIGH;
    } 
  }
}


/* ************************************************************ * 
   void Get_Motor_Values(byte Motor_Armed)
     Desc: Get the values to be outputed for the motors
 * ************************************************************ */
void Get_Motor_Values(byte Motor_Armed)
{
  // Quadcopter mix
  if (Motor_Armed == 1) 
  {   
    #ifdef IsAM
      digitalWrite(FR_LED, HIGH);    // AM-Mode
    #endif

    #ifdef FLIGHT_MODE_+
      if (BMP_mode == 1)
      {
        rightMotor = constrain(ch_throttle + BMP_command_altitude - control_roll  + control_yaw, minThrottle, 2000);
        leftMotor  = constrain(ch_throttle + BMP_command_altitude + control_roll  + control_yaw, minThrottle, 2000);
        frontMotor = constrain(ch_throttle + BMP_command_altitude + control_pitch - control_yaw, minThrottle, 2000);
        backMotor  = constrain(ch_throttle + BMP_command_altitude - control_pitch - control_yaw, minThrottle, 2000);
      } 
      else 
      {
        rightMotor = constrain(ch_throttle - control_roll  + control_yaw, minThrottle, 2000);
        leftMotor  = constrain(ch_throttle + control_roll  + control_yaw, minThrottle, 2000);
        frontMotor = constrain(ch_throttle + control_pitch - control_yaw, minThrottle, 2000);
        backMotor  = constrain(ch_throttle - control_pitch - control_yaw, minThrottle, 2000);
      }
    #endif

    #ifdef FLIGHT_MODE_X
      if (BMP_mode == 1)
      {
        rightMotor = constrain(ch_throttle + BMP_command_altitude - control_roll + control_pitch + control_yaw, minThrottle, 2000); // front right motor
        leftMotor  = constrain(ch_throttle + BMP_command_altitude + control_roll - control_pitch + control_yaw, minThrottle, 2000);  // rear left motor
        frontMotor = constrain(ch_throttle + BMP_command_altitude + control_roll + control_pitch - control_yaw, minThrottle, 2000); // front left motor
        backMotor  = constrain(ch_throttle + BMP_command_altitude - control_roll - control_pitch - control_yaw, minThrottle, 2000);  // rear right motor
      } 
      else 
      {
        rightMotor = constrain(ch_throttle - control_roll + control_pitch + control_yaw, minThrottle, 2000); // front right motor
        leftMotor  = constrain(ch_throttle + control_roll - control_pitch + control_yaw, minThrottle, 2000);  // rear left motor
        frontMotor = constrain(ch_throttle + control_roll + control_pitch - control_yaw, minThrottle, 2000); // front left motor
        backMotor  = constrain(ch_throttle - control_roll - control_pitch - control_yaw, minThrottle, 2000);  // rear right motor
      }  
    #endif








  }
  else
  {
    #ifdef IsAM
      digitalWrite(FR_LED, LOW);    // AM-Mode
    #endif
  
    digitalWrite(LED_Green,HIGH); // Ready LED on

    rightMotor = MIN_THROTTLE;
    leftMotor  = MIN_THROTTLE;
    frontMotor = MIN_THROTTLE;
    backMotor  = MIN_THROTTLE;








    roll_I  = 0;     // reset I terms of PID controls
    pitch_I = 0;
    yaw_I   = 0; 
    // Initialize yaw command to actual yaw when throttle is down...
    command_rx_yaw = ToDeg(yaw);
    BMP_mode = 1;   // in general we reinitialize altitude hold as "on"
  }
}


/* ************************************************************ * 
   void Output_Motors()
     Desc: Outputs the values for the motors
 * ************************************************************ */
void Output_Motors()
{
  APM_RC.OutputCh(0, rightMotor);   // Right motor
  APM_RC.OutputCh(1, leftMotor);    // Left motor
  APM_RC.OutputCh(2, frontMotor);   // Front motor
  APM_RC.OutputCh(3, backMotor);    // Back motor   

   // InstantPWM
  APM_RC.Force_Out0_Out1();
  APM_RC.Force_Out2_Out3();
}


/* ************************************************************ * 
   void Stabilize_Camera()
     Desc: Outputs the values for Pitch and Roll to stabilize camera 
 * ************************************************************ */
void Stabilize_Camera()
{
    // Camera Stabilization
    APM_RC.OutputCh(4, APM_RC.InputCh(6)+(pitch)*600); // Tilt correction 
    APM_RC.OutputCh(5, 1650+(roll)*800);               // Roll correction
}



/* ************************************************************ */
/* **************** MAIN PROGRAM - SETUP ********************** */
/* ************************************************************ */
void setup()
{
  int i, j, y;
  float aux_float[3];

  pinMode(LED_Yellow, OUTPUT); //Yellow LED A  (PC1)
  pinMode(LED_Red   , OUTPUT); //Red LED B     (PC2)
  pinMode(LED_Green , OUTPUT); //Green LED C   (PC0)

  pinMode(SW1_pin,INPUT);     //Switch SW1 (pin PG0)

  pinMode(RELE_pin,OUTPUT);   // Rele output
  digitalWrite(RELE_pin,LOW);
  
  APM_RC.Init();             // APM Radio initialization

  // RC channels Initialization (Quad motors)  
  APM_RC.OutputCh(0,MIN_THROTTLE);  // Motors stoped
  APM_RC.OutputCh(1,MIN_THROTTLE);
  APM_RC.OutputCh(2,MIN_THROTTLE);
  APM_RC.OutputCh(3,MIN_THROTTLE);

  //  delay(1000); // Wait until frame is not moving after initial power cord has connected
  for(i = 0; i <= 50; i++) 
  {
    digitalWrite(LED_Green , HIGH);
    digitalWrite(LED_Yellow, HIGH);
    digitalWrite(LED_Red   , HIGH);
    delay(20);
    digitalWrite(LED_Green , LOW);
    digitalWrite(LED_Yellow, LOW);
    digitalWrite(LED_Red   , LOW);
    delay(20);
  }

  APM_ADC.Init();      // APM ADC library initialization
  DataFlash.Init();    // DataFlash log initialization

  Init_GPS();          // GPS Initialization

  Init_BMP();          // APM ADC initialization

  readUserConfig(); // Load user configurable items from EEPROM

  // Safety measure for Channel mids
  
  if(roll_mid  < 1400 || roll_mid  > 1600) roll_mid  = 1500;
  if(pitch_mid < 1400 || pitch_mid > 1600) pitch_mid = 1500;
  if(yaw_mid   < 1400 || yaw_mid   > 1600) yaw_mid   = 1500;
  
  // Safety measure for Channel mids finished
  

  Init_MAG();  // Magnetometer Initicalization

  DataFlash.StartWrite(1);   // Start a write session on page 1

  SerBeg(SerBau);                      // Initialize SerialXX.port, IsXBEE define declares which port
  
  #ifndef CONFIGURATOR  
    SerPri("ArduCopter Quadcopter v");
    SerPriln(VER)
    SerPri("Serial ready on port: ");    // Printout greeting to selecter serial port
    SerPriln(SerPor);                    // Printout serial port name
  #endif
  
  // Check if we enable the DataFlash log Read Mode (switch)
  // If we press switch 1 at startup we read the Dataflash eeprom
  while (digitalRead(SW1_pin)==0)
  {
    SerPriln("Entering Log Read Mode...");
    Log_Read(1,2000);
    delay(30000);
  }






  Read_adc_raw();
  delay(10);

  // Offset values for accels and gyros...
  AN_OFFSET[3] = acc_offset_x;
  AN_OFFSET[4] = acc_offset_y;
  AN_OFFSET[5] = acc_offset_z;
  aux_float[0] = gyro_offset_roll;
  aux_float[1] = gyro_offset_pitch;
  aux_float[2] = gyro_offset_yaw;

  // Take the gyro offset values
  for(i=0,j=0; i<300; i++)
  {
    Read_adc_raw();
    for(y=0; y<=2; y++)   // Read initial ADC values for gyro offset.
      aux_float[y]=aux_float[y]*0.8 + AN[y]*0.2;

    delay(10);
    
    // Runnings lights effect to let user know that we are taking mesurements
    if(j == 0) 
    {
      digitalWrite(LED_Green , HIGH);
      digitalWrite(LED_Yellow, LOW);
      digitalWrite(LED_Red   , LOW);
    } 
    else if (j == 1) 
    {
      digitalWrite(LED_Green , LOW);
      digitalWrite(LED_Yellow, HIGH);
      digitalWrite(LED_Red   , LOW);
    } 
    else 
    {
      digitalWrite(LED_Green , LOW);
      digitalWrite(LED_Yellow, LOW);
      digitalWrite(LED_Red   , HIGH);
    }
    if((i % 5) == 0) j++;
    if(j >= 3) j = 0;
  }
  digitalWrite(LED_Green , LOW);
  digitalWrite(LED_Yellow, LOW);
  digitalWrite(LED_Red   , LOW);

  for(y=0; y<=2; y++)   
    AN_OFFSET[y]=aux_float[y];

  #ifndef CONFIGURATOR
    for(i=0;i<6;i++)
    {
      SerPri("AN[]:");
      SerPriln(AN_OFFSET[i]);
    }
    SerPri("Yaw neutral value:");
    SerPri(yaw_mid);
  #endif
  
  delay(1000);

  DataFlash.StartWrite(1);   // Start a write session on page 1
  timer = millis();
  tlmTimer = millis();
  Read_adc_raw();        // Initialize ADC readings...
  delay(20);

  #ifdef IsAM
    // Switch Left & Right lights on
    digitalWrite(RI_LED, HIGH);
    digitalWrite(LE_LED, HIGH);
  #endif

  motorArmed = 0;
  digitalWrite(LED_Green,HIGH);     // Initial setup finished - Ready to go...
}

/* ************************************************************ */
/* ************** MAIN PROGRAM - MAIN LOOP ******************** */
/* ************************************************************ */
void loop()
{

  if((millis()-timer)>=5)   // Main loop 200Hz
  {
    timer_old = timer;
    timer     = millis();
    G_Dt      = (timer-timer_old)*0.001;      // Real time of loop run 

    Read_adc_raw(); // IMU DCM Algorithm
    Read_MAG();     // Reads Magnetomer to calculate roll and pitch 
    Read_BMP();     // Reads barometer to set BMP_Altitude 

    Matrix_update(); 
    Normalize();
    Drift_correction();
    Euler_angles();

    Log_Read_Attitude();  //This one could be omited??
    
    if (APM_RC.GetState() == 1)  // Do we have a new radio frame?
    {
      AP_mode = Read_AP_mode();  // Reads the flying mode
      Read_Channels_Commands();  // Reads sticks position from radio Rx and sets commands 
      Check_BMP(AP_mode);        // Reads BMP_Altitude and check throttle for deactivation of Altitude Hold
    }  // End reading new radio frame

    switch (AP_mode)
    {
      case 0: //Acrobatic Mode
          heading_hold_mode   = 0;
          target_alt_position = 0;
          target_position     = 0;
          BMP_mode            = 0; // Altitude hold mode disabled followed by reset of variables
          Clean_BMP_vars();
          Clean_GPS_vars();

//DSP          Read_GPS();        //DSP in v1.5, we where reading GPS even in Acro mode.....REMOVE?

          gled_speed = 400;  //Sets green led Speed to fast
          Rate_control_v2();
          command_rx_yaw = ToDeg(yaw); // Reset yaw, so if we change to stable mode we continue with the actual yaw direction
        break;


      case 1: // Position Control (just GPS without altitude)
          heading_hold_mode   = 1;
          target_alt_position = 0;

          if (target_position == 0)   // If this is the first time we switch to Position control, actual position is our target position
          {
            Get_Target_Position_GPS(); // set target_lattitude, target_longitude, target_position
            Clean_GPS_vars();
          }        
          BMP_mode = 0;   // Altitude hold mode disabled followed by reset of variables
          Clean_BMP_vars();
          Read_GPS();
          gled_speed = 1200;  //Sets green led Speed to slow
          Attitude_control_v3();
//DSP we have just set BMP_mode = 0, so the following code is useless
//DSP          if (BMP_mode == 1)   // mst - was on AP_mode: probabely altitude bug we have searched for
//DSP            BMP_Altitude_control(BMP_target_altitude);

        break;


      case 2: // SuperStable Mode (Altitude Hold and Heading Hold) (if no stick movement)
          heading_hold_mode = 1;
          if (target_alt_position == 0)   // If this is the first time we switch to Position control, actual position is our target position
          {
            BMP_target_altitude = BMP_Altitude;  //first time setting current altitude is target altitude

//DSP Clean_BMP_vars is called after the if always, so this part is useless
//DSP            if (BMP_mode == 0)    // If Altitude hold mode has been disabled reset variables
//DSP            {
//DSP              Clean_BMP_vars();
//DSP            } 
            target_alt_position = 1;  //target altitude has been set
          }
          target_position   = 0;
          Clean_BMP_vars();
          Read_GPS();
          gled_speed = 1200;  //Sets green led Speed to slow
          Attitude_control_v3();

          if (BMP_mode == 1)   // mst - was on AP_mode: probabely altitude bug we have searched for
            BMP_Altitude_control(BMP_target_altitude);

        break;


      case 3:  // Position & Altitude Hold Mode
          heading_hold_mode = 1;
          if (target_position == 0)   // If this is the first time we switch to Position control, actual position is our target position
          {
            if (target_alt_position == 0)
            {
              BMP_target_altitude = BMP_Altitude;  //first time setting current altitude is target altitude

              if (BMP_mode == 0)  // If Altitude hold has been disabled reset variables
                Clean_BMP_vars();
    
              target_alt_position = 1;  //target altitude has been set
            }

            Get_Target_Position_GPS(); // Sets target_lattitude, target_longitude and target_position
            Clean_GPS_vars();
          }        

          Read_GPS();
          gled_speed = 1200;  //Sets green led Speed to slow
          Attitude_control_v3();
          if (BMP_mode == 1)   // mst - was on AP_mode: probabely altitude bug we have searched for
            BMP_Altitude_control(BMP_target_altitude);

        break;
    } //End of switch AP_mode


    motorArmed = Get_Arm_Disarm_Motors(motorArmed);  // Check Throttle down and full yaw right for more than 2 seconds


    Get_Motor_Values(motorArmed);  // Gets values for rightMotor,leftMotor,frontMotor,backMotor


    Output_Motors();  // Outputs the values for the motors

    Stabilize_Camera();  // Outputs the values for Pitch and Roll to stabilize camera 


    #ifdef CONFIGURATOR
      if((millis()-tlmTimer)>=100) 
      {
        readSerialCommand();
        sendSerialTelemetry();
        tlmTimer = millis();
      }
    #else
      SerPriln();  // Line END 
    #endif


    Show_Leds();  // Shows leds status

  } // Main loop 200Hz
} // End of void loop()

// END of Arducopter.pde



