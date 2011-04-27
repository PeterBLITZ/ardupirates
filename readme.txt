Version 0.1 (pre-alpha)

This is a port of the ArduCopter 1.6 SuperStable to *duino Mega board with 1280 or 2560 chip.

Replace the standard SuperStable libraries with ones in the 'libraries' folder.
==============
Features:
=== all the features of the SuperStable 1.6 build
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

=== PPM outputs are increased to 16 channels. Their layout slightly different:

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


===Integrated analog Sonar on the 'ADC 7'
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
See the Wii_motormap.jpg in this folder

==============================	
Tricopter
======
	Type			APM		MultiWii	D pin

	Right			0		3			5
	Left			1		4			6
	Servo			2		0			2
	Back			3		1			3

==============================	
Quad +
======
	Type			APM		MultiWii	D pin

	Right			0		3			5
	Left			1		4			6
	Front			2		0			2
	Back			3		1			3

==============================	
Quad X
======
	Type			APM		MultiWii	D pin

	FrontRCCW		0		3			5
	BackLCCW		1		4			6
	FrontLCW		2		0			2
	BackRCW			3		1			3

==============================	
Hexa Diamond
======

//Hexa Diamond Mode - 6 Motor system in diamond shape

//      L  CCW 0.Front.0 CW  R           // 0 = Motor
//         ......***......               // *** = APM 
//   L  CW 0.....***......0 CCW  R       // ***
//         ......***......               // *** 
//     B  CCW  0.Back..0  CW  B          L = Left motors, R = Right motors, B = Back motors.



	Type			APM		MultiWii	D pin

	LeftCW			0		4			6
	LeftCCW			1		6			8
	RightCW			2		3			5
	RightCCW		3		5			7
	BackCW			6		0			2
	BackCCW			7		1			3


==============================	
Y6
======
	Type			APM		MultiWii	D pin

	UpLeftCW		0		4			6
	DnLeftCCW		1		6			8
	UpRightCW		2		3			5
	DnRightCCW		3		5			7
	DnBackCW		6		0			2
	UpBackCCW		7		1			3





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
Configurator program is fully working

