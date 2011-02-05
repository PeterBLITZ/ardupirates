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

 File     : GCS.pde
 Version  : v1.0, Aug 27, 2010
 Author(s): ArduCopter Team
			 Ted Carancho (aeroquad), Jose Julio, Jordi Muñoz,
			 Jani Hirvinen, Ken McEwans, Roberto Navoni,          
			 Sandro Benigno, Chris Anderson
 Author(s): ArduPirates deveopment team
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
//
// Function  : send_message()
//
// Parameters: 
//  byte severity   - Debug level
//  char str        - Text to write
//
// Returns   : - none

void send_message(byte severity, const char *str)		// This is the instance of send_message for message 0x05
{
  if(severity >= DEBUG_LEVEL){
    SerPri("MSG: ");
    SerPrln(str);
  }
}


////////////////////////////////////////////////// 
// Function  : readSerialCommand()
//
// Parameters: 
//     - none
//
// Returns   : - none
//
void readSerialCommand() {
  // Check for serial message
  if (SerAva()) {
    queryType = SerRea();
    switch (queryType) {
    case 'A': // Stable PID
      KP_QUAD_ROLL = readFloatSerial();
      KI_QUAD_ROLL = readFloatSerial();
      STABLE_MODE_KP_RATE_ROLL = readFloatSerial();
      KP_QUAD_PITCH = readFloatSerial();
      KI_QUAD_PITCH = readFloatSerial();
      STABLE_MODE_KP_RATE_PITCH = readFloatSerial();
      KP_QUAD_YAW = readFloatSerial();
      KI_QUAD_YAW = readFloatSerial();
      STABLE_MODE_KP_RATE_YAW = readFloatSerial();
      STABLE_MODE_KP_RATE = readFloatSerial();   // NOT USED NOW
      MAGNETOMETER = readFloatSerial();
      break;
    case 'C': // Receive GPS PID
      KP_GPS_ROLL = readFloatSerial();
      KI_GPS_ROLL = readFloatSerial();
      KD_GPS_ROLL = readFloatSerial();
      KP_GPS_PITCH = readFloatSerial();
      KI_GPS_PITCH = readFloatSerial();
      KD_GPS_PITCH = readFloatSerial();
      GPS_MAX_ANGLE = readFloatSerial();
      GEOG_CORRECTION_FACTOR = readFloatSerial();
      break;
    case 'E': // Receive altitude PID
      KP_ALTITUDE = readFloatSerial();
      KI_ALTITUDE = readFloatSerial();
      KD_ALTITUDE = readFloatSerial();
      break;
    case 'G': // Receive drift correction PID
      Kp_ROLLPITCH = readFloatSerial();
      Ki_ROLLPITCH = readFloatSerial();
      Kp_YAW = readFloatSerial();
      Ki_YAW = readFloatSerial();
      break;
    case 'I': // Receive sensor offset
      gyro_offset_roll = readFloatSerial();
      gyro_offset_pitch = readFloatSerial();
      gyro_offset_yaw = readFloatSerial();
      acc_offset_x = readFloatSerial();
      acc_offset_y = readFloatSerial();
      acc_offset_z = readFloatSerial();
      break;
    case 'K': // Camera mode
              // 1 = Tilt / Roll without 
      cam_mode = readFloatSerial();
      //BATTLOW = readFloatSerial();
      break;      
    case 'M': // Receive debug motor commands
      frontMotor = readFloatSerial();
      backMotor = readFloatSerial();
      rightMotor = readFloatSerial();
      leftMotor = readFloatSerial();
      motorArmed = readFloatSerial();
      break;     
    case 'O': // Rate Control PID
      Kp_RateRoll = readFloatSerial();
      Ki_RateRoll = readFloatSerial();
      Kd_RateRoll = readFloatSerial();
      Kp_RatePitch = readFloatSerial();
      Ki_RatePitch = readFloatSerial();
      Kd_RatePitch = readFloatSerial();
      Kp_RateYaw = readFloatSerial();
      Ki_RateYaw = readFloatSerial();
      Kd_RateYaw = readFloatSerial();
      xmitFactor = readFloatSerial();
      break;
    case 'W': // Write all user configurable values to EEPROM
      writeUserConfig();
      break;
    case 'Y': // Initialize EEPROM with default values
      defaultUserConfig();
#if AIRFRAME == HELI
      heli_defaultUserConfig();
#endif      
      break;
    case '1': // Receive transmitter calibration values
      ch_roll_slope = readFloatSerial();
      ch_roll_offset = readFloatSerial();
      ch_pitch_slope = readFloatSerial();
      ch_pitch_offset = readFloatSerial();
      ch_yaw_slope = readFloatSerial();
      ch_yaw_offset = readFloatSerial();
      ch_throttle_slope = readFloatSerial();
      ch_throttle_offset = readFloatSerial();
      ch_aux_slope = readFloatSerial();
      ch_aux_offset = readFloatSerial();
      ch_aux2_slope = readFloatSerial();
      ch_aux2_offset = readFloatSerial();
      break;
    case '5': // Special debug features


      break;    
    }
  }
}

