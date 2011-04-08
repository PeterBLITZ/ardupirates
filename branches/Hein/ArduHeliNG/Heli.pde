/*
 www.ArduCopter.com - www.DIYDrones.com
 Copyright (c) 2010.  All rights reserved.
 An Open Source Arduino based multicopter.
 
 File     : Heli.pde
 Desc     : code specific to traditional helicopters
 Version  : v1.0, Aug 27, 2010
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


* ************************************************************** *
TODO:


* ************************************************************** */

#if ((AIRFRAME == HELI) || (AIRFRAME == HELICOPTER))

/**********************************************************************/
// heli_readUserConfig - reads values in from EEPROM
void heli_readUserConfig() 
{ 
    float magicNum = 0;
    magicNum = readEEPROM(EEPROM_MAGIC_NUMBER_ADDR);
    if( magicNum != EEPROM_MAGIC_NUMBER ) {
        SerPri("No heli settings found in EEPROM.  Using defaults");
        heli_defaultUserConfig();
    }else{
        frontLeftCCPMmin = readEEPROM(FRONT_LEFT_CCPM_MIN_ADDR);
        frontLeftCCPMmax = readEEPROM(FRONT_LEFT_CCPM_MAX_ADDR);
        frontRightCCPMmin = readEEPROM(FRONT_RIGHT_CCPM_MIN_ADDR);
        frontRightCCPMmax = readEEPROM(FRONT_RIGHT_CCPM_MAX_ADDR);
        rearCCPMmin = readEEPROM(REAR_CCPM_MIN_ADDR);
        rearCCPMmax = readEEPROM(REAR_CCPM_MAX_ADDR);
        yawMin = readEEPROM(YAW_MIN_ADDR);
        yawMax = readEEPROM(YAW_MAX_ADDR);
        // cross effects
        throttle_yaw_effect = readEEPROM(THROTTLE_YAW_EFFECT_ADDR);
        // I values
//        heli_restore_I_values();
    }
}

/*****************************************************************************/
// heli_save_I_values - store I values so they can't be reused for next flight
void heli_save_I_values()
{
    writeEEPROM(roll_I, ROLL_I_VALUE_ADDR);
    writeEEPROM(pitch_I, PITCH_I_VALUE_ADDR);
    writeEEPROM(heading_I, HEADING_I_VALUE_ADDR);
    writeEEPROM(yaw_I, YAW_I_VALUE_ADDR);
}

/****************************************************************************/
// heli_restore_I_values - restore I values from previous flights
void heli_restore_I_values()
{
    roll_I = readEEPROM(ROLL_I_VALUE_ADDR);
    pitch_I = readEEPROM(PITCH_I_VALUE_ADDR);
    heading_I = readEEPROM(HEADING_I_VALUE_ADDR);
    yaw_I = readEEPROM(YAW_I_VALUE_ADDR);
}


/**********************************************************************/
// default the heli specific values to defaults
void heli_defaultUserConfig() 
{
  // default CCPM values.
  frontLeftCCPMmin =        1200;
  frontLeftCCPMmax =        1800;
  frontRightCCPMmin  =      1900;
  frontRightCCPMmax =       1100;
  rearCCPMmin =             1200;
  rearCCPMmax =             1800;
  yawMin =                  1200;
  yawMax =                  1800; 
  
  // default PID values - Roll
  KP_QUAD_ROLL               = 0.900; //1.100
  KI_QUAD_ROLL               = 0.200;
  STABLE_MODE_KP_RATE_ROLL   = -0.040;
  
  // default PID values - Pitch
  KP_QUAD_PITCH              = 0.900; //1.100
  KI_QUAD_PITCH              = 0.150; //0.120
  STABLE_MODE_KP_RATE_PITCH  = -0.060; //-0.001;
  
  // default PID values - Yaw
  Kp_RateYaw                 = 4.000;  // heading P term
  Ki_RateYaw                 = 0.100;  // heading I term
  KP_QUAD_YAW                = 0.090;  // yaw rate P term
  KI_QUAD_YAW                = 0.040;  // yaw rate I term
  STABLE_MODE_KP_RATE_YAW    = 0.000;  // yaw rate D term
 
  // Sonar altitude hold
  KP_SONAR_ALTITUDE          = 0.8;
  KI_SONAR_ALTITUDE          = 0.3;
  KD_SONAR_ALTITUDE          = 0.12;
  
  // Barometer altitude hold
  KP_ALTITUDE                = 0.08;
  KI_ALTITUDE                = 0.05;
  KD_ALTITUDE                = 0.01;
  
  // GPS
  KP_GPS_ROLL                = 0.006;   //0.012;
  KI_GPS_ROLL                = 0.001;   //0.001;
  KD_GPS_ROLL                = 0.007;   //0.015;
  KP_GPS_PITCH               = 0.006;   //0.010;
  KI_GPS_PITCH               = 0.001;   //0.001;
  KD_GPS_PITCH               = 0.007;   //0.015;
  GPS_MAX_ANGLE              = 15;      //22;  
  
  // Cross effects
  throttle_yaw_effect        = 1.0;
  
  // I values
  roll_I = 0.0;
  pitch_I = 0.0;
  heading_I = 0.0;
  yaw_I = 0.0;
}

