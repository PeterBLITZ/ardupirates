// Init Variable
// #include "conf.h"
#include <stdlib.h>


float MotorPitch[MAX_MOTORS];
float MotorRoll[MAX_MOTORS];
float MotorYaw[MAX_MOTORS];
float MotorGas[MAX_MOTORS];
float motorAxisCommandPitch[MAX_MOTORS];
float motorAxisCommandRoll[MAX_MOTORS];
float motorAxisCommandYaw[MAX_MOTORS];
//float motorAxisCommand[3]; // Command on Roll YAW PITCH Recived from IMU


void init_mixer_table()
{
// Octo configuration. Motors are numberd CW viewed from above starting at front = 1 (CW prop rotation)
// Motor rotation is CCW for odd numbered motors
MotorGas[0] = 100;
MotorPitch[0] = 100;  
MotorRoll[0] =  0;  
MotorYaw[0] =  -100;  

MotorGas[1] = 100;
MotorPitch[1] = 100;  
MotorRoll[1] =  -100;  
MotorYaw[1] =  100;  

MotorGas[2] = 100;
MotorPitch[2] = 0  ; 
MotorRoll[2] =  -100;  
MotorYaw[2] =  -100;  

MotorGas[3] = 100;
MotorPitch[3] =  -100; 
MotorRoll[3] =  -100; 
MotorYaw[3] =  100;  

MotorGas[4] = 100;
MotorPitch[4] =  -100; 
MotorRoll[4] =  0; 
MotorYaw[4] =  -100;  

MotorGas[5] = 100;
MotorPitch[5] =  -100; 
MotorRoll[5] =  100; 
MotorYaw[5] =  100;  

MotorGas[6] = 100;
MotorPitch[6] =  0; 
MotorRoll[6] =  100; 
MotorYaw[6] =  -100; 

MotorGas[7] = 100;
MotorPitch[7] =  100; 
MotorRoll[7] =  100; 
MotorYaw[7] =  100;  
}




void motor_axis_correction()
{
int i;
for (i=0;i<MAX_MOTORS;i++)
  {
  motorAxisCommandPitch[i] = (control_pitch / 100.0) * MotorPitch[i];
  motorAxisCommandRoll[i] = (control_roll / 100.0) * MotorRoll[i];
  motorAxisCommandYaw[i] = (control_yaw / 100.0) * MotorYaw[i];
  }
}


//After that we can mix them together:
void motor_matrix_command()
{
int i;
float valuemotor;
for (i=0;i<MAX_MOTORS;i++)
  {
   valuemotor = ((ch_throttle* MotorGas[i])/100) + motorAxisCommandPitch[i] + motorAxisCommandYaw[i] + motorAxisCommandRoll[i];
   //valuemotor = Throttle + motorAxisCommandPitch[i] + motorAxisCommandYaw[i] + motorAxisCommandRoll[i]; // OLD VERSION WITHOUT GAS CONTROL ON Mixertable
   valuemotor = constrain(valuemotor, 1000, 2000);
   motorCommand[i]=valuemotor;
  }
}


//void matrix_debug()
//{
//#ifdef PRINT_MIXERTABLE 
//     Serial.println();
//     Serial.println("--------------------------");
//     Serial.println("        Motori Mixertable " );
//     Serial.println("--------------------------");
//       Serial.println();
//     Serial.println("--------------------------");
//     Serial.println("   Quad  Motor Debug     " );
//     Serial.println("--------------------------");

//     Serial.print("AIL:");
//     Serial.print(ch_roll);
//     Serial.print(" ELE:");
//     Serial.print(ch_pitch);
//     Serial.print(" THR:");
//    Serial.print( ch_throttle);
//     Serial.print(" YAW:");
//     Serial.print( ch_yaw);
//     Serial.print(" AUX:");
//     Serial.print(ch_aux);
//     Serial.print(" AUX2:");
//     Serial.print(ch_aux2);
//     Serial.println();
//    Serial.print("CONTROL_ROLL:");
//     Serial.print(control_roll);
//     Serial.print(" CONTROL_PITCH:");
//     Serial.print(control_pitch);
//     Serial.print(" CONTROL_YAW:");
//     Serial.print(control_yaw);
//     Serial.print(" SONAR_VALUE:");
//     Serial.print(sonar_value);
//     Serial.print(" TARGET_SONAR_VALUE:");
//    Serial.print(target_sonar_altitude);
//     Serial.print(" ERR_SONAR_VALUE:");
//     Serial.print(err_altitude);
//     Serial.println();
//     Serial.print("latitude:");
//     Serial.print(GPS_np.Lattitude);
//    Serial.print(" longitude:");
//     Serial.print(GPS_np.Longitude);
//     Serial.print(" command gps roll:");
//     Serial.print(command_gps_roll);
//     Serial.print(" command gps pitch:");
//     Serial.print(command_gps_pitch);
//     Serial.print(" Lon_diff:");
//    Serial.print(Lon_diff);
//     Serial.print(" Lon_diff");
//     Serial.print(command_gps_pitch);
 
//     Serial.println();
     
//     Serial.print("AP MODE:");Serial.print((int)AP_mode);
//     Serial.println();
     
// #ifdef HEXARADIAL
//     Serial.println();
//     Serial.print((unsigned int)MotorI2C[5]);
//     comma();
//     Serial.print((unsigned int)MotorI2C[0]);
//     comma();
//     Serial.print((unsigned int)MotorI2C[1]);
//     comma();
//     Serial.println();
//     Serial.print((unsigned int)MotorI2C[4]);
//     comma();
//     Serial.print((unsigned int)MotorI2C[3]);
//     comma();
//     Serial.println((unsigned int)MotorI2C[2]);
//     Serial.println("---------------");
//     Serial.println();
// #endif



// #endif//
//}



// Works faster and is smaller than the constrain() function
//int limitRange(int data, int minLimit, int maxLimit) {
//  if (data < minLimit) return minLimit;
//  else if (data > maxLimit) return maxLimit;
//  else return data;
// }

