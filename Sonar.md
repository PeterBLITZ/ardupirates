## Sonar ##

([ArduCopter's source](http://code.google.com/p/arducopter/wiki/Quad_Sonar))

ArduPiratesNG supports the MaxSonar line of sonars for low level altitude hold and in the future collision avoidance.

Note: these feature is not included in [ArduCopterNG RC2](http://code.google.com/p/arducopter/downloads/detail?name=ArduCopter%20RC2.zip).

![http://arducopter.googlecode.com/svn/images/Sonar/XLMaxsonarEZ4.jpg](http://arducopter.googlecode.com/svn/images/Sonar/XLMaxsonarEZ4.jpg)

## Low Altitude Hold ##

Due to the narrow beam width, the recommended sonars for altitude hold are the [Maxbotix LV-EZ4](http://www.sparkfun.com/products/8504) and [XL-Maxsonar EZ4](http://www.sparkfun.com/products/9495)

**Connecting the Sonar:**
  * Attach the sonar to the body of the quad facing downwards, clear of the battery and anything else that might accidentally pass in front of the sensor
  * Wiring the sonar to the APM is nearly identical for the types supported:
    * The sensor's GND, V+ and "AN" or "3" pins should be connected to the OilPan's Pitot tube GND, +5V and IN pins as shown in the diagram below
![http://arducopter.googlecode.com/svn/images/Sonar/ConnectingXLorLVtoAPMwithPitot.jpg](http://arducopter.googlecode.com/svn/images/Sonar/ConnectingXLorLVtoAPMwithPitot.jpg)