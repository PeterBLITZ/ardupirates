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

 File     : System.pde
 Version  : v1.0, Aug 27, 2010
 Author(s): ArduCopter Team
			 Ted Carancho (aeroquad), Jose Julio, Jordi Muñoz,
			 Jani Hirvinen, Ken McEwans, Roberto Navoni,          
			 Sandro Benigno, Chris Anderson
Author(s): 	ArduPirates deveopment team
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

// General Initialization for all APM electronics
void APM_Init() {

  // Setup proper PIN modes for our switched, LEDs, Relays etc on IMU Board
  pinMode(LED_Yellow,OUTPUT); // Yellow LED A  (PC1)
  pinMode(LED_Red,OUTPUT);    // Red LED    B  (PC2)
  pinMode(LED_Green,OUTPUT);  // Green LED  C  (PC0)
  pinMode(RELAY,OUTPUT);      // Relay output  (PL2)
  pinMode(SW1,INPUT);         // Switch SW1    (PG0)
  pinMode(SW2,INPUT);         // Switch SW2    (PG1)

  // Because DDRE and DDRL Ports are not included to normal Arduino Libraries, we need to
  // initialize them with a special command
  APMPinMode(DDRE,7,INPUT);   // DIP1, (PE7), Closest DIP to sliding SW2 switch
  APMPinMode(DDRE,6,INPUT);   // DIP2, (PE6)
  APMPinMode(DDRL,6,INPUT);   // DIP3, (PL6)
  APMPinMode(DDRL,7,INPUT);   // DIP4, (PL7), Furthest DIP from sliding SW2 switch


  /* ********************************************************* */
  ///////////////////////////////////////////////////////// 
  // Normal Initialization sequence starts from here.
  readUserConfig();          // Load user configurable items from EEPROM

  APM_RC.Init();             // APM Radio initialization

#if AIRFRAME == TRI
  // RC channels Initialization (Quad motors)  
  APM_RC.OutputCh(0,MIN_THROTTLE);  // Motors stoped
  APM_RC.OutputCh(1,MIN_THROTTLE);
  APM_RC.OutputCh(2,MIN_THROTTLE);
  APM_RC.OutputCh(3,CHAN_CENTER);   // Mid position
#endif  

#if AIRFRAME == QUAD
  // RC channels Initialization (Quad motors)  
  APM_RC.OutputCh(0,MIN_THROTTLE);  // Motors stoped
  APM_RC.OutputCh(1,MIN_THROTTLE);
  APM_RC.OutputCh(2,MIN_THROTTLE);
  APM_RC.OutputCh(3,MIN_THROTTLE);
#endif  

#if AIRFRAME == HELI
  // RC channels Initialization (heli servos)  
  APM_RC.OutputCh(0,CHANN_CENTER);  // mid position
  APM_RC.OutputCh(1,CHANN_CENTER);
  APM_RC.OutputCh(2,CHANN_CENTER);
  APM_RC.OutputCh(3,CHANN_CENTER);
#endif 
 
#if AIRFRAME == HEXA
  // RC channels Initialization (Hexa motors) - Motors stoped 
  APM_RC.OutputCh(0, MIN_THROTTLE);    // Left Motor CW
  APM_RC.OutputCh(1, MIN_THROTTLE);    // Left Motor CCW
  APM_RC.OutputCh(2, MIN_THROTTLE);    // Right Motor CW
  APM_RC.OutputCh(3, MIN_THROTTLE);    // Right Motor CCW    
  APM_RC.OutputCh(6, MIN_THROTTLE);    // Front Motor CW
  APM_RC.OutputCh(7, MIN_THROTTLE);    // Back Motor CCW    
#endif

#if AIRFRAME == OCTA
  // RC channels Initialization (Octa motors) - Motors stoped 
  APM_RC.OutputCh(0, MIN_THROTTLE);    // Left Motor CW
  APM_RC.OutputCh(1, MIN_THROTTLE);    // Left Motor CCW
  APM_RC.OutputCh(2, MIN_THROTTLE);    // Right Motor CW
  APM_RC.OutputCh(3, MIN_THROTTLE);    // Right Motor CCW    
  APM_RC.OutputCh(6, MIN_THROTTLE);    // Front Motor CW
  APM_RC.OutputCh(7, MIN_THROTTLE);    // Front Motor CCW    
  APM_RC.OutputCh(9, MIN_THROTTLE);    // Back Motor CW    // Connection PB5 on APM
  APM_RC.OutputCh(10, MIN_THROTTLE);   // Back Motor CCW   // Connection PE3 on APM  
#endif

  // Make sure that Relay is switched off.
  digitalWrite(RELAY,LOW);

  // Wiggle LEDs while ESCs are rebooting  
  FullBlink(50,20);

  adc.Init();            // APM ADC library initialization

#ifdef Use_DataFlash
  DataFlash.Init();      // DataFlash log initialization
#endif
  

//  GPS Setup
#ifdef IsGPS  
  Serial1.begin(38400);
  delay(20);
  gps.init();                // GPS Initialization
  delay(1000);
#endif

  // Read DIP Switches and other important values. DIP switches needs special functions to 
  // read due they are not defined as normal pins like other GPIO's are. 
#ifdef Use_Wii
  SwitchPosition.Dip1 = 1;
  SwitchPosition.Dip2 = 0;
  SwitchPosition.Dip3 = 0;
  SwitchPosition.Dip4 = 0;
  SwitchPosition.Sw1  = 0;
  SwitchPosition.Sw2  = 0;
#else
  //Most pins have pullup resistors so logic is inverted.
  //Keep electrical + software same to reduce confusion.
  SwitchPosition.Dip1 = !APMPinRead(PINE, 7);
  SwitchPosition.Dip2 = !APMPinRead(PINE, 6);
  SwitchPosition.Dip3 = !APMPinRead(PINL, 6);
  SwitchPosition.Dip4 = !APMPinRead(PINL, 7);
  SwitchPosition.Sw1  = !digitalRead(SW1);
  SwitchPosition.Sw2  = digitalRead(SW2);
#endif

  // Is CLI mode active or not, if it is fire it up and never return.
  if(SwitchPosition.Sw2)
    //RunCLI(); // removed Feb 5, 2011 [kidogo]
    // Btw.. We never return from this....



  flightOrientation = SwitchPosition.Dip1;    // DIP1 up (OFF)  = X-mode,         DIP1 down (ON) = + mode
  flightMode        = SwitchPosition.Dip3;    // DIP3 down (ON) = Acrobatic Mode, DIP3 up (OFF)  = Stable Mode.

 
   // Safety measure for Channel mids
  if(roll_mid < 1400 || roll_mid > 1600) roll_mid = 1500;
  if(pitch_mid < 1400 || pitch_mid > 1600) pitch_mid = 1500;
  if(yaw_mid < 1400 || yaw_mid > 1600) yaw_mid = 1500;

#if AIRFRAME == QUAD
  // RC channels Initialization (Quad motors)  
  APM_RC.OutputCh(0,MIN_THROTTLE);  // Motors stoped
  APM_RC.OutputCh(1,MIN_THROTTLE);
  APM_RC.OutputCh(2,MIN_THROTTLE);
  APM_RC.OutputCh(3,MIN_THROTTLE);
#endif  

#if AIRFRAME == HELI
  // RC channels Initialization (heli servos)  
  APM_RC.OutputCh(0,CHANN_CENTER);  // mid position
  APM_RC.OutputCh(1,CHANN_CENTER);
  APM_RC.OutputCh(2,CHANN_CENTER);
  APM_RC.OutputCh(3,CHANN_CENTER);
#endif 

#if AIRFRAME == HEXA
  // RC channels Initialization (Hexa motors) - Motors stoped 
  APM_RC.OutputCh(0, MIN_THROTTLE);    // Left Motor CW
  APM_RC.OutputCh(1, MIN_THROTTLE);    // Left Motor CCW
  APM_RC.OutputCh(2, MIN_THROTTLE);    // Right Motor CW
  APM_RC.OutputCh(3, MIN_THROTTLE);    // Right Motor CCW    
  APM_RC.OutputCh(6, MIN_THROTTLE);    // Front Motor CW
  APM_RC.OutputCh(7, MIN_THROTTLE);    // Back Motor CCW    
#endif
#if AIRFRAME == OCTA
  // RC channels Initialization (Octa motors) - Motors stoped 
  APM_RC.OutputCh(0, MIN_THROTTLE);    // Left Motor CW
  APM_RC.OutputCh(1, MIN_THROTTLE);    // Left Motor CCW
  APM_RC.OutputCh(2, MIN_THROTTLE);    // Right Motor CW
  APM_RC.OutputCh(3, MIN_THROTTLE);    // Right Motor CCW    
  APM_RC.OutputCh(6, MIN_THROTTLE);    // Front Motor CW
  APM_RC.OutputCh(7, MIN_THROTTLE);    // Front Motor CCW    
  APM_RC.OutputCh(9, MIN_THROTTLE);    // Back Motor CW    // Connection PB5 on APM
  APM_RC.OutputCh(10, MIN_THROTTLE);   // Back Motor CCW   // Connection PE3 on APM  
#endif

  // Initialise Wire library used by Magnetometer and Barometer
  Wire.begin();

#ifdef IsMAG
  if (MAGNETOMETER == 1) {
    AP_Compass.init(FALSE);  // I2C initialization
    AP_Compass.set_orientation(MAGORIENTATION);
    AP_Compass.set_offsets(mag_offset_x, mag_offset_y, mag_offset_z);
    AP_Compass.set_declination(ToRad(MAGCALIBRATION));
  }
#endif

#ifdef Use_DataFlash
  DataFlash.StartWrite(1);   // Start a write session on page 1
#endif

  // Proper Serial port/baud are defined on main .pde and then Arducopter.h with
  // Choises of Xbee or normal serial port
  SerBeg(SerBau);

  // Check if we enable the DataFlash log Read Mode (switch)
  // If we press switch 1 at startup we read the Dataflash eeprom
  /*while (digitalRead(SW1)==0)  // LEGACY remove soon by jp, 30-10-10
  {
    Serial.println("Entering Log Read Mode...");    // This will be obsole soon due moving to CLI system
    #ifdef Use_DataFlash
    Log_Read(1,1000);
    #endif
    delay(30000);
  }*/

  calibrateSensors();         // Calibrate neutral values of gyros  (in Sensors.pde)

  //  Neutro_yaw = APM_RC.InputCh(3); // Take yaw neutral radio value
#ifndef CONFIGURATOR
  for(i=0;i<6;i++)
  {
    SerPri("AN[]:");
    SerPrln(AN_OFFSET[i]);
  }
  SerPri("Yaw neutral value:");
  SerPri(yaw_mid);
#endif

#ifdef UseBMP
  APM_BMP085.Init(FALSE);
#endif

// Sonar for Altitude hold
#ifdef IsSONAR
  AP_RangeFinder_down.init(AP_RANGEFINDER_PITOT_TUBE, &adc);  AP_RangeFinder_down.set_orientation(AP_RANGEFINDER_ORIENTATION_DOWN);
  //AP_RangeFinder_down.init(AN5);  AP_RangeFinder_down.set_orientation(AP_RANGEFINDER_ORIENTATION_DOWN);
  sonar_threshold = AP_RangeFinder_down.max_distance * 0.8;
  sonar_status = SONAR_STATUS_OK;  // assume sonar is ok to start with
#endif

  // RangeFinders for obstacle avoidance
#ifdef IsRANGEFINDER  
  AP_RangeFinder_frontRight.init(AN5);  AP_RangeFinder_frontRight.set_orientation(AP_RANGEFINDER_ORIENTATION_FRONT_RIGHT);
  AP_RangeFinder_backRight.init(AN4);  AP_RangeFinder_backRight.set_orientation(AP_RANGEFINDER_ORIENTATION_BACK_RIGHT);
  AP_RangeFinder_backLeft.init(AN3);  AP_RangeFinder_backLeft.set_orientation(AP_RANGEFINDER_ORIENTATION_BACK_LEFT);
  AP_RangeFinder_frontLeft.init(AN2);  AP_RangeFinder_frontLeft.set_orientation(AP_RANGEFINDER_ORIENTATION_FRONT_LEFT);
#endif

  delay(1000);

#ifdef Use_DataFlash
  DataFlash.StartWrite(1);   // Start a write session on page 1
#endif

  // initialise helicopter
#if AIRFRAME == HELI
  heli_setup();
#endif

#ifdef IsAM
  // Switch Left & Right lights on
  digitalWrite(RI_LED, HIGH);
  digitalWrite(LE_LED, HIGH); 
#endif

// Camera Trigger
#ifdef UseCamTrigger
  APM_RC.OutputCh(CH_9, CAM_RELEASE);          // Servo removed from camera Trigger Button
#endif
}


