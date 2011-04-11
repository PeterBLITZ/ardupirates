/*
 www.ArduCopter.com - www.DIYDrones.com
 Copyright (c) 2010.  All rights reserved.
 An Open Source Arduino based multicopter.
 
      ___          _      ______ _           _
     / _ \        | |     | ___ (_)         | |
    / /_\ \_ __ __| |_   _| |_/ /_ _ __ __ _| |_ ___  ___
    |  _  | '__/ _` | | | |  __/| | '__/ _` | __/ _ \/ __|
    | | | | | | (_| | |_| | |   | | | | (_| | ||  __/\__ \
    \_| |_/_|  \__,_|\__,_\_|   |_|_|  \__,_|\__\___||___/

 File     : Radio.pde
 Version  : v1.0, Aug 27, 2010
 Author(s): ArduCopter Team
             Ted Carancho (aeroquad), Jose Julio, Jordi Muñoz,
             Jani Hirvinen, Ken McEwans, Roberto Navoni,          
             Sandro Benigno, Chris Anderson
Author(s):  ArduPirates deveopment team
             Philipp Maloney, Norbert, Hein, Igor, Emile, Kim
 
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


* ************************************************************** *
TODO:


* ************************************************************** */

#define STICK_TO_ANGLE_FACTOR 12.0        // To convert stick position to absolute angles
#define YAW_STICK_TO_ANGLE_FACTOR 150.0

