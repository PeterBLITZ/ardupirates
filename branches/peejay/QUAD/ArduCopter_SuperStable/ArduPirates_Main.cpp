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

/**** Switch Functions *****
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

 ********************************************************************** */


/* ****************************************************************************** */
/* ****************************** Includes ************************************** */
/* ****************************************************************************** */
#include "ArduPirates_Setup.h"

#include "Wire/Wire.h"
#include "APM_ADC/APM_ADC.h"
#include "APM_RC/APM_RC.h"
#include "DataFlash/DataFlash.h"
#include "APM_Compass/APM_Compass.h"
#include "EEPROM/EEPROM.h"

#include "WProgram.h"
#include "HardwareSerial.h"

#include "defines.h"
#include "GlobalVars.h"


#ifdef UseBMP
  #include "APM_BMP085/APM_BMP085.h"
#endif



//Prototype defines//
//DCM.h
void Read_adc_raw(void);
int read_adc(int select);
void Normalize(void);
void Drift_correction(void);
void Accel_adjust(void);
void Matrix_update(void);
void Euler_angles(void);
float Vector_Dot_Product(float vector1[3],float vector2[3]);
void Vector_Cross_Product(float vectorOut[3], float v1[3],float v2[3]);
void Vector_Scale(float vectorOut[3],float vectorIn[3], float scale2);
void Vector_Add(float vectorOut[3],float vectorIn1[3], float vectorIn2[3]);
void Matrix_Multiply(float a[3][3], float b[3][3],float mat[3][3]);

//Functions.h
void RadioCalibration();
void comma();

//Log.h
void Log_Write_Sensor(int s1, int s2, int s3,int s4, int s5, int s6, int s7);
void Log_Write_Attitude(int log_roll, int log_pitch, int log_yaw);
void Log_Write_PID(unsigned char num_PID, int P, int I,int D, int output);
void Log_Write_GPS(long log_Time, long log_Lattitude, long log_Longitude, long log_Altitude, 
                  long log_Ground_Speed, long log_Ground_Course, unsigned char log_Fix, unsigned char log_NumSats);
void Log_Write_Radio(int ch1, int ch2, int ch3,int ch4, int ch5, int ch6);
void Log_Read_Sensor();
void Log_Read_Attitude();
void Log_Read_PID();
void Log_Read_GPS();
void Log_Read_Radio();
void Log_Read(int start_page, int end_page);

//Navigation.h
void Position_control(long lat_dest, long lon_dest);
void BMP_Altitude_control(float BMP_target_alt);

//Sensors.h
void ReadSCP1000(void);
void read_airspeed(void);
void read_battery(void);
void zero_airspeed(void);

//SerialCom.h
void readSerialCommand();
void sendSerialTelemetry();
float readFloatSerial();






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
  if (AP_mode==F_MODE_SUPER_STABLE)        // Normal Mode => Stabilization mode
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
  if (AP_mode==F_MODE_SUPER_STABLE)        // Normal mode => Stabilization mode
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

#ifdef BATTERY_EVENT
  pinMode(LOW_BATTERY_OUT, OUTPUT);   // Battery Alarm output
  digitalWrite(LOW_BATTERY_OUT, LOW); // Silence Alarm
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
    cameracounteron++;

//    BMP_buffercounter++;
    GPS_counter++;
    timer_old = timer;
    timer=millis();
    G_Dt = (timer-timer_old)*0.001;      // Real time of loop run 

    // IMU DCM Algorithm
    Read_adc_raw();
    
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

#ifdef IsCAMERATRIGGER
      if (cameracounteron < 1000)   //interval in seconds between triggering the camera (1000 = 5 seconds)
        APM_RC.OutputCh(4, 2000);    //output for the servo - zero position
      else
        APM_RC.OutputCh(4, 200);     //output for the servo - push
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
        if (AP_mode == F_MODE_SUPER_STABLE || AP_mode == F_MODE_ABS_HOLD) 
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
        AP_mode = F_MODE_SUPER_STABLE;  // Stable mode & Altitude hold mode (Stabilization assist mode)
        digitalWrite(LED_Yellow,LOW);   // Yellow LED off
      }
      else if (ch_aux < 1250 && ch_aux2 > 1800)
      {
        AP_mode = F_MODE_ABS_HOLD;      // Position & Altitude hold mode (GPS position control & Altitude control)
        digitalWrite(LED_Yellow,HIGH);  // Yellow LED On
      }
      else if (ch_aux < 1250 && ch_aux2 < 1250)
      {
        AP_mode = F_MODE_POS_HOLD;      // Position hold mode (GPS position control)
        digitalWrite(LED_Yellow,HIGH);  // Yellow LED On
      }
      else 
      {
        AP_mode = F_MODE_ACROBATIC;     // Acrobatic mode
        digitalWrite(LED_Yellow,LOW); // Yellow LED off
      }     // End reading Quad Mode from Channel 5 & 6

