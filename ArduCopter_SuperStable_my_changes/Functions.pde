/*
   ArduCopter 1.3 - August 2010
   www.ArduCopter.com
   Copyright (c) 2010. All rights reserved.
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

void RadioCalibration() {
  long command_timer;
  int command; 
  int counter = 5;
  boolean Cmd_ok; 
  long roll_new = 0;
  long pitch_new = 0;
  long yaw_new = 0;

  SerFlu();
  SerPriln("Entering Radio Calibration mode");
  SerPriln("Current channel MID values are:");
  SerPri("ROLL: ");
  SerPri(roll_mid);
  SerPri(" PITCH: ");
  SerPri(pitch_mid);
  SerPri(" YAW: ");
  SerPri(yaw_mid);
  SerPriln();
  SerPriln("Recalibrate Channel MID's [Y/N]?: ");
  command_timer = millis();

  // Start counter loop and wait serial input. If not input for 5 seconds, return to normal mode
  while(millis() - command_timer < 5000) {
    if (SerAva()) {
      queryType = SerRea();
      if(queryType == 'y' || queryType == 'Y') {  
        Cmd_ok = TRUE;
        break;    
      } 
      else {
        Cmd_ok = FALSE;     
        break;
      }
    }
  }
  if(Cmd_ok) {
    // We have a go. Let's do new calibration
    SerPriln("Starting calibration run in 5 seconds. Place all sticks to their middle including trims");
    for(counter = 5; counter >= 0; counter --) {
      command_timer = millis();
      while(millis() - command_timer < 1000) {
      }
      SerPriln(counter);
    }
    // Do actual calibration now
    SerPriln("Measuring average channel values");
    SerPriln("ROLL, PITCH, YAW");

    counter = 0; // Reset counter for just in case. 
    command_timer = millis();
    while(millis() - command_timer < 1000) {

      if (APM_RC.GetState()==1) {  // New radio frame?
        // Commands from radio Rx... 
        ch_roll = channel_filter(APM_RC.InputCh(0), ch_roll);
        ch_pitch = channel_filter(APM_RC.InputCh(1), ch_pitch);
        ch_throttle = channel_filter(APM_RC.InputCh(2), ch_throttle);
        ch_yaw = channel_filter(APM_RC.InputCh(3), ch_yaw);
        ch_aux = APM_RC.InputCh(4);
        ch_aux2 = APM_RC.InputCh(5);

        SerPri(ch_roll);
        comma();
        SerPri(ch_pitch);
        comma();
        SerPri(ch_yaw);
        SerPriln();
        roll_new += ch_roll;
        pitch_new += ch_pitch; 
        yaw_new += ch_yaw;
        counter++;
      }
    }
    SerPri("New samples received: ");
    SerPriln(counter);    
    roll_new = roll_new / counter;
    pitch_new = pitch_new / counter;
    yaw_new = yaw_new / counter;
    SerPri("New values as: ");
    SerPri("ROLL: ");
    SerPri(roll_new);
    SerPri(" PITCH: ");
    SerPri(pitch_new);
    SerPri(" YAW: ");
    SerPri(yaw_new);
    SerPriln();
    SerPriln("Accept & Save values [Y/N]?: ");
    Cmd_ok = FALSE;
    while(millis() - command_timer < 5000) {
      if (SerAva()) {
        queryType = SerRea();
        if(queryType == 'y' || queryType == 'Y') { 
          Cmd_ok = TRUE;
          roll_mid = roll_new;
          pitch_mid = pitch_new;
          yaw_mid = yaw_new;
          SerPriln("Values accepted, remember to save them to EEPROM with 'W' command");
          break;    
        } 
        else {
          Cmd_ok = TRUE;
          break;
        }
      }   
    } 
  } 
  if(queryType == 'n' || queryType == 'N') Cmd_ok = TRUE;
  if(Cmd_ok)
    SerPriln("Returning normal mode...");
  else SerPriln("Command timeout, returning normal mode....");
}

void comma() {
  SerPri(',');
}

/* ***************************************************************************** */
// STABLE MODE
// PI absolute angle control driving a P rate control
// Input : desired Roll, Pitch and Yaw absolute angles. Output : Motor commands
void Attitude_control_v3()
{
  #define MAX_CONTROL_OUTPUT 250
  float stable_roll,stable_pitch,stable_yaw;
  
  // ROLL CONTROL    
  if (AP_mode==2)        // Normal Mode => Stabilization mode
    err_roll = command_rx_roll - ToDeg(roll);
  else
    err_roll = (command_rx_roll + command_gps_roll) - ToDeg(roll);  // Position control  
  err_roll = constrain(err_roll,-25,25);  // to limit max roll command...
  
  roll_I += err_roll*G_Dt;
  roll_I = constrain(roll_I,-20,20);

  // PID absolute angle control
  K_aux = KP_QUAD_ROLL; // Comment this out if you want to use transmitter to adjust gain
  stable_roll = K_aux*err_roll + KI_QUAD_ROLL*roll_I;
  
  // PD rate control (we use also the bias corrected gyro rates)
  err_roll = stable_roll - ToDeg(Omega[0]); // Omega[] is the raw gyro reading plus Omega_I, so it´s bias corrected
  control_roll = STABLE_MODE_KP_RATE_ROLL*err_roll;
  control_roll = constrain(control_roll,-MAX_CONTROL_OUTPUT,MAX_CONTROL_OUTPUT);

  // PITCH CONTROL
  if (AP_mode==2)        // Normal mode => Stabilization mode
    err_pitch = command_rx_pitch - ToDeg(pitch);
  else                   // GPS Position hold
    err_pitch = (command_rx_pitch + command_gps_pitch) - ToDeg(pitch);  // Position Control
  err_pitch = constrain(err_pitch,-25,25);  // to limit max pitch command...
  
  pitch_I += err_pitch*G_Dt;
  pitch_I = constrain(pitch_I,-20,20);
 
  // PID absolute angle control
  K_aux = KP_QUAD_PITCH; // Comment this out if you want to use transmitter to adjust gain
  stable_pitch = K_aux*err_pitch + KI_QUAD_PITCH*pitch_I;
  
  // P rate control (we use also the bias corrected gyro rates)
  err_pitch = stable_pitch - ToDeg(Omega[1]);
  control_pitch = STABLE_MODE_KP_RATE_PITCH*err_pitch;
  control_pitch = constrain(control_pitch,-MAX_CONTROL_OUTPUT,MAX_CONTROL_OUTPUT);
  
  // YAW CONTROL
  err_yaw = command_rx_yaw - ToDeg(yaw);
  if (err_yaw > 180)    // Normalize to -180,180
    err_yaw -= 360;
  else if(err_yaw < -180)
    err_yaw += 360;
  err_yaw = constrain(err_yaw,-60,60);  // to limit max yaw command...
  
  yaw_I += err_yaw*G_Dt;
  yaw_I = constrain(yaw_I,-20,20);
 
  // PID absoulte angle control
  stable_yaw = KP_QUAD_YAW*err_yaw + KI_QUAD_YAW*yaw_I;
  // PD rate control (we use also the bias corrected gyro rates)
  err_yaw = stable_yaw - ToDeg(Omega[2]);
  control_yaw = STABLE_MODE_KP_RATE_YAW*err_yaw;
  control_yaw = constrain(control_yaw,-MAX_CONTROL_OUTPUT,MAX_CONTROL_OUTPUT);
}

