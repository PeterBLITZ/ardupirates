////////////////////////////////////////////////////////////////////
// arduHeliSimpleConfig
// description: simple program to set-up configuration in order for
//              arduHeli to correctly decode the ccpm inputs from 
//              user's transmitter
/////////////////////////////////////////////////////////////////////


#include <avr/interrupt.h>
#include <Wire.h>
#include <EEPROM.h>
#include <AP_ADC.h>                                   // ArduPilot Mega Analog to Digital Converter Library
#include <APM_RC.h>                                    // ArduPilot Mega RC Library
#include <AP_Math.h>

////////////////////////////////////////////////////////////////////////////////
//  Setup Procedure
////////////////////////////////////////////////////////////////////////////////

#define SETUP_WAIT_TIME 5000

#define NUM_CHANNELS 8

// RC Channel definitions
#define FRONT_LEFT_CHANNEL 0
#define FRONT_RIGHT_CHANNEL 1
#define REAR_CHANNEL 2
#define YAW_CHANNEL 3
#define THROTTLE_CHANNEL 4

// EEPROM locations
#define EEPROM_BASE_ADDRESS 300
#define EEPROM_MAGIC_NUMBER_ADDR EEPROM_BASE_ADDRESS
#define FRONT_LEFT_CCPM_MIN_ADDR EEPROM_BASE_ADDRESS+4
#define FRONT_LEFT_CCPM_MAX_ADDR EEPROM_BASE_ADDRESS+8
#define FRONT_RIGHT_CCPM_MIN_ADDR EEPROM_BASE_ADDRESS+12
#define FRONT_RIGHT_CCPM_MAX_ADDR EEPROM_BASE_ADDRESS+16
#define REAR_CCPM_MIN_ADDR EEPROM_BASE_ADDRESS+20
#define REAR_CCPM_MAX_ADDR EEPROM_BASE_ADDRESS+24
#define YAW_MIN_ADDR EEPROM_BASE_ADDRESS+28
#define YAW_MAX_ADDR EEPROM_BASE_ADDRESS+32
#define THROTTLE_MIN_ADDR EEPROM_BASE_ADDRESS+36
#define THROTTLE_MAX_ADDR EEPROM_BASE_ADDRESS+40

#define EEPROM_MAGIC_NUMBER 12345.0

// CCPM Types
#define HELI_CCPM_120_TWO_FRONT_ONE_BACK 0
#define HELI_CCPM_120_ONE_FRONT_TWO_BACK 1

// define which CCPM we have
#define HELI_CCPM HELI_CCPM_120_TWO_FRONT_ONE_BACK

// define DeAllocation matrix(converts radio inputs to roll, pitch and collective
//   for example roll = (inputCh0*Row1Col1) + (inputCh1*Row1Col2) + (inputCh2*Row1Col3)
//               pitch = (inputCh0*Row2Col1) + (inputCh1*Row2Col2) + (inputCh2*Row2Col3)
//               collective = (inputCh0*Row3Col1) + (inputCh1*Row3Col2) + (inputCh2*Row3Col3)
// and Allocation matrix (converts roll, pitch, collective to servo outputs)
//   for example servo0 = (roll*Row1Col1) + (pitch*Row1Col2) + (collective*Row1Col3)
//               servo1 = (roll*Row2Col1) + (pitch*Row2Col2) + (collective*Row2Col3)
//               servo2 = (roll*Row3Col1) + (pitch*Row3Col2) + (collective*Row3Col3)
#if HELI_CCPM == HELI_CCPM_120_TWO_FRONT_ONE_BACK
  #define CCPM_DEALLOCATION   0.5774, -0.5774, 0.0000,  \
                              0.3333, 0.3333, -0.6667,  \
                              0.3333, 0.3333, 0.3333
  #define CCPM_ALLOCATION     0.8660,0.5000,  1.0000,   \
                             -0.8660, 0.5000, 1.0000,   \
                              0.0000, -1.0000, 1.0000
#endif

#if HELI_CCPM == HELI_CCPM_120_ONE_FRONT_TWO_BACK
  #define CCPM_DEALLOCATION   0.5774, -0.5774, 0.0000,  \
                              -0.3333,-0.3333, 0.6667,  \
                              0.3333, 0.3333,  0.3333
  #define CCPM_ALLOCATION     0.8660, -0.5000, 1.0000,  \
                             -0.8660, -0.5000, 1.0000,  \
                              0.0000,  1.0000, 1.0000
