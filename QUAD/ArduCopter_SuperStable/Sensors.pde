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

// Read accels and gyros
void Update_Sensors(void)
{
    for(char i = 0; i < 6; i++)
    {
      //Sensor_Data_Raw[i] = Raw_Sensor_Read(i); //for debugging
      if (SENSOR_SIGN[i] < 0)
      {
        Sensor_Input[i] = Sensor_Offset[i] - Raw_Sensor_Read(i);
      }
      else
      {
        Sensor_Input[i] = Raw_Sensor_Read(i) - Sensor_Offset[i];
      }
    }
}


// Remap hardware functions to autopilot functions
int Raw_Sensor_Read(char n)
{
  if(n == GYRO_ROLL) return APM_ADC.Ch(1);
  if(n == GYRO_PITCH)return APM_ADC.Ch(2);
  if(n == GYRO_YAW)  return APM_ADC.Ch(0);
  if(n == ACCEL_X)   return APM_ADC.Ch(4);
  if(n == ACCEL_Y)   return APM_ADC.Ch(5);
  if(n == ACCEL_Z)   return APM_ADC.Ch(6);
  
  return 0;
}
  
  
void Calibrate_Gyro_Offsets(void)
{
  float aux_float[3];
  unsigned char i, j = 0;
  
  //Preload gyro offset filter with some rough values  
  aux_float[0] = Sensor_Offset[GYRO_ROLL];    
  aux_float[1] = Sensor_Offset[GYRO_PITCH];
  aux_float[2] = Sensor_Offset[GYRO_YAW];

  // Take the gyro offset values
  for(i = 0; i < 255; i++)
  {
    for(char y = GYRO_ROLL; y < (GYRO_ROLL + 3); y++)
    {  
      aux_float[y] = aux_float[y] * 0.8 + Raw_Sensor_Read(y) * 0.2;
    }
  
    delay(10);
    
    // Runnings lights effect to let user know that we are taking mesurements
    if(j == 0) 
    {
      digitalWrite(LED_Green, HIGH);
      digitalWrite(LED_Yellow, LOW);
      digitalWrite(LED_Red, LOW);
    } 
    else if (j == 1) 
    {
      digitalWrite(LED_Green, LOW);
      digitalWrite(LED_Yellow, HIGH);
      digitalWrite(LED_Red, LOW);
    } 
    else 
    {
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

  for(char y = GYRO_ROLL; y < (GYRO_ROLL + 3); y++)
  {  
    Sensor_Offset[y] = aux_float[y];
  }
  
#ifndef CONFIGURATOR
    for(i = 0; i < 6; i++)
    {
      SerPri("Sensor_Offset[]:");
      SerPriln(Sensor_Offset[i]);
    }
    SerPri("Yaw neutral value:");
    SerPri(yaw_mid);
#endif
}


/*
#ifdef UseBMP

void read_baro(void)
{
  float tempPresAlt;
  
  tempPresAlt = float(APM_BMP085.Press)/101325.0;
  tempPresAlt = pow(tempPresAlt, 0.190295);
  if (press_alt==0)
    press_alt = (1.0 - tempPresAlt) * 4433000;      // Altitude in cm
  else
    press_alt = press_alt*0.9 + ((1.0 - tempPresAlt) * 443300);  // Altitude in cm (filtered)
}

#endif
*/
// This filter limits the max difference between readings and also aply an average filter
int Sonar_Sensor_Filter(long new_value, int old_value, int max_diff)
{
  int diff_values;
  int result;
  
  if (old_value==0)     // Filter is not initialized (no old value)
    return(new_value);
  diff_values = new_value - old_value;      // Difference with old reading
  if (diff_values>max_diff)   
    result = old_value+max_diff;    // We limit the max difference between readings
  else
    {
    if (diff_values<-max_diff)
      result = old_value-max_diff;        // We limit the max difference between readings
    else
      result = (new_value+old_value)>>1;  // Small filtering (average filter)
    }
  return(result); 
}

// This filter limits the max difference between readings and also aply an average filter
long BMP_Sensor_Filter(long new_value, long old_value, int max_diff)
{
  long diff_values;
  long result;
  
  if (old_value==0)     // Filter is not initialized (no old value)
    return(new_value);
  diff_values = new_value - old_value;      // Difference with old reading
  if (diff_values>max_diff)   
    result = old_value+max_diff;    // We limit the max difference between readings
  else
    {
    if (diff_values<-max_diff)
      result = old_value-max_diff;        // We limit the max difference between readings
    else
      result = (new_value+old_value)>>1;  // Small filtering (average filter)
    }
  return(result); 
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