// ACRO MODE
void Rate_control()
{
  static float previousRollRate, previousPitchRate, previousYawRate;
  float currentRollRate, currentPitchRate, currentYawRate;

  // ROLL CONTROL
  currentRollRate = read_adc(0);      // I need a positive sign here

  err_roll = ((ch_roll - roll_mid) * xmitFactor) - currentRollRate;

  roll_I += err_roll * G_Dt;
  roll_I = constrain(roll_I, -20, 20);

  roll_D = currentRollRate - previousRollRate;
  previousRollRate = currentRollRate;

  // PID control
  control_roll = Kp_RateRoll * err_roll + Kd_RateRoll * roll_D + Ki_RateRoll * roll_I; 

  // PITCH CONTROL
  currentPitchRate = read_adc(1);
  err_pitch = ((ch_pitch - pitch_mid) * xmitFactor) - currentPitchRate;

  pitch_I += err_pitch*G_Dt;
  pitch_I = constrain(pitch_I,-20,20);

  pitch_D = currentPitchRate - previousPitchRate;
  previousPitchRate = currentPitchRate;

  // PID control
  control_pitch = Kp_RatePitch*err_pitch + Kd_RatePitch*pitch_D + Ki_RatePitch*pitch_I; 

  // YAW CONTROL
  currentYawRate = read_adc(2);
  err_yaw = ((ch_yaw - yaw_mid) * xmitFactor) - currentYawRate;

  yaw_I += err_yaw*G_Dt;
  yaw_I = constrain(yaw_I, -20, 20);

  yaw_D = currentYawRate - previousYawRate;
  previousYawRate = currentYawRate;

  // PID control
  K_aux = KP_QUAD_YAW; // Comment this out if you want to use transmitter to adjust gain
  control_yaw = Kp_RateYaw*err_yaw + Kd_RateYaw*yaw_D + Ki_RateYaw*yaw_I; 
}

