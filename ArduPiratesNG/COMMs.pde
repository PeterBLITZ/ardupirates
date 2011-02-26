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

 File     : COMMs.pde
 Version  : v1.0, Feb 05, 2011
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
 2011/04/02 [kidogo]; CLI and GCS are now both obsolete and have
			been consolidated into this new unit, COMMs.pde.
			Two main functions as per the former GCS.pde, one to
			read serial commands which can change values and 
			write EEPROM, one telemetry function to mainly provide
			flight information over a wireless link.
2011/05/02  [kidogo]; Expanded with a full menu with submenus to
			provide an easier way for users to find the right
			command. Serial commands are still in here like before.
			Also fixed excessive use of SerFlu() which would render
			serial comms unstable, loosing bytes and would 
			eventually cause comms to stop altogether - at one
			point a complete halt of processing on the APM was
			observed.
			Please refrain from using SerFlu().
2011/06/02	[kidogo]; Added a 8x8 menu matrix to accomodate
			most commands that are not used in flight.
			Menu driven setup to help newbies and pro's alike.
			Many options still can be accessed by their original
			serial command, to save Configurator capabilities.
			Moved all logic into functions at end of file.
			Added commenting function headers.
 
 
 * ************************************************************** *
 TODO:
 
 
 * ************************************************************** */
  
// Declare variables
boolean ShowMenu;
int SubMenu=0;

/*
***************************************************
*												  *
*		R E A D   S E R I A L   C O M M A N D     *
*												  *
***************************************************

Functions within this function may alter data, may
write values to EEPROM. Generally, these functions
should not be used while in flight.
												 */
		 
void readSerialCommand() {
	//if(!ShowMenu) Show_Menu();
	// Check for serial message
	if (SerAva()) {
		queryType = SerRea();
		switch (queryType) {
	
			// Menu navigation first
			case '?':	// Show main menu
			        SubMenu=0;
                                ShowMenu=false;
				Show_Menu();
				break;
			case '0':  // Always takes us back to main menu
                                if (!ShowMenu){
				if (SubMenu==0){				// Option 0 in main menu; save settings.
					ShowMenu=true;		
                                        SerPrln("Menu closed. Type '?' to re-open the CLI Menu.");
				} else {
					SubMenu=0;
					Show_Menu();
				}
                                }else{
                                  // Actions to take outside menu 
                                }
				break;
			case '1':
                                if (!ShowMenu){
				if (SubMenu==0){				// Takes us to Submenu 1
					SubMenu=1;
					Show_Menu();				
				} else if (SubMenu==1){			// SubMenu 1, choice 1
					Calibrate_Accel_Offset();
					Show_Menu_Prompt();
				}else if (SubMenu==2) {			// SubMenu 2, choice 1
					Receive_Camera_Mode();
					Show_Menu_Prompt();
				} else if (SubMenu==3) {		// SubMenu 3, choice 1 (and so on, and so on, you get it)
					Show_Settings();
					Show_Menu_Prompt();
				} else if (SubMenu==4) {		// SubMenu 4.1
					// PID ?
					Show_Menu_Prompt();
				} else if (SubMenu==5) {		// SubMenu 5.1
					Reset_Settings();
					Show_Menu_Prompt();
				} else if (SubMenu==6) {		// SubMenu 6.1
					//
					Show_Menu_Prompt();
				} else if (SubMenu==7) {		// SubMenu 7.1
					//
					Show_Menu_Prompt();
				} else if (SubMenu==8) {		// Menu 8.1
					//
					Show_Menu_Prompt();
				}
                                }else{
                                  // Actions to take outside menu
                                  Show_Transmitter_Calibration();
			          queryType = 'X';                                  
                                }
                                
                                break;
			case '2':
                                if (!ShowMenu){
				if (SubMenu==0){				// Takes us to Submenu 2
					SubMenu=2;
					Show_Menu();
				} else if (SubMenu==1) {		// SubMenu 1, choice 2
					Calibrate_Compass_Offset();	
					Show_Menu_Prompt();						
				} else if (SubMenu==2) {		// SubMenu 2, choice 2
					Show_Camera_Mode();
					Show_Menu_Prompt();
				} else if (SubMenu==3) {		// SubMenu 3, choice 2 (yes, you get it now ! LOL)
					Show_Sensor_Offsets();
					Show_Menu_Prompt();
				} else if (SubMenu==4) {		// SubMenu 4.2
					//	PID ?
					Show_Menu_Prompt();
				} else if (SubMenu==5) {		// SubMenu 5.2
                                        #ifdef Use_DataFlash					
                                        Log_Erase();
                                        #endif
					Show_Menu_Prompt();
				} else if (SubMenu==6) {		// SubMenu 6.2
					//
					Show_Menu_Prompt();
				} else if (SubMenu==7) {		// SubMenu 7.2
					//
					Show_Menu_Prompt();
				} else if (SubMenu==8) {		// Menu 8.2
					//
					Show_Menu_Prompt();
				}
                                }else{
                                  // Actions to take outside menu 
                                }
				break;
			case '3':
                                if (!ShowMenu){
				if (SubMenu==0){				// Takes us to submenu 3
					SubMenu=3;
					Show_Menu();
				} else if (SubMenu==1) {		// Menu 1.3
					Toggle_Magnetometer();
					Show_Menu_Prompt();
				} else if (SubMenu==2) {		// Menu 2.3
					Set_Camera_Smooth();	
					Show_Menu_Prompt();
				} else if (SubMenu==3) {		// Menu 3.3
					Show_Sensor_Offsets();
					Show_Menu_Prompt();
				} else if (SubMenu==4) {		// Menu 4.3
					// PID ?
					Show_Menu_Prompt();
				} else if (SubMenu==5) {		// Menu 5.3
					//
					Show_Menu_Prompt();
				} else if (SubMenu==6) {		// Menu 6.3
					//
					Show_Menu_Prompt();
				} else if (SubMenu==7) {		// Menu 7.3
					//
					Show_Menu_Prompt();
				} else if (SubMenu==8) {		// Menu 8.3
					//
					Show_Menu_Prompt();
				}
                                }else{
                                  // Actions to take outside menu 
                                }
				break;
			case '4':
                                if (!ShowMenu){
				if (SubMenu==0){				// Takes us to submenu 4
					SubMenu=4;
					Show_Menu();
				} else if (SubMenu==1) {		// Menu 1.4
					Calibrate_Throttle();
					Show_Menu_Prompt();
				} else if (SubMenu==2) {		// Menu 2.4
					Set_Camera_Trigger();		
					Show_Menu_Prompt();
				} else if (SubMenu==3) {		// Menu 3.4
					Show_Debug_Values();
					Show_Menu_Prompt();
				} else if (SubMenu==4) {		// Menu 4.4
					// PID ?
					Show_Menu_Prompt();
				} else if (SubMenu==5) {		// Menu 5.4
					//
					Show_Menu_Prompt();
				} else if (SubMenu==6) {		// Menu 6.4
					//
					Show_Menu_Prompt();
				} else if (SubMenu==7) {		// Menu 7.4
					//
					Show_Menu_Prompt();
				} else if (SubMenu==8) {		// Menu 8.4
					//
					Show_Menu_Prompt();
				}
                                }else{
                                  // Actions to take outside menu 
                                }
				break;
			case '5':
                                if (!ShowMenu){
				if (SubMenu==0){				// Takes us to submenu 5
					SubMenu=5;
					Show_Menu();
				} else if (SubMenu==1) {		// Menu 1.5
					Calibrate_ESC();
					Show_Menu_Prompt();
				} else if (SubMenu==2) {		// Menu 2.5
					//
					Show_Menu_Prompt();
				} else if (SubMenu==3) {		// Menu 3.5
					Show_GPS_Data();
					Show_Menu_Prompt();
				} else if (SubMenu==4) {		// Menu 4.5
					// PID ?
					Show_Menu_Prompt();
				} else if (SubMenu==5) {		// Menu 5.5
					//
					Show_Menu_Prompt();
				} else if (SubMenu==6) {		// Menu 6.5
					//
					Show_Menu_Prompt();
				} else if (SubMenu==7) {		// Menu 7.5
					//
					Show_Menu_Prompt();
				} else if (SubMenu==8) {		// Menu 8.5
					//
					Show_Menu_Prompt();
				}
                                }else{
                                  // Actions to take outside menu 
                                }
				break;
			case '6':
                                if (!ShowMenu){
				if (SubMenu==0){				// Takes us to submenu 6
					SubMenu=6;
					Show_Menu();
				} else if (SubMenu==1) {		// Menu 1.6
					Receive_Motor_Debug_Commands();
					Show_Menu_Prompt();
				} else if (SubMenu==2) {		// Menu 2.6
					//
					Show_Menu_Prompt();
				} else if (SubMenu==3) {		// Menu 3.6
                                        #ifdef Use_DataFlash					
                                        Log_Read(1,4000);
                                        #endif
					Show_Menu_Prompt();
				} else if (SubMenu==4) {		// Menu 4.6
					//
					Show_Menu_Prompt();
				} else if (SubMenu==5) {		// Menu 5.6
					//
					Show_Menu_Prompt();
				} else if (SubMenu==6) {		// Menu 6.6
					//
					Show_Menu_Prompt();
				} else if (SubMenu==7) {		// Menu 7.6
					//
					Show_Menu_Prompt();
				} else if (SubMenu==8) {		// Menu 8.6
					//
					Show_Menu_Prompt();
				}
                                }else{
                                  // Actions to take outside menu 
                                }
				break;
			case '7':
                                if (!ShowMenu){
				if (SubMenu==0){				// Takes us to submenu 7
					SubMenu=7;
					Show_Menu();
				} else if (SubMenu==1) {		// Menu 1.7
					RUN_Motors();
					Show_Menu_Prompt();
				} else if (SubMenu==2) {		// Menu 2.7
					//
					Show_Menu_Prompt();
				} else if (SubMenu==3) {		// Menu 3.7
					//
					Show_Menu_Prompt();
				} else if (SubMenu==4) {		// Menu 4.7
					//
					Show_Menu_Prompt();
				} else if (SubMenu==5) {		// Menu 5.7
					//
					Show_Menu_Prompt();
				} else if (SubMenu==6) {		// Menu 6.7
					//
					Show_Menu_Prompt();
				} else if (SubMenu==7) {		// Menu 7.7
					//
					Show_Menu_Prompt();
				} else if (SubMenu==8) {		// Menu 8.7
					//
					Show_Menu_Prompt();
				}
                                }else{
                                  // Actions to take outside menu 
                                }
				break;
                        case '8':
                                if (!ShowMenu){
				if (SubMenu==0){				// Takes us to submenu 8
					SubMenu=8;
					Show_Menu();
				} else if (SubMenu==1) {		// Menu 1.8
					RUN_Motors();
					Show_Menu_Prompt();
				} else if (SubMenu==2) {		// Menu 2.8
					//
					Show_Menu_Prompt();
				} else if (SubMenu==3) {		// Menu 3.8
					//
					Show_Menu_Prompt();
				} else if (SubMenu==4) {		// Menu 4.8
					//
					Show_Menu_Prompt();
				} else if (SubMenu==5) {		// Menu 5.8
					//
					Show_Menu_Prompt();
				} else if (SubMenu==6) {		// Menu 6.8
					//
					Show_Menu_Prompt();
				} else if (SubMenu==7) {		// Menu 7.8
					//
					Show_Menu_Prompt();
				} else if (SubMenu==8) {		// Menu 8.8
					//
					Show_Menu_Prompt();
				}
                                }else{
                                  // Actions to take outside menu 
                                }
				break;
                        case '9':
                                if (!ShowMenu){
                                if (SubMenu!=0){
                                  SubMenu=0;
                                  Show_Menu();
                                } else {
                                  writeUserConfig();
				  SerPrln("Saved settings to EEPROM.");	
				  delay(2000);
                                  Show_Menu();
                                }
                                }else{
                                  // Actions to take outside menu 
                                }
                                break;
                        default:
                        // do nothing
                        delay(10);
                        }
	//SerFlu();
	}
}

