/*
 www.ArduCopter.com - www.DIYDrones.com
 Copyright (c) 2011.  All rights reserved.
 An Open Source Arduino based multicopter.
 
 File     : Sensors.pde
 Version  : v1.1, February 19, 2011
 Author(s): ArduCopter Team
             Ted Carancho (aeroquad), Jose Julio, Jordi Muñoz,
             Jani Hirvinen, Ken McEwans, Roberto Navoni,          
             Sandro Benigno, Chris Anderson
            Dror Caspi
 
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

* ************************************************************** */

// Local Definitions

#define SENSORS_CALIBRATE_MIN_STABLE_CYCLES     600
          // How many 5 msec cycles are required, with the sensors stable,
          // until we're done calibrating
#define SENSORS_CALIBRATE_AVG_SHIFT             6
          // Determines the IIR averaging period. For 5 msec rate this is
          // about 2^6 * 5 which is ~ 300 msec
#define SENSORS_CALIBRATE_AVG_DEV_MAX           2
          // How much the current reading can deviate from the average
          // and still be considered stable
#define SENSORS_CALIBRATE_OFFSET_DEV_MAX        200
          // How much the gyro reading can deviate from the pre-configured
          // offset and still be acceptable.  This is used for sanity
          // checking only
#define SENSORS_CALIBRATE_LED_STABLE_SLOWDOWN   5
          // Determines blinking rate when sensors are stable
#define SENSORS_CALIBRATE_LED_UNSTABLE_SLOWDOWN 25
          // Determines blinking rate when sensors are not stable


/* ******* ADC functions ********************* */
// Read all the ADC channles
void Read_adc_raw(void)
{
  //int temp;
  
  for (int i=0;i<6;i++)
    AN[i] = adc.Ch(sensors[i]);
}

// Returns an analog value with the offset
int read_adc(int select)
{
  if (SENSOR_SIGN[select]<0)
    return (AN_OFFSET[select]-AN[select]);
  else
    return (AN[select]-AN_OFFSET[select]);
}


//=============================================================================
//
// Calibrate the sensors on power up
//
void calibrateSensors(void)
{
  uint16_t stable_cycles;
  uint8_t  led_cycle = 0;
  uint8_t  led_delay = 0;
  uint8_t  sensor;
  int32_t  avg[LASTSENSOR];   // Average of sensor inputs.  Scale is
                              // (1 << SENSORS_CALIBRATE_AVG_SHIFT) times
                              // sensor read scale.
  int16_t  avg_diff;          // Difference of current reading from average
  int16_t  offset_diff;       // Difference of current reading from nominal offset
  static const int16_t default_offsets[LASTSENSOR] PROGMEM = 
  {
    1650,   // GYROZ
    1650,   // GYROX
    1650,   // GYROY
    2048,   // ACCELX
    2048,   // ACCELY
    2480    // ACCELZ
  };                          // Table of default offset per sensor
                              // TODO: this should really be public, and the numbers
                              // TODO: should be used in the initialization code in
                              // TODO: arducopter.h


  // TODO: I'm not sure why the initial read is really needed
  
  Read_adc_raw();     // Read sensors data
  delay(5);

  // Initialize the averages to the default values
  
  for(sensor = 0; sensor < LASTSENSOR; sensor++)
  {
    avg[sensor] = ((int32_t)pgm_read_word(&default_offsets[sensor])) << SENSORS_CALIBRATE_AVG_SHIFT;
  }
  
  // Loop while the the sensors have not been stable for at least some time
  
  for (stable_cycles = 0; stable_cycles < SENSORS_CALIBRATE_MIN_STABLE_CYCLES; stable_cycles++)
  {
    Read_adc_raw();   // Read sensors
    
    for(sensor = 0; sensor < LASTSENSOR; sensor++)
    {
      // Calculate the current reading's deviation from the average
      
      avg_diff = AN[sensor] - (int16_t)(avg[sensor] >> SENSORS_CALIBRATE_AVG_SHIFT);
      
      // Update moving average (single-term IIR LPF)
      
      avg[sensor] += avg_diff;

      // To be considered stable, the current reading must not deviate from average
      // too much. It also must not deviate from the nominal offset too much.
      // TODO: we should really use the configured offsets, not the default ones.
      // TODO: However currently this would hang the code here in an infinite loop
      // TODO: before we get the chance to set the EEPROM.

      offset_diff = AN[sensor] - (int16_t)pgm_read_word(&default_offsets[sensor]);
      
      if ((avg_diff    > (int16_t) SENSORS_CALIBRATE_AVG_DEV_MAX)     ||
          (avg_diff    < (int16_t)-SENSORS_CALIBRATE_AVG_DEV_MAX)     ||
          (offset_diff > (int16_t) SENSORS_CALIBRATE_OFFSET_DEV_MAX)  ||
          (offset_diff < (int16_t)-SENSORS_CALIBRATE_OFFSET_DEV_MAX))
      {
        // Not stable.  Reset the stable cycles counter.
        
        stable_cycles = 0;
      }
    }

    #if LOG_SEN
    Log_Write_Sensor(AN[0], AN[1], AN[2], AN[3], AN[4], AN[5], 0);
    #endif

    // We sample at 200Hz.  Since it's done before we get into the main loop,
    // we just delay here.
    
    delay(5);

    // Update the running lights display:
    // Fast running for unstable, slow running for stable, waiting to finish
    
    RunningLights(led_cycle);
    led_delay++;
    if (stable_cycles > 0)
    {
      if (led_delay >= SENSORS_CALIBRATE_LED_STABLE_SLOWDOWN)
      {
        led_delay = 0;
      led_cycle++;
      }
    }
    else if (++led_delay >= SENSORS_CALIBRATE_LED_UNSTABLE_SLOWDOWN)
    {
      led_delay = 0;
      led_cycle++;
    };
    if (led_cycle > RUNNING_LIGHTS_LED_STEP_MAX)
      led_cycle = 0;
  }

  // At this point all sensors are stable.
  // Switch off all ABC lights
  
  LightsOff();

  // Store the gyro averages as the updated offset values.
  
  for (sensor = GYROZ; sensor <= GYROY; sensor++)  
  {
    AN_OFFSET[sensor] = avg[sensor] >> SENSORS_CALIBRATE_AVG_SHIFT;
  }

  // Do NOT store the accelerometer averages, the aircraft is not guaranteed
  // to be level now.  Instead, just use the configured offsets from EEPROM
  
  AN_OFFSET[ACCELX] = acc_offset_x;
  AN_OFFSET[ACCELY] = acc_offset_y;
  AN_OFFSET[ACCELZ] = acc_offset_z;
}


