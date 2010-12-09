/* ********************************************************************** */
/*               ArduCopter & ArduPirates Quadcopter code                 */
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
/* Date : 08-08-2010                                                      */
/* Version : 1.7 beta                                                     */
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

*/

/* User definable modules */

// Comment out with // modules that you are not using
#define IsGPS    // Do we have a GPS connected
#define IsNEWMTEK// Do we have MTEK with new firmware
#define IsMAG    // Do we have a Magnetometer connected, if have remember to activate it from Configurator
#define IsXBEE    // Do we have a telemetry connected, eg. XBee connected on Telemetry port
//#define IsAM     // Do we have motormount LED's. AM = Attraction Mode
//#define IsSonar  // Do we have Sonar installed // //XL-Maxsonar EZ4 - Product 9495 from SPF.  I use Analgue output.
//#define IsIR_RF  // Do we have IR Range Finders

#define CONFIGURATOR  // Do se use Configurator or normal text output over serial link

/**********************************************/
// Not in use yet, starting to work with battery monitors and pressure sensors. 
// Added 19-08-2010

//#define UseAirspeed
#define UseBMP
//#define BATTERY_EVENT 1   // (boolean) 0 = don't read battery, 1 = read battery voltage (only if you have it wired up!)

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
//Hexa Diamond Mode - 6 Motor system in diamond shape

//      L  CCW 0.Front.0 CW  R           // 0 = Motor
//         ......***......               // *** = APM 
//   L  CW 0.....***......0 CCW  R       // ***
//         ......***......               // *** 
//     B  CCW  0.Back..0  CW  B          L = Left motors, R = Right motors, B = Back motors.


/**********************************************/

//  Magnetometer Setup

#ifdef IsMAG
// DIYDrones Magnetometer
//#define MAGORIENTATION  APM_COMPASS_COMPONENTS_UP_PINS_FORWARD       // This is default solution for ArduCopter
//#define MAGORIENTATION  APM_COMPASS_COMPONENTS_UP_PINS_BACK        // Alternative orientation for ArduCopter
//#define MAGORIENTATION  APM_COMPASS_COMPONENTS_DOWN_PINS_FORWARD    // If you have soldered Magneto to IMU shield in WIki pictures shows
// or 
// Sparkfun Magnetometer
//#define MAGORIENTATION  APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_FORWARD // Sparkfun Magnetometer orientation.
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_BACK
#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD

// To get Magneto offsets, switch to CLI mode and run offset calibration. During calibration
// you need to roll/bank/tilt/yaw/shake etc your ArduCoptet. Don't kick like Jani always does :)
#define MAGOFFSET -76,22.5,-55.5  // Hein's calibration settings.  You have to determine your own.

// Declination is a correction factor between North Pole and real magnetic North. This is different on every location
// IF you want to use really accurate headholding and future navigation features, you should update this
// You can check Declination to your location from http://www.magnetic-declination.com/
#define DECLINATION -17.65      // Hein, South Africa, Centurion.

// And remember result from NOAA website is in form of DEGREES°MINUTES'. Degrees you can use directly but Minutes you need to 
// recalculate due they one degree is 60 minutes.. For example Jani's real declination is 0.61, correct way to calculate this is
// 37 / 60 = 0.61 and for Helsinki it would be 7°44' eg 7. and then 44/60 = 0.73 so declination for Helsinki/South Finland would be 7.73

// East values are positive
// West values are negative

// Some of devel team's Declinations and their Cities
//#define DECLINATION 0.61      // Jani, Bangkok, 0°37' E  (Due I live almost at Equator, my Declination is rather small)
//#define DECLINATION 7.73      // Jani, Helsinki,7°44' E  (My "summer" home back at Finland)
//#define DECLINATION -20.68    // Sandro, Belo Horizonte, 22°08' W  (Whoah... Sandro is really DECLINED)
//#define DECLINATION 7.03      // Randy, Tokyo, 7°02'E
//#define DECLINATION 8.91      // Doug, Denver, 8°55'E
//#define DECLINATION -6.08     // Jose, Canary Islands, 6°5'W
//#define DECLINATION 0.73      // Tony, Minneapolis, 0°44'E
//#define DECLINATION -17.65      // Hein, South Africa, Centurion.

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
#ifdef UseBMP
#include <APM_BMP085.h>
#endif

//#include <GPS_NMEA.h>   // General NMEA GPS 
#include <GPS_MTK.h>      // MediaTEK DIY Drones GPS. 
//#include <GPS_UBLOX.h>  // uBlox GPS