//******************************************************************************************************************
// TELEMETRY FUNCTIONS BELOW 
//******************************************************************************************************************
/*
***************************************************
*												  *
*		S E R I A L   T E L E M E T R Y           *
*												  *
*												  *
***************************************************
         					 */

void sendSerialTelemetry() {
	float aux_float[3]; // used for sensor calibration
	switch (queryType) {
  
		// Choices are made here, moved all actual logic into 
		// functions more towards the bottom (kidogo, Feb 6, 2011)
		case 'A': 						// Stable PID
			Receive_Stable_PID();
			break;
		case 'B': 						// Show roll, pitch and yaw PID values
			Show_Stable_PIDs();
			queryType = 'X';
			break;
		case 'C': 						// Receive GPS PID
			Receive_GPS_PID();
			break;
		case 'D': 						// Show GPS PID
			Show_GPS_PIDs();
			queryType = 'X';
			break;
		case 'E': 						// Receive altitude PID
			Receive_Altitude_PID();
			break;
		case 'F': 						// Show altitude PID
			Show_Altitude_PIDs();
			queryType = 'X';
			break;
		case 'G': 						// Receive drift correction PID
			Receive_Drift_PID();
			break;
		case 'H': 						// Show drift correction PID
			Show_Drift_Correction_PIDs();
			queryType = 'X';
			break;
		case 'I': 						// Receive sensor offset
			Receive_Sensor_Offsets();
			break;
		case 'J': 						// Show sensor offset
			Show_Sensor_Offsets();
			queryType = 'X';
			break;
		case 'K': 						// Camera mode
			Receive_Camera_Mode();        
			break;      
		case 'L': 						// Camera settings
			Show_Camera_Mode();
			queryType = 'X';
			break;
		case 'M': 						// Receive debug motor commands
			Receive_Motor_Debug_Commands();
			break;     
		case 'N': 						// * SPARE *
			queryType = 'X';
			break;
		case 'O': 						// Rate Control PID
			Receive_Rate_Control_PID();
			break;
		case 'P': 						// Show rate control PID
			Show_Rate_Control_PIDs();
			queryType = 'X';
			break;
		case 'Q': 						// Show sensor data
			Show_Sensor_Data();
			break;
		case 'R': 						// * SPARE *
			break;
		case 'S': 						// Show all flight data
			Show_Flight_Data();
			break;
		case 'T': 						// Show platform information
			Show_Platform_Info();
			queryType = 'X';
			break;
		case 'U': 						// Show receiver values
			Show_Receiver_Values();
			break;
		case 'V': 						// Receive transmitter calibration values
			Receive_Transmitter_Calibration();
			break;
		case 'W':
			Set_SonarAndObstacleAvoidance_PIDs();
			break;
		case 'X': 						// Stop sending messages
			break;
		case 'Y':						// * SPARE *
			break;
		case 'Z':						// * SPARE *
			break;
		case 'v': 						// Show transmitter calibration values
			Show_Transmitter_Calibration();
			queryType = 'X';
			break;
		case '!': 						// Show flight software version
			SerPrln(VER);
			queryType = 'X';
			break;
		case '#':  						// Show Jani's debugs
			Show_Debug_Values();
			break;
		#ifdef IsGPS
		case '*':  						// Show GPS data
			Show_GPS_Data();
			break;
		case '.': 						// Modify GPS settings, print directly to GPS Port
			Serial1.print("$PGCMD,16,0,0,0,0,0*6A\r\n");
			break;
		#endif    
	  
		#if (defined(SerXbee) && defined(Use_PID_Tuning))
		case 'o': 						// Switch PID tuning ON
			PID_Tuning_ON();
			queryType = 'X';
			break;
		case 'f': 						// Switch PID tuning OFF
			PID_Tuning_OFF();
			queryType = 'X';
			break;
		case 'r': 						// Switch to Pitch and Roll, PID tuning
			PID_Tuning_Tune_Roll_Pitch();
			queryType = 'X';
			break;
		case 'w': 						// Switch to Yaw PID tuning
			PID_Tuning_Tune_Yaw();
			queryType = 'X';
			break;
		case 'b': 						// Switch to Baro PID tuning
			PID_Tuning_Tune_Baro();
			queryType = 'X';
			break;
		case 's': 						// Switch to Sonar PID tuning
			PID_Tuning_Tune_Sonar();
			queryType = 'X';
			break;
		case 'g': 						// Switch to GPS PID tuning
			PID_Tuning_Tune_GPS();
			queryType = 'X';
			break;
		case 'p': 						// P factor of PID tuning
			P_PID = 1;
			I_PID = 0;
			D_PID = 0;
			SerPrln("P factor of  PID set");
			queryType = 'X';
			break;
		case 'i': 						// I factor of PID tuning
			P_PID = 0;
			I_PID = 1;
			D_PID = 0;
			SerPrln("I factor of  PID set");
			queryType = 'X';
			break;
		case 'd': 						// D factor of PID tuning
			P_PID = 0;
			I_PID = 0;
			D_PID = 1;
			SerPrln("D factor of  PID set");
			queryType = 'X';
			break;
		case 'x': 						// Switch to Accelerometer Roll Offset Tuning
			PID_Tuning_Tune_Accel_Roll();
			queryType = 'X';
			break;
		case 'y': 						// Switch to Accelerometer Pitch Offset Tuning
			PID_Tuning_Tune_Accel_Pitch();
			queryType = 'X';
			break;
		case 'a': 						// Switch to Camera Smooth Pitch tuning
			PID_Tuning_Tune_Camera_Pitch();
			queryType = 'X';
			break;
		case 'e': 						// Switch to Camera Smooth Roll tuning
			PID_Tuning_Tune_Camera_Roll();
			queryType = 'X';
			break;
		case 'c': 						// Switch to Camera Roll Centre tuning
			PID_Tuning_Tune_Camera_Roll_Centre();
			queryType = 'X';
			break;
		case 'h': 						// Switch to Camera Focus Position tuning - using servo
			PID_Tuning_Tune_Camera_Focus_Position();
			queryType = 'X';
			break;
		case 't': 						// Switch to Camera Trigger Position tuning - using servo
			PID_Tuning_Tune_Camera_Trigger_Position();
			queryType = 'X';
			break;
		case 'j': 						// Switch to Camera Release Position tuning - using servo
			PID_Tuning_Tune_Camera_Release_Position();
			queryType = 'X';
			break;
		#endif	// (defined(SerXbee) && defined(Use_PID_Tuning)) 
                default:
                // do nothing
                delay(10);
                
	}
  //SerFlu();
}