#endif

const Matrix3f ccpmDeallocation(CCPM_DEALLOCATION);
const Matrix3f ccpmAllocation(CCPM_ALLOCATION); 

// global variables
float frontLeftCCPMmin;
float frontLeftCCPMmax;
float frontRightCCPMmin;
float frontRightCCPMmax;
float rearCCPMmin;
float rearCCPMmax;
float yawMin;
float yawMax;

// array of latest RC values
float rc[NUM_CHANNELS];

void setup()
{
    Serial.begin(57600);
    delay(1000);
    
    APM_RC.Init();                                       // APM RC Input/Output Initialization  
    
    // RC channels Initialization (heli servos)  
    APM_RC.OutputCh(0,1500);  // mid position
    APM_RC.OutputCh(1,1500);
    APM_RC.OutputCh(2,1500);
    APM_RC.OutputCh(3,1500);
}

void displayHelp()
{
    // display greeting
    Serial.println("arduHeliSimpleConfig v1.0");
    Serial.println("  c) capture settings from transmitter");
    Serial.println("  d) display settings");
    Serial.println("  m) manually set settings");
    Serial.println("  r) read settings from EEPROM");
    Serial.println("  s) save settings to EEPROM");
    Serial.println("  t) test decoding ccpm");
    Serial.println("  v) test servo output");
    Serial.println("  h) display help");
    Serial.println("  x) exit\n");
}
////////////////////////////////////////////////////////////////////////////////
//  Main Processing Loop
////////////////////////////////////////////////////////////////////////////////

void loop()
{
    // local variables
    int val;
  
    // try to read values from eeprom
    Serial.println("reading initial values from EEPROM..");
    readEEPROM();
    Serial.println();
    
    // display greeting & help
    displayHelp();
  
    while(true)
    {        
        // check Serial
        if( Serial.available() > 0 ) 
        {
            val = Serial.read();
            switch (val) 
            {
                case 'c':
                    captureSettings();
                    //displayHelp();
                    break;
                case 'd':
                    displaySettings();
                    break;
                case 'm':
                    manualSettings();
                    break;
                case 'r':
                    readEEPROM();
                    displaySettings();
                    break;
                case 's':
                    writeEEPROM();
                    break;
                case 't':
                    runTest();
                    break;
                case 'v':
                    runServoTest();
                    break;                    
                case 'h':
                    displayHelp();
                    break;
                case 'x':
                    return;
                    break;
                default:
                    // do nothing
                    break;
            }
            // display help for next 
            displayHelp();
            
        }  // if Serial.available()   
    }  // while(true)
}

//
// displaySettings - reads values in from EEPROM
//
void displaySettings() 
{
    Serial.print("frontLeftCCPM min: ");
    Serial.print(frontLeftCCPMmin);
    Serial.print("\tmax:");
    Serial.print(frontLeftCCPMmax);
    
    if( abs(frontLeftCCPMmin-frontLeftCCPMmax)<50 || frontLeftCCPMmin < 900 || frontLeftCCPMmin > 2100 || frontLeftCCPMmax < 900 || frontLeftCCPMmax > 2100 )
    {
        Serial.println("\t\t<-- check");
    }else
        Serial.println();
    
    Serial.print("frontRightCCPM min: ");
    Serial.print(frontRightCCPMmin);
    Serial.print("\tmax:");
    Serial.print(frontRightCCPMmax);
    if( abs(frontRightCCPMmin-frontRightCCPMmax)<50 || frontRightCCPMmin < 900 || frontRightCCPMmin > 2100 || frontRightCCPMmax < 900 || frontRightCCPMmax > 2100 )
    {
        Serial.println("\t\t<-- check");
    }else
        Serial.println();    
    
    Serial.print("rearCCPM min: ");
    Serial.print(rearCCPMmin);
    Serial.print("\tmax:");
    Serial.print(rearCCPMmax);
    if( abs(rearCCPMmin-rearCCPMmax)<50 || rearCCPMmin < 900 || rearCCPMmin > 2100 || rearCCPMmax < 900 || rearCCPMmax > 2100 )
    {
        Serial.println("\t\t<-- check");
    }else
        Serial.println();
    
    Serial.print("yaw min: ");
    Serial.print(yawMin);
    Serial.print("\tmax:");
    Serial.print(yawMax);
    if( abs(yawMin-yawMax)<50 || yawMin < 900 || yawMin > 2100 || yawMax < 900 || yawMax > 2100 )
    {
        Serial.println("\t\t<-- check");
    }else
        Serial.println();  

    Serial.println();
}

