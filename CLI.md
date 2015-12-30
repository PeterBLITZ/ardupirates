## Using the Command Line Interface (CLI) ##

([ArduCopter's source](http://code.google.com/p/arducopter/wiki/CLI))

The Command Line Interface (CLI) is an alternative to the Configurator. It does most of what the Configurator does, but in a text interface. One reason to use this is that you may not be able to run the Configurator (if you're running Linux, for example, or a Mac without a Windows emulator). You may also prefer it because it's fast and clean. (There are also a few commands that are only available in the CLI).

The way the CLI is used is by sliding the slider switch on the IMU board towards the servo connectors. **Don't forget to return it to the other position before you fly!**

![http://arducopter.googlecode.com/svn/images/CLI_switch.png](http://arducopter.googlecode.com/svn/images/CLI_switch.png)

Now open the Arduino serial monitor, setting baud rate to 115200 baud and ensuring that "Carriage Returns" are enabled. You should see a command line on the monitor:

https://ardupirates.googlecode.com/svn/Images/QuadCLI.PNG

### Available commands ###

Commands that are supported from current CLI are:
**a - Activate/Deactivate magnetometer**

> Every second 'a' activates / deactivates your magnetometer usage inside program. You also need to verify that you defined the mag options in the Online Configurator.

![https://lh6.googleusercontent.com/_SLFLtGv2hms/TUavEmz9pKI/AAAAAAAAA_I/MGz11iHYDo4/s800/mag%20setup.jpg](https://lh6.googleusercontent.com/_SLFLtGv2hms/TUavEmz9pKI/AAAAAAAAA_I/MGz11iHYDo4/s800/mag%20setup.jpg)


**c - Compass offset calibration**

> Roll/Pitch/Move/Rock/Twist your quad on every direction, even upside down until offset numbers are not changing. After values are as desired hit 'Enter' to save values and to exit back to main menu

**e - ESC Max Throttle calibration**

> _Official ArduCopter ESCs_ (and few others too, you can test) supports automatic max/min throttle calibrations.

  1. SAFETY! Disconnect your propellers first!
  1. Activate this calibration
  1. Disconnect your battery but keep USB cable connected
  1. Move your throttle stick to "full"
  1. Connect your battery and listen normal ESC beepboop.. reboot sounds
    1. After ESCs send Beep Beep sound, move your throttle to minimum
    1. Listen final beep sound from ESCs
  1. Give small throttle to your ArduCopter to see that all motors spins same speed
    1. If they do not spin, redo calibration

> Hit 'Enter' to disarm all motors and to exit from ESC Calibration

If you are not using the official ArduCopter ESCs, you can manually calibrate them one by one by connecting each ESC to your RC receiver's channel 3 output. Unplug your battery, move transmitter throttle stick to full, and reconnect the battery. Wait for the beeps to stop, then bring the throttle stick down to the minimum again. Repeat for all four ESCs.

**f - Calibrate Camera Smoothing**

> Here wou can configure the smoothing parameters to make your camera corrections follow your quad moves.
> You can setup corrections for **Tilt**, **Roll** and **Center** values.

> You also need to verify that you activated the Camera Stabilization in the Online Configurator.

https://ardupirates.googlecode.com/svn/Images/CamStab.PNG


**g - Calibrate Camera Trigger**

> Here you can calibrate the trigger or Shutter parameters of your camera, those are the values that will enable your camera to focus and shoot photos on demand.
> You can setup **Focus**, **Trigger** and **Release** values.

> You also need to verify that you activated the Camera Shutter in the Online Configurator.

https://ardupirates.googlecode.com/svn/Images/CamShut.PNG


**i - Initialize and calibrate Accel offsets**

> Place your multicopter on level surface and reset its 0-Level with this command. It takes some seconds until samples will be collected. During calibration do not touch/move or in any other ways make your multicopter to move.

**m - Motor tester with AIL/ELE stick**

> Users can easily test that their ESC/Motors are connected correctly. Move your AIL/ELE stick to up/down/left/right and corresponding motor should start pulsing slowly. Correct motor rotation directions are shown [here](http://code.google.com/p/arducopter/wiki/Quad_ESC).

> SAFETY! Disconnect propellers before doing your test.

> If your motors does not follow your stick input eg. stick to full right and left motors starts, change ESC cable on your PowerDistributor PCB or in APM connectors depending where you have connected your ESCs.


**r - Reset Factory settings**

> With every new revision you need to reset our EEPROM (just in case).


**t - Calibrate MIN Throttle value**

> Read minimum throttle value from your radio. Place throttle at minimum and activate this test, Min throttle value is needed for all altitude hold features and general motor operations.


**s - Show settings**

> Printout most important settings to your serial port.