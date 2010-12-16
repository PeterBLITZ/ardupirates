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

#ifdef UseBMP
/*
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
*/
/*
void read_baro(void)
{
  float tempPresAlt;
  
  tempPresAlt = float(APM_BMP085.Press)/101325.0;
  //tempPresAlt = pow(tempPresAlt, 0.190284);
  //press_alt = (1.0 - tempPresAlt) * 145366.45;
  tempPresAlt = pow(tempPresAlt, 0.190295);
  if (press_alt==0)
    press_alt = (1.0 - tempPresAlt) * 4433000;      // Altitude in cm
  else
    press_alt = press_alt*0.9 + ((1.0 - tempPresAlt) * 443300);  // Altitude in cm (filtered)
}

*/
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
int IR_Sensor_Filter(int new_value, int old_value, int max_diff)
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


/* This function read IR data, convert to cm units and filter the data */
void Read_IR_Sensors()
{
  old_RF_Sensor1=RF_Sensor1;
  old_RF_Sensor2=RF_Sensor2;
  old_RF_Sensor3=RF_Sensor3;
  old_RF_Sensor4=RF_Sensor4;
  
  // Read IR sensors and convert units to cm (aprox)
  RF_Sensor1 = constrain(14500/IR_adc_fl,20,150);    // Limit to 20-150cm
  RF_Sensor2 = constrain(14500/IR_adc_fr,20,150);    
  RF_Sensor3 = constrain(14500/IR_adc_br,20,150);    
  RF_Sensor4 = constrain(14500/IR_adc_bl,20,150);    
  
  RF_Sensor1 = IR_Sensor_Filter(RF_Sensor1,old_RF_Sensor1,10);   // Filter the range data
  RF_Sensor2 = IR_Sensor_Filter(RF_Sensor2,old_RF_Sensor2,10);
  RF_Sensor3 = IR_Sensor_Filter(RF_Sensor3,old_RF_Sensor3,10);
  RF_Sensor4 = IR_Sensor_Filter(RF_Sensor4,old_RF_Sensor4,10);  
}

/* ************************************************************ */
/* Obstacle avoidance routine */
void RF_Obstacle_avoidance(int RF_Sensor1, int RF_Sensor2,int RF_Sensor3, int RF_Sensor4)
{
  
  int RF_pair1;
  int RF_pair2;
  
  int RF_err_roll;
  int RF_err_pitch;
  float RF_roll_D;
  float RF_pitch_D;
  static int RF_err_roll_old;
  static int RF_err_pitch_old;
  
  RF_pair1 = RF_Sensor3 - RF_Sensor1;  // Back right sensor - Front left sensor
  RF_pair2 = RF_Sensor4 - RF_Sensor2;  // Back left sensor - Front right sensor
  
  // ROLL
  RF_err_roll = RF_pair1 - RF_pair2;
  
  RF_roll_D = (RF_err_roll-RF_err_roll_old)/0.05;   // RF_IR frequency is 20Hz (50ms)
  RF_err_roll_old = RF_err_roll;
  
  RF_roll_I += RF_err_roll*0.05;  
  RF_roll_I = constrain(RF_roll_I,-25,25);
  
  command_RF_roll = KP_RF_ROLL*RF_err_roll + KD_RF_ROLL*RF_roll_D + KI_RF_ROLL*RF_roll_I;
  command_RF_roll = constrain(command_RF_roll,-RF_MAX_ANGLE,RF_MAX_ANGLE); // Limit max command
  
  // PITCH
  RF_err_pitch = RF_pair1 + RF_pair2;
  
  RF_pitch_D = (RF_err_pitch-RF_err_pitch_old)/0.05;
  RF_err_pitch_old = RF_err_pitch;
  
  RF_pitch_I += RF_err_pitch*0.05;
  RF_pitch_I = constrain(RF_pitch_I,-25,25);
  
  command_RF_pitch = KP_RF_PITCH*RF_err_pitch + KD_RF_PITCH*RF_pitch_D + KI_RF_PITCH*RF_pitch_I;
  command_RF_pitch = constrain(command_RF_pitch,-RF_MAX_ANGLE,RF_MAX_ANGLE); // Limit max command
}

