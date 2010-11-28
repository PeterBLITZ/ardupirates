/* *****************************************************************************
                  ArduPirate Configuration Setup
   ***************************************************************************** */

//GPS Config
#define IsGPS               // Do we have a GPS connected?

//#define MTK_GPS           // MediaTEK DIY Drones GPS. 
//#define IsNEWMTEK         // Do we have MTEK with new firmware?
//#include <GPS_MTK.h>      

//#define UBLOX_GPS         // uBlox GPS
//#include <GPS_UBLOX.h>   

#include "libraries/GPS_NMEA/GPS_NMEA.h"       // General NMEA GPS
#define NMEA_GPS            



#define IsMAG               // Do we have a Magnetometer connected? If have, remember to activate it from Configurator !
#define UseBMP              // Do we want to use the barometer sensor on the IMU?
#define CONFIGURATOR        // Do we use Configurator or normal text output over serial link?
//#define IsCAMERATRIGGER   // Do we want to use a servo to trigger a camera regularely
//#define IsXBEE            // Do we have a telemetry connected, eg. XBee connected on Telemetry port?
//#define IsAM              // Do we have motormount LED's? (AM = Atraction Mode)
//#define UseAirspeed       // Do we have an airspeed sensor?
//#define BATTERY_EVENT     // Do we have battery alarm wired up?
//#define MOTORMOUNT_LEDS   // Do we have motormount LEDs attched to AN4 and AN5 (NOT the same as IsAM)? See bottom of the file for details
//#define RELAY_LED_LIGHTS  // Do we have LED lights attached through the relay? Turned on and off with Rx Ch7 (FIXME: should be configurable)


/**********************************************/
// Frame build configuration
// THIS FLIGHT MODE X CODE - APM FRONT BETWEEN FRONT AND RIGHT MOTOR.
// NOT LIKE THE ALPHA RELEASE !!!.

//   F  CW  0....Front....0 CCW  R        // 0 = Motor
//          ......***......               // *** = APM 
//          ......***......               // ***
//          ......***......               // *** 
//   L CCW  0....Back.....0  CW  B          L = Left motor, 
//                                          R = Right motor, 
//                                          B = Back motor,
//                                          F = Front motor.  
#define FLIGHT_MODE_X
//#define FLIGHT_MODE_+

/**********************************************/
//  Magnetometer Setup

// To get Magneto offsets, switch to CLI mode and run offset calibration. During calibration
// you need to roll/bank/tilt/yaw/shake etc your ArduCoptet. Don't kick like Jani always does :)
#define MAGOFFSET -81.00,-35.00,30.50

// MAGCALIBRATION is the correction angle in degrees (can be + or -). You need to do this for making sure
// that your Magnetometer is truly showing 0 degress when your AeroQuad is looking to the North.
// Use a real compass (! not your iPhone) to point your AeroQuad to the magnetic north and then adjust this 
// value until you have a 0 dergrees reading in the configurator's atificial horizont. 
// Once you have achieved this fine tune in the configurator's serial monitor by pressing "T" (capital t).
#define MAGCALIBRATION -13.6

// orientations for DIYDrones magnetometer
#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_FORWARD
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_FORWARD_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_BACK_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_BACK
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_BACK_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_UP_PINS_FORWARD_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_FORWARD
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_FORWARD_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_BACK_RIGHT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_BACK
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_BACK_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_LEFT
//#define MAGORIENTATION APM_COMPASS_COMPONENTS_DOWN_PINS_FORWARD_LEFT

// orientations for Sparkfun magnetometer
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_FORWARD
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_FORWARD_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_BACK_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_BACK
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_BACK_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_UP_PINS_FORWARD_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK_RIGHT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_BACK_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_LEFT
//#define MAGORIENTATION APM_COMPASS_SPARKFUN_COMPONENTS_DOWN_PINS_FORWARD_LEFT

//Low Battery Alarm
#define LOW_VOLTAGE      12.5   // Pack voltage at which to trigger alarm (Set to about 1 volt above ESC low voltage cutoff)
#define VOLT_DIV_OHMS    3690   // Value of resistor (in ohms) used on voltage divider


/******************************************************** */
/* END CONFIGURATION                                      */
/******************************************************** */