/****************************************************
  PID TUNING - TURN ON
****************************************************/
void PID_Tuning_ON() {
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
}

/****************************************************
  PID TUNING - TURN OFF
****************************************************/
void PID_Tuning_OFF() {
    ON_PID = 0;    
    SerPrln("PID Tuning OFF"); 
}

/****************************************************
  PID TUNING - TUNE ROLL AND PITCH
****************************************************/
void PID_Tuning_Tune_Roll_Pitch() {
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
}

/****************************************************
  PID TUNING - TUNE YAW
****************************************************/

void PID_Tuning_Tune_Yaw() {
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
}

/****************************************************
  PID TUNING - TUNE BARO
****************************************************/
void PID_Tuning_Tune_Baro() {
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
}

/****************************************************
  PID TUNING - TUNE SONAR
****************************************************/
void PID_Tuning_Tune_Sonar() {
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
}

/****************************************************
  PID TUNING - TUNE GPS
****************************************************/
void PID_Tuning_Tune_GPS() {
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
}

/****************************************************
  PID TUNING - TUNE ACCEL ROLL
****************************************************/
void PID_Tuning_Tune_Accel_Roll() {
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
}

/****************************************************
  PID TUNING - TUNE ACCEL PITCH
****************************************************/
void PID_Tuning_Tune_Accel_Pitch() {
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
}

/****************************************************
  PID TUNING - TUNE CAMERA PITCH
****************************************************/
void PID_Tuning_Tune_Camera_Pitch(){
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
}

/****************************************************
  PID TUNING - TUNE CAMERA ROLL
****************************************************/
void PID_Tuning_Tune_Camera_Roll() {
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
}

/****************************************************
  PID TUNING - TUNE CAMERA ROLL CENTRE
****************************************************/
void PID_Tuning_Tune_Camera_Roll_Centre() {
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
}

/****************************************************
  PID TUNING - TUNE CAMERA FOCUS POSITION
****************************************************/
void PID_Tuning_Tune_Camera_Focus_Position() {
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
}

/****************************************************
  PID TUNING - TUNE CAMERA TRIGGER POSITION
****************************************************/
void PID_Tuning_Tune_Camera_Trigger_Position() {
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
}

/****************************************************
  PID TUNING - CAMERA RELEASE POSITION
****************************************************/
void PID_Tuning_Tune_Camera_Release_Position() {
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
}

/****************************************************
  SHOW GPS DATA
****************************************************/
void Show_GPS_Data() {
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
}

/****************************************************
  SHOW DEBUG VALUES
****************************************************/
void Show_Debug_Values() {
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
}

/****************************************************
  SHOW TRANSMITTER CALIBRATION VALUES
****************************************************/
void Show_Transmitter_Calibration() {
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
}

/****************************************************
  SHOW RECEIVER VALUES
****************************************************/
void Show_Receiver_Values() {
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
}

/****************************************************
  SHOW PLATFORM INFORMATION
****************************************************/
void Show_Platform_Info() {
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
    #if AIRFRAME == OCTA  
      SerPriln("Airframe = Octa");
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
}

/****************************************************
  SHOW FLIGHT DATA
****************************************************/
void Show_Flight_Data() {
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
}

/****************************************************
  SHOW SENSOR DATA
****************************************************/
void Show_Sensor_Data() {
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
    SerPri(degrees(yaw));
    comma();
    SerPrln(press_baro_altitude);
	
}

/****************************************************
  SHOW CAMERA MODE
****************************************************/
void Show_Camera_Mode() {

    SerPri("Camera mode: ");
    SerPri(cam_mode, DEC);
    tab();
    SerPri("Battery low: ");
    SerPri(BATTLOW, DEC);
    tab();
    SerPrln();
}

/****************************************************
  SHOW SENSOR OFFSETS
****************************************************/
void Show_Sensor_Offsets() {
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
}

