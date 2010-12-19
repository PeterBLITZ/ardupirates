/*
 ArduCopter v1.7 - Dec 2010
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

      if (AP_mode == F_MODE_ABS_HOLD)
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
// Altitude control...Based on BMP Sensor 

void BMP_Altitude_control(float BMP_target_alt)
{
  BMP_err_altitude_old = BMP_err_altitude;
  BMP_err_altitude = BMP_target_alt - BMP_Altitude;  
  BMP_altitude_D = (float)(BMP_err_altitude - BMP_err_altitude_old) / G_Dt;
  BMP_altitude_I += (float)BMP_err_altitude * G_Dt;
  BMP_altitude_I = constrain(BMP_altitude_I, -100, 100);
  BMP_command_altitude = KP_ALTITUDE * BMP_err_altitude + KD_ALTITUDE * BMP_altitude_D + KI_ALTITUDE * BMP_altitude_I;
  BMP_command_altitude = constrain(BMP_command_altitude, -100, 100); // Limit max command
}
