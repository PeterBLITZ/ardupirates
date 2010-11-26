/*
 ArduCopter v1.3 - August 2010
 www.ArduCopter.com
 Copyright (c) 2010.  All rights reserved.
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

/* ************************************************************ */
/* GPS based Position control */
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
/* Altitude control... (based on sonar) */
int Altitude_control_Sonar(int Sonar_altitude, int target_sonar_altitude)
{
//  #define ALTITUDE_CONTROL_SONAR_OUTPUT_MIN 60
  #define ALTITUDE_CONTROL_SONAR_OUTPUT_MIN 40
  #define ALTITUDE_CONTROL_SONAR_OUTPUT_MAX 120

  float KP_SONAR_ALTITUDE = (KP_ALTITUDE * STABLE_MODE_KP_RATE); //0.7//0.8 //0.9 //1.0//1.05
  float KI_SONAR_ALTITUDE = (KI_ALTITUDE * STABLE_MODE_KP_RATE); //0.1//0.3
  float KD_SONAR_ALTITUDE = (KD_ALTITUDE * STABLE_MODE_KP_RATE); //0.7//0.7 //0.7 //0.75 //0.8
  
  int control_altitude;
   
  err_altitude_old = err_altitude;
  err_altitude = target_sonar_altitude - Sonar_altitude;  
  altitude_D = (float)(err_altitude-err_altitude_old)/0.05;
  altitude_I += (float)err_altitude*0.05;
  altitude_I = constrain(altitude_I,-100,100); //-1000,1000
  control_altitude = KP_SONAR_ALTITUDE*err_altitude + KD_SONAR_ALTITUDE*altitude_D + KI_SONAR_ALTITUDE*altitude_I;
  control_altitude = constrain(control_altitude,-ALTITUDE_CONTROL_SONAR_OUTPUT_MIN,ALTITUDE_CONTROL_SONAR_OUTPUT_MAX);
  return control_altitude;
}

/* Altitude control... (based on sonar) */
// With accelerometer damping
int Altitude_control_Sonar_v2(int Sonar_altitude, int target_sonar_altitude, float az_f)
{
//  #define ALTITUDE_CONTROL_SONAR_OUTPUT_MIN 60
  #define ALTITUDE_CONTROL_SONAR_OUTPUT_MIN 40
  #define ALTITUDE_CONTROL_SONAR_OUTPUT_MAX 120
  #define KP_ACCZ_DAMP 0.2

  float KP_SONAR_ALTITUDE = KP_ALTITUDE; //0.8 //0.9 //1.0//1.05
  float KI_SONAR_ALTITUDE = KI_ALTITUDE; //0.3
  float KD_SONAR_ALTITUDE = KD_ALTITUDE; //0.7 //0.7 //0.75 //0.8
  
  int control_altitude;
  float damp_factor;
  
  err_altitude_old = err_altitude;
  err_altitude = target_sonar_altitude - Sonar_altitude;  
  altitude_D = (float)(err_altitude-err_altitude_old)/0.05;
  altitude_I += (float)err_altitude*0.05;
  altitude_I = constrain(altitude_I,-25,50); //-1000,1000 
  control_altitude = KP_SONAR_ALTITUDE*err_altitude + KD_SONAR_ALTITUDE*altitude_D + KI_SONAR_ALTITUDE*altitude_I;
  damp_factor = constrain(1-(err_altitude/40),0,1);
  control_altitude -= KP_ACCZ_DAMP*damp_factor*az_f;
  control_altitude = constrain(control_altitude,-ALTITUDE_CONTROL_SONAR_OUTPUT_MIN,ALTITUDE_CONTROL_SONAR_OUTPUT_MAX);
  return control_altitude;
}


/* ************************************************************ */
/* Altitude control... (based on barometer) */
int Altitude_control_baro(int altitude, int target_altitude)
{ 
  #define ALTITUDE_CONTROL_BARO_OUTPUT_MIN 120
  #define ALTITUDE_CONTROL_BARO_OUTPUT_MAX 40
  
//  #define KP_BARO_ALTITUDE 0.6  //0.65
//  #define KD_BARO_ALTITUDE 0.0  //0.05
//  #define KI_BARO_ALTITUDE 0.1  //0.1

  float KP_BARO_ALTITUDE = (KP_ALTITUDE * 2 * STABLE_MODE_KP_RATE); //0.08
  float KI_BARO_ALTITUDE = (KI_ALTITUDE * 0.66 * STABLE_MODE_KP_RATE); //0.02
  float KD_BARO_ALTITUDE = (KD_ALTITUDE * 2 * STABLE_MODE_KP_RATE);
  
  int control_altitude;
  
  err_altitude_old = err_altitude;
  err_altitude = -(target_altitude - altitude);              // Invert error because barometric pressure becomes less the higher the Altitude.
  altitude_D = (float)(err_altitude-err_altitude_old)/0.05;  // 20Hz
  altitude_I += (float)err_altitude*0.05;
  altitude_I = constrain(altitude_I,-25,50);
  control_altitude = KP_BARO_ALTITUDE*err_altitude + KD_BARO_ALTITUDE*altitude_D + KI_BARO_ALTITUDE*altitude_I;
  control_altitude = constrain(control_altitude,-ALTITUDE_CONTROL_BARO_OUTPUT_MIN,ALTITUDE_CONTROL_BARO_OUTPUT_MAX);
  return control_altitude;
}

/* Altitude control... (based on barometer) */
/* Added accelerometer dumping control */
int Altitude_control_baro_v2(int altitude, int target_altitude)
{ 
  #define ALTITUDE_CONTROL_BARO_OUTPUT_MIN 40
  #define ALTITUDE_CONTROL_BARO_OUTPUT_MAX 120
  
//  #define KP_BARO_ALTITUDE 0.6  //0.65
//  #define KD_BARO_ALTITUDE 0.0  //0.05
//  #define KI_BARO_ALTITUDE 0.1  //0.1
  float KP_BARO_ALTITUDE = (KP_ALTITUDE * 2); //0.08
  float KI_BARO_ALTITUDE = (KI_ALTITUDE * 0.66); //0.02
  float KD_BARO_ALTITUDE = (KD_ALTITUDE * 2);
  
  #define KP_ACCZ_DAMP 0.5
  
  float az_f;
  int control_altitude;
  
  err_altitude_old = err_altitude;
  err_altitude = -(target_altitude - altitude);              // Invert error because barometric pressure becomes less the higher the Altitude.
  altitude_D = (float)(err_altitude-err_altitude_old)/0.05;  // 20Hz
  altitude_I += (float)err_altitude*0.05;
  altitude_I = constrain(altitude_I,-25,50);
  control_altitude = KP_BARO_ALTITUDE*err_altitude + KD_BARO_ALTITUDE*altitude_D + KI_BARO_ALTITUDE*altitude_I;
  control_altitude = constrain(control_altitude,-ALTITUDE_CONTROL_BARO_OUTPUT_MIN,ALTITUDE_CONTROL_BARO_OUTPUT_MAX);
  
  // Z accel on NED frame (convert accz from plane frame to NED frame [earth])
  az_f = Vector_Dot_Product(&DCM_Matrix[2][0],Accel_Vector);
  //Serial.println((int)az_f);
  control_altitude -= KP_ACCZ_DAMP*(az_f-GRAVITY);
  
  return control_altitude;
}

