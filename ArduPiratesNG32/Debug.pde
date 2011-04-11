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

 File     : Debug.pde
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


#if DEBUG_SUBSYSTEM == 1
void debug_subsystem()
{
	Serial3.println("Begin Debug: Radio Subsystem ");
	while(1){
		delay(20);
		// Filters radio input - adjust filters in the radio.pde file
		// ----------------------------------------------------------
		read_radio();
		Serial3.print("Radio in ch1: ");
		Serial3.print(radio_in[CH_ROLL], DEC);
		Serial3.print("\tch2: ");
		Serial3.print(radio_in[CH_PITCH], DEC);
		Serial3.print("\tch3: ");
		Serial3.print(radio_in[CH_THROTTLE], DEC);
		Serial3.print("\tch4: ");
		Serial3.print(radio_in[CH_RUDDER], DEC);
		Serial3.print("\tch5: ");
		Serial3.print(radio_in[4], DEC);
		Serial3.print("\tch6: ");
		Serial3.print(radio_in[5], DEC);
		Serial3.print("\tch7: ");
		Serial3.print(radio_in[6], DEC);
		Serial3.print("\tch8: ");
		Serial3.println(radio_in[7], DEC);
	}
}
#endif

#if DEBUG_SUBSYSTEM == 2
void debug_subsystem()
{
	Serial3.println("Begin Debug: Servo Subsystem ");
	Serial3.print("Reverse ROLL - CH1: ");
	Serial3.println(reverse_roll, DEC);
	Serial3.print("Reverse PITCH - CH2: ");
	Serial3.println(reverse_pitch, DEC);
	Serial3.print("Reverse THROTTLE - CH3: ");
	Serial3.println(REVERSE_THROTTLE, DEC);
	Serial3.print("Reverse RUDDER - CH4: ");
	Serial3.println(reverse_rudder, DEC);

	// read the radio to set trims
	// ---------------------------
	trim_radio();

	radio_max[0]				 = 	CH1_MAX;
	radio_max[1]				 = 	CH2_MAX;
	radio_max[2]				 = 	CH3_MAX;
	radio_max[3]				 = 	CH4_MAX;
	radio_max[4]				 = 	CH5_MAX;
	radio_max[5]				 = 	CH6_MAX;
	radio_max[6]				 = 	CH7_MAX;
	radio_max[7]				 = 	CH8_MAX;
	radio_min[0]				 = 	CH1_MIN;
	radio_min[1]				 = 	CH2_MIN;
	radio_min[2]				 = 	CH3_MIN;
	radio_min[3]				 = 	CH4_MIN;
	radio_min[4]				 = 	CH5_MIN;
	radio_min[5]				 = 	CH6_MIN;
	radio_min[6]				 = 	CH7_MIN;
	radio_min[7]				 = 	CH8_MIN;

	while(1){
		delay(20);
		// Filters radio input - adjust filters in the radio.pde file
		// ----------------------------------------------------------
		read_radio();
		update_servo_switches();

		servo_out[CH_ROLL]	= ((radio_in[CH_ROLL]	- radio_trim[CH_ROLL])	* 45.0f	* reverse_roll) / 500;
		servo_out[CH_PITCH] = ((radio_in[CH_PITCH] - radio_trim[CH_PITCH]) * 45.0f	* reverse_roll) / 500;
		servo_out[CH_RUDDER] = ((radio_in[CH_RUDDER] - radio_trim[CH_RUDDER]) * 45.0f	* reverse_rudder) / 500;

		// write out the servo PWM values
		// ------------------------------
		set_servos_4();
		
		
		for(int y = 4; y < 8; y++) { 
			radio_out[y] = constrain(radio_in[y], 	radio_min[y], 	radio_max[y]);	
			APM_RC.OutputCh(y, radio_out[y]); // send to Servos
		}

		/*
		Serial3.print("Servo_out ch1: ");
		Serial3.print(servo_out[CH_ROLL], DEC);
		Serial3.print("\tch2: ");
		Serial3.print(servo_out[CH_PITCH], DEC);
		Serial3.print("\tch3: ");
		Serial3.print(servo_out[CH_THROTTLE], DEC);
		Serial3.print("\tch4: ");
		Serial3.print(servo_out[CH_RUDDER], DEC);
		*/
		///*
		Serial3.print("ch1: ");
		Serial3.print(radio_out[CH_ROLL], DEC);
		Serial3.print("\tch2: ");
		Serial3.print(radio_out[CH_PITCH], DEC);
		Serial3.print("\tch3: ");
		Serial3.print(radio_out[CH_THROTTLE], DEC);
		Serial3.print("\tch4: ");
		Serial3.print(radio_out[CH_RUDDER], DEC);
		Serial3.print("\tch5: ");
		Serial3.print(radio_out[4], DEC);
		Serial3.print("\tch6: ");
		Serial3.print(radio_out[5], DEC);
		Serial3.print("\tch7: ");
		Serial3.print(radio_out[6], DEC);
		Serial3.print("\tch8: ");
		Serial3.println(radio_out[7], DEC);
		
		//*/
	}
}
#endif


