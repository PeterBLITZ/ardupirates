# Xbee for wireless communication #
([ArduPilot Mega's source](http://code.google.com/p/ardupilot-mega/wiki/Wireless))

Adding wireless telemetry is not difficult and can extend the capabilities of your UAV immensely. We recommend using Xbee wireless modules, which have a range of more than a mile.

The first thing to keep in mind is that you should use Xbee modules in a different frequency range than your RC equipment.

If you have 72Mhz RC gear, you can use 2.4Ghz Xbee modules. In that configuration, we use these [Xbee Pro wireless modules](http://www.sparkfun.com/commerce/product_info.php?products_id=8742). But please note that the only reason to use 2.4Ghz Xbee equipment is if you also want to use 900 Mhz video transmission; otherwise, it's better to use 900Mhz Xbees (see next paragraph), which have longer range.

If you have 2.4Ghz RC gear, you should use 900Mhz Xbee modules. In that case, we use this [Xbee Pro with the wire antenna](https://www.sparkfun.com/commerce/product_info.php?products_id=9097) for the aircraft, and this [Xbee Pro with a SMA antenna connector](https://www.sparkfun.com/commerce/product_info.php?products_id=9099) (and a [good 900Mhz antenna](http://www.sparkfun.com/commerce/product_info.php?products_id=9143)) on the ground.

All Xbee modules need adapters to work with APM. You have two choices:

  * A DIY Drones [XtreamBee adapters](http://store.diydrones.com/product_p/br-0015-01.htmXtreemBee) on the aircraft side, and a [Sparkfun USB adapter board](http://www.sparkfun.com/commerce/product_info.php?products_id=8687) on the ground/laptop side with a USB cable.

  * If you already have an [FTDI cable](http://store.diydrones.com/FTDI_Cable_3_3V_p/ttl-232r-3v3.htm), get two [DIY Drones XtreamBee adapters](http://store.diydrones.com/product_p/br-0015-01.htmXtreemBee).

**WARNING:** Pro Serie 2 does not work properly on telemetry. It is not because of APM due APM does not know anything about radio modems. It's the design on Pro 2 that is against us. Their firmware does not work properly with constant data stream.




## Setting up the Xbee modules ##

The Xbee modules ship with a default of 9600bps, which you must change to match the APM's serial speed of 115200 bps; set your Xbee modules to match this speed. (If you want to use a different speed, you can choose it in the [Online Universal Ardu Configurator](http://ardupirates.net/config/APNG_Config.php), which is described in the [Softwares section](http://code.google.com/p/ardupirates/wiki/Online_Config).)

Connect each one of the them to the Sparkfun USB adapter board, plug the USB cable into your PC, and use [Digi's X-CTU utility](http://www.digi.com/support/productdetl.jsp?pid=3352&osvid=57&s=316&tp=5&tp2=0) to select the right serial port and communicate with them. Remember to initially set the utility to 9600bps to contact the new Xbee modules, and than after you've changed the speed, change the utility's serial speed accordingly. You should also give the modules unique Network IDs (VIDs) so they will be paired. Just use any 3-digit number, and just make sure you have set it the same on both modules. (Note: If you will be flying near other UAV planes make sure to verify the Network IDs are unique and not used by others in your vicinity.) You should finally set the Destination Adress High to 0 and the Destination Adress Low to FFFF.

Remember, on the Modem Configuration Tab, select your modem type, set the parameters and click "Write" before you click "Read", otherwise the "Read" button doesn't work. This might seem silly, but the first time you face it, it is not that obvious.

This is what the setting should look like when you click "Read" in Modem Configuration tab of X-CTU (we're using 999 as the VID here as an example):

![http://ardupilot.googlecode.com/svn/images/xbee.png](http://ardupilot.googlecode.com/svn/images/xbee.png)

## Connecting your airborne Xbee to APM ##

Solder four breakaway header pins in the IMU shield's "Telecom" port as shown below:

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4847.JPG

Now pull the black plastic strip off the pins (it's a little tight, but wiggling with a pair of pliers will do it), and bend them over 90 degrees as shown below:

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4848.JPG

## Wiring it up ##

Connect the XtremeBee adapter (with the Xbee plugged in) to the APM shield pins as shown below via four [individual connector wires](http://www.sparkfun.com/commerce/product_info.php?products_id=8430). Your adapter should be in "Master" mode. ("Master" and "Slave" just reverse the TX and RX pins).

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4841.JPG

You can also use a female-to-female servo connector cable for three of the four pins, if you'd like a slightly neater installation:

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4884.JPG

## On the ground side ##


If you're using an XtreamBee adapter on the ground side, connect it to a FTDI cable as shown below and plug that into your USB port. The adapter should also be in Master mode.

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/XtreemBee.png](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/XtreemBee.png)

If you're using the Sparkfun USB adapter, simply connect it via a USB cable as shown:

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4887.JPG

## Testing the connection ##


If you open up a terminal program on your laptop (you can use the Arduino IDE's serial monitor for this, too), select the correct serial port, and set the baud rate to whatever you set the Xbee modules to above (the default is 57600). Once you do this, you should see APM telemetry coming in. Anytime there is a "Serial3.println" in the code, that data will be sent through the Xbees to the ground. You can record any data you want, and even datalog from the ground! You can also open the Ground Station software, setting the right port and baud speed) and it should begin to show APM data.

Additionally, if you want to test the range of your Xbee link, connect the plane-side Xbee module's RX and TX pins together to create a loopback circuit and use the X-CTU utility's range test function. For the modules we are using you should get around a mile.

## Test code ##

ArduPilot Mega has four serial ports so all the usual Arduino serial commands now take a specifier to say which port you want to read from or write to. For example: Serial1.print(), Serial2.print(). The port connected to the USB/FDTI connector is Serial0. The port connected to the Telecom pins is Serial3.

[Here's](http://diydrones.com/forum/attachment/download?id=705844%3AUploadedFi58%3A181838) a quick demo that will print to all four ports so you can check to see that your Xbee connection is working. Here are the instructions on how to use it:

**1)** Plug your Xbee into one USB port and your APM into another. Use Arduino to load the demo code, and then in the Arduino IDE set the serial port to the one assigned to your APM board. Then open the serial monitor, setting the baud rate to 38400. You should see "Port 0" repeated as follows, showing the output from the APM's USB port:

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/xbeetest2.png](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/xbeetest2.png)

**2)** Now switch the serial port to the one your Xbee is assigned to and reopen the serial monitor. You should now see "Port 3" repeated, showing the output from APM's Xbee port:

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/xbeetest1.png](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/xbeetest1.png)

## Unbricking an Xbee ##

IMPORTANT NOTE: Sometimes Xbee modules get corrupted due to signal coming in before power on bootup. To avoid this, ALWAYS disconnect the signal wire (the blue one in the photos above) to the onboard Xbee adapter before powering up. Only reconnect them after the rest of the UAV has power.

If you're finding that yours stops working (green LED on Adafruit adapter doesn't come on), instructions to reload the firmware follow:
Using the Sparkfun USB explorer board:

  1. Take the module out of the interface board.

  1. Connect the interface board to the computer.

  1. Open X-CTU make sure Baud Rate is set to 9600

  1. Go to "Modem Configuration"

  1. Put a check in the "Always update firmware" box

  1. Select proper modem from drop down menu (for the 900Mhz ones recommended above select "XBP09-DP"; for 2.4GHZ Xeebee Pro 2 select "XBP24-B")

  1. Select proper function set and firmware version from the drop down menus ("ZNET 2.5 ROUTER/END DEVICE AT" and "1247" (currently))

  1. Click on the "Write" button. After a few seconds of trying to read the modem, you will get an Info box that says Action Needed. At this point, CAREFULLY insert the module into the interface board. After a few seconds, this should trigger a reloading of the firmware.

  1. You may get the info box again a short while after; if so just use the reset button on the interface board, or if you board doesn't have a reset button connect the reset pin to ground.

  1. Once you've confirmed that it's working again, make you sure you reset its baud rate and ID number to match your other module.

(_Thanks to Doug Barnett for these tips_)
