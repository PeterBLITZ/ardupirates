How to set PID settings for your ArduPirate.

## _Informations above are based on ArduPirates SuperStable code ONLY!  For ArduPiratesNG code, please check [this page](http://code.google.com/p/ardupirates/wiki/PIDSettings) ##_


# Introduction #

**This Tutorial will show you how to set your Stable and Acro Roll/Pitch/Yaw settings to get you ready for your Maiden Flight.**

# Details #

When first setting up your ArduPirate for flight, it is critical to make sure your PID values are setup correctly for your ArduPirate.

To help describe what each PID value does, let's first discuss this in the context of the Acrobatic Mode of the ArduPirate. The Acro Mode only uses gyro sensors (which measure angular rate) for stabilization. The P value scales the gyro sensor data to motor commands. The I value is not used (I = 0) and the D value returns the ArduPirate from forward flight to a level hover quicker. The higher the P value, stronger motor commands are generated based on the gyro's angular rate measurements. If it is set too high, then the user will see oscillations as the ArduPirate tries to stabilize itself. If P is set too low, then your ArduPirate will not respond fast enough to keep itself stable (it looks like it flies "floppy").

Although the user can set the I value in the Configurator, it is generally not needed for stable flight in Acrobatic Mode (I = 0). A negative D value is used to help the ArduPirate
change faster to a level position after forward flight. It is possible to leave D = 0 and still see stable flight. Using a negative D value is only needed based on user preference.

What's the difference between **Acrobatic Mode** and **Stable Mode**?
In the Configurator there are PID values for Acrobatic Mode Roll and Pitch, Stable Mode Roll and Pitch, Yaw and Heading Hold. Before trying to fly in Stable Mode, the user must tune the Acrobatic Mode well first. Acrobatic Mode uses only the gyro data to stabilize the ArduPirate. Basically it detects changes in angular rates on a single axis and adjust the motors such that it resists the angular change from a level position. Stable Mode uses both gyro data and accelerometer data to maintain a level position and has the ability to "auto level" itself when the Pirate let's go of the transmitter sticks.

Since every frame has different characteristics (weight, size, stiffness) it is not possible to use a single set of PID values for all QuadCopters. The defaults (setup when the user hits the Initialize EEPROM button in the Configurator) are good for basic configurations.

