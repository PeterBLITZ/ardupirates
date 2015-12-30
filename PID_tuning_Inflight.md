# Inflight PID Tuning #

Small Manual for PID tuning.

This part of the code could have been made smaller but I wanted to keep simplicity so that most of us can understand the code.... I am very prode of this part because I think it will help a lot of us to fine tune your Copter....including me...

How it works:

You will first need to have telemetry using serial port 3. Use\_PID\_Tuning & define SerXbee must be uncommend to work.

You must also use channel 7 (flight mode 3 position switch). From mid position switching away from you and back to mid position increase a variable (example P\_factor value) and, from mid position pulling switch towards you and back to mid position decrease a variable.

In configurator you will use the Serial Monitor. We will only use small letters for commands that we send by pressing send command button.

List of Commands. (most of them will make sense but not all)

"o" - Switch PID tuning on.

"f" - Switch PID tuning off.

"r" - Tuning Roll and Pitch. If you are in Acro it will tune Acro, If in Stable you will tune Stable mode.

"w" - Tuning Yaw Variables.

"b" - Tuning Barometric sensor.

"s" - Tuning Sonar Sensor.

"g" - Tuning GPS.

"p" - Tuning the P\_factor of PID

"i" - Tuning the I\_factor of PID
"d" - Tuning the D\_factor of PID

"x" - Tuning the Accelerometer Roll offset

"y" - Tuning the Accelerometet Pitch offset

"a" - Tuning the Camera Pitch Smooth factor.

"e" - Tuning the Camera Roll Smooth factor.

"c" - Tuning the Camera Centre factor.

"h" - Tuning the Camera Focus servo position.

"t" - Tuning the Camera Trigger (Shutter) servo position.

"j" - Tuning the Camera Release servo position.

Example: We want to tune the Roll and pitch PID of Stable flight.

We first send "o" switch PID on.
By default Roll and Pitch is already enabled and P\_factor of PID also enabled.

Now you start tuning while flying your P value. land then send "i" for I\_factor of PID. Tune it also while flying. Finely send "d" and start tuning D\_factor of PID while flying.

After that you send "f" to switch PID tuning off.

Hope you understand.

Greetings,
Hein