void Heli_CCPM_Test()
{

Vector3f ccpmInput;                // Array of ccpm input values, converted to percents
Vector3f rollPitchCollInput;       // Array containing deallocated roll, pitch and collective percent commands
Vector3f ccpmOutput;               // Array of ccpm input values, converted to percents
  
    // capture CCPM Values for Front Servo 0
    SerFlu();
    SerPri("Enter Left Servo Value: ");
    while( !SerAva() );  // wait until user presses a key  
    ccpmInput.x = readFloatSerial();  // capture new value
    SerPriln(ccpmInput.x);  // display new value
    
    // capture CCPM Values for Right Servo 1
    SerFlu();
    SerPri("Enter Right Servo Value: ");
    while( !SerAva() );  // wait until user presses a key  
    ccpmInput.y = readFloatSerial();  // capture new value
    SerPriln(ccpmInput.y);  // display new value

    // capture CCPM Values for Front Servo 3
    SerFlu();
    SerPri("Enter Rear Servo Value: ");
    while( !SerAva() );  // wait until user presses a key  
    ccpmInput.z = readFloatSerial();  // capture new value
    SerPriln(ccpmInput.z);  // display new value

    rollPitchCollInput = ccpmDeallocation * ccpmInput;

    SerPri("Roll Value = : ");
    SerPriln(rollPitchCollInput.x);  // display new value
    SerPri("Pitch Value = : ");
    SerPriln(rollPitchCollInput.y);  // display new value
    SerPri("Collective Value = : ");
    SerPriln(rollPitchCollInput.z);  // display new value

    ccpmOutput = ccpmAllocation * rollPitchCollInput;

    SerPri("Front Servo Value = : ");
    SerPriln(ccpmOutput.x);  // display new value
    SerPri("Right Servo Value = : ");
    SerPriln(ccpmOutput.y);  // display new value
    SerPri("Rear Servo Value = : ");
    SerPriln(ccpmOutput.z);  // display new value
}

void Heli_CLI_Settings()
{
    float tempVal1, tempVal2, tempVal3, tempVal4;
    // retrieve from eeprom
    throttle_yaw_effect = readEEPROM(THROTTLE_YAW_EFFECT_ADDR);
    heli_restore_I_values();
  
    // display current throttle_yaw_effect value
    SerPri("throttle yaw effect: ");
    SerPriln(throttle_yaw_effect);
    SerPri("roll_I: ");
    SerPriln(roll_I);
    SerPri("pitch_I: ");
    SerPriln(pitch_I);
    SerPri("heading_I: ");
    SerPriln(heading_I);
    SerPri("yaw_I: ");
    SerPriln(yaw_I);

    // capture new throttle_yaw_effect value
    SerFlu();
    SerPri("Enter throttle yaw effect: ");
    while( !SerAva() );  // wait until user presses a key  
    throttle_yaw_effect = readFloatSerial();  // capture new value
    SerPriln(throttle_yaw_effect);  // display new value
    
    // capture roll_I
    SerFlu();
    SerPri("Enter roll_I: ");
    while( !SerAva() );  // wait until user presses a key  
    roll_I = readFloatSerial();  // capture new value
    SerPriln(roll_I);
    
    // capture pitch_I
    SerFlu();
    SerPri("Enter pitch_I: ");
    while( !SerAva() );  // wait until user presses a key  
    pitch_I = readFloatSerial();  // capture new value
    SerPriln(pitch_I);
   
    // capture heading_I
    SerFlu();
    SerPri("Enter heading_I: ");
    while( !SerAva() );  // wait until user presses a key  
    heading_I = readFloatSerial();  // capture new value
    SerPriln(heading_I);
    
    // capture yaw_I
    SerFlu();
    SerPri("Enter yaw_I: ");
    while( !SerAva() );  // wait until user presses a key  
    yaw_I = readFloatSerial();  // capture new value
    SerPriln(yaw_I);

    // display current throttle_yaw_effect value
    SerPriln();
    SerPri("throttle yaw effect: ");
    SerPriln(throttle_yaw_effect);
    SerPri("roll_I: ");
    SerPriln(roll_I);
    SerPri("pitch_I: ");
    SerPriln(pitch_I);
    SerPri("heading_I: ");
    SerPriln(heading_I);
    SerPri("yaw_I: ");
    SerPriln(yaw_I); 
    
    // save to eeprom
    writeEEPROM(throttle_yaw_effect,THROTTLE_YAW_EFFECT_ADDR);
    heli_save_I_values();
    SerPriln("new values saved to eeprom.");

}


