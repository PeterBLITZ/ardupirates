# Mode Switch #

The next step is to choose your FLIGHT MODE.  This is done by setting switch 3 of the DIP switch on your APM to on or of.  After switching to another flight mode you will need to reboot your APM by  disconnecting and reconnecting power.

**Acrobatic mode:** your multicopter will be handling as a traditional RC  helicopter. You will have to constantly correct it's flight path to keep it  up in the air - like balancing on a ball. On the other hand, this is the mode  that will allow some aerobatics.

**Stable mode:** your multicopter will be automatically piloted by the APM and in  an ideal configuration you'll only have to command it with your transmitter  when you want it to change it's position in the air. Stability, horizontal  attitude, altitude etc. will be controller by the autopilot functionality.


| **DIP3 Position** | **Mode** | **Yellow LED (B)** | **Remark** |
|:------------------|:---------|:-------------------|:-----------|
| ON (down)         | Acrobatic | Flashing           |            |
| OFF (up)          | Stable   | Constant           | AUTOPILOT MODE Status LEDs apply |

AUTOPILOT MODE status LEDs and MODES:

| **AUX 2** | **AUX 1** | **MODE** | **AP\_Mode** | **Yellow LED (B)** | **Red LED (C)** | **Remark** |
|:----------|:----------|:---------|:-------------|:-------------------|:----------------|:-----------|
| OFF       | OFF       | Stable   | 2            | OFF                | OFF             |            |
| OFF       | ON        | Altitude Hold only | 3            | ON                 | OFF             |            |
| ON        | OFF       | Position Hold only | 4            | OFF                | ON              | If the Red LED is flashing, the GPS data is not being logged. |
| ON        | ON        | Position & Alt. Hold | 5            | ON                 | ON              | If the Red LED is flashing, the GPS data is not being logged. |

TIP: in the Configurator, the MODE channel is what we refer to as AUX2.

![http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/APM_IMU.jpg](http://ardupilot-mega.googlecode.com/svn/ArduPilotMegaImages/APM_IMU.jpg)

Here's a quick video about LED's meaning and flying modes.

http://www.vimeo.com/23088502