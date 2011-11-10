/*
	APM_DCM.cpp
		Code by Robert, Seb Madgwick
		Based on the diydrone team.

	This library works with the ArduPilot Mega and "Oilpan"
*/
#include <AP_DCM.h>

#define ROLL 0
#define PITCH 1
#define YAW 2
#define THROTTLE 3
#define MODE 4
#define AUX 5
#define LASTCHANNEL 6

#define ZAXIS 2

#define OUTPUTMODE 1				// This is just used for debugging, remove later
#define ToRad(x) (x*0.01745329252)	// *pi/180
#define ToDeg(x) (x*57.2957795131)	// *180/pi

//====================================================================================================
// Function
//====================================================================================================
#define gyroMeasError 3.14159265358979f * (5.0f / 180.0f) // gyroscope measurement error in rad/s (shown as 5 deg/s)
//#define beta sqrt(3.0f / 4.0f) * gyroMeasError // compute beta
#define betaDef		0.08f		// 2 * proportional gain - was 0.1f (a tad to high?)

//---------------------------------------------------------------------------------------------------
// Variable definitions

volatile float beta = betaDef;								// 2 * proportional gain (Kp)



float AP_DCM::arctan2(float y, float x) {
  // Taken from: http://www.dspguru.com/comp.dsp/tricks/alg/fxdatan2.htm
   float coeff_1 = PI/4;
   float coeff_2 = 3*coeff_1;
   float abs_y = abs(y)+1e-10;      // kludge to prevent 0/0 condition
   float r, angle;
   
   if (x >= 0) {
     r = (x - abs_y) / (x + abs_y);
     angle = coeff_1 - coeff_1 * r;
   }
   else {
     r = (x + abs_y) / (abs_y - x);
     angle = coeff_2 - coeff_1 * r;
   }
   if (y < 0)
     return(-angle);     // negate if in quad III or IV
   else
     return(angle);
}

void
AP_DCM::update_DCM(void)
{
	static float delta_t;

	delta_t = _imu->update();
	_gyro_vector 	= _imu->get_gyro();		// Get current values for IMU sensors
	_accel_vector 	= _imu->get_accel();	// Get current values for IMU sensors

	MadgwickAHRSupdateIMU(delta_t, _gyro_vector.x, _gyro_vector.y, _gyro_vector.z, _accel_vector.x, _accel_vector.y, _accel_vector.z);
	euler_angles();			// Calculate pitch, roll, yaw for stabilization and navigation
}

Vector3f
AP_DCM::get_gyro(void)
{
	if (!filter_result)
		return _gyro_vector; // We return the raw gyro vector corrected for bias
	else
		return _omega_integ_corr;		// Used for _centripetal correction (theoretically better than _omega)
}		
//---------------------------------------------------------------------------------------------------
// Fast inverse square-root
// See: http://en.wikipedia.org/wiki/Fast_inverse_square_root

float AP_DCM::invSqrt(float x) 
{
	float halfx = 0.5f * x;
	float y = x;
	long i = *(long*)&y;
	i = 0x5f3759df - (i>>1);
	y = *(float*)&i;
	y = y * (1.5f - (halfx * y * y));
	return y;
}

//---------------------------------------------------------------------------------------------------
// IMU algorithm update

void 
AP_DCM::MadgwickAHRSupdateIMU(float i_dt, float gx, float gy, float gz, float ax, float ay, float az) 
{
	float recipNorm;
	float s0, s1, s2, s3;
	float qDot1, qDot2, qDot3, qDot4;
	float _2q0, _2q1, _2q2, _2q3, _4q0, _4q1, _4q2 ,_8q1, _8q2, q0q0, q1q1, q2q2, q3q3;

	// Rate of change of quaternion from gyroscope
	qDot1 = 0.5f * (-q1 * gx - q2 * gy - q3 * gz);
	qDot2 = 0.5f * (q0 * gx + q2 * gz - q3 * gy);
	qDot3 = 0.5f * (q0 * gy - q1 * gz + q3 * gx);
	qDot4 = 0.5f * (q0 * gz + q1 * gy - q2 * gx);

	// Compute feedback only if accelerometer measurement valid (avoids NaN in accelerometer normalisation)
	if(!((ax == 0.0f) && (ay == 0.0f) && (az == 0.0f))) {

		// Normalise accelerometer measurement
		recipNorm = invSqrt(ax * ax + ay * ay + az * az);
		ax *= recipNorm;
		ay *= recipNorm;
		az *= recipNorm;   

		// Auxiliary variables to avoid repeated arithmetic
		_2q0 = 2.0f * q0;
		_2q1 = 2.0f * q1;
		_2q2 = 2.0f * q2;
		_2q3 = 2.0f * q3;
		_4q0 = 4.0f * q0;
		_4q1 = 4.0f * q1;
		_4q2 = 4.0f * q2;
		_8q1 = 8.0f * q1;
		_8q2 = 8.0f * q2;
		q0q0 = q0 * q0;
		q1q1 = q1 * q1;
		q2q2 = q2 * q2;
		q3q3 = q3 * q3;

		// Gradient decent algorithm corrective step
		s0 = _4q0 * q2q2 + _2q2 * ax + _4q0 * q1q1 - _2q1 * ay;
		s1 = _4q1 * q3q3 - _2q3 * ax + 4.0f * q0q0 * q1 - _2q0 * ay - _4q1 + _8q1 * q1q1 + _8q1 * q2q2 + _4q1 * az;
		s2 = 4.0f * q0q0 * q2 + _2q0 * ax + _4q2 * q3q3 - _2q3 * ay - _4q2 + _8q2 * q1q1 + _8q2 * q2q2 + _4q2 * az;
		s3 = 4.0f * q1q1 * q3 - _2q1 * ax + 4.0f * q2q2 * q3 - _2q2 * ay;
		recipNorm = invSqrt(s0 * s0 + s1 * s1 + s2 * s2 + s3 * s3); // normalise step magnitude
		s0 *= recipNorm;
		s1 *= recipNorm;
		s2 *= recipNorm;
		s3 *= recipNorm;

		// Apply feedback step
		qDot1 -= beta * s0;
		qDot2 -= beta * s1;
		qDot3 -= beta * s2;
		qDot4 -= beta * s3;
	}

	// Integrate rate of change of quaternion to yield quaternion
	q0 += qDot1 * i_dt;
	q1 += qDot2 * i_dt;
	q2 += qDot3 * i_dt;
	q3 += qDot4 * i_dt;

	// Normalise quaternion
	recipNorm = invSqrt(q0 * q0 + q1 * q1 + q2 * q2 + q3 * q3);
	q0 *= recipNorm;
	q1 *= recipNorm;
	q2 *= recipNorm;
	q3 *= recipNorm;
}

void
AP_DCM::set_compass(Compass *compass)
{
	_compass = compass;
}

/**************************************************/
void
AP_DCM::euler_angles(void)
{
    roll  = atan2(2 * (q0*q1 + q2*q3), 1 - 2 *(q1*q1 + q2*q2));
    pitch = asin(2 * (q0*q2 - q1*q3));
    yaw   = atan2(2 * (q0*q3 + q1*q2), 1 - 2 *(q2*q2 + q3*q3));

	roll_sensor 	= degrees(roll)  * 100;
	pitch_sensor 	= degrees(pitch) * 100;
	yaw_sensor 		= degrees(yaw)   * 100;

	if (yaw_sensor < 0)
		yaw_sensor += 36000;
}


/**************************************************/

float
AP_DCM::get_health(void)
{
	return _health;
}