/****************************************************
  SHOW SETTINGS
****************************************************/
void Show_Settings() {  
    // Reading current EEPROM values

    SerPrln("ArduPirate - Current settings");
    SerPrln("--------------------------------------");
    SerPri("Firmware                 : ");
    SerPri(VER);
    SerPrln();
    SerPrln();
    readUserConfig();
    delay(50);
    SerPri("Magnetom. offsets (x,y,z): ");
    SerPri(mag_offset_x);
    cspc();
    SerPri(mag_offset_y);
    cspc();
    SerPri(mag_offset_z);
    SerPrln();

    SerPri("Accel offsets (x,y,z)    : ");
    SerPri(acc_offset_x);
    cspc();
    SerPri(acc_offset_y);
    cspc();
    SerPri(acc_offset_z);
    SerPrln();

    SerPri("Min Throttle             : ");
    SerPrln(MIN_THROTTLE);

    SerPri("Magnetometer             : ");
    if (MAGNETOMETER==0) {
      SerPrln("Disabled");
    }else{
      SerPrln("Enabled");
    }
	
    SerPri("Camera mode	             : ");
    SerPrln(cam_mode, DEC);
    Show_Camera_Smooth();
    Show_Camera_Trigger();

    #if AIRFRAME == QUAD  
      SerPriln("Airframe 	             : Quad");
      #ifdef FLIGHT_MODE_X_45Degree
        SerPri("Flight orientation       : ");
	if(SW_DIP1) {
          SerPrln("X mode_45Degree (APM front pointing towards Front motor)");
	} else {
	  SerPrln("+ mode");
	}
      #endif 
      #ifdef FLIGHT_MODE_X
	SerPri("Flight orientation       : ");
	SerPrln("X mode (APM front between Front and Right motor) DIP1 not applicable");
      #endif
    #endif
    #if AIRFRAME == HEXA  
      SerPriln("Airframe 	             : Hexa");
    #endif

    Show_SonarAndObstacleAvoidance_PIDs();
    SerPrln(); 
}

/****************************************************
  CALIBRATE THROTTLE
****************************************************/
void Calibrate_Throttle() {
  int tmpThrottle = 1200;

  SerPrln("Move your throttle to MIN, reading starts in 3 seconds");
  delay(1000);
  SerPrln("2. ");
  delay(1000);
  SerPrln("1. ");
  delay(1000);
  SerPrln("Reading Throttle value, hit enter to exit");

  SerFlu();
  delay(50);
  while(1) {
    ch_throttle = APM_RC.InputCh(CH_THROTTLE);
    SerPri("Throttle channel: ");
    SerPrln(ch_throttle);
    if(tmpThrottle > ch_throttle) tmpThrottle = ch_throttle;
    delay(50);
    if(SerAva() > 0){
      break; 
    }
  }

  SerPrln();
  SerPri("Lowest throttle value: ");
  SerPrln(tmpThrottle);
  SerPrln();
  SerPrln("Saving MIN_THROTTLE to EEPROM");
  writeEEPROM(tmpThrottle, MIN_THROTTLE_ADR);
  delay(200);
  SerPrln("Saved..");
  SerPrln();
}

/****************************************************
  CALIBRATE ESCS
****************************************************/
void Calibrate_ESC() {
	SerPrln("Disconnect your battery if you have it connected, keep your USB cable/Xbee connected!");
	SerPrln("After battery is disconnected hit enter key and wait more instructions:");
	SerPrln("As safety measure, unmount all your propellers before continuing!!");

	WaitSerialEnter();      

	SerPrln("Move your Throttle to maximum and connect your battery. ");
	SerPrln("after you hear beep beep tone, move your throttle to minimum and");
	SerPrln("hit enter after you are ready to disarm motors.");
	SerPrln("Arming now all motors");
	delay(500);
	SerPrln("Motors: ARMED");
	delay(200);
	SerPrln("Connect your battery and let ESCs to reboot!");
	while(1) {
		ch_throttle = APM_RC.InputCh(CH_THROTTLE);  
		#if AIRFRAME == QUAD
			APM_RC.OutputCh(0, ch_throttle);    // Right motor
			APM_RC.OutputCh(1, ch_throttle);    // Left motor
			APM_RC.OutputCh(2, ch_throttle);    // Front motor
			APM_RC.OutputCh(3, ch_throttle);    // Back motor   
		#endif
		#if AIRFRAME == HEXA
			APM_RC.OutputCh(0, ch_throttle);    // Left Motor CW
			APM_RC.OutputCh(1, ch_throttle);    // Left Motor CCW
			APM_RC.OutputCh(2, ch_throttle);    // Right Motor CW
			APM_RC.OutputCh(3, ch_throttle);    // Right Motor CCW    
			APM_RC.OutputCh(6, ch_throttle);    // Front Motor CW
			APM_RC.OutputCh(7, ch_throttle);    // Back Motor CCW    
		#endif
		#if AIRFRAME == OCTA
			APM_RC.OutputCh(0, ch_throttle);    // Left Motor CW
			APM_RC.OutputCh(1, ch_throttle);    // left Motor CCW
			APM_RC.OutputCh(2, ch_throttle);    // Right Motor CW
			APM_RC.OutputCh(3, ch_throttle);    // Right Motor CCW
			APM_RC.OutputCh(6, ch_throttle);    // Front Motor CW
			APM_RC.OutputCh(7, ch_throttle);    // Front Motor CCW
			APM_RC.OutputCh(9, ch_throttle);    // Back Motor CW
			APM_RC.OutputCh(10, ch_throttle);   // Back Motor CCW
		#endif
		// InstantPWM => Force inmediate output on PWM signals
		#if AIRFRAME == QUAD   
			// InstantPWM
			APM_RC.Force_Out0_Out1();
			APM_RC.Force_Out2_Out3();
		#endif
		#if ((AIRFRAME == HEXA) || (AIRFRAME == OCTA))
			// InstantPWM
			APM_RC.Force_Out0_Out1(); //Fast PWM at channels 0, 1 & 8
			APM_RC.Force_Out2_Out3(); //Fast PWM at channels 2, 3 & 9
			APM_RC.Force_Out6_Out7(); //Fast PWM at channels 6, 7 & 10
		#endif
		delay(20);
		if(SerAva() > 0){
			break; 
		}
	}
	#if AIRFRAME == QUAD
		APM_RC.OutputCh(0, 900);    // Right motor
		APM_RC.OutputCh(1, 900);    // Left motor
		APM_RC.OutputCh(2, 900);    // Front motor
		APM_RC.OutputCh(3, 900);    // Back motor   
	#endif
	#if AIRFRAME == HEXA
		APM_RC.OutputCh(0, 900);    // Left Motor CW
		APM_RC.OutputCh(1, 900);    // Left Motor CCW
		APM_RC.OutputCh(2, 900);    // Right Motor CW
		APM_RC.OutputCh(3, 900);    // Right Motor CCW    
		APM_RC.OutputCh(6, 900);    // Front Motor CW
		APM_RC.OutputCh(7, 900);    // Back Motor CCW    
	#endif 
	#if AIRFRAME == OCTA
		APM_RC.OutputCh(0, 900);    // Left Motor CW
		APM_RC.OutputCh(1, 900);    // Left Motor CCW
		APM_RC.OutputCh(2, 900);    // Right Motor CW
		APM_RC.OutputCh(3, 900);    // Right Motor CCW
		APM_RC.OutputCh(6, 900);    // Front Motor CW
		APM_RC.OutputCh(7, 900);    // Front Motor CCW
		APM_RC.OutputCh(9, 900);    // Back Motor CW
		APM_RC.OutputCh(10, 900);   // Back Motor CCW
	#endif
	#if AIRFRAME == QUAD   
		// InstantPWM
		APM_RC.Force_Out0_Out1();
		APM_RC.Force_Out2_Out3();
	#endif
	#if ((AIRFRAME == HEXA) || (AIRFRAME == OCTA))
		// InstantPWM
		APM_RC.Force_Out0_Out1(); //Fast PWM at channels 0, 1 & 8
		APM_RC.Force_Out2_Out3(); //Fast PWM at channels 2, 3 & 9
		APM_RC.Force_Out6_Out7(); //Fast PWM at channels 6, 7 & 10
	#endif

	SerPrln("Motors: DISARMED");
	SerPrln();
}