//
// captureSettings - reads values in from EEPROM
//
void captureSettings() 
{
    long startTime = millis();
    int val;
    
    // check transmitter is on
    if( APM_RC.GetState() != 1 ) 
    {
        Serial.println("please turn on your transmitter...");
        
        // wait up to 10 seconds for transmitter to be turned on
        while( APM_RC.GetState() != 1  && (millis()-startTime < SETUP_WAIT_TIME*3));
        
        if( APM_RC.GetState() != 1 )
        {
            Serial.println("failed to read any values from transmitter, aborting!\n");
            return;
        }
    }
    
    // a slightly useless step of capturing mids (we never use them)
    Serial.println("please move sticks to approximate MIDDLE positions");
    delay(SETUP_WAIT_TIME);
    
    // display middle values
    captureRCValues();
    Serial.print("left:\t");
    Serial.println(rc[FRONT_LEFT_CHANNEL]);
    Serial.print("right:\t");
    Serial.println(rc[FRONT_RIGHT_CHANNEL]);
    Serial.print("rear:\t");
    Serial.println(rc[REAR_CHANNEL]);
    Serial.print("yaw:\t");
    Serial.println(rc[YAW_CHANNEL]);
    
    // move elevator (usually right stick) to bottom
    Serial.println("move COLLECTIVE PITCH to BOTTOM");
    delay(SETUP_WAIT_TIME);
    
    // capture and display min values
    captureRCValues();
    frontLeftCCPMmin = rc[FRONT_LEFT_CHANNEL];
    frontRightCCPMmin = rc[FRONT_RIGHT_CHANNEL];
    rearCCPMmin = rc[REAR_CHANNEL];
    Serial.print("left\tmin: ");
    Serial.println(frontLeftCCPMmin);
    Serial.print("right\tmin: ");
    Serial.println(frontRightCCPMmin);
    Serial.print("rear\tmin: ");
    Serial.println(rearCCPMmin);
    Serial.println();
    
    // move elevator (usually right stick) to top
    Serial.println("move COLLECTIVE PITCH to TOP");
    delay(SETUP_WAIT_TIME);
    
    // capture and display max values
    captureRCValues();
    frontLeftCCPMmax = rc[FRONT_LEFT_CHANNEL];
    frontRightCCPMmax = rc[FRONT_RIGHT_CHANNEL];
    rearCCPMmax = rc[REAR_CHANNEL];
    Serial.print("left\tmax: ");
    Serial.println(frontLeftCCPMmax);
    Serial.print("right\tmax: ");
    Serial.println(frontRightCCPMmax);
    Serial.print("rear\tmax: ");
    Serial.println(rearCCPMmax);
    Serial.println();
    
    // run test -- not yet implemented
    // press return key or space when done testing
    
    // move yaw (usually left stick) to far left
    Serial.println("move YAW (usually left stick) to far LEFT");
    delay(SETUP_WAIT_TIME);
    // capture and display max values
    captureRCValues();
    yawMin = rc[YAW_CHANNEL];
    Serial.print("yaw\tmin: ");
    Serial.println(yawMin);
    Serial.println();
    
    // move yaw (usually left stick) to far right
    Serial.println("move YAW (usually left stick) to far RIGHT");
    delay(SETUP_WAIT_TIME);
    // capture and display max values
    captureRCValues();
    yawMax = rc[YAW_CHANNEL];
    Serial.print("yaw\tmax: ");
    Serial.println(yawMax); 
    Serial.println(); 
  
    // display Settings for user to check
    displaySettings();  
    
    // first clear any input from the serial port
    Serial.flush();
    
    // values ok?  if yes store to EEPROM
    Serial.println("would you like to store settings to EEPROM? (Y/N)");
    while( !Serial.available() );
    
    val = Serial.read();
    if( val == 'y' || val == 'Y' )
    {
        writeEEPROM();  
    }else{
        Serial.println("Not storing to EEPROM but you can still do this from main menu");
    }

    Serial.println();
}

