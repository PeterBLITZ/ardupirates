/*
 www.ArduCopter.com - www.DIYDrones.com
 Copyright (c) 2010.  All rights reserved.
 An Open Source Arduino based multicopter.
 
 File     : UserConfig.h 
 Version  : v1.0, Aug 27, 2010
 Author(s): ArduCopter Team
             Ted Carancho (aeroquad), Jose Julio, Jordi Muñoz,
             Jani Hirvinen, Ken McEwans, Roberto Navoni,           
             Sandro Benigno, Chris Anderson
 
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

- 27-08-2010, New header layout

* ************************************************************** *
TODO:

- List of thigs
- that still need to be done

* ************************************************************** */




// Sensor: GYROX, GYROY, GYROZ, ACCELX, ACCELY, ACCELZ
uint8_t sensors[6] = {1, 2, 0, 4, 5, 6};  // For ArduPilot Mega Sensor Shield Hardware

// Sensor: GYROX, GYROY, GYROZ,   ACCELX, ACCELY, ACCELZ,     MAGX, MAGY, MAGZ
int SENSOR_SIGN[]={
  1, -1, -1,    -1, 1, 1,     -1, -1, -1}; 

int AN[6]; //array that store the 6 ADC channels
int AN_OFFSET[6]; //Array that store the Offset of the gyros and accelerometers
int gyro_temp;


float G_Dt=0.02;                  // Integration time for the gyros (DCM algorithm)
float Accel_Vector[3]= {0, 0, 0}; //Store the acceleration in a vector
float Accel_Vector_unfiltered[3]= {0, 0, 0}; //Store the acceleration in a vector
float Gyro_Vector[3]= {0, 0, 0};  //Store the gyros rutn rate in a vector
float Omega_Vector[3]= {0, 0, 0}; //Corrected Gyro_Vector data
float Omega_P[3]= {0, 0, 0};      //Omega Proportional correction
float Omega_I[3]= {0, 0, 0};      //Omega Integrator
float Omega[3]= {0, 0, 0};

float errorRollPitch[3] = {0, 0, 0};
float errorYaw[3] = {0, 0, 0};
float errorCourse = 0;
float COGX = 0; //Course overground X axis
float COGY = 1; //Course overground Y axis

float roll = 0;
float pitch = 0;
float yaw = 0;

//  Counter for Loop Control
unsigned int Magneto_counter = 0;
unsigned int BMP_counter = 0;
unsigned int GPS_counter = 0;
unsigned int cameracounteron = 0;

float DCM_Matrix[3][3]= {
  { 1,0,0 },
  { 0,1,0 },
  { 0,0,1 }}; 
float Update_Matrix[3][3]={
  { 0,1,2 },
  { 3,4,5 },
  { 6,7,8 }}; //Gyros here

float Temporary_Matrix[3][3]={
  { 0,0,0 },
  { 0,0,0 },
  { 0,0,0 }};

// GPS variables
float speed_3d=0;
int GPS_ground_speed=0;

// Main timers
long timer=0; 
long timer_old;
long GPS_timer;
long GPS_timer_old;
float GPS_Dt=0.2;   // GPS Dt

// Attitude control variables
float command_rx_roll=0;        // User commands
float command_rx_pitch=0;
float command_rx_yaw=0;
//float amount_rx_yaw=0;
int control_roll;           // PID control results
int control_pitch;
int control_yaw;
float K_aux;

// Attitude PID controls
float roll_I=0;
float roll_D;
float err_roll;
float pitch_I=0;
float pitch_D;
float err_pitch;
float yaw_I=0;
float yaw_D;
float err_yaw;

//Position and Stable control
long target_longitude;
long target_lattitude;
unsigned char target_position = 0;
unsigned char target_alt_position = 0;
unsigned char heading_hold_mode = 0;
float current_heading_hold;
float target_altitude;
float gps_err_roll;
float gps_err_roll_old;
float gps_roll_D;
float gps_roll_I=0;
float gps_err_pitch;
float gps_err_pitch_old;
float gps_pitch_D;
float gps_pitch_I=0;
float command_gps_roll;
float command_gps_pitch;
float command_throttle;

//Altitude control
int Initial_Throttle;
int target_sonar_altitude;
int err_altitude;
int err_altitude_old;
float command_altitude;
float altitude_I;
float altitude_D;