/****************************************************
  RUN MOTORS WITH STICKS
****************************************************/
void RUN_Motors() {
	long run_timer;
	byte motor;

	SerPrln("Move your ROLL/PITCH Stick to up/down, left/right to start");
	SerPrln("corresponding motor. Motor will pulse slowly! (20% Throttle)");
	SerPrln("SAFETY!! Remove all propellers before doing stick movements");
	SerPrln();
	SerPrln("Exit from test by hiting Enter");
	SerPrln();

	SerFlu();
	delay(50);
	while(1) {

		ch_roll = APM_RC.InputCh(CH_ROLL);
		ch_pitch = APM_RC.InputCh(CH_PITCH);

		if(ch_roll < 1400) {
			SerPrln("Left Motor");
			OutMotor(1);
			delay(500);
		}
		if(ch_roll > 1600) {
			SerPrln("Right Motor");
			OutMotor(0);
			delay(500);
		}
		if(ch_pitch < 1400) {
			SerPrln("Front Motor");
			OutMotor(2);
			delay(500);
		}
		if(ch_pitch > 1600) {
			SerPrln("Rear Motor");
			OutMotor(3);
			delay(500);
		}

		// Shuting down all motors
		APM_RC.OutputCh(0, 900);
		APM_RC.OutputCh(1, 900);
		APM_RC.OutputCh(2, 900);
		APM_RC.OutputCh(3, 900);
		APM_RC.Force_Out0_Out1();
		APM_RC.Force_Out2_Out3();

		delay(100);
		//    delay(20);
		if(SerAva() > 0){
			SerFlu();
			delay(50);
			SerPrln("Exiting motor/esc tester...");
			break; 
		}  
	} 
}

/****************************************************
  MOTOR COMMANDER
****************************************************/
void OutMotor(byte motor_id) {
	APM_RC.OutputCh(motor_id, 1200);
	APM_RC.Force_Out0_Out1();
	APM_RC.Force_Out2_Out3();
}

/****************************************************
  RESET SETTINGS
****************************************************/
byte Reset_Settings() {
	int c;
  
	SerPrln("Reseting EEPROM to default!"); 
	delay(500);
	SerFlu();
	delay(500);
	SerPrln("Hit 'Y' to reset factory settings, any other and you will return to main menu!");
	do {
		c = SerRea();
	} 
	while (-1 == c);

	if (('y' != c) && ('Y' != c)) {
		SerPrln("EEPROM has not reseted!");
		SerPrln("Returning to main menu.");
		return(-1);
	}

	SerPrln("Reseting to factory settings!");
	defaultUserConfig();
	delay(200);
	SerPrln("Saving to EEPROM");
	writeUserConfig();
	SerPrln("Done..");
}

/****************************************************
  SHOW GPS HOLD MODE PIDS
****************************************************/
void Show_GPS_PIDs() {
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
}

/****************************************************
  SHOW STABLE MODE PIDS
****************************************************/
void Show_Stable_PIDs() {
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
}

/****************************************************
  SHOW ALTITUDE HOLD MODE PIDS
****************************************************/
void Show_Altitude_PIDs() {
    SerPri(KP_ALTITUDE, 3);
    comma();
    SerPri(KI_ALTITUDE, 3);
    comma();
    SerPrln(KD_ALTITUDE, 3);
}

/****************************************************
  SHOW DRIFT CORRECTION PIDS
****************************************************/
void Show_Drift_Correction_PIDs() {
    SerPri(Kp_ROLLPITCH, 4);
    comma();
    SerPri(Ki_ROLLPITCH, 7);
    comma();
    SerPri(Kp_YAW, 4);
    comma();
    SerPrln(Ki_YAW, 6);
}

/****************************************************
  SHOW RATE CONTROL PIDS
****************************************************/
void Show_Rate_Control_PIDs() {
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
}

/****************************************************
  SHOW SONAR AND OBST. AVOIDANCE PIDS
****************************************************/
void Show_SonarAndObstacleAvoidance_PIDs() {
	SerPri("\tSonar PID: ");
	SerPri(KP_SONAR_ALTITUDE,3); cspc();
	SerPri(KI_SONAR_ALTITUDE,3); cspc();
	SerPrln(KD_SONAR_ALTITUDE,3);
	SerPri("\tObstacle SafetyZone: ");
	SerPrln(RF_SAFETY_ZONE,3);
	SerPri("\tRoll PID: ");
	SerPri(KP_RF_ROLL,3); cspc();
	SerPri(KI_RF_ROLL,3); cspc();
	SerPrln(KD_RF_ROLL,3);
	SerPri("\tPitch PID: ");
	SerPri(KP_RF_PITCH,3); cspc();
	SerPri(KI_RF_PITCH,3); cspc();
	SerPri(KD_RF_PITCH,3); 
	SerPrln();
	SerPri("\tMaxAngle: "); 
	SerPri(RF_MAX_ANGLE);   
	SerPrln();  
}

/****************************************************
  SAVE SONAR AND OBST. AVOID. PIDS TO EEPROM
****************************************************/
void Save_SonarAndObstacleAvoidancePIDs_toEEPROM() {  
	writeEEPROM(KP_RF_ROLL,KP_RF_ROLL_ADR);
	writeEEPROM(KI_RF_ROLL,KI_RF_ROLL_ADR);  
	writeEEPROM(KD_RF_ROLL,KD_RF_ROLL_ADR);
	writeEEPROM(KP_RF_PITCH,KP_RF_PITCH_ADR);
	writeEEPROM(KI_RF_PITCH,KI_RF_PITCH_ADR);  
	writeEEPROM(KD_RF_PITCH,KD_RF_PITCH_ADR);
	writeEEPROM(RF_MAX_ANGLE,RF_MAX_ANGLE_ADR);
	writeEEPROM(RF_SAFETY_ZONE,RF_SAFETY_ZONE_ADR);
	writeEEPROM(KP_SONAR_ALTITUDE,KP_SONAR_ALTITUDE_ADR);
	writeEEPROM(KI_SONAR_ALTITUDE,KI_SONAR_ALTITUDE_ADR);
	writeEEPROM(KD_SONAR_ALTITUDE,KD_SONAR_ALTITUDE_ADR);    
}

