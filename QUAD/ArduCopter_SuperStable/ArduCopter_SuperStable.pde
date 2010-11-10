/* ********************************************************************** */
/*                    ArduCopter Quadcopter code                          */
/*                                                                        */
/* Quadcopter code from AeroQuad project and ArduIMU quadcopter project   */
/* IMU DCM code from Diydrones.com                                        */
/* (Original ArduIMU code from Jordi Muñoz and William Premerlani)        */
/* Ardupilot core code : from DIYDrones.com development team              */
/* Authors : Arducopter development team                                  */
/*           Ted Carancho (aeroquad), Jose Julio, Jordi Muñoz,            */
/*           Jani Hirvinen, Ken McEwans, Roberto Navoni,                  */
/*           Sandro Benigno, Chris Anderson , Hein, Philipp Maloney       */
/* Date : 12-7-2010                                                       */
/* Version : 1.5  minor modificatios by Philipp Maloney (alt+head hold)   */
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
 AUX OFF && GEAR OFF = Acro Mode (AP_mode = 0)
 AUX ON  && GEAR OFF = SuperStable Mode (Altitude Hold and Heading Hold if no throttle stick movement) (AP_mode = 2)
 AUX ON  && GEAR ON  = Position Hold Mode (AP_mode = 1)
 AUX OFF && GEAR ON  = Postion & Altitude Hold (AP_mode = 3)
 
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

/* ********************************************************************** */

// Comment out with // modules that you are not using

#define IsGPS               // Do we have a GPS connected?
#define IsNEWMTEK           // Do we have MTEK with new firmware?
#define IsMAG               // Do we have a Magnetometer connected? If have, remember to activate it from Configurator !
#define UseBMP              // Do we want to use the barometer sensor on the IMU?
#define CONFIGURATOR        // Do se use Configurator or normal text output over serial link?
//#define IsTEL             // Do we have a telemetry connected, eg. XBee connected on Telemetry port?
//#define IsAM              // Do we have motormount LED's? (AM = Atraction Mode)
//#define UseAirspeed       // Do we have an airspeed sensor?
#define BATTERY_EVENT       // Do we have battery contro wired up? 

/**********************************************/

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

#ifdef IsMAG
// DIYDrones Magnetometer
//#define MAGORIENTATION  APM_COMPASS_COMPONENTS_UP_PINS_FORWARD     // This is default solution for ArduCopter
//#define MAGORIENTATION  APM_COMPASS_COMPONENTS_UP_PINS_BACK        // Alternative orientation for ArduCopter
#define MAGORIENTATION  APM_COMPASS_COMPONENTS_DOWN_PINS_FORWARD     // If you have soldered Magneto to IMU shield in WIki pictures shows
// or 
// Sparkfun Magnetometer
//#define MAGORIENTATION  APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_FORWARD // Sparkfun Magnetometer orientation.
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_BACK
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD

// To get Magneto offsets, switch to CLI mode and run offset calibration. During calibration
// you need to roll/bank/tilt/yaw/shake etc your ArduCoptet. Don't kick like Jani always does :)
//#define MAGOFFSET -77,56,-61  // Hein's calibration settings.
#define MAGOFFSET -81.00,-35.00,30.50  // You have to determine your own.

// MAGCALIBRATION is the correction angle in degrees (can be + or -). You need to do this for making sure
// that your Magnetometer is truly showing 0 degress when your AeroQuad is looking to the North.
// Use a real compass (! not your iPhone) to point your AeroQuad to the magnetic north and then adjust this 
// value until you have a 0 dergrees reading in the configurator's atificial horizont. 
// Once you have achieved this fine tune in the configurator's serial monitor by pressing "T" (capital t).
#define MAGCALIBRATION -13.6

#endif

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
#include <GPS_MTK.h>      // MediaTEK DIY Drones GPS. 
//#include <GPS_UBLOX.h>  // uBlox GPS
//#include <GPS_NMEA.h>   // General NMEA GPS
#include <EEPROM.h>       // EEPROM storage for user configurable values
#include "ArduCopter.h"
#include "UserConfig.h"

#ifdef UseBMP
  #include <APM_BMP085.h>
#endif

/* Software version */
#define VER 1.5    // Current software version (only numeric values)


/* ***************************************************************************** */
/* ************************ CONFIGURATION PART ********************************* */
/* ***************************************************************************** */