void displayManualSettingsHelp()
{
    Serial.println("Manual Override Menu.  Enter a letter followed by a number:");
    Serial.print("a) left min (");
    Serial.print(frontLeftCCPMmin);
    Serial.print(")\tb) left max (");
    Serial.print(frontLeftCCPMmax);
    Serial.println(")");
    
    Serial.print("c) right min (");
    Serial.print(frontRightCCPMmin);
    Serial.print(")\td) right max (");
    Serial.print(frontRightCCPMmax);
    Serial.println(")");
    
    Serial.print("e) back min (");
    Serial.print(rearCCPMmin);
    Serial.print(")\tf) back max (");
    Serial.print(rearCCPMmax);
    Serial.println(")");
    
    Serial.print("g) yaw min (");
    Serial.print(yawMin);
    Serial.print(")\th) yaw max (");
    Serial.print(yawMax);
    Serial.println(")");
    
    Serial.println("x) return to main menu");
    Serial.println();
}

//
// manualSettings - routine allows user to manually set values
//
void manualSettings() 
{
    float tempVal = 0.0;
    int val = 0;
    
    displayManualSettingsHelp();
    
    while( val != 'x' && val != 'X' )
    {
        // check Serial
        if( Serial.available() > 0 ) 
        {
            val = Serial.read();
            
            if( val >= 'a' && val <= 'i' )
            {
                tempVal = readFloatSerial();
                Serial.print("> ");
                Serial.println(tempVal);
                
                if( tempVal == 0.0 )
                {
                    Serial.println("failed to read value");
                }else{
                  
                    // update the appropriate setting
                    switch (val) 
                    {
                        case 'a':
                            frontLeftCCPMmin = tempVal;
                            break;
                        case 'b':
                            frontLeftCCPMmax = tempVal;
                            break;
                        case 'c':
                            frontRightCCPMmin = tempVal;
                            break;
                        case 'd':
                            frontRightCCPMmax = tempVal;
                            break;
                        case 'e':
                            rearCCPMmin = tempVal;
                            break;
                        case 'f':
                            rearCCPMmax = tempVal;
                            break;
                        case 'g':
                            yawMin = tempVal;
                            break;
                        case 'h':
                            yawMax = tempVal;
                            break;
                        default:
                            // do nothing
                            break;
                    }
                }
                // display help
                displayManualSettingsHelp();
            }
        }else{
            delay(10);
        }
    }
}

// captureRCValues - captures the average RC values over a very short period of time
void captureRCValues()
{
    int i,j;
    float numIterations = 0.0;
    
    for(i=0; i<NUM_CHANNELS;i++)
    {
        rc[i] = 0.0;
    }
    
    for(j=0; j<10; j++)
    {
        for(i=0; i<NUM_CHANNELS; i++)
        {
            rc[i] += APM_RC.InputCh(i);
        }
        numIterations++;
    }
    
    for(i=0; i<NUM_CHANNELS;i++)
    {
        rc[i] /= numIterations;
    }
}

