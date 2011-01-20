/*
 www.ArduCopter.com - www.DIYDrones.com
 Copyright (c) 2010.  All rights reserved.
 An Open Source Arduino based multicopter.
 
 File     : Functions.pde
 Version  : v1.0, Aug 28, 2010
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
 30-10-10 added basic camera stabilization functions with jumptables
 
 * ************************************************************** *
 TODO:
 
 
 * ************************************************************** */


// Flash those A,B,C LEDs on IMU Board
// 
// Function: FullBlink(int, int);
//           int 1 = 
void FullBlink(int count, int blinkdelay) {
  for(int i = 0; i <= count; i++) {
    digitalWrite(LED_Green, HIGH);
    digitalWrite(LED_Yellow, HIGH);
    digitalWrite(LED_Red, HIGH);
    delay(blinkdelay);
    digitalWrite(LED_Green, LOW);
    digitalWrite(LED_Yellow, LOW);
    digitalWrite(LED_Red, LOW);
    delay(blinkdelay);
  }
}


void RunningLights(int LightStep) {

  if(LightStep == 0) {
    digitalWrite(LED_Green, HIGH);
    digitalWrite(LED_Yellow, LOW);
    digitalWrite(LED_Red, LOW);
  } 
  else if (LightStep == 1) {
    digitalWrite(LED_Green, LOW);
    digitalWrite(LED_Yellow, HIGH);
    digitalWrite(LED_Red, LOW);
  } 
  else {
    digitalWrite(LED_Green, LOW);
    digitalWrite(LED_Yellow, LOW);
    digitalWrite(LED_Red, HIGH);
  }

}

void LightsOff() {
  digitalWrite(LED_Green, LOW);
  digitalWrite(LED_Yellow, LOW);
  digitalWrite(LED_Red, LOW);
}

// Funtion to normalize an angle in degrees to -180,180 degrees
float Normalize_angle(float angle)
{
  if (angle > 180)         
    return (angle - 360.0);
  else if (angle < -180)
    return (angle + 360.0);
  else
    return(angle);
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
  //return(ch);
}


// Special APM PinMode settings and others
void APMPinMode(volatile unsigned char &Port, byte Pin, boolean Set)
{
  if (Set)  {
    Port |=   (1 << Pin);
  } 
  else  {
    Port &=  ~(1 << Pin);
  }
}

boolean APMPinRead(volatile unsigned char &Port, byte Pin)
{
  if(Port   &   (1 << Pin))
    return 1;
  else
    return 0;
}

// Faster and smaller replacement for contrain() function
int limitRange(int data, int minLimit, int maxLimit) {
  if (data < minLimit) return minLimit;
  else if (data > maxLimit) return maxLimit;
  else return data;
}


// Stepping G, Y, R Leds
// Call CLILedStep(); to change led statuses
// Used on CLI as showing that we are in CLI mode
void CLILedStep () {
  
  switch(cli_step) {
  case 1:
        digitalWrite(LED_Green, HIGH);
        digitalWrite(LED_Yellow, LOW);
        digitalWrite(LED_Red, LOW);
  break;
  case 2:
        digitalWrite(LED_Green, LOW);
        digitalWrite(LED_Yellow, HIGH);
        digitalWrite(LED_Red, LOW);
  break;
  case 3:
        digitalWrite(LED_Green, LOW);
        digitalWrite(LED_Yellow, LOW);
        digitalWrite(LED_Red, HIGH);
  break;
  }
  cli_step ++; 
  if(cli_step == 4) cli_step = 1;  
  
}

void LEDAllON() {
        digitalWrite(LED_Green, HIGH);
        digitalWrite(LED_Red, HIGH);
        digitalWrite(LED_Yellow, HIGH);
}

void LEDAllOFF() {
        digitalWrite(LED_Green, LOW);
        digitalWrite(LED_Red, LOW);
        digitalWrite(LED_Yellow, LOW);
}


//
// Camera functions moved to event due it's and event 31-10-10, jp


