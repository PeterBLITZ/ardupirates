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
    if (APmode == F_MODE_SUPER_STABLE || APmode == F_MODE_ABS_HOLD) 
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

#if BATTERY_EVENT == 1
void read_battery(void)
{
  battery_voltage = BATTERY_VOLTAGE(analogRead(BATTERY_PIN)) * .1 + battery_voltage * .9;
  if(battery_voltage < LOW_VOLTAGE)
    low_battery_event();
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