/**********************************************************************/
// displaySettings - displays heli specific user settings
void heli_displaySettings() 
{
    SerPri("frontLeftCCPM min: ");
    SerPri(frontLeftCCPMmin);
    SerPri("\tmax:");
    SerPri(frontLeftCCPMmax);
    
    if( abs(frontLeftCCPMmin-frontLeftCCPMmax)<50 || frontLeftCCPMmin < 900 || frontLeftCCPMmin > 2100 || frontLeftCCPMmax < 900 || frontLeftCCPMmax > 2100 )
        SerPrln("\t\t<-- check");
    else
        SerPrln();
    
    SerPri("frontRightCCPM min: ");
    SerPri(frontRightCCPMmin);
    SerPri("\tmax:");
    SerPri(frontRightCCPMmax);
    if( abs(frontRightCCPMmin-frontRightCCPMmax)<50 || frontRightCCPMmin < 900 || frontRightCCPMmin > 2100 || frontRightCCPMmax < 900 || frontRightCCPMmax > 2100 )
        SerPrln("\t\t<-- check");
    else
        SerPrln();    
    
    SerPri("rearCCPM min: ");
    SerPri(rearCCPMmin);
    SerPri("\tmax:");
    SerPri(rearCCPMmax);
    if( abs(rearCCPMmin-rearCCPMmax)<50 || rearCCPMmin < 900 || rearCCPMmin > 2100 || rearCCPMmax < 900 || rearCCPMmax > 2100 )
        SerPrln("\t\t<-- check");
    else
        SerPrln();
    
    SerPri("yaw min: ");
    SerPri(yawMin);
    SerPri("\tmax:");
    SerPri(yawMax);
    if( abs(yawMin-yawMax)<50 || yawMin < 900 || yawMin > 2100 || yawMax < 900 || yawMax > 2100 )
        SerPrln("\t\t<-- check");
    else
        SerPrln();   

    SerPrln();
}

////////////////////////////////////////////////////////////////////////////////
//  Setup Procedure
////////////////////////////////////////////////////////////////////////////////
void heli_setup()
{
  int i;
 
  // read heli specific settings (like CCPM values) from EEPROM
  heli_readUserConfig();

#if AIRFRAME == HELI  
  // update CCPM values
  frontLeftCCPMslope =      100 / (frontLeftCCPMmax - frontLeftCCPMmin);
  frontLeftCCPMintercept =  100 - (frontLeftCCPMslope * frontLeftCCPMmax);
  frontRightCCPMslope =     100 / (frontRightCCPMmax - frontRightCCPMmin);
  frontRightCCPMintercept = 100 - (frontRightCCPMslope * frontRightCCPMmax);
  rearCCPMslope =           100 / (rearCCPMmax - rearCCPMmin);
  rearCCPMintercept =       100 - (rearCCPMslope * rearCCPMmax);
  yawSlope =                100 / (yawMax - yawMin);
  yawIntercept =            50 - (yawSlope * yawMax);
#endif

#if AIRFRAME == HELICOPTER
  // update CCPM values
  frontLeftCCPMslope =      1000 / (frontLeftCCPMmax - frontLeftCCPMmin);
  frontLeftCCPMintercept =  1000 - (frontLeftCCPMslope * frontLeftCCPMmax);
  frontRightCCPMslope =     1000 / (frontRightCCPMmax - frontRightCCPMmin);
  frontRightCCPMintercept = 1000 - (frontRightCCPMslope * frontRightCCPMmax);
  rearCCPMslope =           1000 / (rearCCPMmax - rearCCPMmin);
  rearCCPMintercept =       1000 - (rearCCPMslope * rearCCPMmax);
  yawSlope =                1000 / (yawMax - yawMin);
  yawIntercept =            500 - (yawSlope * yawMax);
#endif  

  // capture trims
 
//  heli_read_radio_trims();
  
  // hardcode mids because we will use ccpm
  roll_mid = ROLL_MID;
  pitch_mid = PITCH_MID;
  collective_mid = 1500;
  yaw_mid = (yawMin+yawMax)/2;
  
  // determine which axis APM will control
  roll_control_switch = !SW_DIP1;
  pitch_control_switch = !SW_DIP2;
  yaw_control_switch = !SW_DIP3; 
  collective_control_switch = !SW_DIP4;
  //position_control_switch = !SW_DIP4;  // switch 4 controls whether we will do GPS hold or not

 // clear gyro filter
  for(i=0; i<HELI_GYRO_NUM_AVERAGING; i++) {
      heli_roll_filter[i] = 0;
      heli_pitch_filter[i] = 0;
      heli_yaw_filter[i] = 0;
  }
}

