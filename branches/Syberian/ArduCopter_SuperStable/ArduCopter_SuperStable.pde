/* ********************************************************************** */
/*             ArduCopter & ArduPirates Quad & Hexa copter code           */
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
/*           Philipp Maloney, Norbert, Hein, Igor.                        */
/* Date : 12-7-2010                                                       */
/* Version : 1.6                                                          */
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

/**** Switch Functions *****
 AUX2 OFF && GEAR OFF = Acro Mode (AP_mode = 0)
 AUX2 ON  && GEAR OFF = Stable Mode (Heading Hold only) (AP_mode = 2)
 AUX2 ON  && GEAR ON  = SuperStable Mode (Altitude Hold and Heading Hold if no throttle stick movement) (AP_mode = 1)
 AUX2 OFF && GEAR ON  = Position & Altitude Hold (AP_mode = 3)
 
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

 ********************************************************************** */

 
 /**********************************************/

// FRAME TYPE
//Please define your frame type along with the subtype (flight_mode), if applicable.
//The board pinout is the same as in the MultiWii
/**********************************************/

#define Tri 
//#define Quad
// #define FLIGHT_MODE_+4
// #define FLIGHT_MODE_X4
// #define FLIGHT_MODE_Y4
//#define Hexa
//#define FLIGHT_MODE_+6
// #define FLIGHT_MODE_X6
// #define FLIGHT_MODE_Y6

/**********************************************/

// If you need a yaw reverse, uncomment this:
//#define YAW_REVERSE

 
 
 
//APM motor remap to the MultiWii-style by Syberian
//See http://static.rcgroups.net/forums/attachments/2/9/1/1/2/3/a3927050-35-8%20Connection%20diagram%20Seeeduino%20Mega%20v1.7.jpg
// and APM_RC.cpp library top for pinout info


/* *****************************************************************************
                  ArduPirate Configuration Setup
   ***************************************************************************** */

//GPS Config
#define IsGPS               // Do we have a GPS connected?

//#define MTK_GPS           // MediaTEK DIY Drones GPS. 
//#define IsNEWMTEK         // Do we have MTEK with new firmware?
//#include <GPS_MTK.h>      

//#define UBLOX_GPS         // uBlox GPS
//#include <GPS_UBLOX.h>   

#include <GPS_NMEA.h>       // General NMEA GPS
#define NMEA_GPS            



#define IsMAG               // Do we have a Magnetometer connected? If have, remember to activate it from Configurator !
#define UseBMP              // Do we want to use the barometer sensor on the IMU?
#define IsSonar             // Do we have Sonar installed // //XL-Maxsonar EZ4 - Product 9495 from SPF.  I use Analgue output.
#define CONFIGURATOR        // Do we use Configurator or normal text output over serial link?
//#define IsCAMERATRIGGER   // Do we want to use a servo to trigger a camera regularely
//#define IsXBEE            // Do we have a telemetry connected, eg. XBee connected on Telemetry port?
//#define IsAM              // Do we have motormount LED's? (AM = Atraction Mode)
//#define UseAirspeed       // Do we have an airspeed sensor?
//#define BATTERY_EVENT     // Do we have battery alarm wired up?
//#define MOTORMOUNT_LEDS   // Do we have motormount LEDs attached to AN4 and AN5 (NOT the same as IsAM)? See bottom of the file for details
//#define RELAY_LED_LIGHTS  // Do we have LED lights attached through the relay? Turned on and off with Rx Ch7 (FIXME: should be configurable)



//#define PPM_SUM // enable for using a group PPM channel
//MOVED to APM_RC.CPP library - DO NOT uncomment! OFF by default

// Split pins for Arduino MEGA board placement (DO NOT uncomment!)

/*#define THROTTLEPIN                0  //PIN 62 =  PIN A8
  #define ROLLPIN                    1  //PIN 63 =  PIN A9
  #define PITCHPIN                   2  //PIN 64 =  PIN A10
  #define YAWPIN                     3  //PIN 65 =  PIN A11
  #define AUX1PIN                    4  //PIN 66 =  PIN A12
  #define AUX2PIN                    5  //PIN 67 =  PIN A13
  #define CAM1PIN                    6  //PIN 68 =  PIN A14
  #define CAM2PIN                    7  //PIN 69 =  PIN A15
*/


/**********************************************/

//  Magnetometer Setup

// To get Magneto offsets, switch to CLI mode and run offset calibration. During calibration
// you need to roll/bank/tilt/yaw/shake etc your ArduCoptet. Don't kick like Jani always does :)
#define MAGOFFSET -76,22.5,-55.5  // You will have to determine your own settings.

// MAGCALIBRATION is the correction angle in degrees (can be + or -). You must calibrating your magnetometer to show magnetic north correctly.
// After calibration you will have to determine the declination value between Magnetic north and true north, see following link
// http://code.google.com/p/arducopter/wiki/Quad_Magnetos under additional settings. Both values have to be incorporated
// Example:  Magnetic north calibration show -1.2 degrees offset and declination (true north) is -5.6 then the MAGCALIBRATION shoud be -6.8.
// Your GPS readings is based on true north.
// For Magnetic north calibration make sure that your Magnetometer is truly showing 0 degress when your ArduQuad is looking to the North.
// Use a real compass (! not your iPhone) to point your ArduQuad to the magnetic north and then adjust this 
// value until you have a 0 dergrees reading in the configurator's artificial horizon. 
// Once you have achieved this fine tune in the configurator's serial monitor by pressing "T" (capital t).

#define MAGCALIBRATION -21.65 // You have to determine your own setting.

// orientations for DIYDrones magnetometer
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_FORWARD
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_FORWARD_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_BACK_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_BACK
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_BACK_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_FORWARD_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_FORWARD
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
#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD_LEFT

//Low Battery Alarm
#define LOW_VOLTAGE      12.5   // Pack voltage at which to trigger alarm (Set to about 1 volt above ESC low voltage cutoff)
#define VOLT_DIV_OHMS    3690   // Value of resistor (in ohms) used on voltage divider


/* Preliminary/Experimental support for Wii Hardware,
   replaces the DIYDRONES.COM Oilpan */
