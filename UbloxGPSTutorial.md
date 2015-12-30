This page will help you setup your Ublox to work with your ArduPirates MultiCopter.

# Please follow this page and the detailed information step by step #


This tutorial is to show fellow Pirates how to use the Ublox GPS with your Arducopter. If you don't have the Ublox unit yet, get it [here](https://www.aeroquadstore.com/ProductDetails.asp?ProductCode=GPS%2D09436) or [here](http://www.sparkfun.com/products/9436). You will most likely have to do this setup no matter where you purchase your Ublox. I will try to make this as user friendly so even the most non-tech savvy person can do this. Ready?

I have enclosed all of the files needed for this tutorial.

First get the u-center GPS evaluation software here [GPS u-center](http://code.google.com/p/ardupirates/downloads/list) . Unzip, Install.

Now you will have to connect your GPS to your computer directly. There are many ways out there but I have found my way the EASIEST by far!! NO SOLDERING! If you are reading this, most likely you have the GPS already so you will also need this [GPS adapter cable](http://store.diydrones.com/FTDI_GPS_Adapter_cable_15_cm_p/ca-0001-09.htm), and this [Sparkfun USB/FTDI breakout board](http://www.sparkfun.com/products/9716).

Connect: USB cable from computer (USB to Mini USB)=>USB/FTDI breakout board=>GPS adapter cable=>GPS

You will need to configure uBlox module using U-center, you only have to do it once, these settings will remain in the EEPROM forever unless you change them.

Open u-center (you might have to right click u-center icon=>properties=>compatability tab=>check the box for Run this program in compatability mode for:=>Windows Vista (Service Pack 2)(If this still does not work for your computer, change it to Windows Xp, service pack 3).

Select the Com port that your FTDI cable is attached to.
You will need to tell it what baud rate the GPS module is running at. If you got it from the DIY Drones store, it's 38,400. If you got it from Sparkfun, it's probably 9,600.

![http://i529.photobucket.com/albums/dd340/95ESINT/GPS/u-centerbaudrate.jpg](http://i529.photobucket.com/albums/dd340/95ESINT/GPS/u-centerbaudrate.jpg)

The icons circled below should be green or flashing green (it will not show a sat lock if your indoors)

![http://i529.photobucket.com/albums/dd340/95ESINT/GPS/u-centercomport.jpg](http://i529.photobucket.com/albums/dd340/95ESINT/GPS/u-centercomport.jpg)

If it's not working, check these things:

1) Did you wire it up right?
2) Did you select the right Com port?
3) Did you select the right baud speed?

To flash the new firmware go to Tools=>Firmware update. Make sure the firmware image points to the file you downloaded and unzipped (Located at end of this tut).

![http://i529.photobucket.com/albums/dd340/95ESINT/GPS/ucenter.jpg](http://i529.photobucket.com/albums/dd340/95ESINT/GPS/ucenter.jpg)


To configure GPS module to use with your ArduPirates Multicopter code, use my config file that is located in the link below. Point the "Flash Destination File" to the location you unzipped it to. Once done click Ok.

To configure a module to use with ArduPirates Multicopter, Go to Tools=>GPS configuration, and select my file for Ublox Config.

Check the "Store configuration" box and click on "File >>> GPS".

![http://i529.photobucket.com/albums/dd340/95ESINT/GPS/u-centerstoreconfigfile.jpg](http://i529.photobucket.com/albums/dd340/95ESINT/GPS/u-centerstoreconfigfile.jpg)

If you were communicating with the module at any speed other than 38,400, you'll find that the configuration process will fail midway through. That's because it's just gotten to the bit that changes the communications speed to 38,400. Set your u-center com speed to 38,400 and run it again. It should work this time.

Go to Message view (View->messages view):

![http://i529.photobucket.com/albums/dd340/95ESINT/GPS/u-centermessagesview.jpg](http://i529.photobucket.com/albums/dd340/95ESINT/GPS/u-centermessagesview.jpg)

**MAKE SURE TO CLICK SEND IN THE LOWER LEFT CORNER AFTER EACH CHANGE!!**

1. Right Click on the NMEA Text on top of the tree and choose disable child messages
**SEND**

2. Choose UBX=>CFG=>NAV - set the Dynamic Platform Model to use 3-Pedestrian.
**SEND**

3. Choose UBX=>CFG=>NAV2- set the Dynamic Platform Model to use 3-Pedestrian.
**SEND**

4.  Choose UBX=>CFG=>NAV5- set the Dynamic Model to use 3-Pedestrian.
**SEND**

5. UBX=>CFG=>PRT - set USART1 to 38400bps and make sure ALL the protocol settings are on UBX+NMEA!!
**SEND**

6. Change the baudrate of U-Center to 38400bps if the connection is lost at this point
**SEND**

7. UBX=>CFG=>RATE(Rates) - change the Measurement Period to 200ms This gives a 5 Hz position update since 5 x 200ms is one second.
**SEND**

8. UBX=>CFG=>SBAS : Disable (SBAS appears to cause occasional severe altitude calcuation errors)
**SEND**

9. UBX=>NAV (not UBX=>CFG=>NAV): double click on POSLLH, STATUS, VELNED. They should change from grey to black.
**SEND**

10. UBX=>CFG=>CFG : save current config, click "send" in the lower left corner to permanently save these settings to the receiver.

11. go to Receiver=> Action=> then click Save Config. Otherwise, every time you unplug your gps, it will revert back to the original configuration.



**You are done!! Now plug the GPS to your ArduPirates Multicopter with your SS code and fly like a PIRATE!**

You can download the ArduPirates Code including the Ublox GPS code from here:
[Main Download Page](http://code.google.com/p/ardupirates/downloads/list)