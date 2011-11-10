#ifndef AP_DCM_h
#define AP_DCM_h

// temporarily include all other classes here
// since this naming is a bit off from the
// convention and the AP_DCM should be the top
// header file
#include "AP_DCM_HIL.h"

#include "../FastSerial/FastSerial.h"
#include "../AP_Math/AP_Math.h"
#include <inttypes.h>
#include "WProgram.h"
#include "../AP_Compass/AP_Compass.h"
#include "../AP_ADC/AP_ADC.h"
#include "../AP_GPS/AP_GPS.h"
#include "../AP_IMU/AP_IMU.h"


#define twoKpDef	(2.0f * 0.5f)	// 2 * proportional gain
#define twoKiDef	(2.0f * 0.0f)	// 2 * integral gain

class AP_DCM
{
public:
	// Constructors
	AP_DCM(IMU *imu, GPS *&gps, Compass *withCompass = NULL) :
		_compass(withCompass),
		_gps(gps),
		_imu(imu)
	{
		filter_result = 0;
		twoKp = twoKpDef;											// 2 * proportional gain (Kp)
		twoKi = twoKiDef;											// 2 * integral gain (Ki)
		q0 = 1.0f, q1 = 0.0f, q2 = 0.0f, q3 = 0.0f;					// quaternion of sensor frame relative to auxiliary frame
		integralFBx = 0.0f,  integralFBy = 0.0f, integralFBz = 0.0f;	// integral error terms scaled by Ki
	}

	// Accessors
	Vector3f	get_gyro(void);
	Vector3f	get_accel(void) { return _accel_vector; }
	//Matrix3f	get_dcm_matrix(void) {return _dcm_matrix; }

	void		set_compass(Compass *compass);

	// Methods
	void 		update_DCM(void);
	void		MadgwickAHRSupdateIMU(float i_dt, float gx, float gy, float gz, float ax, float ay, float az);
	void		MahonyAHRSupdateIMU(float i_dt, float gx, float gy, float gz, float ax, float ay, float az);
	float		invSqrt(float x);

	float		get_health(void);

	long		roll_sensor;					// Degrees * 100
	long		pitch_sensor;					// Degrees * 100
	long		yaw_sensor;						// Degrees * 100

	float		roll;							// Radians
	float		pitch;							// Radians
	float		yaw;							// Radians

	byte		filter_result;

	uint8_t 	gyro_sat_count;
	uint8_t 	renorm_sqrt_count;
	uint8_t 	renorm_blowup_count;

	float	kp_roll_pitch() 		{ return _kp_roll_pitch; }
	void	kp_roll_pitch(float v) 	{ _kp_roll_pitch = v; }

	float	ki_roll_pitch() 		{ return _ki_roll_pitch; }
	void	ki_roll_pitch(float v) 	{ _ki_roll_pitch = v; }

	float	kp_yaw() 				{ return _kp_yaw; }
	void	kp_yaw(float v) 		{ _kp_yaw = v; }

	float	ki_yaw() 				{ return _ki_yaw; }
	void	ki_yaw(float v) 		{ _ki_yaw = v; }


private:
	float		_kp_roll_pitch;
	float		_ki_roll_pitch;
	float		_kp_yaw;
	float		_ki_yaw;
	float		_health;

	// Variable definitions
	float		q0;
	float		q1;
	float		q2;
	float		q3;	// quaternion elements representing the estimated orientation

	float twoKp;											// 2 * proportional gain (Kp)
	float twoKi;											// 2 * integral gain (Ki)
	float integralFBx,  integralFBy, integralFBz;	// integral error terms scaled by Ki

	// Methods
	void 		read_adc_raw(void);
	float 		read_adc(int select);
	void 		euler_angles(void);

	//
	float arctan2(float y, float x);
	// members
	Compass 	* _compass;

	// note: we use ref-to-pointer here so that our caller can change the GPS without our noticing
	//       IMU under us without our noticing.
	GPS 		*&_gps;                     // note: this is a reference to a pointer owned by the caller

	IMU 		*_imu;

	//Matrix3f	_dcm_matrix;

	Vector3f 	_accel_vector;				// Store the acceleration in a vector
	Vector3f 	_gyro_vector;				// Store the gyros turn rate in a vector
	Vector3f 	_omega;						// Corrected Gyro_Vector data
	Vector3f 	_omega_integ_corr;

	float					accelADC[3];
	float					gyroADC[3];
};

#endif