#ifdef UseBMP
void read_baro(void)
{
  float tempPresAlt;
  
  tempPresAlt = float(APM_BMP085.Press)/101325.0;
  //tempPresAlt = pow(tempPresAlt, 0.190284);
  //press_alt = (1.0 - tempPresAlt) * 145366.45;
  tempPresAlt = pow(tempPresAlt, 0.190295);
  if (press_baro_altitude == 0)
    press_baro_altitude = (1.0 - tempPresAlt) * 4433000;      // Altitude in cm
  else
    press_baro_altitude = press_baro_altitude * 0.75 + ((1.0 - tempPresAlt) * 4433000)*0.25;  // Altitude in cm (filtered)
}
#endif

#ifdef IsSONAR
/* This function reads in the values from the attached range finders (currently only downward pointing sonar) */
void read_Sonar()
{ 
  // calculate altitude from down sensor
  AP_RangeFinder_down.read();
  
  // translate into an altitude
  press_sonar_altitude = DCM_Matrix[2][2] * AP_RangeFinder_down.distance;
  
  // deal with the unusual case that we're up-side-down
  if( press_sonar_altitude < 0 )
    press_sonar_altitude = 0;  
  
  // set sonar status to OK and update sonar_valid_count which shows reliability of sonar (i.e. are we out of range?)
  if( AP_RangeFinder_down.distance > sonar_threshold ) {
      sonar_status = SONAR_STATUS_BAD;
      if( sonar_valid_count > 0 )
          sonar_valid_count = -1;
      else
          sonar_valid_count--;
  }else{
      sonar_status = SONAR_STATUS_OK;
      if( sonar_valid_count < 0 )
          sonar_valid_count = 1;
      else
          sonar_valid_count++;
  }
  sonar_valid_count = constrain(sonar_valid_count,-10,10);

#if (LOG_RANGEFINDER && !defined(IsRANGEFINDER))
    Log_Write_RangeFinder(AP_RangeFinder_down.distance,0,0,0,0,0);
#endif
}
#endif  // IsSONAR

#ifdef IsRANGEFINDER
/* This function reads in the values from the attached range finders (currently only downward pointing sonar) */
void read_RF_Sensors()
{
  AP_RangeFinder_frontRight.read();
  AP_RangeFinder_backRight.read();
  AP_RangeFinder_backLeft.read();
  AP_RangeFinder_frontLeft.read();

#if LOG_RANGEFINDER 
    Log_Write_RangeFinder(AP_RangeFinder_down.distance, AP_RangeFinder_frontRight.distance, AP_RangeFinder_backRight.distance, AP_RangeFinder_backLeft.distance,AP_RangeFinder_frontLeft.distance,0);
#endif
}
#endif  // IsRANGEFINDER