//Pressure Sensor variables
#ifdef UseBMP
float BMP_target_altitude;
float BMP_err_altitude;
float BMP_err_altitude_old;
float BMP_command_altitude;
float BMP_altitude_I;
float BMP_altitude_D;
float tempPresAlt;
float BMP_Altitude;
#endif

float 	battery_voltage = LOW_VOLTAGE * 1.05;		// Battery Voltage, initialized above threshold

// Sonar variables
int Sonar_value=0;
#define SonarToCm(x) (x*1.26)   // Sonar raw value to centimeters
int Sonar_Counter=0;

// AP_mode : 1=> Position hold  2=> Stabilization assist mode (normal mode) 0=> Acrobatic mode
unsigned char AP_mode = 0;  
unsigned char BMP_mode = 0;  //0 = Altitude hold off

//  PID Tuning
unsigned char Plus = 0;
unsigned char Minus = 0;
unsigned char Plus_mode = 0;
unsigned char Minus_mode = 0;
unsigned char P_of_PID_mode = 0;
unsigned char I_of_PID_mode = 0;
unsigned char D_of_PID_mode = 0;
unsigned char toggle_switch = 0;

// Mode LED timers and variables, used to blink LED_Green
unsigned char gled_status = HIGH;
long gled_timer;
int gled_speed;

long t0;
int num_iter;
float aux_debug;

// Radio definitions
int roll_mid;
int pitch_mid;
int yaw_mid;
int throttle_mid = 1450;

int Neutro_yaw;
int ch_roll;
int ch_pitch;
int ch_throttle;
int ch_yaw;
int ch_aux;
int ch_aux2;
int ch_mode;

int frontMotor;
int backMotor;
int leftMotor;
int rightMotor;
unsigned char motorArmed = 0;
int minThrottle = 0;

// Serial communication
char queryType;
long tlmTimer = 0;

// Arming/Disarming
uint8_t Arming_counter=0;
uint8_t Disarming_counter=0;

long mm_led1_timer;  // time (in milliseconds) of the last blink
int  mm_led1_speed;  // milliseconds between blinks
unsigned char mm_led1_status; // current status - LOW or HIGH

long mm_led2_timer;  // time (in milliseconds) of the last blink
int  mm_led2_speed;  // milliseconds between blinks
unsigned char mm_led2_status; // current status - LOW or HIGH












/*************************************************************/
// Safety & Security 

// Arm & Disarm delays
#define ARM_DELAY 100      // milliseconds of how long you need to keep rudder to max right for arming motors
#define DISARM_DELAY 50   // milliseconds of how long you need to keep rudder to max left for disarming motors


/*************************************************************/
// AM Mode & Flight information 

/* AM PIN Definitions */
/* Will be moved in future to AN extension ports */
/* due need to have PWM pins free for sonars and servos */

#define FR_LED 3  // Mega PE4 pin, OUT7
#define RE_LED 2  // Mega PE5 pin, OUT6
#define RI_LED 7  // Mega PH4 pin, OUT5
#define LE_LED 8  // Mega PH5 pin, OUT4

/* AM PIN Definitions - END */


/*************************************************************/
// Radio related definitions

// If you don't know these values, you can activate RADIO_TEST_MODE below
// and check your mid values

//#define RADIO_TEST_MODE

#define ROLL_MID 1500    // Radio Roll channel mid value
#define PITCH_MID 1500    // Radio Pitch channel mid value
#define YAW_MID 1500    // Radio Yaw channel mid value
#define THROTTLE_MID 1505    // Radio Throttle channel mid value
#define AUX_MID 1500

#define CHANN_CENTER 1500       // Channel center, legacy
#define MIN_THROTTLE 1040       // Throttle pulse width at minimun...