/*****************************************************************************************************/
// heli_read_radio_trims - captures roll, pitch and yaw trims (mids) although only yaw is actually used
//                         trim_yaw is used to center output to the tail which tends to be far from the
//                         physical middle of where the rudder can move.  This helps keep the PID's I
//                         value low and avoid sudden turns left on takeoff
void heli_read_radio_trims()
{
    int i;
    float sumRoll = 0, sumPitch = 0, sumYaw = 0;
    
    // initialiase trims to zero incase this is called more than once
    trim_roll = 0.0;
    trim_pitch = 0.0;
    trim_yaw = 0.0;
    
    // read radio a few times
    for(int i=0; i<10; i++ )
    {
        // make sure new data has arrived
        while( APM_RC.GetState() != 1 )
        {
            delay(20);
        }
        heli_read_radio();
        sumRoll += ch_roll;
        sumPitch += ch_pitch;
        sumYaw += ch_yaw;
    }
    
    // set trim to average 
    trim_roll = sumRoll / 10.0;
    trim_pitch = sumPitch / 10.0;
    trim_yaw = sumYaw / 10.0;
    
    // double check all is ok
    if( trim_roll > 50.0 || trim_roll < -50 )
        trim_roll = 0.0;
    if( trim_pitch >50.0 || trim_roll < -50.0 )
        trim_pitch = 0.0;
    if( trim_yaw > 50.0 || trim_yaw < -50.0 )
        trim_yaw = 0.0;

}