/****************************************************
  SET SONAR AND OBST. AVOIDANCE PIDS
****************************************************/ 
void Set_SonarAndObstacleAvoidance_PIDs() {
	float tempVal1, tempVal2, tempVal3;
	int saveToEeprom = 0;
	// Display current PID values
	SerPrln("Sonar and Obstacle Avoidance:");
	Show_SonarAndObstacleAvoidance_PIDs();
	SerPrln();
  
	// SONAR PIDs
	SerFlu();
	delay(50);
	SerPri("Enter Sonar P;I;D; values or 0 to skip: ");
	while( !SerAva() );  // wait until user presses a key
	tempVal1 = readFloatSerial();
	tempVal2 = readFloatSerial();
	tempVal3 = readFloatSerial();
	if( tempVal1 != 0 || tempVal2 != 0 || tempVal3 != 0 ) {
		KP_SONAR_ALTITUDE = tempVal1;
		KI_SONAR_ALTITUDE = tempVal2;
		KD_SONAR_ALTITUDE = tempVal3;
		SerPrln();
		SerPri("P:");
		SerPri(KP_SONAR_ALTITUDE,3);
		SerPri("\tI:");
		SerPri(KI_SONAR_ALTITUDE,3);
		SerPri("\tD:");
		SerPri(KD_SONAR_ALTITUDE,3);
		saveToEeprom = 1;
	}
	SerPrln();    
  
	// SAFETY ZONE
	SerFlu();
	delay(50);
	SerPri("Enter Safety Zone (in cm) or 0 to skip: ");
	while( !SerAva() );  // wait until user presses a key
	tempVal1 = readFloatSerial();
	if( tempVal1 >= 20 && tempVal1 <= 700 ) {
		RF_SAFETY_ZONE = tempVal1;
		SerPri("SafetyZone: ");
		SerPri(RF_SAFETY_ZONE,3);
		saveToEeprom = 1;   
	}
	SerPrln();      
      
	// ROLL PIDs
	SerFlu();
	delay(50);
	SerPri("Enter Roll P;I;D; values or 0 to skip: ");
	while( !SerAva() );  // wait until user presses a key
	tempVal1 = readFloatSerial();
	tempVal2 = readFloatSerial();
	tempVal3 = readFloatSerial();
	if( tempVal1 != 0 || tempVal2 != 0 || tempVal3 != 0 ) {
		KP_RF_ROLL = tempVal1;
		KI_RF_ROLL = tempVal2;
		KD_RF_ROLL = tempVal3;
		SerPrln();
		SerPri("P:");
		SerPri(KP_RF_ROLL,3);
		SerPri("\tI:");
		SerPri(KI_RF_ROLL,3);
		SerPri("\tD:");
		SerPri(KD_RF_ROLL,3);
		saveToEeprom = 1;
	}
	SerPrln();  
  
	// PITCH PIDs
	SerFlu();
	delay(50);
	SerPri("Enter Pitch P;I;D; values or 0 to skip: ");
	while( !SerAva() );  // wait until user presses a key
	tempVal1 = readFloatSerial();
	tempVal2 = readFloatSerial();
	tempVal3 = readFloatSerial();
	if( tempVal1 != 0 || tempVal2 != 0 || tempVal3 != 0 ) {
		KP_RF_PITCH = tempVal1;
		KI_RF_PITCH = tempVal2;
		KD_RF_PITCH = tempVal3;
		SerPrln();
		SerPri("P:");
		SerPri(KP_RF_PITCH,3);
		SerPri("\tI:");
		SerPri(KI_RF_PITCH,3);
		SerPri("\tD:");
		SerPri(KD_RF_PITCH,3);      
		saveToEeprom = 1;
	}
	SerPrln();  
  
	// Max Angle
	SerFlu();
	delay(50);
	SerPri("Enter Max Angle or 0 to skip: ");
	while( !SerAva() );  // wait until user presses a key
	tempVal1 = readFloatSerial();
	SerPrln(tempVal1);
	if( tempVal1 > 0 && tempVal1 < 90 ) {
		RF_MAX_ANGLE = tempVal1;
		SerPrln();
		SerPri("MaxAngle: "); 
		SerPri(RF_MAX_ANGLE);     
		saveToEeprom = 1;
	}
	SerPrln();   
  
	// save to eeprom
	if( saveToEeprom == 1 ) {
		SerPrln("Obstacle Avoidance:");
		Show_SonarAndObstacleAvoidance_PIDs();
		SerPrln();
		Save_SonarAndObstacleAvoidancePIDs_toEEPROM();
		SerPrln("Saved to EEPROM");
		SerPrln();
	}else{
		SerPrln("No changes. Nothing saved to EEPROM");
		SerPrln();
	}
}

/****************************************************
  SHOW CAMERA SMOOTH VALUES
****************************************************/
void Show_Camera_Smooth() {
	// Display camera values
	SerPri("Current camera smooth values (Tilt, Roll, Center): "); 
	SerPri(CAM_SMOOTHING); cspc();
	SerPri(CAM_SMOOTHING_ROLL); cspc();
	SerPriln(CAM_CENT);
}

/****************************************************
  SAVE CAMERA SMOOTH VALUES TO EEPROM
****************************************************/
void Save_Camera_Smooth_toEEPROM() {  
	writeEEPROM(CAM_SMOOTHING     , CAM_SMOOTHING_ADR);
	writeEEPROM(CAM_SMOOTHING_ROLL, CAM_SMOOTHING_ROLL_ADR);
	writeEEPROM(CAM_CENT          , CAM_CENT_ADR);
}

/****************************************************
  SHOW CAMERA TRIGGER POSITIONS
****************************************************/
void Show_Camera_Trigger() {
	// Display camera Trigger values
	SerPri("Camera Trigger values (Focus, Trigger, Release): "); 
	SerPri(CAM_FOCUS); cspc();
	SerPri(CAM_TRIGGER); cspc();
	SerPriln(CAM_RELEASE);
}

/****************************************************
  SAVE CAMERA TRIGGER POSITIONS TO EEPROM
****************************************************/
void Save_Camera_Trigger_toEEPROM() {  
	writeEEPROM(CAM_FOCUS  , CAM_FOCUS_ADR);
	writeEEPROM(CAM_TRIGGER, CAM_TRIGGER_ADR);
	writeEEPROM(CAM_RELEASE, CAM_RELEASE_ADR);
}

/****************************************************
  SET CAMERA TRIGGER POSITIONS
****************************************************/
void Set_Camera_Trigger() {
	float tempVal;
	int saveToEeprom = 0;

	SerPrln("Camera Trigger values:");
	Show_Camera_Trigger();
	SerPrln();
	// Tilt Smooth
	SerFlu();
	delay(50);
	SerPriln("Enter Camera Trigger Focus Value or 0 to Skip: ");
	while( !SerAva() );  // wait until user presses a key
	tempVal = readFloatSerial();
	if (tempVal != 0) {
		CAM_FOCUS = tempVal;
		SerPri("Camera Focus: ");
		SerPriln(CAM_FOCUS);
		saveToEeprom = 1;   
	}
	SerPrln();      

    // Roll Smooth
	SerFlu();
	delay(50);
	SerPriln("Enter Camera Trigger Value or 0 to Skip: ");
	while( !SerAva() );  // wait until user presses a key
	tempVal = readFloatSerial();
	if (tempVal != 0) {
		CAM_TRIGGER = tempVal;
		SerPri("Camera Trigger: ");
		SerPriln(CAM_TRIGGER);
		saveToEeprom = 1;   
	}
	SerPrln();      

    // Roll Smooth
	SerFlu();
	delay(50);
	SerPriln("Enter Camera Release Value or 0 to Skip: ");
	while( !SerAva() );  // wait until user presses a key
	tempVal = readFloatSerial();
	if (tempVal != 0) {
		CAM_RELEASE = tempVal;
		SerPri("Camera Release: ");
		SerPriln(CAM_RELEASE);
		saveToEeprom = 1;   
	}
	SerPrln();     
  
    // save to eeprom
	if( saveToEeprom == 1 ) {
		Show_Camera_Trigger();
		SerPrln();
		Save_Camera_Trigger_toEEPROM();
		SerPrln("Saved to EEPROM");
		SerPrln();
	}else{
		SerPrln("No changes. Nothing saved to EEPROM");
		SerPrln();
	}
}

/****************************************************
  RECEIVE STABLE MODE PID VALUES
****************************************************/
void Receive_Stable_PID() {
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
}

/****************************************************
  RECEIVE GPS HOLD MODE PID VALUES
****************************************************/
void Receive_GPS_PID() {
    KP_GPS_ROLL = readFloatSerial();
    KI_GPS_ROLL = readFloatSerial();
    KD_GPS_ROLL = readFloatSerial();
    KP_GPS_PITCH = readFloatSerial();
    KI_GPS_PITCH = readFloatSerial();
    KD_GPS_PITCH = readFloatSerial();
    GPS_MAX_ANGLE = readFloatSerial();
    GEOG_CORRECTION_FACTOR = readFloatSerial();
}

/****************************************************
  RECEIVE ALTITUDE HOLD MODE PID VALUES
****************************************************/
void Receive_Altitude_PID() {
    KP_ALTITUDE = readFloatSerial();
    KI_ALTITUDE = readFloatSerial();
    KD_ALTITUDE = readFloatSerial();
}

/****************************************************
  RECEIVE DRIFT CORRECTION PID VALUES
****************************************************/
void Receive_Drift_PID() {
    Kp_ROLLPITCH = readFloatSerial();
    Ki_ROLLPITCH = readFloatSerial();
    Kp_YAW = readFloatSerial();
    Ki_YAW = readFloatSerial();
}