// Following variables stored in EEPROM
float KP_QUAD_ROLL;
float KI_QUAD_ROLL;
float STABLE_MODE_KP_RATE_ROLL;
float KP_QUAD_PITCH;
float KI_QUAD_PITCH;
float STABLE_MODE_KP_RATE_PITCH;
float KP_QUAD_YAW;
float KI_QUAD_YAW;
float STABLE_MODE_KP_RATE_YAW;
float STABLE_MODE_KP_RATE; // NOT USED NOW
float KP_GPS_ROLL;
float KI_GPS_ROLL;
float KD_GPS_ROLL;
float KP_GPS_PITCH;
float KI_GPS_PITCH;
float KD_GPS_PITCH;
float GPS_MAX_ANGLE;
float KP_ALTITUDE;
float KI_ALTITUDE;
float KD_ALTITUDE;
int acc_offset_x;
int acc_offset_y;
int acc_offset_z;
int gyro_offset_roll;
int gyro_offset_pitch;
int gyro_offset_yaw;
float Kp_ROLLPITCH;
float Ki_ROLLPITCH;
float Kp_YAW;
float Ki_YAW;
float GEOG_CORRECTION_FACTOR;
int MAGNETOMETER;
float Kp_RateRoll;
float Ki_RateRoll;
float Kd_RateRoll;
float Kp_RatePitch;
float Ki_RatePitch;
float Kd_RatePitch;
float Kp_RateYaw;
float Ki_RateYaw;
float Kd_RateYaw;
float xmitFactor;
float ch_roll_slope = 1;
float ch_pitch_slope = 1;
float ch_throttle_slope = 1;
float ch_yaw_slope = 1;
float ch_aux_slope = 1;
float ch_aux2_slope = 1;
float ch_roll_offset = 0;
float ch_pitch_offset = 0;
float ch_throttle_offset = 0;
float ch_yaw_offset = 0;
float ch_aux_offset = 0;
float ch_aux2_offset = 0;

// This function call contains the default values that are set to the ArduCopter
// when a "Default EEPROM Value" command is sent through serial interface
void defaultUserConfig() {
  KP_QUAD_ROLL = 4.0;
  KI_QUAD_ROLL = 0.15;
  STABLE_MODE_KP_RATE_ROLL = 1.2;
  KP_QUAD_PITCH = 4.0;
  KI_QUAD_PITCH = 0.15;
  STABLE_MODE_KP_RATE_PITCH = 1.2;
  KP_QUAD_YAW = 3.0;
  KI_QUAD_YAW = 0.15;
  STABLE_MODE_KP_RATE_YAW = 2.4;
  STABLE_MODE_KP_RATE = 0.2;     // NOT USED NOW
  KP_GPS_ROLL = 0.003;
  KI_GPS_ROLL = 0.001;
  KD_GPS_ROLL = 0.0;
  KP_GPS_PITCH = 0.003;
  KI_GPS_PITCH = 0.001;
  KD_GPS_PITCH = 0.0;
  GPS_MAX_ANGLE = 10;
  KP_ALTITUDE = 1.5;
  KI_ALTITUDE = 0.02;
  KD_ALTITUDE = 0.05;
  acc_offset_x = 2047;
  acc_offset_y = 2032;
  acc_offset_z = 2071;
  gyro_offset_roll = 1659;
  gyro_offset_pitch = 1618;
  gyro_offset_yaw = 1673;
  Kp_ROLLPITCH = 0.0014;
  Ki_ROLLPITCH = 0.00000015;
  Kp_YAW = 1.2;
  Ki_YAW = 0.00005;
  GEOG_CORRECTION_FACTOR = 0.680;
  MAGNETOMETER = 1;
  Kp_RateRoll = 1.95;
  Ki_RateRoll = 0.0;
  Kd_RateRoll = 0.0;
  Kp_RatePitch = 1.95;
  Ki_RatePitch = 0.0;
  Kd_RatePitch = 0.0;  
  Kp_RateYaw = 3.2;
  Ki_RateYaw = 0.0;
  Kd_RateYaw = 0.0;
  xmitFactor = 0.32;
  roll_mid = 1500;
  pitch_mid = 1500;
  yaw_mid = 1500;
  ch_roll_slope = 1;
  ch_pitch_slope = 1;
  ch_throttle_slope = 1;
  ch_yaw_slope = 1;
  ch_aux_slope = 1;
  ch_aux2_slope = 1;
  ch_roll_offset = 0;
  ch_pitch_offset = 0;
  ch_throttle_offset = 0;
  ch_yaw_offset = 0;
  ch_aux_offset = 0;
  ch_aux2_offset = 0;
}

