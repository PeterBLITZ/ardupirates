Version 0.2 (alpha)

This is a branch of the ArduCopter 1.6 SuperStable ported to Arduino Mega board with 1280 or 2560 chip.

To compile it properly, backup and REMOVE ALL the folders in the arduino 'libraries' folder,
then copy ones from the 'libraries' project folder.
DO NOT use any 3rd party configurator tools other than supplied ss_cfg.exe. It can lead to the unpredictable results!
==============
Features:

=== Using ITG3200, BMA180, BMP085, HMC5883(5843) I2C sensors instead of analog ones
=== RC input is taken from independent channels:
 
THROTTLE	PIN A8
ROLL		PIN A9
PITCH		PIN A10
YAW		PIN A11
MODE		PIN A12
AUX2		PIN A13
CH7		PIN A14
CH8		PIN A15

(8th channel has no issues and can be used freely)

===
Flight mode selection:

MODE	AUX2	Flight mode

off	off	Acrobatic (gyro only)
on	off	level + heading hold
off	on	level + altitude + heading hold
on	on	level + altitude + heading + position hold
============================================================ 

=== PPM outputs are increased to 16 channels. They has the following layout:

Out	PIN
0	D2
1	D3
2	D4
3	D5
4	D6
5	D7
6	D8
7	D9
8	D22
9	D23
10	D24
11	D25
12	D26
13	D27
14	D28
15	D29


===Integrated impulse Sonar on the 'ADC 7'
 - Usage: %variable%=APM_ADC.Ch(7);
 - when out of range, returns -1 (65535);
 - does not slow the program `cause it works simultaneously (using TIMER5 interrupts)
 - To use range values, uncomment //define IsSonar in the main program
 - 5cm..40m range
 - 0.14cm resolution (output data are rounded to nearest centimeter)
	D48 (PORTL.1) = input from sonar
	D47 (PORTL.2) = sonar Tx (trigger)
 The smaller altitude - lower the cycle time 
 (max range 40m and 150ms cycle, both are decreased proportionally)

// changed Sonar Input from D49 to D48

===============================================
===============================================
===============================================
===============================================
APM motor remapped to the MultiWii-style
The following frame types are supported:

	Tri, +4, x4, Y4, +6, x6, Y6

See the ss_cfg.exe 'Flight Data' tab for the motor pins layout.


==============================	
*/


=============================
===APM_BMP085 has been rearranged:
-Added 3 new functions:
		long GetAltitude(); // Relative altitude in 0.01 meters (1cm)
		long GetASL(); // altitude AboveSeaLevel in 0.01 meters (1cm)
		long Calibrate() // altitude calibration (set the current Altitude to zero)
-Advanced filtering of barometer signal
-'COMPLETE' input is no longer needed
-very accurate readings (+-0.2m in the calm air)



=================================================
NOTES and roadmap:

LEDs was not tested
Configurator program has been rewritten in Delphi to suite my needs