/**********************************************************************/
// Radio decoding
void heli_read_radio()
{
  APM_PERFMON_REGISTER
    // left channel
    ccpmPercents.x  = frontLeftCCPMslope * APM_RC.InputCh(CHANNEL_FRONT_LEFT) + frontLeftCCPMintercept;
    
    // right channel
    ccpmPercents.y = frontRightCCPMslope * APM_RC.InputCh(CHANNEL_FRONT_RIGHT) + frontRightCCPMintercept;
    
    // rear channel
    ccpmPercents.z = rearCCPMslope * APM_RC.InputCh(CHANNEL_REAR) + rearCCPMintercept;
    
    // decode the ccpm
    rollPitchCollPercent = ccpmDeallocation * ccpmPercents;
    
    // get the yaw (not coded)
    yawPercent = (yawSlope * APM_RC.InputCh(CHANNEL_YAW) + yawIntercept) - trim_yaw;
    
    // put decoded values into the global variables    
    ch_roll = rollPitchCollPercent.x;
    ch_pitch = rollPitchCollPercent.y;
    ch_collective = rollPitchCollPercent.z;
    
    // allow a bit of a dead zone for the yaw
    if( fabs(yawPercent) < 2 )
        ch_yaw = 0;
    else
        ch_yaw = yawPercent;

    // get the aux channel (for tuning on/off autopilot)   
    ch_aux = APM_RC.InputCh(CH_5); //* ch_aux_slope + ch_aux_offset;     
    ch_aux2 = APM_RC.InputCh(CH_6); //* ch_aux2_slope + ch_aux2_offset;   //This is the MODE Channel in Configurator.

    // convert to absolute angles
    command_rx_roll = ch_roll / HELI_STICK_TO_ANGLE_FACTOR;        // Convert stick position to absolute angles
    command_rx_pitch = ch_pitch / HELI_STICK_TO_ANGLE_FACTOR;      // Convert stick position to absolute angles
    command_rx_collective = ch_collective;
    command_rx_yaw = ch_yaw / HELI_YAW_STICK_TO_ANGLE_FACTOR;      // Convert stick position to turn rate

    // for use by non-heli parts of code   
    ch_throttle = 1000 + (ch_collective * 10);     
    
    // hardcode flight mode
    flightMode = FM_STABLE_MODE;
    
// FLIGHT MODE
//  This is determine by DIP Switch 3. // When switching over you have to reboot APM.
// DIP3 down (On)  = Acrobatic Mode.  Yellow LED is Flashing. 
// DIP3 up   (Off) = Stable Mode.  AUTOPILOT MODE LEDs status lights become applicable.  See below.

    // Autopilot mode (only works on Stable mode)
    if (flightMode == FM_STABLE_MODE)
    {
      if (ch_aux2 > 1650 && ch_aux > 1650)
      {
        AP_mode = AP_NORMAL_STABLE_MODE  ;      // Stable mode (Heading Hold only)
        digitalWrite(LED_Yellow,LOW);           // Yellow LED OFF : Alititude Hold OFF
        digitalWrite(LED_Red,LOW);              // Red LED OFF : GPS Position Hold OFF
      }
      else if (ch_aux2 > 1650 && ch_aux < 1450)
      {
        AP_mode = AP_ALTITUDE_HOLD;             // Super Stable Mode (Altitude hold mode)
        digitalWrite(LED_Yellow,HIGH);          // Yellow LED ON : Alititude Hold ON
        digitalWrite(LED_Red,LOW);              // Red LED OFF : GPS Position Hold OFF
      }
      else if (ch_aux2 < 1450 && ch_aux > 1650)
      {
        AP_mode = AP_GPS_HOLD;                  // Position Hold (GPS position control)
        digitalWrite(LED_Yellow,LOW);           // Yellow LED OFF : Alititude Hold OFF
        if (gps.fix > 0)
          digitalWrite(LED_Red,HIGH);           // Red LED ON : GPS Position Hold ON
      }
      else 
      {
        AP_mode = AP_ALT_GPS_HOLD;              //Position & Altitude hold mode (GPS position control & Altitude control)
        digitalWrite(LED_Yellow,HIGH);          // Yellow LED ON : Alititude Hold ON
        if (gps.fix > 0)
          digitalWrite(LED_Red,HIGH);           // Red LED ON : GPS Position Hold ON
      }
    } 
#if LOG_ATTITUDE   // log radio at the same time as attitude because you probably want to see how well attitude is tracking to user commands
    // Write Radio data to DataFlash log
    //Log_Write_Radio(command_rx_roll,command_rx_pitch,command_rx_collective,command_rx_yaw,ch_aux,ch_aux2);
#endif
}    
/**********************************************************************/
// output to swash plate based on control variables
// Uses these global variables:
// control_roll       : -50 ~ 50
// control_pitch      : -50 ~ 50
// control_collective :   0 ~ 100
// control_yaw        : -50 ~ 50
void heli_moveSwashPlate()
{
  APM_PERFMON_REGISTER
    static int count = 0;
    // turn pitch, roll, collective commands into ccpm values (i.e. values for each servo)
    ccpmPercents_out = ccpmAllocation * Vector3f(control_roll, control_pitch, control_collective);

    // calculate values to be sent out to RC Output channels
    leftOut =  (ccpmPercents_out.x - frontLeftCCPMintercept) / frontLeftCCPMslope;
    rightOut = (ccpmPercents_out.y - frontRightCCPMintercept) / frontRightCCPMslope;
    rearOut =  (ccpmPercents_out.z - rearCCPMintercept) / rearCCPMslope;
    yawOut =   (control_yaw - yawIntercept) / yawSlope;
      
    APM_RC.OutputCh(CHANNEL_FRONT_LEFT,leftOut);
    APM_RC.OutputCh(CHANNEL_FRONT_RIGHT,rightOut);
    APM_RC.OutputCh(CHANNEL_REAR,rearOut);
    APM_RC.OutputCh(CHANNEL_YAW,yawOut);
    APM_RC.OutputCh(CHANNEL_YAW_GAIN, 1200);

     // InstantPWM
    APM_RC.Force_Out0_Out1();
    APM_RC.Force_Out2_Out3();
}