// EEPROM storage addresses
#define KP_QUAD_ROLL_ADR 0
#define KI_QUAD_ROLL_ADR 8
#define STABLE_MODE_KP_RATE_ROLL_ADR 4
#define KP_QUAD_PITCH_ADR 12
#define KI_QUAD_PITCH_ADR 20
#define STABLE_MODE_KP_RATE_PITCH_ADR 16
#define KP_QUAD_YAW_ADR 24
#define KI_QUAD_YAW_ADR 32
#define STABLE_MODE_KP_RATE_YAW_ADR 28
#define STABLE_MODE_KP_RATE_ADR 36      // NOT USED NOW
#define KP_GPS_ROLL_ADR 40
#define KI_GPS_ROLL_ADR 48
#define KD_GPS_ROLL_ADR 44
#define KP_GPS_PITCH_ADR 52
#define KI_GPS_PITCH_ADR 60
#define KD_GPS_PITCH_ADR 56
#define GPS_MAX_ANGLE_ADR 64
#define KP_ALTITUDE_ADR 68
#define KI_ALTITUDE_ADR 76
#define KD_ALTITUDE_ADR 72
#define acc_offset_x_ADR 80
#define acc_offset_y_ADR 84
#define acc_offset_z_ADR 88
#define gyro_offset_roll_ADR 92
#define gyro_offset_pitch_ADR 96
#define gyro_offset_yaw_ADR 100
#define Kp_ROLLPITCH_ADR 104
#define Ki_ROLLPITCH_ADR 108
#define Kp_YAW_ADR 112
#define Ki_YAW_ADR 116
#define GEOG_CORRECTION_FACTOR_ADR 120
#define MAGNETOMETER_ADR 124
#define XMITFACTOR_ADR 128
#define KP_RATEROLL_ADR 132
#define KI_RATEROLL_ADR 136
#define KD_RATEROLL_ADR 140
#define KP_RATEPITCH_ADR 144
#define KI_RATEPITCH_ADR 148
#define KD_RATEPITCH_ADR 152
#define KP_RATEYAW_ADR 156
#define KI_RATEYAW_ADR 160
#define KD_RATEYAW_ADR 164
#define CHROLL_MID 168
#define CHPITCH_MID 172
#define CHYAW_MID 176
#define ch_roll_slope_ADR 180
#define ch_pitch_slope_ADR 184
#define ch_throttle_slope_ADR 188
#define ch_yaw_slope_ADR 192
#define ch_aux_slope_ADR 196
#define ch_aux2_slope_ADR 200
#define ch_roll_offset_ADR 204
#define ch_pitch_offset_ADR 208
#define ch_throttle_offset_ADR 212
#define ch_yaw_offset_ADR 216
#define ch_aux_offset_ADR 220
#define ch_aux2_offset_ADR 224

// Utilities for writing and reading from the EEPROM
float readEEPROM(int address) {
  union floatStore {
    unsigned char floatByte[4];
    float floatVal;
  } floatOut;
  
  for (int i = 0; i < 4; i++) 
    floatOut.floatByte[i] = EEPROM.read(address + i);
  return floatOut.floatVal;
}

void writeEEPROM(float value, int address) {
  union floatStore {
    unsigned char floatByte[4];
    float floatVal;
  } floatIn;
  
  floatIn.floatVal = value;
  for (int i = 0; i < 4; i++) 
    EEPROM.write(address + i, floatIn.floatByte[i]);
}

