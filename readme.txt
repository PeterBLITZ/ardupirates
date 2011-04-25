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
	D49 (PORTL.0) = input from sonar
	D47 (PORTL.2) = sonar Tx (trigger)
 The smaller altitude - lower the cycle time 
 (max range 40m and 150ms cycle, both are decreased proportionally)





=================================================
NOTES and roadmap:

LEDs was not tested
Configurator program is fully working