//#define USE_WII
//#include <APM_Wii.h>


/******************************************************** */
/* END CONFIGURATION                                      */
/******************************************************** */



//Disable some IMU functions when using Wii
#ifndef USE_WII
//  #define USE_DATAFLASH
  #define USE_IMUADC
#endif


// Quick and easy hack to change FTDI Serial output to Telemetry port. Just activate #define IsXBEE some lines earlier
// Arduino 21 seems to work best with Telemetry.
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

#include <EEPROM.h>         // EEPROM storage for user configurable values
#include "ArduCopter.h"
#include "UserConfig.h"

#ifdef UseBMP
  #include <APM_BMP085.h>
#endif

/* Software version */
#define VER 1.6    // Current software version (only numeric values)


/* ***************************************************************************** */
/* ************************ CONFIGURATION PART ********************************* */
/* ***************************************************************************** */

// Normal users does not need to edit anything below these lines. If you have
// need, go and change them in UserConfig.h

/* ***************************************************************************** */
// STABLE MODE
// PI absolute angle control driving a P rate control
// Input : desired Roll, Pitch and Yaw absolute angles. Output : Motor commands
int ppm_m[8];
void Attitude_control_v3()
{
  #define MAX_CONTROL_OUTPUT 250
  float stable_roll,stable_pitch,stable_yaw;
  
  // ROLL CONTROL    
  if (AP_mode == F_MODE_SUPER_STABLE || AP_mode == F_MODE_STABLE)        // Normal Mode => Stabilization mode
    err_roll = command_rx_roll - ToDeg(roll);
  else
    err_roll = (command_rx_roll + command_gps_roll) - ToDeg(roll);  // Position control  
  err_roll = constrain(err_roll,-25,25);  // to limit max roll command...
  
  roll_I += err_roll*G_Dt;
  roll_I = constrain(roll_I,-20,20);

  // PID absolute angle control
  K_aux = KP_QUAD_ROLL; // Comment this out if you want to use transmitter to adjust gain
  stable_roll = K_aux*err_roll + KI_QUAD_ROLL*roll_I;
  
  // PD rate control (we use also the bias corrected gyro rates)
  err_roll = stable_roll - ToDeg(Omega[0]); // Omega[] is the raw gyro reading plus Omega_I, so it´s bias corrected
  control_roll = STABLE_MODE_KP_RATE_ROLL*err_roll;
  control_roll = constrain(control_roll,-MAX_CONTROL_OUTPUT,MAX_CONTROL_OUTPUT);

  // PITCH CONTROL
  if (AP_mode == F_MODE_SUPER_STABLE || AP_mode == F_MODE_STABLE)        // Normal mode => Stabilization mode
    err_pitch = command_rx_pitch - ToDeg(pitch);
  else                   // GPS Position hold
    err_pitch = (command_rx_pitch + command_gps_pitch) - ToDeg(pitch);  // Position Control
  err_pitch = constrain(err_pitch,-25,25);  // to limit max pitch command...
  
  pitch_I += err_pitch*G_Dt;
  pitch_I = constrain(pitch_I,-20,20);
 
  // PID absolute angle control
  K_aux = KP_QUAD_PITCH; // Comment this out if you want to use transmitter to adjust gain
  stable_pitch = K_aux*err_pitch + KI_QUAD_PITCH*pitch_I;
  
  // P rate control (we use also the bias corrected gyro rates)
  err_pitch = stable_pitch - ToDeg(Omega[1]);
  control_pitch = STABLE_MODE_KP_RATE_PITCH*err_pitch;
  control_pitch = constrain(control_pitch,-MAX_CONTROL_OUTPUT,MAX_CONTROL_OUTPUT);
  
  // YAW CONTROL
  err_yaw = command_rx_yaw - ToDeg(yaw);
  if (err_yaw > 180)    // Normalize to -180,180
    err_yaw -= 360;
  else if(err_yaw < -180)
    err_yaw += 360;
  err_yaw = constrain(err_yaw,-60,60);  // to limit max yaw command...
  
  yaw_I += err_yaw*G_Dt;
  yaw_I = constrain(yaw_I,-20,20);
 
  // PID absoulte angle control
  stable_yaw = KP_QUAD_YAW*err_yaw + KI_QUAD_YAW*yaw_I;
  // PD rate control (we use also the bias corrected gyro rates)
  err_yaw = stable_yaw - ToDeg(Omega[2]);
  control_yaw = STABLE_MODE_KP_RATE_YAW*err_yaw;
  control_yaw = constrain(control_yaw,-MAX_CONTROL_OUTPUT,MAX_CONTROL_OUTPUT);
}

// ACRO MODE
void Rate_control()
{
  static float previousRollRate, previousPitchRate, previousYawRate;
  float currentRollRate, currentPitchRate, currentYawRate;

  // ROLL CONTROL
  currentRollRate = Sensor_Input[GYRO_ROLL];      // I need a positive sign here

  err_roll = ((ch_roll - roll_mid) * xmitFactor) - currentRollRate;

  roll_I += err_roll * G_Dt;
  roll_I = constrain(roll_I, -20, 20);

  roll_D = currentRollRate - previousRollRate;
  previousRollRate = currentRollRate;

  // PID control
  control_roll = Kp_RateRoll * err_roll + Kd_RateRoll * roll_D + Ki_RateRoll * roll_I; 

  // PITCH CONTROL
  currentPitchRate = Sensor_Input[GYRO_PITCH];
  err_pitch = ((ch_pitch - pitch_mid) * xmitFactor) - currentPitchRate;

  pitch_I += err_pitch*G_Dt;
  pitch_I = constrain(pitch_I,-20,20);

  pitch_D = currentPitchRate - previousPitchRate;
  previousPitchRate = currentPitchRate;

  // PID control
  control_pitch = Kp_RatePitch*err_pitch + Kd_RatePitch*pitch_D + Ki_RatePitch*pitch_I; 

  // YAW CONTROL
  currentYawRate = Sensor_Input[GYRO_YAW];
  err_yaw = ((ch_yaw - yaw_mid) * xmitFactor) - currentYawRate;

  yaw_I += err_yaw*G_Dt;
  yaw_I = constrain(yaw_I, -20, 20);

  yaw_D = currentYawRate - previousYawRate;
  previousYawRate = currentYawRate;

  // PID control
  K_aux = KP_QUAD_YAW; // Comment this out if you want to use transmitter to adjust gain
  control_yaw = Kp_RateYaw*err_yaw + Kd_RateYaw*yaw_D + Ki_RateYaw*yaw_I; 
}