// Normal users does not need to edit anything below these lines. If you have
// need, go and change them in UserConfig.h

/* ***************************************************************************** */
// STABLE MODE
// PI absolute angle control driving a P rate control
// Input : desired Roll, Pitch and Yaw absolute angles. Output : Motor commands
void Attitude_control_v3()
{
  #define MAX_CONTROL_OUTPUT 250
  float stable_roll,stable_pitch,stable_yaw;
  
  // ROLL CONTROL    
  if (AP_mode==2)        // Normal Mode => Stabilization mode
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
  if (AP_mode==2)        // Normal mode => Stabilization mode
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
  currentRollRate = read_adc(0);      // I need a positive sign here

  err_roll = ((ch_roll - roll_mid) * xmitFactor) - currentRollRate;

  roll_I += err_roll * G_Dt;
  roll_I = constrain(roll_I, -20, 20);

  roll_D = currentRollRate - previousRollRate;
  previousRollRate = currentRollRate;

  // PID control
  control_roll = Kp_RateRoll * err_roll + Kd_RateRoll * roll_D + Ki_RateRoll * roll_I; 

  // PITCH CONTROL
  currentPitchRate = read_adc(1);
  err_pitch = ((ch_pitch - pitch_mid) * xmitFactor) - currentPitchRate;

  pitch_I += err_pitch*G_Dt;
  pitch_I = constrain(pitch_I,-20,20);

  pitch_D = currentPitchRate - previousPitchRate;
  previousPitchRate = currentPitchRate;

  // PID control
  control_pitch = Kp_RatePitch*err_pitch + Kd_RatePitch*pitch_D + Ki_RatePitch*pitch_I; 

  // YAW CONTROL
  currentYawRate = read_adc(2);
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

// BMP slope filter for readings... (limit max differences between readings)
float BMP_filter(float BMP_reading, float BMP_reading_old)
{
  float diff_BMP_reading_old;

  if (BMP_reading_old == 0)      // BMP_reading_old not initialized
    return(BMP_reading);
    diff_BMP_reading_old = BMP_reading - BMP_reading_old;      // Difference with old reading
  if (diff_BMP_reading_old < 0)
  {
    if (diff_BMP_reading_old <- 5)
      return(BMP_reading_old - 5);        // We limit the max difference between readings
  }
  else
  {
    if (diff_BMP_reading_old > 5)    
      return(BMP_reading_old + 5);
  }
  return((BMP_reading + BMP_reading_old ) / 2);   // Small filtering
} 
/* ************************************************************ */
/* **************** MAIN PROGRAM - SETUP ********************** */
/* ************************************************************ */
void setup()
{
  int i, j;
  float aux_float[3];

  pinMode(LED_Yellow,OUTPUT); //Yellow LED A  (PC1)
  pinMode(LED_Red,OUTPUT);    //Red LED B     (PC2)
  pinMode(LED_Green,OUTPUT);  //Green LED C   (PC0)

  pinMode(SW1_pin,INPUT);     //Switch SW1 (pin PG0)

  pinMode(RELE_pin,OUTPUT);   // Rele output
  digitalWrite(RELE_pin,LOW);
  
#ifdef BATTERY_EVENT
  pinMode(RELAY_PIN, OUTPUT); // Battery Alarm output
  digitalWrite(47, LOW);      // Silence Alarm
#endif

  APM_RC.Init();             // APM Radio initialization
  // RC channels Initialization (Quad motors)  
  APM_RC.OutputCh(0,MIN_THROTTLE);  // Motors stoped
  APM_RC.OutputCh(1,MIN_THROTTLE);
  APM_RC.OutputCh(2,MIN_THROTTLE);
  APM_RC.OutputCh(3,MIN_THROTTLE);

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

  APM_ADC.Init();            // APM ADC library initialization
  DataFlash.Init();          // DataFlash log initialization

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

  j = 0;
  // Take the gyro offset values
  for(i=0;i<300;i++)
  {
    Read_adc_raw();
    for(int y=0; y<=2; y++)   // Read initial ADC values for gyro offset.
    {
      aux_float[y]=aux_float[y]*0.8 + AN[y]*0.2;
    }

    delay(10);
    
    // Runnings lights effect to let user know that we are taking mesurements
    if(j == 0) {
      digitalWrite(LED_Green, HIGH);
      digitalWrite(LED_Yellow, LOW);
      digitalWrite(LED_Red, LOW);
    } 
    else if (j == 1) {
      digitalWrite(LED_Green, LOW);
      digitalWrite(LED_Yellow, HIGH);
      digitalWrite(LED_Red, LOW);
    } 
    else {
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

  for(int y=0; y<=2; y++)   
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
void loop(){

  int aux;
  float aux_float;
  
/*  //BMP varaibles
  int runs;
  int BMP_alt_buffer[10];
  int BMP_buffercounter;
  int BMP_alt_tmp;  */

  //Log variables
  int log_roll;
  int log_pitch;
  int log_yaw;

  if((millis()-timer)>=5)   // Main loop 200Hz
  {
    Magneto_counter++;
    BMP_counter++;
//    BMP_buffercounter++;
    GPS_counter++;
    timer_old = timer;
    timer=millis();
    G_Dt = (timer-timer_old)*0.001;      // Real time of loop run 

#ifdef BATTERY_EVENT
    //Battery Moniter
    read_battery();
#endif

    // IMU DCM Algorithm
    Read_adc_raw();
    
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

#ifdef UseBMP
      if (BMP_counter > 10)  // Reading Barometric data at 20Hz 
      {
        BMP_counter = 0;
        APM_BMP085.Read();
        BMP_Altitude = BMP_filter(APM_BMP085.Press, BMP_Altitude);  // New slope filter engaged

// Former translation from pressure&temperature into cm        
//***********************************************************************************
//        tempPresAlt = float(APM_BMP085.Press)/101325.0;
//        tempPresAlt = pow(tempPresAlt, 0.190295);
//        BMP_Altitude = (1.0 - tempPresAlt) * 4433000;      // Altitude in cm
//***********************************************************************************

// Idea with median of 10 values (to be checked...)
/*************************************************************************************
        runs = 10;
        if (BMP_buffercounter < runs)  //reading x runs values and making median
        {
          BMP_alt_buffer[BMP_buffercounter] = APM_BMP085.Press;
          BMP_alt_tmp = BMP_alt_buffer[BMP_buffercounter] + BMP_alt_tmp;
        }
        else
        {
          BMP_Altitude = BMP_alt_tmp / runs;  //calculating median of x runs
          BMP_buffercounter = 0;
          BMP_alt_tmp = 0;
        }   
*************************************************************************************/
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
 
    if (APM_RC.GetState() == 1)   // New radio frame?
    {
      // Commands from radio Rx... 
      // Stick position defines the desired angle in roll, pitch and yaw
      ch_roll = channel_filter(APM_RC.InputCh(0) * ch_roll_slope + ch_roll_offset, ch_roll);
      ch_pitch = channel_filter(APM_RC.InputCh(1) * ch_pitch_slope + ch_pitch_offset, ch_pitch);
      ch_throttle = channel_filter(APM_RC.InputCh(2), ch_throttle); // Transmiter calibration not used on throttle
      ch_yaw = channel_filter(APM_RC.InputCh(3) * ch_yaw_slope + ch_yaw_offset, ch_yaw);
      ch_aux = APM_RC.InputCh(4);
      ch_aux2 = APM_RC.InputCh(5);
      ch_mode = APM_RC.InputCh(6);
   
      command_throttle = (ch_throttle-throttle_mid) / 12; 
      command_rx_roll = (ch_roll-roll_mid) / 12.0;
      command_rx_pitch = (ch_pitch-pitch_mid) / 12.0;

#ifdef UseBMP  
        // New Altitude Hold using BMP Pressure sensor.  If Trottle stick moves more then 10%, switch Altitude Hold off    
        if (AP_mode == 2 || AP_mode == 3) 
        {
          if(command_throttle >= 15 || command_throttle <= -15 || ch_throttle <= 1200)
          {
            BMP_mode = 0; //Altitude hold is switched off because of stick movement 
            BMP_altitude_I = 0;
            BMP_altitude_D = 0;
            BMP_err_altitude_old = 0;
            BMP_err_altitude = 0;
            BMP_command_altitude = 0;
            target_alt_position = 0;  //target altitude reset
          } 
          else 
          {
            BMP_mode = 1;  //Altitude hold is swithed on.
          }
        } 
#endif
  
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
//     We read the Quad Mode from Channel 5 & 6
//     AP_mode = 0;          // Acrobatic mode
//     AP_mode = 2;          // SuperStable Mode (Altitude Hold and Heading Hold if no throttle stick movement)
//     AP_mode = 1;          // Position hold mode (GPS position control)
//     AP_mode = 3;          // Position hold mode and Altitude Hold
      
      if (ch_aux2 < 1250 && ch_aux > 1800)
      {
        AP_mode = 2;          // Stable mode & Altitude hold mode (Stabilization assist mode)
        digitalWrite(LED_Yellow,LOW); // Yellow LED off
      }
      else if (ch_aux < 1250 && ch_aux2 > 1800)
      {
        AP_mode = 3;           // Position & Altitude hold mode (GPS position control & Altitude control)
        digitalWrite(LED_Yellow,HIGH); // Yellow LED On
      }
      else if (ch_aux < 1250 && ch_aux2 < 1250)
      {
        AP_mode = 1;           // Position hold mode (GPS position control)
        digitalWrite(LED_Yellow,HIGH); // Yellow LED On
      }
      else 
      {
        AP_mode = 0;          // Acrobatic mode
        digitalWrite(LED_Yellow,LOW); // Yellow LED off
      }     // End reading Quad Mode from Channel 5 & 6
    }  // End reading new radio frame
	  
   if (AP_mode==3)  // Position & Altitude Hold Mode
    {
      heading_hold_mode = 1;
      if (target_position == 0)   // If this is the first time we switch to Position control, actual position is our target position
      {
        target_lattitude = GPS.Lattitude;
        target_longitude = GPS.Longitude;
        if (target_alt_position == 0)
        {
          BMP_target_altitude = BMP_Altitude;  //first time setting current altitude is target altitude
          if (BMP_mode == 0)  // If Altitude hold has been disabled reset variables
          {
            BMP_altitude_I = 0;
            BMP_altitude_D = 0;
            BMP_err_altitude_old = 0;
            BMP_err_altitude = 0;
            BMP_command_altitude = 0;
          } 
          target_alt_position = 1;  //target altitude has been set
        }  
        target_position = 1;  //target position has been set
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
    else if (AP_mode==1)  // Position Control (just GPS without altitude)
    {
      heading_hold_mode = 1;
      target_alt_position = 0;
      if (target_position == 0)   // If this is the first time we switch to Position control, actual position is our target position
      {
        target_lattitude = GPS.Lattitude;
        target_longitude = GPS.Longitude;
        target_position = 1;
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
      BMP_mode = 0;   // Altitude hold mode disabled followed by reset of variables
      BMP_altitude_I = 0;
      BMP_altitude_D = 0;
      BMP_err_altitude_old = 0;
      BMP_err_altitude = 0;
      BMP_command_altitude = 0;
    }
    else if (AP_mode==2)  // SuperStable Mode (Altitude Hold and Heading Hold) (if no stick movement)
    {
      if (target_alt_position == 0)   // If this is the first time we switch to Position control, actual position is our target position
      {
        BMP_target_altitude = BMP_Altitude;  //first time setting current altitude is target altitude
        if (BMP_mode == 0)    // If Altitude hold mode has been disabled reset variables
        {
          BMP_altitude_I = 0;
          BMP_altitude_D = 0;
          BMP_err_altitude_old = 0;
          BMP_err_altitude = 0;
          BMP_command_altitude = 0;
        } 
        target_alt_position = 1;  //target altitude has been set
      }
      heading_hold_mode = 1;
      target_position = 0;
    }
    else if (AP_mode == 0)  //Acrobatic Mode
    {
      BMP_altitude_I = 0;
      BMP_altitude_D = 0;
      BMP_err_altitude_old = 0;
      BMP_err_altitude = 0;
      BMP_command_altitude = 0;
      BMP_mode = 0;   //Altitude hold mode has been disabled
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

      if (AP_mode == 1 || AP_mode ==3)
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

    if (AP_mode == 1 || AP_mode == 2 || AP_mode == 3) 
    {
      gled_speed = 1200;
      Attitude_control_v3();
      if (BMP_mode == 1)
        BMP_Altitude_control(BMP_target_altitude);
     }
    else
    {
      gled_speed = 400;
      Rate_control_v2();
      // Reset yaw, so if we change to stable mode we continue with the actual yaw direction
      command_rx_yaw = ToDeg(yaw);
    }
    
    // Arm motor output : Throttle down and full yaw right for more than 2 seconds
    if (ch_throttle < (MIN_THROTTLE + 100)) {
      control_yaw = 0;
      command_rx_yaw = ToDeg(yaw);
      if (ch_yaw > 1850) {
        if (Arming_counter > ARM_DELAY){
          if(ch_throttle > 800) 
          {
            motorArmed = 1;
            minThrottle = MIN_THROTTLE + 60;  // A minimun value for mantain a bit if throttle
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
          minThrottle = MIN_THROTTLE;
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

    // Quadcopter mix
    if (motorArmed == 1) {   
#ifdef IsAM
      digitalWrite(FR_LED, HIGH);    // AM-Mode
#endif
#ifdef FLIGHT_MODE_+
      if (BMP_mode == 1){
        rightMotor = constrain(ch_throttle + BMP_command_altitude - control_roll + control_yaw, minThrottle, 2000);
        leftMotor = constrain(ch_throttle + BMP_command_altitude + control_roll + control_yaw, minThrottle, 2000);
        frontMotor = constrain(ch_throttle + BMP_command_altitude + control_pitch - control_yaw, minThrottle, 2000);
        backMotor = constrain(ch_throttle + BMP_command_altitude - control_pitch - control_yaw, minThrottle, 2000);
      } else {
        rightMotor = constrain(ch_throttle - control_roll + control_yaw, minThrottle, 2000);
        leftMotor = constrain(ch_throttle + control_roll + control_yaw, minThrottle, 2000);
        frontMotor = constrain(ch_throttle + control_pitch - control_yaw, minThrottle, 2000);
        backMotor = constrain(ch_throttle - control_pitch - control_yaw, minThrottle, 2000);
      }
#endif
#ifdef FLIGHT_MODE_X
      if (BMP_mode == 1){
        rightMotor = constrain(ch_throttle + BMP_command_altitude - control_roll + control_pitch + control_yaw, minThrottle, 2000); // front right motor
        leftMotor = constrain(ch_throttle + BMP_command_altitude + control_roll - control_pitch + control_yaw, minThrottle, 2000);  // rear left motor
        frontMotor = constrain(ch_throttle + BMP_command_altitude + control_roll + control_pitch - control_yaw, minThrottle, 2000); // front left motor
        backMotor = constrain(ch_throttle + BMP_command_altitude - control_roll - control_pitch - control_yaw, minThrottle, 2000);  // rear right motor
      } else {
        rightMotor = constrain(ch_throttle - control_roll + control_pitch + control_yaw, minThrottle, 2000); // front right motor
        leftMotor = constrain(ch_throttle + control_roll - control_pitch + control_yaw, minThrottle, 2000);  // rear left motor
        frontMotor = constrain(ch_throttle + control_roll + control_pitch - control_yaw, minThrottle, 2000); // front left motor
        backMotor = constrain(ch_throttle - control_roll - control_pitch - control_yaw, minThrottle, 2000);  // rear right motor
      }  
#endif
    }
    if (motorArmed == 0) {
      
#ifdef IsAM
        digitalWrite(FR_LED, LOW);    // AM-Mode
#endif
    
      digitalWrite(LED_Green,HIGH); // Ready LED on

      rightMotor = MIN_THROTTLE;
      leftMotor = MIN_THROTTLE;
      frontMotor = MIN_THROTTLE;
      backMotor = MIN_THROTTLE;
      roll_I = 0;     // reset I terms of PID controls
      pitch_I = 0;
      yaw_I = 0; 
      // Initialize yaw command to actual yaw when throttle is down...
      command_rx_yaw = ToDeg(yaw);
      BMP_mode = 1;   // in general we reinitialize altitude hold as "on"
    }
    
    APM_RC.OutputCh(0, rightMotor);   // Right motor
    APM_RC.OutputCh(1, leftMotor);    // Left motor
    APM_RC.OutputCh(2, frontMotor);   // Front motor
    APM_RC.OutputCh(3, backMotor);    // Back motor   
  
    // Camera Stabilization
//    APM_RC.OutputCh(4, APM_RC.InputCh(6)+(pitch)*1000); // Tilt correction 
//    APM_RC.OutputCh(5, 1510+(roll)*-400);               // Roll correction
   
     // InstantPWM
    APM_RC.Force_Out0_Out1();
    APM_RC.Force_Out2_Out3();

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
} // End of void loop()

// END of Arducopter.pde



