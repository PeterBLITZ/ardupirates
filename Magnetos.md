## Using a magnetometer ##
([ArduCopter's source](http://code.google.com/p/arducopter/wiki/Quad_Magnetos))

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/BR-HMC5843-01-2.jpg](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/BR-HMC5843-01-2.jpg)

If you're flying quadcopters or helis, which are often hovering in one position, the GPS (which can only calculate a directional vector when it's in forward motion) will not be able to correct the drift in the yaw gyro.  For these applications, you may want to add a magnetometer, which can correct the yaw even when the vehicle is not moving.

Also if you wish to use GPS hold a magnetometer is required in order for the quad or heli to know which way it should move in order to correct changes in it's longitude or lattitude.

Both DIYDrones store and Sparkfun sell the HMC5843 that has been tested to work with the ArduPirates software but the wiring and mounting method is different.  Instructions for both appear below.

## Connecting the DIYDrones magnetometer ##

### Method One: Using a cable ###

You can attach the [HMC5843 magnotometer](http://store.diydrones.com/product_p/br-hmc5843-01.htm) to the APM IMU shield's I2C sensor port, which looks like a GPS connector but says "No GPS". One reason to do this is you intend to daisy-chain other I2C sensors to your board.

One way to do this is to modify a [GPS cable](http://store.diydrones.com/product_p/ca-0001-04.htm), cutting off the connector on one side and soldering the wires to the right pads on the magnetometer board. You'll only be using four of the six wires; the other two can be cut off. Plug the connector into the shield and look at the bottom of the shield where you should see "SCL", "SDA", "+5V", "GND" written, corresponding to the four wires. These should be matched up to the same pins on the magnetometer, as shown below:

![http://arducopter.googlecode.com/svn/images/Magneto/MAGneto-01.png](http://arducopter.googlecode.com/svn/images/Magneto/MAGneto-01.png)

Here's a picture of how it should look (we recommend threading the wires through the hole before soldering for strain relief):

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4888.JPG

Plug it in here:

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4891.JPG

Ensure that the small solder blob jumper on the magnetometer is set to 5V before connecting it. If it isn't, you should switch it as shown below:

![http://arducopter.googlecode.com/svn/images/Magneto/Mag_HMC5843_jumper.jpg](http://arducopter.googlecode.com/svn/images/Magneto/Mag_HMC5843_jumper.jpg)

### Method Two: Mounted on the board ###

If you don't plan on using the 4-pin I2C connector on the shield for anything else, you can mount the magnetometer directly on top. Again, check that the voltage solder jumper is in the 5V position, as discussed above.

You can use a 4 pin breakaway header as a standoff.  Take care to align the magnetometer and shield edges.  Precision here will reduce the amount of trimming you have to do later.

First, snap off the mounting area of the magnetometer:

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/BR-HMC5843-01-3.jpg](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/BR-HMC5843-01-3.jpg)

Now, solder in the four pin header (example of the header shown on the table next to the board). It's best to solder the header with the long pins down, going through the board.

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4889.JPG

Finally, solder on the magnetometer **component side down**, as shown here:

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4890.JPG


---


## Connecting the Sparkfun magnetometer ##

![http://arducopter.googlecode.com/svn/images/Magneto/SparkFunHMC5843.jpg](http://arducopter.googlecode.com/svn/images/Magneto/SparkFunHMC5843.jpg)  ![http://arducopter.googlecode.com/svn/images/Magneto/SparkFunLogicLevelConverter.jpg](http://arducopter.googlecode.com/svn/images/Magneto/SparkFunLogicLevelConverter.jpg)

To use the [Sparkfun magnetometer](http://www.sparkfun.com/commerce/product_info.php?products_id=9371) you will need an i2c logic level converter from [diyDrones](http://store.diydrones.com/I2C_SMBus_Voltage_Traslator_I2C_Level_Shifter_p/br-0009-01.htm) or [Sparkfun](http://www.sparkfun.com/commerce/product_info.php?products_id=8745) because the Sparkfun magnetometer requires 3.3v but the i2c bus on the OilPan supplies 5v.  The wiring instructions below assume you have the Sparkfun converter.

![http://arducopter.googlecode.com/svn/images/Magneto/ConnectingSparkFunMag.jpg](http://arducopter.googlecode.com/svn/images/Magneto/ConnectingSparkFunMag.jpg)

Note on the Sparkfun converter there is an HV side and an LV side.  The HV side connects to the Oilpan, the LV Side connects to the Sparkfun Magnetometer.

**Step #1**: Hack a GPS cable as you would if you were using the diyDrones magnetometer and connect the Oilpan to the HV side of the converter like this:
  * OilPan GND -> HV GND
  * OilPan 5V  -> HV HV
  * OilPan SDA -> HV Tx0 (the top one)
  * OilPan SCL -> HV Tx0 (the bottom one)

**Step #2**: Connect the Sparkfun Magnetometer to the LV side of the converter:
  * SF Mag GND -> LV GND
  * SF Mag VCC -> LV LV
  * SF Mag SDA -> LV TxI (TOP)
  * SF Mag SCL -> LV TxI (BOTTOM)

**Step #3**: Connect the VREF on the Oilpan (which happens to output 3.3v) to the converter's LV LV pin or the Sparkfun Mag's VCC pin.

**Step #4**: Before you connect the Oilpan to your USB port or battery, double check that everything is wired up correctly!  Use a voltmeter to check that 3.3v (or close to it) will be supplied to the magnetometer.