// RATE CONTROL MODE
// Using Omega vector (bias corrected gyro rate)
void Rate_control_v2()
{
  static float previousRollRate, previousPitchRate, previousYawRate;
  float currentRollRate, currentPitchRate, currentYawRate;
  
  // ROLL CONTROL
  currentRollRate = ToDeg(Omega[0]);  // Omega[] is the raw gyro reading plus Omega_I, so it´s bias corrected
  
  err_roll = ((ch_roll- roll_mid) * xmitFactor) - currentRollRate;
  
  roll_I += err_roll*G_Dt;
  roll_I = constrain(roll_I,-20,20);

  roll_D = (currentRollRate - previousRollRate)/G_Dt;
  previousRollRate = currentRollRate;
  
  // PID control
  control_roll = Kp_RateRoll*err_roll + Kd_RateRoll*roll_D + Ki_RateRoll*roll_I; 
  
  // PITCH CONTROL
  currentPitchRate = ToDeg(Omega[1]);  // Omega[] is the raw gyro reading plus Omega_I, so it´s bias corrected
  err_pitch = ((ch_pitch - pitch_mid) * xmitFactor) - currentPitchRate;
  
  pitch_I += err_pitch*G_Dt;
  pitch_I = constrain(pitch_I,-20,20);

  pitch_D = (currentPitchRate - previousPitchRate)/G_Dt;
  previousPitchRate = currentPitchRate;
 
  // PID control
  control_pitch = Kp_RatePitch*err_pitch + Kd_RatePitch*pitch_D + Ki_RatePitch*pitch_I; 
  
  // YAW CONTROL
  currentYawRate = ToDeg(Omega[2]);  // Omega[] is the raw gyro reading plus Omega_I, so it´s bias corrected;
  err_yaw = ((ch_yaw - yaw_mid)* xmitFactor) - currentYawRate;
  
  yaw_I += err_yaw*G_Dt;
  yaw_I = constrain(yaw_I,-20,20);

  yaw_D = (currentYawRate - previousYawRate)/G_Dt;
  previousYawRate = currentYawRate;
 
  // PID control
  K_aux = KP_QUAD_YAW; // Comment this out if you want to use transmitter to adjust gain
  control_yaw = Kp_RateYaw*err_yaw + Kd_RateYaw*yaw_D + Ki_RateYaw*yaw_I; 
}

// Maximun slope filter for radio inputs... (limit max differences between readings)
int channel_filter(int ch, int ch_old)
{
  int diff_ch_old;

  if (ch_old==0)      // ch_old not initialized
    return(ch);
  diff_ch_old = ch - ch_old;      // Difference with old reading
  if (diff_ch_old < 0)
  {
    if (diff_ch_old <- 60)
      return(ch_old - 60);        // We limit the max difference between readings
  }
  else
  {
    if (diff_ch_old > 60)    
      return(ch_old + 60);
  }
  return((ch + ch_old) >> 1);   // Small filtering
} 