/****************************************************
  RECEIVE SENSOR OFFSET VALUES
****************************************************/
void Receive_Sensor_Offsets() {
    gyro_offset_roll = readFloatSerial();
    gyro_offset_pitch = readFloatSerial();
    gyro_offset_yaw = readFloatSerial();
    acc_offset_x = readFloatSerial();
    acc_offset_y = readFloatSerial();
    acc_offset_z = readFloatSerial();
}

/****************************************************
  RECEIVE CAMERA MODE
****************************************************/
void Receive_Camera_Mode() {
        SerFlu();
        delay(53);
        float tempVal;
	SerPrln("Enter camera mode [1-4]: ");
	while( SerAva()==0 );  // wait until user presses a key
        tempVal=0;
	tempVal = readFloatSerial();
	if (tempVal != 0) {
		cam_mode = tempVal;
		SerPri("Camera mode is now set to: ");
		SerPriln(cam_mode,DEC);
	}
	SerPri("Saving camera mode setting to EEPROM...");   
        delay(100);
        writeEEPROM(cam_mode, cam_mode_ADR);
        delay(1000);
        SerPrln("done.");
    cam_mode = readFloatSerial();
    //BATTLOW = readFloatSerial();
}

/****************************************************
  RECEIVE MOTOR DEBUG COMMANDS
****************************************************/
void Receive_Motor_Debug_Commands() {
    frontMotor = readFloatSerial();
    backMotor = readFloatSerial();
    rightMotor = readFloatSerial();
    leftMotor = readFloatSerial();
    motorArmed = readFloatSerial();
}

/****************************************************
  RECEIVE RATE CONTROL PID VALUES
****************************************************/
void Receive_Rate_Control_PID() {
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
}

/****************************************************
  RECEIVE TRANSMITTER CALIBRATION VALUES
****************************************************/
void Receive_Transmitter_Calibration() {
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
}

/****************************************************
  CALIBRATE ACCELEROMETER OFFSET
****************************************************/
void Calibrate_Accel_Offset() {

  int loopy;
  long xx = 0, xy = 0, xz = 0; 

  SerPrln("Initializing Accelerometer..");
  adc.Init();            // APM ADC library initialization
  //  delay(250);                // Giving small moment before starting

  calibrateSensors();         // Calibrate neutral values of gyros  (in Sensors.pde)

  SerPrln();
  SerPrln("Sampling 50 samples from Accelerometers, don't move your ArduCopter!");
  SerPrln("Sample:\tAcc-X\tAxx-Y\tAcc-Z");

  for(loopy = 1; loopy <= 50; loopy++) {
    SerPri("  ");
    SerPri(loopy);
    SerPri(":");
    tab();
    SerPri(xx += adc.Ch(4));
    tab();
    SerPri(xy += adc.Ch(5));
    tab();
    SerPrln(xz += adc.Ch(6));
    delay(20);
  }

  xx = xx / (loopy - 1);
  xy = xy / (loopy - 1);
  xz = xz / (loopy - 1);
  xz = xz - 407;               // Z-Axis correction
  //  xx += 42;

  acc_offset_y = xy;
  acc_offset_x = xx;
  acc_offset_z = xz;

  AN_OFFSET[3] = acc_offset_x;
  AN_OFFSET[4] = acc_offset_y;
  AN_OFFSET[5] = acc_offset_z;

  SerPrln();
  SerPrln("Offsets as follows: ");  
  SerPri("  ");
  tab();
  SerPri(acc_offset_x);
  tab();
  SerPri(acc_offset_y);
  tab();
  SerPrln(acc_offset_z);

  SerPrln("Final results as follows: ");  
  SerPri("  ");
  tab();
  SerPri(adc.Ch(4) - acc_offset_x);
  tab();
  SerPri(adc.Ch(5) - acc_offset_y);
  tab();
  SerPrln(adc.Ch(6) - acc_offset_z);
  SerPrln();
  SerPrln("Final results should be close to 0, 0, 408 if they are far (-+10) from ");
  SerPrln("those values, redo initialization or use Configurator for finetuning");
  SerPrln();
  SerPrln("Saving values to EEPROM");
  writeEEPROM(acc_offset_x, acc_offset_x_ADR);
  writeEEPROM(acc_offset_y, acc_offset_y_ADR);
  writeEEPROM(acc_offset_z, acc_offset_z_ADR);
  delay(200);
  SerPrln("Saved..");
  SerPrln();
}

/****************************************************
  TOGGLE MAGNETOMETER ON/OFF
****************************************************/
void Toggle_Magnetometer() {
	SerPrln("Enable/disable Magnetometer!");
	SerPri("Magnetometer is now: ");
	delay(500);
	if (MAGNETOMETER == 0) {
		MAGNETOMETER = 1;
		writeEEPROM(MAGNETOMETER, MAGNETOMETER_ADR);
		SerPrln("Enabled");
	} else { 
		MAGNETOMETER = 0;
		writeEEPROM(MAGNETOMETER, MAGNETOMETER_ADR);
		SerPrln("Disabled");
	}
	delay(1000);
	SerPrln("Setting saved to EEPROM.");
 }

/****************************************************
  CALIBRATE MAGNETOMETER (COMPASS) OFFSET
****************************************************/
void Calibrate_Compass_Offset() {

	#ifdef IsMAG  
	SerPrln("Rotate/Pitch/Roll your ArduCopter untill offset variables are not");
	SerPrln("anymore changing, write down your offset values and update them ");
	SerPrln("to their correct location.  Starting in..");
	SerPrln("2 secs.");
	delay(1000);
	SerPrln("1 secs.");
	delay(1000);
	SerPrln("starting now....");

	AP_Compass.init();   // Initialization
	AP_Compass.set_orientation(MAGORIENTATION);         // set compass's orientation on aircraft
	AP_Compass.set_offsets(0,0,0);                      // set offsets to account for surrounding interference
	AP_Compass.set_declination(ToRad(MAGCALIBRATION));  // set local difference between magnetic north and true north
	int counter = 0; 

	SerFlu();
        delay(50);
	while(1) {
		static float min[3], max[3], offset[3];
		if((millis()- timer) > 100)  {
			timer = millis();
			AP_Compass.read();
			AP_Compass.calculate(0,0);  // roll = 0, pitch = 0 for this example

			// capture min
			if( AP_Compass.mag_x < min[0] ) min[0] = AP_Compass.mag_x;
			if( AP_Compass.mag_y < min[1] ) min[1] = AP_Compass.mag_y;
			if( AP_Compass.mag_z < min[2] ) min[2] = AP_Compass.mag_z;

			// capture max
			if( AP_Compass.mag_x > max[0] ) max[0] = AP_Compass.mag_x;
			if( AP_Compass.mag_y > max[1] ) max[1] = AP_Compass.mag_y;
			if( AP_Compass.mag_z > max[2] ) max[2] = AP_Compass.mag_z;

			// calculate offsets
			offset[0] = -(max[0]+min[0])/2;
			offset[1] = -(max[1]+min[1])/2;
			offset[2] = -(max[2]+min[2])/2;

			// display all to user
			SerPri("Heading:");
			SerPri(ToDeg(AP_Compass.heading));
			SerPri("  \t(");
			SerPri(AP_Compass.mag_x);
			SerPri(",");
			SerPri(AP_Compass.mag_y);
			SerPri(",");    
			SerPri(AP_Compass.mag_z);
			SerPri(")");

			// display offsets
			SerPri("\t offsets(");
			SerPri(offset[0]);
			SerPri(",");
			SerPri(offset[1]);
			SerPri(",");
			SerPri(offset[2]);
			SerPri(")");
			SerPrln();

			if(counter == 200) {
				counter = 0;
				SerPrln();
				SerPrln("Roll and Rotate your quad untill offsets are not changing!");
				//        SerPrln("to exit from this loop, reboot your APM");        
				SerPrln();        
				delay(500);
			}
			counter++;
		}
		if(SerAva() > 0){
			mag_offset_x = offset[0];
			mag_offset_y = offset[1];
			mag_offset_z = offset[2];

			SerPrln("Saving Offsets to EEPROM");
			writeEEPROM(mag_offset_x, mag_offset_x_ADR);
			writeEEPROM(mag_offset_y, mag_offset_y_ADR);
			writeEEPROM(mag_offset_z, mag_offset_z_ADR);
			delay(500);
			SerPrln("Saved...");
			SerPrln();
			break;
		}
	}
	#else
	SerPrln("Magneto module is not activated in Config.h");
	SerPrln("Check your program #definitions, upload firmware and try again!!");
	//  SerPrln("Now reboot your APM");
	//  for(;;)
	//    delay(10);
	#endif      
}