//
// runTest - displays roll, pitch, collective, yaw values to terminal
//
void runTest() {
   
    float frontLeftCCPMslope;
    float frontLeftCCPMintercept;
    float frontRightCCPMslope;
    float frontRightCCPMintercept;
    float rearCCPMslope;
    float rearCCPMintercept;
    float yawSlope;
    float yawIntercept;
    float yawPercent;
    float roll, pitch, collective, yaw;
    Vector3f ccpmPercents,rollPitchCollPercent; 
    int val;  
  
    // update CCPM values
    frontLeftCCPMslope =      100 / (frontLeftCCPMmax - frontLeftCCPMmin);
    frontLeftCCPMintercept =  100 - (frontLeftCCPMslope * frontLeftCCPMmax);
    frontRightCCPMslope =     100 / (frontRightCCPMmax - frontRightCCPMmin);
    frontRightCCPMintercept = 100 - (frontRightCCPMslope * frontRightCCPMmax);
    rearCCPMslope =           100 / (rearCCPMmax - rearCCPMmin);
    rearCCPMintercept =       100 - (rearCCPMslope * rearCCPMmax);
    yawSlope =                100 / (yawMax - yawMin);
    yawIntercept =            50 - (yawSlope * yawMax);
  
    // clear Serial
    Serial.flush();
  
    // display instructions
    Serial.println();
    Serial.println("The values that will be displayed below will be the 4 raw channels from receiver");
    Serial.println("followed by decoded values for the roll, pitch, collective and rudder");
    Serial.println("   channel 0: front left servo");
    Serial.println("   channel 1: front right servo");
    Serial.println("   channel 2: back servo");
    Serial.println("   channel 3: rudder");
    Serial.println("   roll:      -50 (left)    ~   50 (right)");
    Serial.println("   pitch:     -50 (forward) ~   50 (back)");
    Serial.println("   collective:  0 (bottom)  ~  100 (top)");
    Serial.println("   rudder:    -50 (left)    ~   50 (right)");
    Serial.println();
    Serial.println("press any key to start and stop the test");
  
    // wait until user presses a key
    while( Serial.available() == 0 );
    
    // clear the Serial port
    Serial.flush();
  
    // run the test until the user presses a keys
    while( Serial.available() <= 0 ) 
    {
        // left channel
        ccpmPercents.x  = frontLeftCCPMslope * APM_RC.InputCh(FRONT_LEFT_CHANNEL) + frontLeftCCPMintercept;

        // right channel
        ccpmPercents.y = frontRightCCPMslope * APM_RC.InputCh(FRONT_RIGHT_CHANNEL) + frontRightCCPMintercept;
        
        // rear channel
        ccpmPercents.z = rearCCPMslope * APM_RC.InputCh(REAR_CHANNEL) + rearCCPMintercept;
        
        rollPitchCollPercent = ccpmDeallocation * ccpmPercents;
        
        yaw = yawPercent      =      yawSlope * APM_RC.InputCh(YAW_CHANNEL) + yawIntercept;
        
        // to make it fit in with rest of arduCopter code..
        // these might not be the right sizes (i..e -100~100 vs 1000~2000 for regular arduCopter)
        roll = rollPitchCollPercent.x;
        pitch = rollPitchCollPercent.y;
        collective = rollPitchCollPercent.z;
    
        Serial.print("0: ");
        Serial.print(APM_RC.InputCh(FRONT_LEFT_CHANNEL));
        Serial.print("\t1: ");
        Serial.print(APM_RC.InputCh(FRONT_RIGHT_CHANNEL));
        Serial.print("\t2: ");
        Serial.print(APM_RC.InputCh(REAR_CHANNEL));
        Serial.print("\t3: ");
        Serial.print(APM_RC.InputCh(YAW_CHANNEL));
        Serial.print("\tr: ");
        Serial.print(roll,0);
        Serial.print("\tp: ");
        Serial.print(pitch,0);
        Serial.print("\tc: ");
        Serial.print(collective,0);
        Serial.print("\ty: ");
        Serial.print(yaw,0);
        Serial.println();
        delay(100);
    }
    
    // a value must have been received on the serial port so get rid of it.
    Serial.flush();
    Serial.println();
  
}

//
// runServoTest - waggles servos to show user which servos are attached to which channels
//
void runServoTest() {
  
    int val = 0;  // value captured from user
    int servoNum = -1;
    int servoPos = 1400;
    int dir = 1;
    int done = 0;
  
    // clear Serial
    Serial.flush();
  
    // display instructions
    Serial.println();
    Serial.println("Check the correct servo moves in line with the channel selected");
    Serial.println("   channel 0: front left servo");
    Serial.println("   channel 1: front right servo");
    Serial.println("   channel 2: back servo");
    Serial.println("   channel 3: rudder");
    Serial.println();
    Serial.println("Enter a number from 0 ~ 3 to move that servo.");
    Serial.println("Enter 'z' to stop moving servos.");
    Serial.println("Any other key to return to main menu.");
  
    // loop until user presses a key other than 0~3
    do {
        // wait until user presses a key
        if( Serial.available() > 0 )
        {
            val = Serial.read();
            if( val >= '0' && val <= '3' ) { // specify servo to move
            
                // if a servo is already moving, move it back to mid
                if( servoNum != -1 )
                    APM_RC.OutputCh(servoNum,1500);
                
                // figure out which servo to move next
                servoNum = val - '0';
                Serial.print("moving servo ");
                Serial.println(servoNum);
                
            }else if( val == 'z' || val == 'Z' ) { // stop moving servos
                APM_RC.OutputCh(servoNum,1500);
                servoNum = -1;
            }else
                done = 1;  // exit test
        }
        
        // move the servo
        if( servoNum >= 0 || servoNum <= 3 )
        {
            servoPos += dir;
            if( servoPos <= 1400 || servoPos >= 1600 )
                dir = -dir;
                
            APM_RC.OutputCh(servoNum,servoPos);
        }
        
        // short delay
        delay(10);
        
    } while( done == 0 );
    
    // clear the Serial port
    Serial.flush();
 
    // extra line to pretty up output
    Serial.println();
}