How can I tune my Acrobatic Mode PID values for my ArduPirate?
First make sure that your ArduPirate is properly setup by following the Pre-Flight Checkout. If the QuadCopter is tilted along an axis and quickly flips on that axis, this is an indication that either the sensors are setup backwards (if you are buidling your own Shield) or the motors are setup backwards. If you move a transmitter stick along a particular axis and the ArduPirate immediately flips, most likely that transmitter channel is setup backwards (use the transmitter channel reversing function to fix). The following are simple steps to tuning the ArduPirate. Setting up a test stand is the safest way to tune your PID values, otherwise it is very easy to do it by touch. You will first need to tune your QuadCopter in Acrobatic Mode, set the DIP switch 1 in ON position (down).

  1. Connect the Configurator to the ArduPirate with a USB cable or wirelessly using XBee modems. Use the Configurator to verify you are configured for Acrobatic Mode.
  1. Set all Acrobatic Mode PID values to the default values by clicking the Initialize EEPROM button in the Configurator. Perform all other calibrations (transmitter/ESC's/sensors) before starting this tuning procedure. Insure the Pre-Flight Checkout has already been performed.
  1. Connect the Lipo battery to the ArduPirate to power up the motors.
  1. Arm the ArduPirate motors by moving the transmitter throttle stick to the lower right hand corner.
  1. Hold the ArduPirate securely in your hand, making sure to keep the props away from your eyes and arms.
  1. Move the throttle to about 1/3 power.
  1. Tilt the ArduPirate by hand along the roll axis and pitch axis. The ArduPirate should resist movement as you move it along each axis.
  1. Slowly increase the P value in 0.5 increments, until it feels very "stiff" or is difficult to tilt it on the roll axis and pitch axis.
  1. Shake the ArduPirate, be sure to maintain a good hold of the QuadCopter. Does it oscillate? If it does, reduce the P value until it stops oscillating. What you want is a stiff resistance to forced movement along the roll axis and pitch axis, but with no oscillations (no overcompensation from the ArduPirate).
  1. Do the same adjustment for the yaw axis. Shake the ArduPirate again to make sure there are no oscillations. Reduce the P value if there are.
  1. You may observe that the default values are good enough to fly with.
  1. When satisfied with hand tuning, disconnect the USB cable (If a cable is used. If using XBee, leave it connected, it's easiest to tune the ArduPirate using a wireless connection.) and place it on the ground for a first flight. Start with the rear of the ArduPirate facing towards you and the front of the ArduPirate away from you.
  1. Arm the motors, and slowly get it to about knee height. If you fly too low, you will experience ground effects where the air swash from the propellers will interfere with stable flight of the ArduPirate.
  1. If the ArduPirate starts to tilt away from a level hover, use the transmitter trims to adjust it until it hovers in place with minimal transmitter adjustments from the pilot.
  1. Do you still observe oscillations? Land immediately, lower the P values and test fly again. Does the ArduPirate seem "floppy" or does not fly very "stiff" in the air? Try raising the P values to make it more stable.
  1. Once you are happy with hovering performance, try moving the ArduPirate into forward flight, then stop to a hover. Does the ArduPirate dip too much when transitioning from forward flight to a hover? If it does, the P gain may be overcompensating (too much motor power) when transitioning to a hover. To reduce this, apply a negative D value (try decreasing it by decrements of -5) to reduce this effect.

You will probably repeat steps 12-16 until you are happy with the ArduPirate's flight performance. Also consider adjusting the Transmitter Factor. A higher number will cause a change in a transmitter stick position to have a stronger effect on the ArduPirate. A lower number will soften transmitter commands.

If you find that the ArduPirate acts "jittery" even when holding it in your hand with some throttle applied, the gyros may be seeing some noise (may be vibration detected through the frame, etc.). To smooth this noise out, adjust the Gyro Smooth Factor to a lower number. Be careful setting this value too low will cause a slower response from the gyro, making the ArduPirate fly "floppy", or not as "stiff" in the air.

## **Tuning your ArduPirate for Stable Mode** ##
You should have a well tuned set of parameters in Acro Mode before starting to tune for Stable Mode. Also, be sure to calibrate the quad on a level surface to zero all sensors. When flying in Acro Mode, adjust the transmitter trims to achieve a level hover with as little pilot input as possible. When starting tuning for Stable Mode, use the P and D values from Acro Mode as the starting pitch and roll gyro values in Stable Mode to begin with. _For the Stable Mode accel pitch and roll, use P=4.0 and I=0.150 as starting values._

  1. Now CAREFULLY hold the quad in your hand and slowly increase throttle around 30-50%. Increase the P Accel Roll/Pitch value (suggested increasing value by 1-5 each time) until the quad returns to level on it's own. There will typically be oscillation when the P Accel value is increased. To reduce this fast oscillation, decrease the negative value of the D Gyro (suggested value is by -5 each time) until the oscillation goes away.
  1. Now it's time for flight testing. As you fly, if you see that the QuadCopter is not responsive enough to get to zero degrees along the roll and pitch axis, start increasing the P Accel. If you get fast oscillations during flight, try decreasing the negative D Gyro value for better stability. Tuning is always a difficult process, so expect many interations to increase/decrease values to suit your quad's capabilities. It helps to have a wireless system during this process (link to wireless tutorial [here](http://code.google.com/p/ardupirates/wiki/Xbee)).
  1. When you are satisfied with this stage, you can add manual transmitter trim to fix level flight, as the sensors, motors and frame may not be exactly square with one another. When you have some reasonable level flight, try to flick the transmitter stick along the roll or pitch axis. What you should see is the QuadCopter should self correct itself without oscillation. Adjust values again to your satisfaction.
  1. The next step is to choose an Integral Accel value. This will "auto trim" your quad further during flight. Fly the QuadCopter to a stable hover and adjust the trims so that you can fly as best as possible in the same spot without any pilot input. When ever you adjust the trims manually with the transmitter, you must land, then take off for the new trimmed values to be recognized. After manually trimming, if you still see additional drift, try to increase the Integral Accel value in increments of 5 to 10 until the QuadCopter can "auto trim" or stay in the same spot for as long as possible.
  1. The last step is to configure the Level Off value through the Configurator. You can use the Transmitter Adjustment screen to help you with this next setup step. This defines the amount of transmitter stick input that will turn off the "auto trim" function during forward flight. When this value is exceeded, it will reset the "auto trim" internal values so that an unexpected offset in level flight does not appear when transitioning from forward flight to a hover. As you move the stick input past the Level Off value, you shoudl expect to see the Red LED turn off indicating the Level Off value has been exceeded.

That's it! You'll need a lot of patience to get your settings just right, so please expect a lot of trial and error. These instructions are general guidelines, if you have any feedback on improvements on any of these steps, please post them here or in [our forum](http://www.rcgroups.com/forums/showthread.php?t=1286011). Have fun flying your ArduPirate!