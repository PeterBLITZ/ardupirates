#ifndef MOTORS_H
#define MOTORS_H

#define byte uint8_t

#define MOTORID1 0		
#define MOTORID2 1		
#define MOTORID3 2		
#define MOTORID4 3		
#define MOTORID5 4		
#define MOTORID6 5
#define MOTORID7 6
#define MOTORID8 7		
 

  // Scale motor commands to analogWrite                
  // m = (250-126)/(2000-1000) = 0.124          
  // b = y1 - (m * x1) = 126 - (0.124 * 1000) = 2               
  //float mMotorCommand = 0.124;                
  //float bMotorCommand = 2 ;
  float mMotorCommand = 0.255;          
  float bMotorCommand = -255 ;

int motorCommand[8] = {1000,1000,1000,1000,1000,1000,1000,1000};

int subtrim[4] = {1500,1500,1500,1500};    //SUBTRIM li esprimo in millisecondi come i servi per standardizzazione. 
int motorAxisCommand[3] = {0,0,0};
int motor = 0;
//float motorArmed=0;
// If AREF = 3.3V, then A/D is 931 at 3V and 465 = 1.5V 
// Scale gyro output (-465 to +465) to motor commands (1000 to 2000) 
// use y = mx + b 
float mMotorRate = 1.0753; // m = (y2 - y1) / (x2 - x1) = (2000 - 1000) / (465 - (-465)) 
float bMotorRate = 1500;   // b = y1 - m * x1

byte buff_i2c[6];

void configureMotors();
void commandMotors();
void commandAllMotors(int motorCommand);
void pulseMotors(byte quantity);


#endif

