# Online Universal Ardu Configurator #

This is a really useful tool, that create a config.h file with all your personal settings. You will find it [here](http://ardupirates.net/config/).

Select your platform:

![http://lh5.ggpht.com/_SLFLtGv2hms/TUK8GKJic_I/AAAAAAAAA-I/1JFt8H7bfbw/s800/config001.PNG.jpg](http://lh5.ggpht.com/_SLFLtGv2hms/TUK8GKJic_I/AAAAAAAAA-I/1JFt8H7bfbw/s800/config001.PNG.jpg)

  * Select your aiframe type and your flight mode.
  * High Altitude Dectection uses a pressure sensor and it's built-in on the IMU Shield/Oilpan.
  * Low Altitude Detection uses a Sonar. If you want to know how to install one, read this [how-to](http://code.google.com/p/arducopter/wiki/Quad_Sonar)
  * Obstacle avoidance is not used with ArduPiratesNG at the moment.

![http://lh6.ggpht.com/_SLFLtGv2hms/TUK8GYHKQAI/AAAAAAAAA-M/10f7OyqHwVc/s800/config002.PNG.jpg](http://lh6.ggpht.com/_SLFLtGv2hms/TUK8GYHKQAI/AAAAAAAAA-M/10f7OyqHwVc/s800/config002.PNG.jpg)

**informations about the camera shutter [here](Cam_Shutter.md)**

![http://lh3.ggpht.com/_SLFLtGv2hms/TUK8GqU686I/AAAAAAAAA-Q/zXeEo9mompc/s800/config003.PNG.jpg](http://lh3.ggpht.com/_SLFLtGv2hms/TUK8GqU686I/AAAAAAAAA-Q/zXeEo9mompc/s800/config003.PNG.jpg)

  * informations about the battery alarm [here](BatteryAlarmHowto.md)
  * informations about telemetry [here](http://code.google.com/p/ardupilot-mega/wiki/Wireless)
  * informations about PID tuning during flight are [here](PID_tuning_Inflight.md)


![http://lh3.ggpht.com/_SLFLtGv2hms/TUK8HT_rJjI/AAAAAAAAA-U/w-E1ZHaL__s/s800/config004.PNG.jpg](http://lh3.ggpht.com/_SLFLtGv2hms/TUK8HT_rJjI/AAAAAAAAA-U/w-E1ZHaL__s/s800/config004.PNG.jpg)



After choosing all your settings and options, click on "Validate Options" and then "Generate Code"

The code is now generated and will be displayed like this:

![http://lh4.ggpht.com/_SLFLtGv2hms/TUK8HtynjsI/AAAAAAAAA-Y/rb7eKaAtUbU/s800/config005.PNG.jpg](http://lh4.ggpht.com/_SLFLtGv2hms/TUK8HtynjsI/AAAAAAAAA-Y/rb7eKaAtUbU/s800/config005.PNG.jpg)

You have now two options:
  * Copy and overwrite the code of your config.h file
  * Click on "Save Code" and follow instructions