/* ************************************************************ */
/* **************** MAIN PROGRAM - SETUP ********************** */
/* ************************************************************ */
void setup()
{
  int i;

  pinMode(LED_Yellow,OUTPUT); //Yellow LED A  (PC1)
  pinMode(LED_Red,OUTPUT);    //Red LED B     (PC2)
  pinMode(LED_Green,OUTPUT);  //Green LED C   (PC0)

  pinMode(SW1_pin,INPUT);     //Switch SW1 (pin PG0)

  pinMode(RELE_pin,OUTPUT);   // Rele output
  digitalWrite(RELE_pin,LOW);

#ifdef MOTORMOUNT_LEDS
  pinMode( MM_LED1, OUTPUT );   // Motormount LEDs
  digitalWrite( MM_LED1, LOW );
  mm_led1_speed  = -1;          // Lights off
  mm_led1_status = LOW;
  pinMode( MM_LED2, OUTPUT );
  digitalWrite( MM_LED2, LOW );
  mm_led2_speed  = -1;          // Lights off
  mm_led2_status = LOW;
#endif
  
  APM_RC.Init();             // APM Radio initialization

  // RC channels Initialization (Quad motors) Wii 
  APM_RC.OutputCh(0,MIN_COMMAND);
  APM_RC.OutputCh(1,MIN_COMMAND);
  APM_RC.OutputCh(2,MIN_COMMAND);
  APM_RC.OutputCh(3,MIN_COMMAND);
  APM_RC.OutputCh(4,MIN_COMMAND);  // Motors stoped
  APM_RC.OutputCh(5,MIN_COMMAND);
  APM_RC.OutputCh(6,MIN_COMMAND);
  APM_RC.OutputCh(7,MIN_COMMAND);
#ifdef Tri
  APM_RC.OutputCh(0,1500); // servo
#endif  

  
  //  delay(1000); // Wait until frame is not moving after initial power cord has connected
  for(i = 0; i <= 50; i++) {
    digitalWrite(LED_Green, HIGH);
    digitalWrite(LED_Yellow, HIGH);
    digitalWrite(LED_Red, HIGH);
    delay(20);
    digitalWrite(LED_Green, LOW);
    digitalWrite(LED_Yellow, LOW);
    digitalWrite(LED_Red, LOW);
    delay(20);
  }

#ifdef USE_IMUADC
  APM_ADC.Init();            // APM ADC library initialization
#endif

#ifdef USE_WII
  APM_Wii.Init();
#endif

#ifdef USE_DATAFLASH
  DataFlash.Init();          // DataFlash log initialization
#endif

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

#ifdef UseBMP
    APM_BMP085.Init();   // APM ADC initialization
#endif
  readUserConfig(); // Load user configurable items from EEPROM

  // Safety measure for Channel mids
  
  if(roll_mid < 1400 || roll_mid > 1600) roll_mid = 1500;
  if(pitch_mid < 1400 || pitch_mid > 1600) pitch_mid = 1500;
  if(yaw_mid < 1400 || yaw_mid > 1600) yaw_mid = 1500;
  
  // Safety measure for Channel mids finished
  
#ifdef IsMAG
    if (MAGNETOMETER == 1) 
    {
      APM_Compass.Init();  // I2C initialization
      APM_Compass.SetOrientation(MAGORIENTATION);
      APM_Compass.SetOffsets(MAGOFFSET);
      APM_Compass.SetDeclination(ToRad(MAGCALIBRATION));
    }
#endif

#ifdef USE_DATAFLASH
  DataFlash.StartWrite(1);   // Start a write session on page 1
#endif

  SerBeg(SerBau);                      // Initialize SerialXX.port, IsXBEE define declares which port
  
#ifndef CONFIGURATOR  
    SerPri("ArduCopter Quadcopter v");
    SerPriln(VER);
    SerPri("Serial ready on port: ");    // Printout greeting to selecter serial port
    SerPriln(SerPor);                    // Printout serial port name
#endif

#ifdef BATTERY_EVENT
  pinMode(LOW_BATTERY_OUT, OUTPUT);   // Battery Alarm output
  digitalWrite(LOW_BATTERY_OUT, LOW); // Silence Alarm
#endif
  
  // Check if we enable the DataFlash log Read Mode (switch)
  // If we press switch 1 at startup we read the Dataflash eeprom
#ifdef USE_DATAFLASH
  while (digitalRead(SW1_pin)==0)
  {
    SerPriln("Entering Log Read Mode...");
    Log_Read(1,2000);
    delay(30000);
  }
#endif

  Calibrate_Gyro_Offsets();

#ifdef USE_DATAFLASH
  DataFlash.StartWrite(1);   // Start a write session on page 1
#endif

  timer = millis();
  tlmTimer = millis();
  Update_Sensors();        // Initialize ADC readings...
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
void loop(){

  int aux;
  float aux_float;
  
  //Log variables
  int log_roll;
  int log_pitch;
  int log_yaw;

  if((millis()-timer)>=5)   // Main loop 200Hz
  {
    Magneto_counter++;
    GPS_counter++;
    cameracounteron++;

    timer_old = timer;
    timer=millis();
    G_Dt = (timer-timer_old)*0.001;      // Real time of loop run 

    //Read sensor inputs
    Update_Sensors();
    
#ifdef BATTERY_EVENT
    read_battery();
#endif

#ifdef IsMAG
      if (MAGNETOMETER == 1) {
        if (Magneto_counter > 20)  // Read compass data at 10Hz... (20 loop runs)
        {
          Magneto_counter = 0;
          APM_Compass.Read();     // Read magnetometer
          APM_Compass.Calculate(roll,pitch);  // Calculate heading
        }
      }
#endif   

/*  // In the future the GCS can do the conversion to cm and give as the 
    // barometric pressure for a specific altitude...less processing power needed.
// Former translation from pressure&temperature into cm        
//***********************************************************************************
//        tempPresAlt = float(APM_BMP085.Press)/101325.0;
//        tempPresAlt = pow(tempPresAlt, 0.190295);
//        BMP_Altitude = (1.0 - tempPresAlt) * 4433000;      // Altitude in cm
//***********************************************************************************/

#ifdef UseBMP
    BMP_counter++;
    if (BMP_counter > 10)  // Reading Barometric data at 20Hz
    {
      APM_BMP085.Read();
      BMP_counter = 0;
      press_alt = APM_BMP085.GetAltitude();  
      Baro_new_data=1;
    }
#endif

#ifdef IsCAMERATRIGGER
      if (cameracounteron < 1000)   //interval in seconds between triggering the camera (1000 = 5 seconds)
        APM_RC.OutputCh(9, 2000);    //output for the servo - zero position
      else
        APM_RC.OutputCh(9, 1000);     //output for the servo - push
      if (cameracounteron > 1200)   //pushduration of the trigger (interval time + pushduration -> +200 = 1 second)    
      {
        cameracounteron = 0;
      }
#endif


    Matrix_update(); 
    Normalize();
    Drift_correction();
    Euler_angles();

#ifndef CONFIGURATOR    
      SerPri(log_roll);
      SerPri(",");
      SerPri(log_pitch);
      SerPri(",");
      SerPri(log_yaw);
#endif
 
#ifdef IsSonar
    sonar_read = APM_ADC.Ch(7);   // Sonar is connected to pitot input in shield (Infront of shield, marked pitot between led's)
                                  // At the bottom of shield is written gnd +5V IN.  We use the IN....
                                  //XL-Maxsonar EZ4 - Product 9495 from SPF.  I use Analgue output. (pin 3)  Will still consider PW..pin2
    if (sonar_read ==-1)        // For testing purposes I am monitoring sonar_read value.
      Use_BMP_Altitude = 1; 
    else  
    {  
      sonar_adc += sonar_read;      
      Sonar_Counter++;
      Use_BMP_Altitude = 0;
    }
#endif
    
#ifdef IsSonar
  if (Sonar_Counter > 10)   // New sonar data at 20Hz
      {
      sonar_adc = sonar_adc/Sonar_Counter;  // Average sensor readings (to filter noise)
      Sonar_value = Sonar_Sensor_Filter(sonar_adc,Sonar_value,4);
      sonar_adc=0;
      Sonar_Counter=0;
      Sonar_new_data=1;  // New sonar data flag
      }
#endif


    if (APM_RC.GetState() == 1)   // New radio frame?
    {
      // Commands from radio Rx... 
      // Stick position defines the desired angle in roll, pitch and yaw
      ch_roll = channel_filter(APM_RC.InputCh(0) * ch_roll_slope + ch_roll_offset, ch_roll);
      ch_pitch = channel_filter(APM_RC.InputCh(1) * ch_pitch_slope + ch_pitch_offset, ch_pitch);
      ch_throttle = channel_filter(APM_RC.InputCh(2), ch_throttle); // Transmiter calibration not used on throttle
      ch_yaw = channel_filter(APM_RC.InputCh(3) * ch_yaw_slope + ch_yaw_offset, ch_yaw);
      ch_gear = APM_RC.InputCh(4) * ch_gear_slope + ch_gear_offset;
      ch_aux2 = APM_RC.InputCh(5) * ch_aux2_slope + ch_aux2_offset;
      ch_aux1 = APM_RC.InputCh(6);  // flight mode 3-position switch.
      // Channel Aux1 can be used for PID tuning or Changing Camera Pitch Angles... 
      
      #define STICK_TO_ANGLE_FACTOR 12.0;
      
      command_rx_roll = (ch_roll-roll_mid) / STICK_TO_ANGLE_FACTOR;
      command_rx_pitch = (ch_pitch-pitch_mid) / STICK_TO_ANGLE_FACTOR;



      if (abs(ch_yaw-yaw_mid)<12)   // Take into account a bit of "dead zone" on yaw
        aux_float = 0.0;
      else
        aux_float = (ch_yaw-yaw_mid) / 180.0;
      command_rx_yaw += aux_float;
      if (command_rx_yaw > 180)         // Normalize yaw to -180,180 degrees
        command_rx_yaw -= 360.0;
      else if (command_rx_yaw < -180)
        command_rx_yaw += 360.0;

// **************************************************************************		
//     We read the Quad Mode from Gear and Aux2 Channel on radio (example Spektrum Radio)

//     AUX2 OFF && GEAR OFF = Acrobatic mode
//     AUX2 ON  && GEAR OFF = Stable Mode (Heading Hold only)
//     AUX2 ON  && GEAR ON  = SuperStable Mode (Altitude Hold and Heading Hold if no throttle stick movement)
//     AUX2 OFF && GEAR ON  = Position hold mode and Altitude Hold  


      if (ch_aux2 < 1250 && ch_gear > 1800)
      {
        AP_mode = F_MODE_STABLE  ;      // Stable mode (Heading Hold only)
        digitalWrite(LED_Yellow,LOW);  // Yellow LED off
      }
      else if (ch_gear > 1800 && ch_aux2 > 1800)
      {
        AP_mode = F_MODE_ABS_HOLD;      // Position & Altitude hold mode (GPS position control & Altitude control)
        digitalWrite(LED_Yellow,HIGH);  // Yellow LED On
      }
      else if (ch_gear < 1250 && ch_aux2 > 1800)
      {
        AP_mode = F_MODE_SUPER_STABLE;  // Super Stable Mode (Stable mode & Altitude hold mode)
        digitalWrite(LED_Yellow,LOW);   // Yellow LED off
      }
      else 
      {
        AP_mode = F_MODE_ACROBATIC;     // Acrobatic mode
        digitalWrite(LED_Yellow,LOW); // Yellow LED off
      } //  End reading  the Quad Mode from Gear and Aux2 Channel on radio (example Spektrum Radio)

#ifdef RELAY_LED_LIGHTS
      if( ch_aux1 > 1500 ) {  // FIXME, Rx channel and value should be configurable
        digitalWrite( RELE_pin, HIGH );
      } else {
        digitalWrite( RELE_pin, LOW );
      }
#endif
    }  // End reading new radio frame
	  
   if (AP_mode==F_MODE_ABS_HOLD)  // Position & Altitude Hold Mode
   {
      heading_hold_mode = 1;
      if (target_position == 0)   // If this is the first time we switch to Position control, actual position is our target position
      {
#ifdef IsGPS
        target_lattitude = GPS.Lattitude;
        target_longitude = GPS.Longitude;
#endif
        target_position = 1;
        gps_err_roll = 0;
        gps_err_pitch = 0;
        gps_roll_D = 0;
        gps_pitch_D = 0;
        gps_err_roll_old = 0;
        gps_err_pitch_old = 0;
        command_gps_roll = 0;
        command_gps_pitch = 0;
        // Reset I terms
        gps_roll_I = 0;
        gps_pitch_I = 0;
      }        
      if (target_alt_position == 0)   // If this is the first time we switch to Altitude control, actual position is our target position
      {
        target_sonar_altitude = Sonar_value;
#ifdef IsSonar
        if (target_sonar_altitude == 0)
          Use_BMP_Altitude = 1;      // We test if Sonar sensor is not out of range, else we use BMP sensor for Alitude Hold.
        else if (target_sonar_altitude > 450)
          Use_BMP_Altitude = 1; 
        else
          Use_BMP_Altitude = 0;
#endif          
        Initial_Throttle = ch_throttle;
        ch_throttle_altitude_hold = ch_throttle;
        target_baro_altitude = press_alt;
       // Reset I terms
        altitude_I = 0;
        altitude_D = 0;
        err_altitude_old = 0;
        err_altitude = 0;
        target_alt_position=1;
      }        
    }
    else if (AP_mode==F_MODE_SUPER_STABLE)  // Super Stable Mode (Stable & Altitude Hold)
    {
      heading_hold_mode = 1;
      target_position = 0;
      if (target_alt_position == 0)   // If this is the first time we switch to Altitude control, actual position is our target position
      {
        target_alt_position = 1;
        target_sonar_altitude = Sonar_value;
#ifdef IsSonar
        if (target_sonar_altitude == 0)
          Use_BMP_Altitude = 1;      // We test if Sonar sensor is not out of range, else we use BMP sensor for Alitude Hold.
        else if (target_sonar_altitude > 450)
          Use_BMP_Altitude = 1; 
        else
          Use_BMP_Altitude = 0;
#endif
        Initial_Throttle = ch_throttle;
        ch_throttle_altitude_hold = ch_throttle;
        target_baro_altitude = press_alt;
        // Reset I terms
        altitude_I = 0;
        altitude_D = 0;
        err_altitude_old = 0;
        err_altitude = 0;
      }        
      gps_err_roll = 0;
      gps_err_pitch = 0;
      gps_roll_D = 0;
      gps_pitch_D = 0;
      gps_err_roll_old = 0;
      gps_err_pitch_old = 0;
      command_gps_roll = 0;
      command_gps_pitch = 0;
      // Reset I terms
      gps_roll_I = 0;
      gps_pitch_I = 0;
    }
    else if (AP_mode==F_MODE_STABLE)  // Stable Mode (Heading Hold only)
    {
      target_position = 0;
      heading_hold_mode = 1;
      target_alt_position = 0;
      // Reset I terms
      altitude_I = 0;
      altitude_D = 0;
      err_altitude_old = 0;
      err_altitude = 0;
      gps_roll_I = 0;
      gps_pitch_I = 0;
      gps_err_roll = 0;
      gps_err_pitch = 0;
      gps_roll_D = 0;
      gps_pitch_D = 0;
      gps_err_roll_old = 0;
      gps_err_pitch_old = 0;
      command_gps_roll = 0;
      command_gps_pitch = 0;
    }
    else if (AP_mode == F_MODE_ACROBATIC)  //Acrobatic Mode
    {
     // Reset I terms
      altitude_I = 0;
      altitude_D = 0;
      err_altitude_old = 0;
      err_altitude = 0;
      gps_roll_I = 0;
      gps_pitch_I = 0;
      gps_err_roll = 0;
      gps_err_pitch = 0;
      gps_roll_D = 0;
      gps_pitch_D = 0;
      gps_err_roll_old = 0;
      gps_err_pitch_old = 0;
      command_gps_roll = 0;
      command_gps_pitch = 0;
      heading_hold_mode = 0;
      target_position = 0;
      target_alt_position = 0;
    }

    //Read GPS
#ifdef IsGPS
    if (GPS_counter > 3)  // Reading GPS data at 60 Hz
    {
      GPS_counter = 0;
      GPS.Read();
    } 
    if (GPS.NewData)  // New GPS data?
    {
      GPS_timer_old=GPS_timer;   // Update GPS timer
      GPS_timer = timer;
      GPS_Dt = (GPS_timer-GPS_timer_old)*0.001;   // GPS_Dt
      GPS.NewData = 0;    // We Reset the flag...

      if (GPS.Fix)
        digitalWrite(LED_Red,HIGH);  // GPS Fix => Blue LED
      else
        digitalWrite(LED_Red,LOW);

      if (AP_mode == F_MODE_ABS_HOLD)
      {
        if ((target_position == 1) && (GPS.Fix))
        {
          Position_control(target_lattitude,target_longitude);  // Call position hold routine
        }
        else
        {
          gps_roll_I = 0;
          gps_pitch_I = 0;
          gps_err_roll = 0;
          gps_err_pitch = 0;
          gps_roll_D = 0;
          gps_pitch_D = 0;
          gps_err_roll_old = 0;
          gps_err_pitch_old = 0;
          command_gps_roll = 0;
          command_gps_pitch = 0;
        }
      }
    }
#endif
 
    if (AP_mode == F_MODE_ABS_HOLD || AP_mode == F_MODE_SUPER_STABLE)  // Altitude control
    {
#ifdef IsSonar
      if (Sonar_new_data == 1 && Use_BMP_Altitude == 0)  // Do altitude control on each new sonar data
      { 
//         ch_throttle_altitude_hold = (ch_throttle_altitude_hold*0.5) + (Altitude_control_Sonar_v2(Sonar_value,target_sonar_altitude)*0.5);
         ch_throttle_altitude_hold = (ch_throttle_altitude_hold*0.5) + (Altitude_control_Sonar(Sonar_value,target_sonar_altitude)*0.5);
         Sonar_new_data=0;
      }
#endif
#ifdef UseBMP
      if (Baro_new_data == 1 && Use_BMP_Altitude == 1)
      {
//        ch_throttle_altitude_hold = Altitude_control_baro_v2(press_alt,target_baro_altitude);
        ch_throttle_altitude_hold = Altitude_control_baro(target_baro_altitude,press_alt);
        Baro_new_data=0;
      }
#endif
    }
      
      
    if (AP_mode == F_MODE_STABLE || AP_mode == F_MODE_SUPER_STABLE || AP_mode == F_MODE_ABS_HOLD) 
    {
      gled_speed = 1200;
      Attitude_control_v3();
    }
    else
    {
      gled_speed = 400;
      Rate_control_v2();
      // Reset yaw, so if we change to stable mode we continue with the actual yaw direction
      command_rx_yaw = ToDeg(yaw);
    }

#ifdef IsSonar
    if ((AP_mode == F_MODE_ABS_HOLD || AP_mode == F_MODE_SUPER_STABLE) && Use_BMP_Altitude == 0)  // Altitude control
      ch_throttle = ch_throttle_altitude_hold;
#endif

#ifdef UseBMP
    if ((AP_mode == F_MODE_ABS_HOLD || AP_mode == F_MODE_SUPER_STABLE) && Use_BMP_Altitude == 1)  // Altitude control
      ch_throttle = (ch_throttle_altitude_hold + 20);
#endif
    
    // Arm motor output : Throttle down and full yaw right for more than 2 seconds
    if (ch_throttle < MIN_THROTTLE) {
      control_yaw = 0;
      command_rx_yaw = ToDeg(yaw);
      if (ch_yaw > 1850) {
        if (Arming_counter > ARM_DELAY){
          if(ch_throttle > 800) 
          {
            motorArmed = 1;
            minThrottle = MIN_THROTTLE;  // A minimun value for mantain a bit if throttle
//            motorArmed = 0;                 // Disable sometimes motors when testing sensors. 
//            minThrottle = MIN_THROTTLE;
          }
        }
        else
          Arming_counter++;
      }
      else
        Arming_counter=0;
		
      // To Disarm motor output : Throttle down and full yaw left for more than 2 seconds
      if (ch_yaw < 1150) {
        if (Disarming_counter > DISARM_DELAY){
          motorArmed = 0;
          minThrottle = MIN_COMMAND;
        }
        else
          Disarming_counter++;
      }
      else
        Disarming_counter=0;
    }
    else{
      Arming_counter=0;
      Disarming_counter=0;
    }


    if (motorArmed == 1) {   
#ifdef IsAM
      digitalWrite(FR_LED, HIGH);    // AM-Mode
#endif

						// fill the motor massive with data

//3,5,6,2,7,8,9,10
//1,3,4,0,5,6 - motor order
//0,1,2,3,4,5,6,7
//ppm_m[8];
for (byte i=0;i<7;i++) ppm_m[i]=MIN_COMMAND; // stop all the motors
//control_roll=-control_roll;
control_pitch=-control_pitch;
#ifdef YAW_REVERSE
control_yaw=-control_yaw;
#endif
#ifdef Tri
        ppm_m[1] = constrain(ch_throttle                + 1.33*control_pitch, minThrottle, 2000);
        ppm_m[3] = constrain(ch_throttle - control_roll - 0.66*control_pitch, minThrottle, 2000);
        ppm_m[4] = constrain(ch_throttle + control_roll - 0.66*control_pitch, minThrottle, 2000);
		ppm_m[0] = constrain(1500 + control_yaw * 2                         , 1000, 2000); //Servo
#endif
#ifdef Quad
   // Quadcopter mix
#ifdef FLIGHT_MODE_+4
        ppm_m[1] = constrain(ch_throttle                + control_pitch - control_yaw, minThrottle, 2000);
        ppm_m[3] = constrain(ch_throttle - control_roll                 + control_yaw, minThrottle, 2000);
        ppm_m[4]  = constrain(ch_throttle + control_roll                 + control_yaw, minThrottle, 2000);
        ppm_m[0]  = constrain(ch_throttle                - control_pitch - control_yaw, minThrottle, 2000);
#endif
#ifdef FLIGHT_MODE_X4
        ppm_m[1] = constrain(ch_throttle - control_roll + control_pitch - control_yaw, minThrottle, 2000); // Right motor
        ppm_m[3] = constrain(ch_throttle - control_roll - control_pitch + control_yaw, minThrottle, 2000);  // Left motor
        ppm_m[4] = constrain(ch_throttle + control_roll + control_pitch + control_yaw, minThrottle, 2000); // Front motor
        ppm_m[0] = constrain(ch_throttle + control_roll - control_pitch - control_yaw, minThrottle, 2000);  // Back motor
#endif
#ifdef FLIGHT_MODE_Y4
        ppm_m[1] = constrain(ch_throttle + control_pitch - control_yaw, minThrottle, 2000); 
        ppm_m[3] = constrain(ch_throttle - control_roll - control_pitch, minThrottle, 2000);
        ppm_m[4] = constrain(ch_throttle + control_pitch + control_yaw, minThrottle, 2000); 
        ppm_m[0] = constrain(ch_throttle + control_roll - control_pitch, minThrottle, 2000);
#endif
#endif
#ifdef Hexa
   // Hexacopter mix
#ifdef FLIGHT_MODE_+6
        ppm_m[1] = constrain(ch_throttle - control_roll/2 + control_pitch/2 + control_yaw, minThrottle, 2000); // Left Motor CW
        ppm_m[3] = constrain(ch_throttle - control_roll/2 - control_pitch/2 - control_yaw, minThrottle, 2000); // Left Motor CCW
        ppm_m[4] = constrain(ch_throttle + control_roll/2 + control_pitch/2 + control_yaw, minThrottle, 2000); // Right Motor CW
        ppm_m[0] = constrain(ch_throttle + control_roll/2 - control_pitch/2 - control_yaw, minThrottle, 2000); // Right Motor CCW
        ppm_m[5] = constrain(ch_throttle                  - control_pitch   + control_yaw, minThrottle, 2000); // Back Motor CW
        ppm_m[6] = constrain(ch_throttle                  + control_pitch   + control_yaw, minThrottle, 2000); // Back Motor CCW
#endif	
#ifdef FLIGHT_MODE_X6   
    ppm_m[1] = constrain(ch_throttle - control_roll/2 + control_pitch/2 + control_yaw, minThrottle, 2000); // Left Motor CW
    ppm_m[3] = constrain(ch_throttle - control_roll/2 - control_pitch/2 + control_yaw, minThrottle, 2000); // Left Motor CCW
    ppm_m[4] = constrain(ch_throttle + control_roll/2 + control_pitch/2 - control_yaw, minThrottle, 2000); // Right Motor CW
    ppm_m[0] = constrain(ch_throttle + control_roll/2 - control_pitch/2 - control_yaw, minThrottle, 2000); // Right Motor CCW
    ppm_m[5] = constrain(ch_throttle - control_roll                     - control_yaw, minThrottle, 2000); // Back Motor CW
    ppm_m[6] = constrain(ch_throttle + control_roll                     + control_yaw, minThrottle, 2000); // Back Motor CCW
#endif   
#ifdef FLIGHT_MODE_Y6
    motor[0] = rcCommand[THROTTLE]                 + axisPID[PITCH]*4/3 + YAW_DIRECTION * axisPID[YAW]; //REAR
    motor[1] = rcCommand[THROTTLE] - axisPID[ROLL] - axisPID[PITCH]*2/3 - YAW_DIRECTION * axisPID[YAW]; //RIGHT
    motor[2] = rcCommand[THROTTLE] + axisPID[ROLL] - axisPID[PITCH]*2/3 - YAW_DIRECTION * axisPID[YAW]; //LEFT
    motor[3] = rcCommand[THROTTLE]                 + axisPID[PITCH]*4/3 - YAW_DIRECTION * axisPID[YAW]; //UNDER_REAR
    motor[4] = rcCommand[THROTTLE] - axisPID[ROLL] - axisPID[PITCH]*2/3 + YAW_DIRECTION * axisPID[YAW]; //UNDER_RIGHT
    motor[5] = rcCommand[THROTTLE] + axisPID[ROLL] - axisPID[PITCH]*2/3 + YAW_DIRECTION * axisPID[YAW]; //UNDER_LEFT
   // Hexacopter Y6 mix
    ppm_m[1] = constrain(ch_throttle                + (control_pitch*4)/3 + control_yaw, minThrottle, 2000); // Left Motor CW
    ppm_m[3] = constrain(ch_throttle - control_roll - (control_pitch*2)/3 - control_yaw, minThrottle, 2000); // Left Motor CCW
    ppm_m[4] = constrain(ch_throttle + control_roll - (control_pitch*2)/3 - control_yaw, minThrottle, 2000); // Right Motor CW
    ppm_m[0] = constrain(ch_throttle                + (control_pitch*4)/3 - control_yaw, minThrottle, 2000); // Right Motor CCW
    ppm_m[5] = constrain(ch_throttle - control_roll - (control_pitch*2)/3 + control_yaw, minThrottle, 2000); // Back Motor CW
    ppm_m[6] = constrain(ch_throttle + control_roll - (control_pitch*2)/3 + control_yaw, minThrottle, 2000); // Back Motor CCW
#endif   
#endif

    }
    if (motorArmed == 0) {
      
#ifdef IsAM
        digitalWrite(FR_LED, LOW);    // AM-Mode
#endif
    
      digitalWrite(LED_Green,HIGH); // Ready LED on
//3,5,6,2,7,8,9,10
//1,3,4,0,5,6 - motor order
//ppm_m[8];
for (byte i=0;i<7;i++) ppm_m[i]=MIN_COMMAND; // stop all the motors
#ifdef Tri
ppm_m[0]=1500;// neutral servo
#endif

      roll_I = 0;     // reset I terms of PID controls
      pitch_I = 0;
      yaw_I = 0; 
      // Initialize yaw command to actual yaw when throttle is down...
      command_rx_yaw = ToDeg(yaw);
    }
    
for (byte i=0;i<7;i++) APM_RC.OutputCh(i,ppm_m[i]); // write motors data

    // Camera Stabilization
//    APM_RC.OutputCh(4, APM_RC.InputCh(6)+(pitch)*1000); // Tilt correction 
//    APM_RC.OutputCh(5, 1510+(roll)*-400);               // Roll correction


#ifndef CONFIGURATOR
      SerPriln();  // Line END 
#endif
  }
#ifdef CONFIGURATOR
    if((millis()-tlmTimer)>=100) {
      readSerialCommand();
      sendSerialTelemetry();
      tlmTimer = millis();
    }
#endif

  // AM and Mode status LED lights
  if(millis() - gled_timer > gled_speed) {
    gled_timer = millis();
    if(gled_status == HIGH) { 
      digitalWrite(LED_Green, LOW);
#ifdef IsAM      
        digitalWrite(RE_LED, LOW);
#endif
      gled_status = LOW;
    } 
    else {
      digitalWrite(LED_Green, HIGH);
#ifdef IsAM
        if(motorArmed) digitalWrite(RE_LED, HIGH);
#endif
      gled_status = HIGH;
    } 
  }

#ifdef MOTORMOUNT_LEDS
  // Motormount LEDs (NOT Attraction mode)
  // We use 2 LEDs - one for indicating Acro/Stable mode (MM_LED1)
  // and onw for indicating Position Hold mode (MM_LED2)
  //
  // MM_LED1
  // Off         -> motors disarmed
  // Rapid blink -> motors armed, Acro mode
  // Slow blink  -> motors armed, Stable mode, no Altitude Hold
  // On          -> motors armed, Superstable mode
  //
  // MM_LED2
  // Off         -> No GPS or no GPS fix
  // Rapid blink -> GPS fix, Position Hold inactive
  // Slow blink  -> GPS fix, Position Hold active, no Altitude Hold
  // On          -> GPS fix, Position Hold & Altitude Hold
  //
  // First, figure out how should we blink ;)
  if( !motorArmed ) {
    mm_led1_speed = -1;         // Off
  } else {
    switch( AP_mode ) {
      case F_MODE_ACROBATIC:
        mm_led1_speed = 400;    // Rapid blink
        break;
      case F_MODE_STABLE:
        mm_led1_speed = 1200;   // Slow blink
        break;
      case F_MODE_SUPER_STABLE:
      case F_MODE_ABS_HOLD:
        mm_led1_speed = 0;      // On
        break;
      default:  // should not really happen
        mm_led1_speed = -1;     // Off
    }
  }
  mm_led2_speed = -1;           // Off
#ifdef IsGPS
  if( GPS.Fix ) {
    switch( AP_mode ) {
      case F_MODE_ACROBATIC:
      case F_MODE_SUPER_STABLE:
        mm_led2_speed = 400;    // Rapid blink
        break;
      case F_MODE_STABLE:
        mm_led2_speed = 1200;   // Slow blink
        break;
      case F_MODE_ABS_HOLD:
        mm_led2_speed = 0;      // On
        break;
      default:  // should not really happen
        mm_led2_speed = -1;     // Off
    }
  }
#endif
  if( mm_led1_speed < 0 ) {
    digitalWrite( MM_LED1, LOW );
    mm_led1_status = LOW;
  } else if( mm_led1_speed == 0 ) {
    digitalWrite( MM_LED1, HIGH );
    mm_led1_status = HIGH;
  } else if(millis() - mm_led1_timer > mm_led1_speed) {
    mm_led1_timer = millis();
    if(mm_led1_status == HIGH) { 
      digitalWrite(MM_LED1, LOW);
      mm_led1_status = LOW;
    } else {
      digitalWrite(MM_LED1, HIGH);
      mm_led1_status = HIGH;
    } 
  }
  if( mm_led2_speed < 0 ) {
    digitalWrite( MM_LED2, LOW );
    mm_led2_status = LOW;
  } else if( mm_led2_speed == 0 ) {
    digitalWrite( MM_LED2, HIGH );
    mm_led2_status = HIGH;
  } else if(millis() - mm_led2_timer > mm_led2_speed) {
    mm_led2_timer = millis();
    if(mm_led2_status == HIGH) { 
      digitalWrite(MM_LED2, LOW);
      mm_led2_status = LOW;
    } else {
      digitalWrite(MM_LED2, HIGH);
      mm_led2_status = HIGH;
    } 
  }
#endif

} // End of void loop()

// END of Arducopter.pde