#if DEBUG_SUBSYSTEM == 3
void debug_subsystem()
{
	Serial3.println("Begin Debug: Analog Sensor Subsystem ");
	
	Serial3.print("AirSpeed sensor enabled: ");
	Serial3.println(AIRSPEED_SENSOR, DEC);
		
	Serial3.print("Enable Battery: ");
	Serial3.println(BATTERY_EVENT, DEC);
	zero_airspeed();
	
	Serial3.print("Air pressure offset:");
	Serial3.println(airpressure_offset, DEC);

	while(1){
		delay(20);
		read_z_sensor();
		read_XY_sensors();
		read_battery();
		
		Serial3.print("Analog IN:");
		Serial3.print("  0:");
		Serial3.print(analog0, DEC);
		Serial3.print(", 1: ");
		Serial3.print(analog1, DEC);
		Serial3.print(", 2: ");
		Serial3.print(analog2, DEC);
		Serial3.print(", 3: ");
		Serial3.print(airpressure_raw, DEC);
		
		Serial3.print("\t\tSensors:");
		Serial3.print("  airSpeed:");
		Serial3.print(airspeed, DEC);
		Serial3.print("m \tbattV:");
		Serial3.print(battery_voltage, DEC);
		Serial3.println("volts ");
	}
}
#endif