//  Use this function for PID tuning using your flightmode 3-position switch on the radio.
//  You will need at least a 7 channel radio.  Radio must be in Acro (plane) mode.
//  Select only one set of parameters below.  See default selection below.
void PID_Tuning()  {
// Tuning PID values using only 3 position channel switch (Flight Mode).
     if (ch_flightmode >= 1800) 
     {
       Plus = 1;
       Minus = 0;
     } 
     else if (ch_flightmode <= 1200) 
     {
          Plus = 0;
          Minus = 1;
     } 
     else if (ch_flightmode >= 1400 && ch_flightmode <= 1600) 
     {
             if (Plus == 1){
//                KP_GPS_ROLL += 0.001;
//                writeEEPROM(KP_GPS_ROLL, KP_GPS_ROLL_ADR);

//                KP_GPS_PITCH += 0.001;
//                writeEEPROM(KP_GPS_PITCH, KP_GPS_PITCH_ADR);

//                KI_GPS_ROLL += 0.0001;
//                writeEEPROM(KI_GPS_ROLL, KI_GPS_ROLL_ADR);

//                KI_GPS_PITCH += 0.0001;
//                writeEEPROM(KI_GPS_PITCH, KI_GPS_PITCH_ADR);

//                KP_QUAD_YAW += 0.1;
//                writeEEPROM(KP_QUAD_YAW, KP_QUAD_YAW_ADR);

//                KI_QUAD_YAW += 0.01;
//                writeEEPROM(KI_QUAD_YAW, KI_QUAD_YAW_ADR);

//                  CAM_FOCUs += 10; 

//                STABLE_MODE_KP_RATE += 0.05;                                        // default
//                writeEEPROM(STABLE_MODE_KP_RATE, STABLE_MODE_KP_RATE_ADR);          // default

//                STABLE_MODE_KP_RATE_YAW += 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_YAW, STABLE_MODE_KP_RATE_YAW_ADR);

//                STABLE_MODE_KP_RATE_ROLL += 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_ROLL, STABLE_MODE_KP_RATE_ROLL_ADR);

//                STABLE_MODE_KP_RATE_PITCH += 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_PITCH, STABLE_MODE_KP_RATE_PITCH_ADR);

//                Kp_RateRoll += 0.1;
//                writeEEPROM(Kp_RateRoll, KP_RATEROLL_ADR);

//                Kp_RatePitch += 0.1;
//                writeEEPROM(Kp_RatePitch, KP_RATEPITCH_ADR);

//                KP_ALTITUDE += 0.1;
//                writeEEPROM(KP_ALTITUDE, KP_ALTITUDE_ADR);

//                KI_ALTITUDE += 0.3;
//                writeEEPROM(KI_ALTITUDE, KI_ALTITUDE_ADR);

//                KD_ALTITUDE += 0.3;
//                writeEEPROM(KD_ALTITUDE, KD_ALTITUDE_ADR);

//                Magoffset1 += 1;
//                writeEEPROM(Magoffset1_ADR);
//                APM_Compass.SetOffsets(Magoffset1);    

                Plus = 0;
                Minus = 0;
             } else if (Minus == 1) {
//                KP_GPS_ROLL -= 0.001;
//                writeEEPROM(KP_GPS_ROLL, KP_GPS_ROLL_ADR);

//                KP_GPS_PITCH -= 0.001;
//                writeEEPROM(KP_GPS_PITCH, KP_GPS_PITCH_ADR);

//                KI_GPS_ROLL -= 0.0001;
//                writeEEPROM(KI_GPS_ROLL, KI_GPS_ROLL_ADR);

//                KI_GPS_PITCH -= 0.0001;
//                writeEEPROM(KI_GPS_PITCH, KI_GPS_PITCH_ADR);

//                KP_QUAD_YAW -= 0.1;
//                writeEEPROM(KP_QUAD_YAW, KP_QUAD_YAW_ADR);

//                KI_QUAD_YAW -= 0.01;
//                writeEEPROM(KI_QUAD_YAW, KI_QUAD_YAW_ADR);

//                  CAM_FOCUs -= 10; 

//                STABLE_MODE_KP_RATE -= 0.05;                                        // default
//                writeEEPROM(STABLE_MODE_KP_RATE, STABLE_MODE_KP_RATE_ADR);          // default

//                STABLE_MODE_KP_RATE_YAW -= 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_YAW, STABLE_MODE_KP_RATE_YAW_ADR);

//                STABLE_MODE_KP_RATE_ROLL -= 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_ROLL, STABLE_MODE_KP_RATE_ROLL_ADR);

//                STABLE_MODE_KP_RATE_PITCH -= 0.1;
//                writeEEPROM(STABLE_MODE_KP_RATE_PITCH, STABLE_MODE_KP_RATE_PITCH_ADR);

//                Kp_RateRoll -= 0.1;
//                writeEEPROM(Kp_RateRoll, KP_RATEROLL_ADR);

//                Kp_RatePitch -= 0.1;
//                writeEEPROM(Kp_RatePitch, KP_RATEPITCH_ADR);

//                KP_ALTITUDE -= 0.1;
//                writeEEPROM(KP_ALTITUDE, KP_ALTITUDE_ADR);

//                KI_ALTITUDE -= 0.3;
//                writeEEPROM(KI_ALTITUDE, KI_ALTITUDE_ADR);

//                KD_ALTITUDE -= 0.3;
//                writeEEPROM(KD_ALTITUDE, KD_ALTITUDE_ADR);

//                Magoffset1 -= 1;
//                writeEEPROM(Magoffset1_ADR);
//                APM_Compass.SetOffsets(Magoffset1);    

                Plus = 0;
                Minus = 0;
             }
     }
}