// RATE CONTROL MODE
// Using Omega vector (bias corrected gyro rate)
void Rate_control_v2()
{
  static float previousRollRate, previousPitchRate, previousYawRate;
  float currentRollRate, currentPitchRate, currentYawRate;
  
  // ROLL CONTROL
  currentRollRate = ToDeg(Omega[0]);  // Omega[] is the raw gyro reading plus Omega_I, so it´s bias corrected
  
  err_roll = ((ch_roll- roll_mid) * xmitFactor) - currentRollRate;
  
  roll_I += err_roll*G_Dt;
  roll_I = constrain(roll_I,-20,20);

  roll_D = (currentRollRate - previousRollRate)/G_Dt;
  previousRollRate = currentRollRate;
  
  // PID control
  control_roll = Kp_RateRoll*err_roll + Kd_RateRoll*roll_D + Ki_RateRoll*roll_I; 
  
  // PITCH CONTROL
  currentPitchRate = ToDeg(Omega[1]);  // Omega[] is the raw gyro reading plus Omega_I, so it´s bias corrected
  err_pitch = ((ch_pitch - pitch_mid) * xmitFactor) - currentPitchRate;
  
  pitch_I += err_pitch*G_Dt;
  pitch_I = constrain(pitch_I,-20,20);

  pitch_D = (currentPitchRate - previousPitchRate)/G_Dt;
  previousPitchRate = currentPitchRate;
 
  // PID control
  control_pitch = Kp_RatePitch*err_pitch + Kd_RatePitch*pitch_D + Ki_RatePitch*pitch_I; 
  
  // YAW CONTROL
  currentYawRate = ToDeg(Omega[2]);  // Omega[] is the raw gyro reading plus Omega_I, so it´s bias corrected;
  err_yaw = ((ch_yaw - yaw_mid)* xmitFactor) - currentYawRate;
  
  yaw_I += err_yaw*G_Dt;
  yaw_I = constrain(yaw_I,-20,20);

  yaw_D = (currentYawRate - previousYawRate)/G_Dt;
  previousYawRate = currentYawRate;
 
  // PID control
  K_aux = KP_QUAD_YAW; // Comment this out if you want to use transmitter to adjust gain
  control_yaw = Kp_RateYaw*err_yaw + Kd_RateYaw*yaw_D + Ki_RateYaw*yaw_I; 
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
} 

// BMP slope filter for readings... (limit max differences between readings)
float BMP_filter(float BMP_reading, float BMP_reading_old)
{
  float diff_BMP_reading_old;

  if (BMP_reading_old == 0)      // BMP_reading_old not initialized
    return(BMP_reading);
    diff_BMP_reading_old = BMP_reading - BMP_reading_old;      // Difference with old reading
  if (diff_BMP_reading_old < 0)
  {
    if (diff_BMP_reading_old <- 5)
      return(BMP_reading_old - 5);        // We limit the max difference between readings
  }
  else
  {
    if (diff_BMP_reading_old > 5)    
      return(BMP_reading_old + 5);
  }
  return((BMP_reading + BMP_reading_old ) / 2);   // Small filtering
}

