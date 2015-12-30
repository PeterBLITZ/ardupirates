# Loading the code #

After installing the different softwares, you are ready to load the code into the board.

![http://arducopter.googlecode.com/svn/images/warning_rc_connection.png](http://arducopter.googlecode.com/svn/images/warning_rc_connection.png)

Plug the board into the USB. If you have ESCs connected to the board, you must also be powering the board via a battery (though the power distribution board if you're using the standard ArduCopter frame). If you do have the ESCs connected to the APM board and you don't power the independently of the USB cable, the code will not load and you may blow your outputs. (You can tell you're overloading your board if your APM LEDs are dim).

Launch Arduino

![http://lh4.ggpht.com/_SLFLtGv2hms/TUB5jXQSvzI/AAAAAAAAA9k/Ibea0YwxtC8/s800/arduino.PNG.jpg](http://lh4.ggpht.com/_SLFLtGv2hms/TUB5jXQSvzI/AAAAAAAAA9k/Ibea0YwxtC8/s800/arduino.PNG.jpg)

Go to -> File -> Preferences and set the location for your sketches. (Choose the folder that resides above the "ArduCopter-firmware" folder.)

Go to -> Tools -> Board and choose the "Arduino Mega (ATmega1280)" as your board.

Go to -> Tools -> Serial Port and choose the port that was previously set up after you installed the USB Serial Driver (usually COM3)

Go to -> Tools -> Serial Monitor and set the port speed to 115200 baud

Then go to -> File -> Sketchebook -> Sketches -> ArduPiratesNG

![http://lh5.ggpht.com/_SLFLtGv2hms/TUB93AoAFII/AAAAAAAAA9s/BpBW5z6NBgM/s800/arduino1.PNG.jpg](http://lh5.ggpht.com/_SLFLtGv2hms/TUB93AoAFII/AAAAAAAAA9s/BpBW5z6NBgM/s800/arduino1.PNG.jpg)

Verify that your config.h is matching your setup

![http://lh5.ggpht.com/_SLFLtGv2hms/TUB-9_2BkaI/AAAAAAAAA90/r5Kjx9HH89k/s800/arduino2.PNG.jpg](http://lh5.ggpht.com/_SLFLtGv2hms/TUB-9_2BkaI/AAAAAAAAA90/r5Kjx9HH89k/s800/arduino2.PNG.jpg)

When done, click on the "Compile" button if you've made changes, and then on the "Upload" button. It will take a minute or two for the code to compile, after which the Arduino IDE will report "Binary sketch size: XXX bytes (of a 126976 byte maximum) " in the status window at the bottom. Then it will start uploading the compiled code to APM, during which the RX and TX LEDS on the IMU shield will rapidly flash (showing data transfer). After about a minute, the LEDs will go out and the Arduino IDE will report that the code upload is complete.