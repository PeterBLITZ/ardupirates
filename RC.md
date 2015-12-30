# Connecting your RC #

([ArduCopter's source](http://code.google.com/p/arducopter/wiki/Quad_Radio))

What you'll need:

  * At least a 5-channel RC unit. 7 channels or more is highly recommended
  * Female-to female cables for each channel you'll be using. We use [these short ones](http://store.diydrones.com/product_p/pr-0003-03-5cm.htm) to minimize cable clutter.
  * A power source. Use one of your ESC's or an additional battery (if you use a 2S lipo, don't forget to place a BEC between the battery and the APM!) ArduPilot Mega gets its power from the RC system.


---


![http://arducopter.googlecode.com/svn/images/warning_rc_connection.png](http://arducopter.googlecode.com/svn/images/warning_rc_connection.png)

Note: If you experience the brown-outs and you want to keep Lipo battery disconnected, try to take off the APM power cable from the Power Distribution Board (PDB).

### Instructions: ###

First, a note on pin numbering. Early version of the APM board (1.0) numbered the eight RC in and out pins 0-7 (engineers!). More recent versions of the APM board (from 1.4 on up) use the standard RC convention of 1-8. You can see the difference between the two boards here:

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4936.jpg](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/IMG_4936.jpg)

The diagrams below will refer to these two versions

**Note**: When you're plugging your motors into the ESCs, you'll see that there are typically three wires of different colors. It doesn't matter which color goes where and those wire colors actually vary from batch to batch. Just connect them randomly and then when it come time to [set correct prop spin direction](http://code.google.com/p/arducopter/wiki/Quad_ESC), just switch any two wires on any motor that needs to be reversed.

**For APM 1.0:**

![http://arducopter.googlecode.com/svn/images/DiagramRC.jpg](http://arducopter.googlecode.com/svn/images/DiagramRC.jpg)

**For APM 1.4 and above:**

![http://arducopter.googlecode.com/svn/images/DiagramRC_APM14.jpg](http://arducopter.googlecode.com/svn/images/DiagramRC_APM14.jpg)

**Hexa setup for APM 1.0 & 1.4**

![https://lh5.googleusercontent.com/_SLFLtGv2hms/TbCnioiuxWI/AAAAAAAABDY/AX6ZWzykUVs/s800/Arducopter%2BHexa%2BConfig.jpg](https://lh5.googleusercontent.com/_SLFLtGv2hms/TbCnioiuxWI/AAAAAAAABDY/AX6ZWzykUVs/s800/Arducopter%2BHexa%2BConfig.jpg)

### Choosing between "X" and "+" flight directions ###

You can either fly with one arm forward ("+" configuration) or with two arms at 45 degrees on either side of the direction of motion ("x" configuration). The first is easier to fly for many people because you can tell which way is the front (most people paint the forward arm red or put a ping ping ball on the forward leg so they can see it from the ground). The second is preferred by people who are flying with a camera, since the forward view is between the two arms and not obscured.

The way to select which mode you want is with the DIP switch on the IMU board:

![http://arducopter.googlecode.com/svn/images/Flight_Mode_Selection.jpg](http://arducopter.googlecode.com/svn/images/Flight_Mode_Selection.jpg)


Flight Mode is determined by DIP Switch 1. Up is "x" mode; Down is "+" mode.

### Wiring diagram for "+" config: ###

**For APM 1.0:**

![http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-PlusSetup.jpg](http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-PlusSetup.jpg)

**For APM 1.4 and above:**

![http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-PlusSetup_APM14.jpg](http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-PlusSetup_APM14.jpg)

### Wiring diagram for "x" config: ###

**Note:** _In "X" config, the ArduPiratesNG code also supports the APM pointing between front arms, i.e. motors 2 and 0 for APM 1.0, and motors 3 and 1 for APM 1.4._

**For APM 1.0:**

![http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-XSetup.jpg](http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-XSetup.jpg)

**For APM 1.4 and above:**

![http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-XSetup_APM14.jpg](http://arducopter.googlecode.com/svn/images/instructional/AC-Wiring-XSetup_APM14.jpg)


Connect your RC receiver to ArduPilot Mega (APM) with female-to-female cables. Each channel that you want APM to control should be connected to an Input on the APM board. When you follow the right-angle connector, you'll see that the signal pins are those that connect to the board furthest from the edge; the ground pins are closest to the edge. Usually signal is white or yellow; ground is black.

Here's an example of APM, the power distribution board and a RC receiver all connected correctly:

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/Powerlines_APM_RC.jpg](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/Powerlines_APM_RC.jpg)

The first four channels are controlled by APM's multiplexer. To use this feature you should connect the channel on your RC receiver that you want to use to select the autopilot mode to the last channel input (marked as 7 on the board) on ArduPilot Mega.

When you place APM in your aircraft, it is very important that it face the right way. The GPS connector should face forward, and the servo cables face back. Like this (note: there's a little arrow on the bottom of the shield that point to the front, too, in case you need a reminder at the field):

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/imudirection2.jpg](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/imudirection2.jpg)