void readUserConfig() {
  KP_QUAD_ROLL = readEEPROM(KP_QUAD_ROLL_ADR);
  KI_QUAD_ROLL = readEEPROM(KI_QUAD_ROLL_ADR);
  STABLE_MODE_KP_RATE_ROLL = readEEPROM(STABLE_MODE_KP_RATE_ROLL_ADR);
  KP_QUAD_PITCH = readEEPROM(KP_QUAD_PITCH_ADR);
  KI_QUAD_PITCH = readEEPROM(KI_QUAD_PITCH_ADR);
  STABLE_MODE_KP_RATE_PITCH = readEEPROM(STABLE_MODE_KP_RATE_PITCH_ADR);
  KP_QUAD_YAW = readEEPROM(KP_QUAD_YAW_ADR);
  KI_QUAD_YAW = readEEPROM(KI_QUAD_YAW_ADR);
  STABLE_MODE_KP_RATE_YAW = readEEPROM(STABLE_MODE_KP_RATE_YAW_ADR);
  STABLE_MODE_KP_RATE = readEEPROM(STABLE_MODE_KP_RATE_ADR);          // NOT USED NOW
  KP_GPS_ROLL = readEEPROM(KP_GPS_ROLL_ADR);
  KI_GPS_ROLL = readEEPROM(KI_GPS_ROLL_ADR);
  KD_GPS_ROLL = readEEPROM(KD_GPS_ROLL_ADR);
  KP_GPS_PITCH = readEEPROM(KP_GPS_PITCH_ADR);
  KI_GPS_PITCH = readEEPROM(KI_GPS_PITCH_ADR);
  KD_GPS_PITCH = readEEPROM(KD_GPS_PITCH_ADR);
  GPS_MAX_ANGLE = readEEPROM(GPS_MAX_ANGLE_ADR);
  KP_ALTITUDE = readEEPROM(KP_ALTITUDE_ADR);
  KI_ALTITUDE = readEEPROM(KI_ALTITUDE_ADR);
  KD_ALTITUDE = readEEPROM(KD_ALTITUDE_ADR);
  acc_offset_x = readEEPROM(acc_offset_x_ADR);
  acc_offset_y = readEEPROM(acc_offset_y_ADR);
  acc_offset_z = readEEPROM(acc_offset_z_ADR);
  gyro_offset_roll = readEEPROM(gyro_offset_roll_ADR);
  gyro_offset_pitch = readEEPROM(gyro_offset_pitch_ADR);
  gyro_offset_yaw = readEEPROM(gyro_offset_yaw_ADR);
  Kp_ROLLPITCH = readEEPROM(Kp_ROLLPITCH_ADR);
  Ki_ROLLPITCH = readEEPROM(Ki_ROLLPITCH_ADR);
  Kp_YAW = readEEPROM(Kp_YAW_ADR);
  Ki_YAW = readEEPROM(Ki_YAW_ADR);
  GEOG_CORRECTION_FACTOR = readEEPROM(GEOG_CORRECTION_FACTOR_ADR);
  MAGNETOMETER = readEEPROM(MAGNETOMETER_ADR);
  Kp_RateRoll = readEEPROM(KP_RATEROLL_ADR);
  Ki_RateRoll = readEEPROM(KI_RATEROLL_ADR);
  Kd_RateRoll = readEEPROM(KD_RATEROLL_ADR);
  Kp_RatePitch = readEEPROM(KP_RATEPITCH_ADR);
  Ki_RatePitch = readEEPROM(KI_RATEPITCH_ADR);
  Kd_RatePitch = readEEPROM(KD_RATEPITCH_ADR);
  Kp_RateYaw = readEEPROM(KP_RATEYAW_ADR);
  Ki_RateYaw = readEEPROM(KI_RATEYAW_ADR);
  Kd_RateYaw = readEEPROM(KD_RATEYAW_ADR);
  xmitFactor = readEEPROM(XMITFACTOR_ADR);
  roll_mid = readEEPROM(CHROLL_MID);
  pitch_mid = readEEPROM(CHPITCH_MID);
  yaw_mid = readEEPROM(CHYAW_MID);
  ch_roll_slope = readEEPROM(ch_roll_slope_ADR);
  ch_pitch_slope = readEEPROM(ch_pitch_slope_ADR);
  ch_throttle_slope = readEEPROM(ch_throttle_slope_ADR);
  ch_yaw_slope = readEEPROM(ch_yaw_slope_ADR);
  ch_aux_slope = readEEPROM(ch_aux_slope_ADR);
  ch_aux2_slope = readEEPROM(ch_aux2_slope_ADR);
  ch_roll_offset = readEEPROM(ch_roll_offset_ADR);
  ch_pitch_offset = readEEPROM(ch_pitch_offset_ADR);
  ch_throttle_offset = readEEPROM(ch_throttle_offset_ADR);
  ch_yaw_offset = readEEPROM(ch_yaw_offset_ADR);
  ch_aux_offset = readEEPROM(ch_aux_offset_ADR);
  ch_aux2_offset = readEEPROM(ch_aux2_offset_ADR);
}