//
// readEEPROM - reads values in from EEPROM
//
void readEEPROM() 
{
    float magicNum = 0;
    magicNum = readFloat(EEPROM_MAGIC_NUMBER_ADDR);
    if( magicNum != EEPROM_MAGIC_NUMBER )
    {
        Serial.println("No settings found in EEPROM");
    }else{
        frontLeftCCPMmin = readFloat(FRONT_LEFT_CCPM_MIN_ADDR);
        frontLeftCCPMmax = readFloat(FRONT_LEFT_CCPM_MAX_ADDR);
        frontRightCCPMmin = readFloat(FRONT_RIGHT_CCPM_MIN_ADDR);
        frontRightCCPMmax = readFloat(FRONT_RIGHT_CCPM_MAX_ADDR);
        rearCCPMmin = readFloat(REAR_CCPM_MIN_ADDR);
        rearCCPMmax = readFloat(REAR_CCPM_MAX_ADDR);
        yawMin = readFloat(YAW_MIN_ADDR);
        yawMax = readFloat(YAW_MAX_ADDR);
        Serial.println("successfully read settings from EEPROM\n");
    }
}

//
// writeEEPROM - reads values in from EEPROM
//
void writeEEPROM() 
{
    writeFloat(EEPROM_MAGIC_NUMBER,EEPROM_MAGIC_NUMBER_ADDR);
    writeFloat(frontLeftCCPMmin,FRONT_LEFT_CCPM_MIN_ADDR);
    writeFloat(frontLeftCCPMmax,FRONT_LEFT_CCPM_MAX_ADDR);
    
    writeFloat(frontRightCCPMmin,FRONT_RIGHT_CCPM_MIN_ADDR);
    writeFloat(frontRightCCPMmax,FRONT_RIGHT_CCPM_MAX_ADDR);
    
    writeFloat(rearCCPMmin,REAR_CCPM_MIN_ADDR);
    writeFloat(rearCCPMmax,REAR_CCPM_MAX_ADDR);
    
    writeFloat(yawMin,YAW_MIN_ADDR);
    writeFloat(yawMax,YAW_MAX_ADDR);
    
    // check settings were written
    Serial.println("settings written to EEPROM\n");
}

// Utilities for writing and reading from the EEPROM
float readFloat(int address) {
  union floatStore {
    byte floatByte[4];
    float floatVal;
  } floatOut;
  
  for (int i = 0; i < 4; i++) 
  {
    floatOut.floatByte[i] = EEPROM.read(address + i);
  }
  return floatOut.floatVal;
}

//
// writeFloat -- writes a float to EEPROM
//
void writeFloat(float value, int address) 
{
  union floatStore {
    byte floatByte[4];
    float floatVal;
  } floatIn;
  
  floatIn.floatVal = value;
  for (int i = 0; i < 4; i++) 
    EEPROM.write(address + i, floatIn.floatByte[i]);
}

// Used to read floating point values from the serial port
// taken from AeroQuad code
float readFloatSerial() {
  byte index = 0;
  byte timeout = 0;
  char data[128] = "";

  do {
    if (Serial.available() == 0) {
      delay(10);
      timeout++;
    }
    else {
      data[index] = Serial.read();
      timeout = 0;
      index++;
    }
  }  while ((data[constrain(index-1, 0, 128)] != ';') && (timeout < 5) && (index < 128));
  return atof(data);
}

