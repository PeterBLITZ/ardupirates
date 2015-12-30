## Configurator Tool ##

([ArduCopter's source](http://code.google.com/p/arducopter/wiki/Quad_ConfiguratorTool))

**What is it?**
The purpose of this tool is to allow the user to setup the ArduCopter before it's first flight and to quickly adjust settings for desired flight characteristics. The user will be able to graphically observe correct operation of the sensors, transmitter commands and motor control of the quadrocopter. Additionally there are user programmable values such as PID control loop values and the Transmitter Factor that can be adjusted and stored to the ArduCopter's EEPROM (static memory).

**Note:** The Configurator only runs in Windows. If you can't run Windows, you can do the same functions with the text-oriented [Command Line Interface](http://code.google.com/p/ardupirates/wiki/CLI).

It is highly recommended to first checkout the ArduCopter without motors connected (or powered on) with the Configurator. Another good option is to mount off the props making the quad safe yet show how it will respond in reality.

## Connect ##

Ensure the slider switch is towards the front of the board (in the direction of the GPS connector), like this:

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/sliderflight.jpg](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/sliderflight.jpg)

Set the **connection timeout to 30** (this is important; it won't connect if you don't) and pick the right COM port for your USB connection to ArduCopter (make sure you've closed the Arduino IDE, so it's not using the same COM port. Press "Connect" and it should start reading data in 20 seconds or so.

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/arducopter-configurator-600px.jpg](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/arducopter-configurator-600px.jpg)

## Configuration ##

Once you've connected, select Initial Setup.

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/ACsetup.png](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/ACsetup.png)

Then Initialize the EEPROM:

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/ACinitialize.PNG

Now configure your transmitter. Move both of your sticks to all four corners and move your toggle swithes until all channels show blue bars as shown to mark their extremes.

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/ACtransmitter.PNG

Now switch the Calibration window and check that the sensors are responding to movement of your board/quad.

Note: The accel values will not be correct until you save your configuration to EEPROM, which will normalize them.

With your multicopter on a flat surface adjust the X and Y accelerometer offsets circled below until the the matching data bars are reading close to zero ( Z will not be zero because it is measuring the vertical force of gravity, adjuts it to match 408. ). After every change, click on the Update button circled below.

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/calibrateaccels.png](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/calibrateaccels.png)

If you are using a magnetometer, you can enable it as shown below in the Stable Mode menu. Remember that you'll need to tell the code what orientation you magnetometer is in, as described earlier in the [code section](http://code.google.com/p/arducopter/wiki/Quad_LoadingPage).

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/quadmag.PNG

Finally, you can reconnect the motors and test them, ensuring that the props are turning in the correct directions. Use the Motor Commands screen to test each motor, ensuring that the prop is rotating in the right direction. (Correct rotation directions are shown [here](http://code.google.com/p/arducopter/wiki/Quad_ESC).) If a prop is turning the wrong way, just swap two of the three wires going to that motor.

**WARNING: be very careful to hold the quad down and keep the props away from anything, especially yourself, while doing this! Please use safety goggles.**

http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/ACmotors.PNG