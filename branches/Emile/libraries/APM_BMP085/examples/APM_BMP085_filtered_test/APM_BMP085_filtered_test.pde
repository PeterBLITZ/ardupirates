/*
  Example of APM_BMP085 (absolute pressure sensor) library.
  Code by Jordi Muñoz and Jose Julio. DIYDrones.com
*/

#include <Wire.h>
#include <APM_BMP085.h> // ArduPilot Mega BMP085 Library

APM_BMP085_Class APM_BMP085;

unsigned long timer;
  long old_value;
  int max_diff;
  long new_value;
  long Altitude_filtered = 0;
    long Altitude_filtered_cm;
  
void setup()
{  
  APM_BMP085.Init();   // APM ADC initialization
  Serial.begin(57600);
  Serial.println("ArduPilot Mega BMP085 library test");
  delay(1000);
  timer = millis();
  old_value=0;
  max_diff = 10;
}

void loop()
{
  int ch;
  float tmp_float;
  float Altitude;
  long diff_values;
  long result;


  
  if((millis()- timer) > 50)
    {
    timer=millis();
    APM_BMP085.Read();
    Serial.print("Pressure:");
    Serial.print(APM_BMP085.Press);
    Serial.print(" Temperature:");
    Serial.print(APM_BMP085.Temp/10.0);
    Serial.print(" Altitude:");
    tmp_float = (APM_BMP085.Press/101325.0);
    tmp_float = pow(tmp_float,0.190295);
    if(Altitude == 0) {
    	Altitude = (1.0 - tmp_float) * 4433000;
	}
	else
	{
    Altitude = Altitude * 0.75 + ((1.0 - tmp_float) * 4433000)*0.25 ;
    }
    Serial.print(Altitude);

	Altitude_filtered = BMP_Sensor_Filter(APM_BMP085.Press, Altitude_filtered, 15);

	Serial.print(" Altitude Filtered:");
    Serial.print(Altitude_filtered);
	
	tmp_float = (Altitude_filtered/101325.0);
    tmp_float = pow(tmp_float,0.190295);
	Altitude_filtered_cm = (1.0 - tmp_float) * 4433000;
	
	Serial.print(" Altitude Filtered cm:");
    Serial.print(Altitude_filtered_cm);
	Serial.println();
	}
}
// This filter limits the max difference between readings and also aply an average filter
long BMP_Sensor_Filter(long new_value, long old_value, int max_diff)
{
  long diff_values;
  long result;
  
  if (old_value==0)     // Filter is not initialized (no old value)
    return(new_value);
  diff_values = new_value - old_value;      // Difference with old reading
  if (diff_values>max_diff)   
    result = old_value+max_diff;    // We limit the max difference between readings
  else
    {
    if (diff_values<-max_diff)
      result = old_value-max_diff;        // We limit the max difference between readings
    else
      result = (new_value+old_value)>>1;  // Small filtering (average filter)
    }
  return(result); 
}