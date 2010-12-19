/*
 ArduPirates v1.7 - December 2010
 http://code.google.com/p/ardupirates/
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


// Pins used for motormount LEDs (lyagukh@gmail.com, 20101121)

#define  MM_LED1 58  // AN4
#define  MM_LED2 59  // AN5

long mm_led1_timer;  // time (in milliseconds) of the last blink
int  mm_led1_speed;  // milliseconds between blinks
byte mm_led1_status; // current status - LOW or HIGH

long mm_led2_timer;  // time (in milliseconds) of the last blink
int  mm_led2_speed;  // milliseconds between blinks
byte mm_led2_status; // current status - LOW or HIGH


/* ************************************************************ * 
   void MotorMount_Leds_Init();
     Desc: Initializes MotorMount leds
 * ************************************************************ */
void MotorMount_Leds_Init()
{
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
}



/* ************************************************************ * 
   void Show_Leds();
     Desc: Shows leds status
 * ************************************************************ */
void Show_Leds()
{
  // AM and Mode status LED lights
  if(millis() - gled_timer > gled_speed) 
  {
    gled_timer = millis();
    if(gled_status == HIGH) 
    { 
      digitalWrite(LED_Green, LOW);
      #ifdef IsAM      
        digitalWrite(RE_LED, LOW);
      #endif
      gled_status = LOW;
    } 
    else 
    {
      digitalWrite(LED_Green, HIGH);
      #ifdef IsAM
        if(motorArmed) 
          digitalWrite(RE_LED, HIGH);
      #endif
      gled_status = HIGH;
    } 
  }
}

 
void  Show_MotorMount_Leds(byte APmode)
{
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
    if( !motorArmed ) 
    {
      mm_led1_speed = LED_SPEED_OFF;         // Off
    } 
    else 
    {
      switch( APmode ) 
      {
//        case F_MODE_ACROBATIC:
//          mm_led1_speed = LED_SPEED_FAST;    // Rapid blink
//          break;
        case F_MODE_STABLE:
          mm_led1_speed = LED_SPEED_SLOW;   // Slow blink
          break;
        case F_MODE_SUPER_STABLE:
        case F_MODE_ABS_HOLD:
          mm_led1_speed = 0;      // On
          break;
        default:  // should not really happen
          mm_led1_speed = LED_SPEED_OFF;     // Off
      }
    }
    mm_led2_speed = LED_SPEED_OFF;           // Off
    #ifdef IsGPS
      if( GPS.Fix ) 
      {
        switch( APmode ) 
        {
//          case F_MODE_ACROBATIC:
          case F_MODE_SUPER_STABLE:
            mm_led2_speed = LED_SPEED_FAST; // Rapid blink
            break;
          case F_MODE_STABLE:
            mm_led2_speed = LED_SPEED_SLOW; // Slow blink
            break;
          case F_MODE_ABS_HOLD:
            mm_led2_speed = LED_SPEED_ON;         // On
            break;
          default:  // should not really happen
            mm_led2_speed = LED_SPEED_OFF;        // Off
        }
      }
    #endif

    if( mm_led1_speed == LED_SPEED_OFF ) 
    {
      digitalWrite( MM_LED1, LOW );
      mm_led1_status = LOW;
    } 
    else if( mm_led1_speed == LED_SPEED_ON ) 
    {
      digitalWrite( MM_LED1, HIGH );
      mm_led1_status = HIGH;
    } 
    else if(millis() - mm_led1_timer > mm_led1_speed) 
    {
      mm_led1_timer = millis();
      if(mm_led1_status == HIGH) 
      { 
        digitalWrite(MM_LED1, LOW);
        mm_led1_status = LOW;
      } 
      else 
      {
        digitalWrite(MM_LED1, HIGH);
        mm_led1_status = HIGH;
      } 
    }

    if( mm_led2_speed == LED_SPEED_OFF ) 
    {
      digitalWrite( MM_LED2, LOW );
      mm_led2_status = LOW;
    } 
    else if( mm_led2_speed == LED_SPEED_OFF ) 
    {
      digitalWrite( MM_LED2, HIGH );
      mm_led2_status = HIGH;
    } 
    else if(millis() - mm_led2_timer > mm_led2_speed) 
    {
      mm_led2_timer = millis();
      if(mm_led2_status == HIGH) 
      { 
        digitalWrite(MM_LED2, LOW);
        mm_led2_status = LOW;
      } 
      else 
      {
        digitalWrite(MM_LED2, HIGH);
        mm_led2_status = HIGH;
      } 
    }
  #endif
}