/****************************************************
  SET CAMERA SMOOTHING VALUES
****************************************************/
void Set_Camera_Smooth() {
	float tempVal;
	int saveToEeprom = 0;

	SerPrln("Camera Smooth values:");
	Show_Camera_Smooth();
	SerPrln();
	// Tilt Smooth
        SerPriln("Enter Camera Tilt Smooth Value or 0 to Skip: ");
        //SerFlu();
	while( SerAva()==0 );  // wait until user presses a key
	tempVal = readFloatSerial();
	if (tempVal != 0) {
		CAM_SMOOTHING = tempVal;
		SerPri("Camera Tilt Smooth: ");
		SerPriln(CAM_SMOOTHING);
		saveToEeprom = 1;   
	}
	SerPrln();      

	// Roll Smooth
        SerPriln("Enter Camera Roll Smooth Value or 0 to Skip: ");
        //SerFlu();
        while( SerAva()==0 );  // wait until user presses a key
	tempVal = readFloatSerial();
	if (tempVal != 0) {
		CAM_SMOOTHING_ROLL = tempVal;
		SerPri("Camera Roll Smooth: ");
		SerPriln(CAM_SMOOTHING_ROLL);
		saveToEeprom = 1;   
	}
	SerPrln();      

    // Roll Smooth
        SerPriln("Enter Camera Center Value or 0 to Skip: ");
        //SerFlu();
        while( SerAva()==0 );  // wait until user presses a key
	tempVal = readFloatSerial();
	if (tempVal != 0) {
		CAM_CENT = tempVal;
		SerPri("Camera Roll Smooth: ");
		SerPriln(CAM_CENT);
		saveToEeprom = 1;   
	}
	SerPrln();     
  
  
	// save to eeprom
	if( saveToEeprom == 1 ) {
		Show_Camera_Smooth();
		SerPrln();
		Save_Camera_Smooth_toEEPROM();
		SerPrln("Saved to EEPROM");
		SerPrln();
	}else{
		SerPrln("No changes. Nothing saved to EEPROM");
		SerPrln();
	}
}

//******************************************************************************************************************
// SUPPORTING FUNCTIONS BELOW 
//******************************************************************************************************************

/****************************************************
  PRINT COMMA OVER SERIAL
****************************************************/
void comma() {
	SerPri(',');
}

/****************************************************
  PRINT TAB OVER SERIAL
****************************************************/
void tab() {
	SerPri("\t");
}

/****************************************************
  PRINT COMMA, SPACE OVER SERIAL
****************************************************/
void cspc() {
	SerPri(", ");
}

/****************************************************
  WAIT FOR SERIAL ENTER
****************************************************/
void WaitSerialEnter() {
	// Flush serials
	SerFlu();
	delay(50);
	while(1) {
		if(SerAva() > 0){
			break; 
		}
		delay(20);
	}
	delay(250);
	SerFlu();  
}

/****************************************************
  PRINT COMMAND TO CLEAR REMOTE SERIAL CONSOLE
****************************************************/
void SerPriClScr() {  // Clears the remote serial console
	SerPrln(". . . .");
}

/****************************************************
  READ FLOATING POINT VALUE OVER SERIAL
****************************************************/
float readFloatSerial() {
	byte index = 0;
	byte timeout = 0;
	char data[128] = "";

	do {
		if (SerAva() == 0) {
			delay(10);
			timeout++;
		} else {
			data[index] = SerRea();
			timeout = 0;
			index++;
		}
	}  
	while ((data[constrain(index-1, 0, 128)] != ';') && (timeout < 5) && (index < 128));
	return atof(data);
}


//******************************************************************************************************************
// MENU FUNCTIONS BELOW 
//******************************************************************************************************************

/*
***********************************************************
SHOW MENU AND SUBMENUS
***********************************************************
                                                         */
void Show_Menu_Prompt() {
	delay(1000);
	SerPrln("Type 0 or ? to show the CLI menu when you're done.");
}
	
void Show_Menu() {
	// SubMenu; if 0 the main menu, otherwise a submenu
	// MenuOption; if 0, go back to previous menu otherwise make a choice.
	SerPriClScr(); // ArduPirates Management Console; clears the console screen
	SerPri("ArduPirate v.");
	SerPri(VER);
	SerPri(" CLI Menu - ");
	if (SubMenu==0) {
		SerPrln("Main");
		SerPrln("----------------------------------------------");
		SerPrln(" 1 - Initial Setup");
		SerPrln(" 2 - Camera settings");  
		SerPrln(" 3 - Show settings");
		SerPrln(" 4 - PID Tuning");
		SerPrln(" 5 - Reset...");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" 9 - Save settings to EEPROM");
		SerPrln(" 0 - Exit CLI menu");
	} else if (SubMenu==1) {
		SerPrln("Initial Setup");
		SerPrln("----------------------------------------------");
		SerPrln(" 1 - Calibrate accelerometer");
		SerPrln(" 2 - Calibrate magnetometer");
		SerPrln(" 3 - Toggle magnetometer");  
		SerPrln(" 4 - Set throttle minimum");
		SerPrln(" 5 - Calibrate ESCs");
		SerPrln(" 6 - Run motors (test)");
		SerPrln(" 7 - View motor debug");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" 9 - Back to main menu");
	}  else if (SubMenu==2) {
		SerPrln("Camera settings");
		SerPrln("----------------------------------------------");
		SerPrln(" 1 - Set camera mode");
		SerPrln(" 2 - View camera mode");
		SerPrln(" 3 - Set camera smoothing values");  
		SerPrln(" 4 - Set camera trigger values");
		SerPrln(" 5 - ");
		SerPrln(" 6 - ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" 9 - Back to main menu");
	}else if (SubMenu==3) {
		SerPrln("Show settings");
		SerPrln("----------------------------------------------");
		SerPrln(" 1 - Platform information");
		SerPrln(" 2 - Other settings");
		SerPrln(" 3 - View sensor offsets");
		SerPrln(" 4 - View all variables");  
		SerPrln(" 5 - View GPS data");
		SerPrln(" 6 - Dump logs to serial");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" 9 - Back to main menu");
	}else if (SubMenu==4) {
		SerPrln("PID Tuning");
		SerPrln("----------------------------------------------");
		SerPrln(" 1 - ");
		SerPrln(" 2 - ");
		SerPrln(" 3 - ");  
		SerPrln(" 4 - ");
		SerPrln(" 5 - ");
		SerPrln(" 6 - ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" 9 - Back to main menu");
	}else if (SubMenu==5) {
		SerPrln("Reset...");
		SerPrln("----------------------------------------------");
		SerPrln(" 1 - Reset settings and set default values");
		SerPrln(" 2 - Erase logs");
		SerPrln(" ");  
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" ");
		SerPrln(" 9 - Back to main menu");
	}else {
		SerPrln("Invalid choice. Try again.");
	}
		
	//SerFlu();
	SerPrln(" ");
	SerPrln("Type your choice, followed by Enter;");
	delay(50);
}
