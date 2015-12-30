## Reloading or updating your MediaTek GPS firmware ##

This is a pretty simple process, requiring only a [FTDI cable](http://store.diydrones.com/product_p/ttl-232r-3v3.htm), at least eight pins of [breakaway headers](http://store.diydrones.com/product_p/pr-pbc36saan.htm), and four [female-to-female jumper cables](http://www.adafruit.com/index.php?main_page=product_info&cPath=33&products_id=266) or a couple [female-to-female servo cables](http://store.diydrones.com/product_p/pr-0003-03-5cm.htm) (you've had to use all of these for basic ArduPilot, so you should have them on hand)

**Step 1**: Solder on a four-pin header on the pads on the bottom of the MediaTek module, as shown in the picture below.

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4843.JPG

**Step 2**: Press some spare headers into the FTDI cable connector. Now connect the GPS module to the FTDI cable with jumper cables, as shown below (GPS GND->FTDI black, GPS 5V->FTDI red, GPS Out->FTDI yellow, GPS in -> FTDI orange).

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4842.JPG

**Alternative to Steps 1 and 2**: Instead of soldering to the GPS board, prepare a cable that plugs to its connector on one side, and to the FTDI cable on the other side.

Connect as follows:

  * GPS connector pin 1 (red wire): not connected
  * GPS connector pin 2 (GND ) to FTDI cable pin 1 (GND)
  * GPS connector pin 3 (Tx-O) to FTDI cable pin 5 (GND)
  * GPS connector pin 4 (Rx-I) to FTDI cable pin 4 (GND)
  * GPS connector pin 5 (+5V ) to FTDI cable pin 3 (GND)
  * GPS connector pin 6 (GND ) to FTDI cable pin 1 (GND)
  * FTDI cable pin 2 (CTS) and pin 6 (RTS): not connected

https://lh6.googleusercontent.com/_PE66mXSGPRw/TW_7ExJ7YII/AAAAAAAA1iw/ADXB3lm-88A/s800/2010_0302%2004%20%20%20GPS%20Programming%20Cable.JPG

https://lh3.googleusercontent.com/_PE66mXSGPRw/TW_7E-4IO3I/AAAAAAAA1iw/XtvX7AFlX4k/s512/2010_0302%2001%20%20%20GPS%20Programming%20Cable.JPG

**Step 3**: Now download the firmware update utility and new firmware [here](http://code.google.com/p/ardupirates/downloads/detail?name=MTK_utility_update1.6.zip&can=2&q=). Unzip the file, and follow the instructions in the MediaTek\_Programming.pdf document.

Note: Be sure you program version 1.6 of the firmware. The original ArduPilot MTK GPS update page is
[here](http://code.google.com/p/ardupilot/wiki/MediaTek)