# Ardu Pirate's Motor Mixing Guidelines #

Hi Pirates, based on Hein's comments, I'm going to try to explain how is the mixing of the motors done, depending on the layout/position of your ship's motors.

All mixing depends directly on the motor's positions and layout proportions.

Depending on it's position each motor needs to apply different strength to pitch and roll axles. In other words, the farthest a motor is from a axle, the stronger it will have to pull.

This strength is set for each motor in the motor mixing (motors.pde) as meanings of a percentual value, that will multiply pitch and roll values used to calculate the final value for each motor.

When the percentage is 100%, it is not necessary to add any value, as we would be multiplying by 1.

## Pitch and Roll Axles ##
Pitch and Roll axles will be used as reference to  find the needed strength for each motor:

![http://ardupirates.googlecode.com/svn/Images/motormixing/motmix00.jpg](http://ardupirates.googlecode.com/svn/Images/motormixing/motmix00.jpg)

The axles layout will help to understand and locate what will the values for each motor be, and it's sign.

As for the Yaw, things are simpler, as it just depends on the rotation direction of the motor:

If Motor turns ClockWise (CW) Yaw is  **Negative**.

If Motor turns CounterClockWise (CCW) Yaw is **Positive**.

I will try to explain it using exampes for the frames we commonly use:

## Quad + mode ##
Ok, let's start with a simple Quad in + mode:

![http://ardupirates.googlecode.com/svn/Images/motormixing/motmix01.jpg](http://ardupirates.googlecode.com/svn/Images/motormixing/motmix01.jpg)

As all 4 motors are on the axles, they all will have 100% and 0% as strength values, in more detail:

|     Motor    | Pitch | Roll  | Yaw |
|:-------------|:------|:------|:----|
|  0 Right CCW |  -0%  | -100% |  +  |
|  1 Left  CCW |  +0%  | +100% |  +  |
|  2 Front  CW |+100%  |    0% |  -  |
|  3 Back   CW |-100%  |    0% |  -  |


Thus the resulting pseudo-code would have these signs and values:


rightCCW = - (0 `*` Pitch) - (1 `*` Roll) + Yaw

leftCCW  = + (0 `*` Pitch) + (1 `*` Roll) + Yaw

frontCW  = + (1 `*` Pitch) + (0 `*` Roll) - Yaw

backCW   = - (1 `*` Pitch) - (0 `*` Roll) - Yaw


and the final code in Motors.pde, adding throttle and controlling total motor value not to get out of bounds, would be:


rightCCW = constrain(throttle                 - control\_roll + control\_yaw, minThrottle, 2000);

leftCCW  = constrain(throttle                 + control\_roll + control\_yaw, minThrottle, 2000);

frontCW  = constrain(throttle + control\_pitch                - control\_yaw, minThrottle, 2000);

backCW   = constrain(throttle - control\_pitch                - control\_yaw, minThrottle, 2000);



## Quad X mode ##
Next, Quad in X mode:

![http://ardupirates.googlecode.com/svn/Images/motormixing/motmix02.jpg](http://ardupirates.googlecode.com/svn/Images/motormixing/motmix02.jpg)

This tiem all 4 motor are rotated 45º from the axles, they all will have 71% as strength values in pitch and roll, in more detail:

|       Motor       | Pitch | Roll  | Yaw |
|:------------------|:------|:------|:----|
|  0 FrontRight CCW | +71%  |  -71% |  +  |
|  1 BackLeft   CCW | -71%  |  +71% |  +  |
|  2 FrontLeft  CW  | +71%  |  +71% |  -  |
|  3 BackRight  CW  | -71%  |  -71% |  -  |


Thus the resulting pseudo-code should have these signs and values:


FrontRightCCW = +(0.71 `*` Pitch) - (0.71 `*` Roll) + Yaw

BackLeftCCW   = -(0.71 `*` Pitch) + (0.71 `*` Roll) + Yaw

FrontLeftCW   = +(0.71 `*` Pitch) + (0.71 `*` Roll) - Yaw

BackRightCW   = -(0.71 `*` Pitch) - (0.71 `*` Roll) - Yaw


BUT, as we would be lowering the overall power to 71%, and all values are the same we can simply uses 100% for all values, this way we're still keeping the same proportion of strength between motors, so there will be no difference in stability, but we will be getting all the available power.

and the final code in Motors.pde, adding throttle and controlling total motor value not to get out of bounds, would be:


FrontRightCCW = constrain(throttle + control\_pitch - control\_roll + control\_yaw, minThrottle, 2000);

BackLeftCCW   = constrain(throttle - control\_pitch + control\_roll + control\_yaw, minThrottle, 2000);

FrontLeftCW   = constrain(throttle + control\_pitch + control\_roll - control\_yaw, minThrottle, 2000);

BackRightCW   = constrain(throttle - control\_pitch - control\_roll - control\_yaw, minThrottle, 2000);


## Hexa classic mode ##
Next, HEXA in classic mode, with all motors at the same distance from center frame:

![http://ardupirates.googlecode.com/svn/Images/motormixing/motmix03.jpg](http://ardupirates.googlecode.com/svn/Images/motormixing/motmix03.jpg)

More in detail:

|       Motor       | Pitch | Roll | Yaw |
|:------------------|:------|:-----|:----|
|  7 Front      CW  | +100% |  +0% |  -  |
|  2 FrontLeft  CCW |  +50% | +87% |  +  |
|  1 BackLeft   CW  |  -50% | +87% |  -  |
|  4 FrontRight CCW |  +50% | -87% |  +  |
|  3 BackRight  CW  |  -50% | -87% |  +  |
|  8 Back       CCW | -100% | -0%  |  -  |


Thus the resulting pseudo-code should have these signs and values:


FrontCW       = + (  1 `*` Pitch) + (   0 `*` Roll) - Yaw

FrontLeftCCW  = + (0.5 `*` Pitch) + (0.87 `*` Roll) + Yaw

BackLeftCW    = - (0.5 `*` Pitch) + (0.87 `*` Roll) - Yaw

FrontRightCCW = + (0.5 `*` Pitch) - (0.87 `*` Roll) + Yaw

BackRightCW   = - (0.5 `*` Pitch) - (0.87 `*` Roll) - Yaw

BackCCW       = - (  1 `*` Pitch) - (   0 `*` Roll) + Yaw


So all 4 left and righ motors will have a pitch strength of 50% and roll of 87% while front and back will only have 100% on pitch and no effect on roll.

and the final code in Motors.pde, adding throttle and controlling total motor value not to get out of bounds, would be:


FrontCW       = constrain(throttle + control\_pitch                                 - control\_yaw, minThrottle, 2000);

FrontLeftCCW  = constrain(throttle + (0.5 `*` control\_pitch) + (0.87 `*` control\_roll) + control\_yaw, minThrottle, 2000);

BackLeftCW    = constrain(throttle - (0.5 `*` control\_pitch) + (0.87 `*` control\_roll) - control\_yaw, minThrottle, 2000);

FrontRightCCW = constrain(throttle + (0.5 `*` control\_pitch) - (0.87 `*` control\_roll) + control\_yaw, minThrottle, 2000);

BackRightCW   = constrain(throttle - (0.5 `*` control\_pitch) - (0.87 `*` control\_roll) - control\_yaw, minThrottle, 2000);

BackCCW       = constrain(throttle - control\_pitch                                 + control\_yaw, minThrottle, 2000);


**NOTE**: The current NG code, is not using the 87% on roll, but 100%; in this case it is like the all four left and right motors were a bit farther away from center (13%).


## Hexa H mode ##
Next, HEXA in H mode, with three motors on each side in a "H" shape, forming a perfect square:

![http://ardupirates.googlecode.com/svn/Images/motormixing/motmix04.jpg](http://ardupirates.googlecode.com/svn/Images/motormixing/motmix04.jpg)

More in detail:

|       Motor       | Pitch |  Roll | Yaw |
|:------------------|:------|:------|:----|
|  1 FrontLeft  CW  | +100% | +100% |  -  |
|  6 Left       CCW |   +0% | +100% |  +  |
|  5 BackLeft   CW  | -100% | +100% |  -  |
|  2 FrontRight CCW | +100% | -100% |  +  |
|  3 Right      CW  |   -0% | -100% |  -  |
|  4 BackRight  CCW | -100% | -100% |  +  |


Thus the resulting pseudo-code should have these signs and values:


FrontLeftCW   = + (1 `*` Pitch) + (1 `*` Roll) - Yaw

LeftCCW       = + (0 `*` Pitch) + (1 `*` Roll) + Yaw

BackLeftCW    = - (1 `*` Pitch) + (1 `*` Roll) - Yaw

FrontRightCCW = + (1 `*` Pitch) - (1 `*` Roll) + Yaw

BackRightCW   = - (0 `*` Pitch) - (1 `*` Roll) - Yaw

BackCCW       = - (1 `*` Pitch) - (1 `*` Roll) + Yaw


So in this case as all values are 100%, no value is needed, only signs.

and the final code in Motors.pde, adding throttle and controlling total motor value not to get out of bounds, would be:


FrontCW       = constrain(throttle + control\_pitch + control\_roll - control\_yaw, minThrottle, 2000);

FrontLeftCCW  = constrain(throttle + control\_pitch + control\_roll + control\_yaw, minThrottle, 2000);

BackLeftCW    = constrain(throttle - control\_pitch + control\_roll - control\_yaw, minThrottle, 2000);

FrontRightCCW = constrain(throttle + control\_pitch - control\_roll + control\_yaw, minThrottle, 2000);

BackRightCW   = constrain(throttle - control\_pitch - control\_roll - control\_yaw, minThrottle, 2000);

BackCCW       = constrain(throttle - control\_pitch - control\_roll + control\_yaw, minThrottle, 2000);


**NOTE**: If the sides where to be closer to each other, a roll value would have to be added, as  they would have to have less strength than pitch; and viceversa, if you got your front and back motors closer, you would have to add a pitch value.


## Octo classic mode ##
Next, Octo in classic mode, with one motor at front and back:

![http://ardupirates.googlecode.com/svn/Images/motormixing/motmix05.jpg](http://ardupirates.googlecode.com/svn/Images/motormixing/motmix05.jpg)

More in detail:

|       Motor       | Pitch |  Roll | Yaw |
|:------------------|:------|:------|:----|
|  0 Front      CW  | +100% |   +0% |  -  |
|  1 FrontRight CCW |  +71% |  -71% |  +  |
|  2 Right      CW  |   +0% | -100% |  -  |
|  3 BackRight  CCW |  -71% |  -71% |  +  |
|  6 Back       CW  |   -0% |   -0% |  -  |
|  7 BackLeft   CCW |  -71% |  +71% |  +  |
|  9 Left       CW  |   +0% | +100% |  -  |
| 10 FrontLeft  CCW |  +71% |  +71% |  +  |

Thus the resulting pseudo-code should have these signs and values:


FrontCW       = + (1    `*` Pitch) + (0    `*` Roll) - Yaw

FrontRightCCW = + (0.71 `*` Pitch) - (0.71 `*` Roll) + Yaw

RightCW       = + (0    `*` Pitch) - (1    `*` Roll) - Yaw

BackRightCCW  = - (0.71 `*` Pitch) - (0.71 `*` Roll) + Yaw

BackCW        = - (0    `*` Pitch) - (0    `*` Roll) - Yaw

BackLeftCCW   = - (0.71 `*` Pitch) + (0.71 `*` Roll) + Yaw

LeftCW        = + (0    `*` Pitch) + (1    `*` Roll) - Yaw

FrontLeftCCW  = + (0.71 `*` Pitch) + (0.71 `*` Roll) + Yaw


and the final code in Motors.pde, adding throttle and controlling total motor value not to get out of bounds, would be:


FrontCW       = constrain(throttle +       control\_pitch                            - control\_yaw, minThrottle, 2000);

FrontRightCCW = constrain(throttle + (0.71 `*` control\_pitch) - (0.71 `*` control\_roll) + control\_yaw, minThrottle, 2000);

RightCW       = constrain(throttle                          -         control\_roll  - control\_yaw, minThrottle, 2000);

BackRightCCW  = constrain(throttle - (0.71 `*` control\_pitch) - (0.71 `*` control\_roll) + control\_yaw, minThrottle, 2000);

BackCW        = constrain(throttle -         control\_pitch                          - control\_yaw, minThrottle, 2000);

BackLeftCCW   = constrain(throttle - (0.71 `*` control\_pitch) + (0.71 `*` control\_roll) + control\_yaw, minThrottle, 2000);

LeftCW        = constrain(throttle                          +         control\_roll  - control\_yaw, minThrottle, 2000);

FrontLeftCCW  = constrain(throttle + (0.71 `*` control\_pitch) + (0.71 `*` control\_roll) + control\_yaw, minThrottle, 2000);


## Octo X mode ##
Next, Octo in "Quad" mode, 2 motors at the front, 2 at the back, 2 left and 2 right:

![http://ardupirates.googlecode.com/svn/Images/motormixing/motmix06.jpg](http://ardupirates.googlecode.com/svn/Images/motormixing/motmix06.jpg)

More in detail:

|       Motor   | Pitch |  Roll | Yaw |
|:--------------|:------|:------|:----|
|  0 Front  CW  |  +92% |  -38% |  -  |
|  1 Front  CCW |  +92% |  +38% |  +  |
|  2 Left   CW  |  +38% |  +92% |  -  |
|  3 Left   CCW |  -38% |  +92% |  +  |
|  6 Right  CW  |  -38% |  -92% |  -  |
|  7 Right  CCW |  +38% |  -92% |  +  |
|  9 Back   CW  |  -92% |  +38% |  -  |
| 10 Back   CCW |  -92% |  -38% |  +  |

To be able to have full power, I'll increase values to 100%, so proportionally they will be:  92% = 100%  and 38% = 42%, thus giving these values:


Thus the resulting pseudo-code should have these signs and values:


FrontCW  = + (1    `*` Pitch) - (0.42 `*` Roll) - Yaw

FrontCCW = + (1    `*` Pitch) + (0.42 `*` Roll) + Yaw

LeftCW   = + (0.42 `*` Pitch) + (1    `*` Roll) - Yaw

LeftCCW  = - (0.42 `*` Pitch) + (1    `*` Roll) + Yaw

RightCW  = - (0.42 `*` Pitch) - (1    `*` Roll) - Yaw

RightCCW = + (0.42 `*` Pitch) - (1    `*` Roll) + Yaw

BackCW   = - (1    `*` Pitch) + (0.42 `*` Roll) - Yaw

BackCCW  = - (1    `*` Pitch) - (0.42 `*` Roll) + Yaw


and the final code in Motors.pde, adding throttle and controlling total motor value not to get out of bounds, would be:


FrontCW  = constrain(throttle + control\_pitch          - (0.42 `*` control\_roll) - control\_yaw, minThrottle, 2000); // Front Motor CW

FrontCCW = constrain(throttle + control\_pitch          + (0.42 `*` control\_roll) + control\_yaw, minThrottle, 2000); // Front Motor CCW

LeftCW   = constrain(throttle + (0.42 `*` control\_pitch) + control\_roll          - control\_yaw, minThrottle, 2000); // Left Motor CW

LeftCCW  = constrain(throttle - (0.42 `*` control\_pitch) + control\_roll          + control\_yaw, minThrottle, 2000); // Left Motor CCW

RightCW  = constrain(throttle - (0.42 `*` control\_pitch) - control\_roll          - control\_yaw, minThrottle, 2000); // Right Motor CW

RightCCW = constrain(throttle + (0.42 `*` control\_pitch) - control\_roll          + control\_yaw, minThrottle, 2000); // Right Motor CCW

BackCW   = constrain(throttle - control\_pitch          + (0.42 `*` control\_roll) - control\_yaw, minThrottle, 2000); // Back Motor CW

BackCCW  = constrain(throttle - control\_pitch          - (0.42 `*` control\_roll) + control\_yaw, minThrottle, 2000); // Back Motor CCW



## Files to Check when adding new motor mixing ##

  1. COMMS.pde (Show\_Platform\_Info())
  1. Motors.pde (motor\_output())
  1. system.pde (APM\_Init())





Well Pirates, that's about it, I hope I've been able to enlighten a bit more your pirte minds! Now you can go and design your own weird shape!!

I hope you got the idea!


any comments / addings / corrections are welcome!!


Happy Landings,

Dani