void read_radio()
{
    int tempThrottle = 0;
  
    // Commands from radio Rx
    // We apply the Radio calibration parameters (from configurator) except for throttle
    ch_roll = channel_filter(APM_RC.InputCh(CH_ROLL) * ch_roll_slope + ch_roll_offset, ch_roll);
    ch_pitch = channel_filter(APM_RC.InputCh(CH_PITCH) * ch_pitch_slope + ch_pitch_offset, ch_pitch);
    ch_yaw = channel_filter(APM_RC.InputCh(CH_RUDDER) * ch_yaw_slope + ch_yaw_offset, ch_yaw);
    ch_aux = APM_RC.InputCh(CH_5) * ch_aux_slope + ch_aux_offset;
    ch_aux2 = APM_RC.InputCh(CH_6) * ch_aux2_slope + ch_aux2_offset;   //This is the MODE Channel in Configurator.
//  Use this channel if you have a 7 or more Channel Radio.
//  Can be used for PID tuning (see FUNCTIONS) or Camera 3 position tilt (pitch).
#if (defined(SerXbee) && defined(Use_PID_Tuning))
    ch_flightmode = APM_RC.InputCh(CH_7);  // flight mode 3-position switch.
#endif    
    // special checks for throttle
    tempThrottle = APM_RC.InputCh(CH_THROTTLE);

    // throttle safety check
    if( motorSafety == 1 ) {
        if( motorArmed == 1 ) {
            if( ch_throttle > MIN_THROTTLE + 100 ) { // if throttle is now over MIN..
                // if throttle has increased suddenly, disarm motors 
                if((tempThrottle - ch_throttle) > SAFETY_MAX_THROTTLE_INCREASE && safetyOff == 0) {     
                    motorArmed = 0;
                }else if (tempThrottle > SAFETY_HOVER_THROTTLE){ // if it hasn't increased too quickly turn off the safety
                    motorSafety = 0;
                    safetyOff = 1;                               // throttle is over Safety Hover Throttle we switch off Safety.
                }else{  
                    motorSafety = 0;
                }
            }
        }
    }else if( ch_throttle < MIN_THROTTLE + 100 ) { // Safety logic: hold throttle low for more than 1 second, safety comes on which stops sudden increases in throttle
        Safety_counter++;
        if( Safety_counter > SAFETY_DELAY ) {
            motorSafety = 1;
            Safety_counter = 0;
        }
     }else {
       Safety_counter = 0;
    }   
    if(motorSafety == 0 && tempThrottle > SAFETY_HOVER_THROTTLE){  // throttle is over MIN so make sure to reset Safety_counter
       Safety_counter = 0;
       safetyOff = 1;                                              // throttle is over Safety Hover Throttle we switch off Safety.
    }   
   
    // normal throttle filtering.  Note: Transmiter calibration not used on throttle
    ch_throttle = channel_filter(tempThrottle, ch_throttle);
        
// FLIGHT MODE
//  This is determine by DIP Switch 3. // When switching over you have to reboot APM.
// DIP3 down (On)  = Acrobatic Mode.  Yellow LED is Flashing. 
// DIP3 up   (Off) = Stable Mode.  AUTOPILOT MODE LEDs status lights become applicable.  See below.

    // Autopilot mode (only works on Stable mode)
    if (flightMode == FM_STABLE_MODE)
    {
      if (ch_aux2 > 1800 && ch_aux > 1800)
      {
        AP_mode = AP_NORMAL_STABLE_MODE  ;      // Stable mode (Heading Hold only)
        digitalWrite(LED_Yellow,LOW);           // Yellow LED OFF : Alititude Hold OFF
        digitalWrite(LED_Red,LOW);              // Red LED OFF : GPS Position Hold OFF
      }
      else if (ch_aux2 > 1800 && ch_aux < 1250)
      {
        AP_mode = AP_ALTITUDE_HOLD;             // Super Stable Mode (Altitude hold mode)
        digitalWrite(LED_Yellow,HIGH);          // Yellow LED ON : Alititude Hold ON
        digitalWrite(LED_Red,LOW);              // Red LED OFF : GPS Position Hold OFF
      }
      else if (ch_aux2 < 1250 && ch_aux > 1800)
      {
        AP_mode = AP_GPS_HOLD;                  // Position Hold (GPS position control)
        digitalWrite(LED_Yellow,LOW);           // Yellow LED OFF : Alititude Hold OFF
       #ifdef isGPS
        if (gps.fix > 0)
          digitalWrite(LED_Red,HIGH);           // Red LED ON : GPS Position Hold ON
       #endif
      }
      else 
      {
        AP_mode = AP_ALT_GPS_HOLD;              //Position & Altitude hold mode (GPS position control & Altitude control)
        digitalWrite(LED_Yellow,HIGH);          // Yellow LED ON : Alititude Hold ON
        #ifdef isGPS
       
        if (gps.fix > 0)
          digitalWrite(LED_Red,HIGH);           // Red LED ON : GPS Position Hold ON
        #endif 
      }
    } 

/*
    if (flightMode == STABLE_MODE)
      {
      if(ch_aux < 1300)
        AP_mode = AP_AUTOMATIC_MODE;   // Automatic mode : GPS position hold mode + altitude hold
      else 
        AP_mode = AP_NORMAL_MODE;   // Normal mode
      }
*/      
    if (flightMode == FM_STABLE_MODE)  // IN STABLE MODE we convert stick positions to absolute angles
      {
      // In Stable mode stick position defines the desired angle in roll, pitch and yaw
#if AIRFRAME == QUAD
#ifndef FLIGHT_MODE_X
      if(flightOrientation) {
        //FLIGHT_MODE_X_45Degree
        // For X mode - (APM-front pointing towards front motor)
        float aux_roll = (ch_roll-roll_mid) / STICK_TO_ANGLE_FACTOR;
        float aux_pitch = (ch_pitch-pitch_mid) / STICK_TO_ANGLE_FACTOR;
        command_rx_roll = aux_roll - aux_pitch;
        command_rx_pitch = aux_roll + aux_pitch;
        // For X mode - APM front between front and right motor 
      } 
      else 
      {
        command_rx_roll = (ch_roll-roll_mid) / STICK_TO_ANGLE_FACTOR;       // Convert stick position to absolute angles
        command_rx_pitch = (ch_pitch-pitch_mid) / STICK_TO_ANGLE_FACTOR;
      }
#endif
#ifdef FLIGHT_MODE_X
        command_rx_roll = (ch_roll-roll_mid) / STICK_TO_ANGLE_FACTOR;       // Convert stick position to absolute angles
        command_rx_pitch = (ch_pitch-pitch_mid) / STICK_TO_ANGLE_FACTOR;
#endif
#endif

#if ((AIRFRAME == HEXA) || (AIRFRAME == OCTA))     
        command_rx_roll = (ch_roll-roll_mid) / STICK_TO_ANGLE_FACTOR;       // Convert stick position to absolute angles
        command_rx_pitch = (ch_pitch-pitch_mid) / STICK_TO_ANGLE_FACTOR;
#endif  
      // YAW
      if (abs(ch_yaw-yaw_mid)>6)   // Take into account a bit of "dead zone" on yaw
        {
        command_rx_yaw += (ch_yaw-yaw_mid) / YAW_STICK_TO_ANGLE_FACTOR;
        command_rx_yaw = Normalize_angle(command_rx_yaw);    // Normalize angle to [-180,180]
        }
      }
    
    // Write Radio data to DataFlash log
    #if LOG_RADIO
    Log_Write_Radio(ch_roll,ch_pitch,ch_throttle,ch_yaw,ch_aux,ch_aux2);
    #endif
    
    // Motor arm logic
    if (ch_throttle < (MIN_THROTTLE + 100)) {
      control_yaw = 0;
      command_rx_yaw = ToDeg(yaw);
      
      // Arm motor output : Throttle down and full yaw right for more than 2 seconds
      if (ch_yaw > 1850) {
        if (Arming_counter > ARM_DELAY){
          if(ch_throttle > 800) {
          motorArmed = 1;
          minThrottle = MIN_THROTTLE+60;  // A minimun value for mantain a bit if throttle
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
          safetyOff = 0;
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
}


