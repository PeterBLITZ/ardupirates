# Position Hold & Altitude Hold #

### altitude hold ###

  1. At field, before flight you could check that you can enable/disable Altitude hold with your transmitter switch.
  1. Start flying in Stable mode with altitude hold disable. When you reach the desired point enable altitude hold.

It´s important to know that the altitude hold control takes your actual Throttle position as "hovering point" and aircraft will maintain the current altitude. Moving the throttle stick will not increase or decrease altitude. So it´s important that you are not ascending/descending when you switch to another mode.

| **AUX 2** | **AUX 1** | **MODE** | **AP\_Mode** | **Yellow LED (B)** | **Red LED (C)** |
|:----------|:----------|:---------|:-------------|:-------------------|:----------------|
| OFF       | ON        | Altitude Hold only | 3            | ON                 | OFF             |


### Position Hold ###

  1. First, it´s important that your Stable Mode is working fine.
  1. You need to wait until GPS gets a lock. The Red LED as to be solid (it´s better to wait a bit more to get more satellites).
  1. At field, before flight you could check that you can enable/disable GPS position hold with your transmitter switch.
  1. Start flying in Stable mode with position hold disable. When you reach the desired point enable GPS position hold.

| **AUX 2** | **AUX 1** | **MODE** | **AP\_Mode** | **Yellow LED (B)** | **Red LED (C)** | **Remark** |
|:----------|:----------|:---------|:-------------|:-------------------|:----------------|:-----------|
| ON        | OFF       | Position Hold only | 4            | OFF                | ON              | If the Red LED is flashing, the GPS data is not being logged. |

the aircraft will maintain its current GPS coordinates. You can still control the throttle to change altitude and yaw to change the aircraft heading. If you command a roll or pitch the aircraft will break from GPS hold to respond to your commands. Letting go of the roll/pitch stick will force the aircraft to return to the original GPS hold position. To set a new GPS Hold position you must exit GPS Hold AND Altitude Hold modes (Red & Amber LED off), move to the desired position and reengage GPS hold.


### position hold & altitude hold togheter ###

After testing separately both modes, you can enable them togheter.

| **AUX 2** | **AUX 1** | **MODE** | **AP\_Mode** | **Yellow LED (B)** | **Red LED (C)** | **Remark** |
|:----------|:----------|:---------|:-------------|:-------------------|:----------------|:-----------|
| ON        | ON        | Position & Alt. Hold | 5            | ON                 | ON              | If the Red LED is flashing, the GPS data is not being logged. |


### General behavior ###

The aircraft should stay in an around 4-5 meters "circle" around the target position, depending on your PID settings.

If there are some wind, it takes some seconds to the control to compensate for this. If the wind is stronger you could see that the control could not compensate at all and you will have more error (ex 10-15 meters).

Actual altitude control is a very simple solution so you could expect to be into 1-2 meters error.