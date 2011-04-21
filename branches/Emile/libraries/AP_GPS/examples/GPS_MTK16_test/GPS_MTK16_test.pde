/*
	Example of GPS MTK library.
	Code by Jordi Munoz and Jose Julio. DIYDrones.com

	Works with Ardupilot Mega Hardware (GPS on Serial Port1)
*/

#include <FastSerial.h>
#include <AP_GPS_MTK16.h>
#include <stdio.h>
#include <math.h>
#include <stdint.h>
#include "c++.h" // c++ additions

struct Location {
	uint8_t		id;					///< command id
	uint8_t		options;			///< options bitmask (1<<0 = relative altitude)
	uint8_t		p1;					///< param 1
	int32_t		alt;				///< param 2 - Altitude in centimeters (meters * 100)
	int32_t		lat;				///< param 3 - Lattitude * 10**7
	int32_t		lng;				///< param 4 - Longitude * 10**7
};


FastSerialPort0(Serial);
FastSerialPort1(Serial1);

AP_GPS_MTK16 gps(&Serial1);
#define T6 1000000
#define T7 10000000.0

long bearing_1 = 0;
float bearing_2 = 0.0;
long bearing_3 = 0;

struct Location home_loc;

long lat = 454472160;
long lng = 91233730;

void setup()
{
	Serial.begin(38400);
	Serial1.begin(38400);
	stderr = stdout;
	home_loc.lat = 454472160;
    home_loc.lng = 91233730;
	
	gps.print_errors = true;

	Serial.println("GPS MTK16 library test");
	gps.init();	 // GPS Initialization
	delay(1000);
	
	
}
void loop()
{
	delay(500);
	gps.update();
	if (gps.new_data){
	
	bearing_1 = get_bearing(lat, lng, gps.latitude, gps.longitude);
	bearing_2 = get_bearing2(lat, lng, gps.latitude, gps.longitude);
	bearing_3 = get_bearing3(&home_loc, gps.latitude, gps.longitude);
	
		Serial.print("gps:");
		Serial.print(" Lat:");
		Serial.print((float)gps.latitude / T7, DEC);
		Serial.print(" Lon:");
		Serial.print((float)gps.longitude / T7, DEC);
		Serial.print(" Alt:");
		Serial.print((float)gps.altitude / 100.0, DEC);
		Serial.print(" GSP:");
		Serial.print(gps.ground_speed / 100.0);
		Serial.print(" COG:");
		Serial.print(gps.ground_course / 100.0, DEC);
		Serial.print(" SAT:");
		Serial.print(gps.num_sats, DEC);
		Serial.print(" FIX:");
		Serial.print(gps.fix, DEC);
		Serial.print(" TIM:");
		Serial.print(gps.time, DEC);
		Serial.print(" HDOP:");
		Serial.print((float)gps.hdop / 100.0, 2);
		Serial.println();
		Serial.print("Bearing:");
		Serial.print(bearing_1);
		Serial.println();
				Serial.print("Bearing2:");
		Serial.print(bearing_2);
		Serial.println();
				Serial.print("Bearing3:");
		Serial.print(bearing_3);
		Serial.println();

		gps.new_data = 0; // We have readed the data
		}
}

long get_bearing(long lat2, long lng2, long lat1, long lng1)
{
	// this is used to offset the shrinking longitude as we go towards the poles
	float rads 			= (abs(lat2)/T7) * 0.0174532925;
	float scaleLongDown 		= cos(rads);
	float scaleLongUp 		= 1.0f/cos(rads);
	
	long off_x = lng2 - lng1;
	long off_y = (lat2 - lat1) * scaleLongUp;
	long bearing =	9000 + atan2(-off_y, off_x) * 5729.57795;
	if (bearing < 0) bearing += 36000;
	return bearing;

}

float get_bearing2 (long lat2, long lng2, long lat1, long lng1)
{
  float lat_f = radians((float)(lat1/T7));
  float lat_to = radians((float)(lat2/T7));
  float dLon = radians((float)(lng2/T7) - (float)(lng1/T7));

  float y = (sin(dLon) * cos(lat_to));
  float x = ((cos(lat_f)* sin(lat_to)) - (sin(lat_f)* cos(lat_to) * cos(dLon)));
  float brng = (atan2(y, x));
  brng = degrees(brng);
  return (brng + 360);
  }
long get_bearing3(struct Location *loc2, long lat1, long lng1)
{
	// this is used to offset the shrinking longitude as we go towards the poles
	float rads 			= (abs(loc2->lat)/T7) * 0.0174532925;
	float scaleLongDown 		= cos(rads);
	float scaleLongUp 		= 1.0f/cos(rads);
	
	long off_x = loc2->lng - lng1;
	long off_y = (loc2->lat - lat1) * scaleLongUp;
	long bearing =	9000 + atan2(-off_y, off_x) * 5729.57795;
	if (bearing < 0) bearing += 36000;
	return bearing;

}
