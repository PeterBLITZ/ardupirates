#include "Motors.h"
// #include "conf.h"

#define DEBUG

#define MAX_MOTORS 8
#define MINCOMMAND 30   // Comando minimo dei motori.

#define MOTOR1  0
#define MOTOR2  1
#define MOTOR3  2
#define MOTOR4  3
#define MOTOR5  4
#define MOTOR6	5
#define MOTOR7  6
#define MOTOR8	7

volatile unsigned char twi_state = 0;
unsigned char MotorI2C[MAX_MOTORS];
unsigned char i2cmotor=0,PlatinenVersion=0;
unsigned char motors=0;
unsigned char motorread = 0,MissingMotor = 0;
unsigned char motor_rx[16],motor_rx2[16];
unsigned char MotorPresent[MAX_MOTORS];
unsigned char MotorError[MAX_MOTORS];

byte x = 0;
int mnumber = 6;
byte command=0;

int index=0;
int i=0;
char escspeed=100;
int nmotor=0;

void configureMotors() {
  Wire.begin(0x29);
  //Wire.onReceive(receiveEvent);
}

void commandMotors() {
    
    MotorI2C[MOTORID1]=(motorCommand[0] * mMotorCommand) + bMotorCommand; 
    MotorI2C[MOTORID2]=(motorCommand[1] * mMotorCommand) + bMotorCommand;
    MotorI2C[MOTORID3]=(motorCommand[2] * mMotorCommand) + bMotorCommand;
    MotorI2C[MOTORID4]=(motorCommand[3] * mMotorCommand) + bMotorCommand;
    MotorI2C[MOTORID5]=(motorCommand[4] * mMotorCommand) + bMotorCommand;
    MotorI2C[MOTORID6]=(motorCommand[5] * mMotorCommand) + bMotorCommand; 
    MotorI2C[MOTORID7]=(motorCommand[6] * mMotorCommand) + bMotorCommand;
    MotorI2C[MOTORID8]=(motorCommand[7] * mMotorCommand) + bMotorCommand;
    WireMotorWrite();
  
}

// Sends commands to all motors
void commandAllMotors(int motorCommand) {
  
    MotorI2C[MOTORID1]=(motorCommand * mMotorCommand) + bMotorCommand;
    MotorI2C[MOTORID2]=(motorCommand * mMotorCommand) + bMotorCommand;
    MotorI2C[MOTORID3]=(motorCommand * mMotorCommand) + bMotorCommand;
    MotorI2C[MOTORID4]=(motorCommand * mMotorCommand) + bMotorCommand;
    MotorI2C[MOTORID5]=(motorCommand * mMotorCommand) + bMotorCommand;
    MotorI2C[MOTORID6]=(motorCommand * mMotorCommand) + bMotorCommand;
  
    WireMotorWrite();

}

void pulseMotors(byte quantity) {
  for (byte i = 0; i < quantity; i++) {       
    commandAllMotors(MINCOMMAND + 100);
    delay(2000);
  }
}


void WireMotorWrite()
{
int i = 0;
int tout=0;

Wire.endTransmission(); //end transmission
for(nmotor=0;nmotor<8;nmotor++)
  {
  index=0x29+nmotor;
  Wire.beginTransmission(index);
  Wire.send(MotorI2C[nmotor]);
  Wire.endTransmission(); //end transmission
  /*Wire.requestFrom(index, 1); // request 6 bytes from device
  i=0;
  //while(1)
  while((Wire.available())&&(i<6))
  {
    //if (Wire.available()==0)break;
    buff_i2c[i] = Wire.receive(); // receive one byte
    i++;
    //if (i>8)break;
    //Serial.print(i);
    
  }
*/
  }

}