#ifdef RELAY_LED_LIGHTS
      if( ch_mode > 1500 ) {  // FIXME, Rx channel and value should be configurable
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
    else if (AP_mode==F_MODE_POS_HOLD)  // Position Control (just GPS without altitude)
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
    else if (AP_mode==F_MODE_SUPER_STABLE)  // SuperStable Mode (Altitude Hold and Heading Hold) (if no stick movement)
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
    else if (AP_mode == F_MODE_ACROBATIC)  //Acrobatic Mode
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

      if (AP_mode == F_MODE_POS_HOLD || AP_mode ==F_MODE_ABS_HOLD)
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

    if (AP_mode == F_MODE_POS_HOLD || AP_mode == F_MODE_SUPER_STABLE || AP_mode == F_MODE_ABS_HOLD) 
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
      case F_MODE_POS_HOLD:
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
      case F_MODE_POS_HOLD:
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










//Functions.pde


void RadioCalibration() 
{
  long command_timer;
  int command; 
  int counter = 5;
  boolean Cmd_ok; 
  long roll_new = 0;
  long pitch_new = 0;
  long yaw_new = 0;

  SerFlu();
  SerPriln("Entering Radio Calibration mode");
  SerPriln("Current channel MID values are:");
  SerPri("ROLL: ");
  SerPri(roll_mid);
  SerPri(" PITCH: ");
  SerPri(pitch_mid);
  SerPri(" YAW: ");
  SerPri(yaw_mid);
  SerPriln();
  SerPriln("Recalibrate Channel MID's [Y/N]?: ");
  command_timer = millis();

  // Start counter loop and wait serial input. If not input for 5 seconds, return to normal mode
  while(millis() - command_timer < 5000) {
    if (SerAva()) {
      queryType = SerRea();
      if(queryType == 'y' || queryType == 'Y') {  
        Cmd_ok = TRUE;
        break;    
      } 
      else {
        Cmd_ok = FALSE;     
        break;
      }
    }
  }
  if(Cmd_ok) {
    // We have a go. Let's do new calibration
    SerPriln("Starting calibration run in 5 seconds. Place all sticks to their middle including trims");
    for(counter = 5; counter >= 0; counter --) {
      command_timer = millis();
      while(millis() - command_timer < 1000) {
      }
      SerPriln(counter);
    }
    // Do actual calibration now
    SerPriln("Measuring average channel values");
    SerPriln("ROLL, PITCH, YAW");

    counter = 0; // Reset counter for just in case. 
    command_timer = millis();
    while(millis() - command_timer < 1000) {

      if (APM_RC.GetState()==1) {  // New radio frame?
        // Commands from radio Rx... 
        ch_roll = channel_filter(APM_RC.InputCh(0), ch_roll);
        ch_pitch = channel_filter(APM_RC.InputCh(1), ch_pitch);
        ch_throttle = channel_filter(APM_RC.InputCh(2), ch_throttle);
        ch_yaw = channel_filter(APM_RC.InputCh(3), ch_yaw);
        ch_aux = APM_RC.InputCh(4);
        ch_aux2 = APM_RC.InputCh(5);

        SerPri(ch_roll);
        comma();
        SerPri(ch_pitch);
        comma();
        SerPri(ch_yaw);
        SerPriln();
        roll_new += ch_roll;
        pitch_new += ch_pitch; 
        yaw_new += ch_yaw;
        counter++;
      }
    }
    SerPri("New samples received: ");
    SerPriln(counter);    
    roll_new = roll_new / counter;
    pitch_new = pitch_new / counter;
    yaw_new = yaw_new / counter;
    SerPri("New values as: ");
    SerPri("ROLL: ");
    SerPri(roll_new);
    SerPri(" PITCH: ");
    SerPri(pitch_new);
    SerPri(" YAW: ");
    SerPri(yaw_new);
    SerPriln();
    SerPriln("Accept & Save values [Y/N]?: ");
    Cmd_ok = FALSE;
    while(millis() - command_timer < 5000) {
      if (SerAva()) {
        queryType = SerRea();
        if(queryType == 'y' || queryType == 'Y') { 
          Cmd_ok = TRUE;
          roll_mid = roll_new;
          pitch_mid = pitch_new;
          yaw_mid = yaw_new;
          SerPriln("Values accepted, remember to save them to EEPROM with 'W' command");
          break;    
        } 
        else {
          Cmd_ok = TRUE;
          break;
        }
      }   
    } 
  } 
  if(queryType == 'n' || queryType == 'N') Cmd_ok = TRUE;
  if(Cmd_ok)
    SerPriln("Returning normal mode...");
  else SerPriln("Command timeout, returning normal mode....");
}

void comma() {
  SerPri(',');
}



















//SerialComm.pde
void readSerialCommand() {
  // Check for serial message
  if (SerAva()) {
    queryType = SerRea();
    switch (queryType) {
    case 'A': // Stable PID
      KP_QUAD_ROLL = readFloatSerial();
      KI_QUAD_ROLL = readFloatSerial();
      STABLE_MODE_KP_RATE_ROLL = readFloatSerial();
      KP_QUAD_PITCH = readFloatSerial();
      KI_QUAD_PITCH = readFloatSerial();
      STABLE_MODE_KP_RATE_PITCH = readFloatSerial();
      KP_QUAD_YAW = readFloatSerial();
      KI_QUAD_YAW = readFloatSerial();
      STABLE_MODE_KP_RATE_YAW = readFloatSerial();
      STABLE_MODE_KP_RATE = readFloatSerial();   // NOT USED NOW
      MAGNETOMETER = readFloatSerial();
      break;
    case 'C': // Receive GPS PID
      KP_GPS_ROLL = readFloatSerial();
      KI_GPS_ROLL = readFloatSerial();
      KD_GPS_ROLL = readFloatSerial();
      KP_GPS_PITCH = readFloatSerial();
      KI_GPS_PITCH = readFloatSerial();
      KD_GPS_PITCH = readFloatSerial();
      GPS_MAX_ANGLE = readFloatSerial();
      GEOG_CORRECTION_FACTOR = readFloatSerial();
      break;
    case 'E': // Receive altitude PID
      KP_ALTITUDE = readFloatSerial();
      KD_ALTITUDE = readFloatSerial();
      KI_ALTITUDE = readFloatSerial();
      break;
    case 'G': // Receive drift correction PID
      Kp_ROLLPITCH = readFloatSerial();
      Ki_ROLLPITCH = readFloatSerial();
      Kp_YAW = readFloatSerial();
      Ki_YAW = readFloatSerial();
      break;
    case 'I': // Receive sensor offset
      gyro_offset_roll = readFloatSerial();
      gyro_offset_pitch = readFloatSerial();
      gyro_offset_yaw = readFloatSerial();
      acc_offset_x = readFloatSerial();
      acc_offset_y = readFloatSerial();
      acc_offset_z = readFloatSerial();
      break;
    case 'K': // Spare
      break;
    case 'M': // Receive debug motor commands
      frontMotor = readFloatSerial();
      backMotor = readFloatSerial();
      rightMotor = readFloatSerial();
      leftMotor = readFloatSerial();
      motorArmed = readFloatSerial();
      break;
    case 'O': // Rate Control PID
      Kp_RateRoll = readFloatSerial();
      Ki_RateRoll = readFloatSerial();
      Kd_RateRoll = readFloatSerial();
      Kp_RatePitch = readFloatSerial();
      Ki_RatePitch = readFloatSerial();
      Kd_RatePitch = readFloatSerial();
      Kp_RateYaw = readFloatSerial();
      Ki_RateYaw = readFloatSerial();
      Kd_RateYaw = readFloatSerial();
      xmitFactor = readFloatSerial();
      break;
    case 'W': // Write all user configurable values to EEPROM
      writeUserConfig();
      break;
    case 'Y': // Initialize EEPROM with default values
      defaultUserConfig();
      break;
    case '1': // Receive transmitter calibration values
      ch_roll_slope = readFloatSerial();
      ch_roll_offset = readFloatSerial();
      ch_pitch_slope = readFloatSerial();
      ch_pitch_offset = readFloatSerial();
      ch_yaw_slope = readFloatSerial();
      ch_yaw_offset = readFloatSerial();
      ch_throttle_slope = readFloatSerial();
      ch_throttle_offset = readFloatSerial();
      ch_aux_slope = readFloatSerial();
      ch_aux_offset = readFloatSerial();
      ch_aux2_slope = readFloatSerial();
      ch_aux2_offset = readFloatSerial();
    break;
    }
  }
}

void sendSerialTelemetry() {
  float aux_float[3]; // used for sensor calibration
  switch (queryType) {
  case '=': // Reserved debug command to view any variable from Serial Monitor
/*    SerPri("throttle =");
    SerPriln(ch_throttle);
    SerPri("control roll =");
    SerPriln(control_roll-CHANN_CENTER);
    SerPri("control pitch =");
    SerPriln(control_pitch-CHANN_CENTER);
    SerPri("control yaw =");
    SerPriln(control_yaw-CHANN_CENTER);
    SerPri("front left yaw =");
    SerPriln(frontMotor);
    SerPri("front right yaw =");
    SerPriln(rightMotor);
    SerPri("rear left yaw =");
    SerPriln(leftMotor);
    SerPri("rear right motor =");
    SerPriln(backMotor);
    SerPriln();

    SerPri("current roll rate =");
    SerPriln(read_adc(0));
    SerPri("current pitch rate =");
    SerPriln(read_adc(1));
    SerPri("current yaw rate =");
    SerPriln(read_adc(2));
    SerPri("command rx yaw =");
    SerPriln(command_rx_yaw);
    SerPriln(); 
    queryType = 'X';*/
    SerPri(APM_RC.InputCh(0));
    comma();
    SerPri(ch_roll_slope);
    comma();
    SerPri(ch_roll_offset);
    comma();
    SerPriln(ch_roll);
    break;
  case 'B': // Send roll, pitch and yaw PID values
    SerPri(KP_QUAD_ROLL, 3);
    comma();
    SerPri(KI_QUAD_ROLL, 3);
    comma();
    SerPri(STABLE_MODE_KP_RATE_ROLL, 3);
    comma();
    SerPri(KP_QUAD_PITCH, 3);
    comma();
    SerPri(KI_QUAD_PITCH, 3);
    comma();
    SerPri(STABLE_MODE_KP_RATE_PITCH, 3);
    comma();
    SerPri(KP_QUAD_YAW, 3);
    comma();
    SerPri(KI_QUAD_YAW, 3);
    comma();
    SerPri(STABLE_MODE_KP_RATE_YAW, 3);
    comma();
    SerPri(STABLE_MODE_KP_RATE, 3);    // NOT USED NOW
    comma();
    SerPriln(MAGNETOMETER, 3);
    queryType = 'X';
    break;
  case 'D': // Send GPS PID
    SerPri(KP_GPS_ROLL, 3);
    comma();
    SerPri(KI_GPS_ROLL, 3);
    comma();
    SerPri(KD_GPS_ROLL, 3);
    comma();
    SerPri(KP_GPS_PITCH, 3);
    comma();
    SerPri(KI_GPS_PITCH, 3);
    comma();
    SerPri(KD_GPS_PITCH, 3);
    comma();
    SerPri(GPS_MAX_ANGLE, 3);
    comma();
    SerPriln(GEOG_CORRECTION_FACTOR, 3);
    queryType = 'X';
    break;
  case 'F': // Send altitude PID
    SerPri(KP_ALTITUDE, 3);
    comma();
    SerPri(KI_ALTITUDE, 3);
    comma();
    SerPriln(KD_ALTITUDE, 3);
    queryType = 'X';
    break;
  case 'H': // Send drift correction PID
    SerPri(Kp_ROLLPITCH, 4);
    comma();
    SerPri(Ki_ROLLPITCH, 7);
    comma();
    SerPri(Kp_YAW, 4);
    comma();
    SerPriln(Ki_YAW, 6);
    queryType = 'X';
    break;
  case 'J': // Send sensor offset
    SerPri(gyro_offset_roll);
    comma();
    SerPri(gyro_offset_pitch);
    comma();
    SerPri(gyro_offset_yaw);
    comma();
    SerPri(acc_offset_x);
    comma();
    SerPri(acc_offset_y);
    comma();
    SerPriln(acc_offset_z);
    AN_OFFSET[3] = acc_offset_x;
    AN_OFFSET[4] = acc_offset_y;
    AN_OFFSET[5] = acc_offset_z;
    queryType = 'X';
    break;
  case 'L': // Spare
    RadioCalibration();
    queryType = 'X';
    break;
  case 'N': // Send magnetometer config
    queryType = 'X';
    break;
  case 'P': // Send rate control PID
    SerPri(Kp_RateRoll, 3);
    comma();
    SerPri(Ki_RateRoll, 3);
    comma();
    SerPri(Kd_RateRoll, 3);
    comma();
    SerPri(Kp_RatePitch, 3);
    comma();
    SerPri(Ki_RatePitch, 3);
    comma();
    SerPri(Kd_RatePitch, 3);
    comma();
    SerPri(Kp_RateYaw, 3);
    comma();
    SerPri(Ki_RateYaw, 3);
    comma();
    SerPri(Kd_RateYaw, 3);
    comma();
    SerPriln(xmitFactor, 3);
    queryType = 'X';
    break;
  case 'Q': // Send sensor data
    SerPri(read_adc(0));
    comma();
    SerPri(read_adc(1));
    comma();
    SerPri(read_adc(2));
    comma();
    SerPri(read_adc(4));
    comma();
    SerPri(read_adc(3));
    comma();
    SerPri(read_adc(5));
    comma();
    SerPri(err_roll);
    comma();
    SerPri(err_pitch);
    comma();
    SerPri(degrees(roll));
    comma();
    SerPri(degrees(pitch));
    comma();
    SerPriln(degrees(yaw));
    break;
  case 'R': // Send raw sensor data
    break;
  case 'S': // Send all flight data
    SerPri(timer-timer_old);
    comma();
    SerPri(read_adc(0));
    comma();
    SerPri(read_adc(1));
    comma();
    SerPri(read_adc(2));
    comma();
    SerPri(ch_throttle);
    comma();
    SerPri(control_roll);
    comma();
    SerPri(control_pitch);
    comma();
    SerPri(control_yaw);
    comma();
    SerPri(frontMotor); // Front Motor
    comma();
    SerPri(backMotor); // Back Motor
    comma();
    SerPri(rightMotor); // Right Motor
    comma();
    SerPri(leftMotor); // Left Motor
    comma();
    SerPri(read_adc(4));
    comma();
    SerPri(read_adc(3));
    comma();
    SerPriln(read_adc(5));
    break;
  case 'T': // Spare
    SerPri("AP Mode = ");
    if (AP_mode == 0) 
      SerPriln("Acrobatic");
    else if (AP_mode == 1)
      SerPriln("Position Hold");
    else if (AP_mode == 2)
      SerPriln("Stable Mode");
    else if (AP_mode == 3)
      SerPriln("Position & Altitude Hold");
//    SerPri("BMP Mode = ");
//    if (BMP_mode == 0) {
//      SerPriln("Off");
//    } else {
//      SerPriln("On");
//    } 
//    SerPri("Target Altitude = ");
//    SerPriln(BMP_target_altitude);
//    SerPri("Current Altitude = ");
//    SerPriln(BMP_Altitude);
//    SerPri("throttle_command = ");
//    SerPriln(ch_throttle);
//    SerPri("Yaw mid = ");
//    SerPriln(yaw_mid);
//    SerPri("BMP_altitude command = ");
//    SerPriln(BMP_command_altitude);
//    SerPri("Amount RX Yaw = ");
//    SerPriln(amount_rx_yaw);
    SerPri("Current Compass Heading = ");
    current_heading_hold = APM_Compass.Heading;
    if (current_heading_hold < 0)
      current_heading_hold += ToRad(360);
    SerPriln(ToDeg(current_heading_hold), 3);
//    SerPri("Error Course = ");
//    SerPriln(ToDeg(errorCourse), 3);
    SerPri("Heading Hold Mode = ");
    if (heading_hold_mode == 0) 
      SerPriln("Off");
    else 
      SerPriln("On");
    SerPri("BMP Mode = ");
    if (BMP_mode == 0) 
      SerPriln("Off");
    else 
      SerPriln("On");
//    SerPri("KP ALTITUDE = ");
//    SerPriln(KP_ALTITUDE, 3);
//    SerPri("EEPROM KP ALTITUDE = ");
//    SerPriln(readEEPROM(KP_ALTITUDE_ADR), 3);
//    SerPri("KP ROLL ACRO MODE = ");
//    SerPriln(Kp_RateRoll, 3);
//    SerPri("EEPROM KP ROLL ACRO MODE = ");
//    SerPriln(readEEPROM(KP_RATEROLL_ADR), 3);
    SerPri("STABLE MODE KP RATE ROLL = ");
    SerPriln(STABLE_MODE_KP_RATE_ROLL, 3);
    SerPri("EEPROM STABLE MODE KP RATE ROLL = ");
    SerPriln(readEEPROM(STABLE_MODE_KP_RATE_ROLL_ADR), 3);
    SerPri("STABLE MODE KP RATE PITCH = ");
    SerPriln(STABLE_MODE_KP_RATE_PITCH, 3);
    SerPri("EEPROM STABLE MODE KP RATE PITCH = ");
    SerPriln(readEEPROM(STABLE_MODE_KP_RATE_PITCH_ADR), 3);
//    SerPri("KP PITCH ACRO MODE = ");
//    SerPriln(Kp_RatePitch, 3);
//    SerPri("EEPROM KP PITCH ACRO MODE = ");
//    SerPriln(readEEPROM(KP_RATEPITCH_ADR), 3);
//    SerPri("KP STABLE MODE YAW = ");
//    SerPriln(STABLE_MODE_KP_RATE_YAW, 3);
//    SerPri("EEPROM KP STABLE MODE YAW = ");
//    SerPriln(readEEPROM(STABLE_MODE_KP_RATE_YAW_ADR), 3);

//    SerPri("KP GPS Roll = ");
//    SerPriln(KP_GPS_ROLL, 3);
//    SerPri("KP GPS Pitch = ");
//    SerPriln(KP_GPS_PITCH, 3);
//    SerPri("EEPROM KP GPS ROLL = ");
//    SerPriln(readEEPROM(KP_GPS_ROLL_ADR), 3);
//    SerPri("EEPROM KP GPS PITCH = ");
//    SerPriln(readEEPROM(KP_GPS_ROLL_ADR), 3);
//    SerPri("KI GPS Roll = ");
//    SerPriln(KI_GPS_ROLL, 4);
//    SerPri("KI GPS Pitch = ");
//    SerPriln(KI_GPS_PITCH, 4);
//    SerPri("EEPROM KI GPS ROLL = ");
//    SerPriln(readEEPROM(KI_GPS_ROLL_ADR), 4);
//    SerPri("EEPROM KP GPS PITCH = ");
//    SerPriln(readEEPROM(KI_GPS_ROLL_ADR), 4);
    SerPri("Magnetometer = ");
    SerPriln(MAGNETOMETER);
    SerPri("EEPROM Magnetometer = ");
    SerPriln(readEEPROM(MAGNETOMETER_ADR));
//    SerPri("Magnetometer Offset= ");
//    SerPriln(Magoffset);
//    SerPri("EEPROM Magoffset = ");
//    SerPriln(readEEPROM(Magoffset_ADR));
    
//    SerPri("Yaw = ");
//    SerPriln(yaw);
//    SerPri("Yaw to Degree = ");
//    SerPriln(ToDeg(yaw));
//    SerPri("command rx yaw =");
//    SerPriln(command_rx_yaw);

    SerPriln(); 
    queryType = 'X';
     break;
  case 'U': // Send receiver values
    SerPri(ch_roll); // Aileron
    comma();
    SerPri(ch_pitch); // Elevator
    comma();
    SerPri(ch_yaw); // Yaw
    comma();
    SerPri(ch_throttle); // Throttle
    comma();
    SerPri(ch_aux); // AUX1 (Mode)
    comma();
    SerPri(ch_aux2); // AUX2 
    comma();
    SerPri(roll_mid); // Roll MID value
    comma();
    SerPri(pitch_mid); // Pitch MID value
    comma();
    SerPriln(yaw_mid); // Yaw MID Value
    break;
  case 'V': // Spare
    break;
  case 'X': // Stop sending messages
    break;
  case '!': // Send flight software version
    SerPriln(VER);
    queryType = 'X';
    break;
  case '2': // Send transmitter calibration values
    SerPri(ch_roll_slope);
    comma();
    SerPri(ch_roll_offset);
    comma();
    SerPri(ch_pitch_slope);
    comma();
    SerPri(ch_pitch_offset);
    comma();
    SerPri(ch_yaw_slope);
    comma();
    SerPri(ch_yaw_offset);
    comma();
    SerPri(ch_throttle_slope);
    comma();
    SerPri(ch_throttle_offset);
    comma();
    SerPri(ch_aux_slope);
    comma();
    SerPri(ch_aux_offset);
    comma();
    SerPri(ch_aux2_slope);
    comma();
    SerPriln(ch_aux2_offset);
    queryType = 'X';
  break;
  case '.': // Modify GPS settings
    Serial1.print("$PGCMD,16,0,0,0,0,0*6A\r\n");
    break;
  }
}

// Used to read floating point values from the serial port
float readFloatSerial() {
  byte index = 0;
  byte timeout = 0;
  char data[128] = "";

  do {
    if (SerAva() == 0) {
      delay(10);
      timeout++;
    }
    else {
      data[index] = SerRea();
      timeout = 0;
      index++;
    }
  }  
  while ((data[constrain(index-1, 0, 128)] != ';') && (timeout < 5) && (index < 128));
  return atof(data);
}
































//DCM.pde
/* ******* ADC functions ********************* */
// Read all the ADC channles
void Read_adc_raw(void)
{
  int temp;
  
  for (int i=0;i<6;i++)
    AN[i] = APM_ADC.Ch(sensors[i]);
  
  // Correction for non ratiometric sensor (test code)
  gyro_temp = APM_ADC.Ch(3);
}

// Returns an analog value with the offset
int read_adc(int select)
{
  if (SENSOR_SIGN[select]<0)
    return (AN_OFFSET[select]-AN[select]);
  else
    return (AN[select]-AN_OFFSET[select]);
}
/* ******************************************* */

/* ******* DCM IMU functions ********************* */
/**************************************************/
void Normalize(void)
{
  float error=0;
  float temporary[3][3];
  float renorm=0;
  
  error= -Vector_Dot_Product(&DCM_Matrix[0][0],&DCM_Matrix[1][0])*.5; //eq.19

  Vector_Scale(&temporary[0][0], &DCM_Matrix[1][0], error); //eq.19
  Vector_Scale(&temporary[1][0], &DCM_Matrix[0][0], error); //eq.19
  
  Vector_Add(&temporary[0][0], &temporary[0][0], &DCM_Matrix[0][0]);//eq.19
  Vector_Add(&temporary[1][0], &temporary[1][0], &DCM_Matrix[1][0]);//eq.19
  
  Vector_Cross_Product(&temporary[2][0],&temporary[0][0],&temporary[1][0]); // c= a x b //eq.20
  
  renorm= .5 *(3 - Vector_Dot_Product(&temporary[0][0],&temporary[0][0])); //eq.21
  Vector_Scale(&DCM_Matrix[0][0], &temporary[0][0], renorm);
  
  renorm= .5 *(3 - Vector_Dot_Product(&temporary[1][0],&temporary[1][0])); //eq.21
  Vector_Scale(&DCM_Matrix[1][0], &temporary[1][0], renorm);
  
  renorm= .5 *(3 - Vector_Dot_Product(&temporary[2][0],&temporary[2][0])); //eq.21
  Vector_Scale(&DCM_Matrix[2][0], &temporary[2][0], renorm);
}

/**************************************************/
void Drift_correction(void)
{
  //Compensation the Roll, Pitch and Yaw drift. 
  float errorCourse;
  static float Scaled_Omega_P[3];
  static float Scaled_Omega_I[3];
  float Compass_Reading;
  
  //*****Roll and Pitch***************

  Vector_Cross_Product(&errorRollPitch[0],&Accel_Vector[0],&DCM_Matrix[2][0]); //adjust the ground of reference
  // errorRollPitch are in Accel ADC units
  // Limit max errorRollPitch to limit max Omega_P and Omega_I
  errorRollPitch[0] = constrain(errorRollPitch[0],-50,50);
  errorRollPitch[1] = constrain(errorRollPitch[1],-50,50);
  errorRollPitch[2] = constrain(errorRollPitch[2],-50,50);
  Vector_Scale(&Omega_P[0],&errorRollPitch[0],Kp_ROLLPITCH);
  
  Vector_Scale(&Scaled_Omega_I[0],&errorRollPitch[0],Ki_ROLLPITCH);
  Vector_Add(Omega_I,Omega_I,Scaled_Omega_I);
  
  //*****YAW***************
  // We make the gyro YAW drift correction based on compass magnetic heading 
if (MAGNETOMETER == 1) {

      errorCourse= (DCM_Matrix[0][0]*APM_Compass.Heading_Y) - (DCM_Matrix[1][0]*APM_Compass.Heading_X);  //Calculating YAW error
      Vector_Scale(errorYaw,&DCM_Matrix[2][0],errorCourse); //Applys the yaw correction to the XYZ rotation of the aircraft, depeding the position.
  
      Vector_Scale(&Scaled_Omega_P[0],&errorYaw[0],Kp_YAW);
      Vector_Add(Omega_P,Omega_P,Scaled_Omega_P);//Adding  Proportional.
    
    // Limit max errorYaw to limit max Omega_I
      errorYaw[0] = constrain(errorYaw[0],-50,50);
      errorYaw[1] = constrain(errorYaw[1],-50,50);
      errorYaw[2] = constrain(errorYaw[2],-50,50);
  
      Vector_Scale(&Scaled_Omega_I[0],&errorYaw[0],Ki_YAW);
      Vector_Add(Omega_I,Omega_I,Scaled_Omega_I);//adding integrator to the Omega_I
  }
}
/**************************************************/
void Accel_adjust(void)
{
  //Accel_Vector[1] += Accel_Scale(speed_3d*Omega[2]);  // Centrifugal force on Acc_y = GPS_speed*GyroZ
  //Accel_Vector[2] -= Accel_Scale(speed_3d*Omega[1]);  // Centrifugal force on Acc_z = GPS_speed*GyroY
}
/**************************************************/

void Matrix_update(void)
{
  Gyro_Vector[0]=Gyro_Scaled_X(read_adc(0)); //gyro x roll
  Gyro_Vector[1]=Gyro_Scaled_Y(read_adc(1)); //gyro y pitch
  Gyro_Vector[2]=Gyro_Scaled_Z(read_adc(2)); //gyro Z yaw
  
  // Low pass filter on accelerometer data (to filter vibrations)
  Accel_Vector[0]=Accel_Vector[0]*0.6 + (float)read_adc(3)*0.4; // acc x
  Accel_Vector[1]=Accel_Vector[1]*0.6 + (float)read_adc(4)*0.4; // acc y
  Accel_Vector[2]=Accel_Vector[2]*0.6 + (float)read_adc(5)*0.4; // acc z
  
  Vector_Add(&Omega[0], &Gyro_Vector[0], &Omega_I[0]);//adding integrator
  Vector_Add(&Omega_Vector[0], &Omega[0], &Omega_P[0]);//adding proportional
  
  //Accel_adjust();//adjusting centrifugal acceleration. // Not used for quadcopter
  
 #if OUTPUTMODE==1 // corrected mode
  Update_Matrix[0][0]=0;
  Update_Matrix[0][1]=-G_Dt*Omega_Vector[2];//-z
  Update_Matrix[0][2]=G_Dt*Omega_Vector[1];//y
  Update_Matrix[1][0]=G_Dt*Omega_Vector[2];//z
  Update_Matrix[1][1]=0;
  Update_Matrix[1][2]=-G_Dt*Omega_Vector[0];//-x
  Update_Matrix[2][0]=-G_Dt*Omega_Vector[1];//-y
  Update_Matrix[2][1]=G_Dt*Omega_Vector[0];//x
  Update_Matrix[2][2]=0;
  #endif
  #if OUTPUTMODE==0 // uncorrected data of the gyros (with drift)
  Update_Matrix[0][0]=0;
  Update_Matrix[0][1]=-G_Dt*Gyro_Vector[2];//-z
  Update_Matrix[0][2]=G_Dt*Gyro_Vector[1];//y
  Update_Matrix[1][0]=G_Dt*Gyro_Vector[2];//z
  Update_Matrix[1][1]=0;
  Update_Matrix[1][2]=-G_Dt*Gyro_Vector[0];
  Update_Matrix[2][0]=-G_Dt*Gyro_Vector[1];
  Update_Matrix[2][1]=G_Dt*Gyro_Vector[0];
  Update_Matrix[2][2]=0;
  #endif

  Matrix_Multiply(DCM_Matrix,Update_Matrix,Temporary_Matrix); //a*b=c

  for(int x=0; x<3; x++)  //Matrix Addition (update)
  {
    for(int y=0; y<3; y++)
    {
      DCM_Matrix[x][y]+=Temporary_Matrix[x][y];
    } 
  }
}

void Euler_angles(void)
{
  #if (OUTPUTMODE==2)         // Only accelerometer info (debugging purposes)
    roll = atan2(Accel_Vector[1],Accel_Vector[2]);   // atan2(acc_y,acc_z)
    pitch = -asin((Accel_Vector[0])/(float)GRAVITY); // asin(acc_x)
    yaw = 0;
  #else        // Euler angles from DCM matrix
    pitch = asin(-DCM_Matrix[2][0]);
    roll = atan2(DCM_Matrix[2][1],DCM_Matrix[2][2]);
    yaw = atan2(DCM_Matrix[1][0],DCM_Matrix[0][0]);
  #endif
}


// VECTOR FUNCTIONS
//Computes the dot product of two vectors
float Vector_Dot_Product(float vector1[3],float vector2[3])
{
  float op=0;
  
  for(int c=0; c<3; c++)
  {
  op+=vector1[c]*vector2[c];
  }
  
  return op; 
}

//Computes the cross product of two vectors
void Vector_Cross_Product(float vectorOut[3], float v1[3],float v2[3])
{
  vectorOut[0]= (v1[1]*v2[2]) - (v1[2]*v2[1]);
  vectorOut[1]= (v1[2]*v2[0]) - (v1[0]*v2[2]);
  vectorOut[2]= (v1[0]*v2[1]) - (v1[1]*v2[0]);
}

//Multiply the vector by a scalar. 
void Vector_Scale(float vectorOut[3],float vectorIn[3], float scale2)
{
  for(int c=0; c<3; c++)
  {
   vectorOut[c]=vectorIn[c]*scale2; 
  }
}

void Vector_Add(float vectorOut[3],float vectorIn1[3], float vectorIn2[3])
{
  for(int c=0; c<3; c++)
  {
     vectorOut[c]=vectorIn1[c]+vectorIn2[c];
  }
}

/********* MATRIX FUNCTIONS *****************************************/
//Multiply two 3x3 matrixs. This function developed by Jordi can be easily adapted to multiple n*n matrix's. (Pero me da flojera!). 
void Matrix_Multiply(float a[3][3], float b[3][3],float mat[3][3])
{
  float op[3]; 
  for(int x=0; x<3; x++)
  {
    for(int y=0; y<3; y++)
    {
      for(int w=0; w<3; w++)
      {
       op[w]=a[x][w]*b[w][y];
      } 
      mat[x][y]=0;
      mat[x][y]=op[0]+op[1]+op[2];
      
      float test=mat[x][y];
    }
  }
}

























//Sensors.pde

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























//Log.pde
// Test code to Write and Read packets from DataFlash log memory
// Packets : Attitude, Raw sensor data, Radio and GPS

#define HEAD_BYTE1 0xA3
#define HEAD_BYTE2 0x95
#define END_BYTE   0xBA

#define LOG_ATTITUDE_MSG 0x01
#define LOG_GPS_MSG      0x02
#define LOG_RADIO_MSG    0x03
#define LOG_SENSOR_MSG   0x04
#define LOG_PID_MSG      0x05

// Write a Sensor Raw data packet
void Log_Write_Sensor(int s1, int s2, int s3,int s4, int s5, int s6, int s7)
{
  DataFlash.WriteByte(HEAD_BYTE1);
  DataFlash.WriteByte(HEAD_BYTE2);
  DataFlash.WriteByte(LOG_SENSOR_MSG);
  DataFlash.WriteInt(s1);
  DataFlash.WriteInt(s2);
  DataFlash.WriteInt(s3);
  DataFlash.WriteInt(s4);
  DataFlash.WriteInt(s5);
  DataFlash.WriteInt(s6);
  DataFlash.WriteInt(s7);
  DataFlash.WriteByte(END_BYTE);
}

// Write an attitude packet. Total length : 10 bytes
void Log_Write_Attitude(int log_roll, int log_pitch, int log_yaw)
{
  DataFlash.WriteByte(HEAD_BYTE1);
  DataFlash.WriteByte(HEAD_BYTE2);
  DataFlash.WriteByte(LOG_ATTITUDE_MSG);
  DataFlash.WriteInt(log_roll);
  DataFlash.WriteInt(log_pitch);
  DataFlash.WriteInt(log_yaw);
  DataFlash.WriteByte(END_BYTE);
}

// Write a PID control info
void Log_Write_PID(byte num_PID, int P, int I,int D, int output)
{
  DataFlash.WriteByte(HEAD_BYTE1);
  DataFlash.WriteByte(HEAD_BYTE2);
  DataFlash.WriteByte(LOG_PID_MSG);
  DataFlash.WriteByte(num_PID);
  DataFlash.WriteInt(P);
  DataFlash.WriteInt(I);
  DataFlash.WriteInt(D);
  DataFlash.WriteInt(output);
  DataFlash.WriteByte(END_BYTE);
}

// Write an GPS packet. Total length : 30 bytes
void Log_Write_GPS(long log_Time, long log_Lattitude, long log_Longitude, long log_Altitude, 
                  long log_Ground_Speed, long log_Ground_Course, byte log_Fix, byte log_NumSats)
{
  DataFlash.WriteByte(HEAD_BYTE1);
  DataFlash.WriteByte(HEAD_BYTE2);
  DataFlash.WriteByte(LOG_GPS_MSG);
  DataFlash.WriteLong(log_Time);
  DataFlash.WriteByte(log_Fix);
  DataFlash.WriteByte(log_NumSats);
  DataFlash.WriteLong(log_Lattitude);
  DataFlash.WriteLong(log_Longitude);
  DataFlash.WriteLong(log_Altitude);
  DataFlash.WriteLong(log_Ground_Speed);
  DataFlash.WriteLong(log_Ground_Course);
  DataFlash.WriteByte(END_BYTE);
}

// Write a Radio packet
void Log_Write_Radio(int ch1, int ch2, int ch3,int ch4, int ch5, int ch6)
{
  DataFlash.WriteByte(HEAD_BYTE1);
  DataFlash.WriteByte(HEAD_BYTE2);
  DataFlash.WriteByte(LOG_RADIO_MSG);
  DataFlash.WriteInt(ch1);
  DataFlash.WriteInt(ch2);
  DataFlash.WriteInt(ch3);
  DataFlash.WriteInt(ch4);
  DataFlash.WriteInt(ch5);
  DataFlash.WriteInt(ch6);
  DataFlash.WriteByte(END_BYTE);
}

// **** READ ROUTINES ****

// Read a Sensor raw data packet
void Log_Read_Sensor()
{  
  SerPri("SENSOR:");
  SerPri(DataFlash.ReadInt());  // GX
  SerPri(",");
  SerPri(DataFlash.ReadInt());  // GY
  SerPri(",");
  SerPri(DataFlash.ReadInt());  // GZ
  SerPri(",");
  SerPri(DataFlash.ReadInt());  // ACCX
  SerPri(",");
  SerPri(DataFlash.ReadInt());  // ACCY
  SerPri(",");
  SerPri(DataFlash.ReadInt());  // ACCZ
  SerPri(",");
  SerPri(DataFlash.ReadInt());  // AUX 
  SerPriln();
}

// Read an attitude packet
void Log_Read_Attitude()
{  
  int log_roll;
  int log_pitch;
  int log_yaw;

  log_roll = DataFlash.ReadInt();
  log_pitch = DataFlash.ReadInt();
  log_yaw = DataFlash.ReadInt(); 
  SerPri("ATT:");
  SerPri(log_roll);
  SerPri(",");
  SerPri(log_pitch);
  SerPri(",");
  SerPri(log_yaw);
  SerPriln();
}

// Read a Sensor raw data packet
void Log_Read_PID()
{  
  SerPri("PID:");
  SerPri((int)DataFlash.ReadByte());  // NUM_PID
  SerPri(",");
  SerPri(DataFlash.ReadInt());  // P
  SerPri(",");
  SerPri(DataFlash.ReadInt());  // I
  SerPri(",");
  SerPri(DataFlash.ReadInt());  // D
  SerPri(",");
  SerPri(DataFlash.ReadInt());  // output
  SerPriln();
}

// Read a GPS packet
void Log_Read_GPS()
{
  long log_Time;
  byte log_Fix;
  byte log_NumSats;
  long log_Lattitude;
  long log_Longitude;
  long log_Altitude;
  long log_Ground_Speed;
  long log_Ground_Course;
 
  log_Time = DataFlash.ReadLong();
  log_Fix = DataFlash.ReadByte();
  log_NumSats = DataFlash.ReadByte();
  log_Lattitude = DataFlash.ReadLong();
  log_Longitude = DataFlash.ReadLong();
  log_Altitude = DataFlash.ReadLong();
  log_Ground_Speed = DataFlash.ReadLong();
  log_Ground_Course = DataFlash.ReadLong();

  SerPri("GPS:");
  SerPri(log_Time);
  SerPri(",");
  SerPri((int)log_Fix);
  SerPri(",");
  SerPri((int)log_NumSats);
  SerPri(",");
  SerPri(log_Lattitude);
  SerPri(",");
  SerPri(log_Longitude);
  SerPri(",");
  SerPri(log_Altitude);
  SerPri(",");
  SerPri(log_Ground_Speed);
  SerPri(",");
  SerPri(log_Ground_Course);
  SerPriln();

}

// Read an Radio packet
void Log_Read_Radio()
{  
  SerPri("RADIO:");
  SerPri(DataFlash.ReadInt());
  SerPri(",");
  SerPri(DataFlash.ReadInt());
  SerPri(",");
  SerPri(DataFlash.ReadInt());
  SerPri(",");
  SerPri(DataFlash.ReadInt());
  SerPri(",");
  SerPri(DataFlash.ReadInt());
  SerPri(",");
  SerPri(DataFlash.ReadInt());
  SerPriln();
}

// Read the DataFlash log memory : Packet Parser
void Log_Read(int start_page, int end_page)
{
  byte data;
  byte log_step=0;
  long packet_count=0; 

  DataFlash.StartRead(start_page);
  while (DataFlash.GetPage() < end_page)
    {
    data = DataFlash.ReadByte();
    switch(log_step)     //This is a state machine to read the packets
      {
      case 0:  
        if(data==HEAD_BYTE1)  // Head byte 1
          log_step++;
        break; 
      case 1:
        if(data==HEAD_BYTE2)  // Head byte 2
          log_step++;
        break; 
      case 2:
        switch (data)
          {
          case LOG_ATTITUDE_MSG:
            Log_Read_Attitude();
            log_step++;
            break;
          case LOG_GPS_MSG:
            Log_Read_GPS();
            log_step++;
            break;
          case LOG_RADIO_MSG:
            Log_Read_Radio();
            log_step++;
            break;
          case LOG_SENSOR_MSG:
            Log_Read_Sensor();
            log_step++;
            break;
          case LOG_PID_MSG:
            Log_Read_PID();
            log_step++;
            break;
          default:
            SerPri("Error Reading Packet: ");
            SerPri(packet_count); 
            log_step=0;   // Restart, we have a problem...
          }
        break;
      case 3:
        if(data==END_BYTE)
           packet_count++;
        else
           SerPriln("Error Reading END_BYTE");
        log_step=0;      // Restart sequence: new packet...        
      }
    }
  SerPri("Number of packets read: ");
  SerPriln(packet_count);
}




























//Navigation.pde
void Position_control(long lat_dest, long lon_dest)
{
  long Lon_diff;
  long Lat_diff;

  Lon_diff = lon_dest - GPS.Longitude;
  Lat_diff = lat_dest - GPS.Lattitude;
  
  // ROLL
  //Optimization : cos(yaw) = DCM_Matrix[0][0] ;  sin(yaw) = DCM_Matrix[1][0] 
  gps_err_roll = (float)Lon_diff * GEOG_CORRECTION_FACTOR * DCM_Matrix[0][0] - (float)Lat_diff * DCM_Matrix[1][0];

  gps_roll_D = (gps_err_roll-gps_err_roll_old) / GPS_Dt;
  gps_err_roll_old = gps_err_roll;
  
  gps_roll_I += gps_err_roll * GPS_Dt;
  gps_roll_I = constrain(gps_roll_I, -800, 800);

  command_gps_roll = KP_GPS_ROLL * gps_err_roll + KD_GPS_ROLL * gps_roll_D + KI_GPS_ROLL * gps_roll_I;
  command_gps_roll = constrain(command_gps_roll, -GPS_MAX_ANGLE, GPS_MAX_ANGLE); // Limit max command
  
  //Log_Write_PID(1,KP_GPS_ROLL*gps_err_roll*10,KI_GPS_ROLL*gps_roll_I*10,KD_GPS_ROLL*gps_roll_D*10,command_gps_roll*10);
  
  // PITCH
  gps_err_pitch = -(float)Lat_diff * DCM_Matrix[0][0] - (float)Lon_diff * GEOG_CORRECTION_FACTOR * DCM_Matrix[1][0];

  gps_pitch_D = (gps_err_pitch - gps_err_pitch_old) / GPS_Dt;
  gps_err_pitch_old = gps_err_pitch;

  gps_pitch_I += gps_err_pitch * GPS_Dt;
  gps_pitch_I = constrain(gps_pitch_I, -800, 800);

  command_gps_pitch = KP_GPS_PITCH * gps_err_pitch + KD_GPS_PITCH * gps_pitch_D + KI_GPS_PITCH * gps_pitch_I;
  command_gps_pitch = constrain(command_gps_pitch, -GPS_MAX_ANGLE, GPS_MAX_ANGLE); // Limit max command

  
  //Log_Write_PID(1,KP_GPS_ROLL*gps_err_roll*10,KI_GPS_ROLL*gps_roll_I*10,KD_GPS_ROLL*gps_roll_D*10,command_gps_roll*10);
}

/* ************************************************************ */
// Altitude control...Based on BMP Sensor 
// Hein's Version 
void BMP_Altitude_control(float BMP_target_alt)
{
  BMP_err_altitude_old = BMP_err_altitude;
  BMP_err_altitude = BMP_target_alt - BMP_Altitude;  
  BMP_altitude_D = (float)(BMP_err_altitude - BMP_err_altitude_old) / G_Dt;
  BMP_altitude_I += (float)BMP_err_altitude * G_Dt;
  BMP_altitude_I = constrain(BMP_altitude_I, -50, 50);
  BMP_command_altitude = (KP_ALTITUDE * BMP_err_altitude) + (KD_ALTITUDE * BMP_altitude_D) + (KI_ALTITUDE * BMP_altitude_I);
  BMP_command_altitude = constrain(BMP_command_altitude, -50, 50); // Limit max command
  BMP_command_altitude = -1 * BMP_command_altitude;
  if (BMP_command_altitude < -30)    // Limit the copter from fall to fast out of the sky.  
    BMP_command_altitude = -30;      //Somehow the constrain -30,50 cause the heading hold to drift.
}