void writeUserConfig() {
  writeEEPROM(KP_QUAD_ROLL, KP_QUAD_ROLL_ADR);
  writeEEPROM(KI_QUAD_ROLL, KI_QUAD_ROLL_ADR);
  writeEEPROM(STABLE_MODE_KP_RATE_ROLL, STABLE_MODE_KP_RATE_ROLL_ADR);
  writeEEPROM(KP_QUAD_PITCH, KP_QUAD_PITCH_ADR);
  writeEEPROM(KI_QUAD_PITCH, KI_QUAD_PITCH_ADR);
  writeEEPROM(STABLE_MODE_KP_RATE_PITCH, STABLE_MODE_KP_RATE_PITCH_ADR);
  writeEEPROM(KP_QUAD_YAW, KP_QUAD_YAW_ADR);
  writeEEPROM(KI_QUAD_YAW, KI_QUAD_YAW_ADR);
  writeEEPROM(STABLE_MODE_KP_RATE_YAW, STABLE_MODE_KP_RATE_YAW_ADR);
  writeEEPROM(STABLE_MODE_KP_RATE, STABLE_MODE_KP_RATE_ADR);  // NOT USED NOW
  writeEEPROM(KP_GPS_ROLL, KP_GPS_ROLL_ADR);
  writeEEPROM(KD_GPS_ROLL, KD_GPS_ROLL_ADR);
  writeEEPROM(KI_GPS_ROLL, KI_GPS_ROLL_ADR);
  writeEEPROM(KP_GPS_PITCH, KP_GPS_PITCH_ADR);
  writeEEPROM(KD_GPS_PITCH, KD_GPS_PITCH_ADR);
  writeEEPROM(KI_GPS_PITCH, KI_GPS_PITCH_ADR);
  writeEEPROM(GPS_MAX_ANGLE, GPS_MAX_ANGLE_ADR);
  writeEEPROM(KP_ALTITUDE, KP_ALTITUDE_ADR);
  writeEEPROM(KD_ALTITUDE, KD_ALTITUDE_ADR);
  writeEEPROM(KI_ALTITUDE, KI_ALTITUDE_ADR);
  writeEEPROM(acc_offset_x, acc_offset_x_ADR);
  writeEEPROM(acc_offset_y, acc_offset_y_ADR);
  writeEEPROM(acc_offset_z, acc_offset_z_ADR);
  writeEEPROM(gyro_offset_roll, gyro_offset_roll_ADR);
  writeEEPROM(gyro_offset_pitch, gyro_offset_pitch_ADR);
  writeEEPROM(gyro_offset_yaw, gyro_offset_yaw_ADR);
  writeEEPROM(Kp_ROLLPITCH, Kp_ROLLPITCH_ADR);
  writeEEPROM(Ki_ROLLPITCH, Ki_ROLLPITCH_ADR);
  writeEEPROM(Kp_YAW, Kp_YAW_ADR);
  writeEEPROM(Ki_YAW, Ki_YAW_ADR);
  writeEEPROM(GEOG_CORRECTION_FACTOR, GEOG_CORRECTION_FACTOR_ADR);
  writeEEPROM(MAGNETOMETER, MAGNETOMETER_ADR);
  writeEEPROM(Kp_RateRoll, KP_RATEROLL_ADR);
  writeEEPROM(Ki_RateRoll, KI_RATEROLL_ADR);
  writeEEPROM(Kd_RateRoll, KD_RATEROLL_ADR);
  writeEEPROM(Kp_RatePitch, KP_RATEPITCH_ADR);
  writeEEPROM(Ki_RatePitch, KI_RATEPITCH_ADR);
  writeEEPROM(Kd_RatePitch, KD_RATEPITCH_ADR);
  writeEEPROM(Kp_RateYaw, KP_RATEYAW_ADR);
  writeEEPROM(Ki_RateYaw, KI_RATEYAW_ADR);
  writeEEPROM(Kd_RateYaw, KD_RATEYAW_ADR);
  writeEEPROM(xmitFactor, XMITFACTOR_ADR);
  writeEEPROM(roll_mid, CHROLL_MID);
  writeEEPROM(pitch_mid, CHPITCH_MID);
  writeEEPROM(yaw_mid, CHYAW_MID);
  writeEEPROM(ch_roll_slope, ch_roll_slope_ADR);
  writeEEPROM(ch_pitch_slope, ch_pitch_slope_ADR);
  writeEEPROM(ch_throttle_slope, ch_throttle_slope_ADR);
  writeEEPROM(ch_yaw_slope, ch_yaw_slope_ADR);
  writeEEPROM(ch_aux_slope, ch_aux_slope_ADR);
  writeEEPROM(ch_aux2_slope, ch_aux2_slope_ADR);
  writeEEPROM(ch_roll_offset, ch_roll_offset_ADR);
  writeEEPROM(ch_pitch_offset, ch_pitch_offset_ADR);
  writeEEPROM(ch_throttle_offset, ch_throttle_offset_ADR);
  writeEEPROM(ch_yaw_offset, ch_yaw_offset_ADR);
  writeEEPROM(ch_aux_offset, ch_aux_offset_ADR);
  writeEEPROM(ch_aux2_offset, ch_aux2_offset_ADR);
}