// EEPROM storage for user configurable values
#include <EEPROM.h>
#include "ArduCopter.h"
#include "UserConfig.h"

/* Software version */
#define VER 2.1    // Current software version (only numeric values)


/* ***************************************************************************** */
/* ************************ CONFIGURATION PART ********************************* */
/* ***************************************************************************** */

// Normal users does not need to edit anything below these lines. If you have
// need, go and change them in UserConfig.h

/* ************************************************************ */
// STABLE MODE
// PI absolute angle control driving a P rate control
// Input : desired Roll, Pitch and Yaw absolute angles. Output : Motor commands
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
  
  if (command_rx_yaw > 0 || command_rx_yaw < 0)
    yaw_I = 0;  
  
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
  
  APM_RC.Init();             // APM Radio initialization
 
 #ifdef Quad
  // RC channels Initialization (Quad motors)  
  APM_RC.OutputCh(0,MIN_THROTTLE);  // Motors stoped
  APM_RC.OutputCh(1,MIN_THROTTLE);
  APM_RC.OutputCh(2,MIN_THROTTLE);
  APM_RC.OutputCh(3,MIN_THROTTLE);
#endif

#ifdef Hexa
  // RC channels Initialization (Hexa motors) - Motors stoped 
  APM_RC.OutputCh(0,MIN_THROTTLE);     // Left Motor CW
  APM_RC.OutputCh(1, MIN_THROTTLE);    // Left Motor CCW
  APM_RC.OutputCh(2, MIN_THROTTLE);    // Right Motor CW
  APM_RC.OutputCh(3, MIN_THROTTLE);    // Right Motor CCW    
  APM_RC.OutputCh(6, MIN_THROTTLE);    // Back Motor CW
  APM_RC.OutputCh(7, MIN_THROTTLE);    // Back Motor CCW    
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

#ifdef IsMAG
  if (MAGNETOMETER == 1) 
  {
    APM_Compass.Init();  // I2C initialization
    APM_Compass.SetOrientation(MAGORIENTATION);
    APM_Compass.SetOffsets(MAGOFFSET);
    APM_Compass.SetDeclination(ToRad(DECLINATION));
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
//    Log_Write_Sensor(AN[0],AN[1],AN[2],AN[3],AN[4],AN[5],ch_throttle);
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
  digitalWrite(LED_Green,HIGH);     // Ready to go...
}