void sendSerialTelemetry() {
  float aux_float[3]; // used for sensor calibration
  switch (queryType) {
  case '=': // Reserved debug command to view any variable from Serial Monitor
    /*    SerPri(("throttle =");
     SerPrln(ch_throttle);
     SerPri("control roll =");
     SerPrln(control_roll-CHANN_CENTER);
     SerPri("control pitch =");
     SerPrln(control_pitch-CHANN_CENTER);
     SerPri("control yaw =");
     SerPrln(control_yaw-CHANN_CENTER);
     SerPri("front left yaw =");
     SerPrln(frontMotor);
     SerPri("front right yaw =");
     SerPrln(rightMotor);
     SerPri("rear left yaw =");
     SerPrln(leftMotor);
     SerPri("rear right motor =");
     SerPrln(backMotor);
     SerPrln();
     
     SerPri("current roll rate =");
     SerPrln(read_adc(0));
     SerPri("current pitch rate =");
     SerPrln(read_adc(1));
     SerPri("current yaw rate =");
     SerPrln(read_adc(2));
     SerPri("command rx yaw =");
     SerPrln(command_rx_yaw);
     SerPrln(); 
     queryType = 'X';*/
    SerPri(APM_RC.InputCh(0));
    comma();
    SerPri(ch_roll_slope);
    comma();
    SerPri(ch_roll_offset);
    comma();
    SerPrln(ch_roll);
    break;
  case 'B': // Send roll, pitch and yaw PID values
    SerPri(KP_QUAD_ROLL, 3);
    comma();
    SerPri(KI_QUAD_ROLL, 3);
    comma();
    SerPri(STABLE_MODE_KP_RATE_ROLL, 3);
    comma();
    SerPri(KP_QUAD_PITCH, 3);
    comma();
    SerPri(KI_QUAD_PITCH, 3);
    comma();
    SerPri(STABLE_MODE_KP_RATE_PITCH, 3);
    comma();
    SerPri(KP_QUAD_YAW, 3);
    comma();
    SerPri(KI_QUAD_YAW, 3);
    comma();
    SerPri(STABLE_MODE_KP_RATE_YAW, 3);
    comma();
    SerPri(STABLE_MODE_KP_RATE, 3);    // NOT USED NOW
    comma();
    SerPrln(MAGNETOMETER, 3);
    queryType = 'X';
    break;
  case 'D': // Send GPS PID
    SerPri(KP_GPS_ROLL, 3);
    comma();
    SerPri(KI_GPS_ROLL, 3);
    comma();
    SerPri(KD_GPS_ROLL, 3);
    comma();
    SerPri(KP_GPS_PITCH, 3);
    comma();
    SerPri(KI_GPS_PITCH, 3);
    comma();
    SerPri(KD_GPS_PITCH, 3);
    comma();
    SerPri(GPS_MAX_ANGLE, 3);
    comma();
    SerPrln(GEOG_CORRECTION_FACTOR, 3);
    queryType = 'X';
    break;
  case 'F': // Send altitude PID
    SerPri(KP_ALTITUDE, 3);
    comma();
    SerPri(KI_ALTITUDE, 3);
    comma();
    SerPrln(KD_ALTITUDE, 3);
    queryType = 'X';
    break;
  case 'H': // Send drift correction PID
    SerPri(Kp_ROLLPITCH, 4);
    comma();
    SerPri(Ki_ROLLPITCH, 7);
    comma();
    SerPri(Kp_YAW, 4);
    comma();
    SerPrln(Ki_YAW, 6);
    queryType = 'X';
    break;
  case 'J': // Send sensor offset
    SerPri(gyro_offset_roll);
    comma();
    SerPri(gyro_offset_pitch);
    comma();
    SerPri(gyro_offset_yaw);
    comma();
    SerPri(acc_offset_x);
    comma();
    SerPri(acc_offset_y);
    comma();
    SerPrln(acc_offset_z);
    AN_OFFSET[3] = acc_offset_x;
    AN_OFFSET[4] = acc_offset_y;
    AN_OFFSET[5] = acc_offset_z;
    queryType = 'X';
    break;
  case 'L': // Camera settings and 
    SerPri(cam_mode, DEC);
    tab();
    SerPri(BATTLOW, DEC);
    tab();
    queryType = 'X';
    break;
  case 'N': // Send magnetometer config
    queryType = 'X';
    break;
  case 'P': // Send rate control PID
    SerPri(Kp_RateRoll, 3);
    comma();
    SerPri(Ki_RateRoll, 3);
    comma();
    SerPri(Kd_RateRoll, 3);
    comma();
    SerPri(Kp_RatePitch, 3);
    comma();
    SerPri(Ki_RatePitch, 3);
    comma();
    SerPri(Kd_RatePitch, 3);
    comma();
    SerPri(Kp_RateYaw, 3);
    comma();
    SerPri(Ki_RateYaw, 3);
    comma();
    SerPri(Kd_RateYaw, 3);
    comma();
    SerPrln(xmitFactor, 3);
    queryType = 'X';
    break;
  case 'Q': // Send sensor data
    SerPri(read_adc(0));
    comma();
    SerPri(read_adc(1));
    comma();
    SerPri(read_adc(2));
    comma();
    SerPri(read_adc(4));
    comma();
    SerPri(read_adc(3));
    comma();
    SerPri(read_adc(5));
    comma();
    SerPri(err_roll);
    comma();
    SerPri(err_pitch);
    comma();
    SerPri(degrees(roll));
    comma();
    SerPri(degrees(pitch));
    comma();
    SerPrln(degrees(yaw));
    break;
  case 'R': // Send raw sensor data
    break;
  case 'S': // Send all flight data
    SerPri(timer-timer_old);
    comma();
    SerPri(read_adc(0));
    comma();
    SerPri(read_adc(1));
    comma();
    SerPri(read_adc(2));
    comma();
    SerPri(ch_throttle);
    comma();
    SerPri(control_roll);
    comma();
    SerPri(control_pitch);
    comma();
    SerPri(control_yaw);
    comma();
    SerPri(frontMotor); // Front Motor
    comma();
    SerPri(backMotor); // Back Motor
    comma();
    SerPri(rightMotor); // Right Motor
    comma();
    SerPri(leftMotor); // Left Motor
    comma();
    SerPri(read_adc(4));
    comma();
    SerPri(read_adc(3));
    comma();
    SerPri(read_adc(5));
     comma();
     SerPri(AP_Compass.heading, 4);
     comma();
     SerPri(AP_Compass.heading_x, 4);
     comma();
     SerPri(AP_Compass.heading_y, 4);
     comma();
     SerPri(AP_Compass.mag_x);
     comma();    
     SerPri(AP_Compass.mag_y);
     comma();
     SerPri(AP_Compass.mag_z);
     comma();
     SerPri(press_baro_altitude);
	   comma();
     SerPriln();
    break;
  case 'T': // Spare
    if (AP_mode == AP_NORMAL_STABLE_MODE) 
      SerPriln("AP Mode = Normal Stable Mode");
    else if (AP_mode == AP_ALTITUDE_HOLD)
      SerPriln("AP Mode = Altitude Hold Mode");
    else if (AP_mode == AP_GPS_HOLD)
      SerPriln("AP Mode = GPS Hold Mode");
    else if (AP_mode == AP_ALT_GPS_HOLD)
      SerPriln("AP Mode = GPS & Altitude Hold Mode");
    if (flightMode == FM_STABLE_MODE)
      SerPriln("Flight Mode = Stable Mode");
    else if (flightMode == FM_ACRO_MODE)
      SerPriln("Flight Mode = Acrobatic Mode");
#if AIRFRAME == QUAD  
#ifdef FLIGHT_MODE_X_45Degree
    SerPri("Flight orientation: ");
    if(SW_DIP1) {
      SerPrln("x mode_45Degree (APM front pointing towards Front motor)");
    } 
    else {
      SerPrln("+ mode");
    }
#endif    
#ifdef FLIGHT_MODE_X
    SerPri("Flight orientation: ");
    SerPrln("x mode (APM front between Front and Right motor) DIP1 not applicable");
#endif
#endif
    #if AIRFRAME == QUAD  
      SerPriln("Airframe = Quad");
    #endif
    #if AIRFRAME == HEXA  
      SerPriln("Airframe = Hexa");
    #endif
    #if AIRFRAME == HELI  
      SerPriln("Airframe = Heli");
    #endif
    if (gps.new_data){
      SerPri("gps:");
      SerPri(" Lat:");
      SerPri((float)gps.latitude / 10000000, DEC);
      SerPri(" Lon:");
      SerPri((float)gps.longitude / 10000000, DEC);
      SerPri(" Alt:");
      SerPri((float)gps.altitude / 100.0, DEC);
      SerPri(" GSP:");
      SerPri(gps.ground_speed / 100.0);
      SerPri(" COG:");
      SerPri(gps.ground_course / 100.0, DEC);
      SerPri(" SAT:");
      SerPri(gps.num_sats, DEC);
      SerPri(" FIX:");
      SerPri(gps.fix, DEC);
      SerPri(" TIM:");
      SerPri(gps.time, DEC);
      SerPriln();
      gps.new_data = 0; // We have readed the data
    }else {
      SerPri("no new gps data:");
      SerPri(" Lat:");
      SerPri((float)gps.latitude / 10000000, DEC);
      SerPri(" Lon:");
      SerPri((float)gps.longitude / 10000000, DEC);
    }
//    SerPri("Focus Servo = ");
//    SerPriln(CAM_FOCUs);
    
//    SerPri("Current Sonar Valude = ");
//    SerPriln(Sonar_value);
//    SerPri("Target Sonar Altitude = ");
//    SerPriln(target_sonar_altitude);
//    SerPri("Current Baro Altitude = ");
//    SerPriln(press_alt);
//    SerPri("Target Baro Altitude = ");
//    SerPriln(target_baro_altitude);
//    SerPri("Throttle Altitude Change Mode = ");
//    if (Throttle_Altitude_Change_mode == 0) 
//      SerPriln("Off");
//    else if (Throttle_Altitude_Change_mode == 1)  
//      SerPriln("On");
//    SerPri("Hover Throttle Position Mode = ");
//    if (Hover_Throttle_Position_mode == 0) 
//      SerPriln("Off");
//    else if (Hover_Throttle_Position_mode == 1)  
//      SerPriln("On");
//    SerPri("USE BMP Altitude mode = ");
//    if (Use_BMP_Altitude == 0) 
//      SerPriln("Off");
//    else if (Use_BMP_Altitude == 1)
//      SerPriln("On");
//    SerPri("Hover Throttle Position = ");
//    SerPriln(Hover_Throttle_Position);
//    SerPri("Altitude I Grow = ");
//    SerPriln(altitude_I_grow);
//    SerPri("Current Sonar raw Reading = ");
//    SerPriln(sonar_read);
//    SerPri("STABLE MODE KP RATE = ");
//    SerPriln(STABLE_MODE_KP_RATE, 3);
//    SerPri("Altitude Command = ");
//    SerPriln(command_altitude);
//    SerPri("Total Throttle Command = ");
//    SerPriln(ch_throttle + command_altitude);

//    SerPri("Current Altitude = ");
//    SerPriln(BMP_Altitude);
//    SerPri("Current throttle = ");
//    SerPriln(ch_throttle);
//    SerPri("Yaw mid = ");
//    SerPriln(yaw_mid);
//    SerPri("BMP_altitude command = ");
//    SerPriln(BMP_command_altitude);
//    SerPri("Amount RX Yaw = ");
//    SerPriln(amount_rx_yaw);
//    SerPri("Current Compass Heading = ");
//    current_heading_hold = APM_Compass.Heading;
//    if (current_heading_hold < 0)
//      current_heading_hold += ToRad(360);
//    SerPriln(ToDeg(current_heading_hold), 3);
//    SerPri("Error Course = ");
//    SerPriln(ToDeg(errorCourse), 3);
//    SerPri("Heading Hold Mode = ");
//    if (heading_hold_mode == 0) 
//      SerPriln("Off");
//    else 
//      SerPriln("On");
//    SerPri("KP ALTITUDE = ");
//    SerPriln(KP_ALTITUDE, 3);
//    SerPri("EEPROM KP ALTITUDE = ");
//    SerPriln(readEEPROM(KP_ALTITUDE_ADR), 3);
//    SerPri("KI ALTITUDE = ");
//    SerPriln(KI_ALTITUDE, 3);
//    SerPri("EEPROM KI ALTITUDE = ");
//    SerPriln(readEEPROM(KI_ALTITUDE_ADR), 3);
//    SerPri("KD ALTITUDE = ");
//    SerPriln(KD_ALTITUDE, 3);
//    SerPri("EEPROM KD ALTITUDE = ");
//    SerPriln(readEEPROM(KD_ALTITUDE_ADR), 3);
//    SerPri("KP ROLL ACRO MODE = ");
//    SerPriln(Kp_RateRoll, 3);
//    SerPri("EEPROM KP ROLL ACRO MODE = ");
//    SerPriln(readEEPROM(KP_RATEROLL_ADR), 3);
//    SerPri("STABLE MODE KP RATE ROLL = ");
//    SerPriln(STABLE_MODE_KP_RATE_ROLL, 3);
//    SerPri("EEPROM STABLE MODE KP RATE ROLL = ");
//    SerPriln(readEEPROM(STABLE_MODE_KP_RATE_ROLL_ADR), 3);
//    SerPri("STABLE MODE KP RATE PITCH = ");
//    SerPriln(STABLE_MODE_KP_RATE_PITCH, 3);
//    SerPri("EEPROM STABLE MODE KP RATE PITCH = ");
//    SerPriln(readEEPROM(STABLE_MODE_KP_RATE_PITCH_ADR), 3);
//    SerPri("KP PITCH ACRO MODE = ");
//    SerPriln(Kp_RatePitch, 3);
//    SerPri("EEPROM KP PITCH ACRO MODE = ");
//    SerPriln(readEEPROM(KP_RATEPITCH_ADR), 3);
//    SerPri("KP STABLE MODE YAW = ");
//    SerPriln(STABLE_MODE_KP_RATE_YAW, 3);
//    SerPri("EEPROM KP STABLE MODE YAW = ");
//    SerPriln(readEEPROM(STABLE_MODE_KP_RATE_YAW_ADR), 3);

//    SerPri("KP GPS Roll = ");
//    SerPriln(KP_GPS_ROLL, 3);
//    SerPri("KP GPS Pitch = ");
//    SerPriln(KP_GPS_PITCH, 3);
//    SerPri("EEPROM KP GPS ROLL = ");
//    SerPriln(readEEPROM(KP_GPS_ROLL_ADR), 3);
//    SerPri("EEPROM KP GPS PITCH = ");
//    SerPriln(readEEPROM(KP_GPS_ROLL_ADR), 3);
//    SerPri("KI GPS Roll = ");
//    SerPriln(KI_GPS_ROLL, 4);
//    SerPri("KI GPS Pitch = ");
//    SerPriln(KI_GPS_PITCH, 4);
//    SerPri("EEPROM KI GPS ROLL = ");
//    SerPriln(readEEPROM(KI_GPS_ROLL_ADR), 4);
//    SerPri("EEPROM KP GPS PITCH = ");
//    SerPriln(readEEPROM(KI_GPS_ROLL_ADR), 4);
//    SerPri("Magnetometer = ");
//    SerPriln(MAGNETOMETER);
//    SerPri("EEPROM Magnetometer = ");
//    SerPriln(readEEPROM(MAGNETOMETER_ADR));
//    SerPri("Magnetometer Offset= ");
//    SerPriln(Magoffset);
//    SerPri("EEPROM Magoffset = ");
//    SerPriln(readEEPROM(Magoffset_ADR));
    
//    SerPri("Yaw = ");
//    SerPriln(yaw);
//    SerPri("Yaw to Degree = ");
//    SerPriln(ToDeg(yaw));
//    SerPri("command rx yaw =");
//    SerPriln(command_rx_yaw);

    SerPriln(); 
    queryType = 'X';
    break;
  case 'U': // Send receiver values
    SerPri(ch_roll); // Aileron
    comma();
    SerPri(ch_pitch); // Elevator
    comma();
    SerPri(ch_yaw); // Yaw
    comma();
    SerPri(ch_throttle); // Throttle
    comma();
    SerPri(ch_aux); // AUX1 (Mode)
    comma();
    SerPri(ch_aux2); // AUX2 
    comma();
    SerPri(roll_mid); // Roll MID value
    comma();
    SerPri(pitch_mid); // Pitch MID value
    comma();
    SerPrln(yaw_mid); // Yaw MID Value
    break;
  case 'V': // Spare
    break;
  case 'X': // Stop sending messages
    break;
  case '!': // Send flight software version
    SerPrln(VER);
    queryType = 'X';
    break;
  case '2': // Send transmitter calibration values
    SerPri(ch_roll_slope);
    comma();
    SerPri(ch_roll_offset);
    comma();
    SerPri(ch_pitch_slope);
    comma();
    SerPri(ch_pitch_offset);
    comma();
    SerPri(ch_yaw_slope);
    comma();
    SerPri(ch_yaw_offset);
    comma();
    SerPri(ch_throttle_slope);
    comma();
    SerPri(ch_throttle_offset);
    comma();
    SerPri(ch_aux_slope);
    comma();
    SerPri(ch_aux_offset);
    comma();
    SerPri(ch_aux2_slope);
    comma();
    SerPrln(ch_aux2_offset);
    queryType = 'X';
    break;
  case '3':  // Jani's debugs
    SerPri(yaw);
    tab();
    SerPri(command_rx_yaw);
    tab();
    SerPri(control_yaw);
    tab();
    SerPri(err_yaw);
    tab();
    SerPri(AN[0]);
    tab();
    SerPri(AN[1]);
    tab();
    SerPri(AN[2] - gyro_offset_yaw);
    tab();
    SerPri(gyro_offset_yaw - AN[2]);
    tab();
    SerPri(gyro_offset_yaw);
    tab();
    SerPri(1500 - (gyro_offset_yaw - AN[2]));
    tab();
    SerPriln();
    break;
#ifdef IsGPS
  case '4':  // Jani's debugs
//  Log_Write_GPS(GPS.Time, GPS.Latitude, GPS.Longitude, GPS.Altitude, GPS.Altitude, GPS.Ground_Speed, GPS.Ground_Course, gps.fix, GPS.NumSats);

    SerPri(gps.time);
    tab();
    SerPri(gps.latitude);
    tab();
    SerPri(gps.longitude);
    tab();
    SerPri(gps.altitude);
    tab();
    SerPri(gps.ground_speed);
    tab();
    SerPri(gps.ground_course);
    tab();
    SerPri(gps.fix);
    tab();
    SerPri(gps.num_sats);

    tab();
    SerPriln();
    break;
#endif    
#if (defined(SerXbee) && defined(Use_PID_Tuning))
  case 'o': // Switch PID tuning ON
    ON_PID = 1;    
    SerPrln("PID Tuning ON, Pitch & Roll set, P of PID set"); 
    Pitch_Roll_PID = 1;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0;    
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    P_PID = 1;
    I_PID = 0;
    D_PID = 0;
    queryType = 'X';
    break;
  case 'f': // Switch PID tuning OFF
    ON_PID = 0;    
    SerPrln("PID Tuning OFF"); 
    queryType = 'X';
    break;
  case 'r': // Switch to Pitch and Roll, PID tuning
    Pitch_Roll_PID = 1;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0;    
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    P_PID = 1;
    I_PID = 0;
    D_PID = 0;
    SerPrln("Pitch and Roll, PID Tuning, P of PID set");
    queryType = 'X';
    break;
  case 'w': // Yaw PID tuning
    Pitch_Roll_PID = 0;
    Yaw_PID = 1;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0;    
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    P_PID = 1;
    I_PID = 0;
    D_PID = 0;
    SerPrln("Yaw, PID Tuning, P of PID set");
    queryType = 'X';
    break;
  case 'b': // Baro PID tuning
    Pitch_Roll_PID = 0;
    Yaw_PID = 0;
    Baro_PID = 1;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0;    
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    P_PID = 1;
    I_PID = 0;
    D_PID = 0;
    SerPrln("Baro, PID Tuning, P of PID set");
    queryType = 'X';
    break;
  case 's': // Sonar PID tuning
    Pitch_Roll_PID = 0;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 1;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0;    
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    P_PID = 1;
    I_PID = 0;
    D_PID = 0;
    SerPrln("Sonar, PID Tuning, P of PID set");
    queryType = 'X';
    break;
  case 'g': // GPS PID tuning
    Pitch_Roll_PID = 0;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 1;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0;    
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    P_PID = 1;
    I_PID = 0;
    D_PID = 0;
    SerPrln("GPS, PID Tuning, P of PID set");
    queryType = 'X';
    break;
  case 'p': // P factor of PID tuning
    P_PID = 1;
    I_PID = 0;
    D_PID = 0;
    SerPrln("P factor of  PID set");
    queryType = 'X';
    break;
  case 'i': // I factor of PID tuning
    P_PID = 0;
    I_PID = 1;
    D_PID = 0;
    SerPrln("I factor of  PID set");
    queryType = 'X';
    break;
  case 'd': // D factor of PID tuning
    P_PID = 0;
    I_PID = 0;
    D_PID = 1;
    SerPrln("D factor of  PID set");
    queryType = 'X';
    break;
  case 'x': // Accelerometer Roll Offset Tuning
    P_PID = 0;
    I_PID = 0;
    D_PID = 0;
    Pitch_Roll_PID = 0;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 1;    
    ACC_offset_y_adj = 0;    
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    SerPrln("Accelerometer Roll Offset Tuning");
    queryType = 'X';
    break;
  case 'y': // Accelerometer Pitch Offset Tuning
    P_PID = 0;
    I_PID = 0;
    D_PID = 0;
    Pitch_Roll_PID = 0;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 1;    
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    SerPrln("Accelerometer Pitch Offset Tuning");
    queryType = 'X';
    break;
  case 'a': // Camera Smooth Pitch
    P_PID = 0;
    I_PID = 0;
    D_PID = 0;
    Pitch_Roll_PID = 0;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0;    
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 1;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    SerPri("Camera Smooth Pitch");
    SerPrln(CAM_SMOOTHING);
    queryType = 'X';
    break;
  case 'e': // Camera Smooth Roll
    P_PID = 0;
    I_PID = 0;
    D_PID = 0;
    Pitch_Roll_PID = 0;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0;    
    Camera_Smooth_Roll_adj = 1;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    SerPri("Camera Smooth Roll");
    SerPrln(CAM_SMOOTHING_ROLL);
    queryType = 'X';
    break;
  case 'c': // Camera Roll Centre
    P_PID = 0;
    I_PID = 0;
    D_PID = 0;
    Pitch_Roll_PID = 0;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0; 
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 1;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    SerPri("Camera Roll Centre");
    SerPrln(CAM_CENT);
    queryType = 'X';
    break;
  case 'h': // Camera Focus Position - using servo
    P_PID = 0;
    I_PID = 0;
    D_PID = 0;
    Pitch_Roll_PID = 0;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0; 
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 1;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 0;    
    SerPri("Camera Focus Position - using servo");
    SerPrln(CAM_FOCUS);
    queryType = 'X';
    break;
  case 't': // Camera Trigger Position - using servo
    P_PID = 0;
    I_PID = 0;
    D_PID = 0;
    Pitch_Roll_PID = 0;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0; 
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 1;
    Camera_Release_adj = 0;    
    SerPri("Camera Trigger Position - using servo");
    SerPrln(CAM_TRIGGER);
    queryType = 'X';
    break;
  case 'j': // Camera Release Position - using servo
    P_PID = 0;
    I_PID = 0;
    D_PID = 0;
    Pitch_Roll_PID = 0;
    Yaw_PID = 0;
    Baro_PID = 0;
    Sonar_PID = 0;
    GPS_PID = 0;
    ACC_offset_x_adj = 0;    
    ACC_offset_y_adj = 0; 
    Camera_Smooth_Roll_adj = 0;
    Camera_Smooth_Pitch_adj = 0;
    Camera_Roll_Centre_adj = 0;  
    Camera_Focus_adj = 0;
    Camera_Trigger_adj = 0;
    Camera_Release_adj = 1;    
    SerPri("Camera Release Position - using servo");
    SerPrln(CAM_RELEASE);
    queryType = 'X';
    break;
#endif
  case '.': // Modify GPS settings, print directly to GPS Port
    Serial1.print("$PGCMD,16,0,0,0,0,0*6A\r\n");
    break;
  }
}

void comma() {
  SerPri(',');
}

void tab() {
  SerPri("\t");
}

// Used to read floating point values from the serial port
float readFloatSerial() {
  byte index = 0;
  byte timeout = 0;
  char data[128] = "";

  do {
    if (SerAva() == 0) {
      delay(10);
      timeout++;
    }
    else {
      data[index] = SerRea();
      timeout = 0;
      index++;
    }
  }  
  while ((data[constrain(index-1, 0, 128)] != ';') && (timeout < 5) && (index < 128));
  return atof(data);
}


