# Calibrating ESC's #

([ArduCopter's source)](http://code.google.com/p/arducopter/wiki/Quad_ESC)

Usually all calibration will be done through the configurator. For now you can only initialize the EEPROM and calibrate your tx/rx within the version 1.22.

![http://www.board-portal.de/Turnigy18ABasic.jpg](http://www.board-portal.de/Turnigy18ABasic.jpg)

So you'll have to do the calibration manually:

Plug each ESC attached to the motors into the throttle channel of your rx.

Switch on your tx and go to full throttle. Plug in the lipo for the motors. You'll here 2 series of beeps confirming the correct calibration.

Then pull your throttle back to zero and wait for the beep code. When done unplug the lipo, switch the tx off, plug the lipos again and switch on the tx again.

Do this for each ESC you want to calibrate.

After the calibration is done you can plug all cables in place and try to arm the motors by pushing the throttle stick to the lower right position.

To disarm the motors push it to the lower left.

### Getting your motors turning the right direction ###

Motor direction must be checked and set by hand. You do that by switching wires connecting the motors and ESCs for any motor turning the wrong direction.

You'll see that there are typically three wires of different colors coming out of the motors. It doesn't matter which color goes where and those wire colors actually vary from batch to batch. Just connect them randomly and then when we set correct prop spin direction below, you can just switch any two wires on any motor that needs to be reversed.

### Output Port and Propeller Orientation ###
**ArduCopter Quad uses 4 motors in total and they are named as:**

**+ Configuration**

|**APM 1.0**|**APM 1.4**|
|:----------|:----------|
|OUT0 = Right motor (CCW)|OUT1 = Right motor (CCW)|
|OUT1 = Left motor (CCW)|OUT2 = Left motor (CCW)|
|OUT2 = Front motor (CW)|OUT3 = Front motor (CW)|
|OUT3 = Rear motor (CW)|OUT4 = Rear motor (CW)|



**X Configuration**

|**APM 1.0**|**APM 1.4**|
|:----------|:----------|
|OUT0 = Right motor (CCW)|OUT1 = Right motor (CCW)|
|OUT1 = Left motor (CCW)|OUT2 = Left motor (CCW)|
|OUT2 = Front motor (CW)|OUT3 = Front motor (CW)|
|OUT3 = Rear motor (CW)|OUT4 = Rear motor (CW)|


CCW = Counter-clockwise rotation (eg backwards) CS = Clockwise (eg rotating the same direction as a clock's hands)

### Wiring diagram for "+" config: ###

**For APM 1.0:**

![http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-PlusSetup.jpg](http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-PlusSetup.jpg)

**For APM 1.4 and above:**

![http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-PlusSetup_APM14.jpg](http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-PlusSetup_APM14.jpg)

### Wiring diagram for "x" config: ###

**For APM 1.0:**

![http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-XSetup.jpg](http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-XSetup.jpg)

**For APM 1.4 and above:**

![http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-XSetup_APM14.jpg](http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-XSetup_APM14.jpg)