#if DEBUG_SUBSYSTEM == 4
void debug_subsystem()
{
	Serial3.println("Begin Debug: GPS Subsystem ");
	
	while(1){
		delay(333);

		// Blink GPS LED if we don't have a fix
		// ------------------------------------
		//update_GPS_light();
		
		gps.update();
		
		if (gps.new_data){
			Serial3.print("Lat:");
			Serial3.print(gps.latitude, DEC);
			Serial3.print(" Lon:");
			Serial3.print(gps.longitude, DEC);
			Serial3.print(" Alt:");
			Serial3.print(gps.altitude / 100, DEC);
			Serial3.print("m, gs: ");
			Serial3.print(gps.ground_speed / 100, DEC);
			Serial3.print(" COG:");
			Serial3.print(gps.ground_course / 1000l);
			Serial3.print(" SAT:");
			Serial3.print(gps.num_sats, DEC);
			Serial3.print(" FIX:");
			Serial3.print(gps.fix, DEC);
			Serial3.print(" TIM:");
			Serial3.print(gps.time);
			Serial3.print(" HDOP:");
			Serial3.print(gps.hdop / 100, DEC;
			Serial3.println();
		}
	}
}
#endif

#if DEBUG_SUBSYSTEM == 5
void debug_subsystem()
{
	Serial3.println("Begin Debug: GPS Subsystem, RAW OUTPUT");
	
	while(1){
		if(Serial1.available() > 0)	// Ok, let me see, the buffer is empty?
		{
			
			delay(60);	// wait for it all
			while(Serial1.available() > 0){
				byte incoming = Serial1.read();
				//Serial3.print(incoming,DEC);
				Serial3.print(incoming, HEX); // will output Hex values
				Serial3.print(",");
			}
			Serial3.println(" ");
		}

	}
}
#endif

#if DEBUG_SUBSYSTEM == 6
void debug_subsystem()
{
	Serial3.println("Begin Debug: IMU Subsystem ");
	startup_IMU_ground();
	
	while(1){
		delay(20);
		read_AHRS();
		
		// We are using the IMU
		// ---------------------
		Serial3.print("  roll:");
		Serial3.print(roll_sensor / 100, DEC);	
		Serial3.print("  pitch:");
		Serial3.print(pitch_sensor / 100, DEC);
		Serial3.print("  yaw:");
		Serial3.println(yaw_sensor / 100, DEC);
		
	}
}
#endif

#if DEBUG_SUBSYSTEM == 7
void debug_subsystem()
{
	Serial3.println("Begin Debug: Control Switch Test");
	
	while(1){
		delay(20);
		byte switchPosition = readSwitch();
		if (oldSwitchPosition != switchPosition){
			
			switch(switchPosition)
			{
				case 1: // First position
				Serial3.println("Position 1");

				break;
		
				case 2: // middle position
				Serial3.println("Position 2");
				break;
		
				case 3: // last position
				Serial3.println("Position 3");
				break;

				case 4: // last position
				Serial3.println("Position 4");
				break;

				case 5: // last position
				Serial3.println("Position 5 - Software Manual");
				break;

				case 6: // last position
				Serial3.println("Position 6 - Hardware MUX, Manual");
				break;

			}
	
			oldSwitchPosition = switchPosition;
		}
	}
}
#endif

#if DEBUG_SUBSYSTEM == 8
void debug_subsystem()
{
	Serial3.println("Begin Debug: Control Switch Test");
	
	while(1){
		delay(20);
		update_servo_switches();
		if (mix_mode == 0) {
			Serial3.print("Mix:standard ");
			Serial3.print("\t reverse_roll: ");
			Serial3.print(reverse_roll, DEC);
			Serial3.print("\t reverse_pitch: ");
			Serial3.print(reverse_pitch, DEC);
			Serial3.print("\t reverse_rudder: ");
			Serial3.println(reverse_rudder, DEC);
		} else {
			Serial3.print("Mix:elevons ");
			Serial3.print("\t reverse_elevons: ");
			Serial3.print(reverse_elevons, DEC);
			Serial3.print("\t reverse_ch1_elevon: ");
			Serial3.print(reverse_ch1_elevon, DEC);
			Serial3.print("\t reverse_ch2_elevon: ");
			Serial3.println(reverse_ch2_elevon, DEC);
		}
	}
}
#endif


#if DEBUG_SUBSYSTEM == 9
void debug_subsystem()
{
	Serial3.println("Begin Debug: Relay");
	pinMode(RELAY_PIN, OUTPUT);
	
	while(1){
		delay(3000);
	
		Serial3.println("Relay Position A");
		PORTL |= B00000100;
		delay(3000);
	
		Serial3.println("Relay Position B");
		PORTL ^= B00000100;		
	}
}
#endif

#if DEBUG_SUBSYSTEM == 10
void debug_subsystem()
{
	Serial3.println("Begin Debug: Magnatometer");
	
	while(1){
		delay(3000);
	}
}
#endif

#if DEBUG_SUBSYSTEM == 11
void debug_subsystem()
{
	ground_alt = 0;
	Serial3.println("Begin Debug: Absolute Airpressure");	
	while(1){
		delay(20);
		read_airpressure();
		Serial3.print("Air Pressure Altitude: ");
		Serial3.print(press_alt / 100, DEC);
		Serial3.println("meters");
	}
}
#endif

#if DEBUG_SUBSYSTEM == 12
void debug_subsystem()
{
	ground_alt = 0;
	Serial3.println("Begin Debug: Display Waypoints");	
	delay(500);

	eeprom_busy_wait();
	uint8_t options 	= eeprom_read_byte((uint8_t *) EE_CONFIG);

	eeprom_busy_wait();
	int32_t hold = eeprom_read_dword((uint32_t *) EE_ALT_HOLD_HOME);

	// save the alitude above home option
	if(options & HOLD_ALT_ABOVE_HOME){
		Serial3.print("Hold this altitude over home: ");
		Serial3.print(hold / 100, DEC);
		Serial3.println(" meters");
	}else{
		Serial3.println("Maintain current altitude ");
	}
	
	Serial3.print("Number of Waypoints: ");
	Serial3.println(wp_total, DEC);

	Serial3.print("Hit radius for Waypoints: ");
	Serial3.println(wp_radius, DEC);

	Serial3.print("Loiter radius around Waypoints: ");
	Serial3.println(loiter_radius, DEC);
	Serial3.println(" ");
	
	for(byte i = 0; i < wp_total; i++){
		struct Location temp = get_wp_with_index(i);
		print_waypoint(&temp, i);
	}
	
	while(1){
	}

}
#endif



#if DEBUG_SUBSYSTEM == 13
void debug_subsystem()
{
	Serial3.println("Begin Debug: Throttle Subsystem ");
	read_radio();
	
	uint16_t low_pwm = radio_in[CH_THROTTLE];
	uint16_t high_pwm = radio_in[CH_THROTTLE];
	
	while(1){
		delay(20);
		// Filters radio input - adjust filters in the radio.pde file
		// ----------------------------------------------------------
		read_radio();
		//update_throttle();
		set_servos_4();
		low_pwm = min(low_pwm, radio_in[CH_THROTTLE]);
		high_pwm = max(high_pwm, radio_in[CH_THROTTLE]);
		
		Serial3.print("Radio_in: ");
		Serial3.print(radio_in[CH_THROTTLE], DEC);
		Serial3.print("\tPWM output: ");
		Serial3.print(radio_out[CH_THROTTLE], DEC);
		Serial3.print("\tThrottle: ");
		Serial3.print(servo_out[CH_THROTTLE], DEC);
		Serial3.print("\tPWM Min: ");
		Serial3.print(low_pwm, DEC);
		Serial3.print("\tPWM Max: ");
		Serial3.println(high_pwm, DEC);
	}
}
#endif


#if DEBUG_SUBSYSTEM == 14
void debug_subsystem()
{
	Serial3.println("Begin Debug: Radio Min Max ");
	uint16_t low_pwm[8];
	uint16_t high_pwm[8];
	uint8_t i;
	
	for(i = 0; i < 100; i++){
		delay(20);
		read_radio();
	}
	
	for(i = 0; i < 8; i++){
		radio_min[i] = 0;
		radio_max[i] = 2400;
	 	low_pwm[i]	= radio_in[i];
 		high_pwm[i]	= radio_in[i];
	}
	
	while(1){
		delay(100);
		// Filters radio input - adjust filters in the radio.pde file
		// ----------------------------------------------------------
		read_radio();
		for(i = 0; i < 8; i++){
			low_pwm[i] = min(low_pwm[i], radio_in[i]);
			high_pwm[i] = max(high_pwm[i], radio_in[i]);
		}

		for(i = 0; i < 8; i++){
			Serial3.print("CH");
			Serial3.print(i + 1, DEC);
			Serial3.print(": ");
			low_pwm[i] = min(low_pwm[i], radio_in[i]);
			Serial3.print(low_pwm[i], DEC);
			Serial3.print("|");
			high_pwm[i] = max(high_pwm[i], radio_in[i]);
			Serial3.print(high_pwm[i], DEC);
			Serial3.print("   ");
		}
		Serial3.println(" ");
	}
}
#endif


#if DEBUG_SUBSYSTEM == 15
void debug_subsystem()
{
	Serial3.println("Begin Debug: EEPROM Dump ");
	uint16_t temp;
	for(int n = 0; n < 512; n++){
		for(int i = 0; i < 4; i++){
			temp = eeprom_read_word((uint16_t *) mem);
			mem += 2;
			Serial3.print(temp, HEX);
			Serial3.print("  ");
		}
		Serial3.print("  ");
		for(int i = 0; i < 4; i++){
			temp = eeprom_read_word((uint16_t *) mem);
			mem += 2;
			Serial3.print(temp, HEX);
			Serial3.print("  ");
		}
	}
}
#endif
