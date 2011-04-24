These libraries are for migrating from ArduPilot + OilPan hardware to the conventional Ar(See)duino + I2C sensors.
Features:

AP_ADC:
-ITG3200 gyro, BMA180 accelerometer are supported instead of ADC-tied analog sensors
-Automatic Gyro zero calibration during init
-Tuned gyro sensitivity

DataFlash:
-DataFlash library was ripped (empty functions) for those who has no DataFlash

AP_Compass:
-Tuned the compass gain to prevent overloading the HMC5883 unit

===
Copy these libraries to the arduino IDE 'libraries' folder replacing the old ones.
===
Syberian