/* ************************************************************ */
/* ************** MAIN PROGRAM - MAIN LOOP ******************** */
/* ************************************************************ */
void loop(){

  int aux;
  int i;
  float aux_float;

  //Log variables
  int log_roll;
  int log_pitch;
  int log_yaw;

  if((millis()-timer)>=5)   // Main loop 200Hz
  {
    Magneto_counter++;
    GPS_counter++;
    RF_Counter++;
    timer_old = timer;
    timer=millis();
    G_Dt = (timer-timer_old)*0.001;      // Real time of loop run 

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
    BMP_counter++;
    if (BMP_counter > 10)  // Reading Barometric data at 20Hz
    {
      APM_BMP085.Read();
      BMP_counter = 0;
      press_alt = BMP_Sensor_Filter(APM_BMP085.Press, press_alt, 15);  // Filter Barometric readings.
      Baro_new_data=1;
    }
#endif

    Matrix_update(); 
    Normalize();
    Drift_correction();
    Euler_angles();

    // *****************
    // Output data
    log_roll = ToDeg(roll) * 10;
    log_pitch = ToDeg(pitch) * 10;
    log_yaw = ToDeg(yaw) * 10;

#ifndef CONFIGURATOR    
    SerPri(log_roll);
    SerPri(",");
    SerPri(log_pitch);
    SerPri(",");
    SerPri(log_yaw);
#endif

    // Write Sensor raw data to DataFlash log
//    Log_Write_Sensor(AN[0],AN[1],AN[2],AN[3],AN[4],AN[5],gyro_temp);
    // Write attitude to DataFlash log
//    Log_Write_Attitude(log_roll,log_pitch,log_yaw);  

#ifdef IsSonar
    sonar_read = APM_ADC.Ch(7);   // Sonar is connected to pitot input in shield (Infront of shield, marked pitot between led's)
                                  // At the bottom of shield is written gnd +5V IN.  We use the IN....
                                  //XL-Maxsonar EZ4 - Product 9495 from SPF.  I use Analgue output. (pin 3)  Will still consider PW..pin2
    if (sonar_read > 2000)        // For testing purposes I am monitoring sonar_read value.
      Use_BMP_Altitude = 1; 
    else  
    {  
      sonar_adc += sonar_read;      
      Sonar_Counter++;
      Use_BMP_Altitude = 0;
    }
#endif
    
#ifdef IsIR_RF
    IR_adc_fl += analogRead(IR_Front_Left);
    IR_adc_fr += analogRead(IR_Front_Right);    
    IR_adc_br += analogRead(IR_Back_Right);    
    IR_adc_bl += analogRead(IR_Back_Left);
#endif

#ifdef IsSonar
  if (Sonar_Counter > 10)   // New sonar data at 20Hz
      {
      sonar_adc = sonar_adc/Sonar_Counter;  // Average sensor readings (to filter noise)
      Sonar_value = Sensor_Filter(SonarToCm(sonar_adc),Sonar_value,4);
      sonar_adc=0;
      Sonar_Counter=0;
      Sonar_new_data=1;  // New sonar data flag
      }
#endif
#ifdef IsIR_RF
  if (RF_Counter > 10)
      {
      IR_adc_fl = IR_adc_fl/RF_Counter;   // Average sensor readings (to filter noise)
      IR_adc_fr = IR_adc_fr/RF_Counter;
      IR_adc_br = IR_adc_br/RF_Counter;
      IR_adc_bl = IR_adc_bl/RF_Counter;
      RF_Counter=0;
      Read_IR_Sensors();
      RF_new_data=1;      // New range finder data flag
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

      #define STICK_TO_ANGLE_FACTOR 12.0
   
      command_throttle = (ch_throttle - Hover_Throttle_Position) / STICK_TO_ANGLE_FACTOR; 
      command_rx_roll = (ch_roll-roll_mid) / STICK_TO_ANGLE_FACTOR;
      command_rx_pitch = (ch_pitch-pitch_mid) / STICK_TO_ANGLE_FACTOR;


#ifdef UseBMP || IsSonar
  
      // New Altitude Hold using BMP Pressure sensor.  If Throttle stick moves more then 10%, switch Altitude Hold off    
      if (AP_mode == F_MODE_ABS_HOLD || AP_mode == F_MODE_SUPER_STABLE) 
      {
        if(command_throttle >= 12 || command_throttle <= -12 || ch_throttle <= 1200) 
        {
          Throttle_Altitude_Change_mode = 1; //Throttle Applied in Altitude hold is switched on.  Changing Altitude. 
          target_alt_position = 0;
        } 
        else 
        {
          Throttle_Altitude_Change_mode = 0;  //No more Throttle Applied in Altitude hold is swithed off.  Lock Altitude again.
        }
      } 
#endif

/*
     // Tuning P of PID using only 3 position channel switch (Flight Mode).
     if (ch_aux1 >= 1800) 
     {
       Plus = 1;
       Minus = 0;
     } 
     else if (ch_aux1 <= 1200) 
     {
          Plus = 0;
          Minus = 1;
     } 
     else if (ch_aux1 >= 1400 && ch_aux1 <= 1600) 
     {
             if (Plus == 1){
//                KP_GPS_ROLL += 0.001;
//                writeEEPROM(KP_GPS_ROLL, KP_GPS_ROLL_ADR);
//                KP_GPS_PITCH += 0.001;
//                writeEEPROM(KP_GPS_PITCH, KP_GPS_PITCH_ADR);
//                KI_GPS_ROLL += 0.0001;
//                writeEEPROM(KI_GPS_ROLL, KI_GPS_ROLL_ADR);
//                KI_GPS_PITCH += 0.0001;
//                writeEEPROM(KI_GPS_PITCH, KI_GPS_PITCH_ADR);
//                KP_QUAD_YAW += 0.1;
//                writeEEPROM(KP_QUAD_YAW, KP_QUAD_YAW_ADR);
//                KI_QUAD_YAW += 0.01;
//                writeEEPROM(KI_QUAD_YAW, KI_QUAD_YAW_ADR);
                STABLE_MODE_KP_RATE += 0.05;
                writeEEPROM(STABLE_MODE_KP_RATE, STABLE_MODE_KP_RATE_ADR);
//                STABLE_MODE_KP_RATE_YAW += 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_YAW, STABLE_MODE_KP_RATE_YAW_ADR);
//                STABLE_MODE_KP_RATE_ROLL += 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_ROLL, STABLE_MODE_KP_RATE_ROLL_ADR);
//                STABLE_MODE_KP_RATE_PITCH += 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_PITCH, STABLE_MODE_KP_RATE_PITCH_ADR);
//                Kp_RateRoll += 0.1;
//                writeEEPROM(Kp_RateRoll, KP_RATEROLL_ADR);
//                Kp_RatePitch += 0.1;
//                writeEEPROM(Kp_RatePitch, KP_RATEPITCH_ADR);
//                KP_ALTITUDE += 0.1;
//                writeEEPROM(KP_ALTITUDE, KP_ALTITUDE_ADR);
//                KI_ALTITUDE += 0.3;
//                writeEEPROM(KI_ALTITUDE, KI_ALTITUDE_ADR);
//                KD_ALTITUDE += 0.3;
//                writeEEPROM(KD_ALTITUDE, KD_ALTITUDE_ADR);
//                Magoffset1 += 1;
//                writeEEPROM(Magoffset1_ADR);
//                APM_Compass.SetOffsets(Magoffset1);    

                Plus = 0;
                Minus = 0;
             } else if (Minus == 1) {
//                KP_GPS_ROLL -= 0.001;
//                writeEEPROM(KP_GPS_ROLL, KP_GPS_ROLL_ADR);
//                KP_GPS_PITCH -= 0.001;
//                writeEEPROM(KP_GPS_PITCH, KP_GPS_PITCH_ADR);
//                KI_GPS_ROLL -= 0.0001;
//                writeEEPROM(KI_GPS_ROLL, KI_GPS_ROLL_ADR);
//                KI_GPS_PITCH -= 0.0001;
//                writeEEPROM(KI_GPS_PITCH, KI_GPS_PITCH_ADR);
//                KP_QUAD_YAW -= 0.1;
//                writeEEPROM(KP_QUAD_YAW, KP_QUAD_YAW_ADR);
//                KI_QUAD_YAW -= 0.01;
//                writeEEPROM(KI_QUAD_YAW, KI_QUAD_YAW_ADR);
                STABLE_MODE_KP_RATE -= 0.05;
                writeEEPROM(STABLE_MODE_KP_RATE, STABLE_MODE_KP_RATE_ADR);
//                STABLE_MODE_KP_RATE_YAW -= 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_YAW, STABLE_MODE_KP_RATE_YAW_ADR);
//                STABLE_MODE_KP_RATE_ROLL -= 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_ROLL, STABLE_MODE_KP_RATE_ROLL_ADR);
//                STABLE_MODE_KP_RATE_PITCH -= 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_PITCH, STABLE_MODE_KP_RATE_PITCH_ADR);
//                Kp_RateRoll -= 0.1;
//                writeEEPROM(Kp_RateRoll, KP_RATEROLL_ADR);
//                Kp_RatePitch -= 0.1;
//                writeEEPROM(Kp_RatePitch, KP_RATEPITCH_ADR);
//                KP_ALTITUDE -= 0.1;
//                writeEEPROM(KP_ALTITUDE, KP_ALTITUDE_ADR);
//                KI_ALTITUDE -= 0.3;
//                writeEEPROM(KI_ALTITUDE, KI_ALTITUDE_ADR);
//                KD_ALTITUDE -= 0.3;
//                writeEEPROM(KD_ALTITUDE, KD_ALTITUDE_ADR);
//                Magoffset1 -= 1;
//                writeEEPROM(Magoffset1_ADR);
//                APM_Compass.SetOffsets(Magoffset1);    

                Plus = 0;
                Minus = 0;
             }
     }

*/
      if (abs(ch_yaw-yaw_mid)<12)   // Take into account a bit of "dead zone" on yaw
        aux_float = 0.0;
      else
        aux_float = (ch_yaw-yaw_mid) / 180.0;
      command_rx_yaw += aux_float;
      if (command_rx_yaw > 180)         // Normalize yaw to -180,180 degrees
        command_rx_yaw -= 360.0;
      else if (command_rx_yaw < -180)
        command_rx_yaw += 360.0;
  
//     We read the Quad Mode from Gear and Aux2 Channel on radio (example Spektrum Radio)

//     AUX2 OFF && GEAR OFF = Acrobatic mode
//     AUX2 ON  && GEAR OFF = Stable Mode (Heading Hold only)
//     AUX2 ON  && GEAR ON  = SuperStable Mode (Altitude Hold and Heading Hold if no throttle stick movement)
//     AUX2 OFF && GEAR ON  = Position hold mode and Altitude Hold  
      
      if (ch_aux2 < 1250 && ch_gear > 1800)
      {
        AP_mode = F_MODE_STABLE  ;      // Stable mode (Heading Hold only)
        digitalWrite(LED_Yellow,LOW); // Yellow LED off
      }
      else if (ch_gear < 1250 && ch_aux2 > 1800)
      {
        AP_mode = F_MODE_ABS_HOLD;      // Position & Altitude hold mode (GPS position control & Altitude control)
        digitalWrite(LED_Yellow,HIGH); // Yellow LED On
      }
      else if (ch_gear < 1250 && ch_aux2 < 1250)
      {
        AP_mode = F_MODE_SUPER_STABLE;  // Super Stable Mode (Stable mode & Altitude hold mode)
        digitalWrite(LED_Yellow,LOW); // Yellow LED off
      }
      else 
      {
        AP_mode = F_MODE_ACROBATIC;     // Acrobatic mode
        digitalWrite(LED_Yellow,LOW); // Yellow LED off
      }
      // Write Radio data to DataFlash log
//        Log_Write_Radio(ch_roll,ch_pitch,ch_throttle,ch_yaw,int(K_aux*100),(int)AP_mode);
    }  // END new radio data


   if (AP_mode==F_MODE_ABS_HOLD)  // Position & Altitude Hold Mode
   {
      heading_hold_mode = 1;
      if (target_position == 0)   // If this is the first time we switch to Position control, actual position is our target position
      {
        target_lattitude = GPS.Lattitude;
        target_longitude = GPS.Longitude;
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
        if (Hover_Throttle_Position_mode == 0)
        {
          Hover_Throttle_Position = ch_throttle;
          Hover_Throttle_Position_mode = 1;
        }  
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
        if (Hover_Throttle_Position_mode == 0)
        {
          Hover_Throttle_Position = ch_throttle;
          Hover_Throttle_Position_mode = 1;
        }  
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
      Hover_Throttle_Position_mode = 0;      
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
      Hover_Throttle_Position_mode = 0;      
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
      GPS.NewData=0;  // We Reset the flag...


      // Write GPS data to DataFlash log
//      Log_Write_GPS(GPS.Time, GPS.Lattitude,GPS.Longitude,GPS.Altitude, GPS.Ground_Speed, GPS.Ground_Course, GPS.Fix, GPS.NumSats);

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
    
    if ((AP_mode == F_MODE_ABS_HOLD || AP_mode == F_MODE_SUPER_STABLE) && Throttle_Altitude_Change_mode == 0)  // Altitude control
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
        ch_throttle_altitude_hold = Altitude_control_baro(press_alt,target_baro_altitude);
        Baro_new_data=0;
      }
#endif
#ifdef IsIR_RF
      if (RF_new_data)  // Range Finder Obstacle avoidance (Position control)  
      {
        RF_Obstacle_avoidance(RF_Sensor1,RF_Sensor2,RF_Sensor3,RF_Sensor4);  // Call Range Finder position control routine
        RF_new_data=0;
        //Serial.print(command_RF_roll);
        //Serial.print(",");
        //Serial.print(command_RF_pitch);
      }
#endif
    }
      
    // Control methodology selected using AUX2
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
    if ((AP_mode == F_MODE_ABS_HOLD || AP_mode == F_MODE_SUPER_STABLE) && Use_BMP_Altitude == 0 && Throttle_Altitude_Change_mode == 0)  // Altitude control
      ch_throttle = ch_throttle_altitude_hold;
#endif

#ifdef UseBMP
    if ((AP_mode == F_MODE_ABS_HOLD || AP_mode == F_MODE_SUPER_STABLE) && Use_BMP_Altitude == 1 && Throttle_Altitude_Change_mode == 0)  // Altitude control
      ch_throttle = (ch_throttle_altitude_hold + 20);
#endif

    // Arm motor output : Throttle down and full yaw right for more than 2 seconds
    if (ch_throttle < (MIN_THROTTLE + 100)) {
      control_yaw = 0;
      command_rx_yaw = ToDeg(yaw);
      if (ch_yaw > 1850) {
        if (Arming_counter > ARM_DELAY){
          if(ch_throttle > 800) 
          {
            motorArmed = 1;
            minThrottle = MIN_THROTTLE+60;  // A minimun value for mantain a bit of throttle
//            motorArmed = 0;
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

#ifdef Quad
   // Quadcopter mix
#ifdef FLIGHT_MODE_+
        rightMotor = constrain(ch_throttle - control_roll + control_yaw, minThrottle, 2000);
        leftMotor = constrain(ch_throttle + control_roll + control_yaw, minThrottle, 2000);
        frontMotor = constrain(ch_throttle + control_pitch - control_yaw, minThrottle, 2000);
        backMotor = constrain(ch_throttle - control_pitch - control_yaw, minThrottle, 2000);
#endif
#ifdef FLIGHT_MODE_X
        rightMotor = constrain(ch_throttle - control_roll + control_pitch + control_yaw, minThrottle, 2000); // Right motor
        leftMotor = constrain(ch_throttle + control_roll - control_pitch + control_yaw, minThrottle, 2000);  // Left motor
        frontMotor = constrain(ch_throttle + control_roll + control_pitch - control_yaw, minThrottle, 2000); // Front motor
        backMotor = constrain(ch_throttle - control_roll - control_pitch - control_yaw, minThrottle, 2000);  // Back motor
#endif
#endif

#ifdef Hexa
   // Hexacopter mix
        LeftCWMotor = constrain(ch_throttle + control_roll - control_yaw, minThrottle, 2000); // Left Motor CW
        LeftCCWMotor = constrain(ch_throttle + (0.43*control_roll) + (0.89*control_pitch) + control_yaw, minThrottle, 2000); // Left Motor CCW
        RightCWMotor = constrain(ch_throttle - (0.43*control_roll) + (0.89*control_pitch) - control_yaw, minThrottle, 2000); // Right Motor CW
        RightCCWMotor = constrain(ch_throttle - control_roll + control_yaw, minThrottle, 2000); // Right Motor CCW
        BackCWMotor = constrain(ch_throttle - (0.44*control_roll) - control_pitch - control_yaw, minThrottle, 2000);  // Back Motor CW
        BackCCWMotor = constrain(ch_throttle + (0.44*control_roll) - control_pitch + control_yaw, minThrottle, 2000); // Back Motor CCW
#endif   
    }
    if (motorArmed == 0) {
#ifdef IsAM
      digitalWrite(FR_LED, LOW);    // AM-Mode
#endif
      digitalWrite(LED_Green,HIGH); // Ready LED on

#ifdef Quad
      rightMotor = MIN_THROTTLE;
      leftMotor = MIN_THROTTLE;
      frontMotor = MIN_THROTTLE;
      backMotor = MIN_THROTTLE;
#endif

#ifdef Hexa
      LeftCWMotor = MIN_THROTTLE;
      LeftCCWMotor = MIN_THROTTLE;
      RightCWMotor = MIN_THROTTLE;
      RightCCWMotor = MIN_THROTTLE;
      BackCWMotor = MIN_THROTTLE;
      BackCCWMotor = MIN_THROTTLE;
#endif

      roll_I = 0;     // reset I terms of PID controls
      pitch_I = 0;
      yaw_I = 0; 
      // Initialize yaw command to actual yaw when throttle is down...
      command_rx_yaw = ToDeg(yaw);
    }
 
 #ifdef Quad
    APM_RC.OutputCh(0, rightMotor);   // Right motor
    APM_RC.OutputCh(1, leftMotor);    // Left motor
    APM_RC.OutputCh(2, frontMotor);   // Front motor
    APM_RC.OutputCh(3, backMotor);    // Back motor   
#endif

#ifdef Hexa
    APM_RC.OutputCh(0, LeftCWMotor);    // Left Motor CW
    APM_RC.OutputCh(1, LeftCCWMotor);    // Left Motor CCW
    APM_RC.OutputCh(2, RightCWMotor);   // Right Motor CW
    APM_RC.OutputCh(3, RightCCWMotor);   // Right Motor CCW    
    APM_RC.OutputCh(6, BackCWMotor);   // Back Motor CW
    APM_RC.OutputCh(7, BackCCWMotor);   // Back Motor CCW    
#endif

#ifdef Quad   
     // InstantPWM
    APM_RC.Force_Out0_Out1();
    APM_RC.Force_Out2_Out3();
#endif

#ifdef Hexa
      // InstantPWM
    APM_RC.Force_Out0_Out1();
    APM_RC.Force_Out2_Out3();
    APM_RC.Force_Out6_Out7();
#endif

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