/**********************************************************************/
// ROLL, PITCH and YAW PID controls... 
// Input : desired Roll, Pitch absolute angles
//         collective as a percentage from 0~100
//         yaw as a rate of rotation
// Output : control_roll - roll servo as a percentage (-50 to 50)
//          control_pitch - pitch servo as a percentage (-50 to 50)
//          control_collective - collective servo as a percentage (0 to 100)
//          control_yaw - yaw servo as a percentage (0 to 100)
void heli_attitude_control(int command_roll, int command_pitch, int command_collective, int command_yaw)
{
  APM_PERFMON_REGISTER
    static float firstIteration = 1;
    static float command_yaw_previous = 0;
    static float previousYawRate = 0;
    float currentYawRate;
    float control_yaw_rate;
    float err_heading;
    static int aCounter = 0;
    float heli_G_Dt;
    int i;
    float collective_yaw_impact;
   
    // get current time
    heli_G_Dt = (currentTimeMicros-heli_previousTimeMicros) * 0.000001;   // Microseconds!!!
    heli_previousTimeMicros = currentTimeMicros;
    
    // always pass through collective command
    control_collective = command_rx_collective;

    // ROLL CONTROL -- ONE PID
    if( roll_control_switch ) 
    {
        // P term
        err_roll = command_roll - ToDeg(roll);    
        err_roll = constrain(err_roll,-200,200); //-25,25 // to limit max roll command...
        // I term
        if( heli_moving == TRUE ) {  // only update I values if moving
            roll_I += err_roll*heli_G_Dt*KI_QUAD_ROLL;
            roll_I = constrain(roll_I,-50,50);  //10,10
        }
        // D term
        roll_D = ToDeg(Omega[0]) * STABLE_MODE_KP_RATE_ROLL;  // Take into account Angular velocity
        roll_D = constrain(roll_D,-200,200);  //-25,25
      
        // PID control
        control_roll = KP_QUAD_ROLL*err_roll + roll_I + roll_D;
        control_roll = constrain(control_roll,-250,250);  //-50,50
    }else{
        // straight pass through
        control_roll = ch_roll;
    }
  
    // PITCH CONTROL -- ONE PIDS
    if( pitch_control_switch ) 
    {    
        // P term
        err_pitch = command_pitch - ToDeg(pitch);
        err_pitch = constrain(err_pitch,-200,200);  //-25,25// to limit max pitch command...
        // I term
        if( heli_moving == TRUE ) {  // only update I values if moving        
            pitch_I += err_pitch * heli_G_Dt * KI_QUAD_PITCH;
            pitch_I = constrain(pitch_I,-50,50);
        }
        // D term
        pitch_D = ToDeg(Omega[1]) * STABLE_MODE_KP_RATE_PITCH; // Take into account Angular velocity
        pitch_D = constrain(pitch_D,-200,200);  //-25,25
        // PID control
        control_pitch = KP_QUAD_PITCH*err_pitch + pitch_I + pitch_D;
        control_pitch = constrain(control_pitch,-250,250); //-50,50
    }else{
        control_pitch = ch_pitch;
    }

    // YAW CONTROL
    if( yaw_control_switch ) 
    {     
        if( command_yaw == 0 )  // heading hold mode 
        {
            // check we don't need to reset targetHeading
            if( command_yaw_previous != 0 )
                targetHeading = ToDeg(yaw);
    
            // ensure reasonable targetHeading
            if( firstIteration || targetHeading > 180 || targetHeading < -180 )
            {
                firstIteration = 0;
                targetHeading = ToDeg(yaw);
            }
                
            err_heading = Normalize_angle(targetHeading - ToDeg(yaw));     
            err_heading = constrain(err_heading,-90,90);  // don't make it travel any faster beyond 90 degrees
          
            // I term
            if( heli_moving == TRUE ) {  // only update I values if moving            
                heading_I += (err_heading * heli_G_Dt * Ki_RateYaw);
                heading_I = constrain(heading_I,-20,20);
            }
    
            // PID control - a bit bad - we're using the acro mode's PID values because there's not PID for heading
            control_yaw_rate = Kp_RateYaw*err_heading + heading_I;
            control_yaw_rate = constrain(control_yaw_rate,-HELI_YAW_MAX_RATE,HELI_YAW_MAX_RATE);  // to limit max yaw command
         }else{      // rate mode
            err_heading = 0;
            control_yaw_rate = command_yaw;
        }
        command_yaw_previous = command_yaw;
    
        // YAW RATE CONTROL

        // filter yaw gyro
        heli_yaw_filter[heli_filter_ptr] = ToDeg(Gyro_Scaled_Z(read_adc(2)));
        heli_filter_ptr++;
        heli_filter_ptr %= HELI_GYRO_NUM_AVERAGING;

        //currentYawRate = ToDeg(Gyro_Scaled_Z(read_adc(2)));
        currentYawRate = 0;
        for(i=0; i<HELI_GYRO_NUM_AVERAGING; i++){
            currentYawRate += heli_yaw_filter[i];
        }
        currentYawRate /= ((float)HELI_GYRO_NUM_AVERAGING);
        
        //currentYawRate = ToDeg(Omega_Vector[2]);  <-- makes things very unstable!!
        err_yaw = control_yaw_rate-currentYawRate;
      
        // I term
        if( heli_moving == TRUE ) {  // only update I values if moving        
            yaw_I += err_yaw * heli_G_Dt * KI_QUAD_YAW;
            yaw_I = constrain(yaw_I, -30, 30);
        }
        // D term
        yaw_D = (currentYawRate-previousYawRate) * STABLE_MODE_KP_RATE_YAW; // Take into account the change in angular velocity
        yaw_D = constrain(yaw_D,-25,25);    
        previousYawRate = currentYawRate;
      
        // add collective impact
        collective_yaw_impact = ((float)(command_collective - 50)) * throttle_yaw_effect;
      
        // PID control
        //control_yaw = trim_yaw + (KP_QUAD_YAW*err_yaw + yaw_I + yaw_D);  // add back in the yaw trim so that it is our center point
        control_yaw = trim_yaw + KP_QUAD_YAW*(err_yaw-collective_yaw_impact) + yaw_I + yaw_D;  // add back in the yaw trim so that it is our center point
        control_yaw = constrain(control_yaw,-250,250);  //25,25
    }else{
        // straight pass through
        control_yaw = ch_yaw + trim_yaw;
    }
    
    // movement sensing
    if( fabs(ToDeg(Omega[0])) > HELI_GYRO_NO_MOVEMENT_VALUE || fabs(ToDeg(Omega[1])) > HELI_GYRO_NO_MOVEMENT_VALUE || fabs(ToDeg(Omega[2])) > HELI_GYRO_NO_MOVEMENT_VALUE ) {
        heli_movement_counter++;
        if( heli_movement_counter > 10 ) {
            heli_movement_counter = 10;
            if( heli_moving == FALSE ) {
                //SerPriln("Moving!");
                heli_startedMovingTime = currentTime;  // record time we started moving
                //heli_stoppedTime = 0;
            }
            heli_moving = TRUE;
        }
    }else{
        heli_movement_counter--;
        if( heli_movement_counter < -10 ) {
            heli_movement_counter = -10;
            if( heli_moving == TRUE ) {
                //SerPriln("stopped!");  
                heli_stoppedTime = currentTime;  // record time we started moving
                if( heli_startedMovingTime != 0 && (currentTime - heli_startedMovingTime) > HELI_SETTING_SAVE_AFTER_MILLIS_OF_MOVEMENT ) {  // if we've flown enough, save settings when we land
                    heli_saveSettings = TRUE;
                }
            }
            heli_moving = FALSE;
            // check if we should save values
            if( heli_saveSettings == TRUE && (currentTime - heli_stoppedTime) > HELI_SETTING_SAVE_AFTER_MILLIS_OF_STABILITY ) {
                //SerPriln("saving settings!");
//                heli_save_I_values();
                heli_saveSettings = FALSE;  // mark that we've stored settings
            }
        }        
    }    
    
    
#if LOG_ATTITUDE
    //Log_Write_PID(7,KP_QUAD_ROLL*err_roll,roll_I,roll_D,control_roll);
    //Log_Write_PID(8,KP_QUAD_PITCH*err_pitch,pitch_I,pitch_D,control_pitch);
    Log_Write_PID(9,Kp_RateYaw*err_heading,heading_I,0,control_yaw_rate);
    Log_Write_PID(10,KP_QUAD_YAW*err_yaw,yaw_I,yaw_D,control_yaw);
#endif
}

#endif  // #if AIRFRAME == HELI
