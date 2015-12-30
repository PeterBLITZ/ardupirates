## GPS ##

([ArduCopter's source](http://code.google.com/p/arducopter/wiki/Quad_GPS))

ArduCopter / ArduPiratesNG fully supports GPS modules for position hold (Note: you will also need a magnetometer.) These two modules are recommended:

### MediaTek GPS module ###

![http://store.diydrones.com/v/vspfiles/photos/MT3329-02-2T.jpg](http://store.diydrones.com/v/vspfiles/photos/MT3329-02-2T.jpg)

**The state-of-the-art 66 channels MediaTek MT3329 GPS Engine**

**Features:**

  * Based on MediaTek Single Chip Architecture.
  * Dimension：16mm x 16mm x 6mm
  * L1 Frequency, C/A code, 66 channels
  * High Sensitivity：Up to -165dBm tracking, superior urban performances
  * Position Accuracy：< 3m CEP (50%) without SA (horizontal)
  * Cold Start is under 35 seconds (Typical)
  * Warm Start is under 34 seconds (Typical)
  * Hot Start is under 1 second (Typical)
  * Low Power Consumption：48mA @ acquisition, 37mA @ tracking
  * Low shut-down current consumption：15uA, typical
  * DGPS(WAAS, EGNOS, MSAS) support (optional by firmware)
  * USB/UART Interface
  * Support AGPS function ( Offline mode : EPO valid up to 14 days )


---


### UBlox GPS module ###

![http://store.diydrones.com/v/vspfiles/photos/SPK-GPS-GS407-2T.jpg](http://store.diydrones.com/v/vspfiles/photos/SPK-GPS-GS407-2T.jpg)

The powerful GPS based on Ublox 5 chip-set and Saratel helix antenna.

It comes pre-programmed for ArduPilot and Paparazzi UAV.

**Features:**

  * u-Blox 5H chipset
  * Sarantel omni-directional Geo-helix S-type active antenna
  * Real 2Hz Refresh rate, can be used up to 4Hz
  * Fifty channels
  * Supports UBX, NMEA and USB&NMEA
  * High immunity to RF interference
  * Firmware upgradable


---


Attach your GPS module as shown below (MediaTek module shown). It goes in the connector on the APM board, **not** the similar one on the IMU shield (that one, which says "No GPS!", is an I2C connector for the optional magnetometer or other I2C sensor).

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4846.JPG