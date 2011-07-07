using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ArdupilotMega
{
    public class CurrentState
    {
        // multipliers
        public float multiplierdist = 1;
        public float multiplierspeed = 1;

        // orientation - rads
        public float roll { get; set; }
        public float pitch { get; set; }
        public float yaw { get { return _yaw; } set { if (value < 0) { _yaw = value + 360; } else { _yaw = value; } } }
        private float _yaw = 0;

        public float groundcourse { get { return _groundcourse; } set { if (value < 0) { _groundcourse = value + 360; } else { _groundcourse = value; } } }
        private float _groundcourse = 0;

        // speeds
        public float airspeed { get { return _airspeed * multiplierspeed; } set { _airspeed = value; } }
        public float groundspeed { get { return _groundspeed * multiplierspeed; } set { _groundspeed = value; } }
        float _airspeed;
        float _groundspeed;

        // position
        public float lat { get; set; }
        public float lng { get; set; }
        public float alt { get { return (_alt - altoffsethome) * multiplierdist; } set { _alt = value; } }
        public float altoffsethome { get; set; }
        private float _alt = 0;
        public float gpsstatus { get; set; }
        public float gpshdop { get; set; }

        public float altd1000 { get { return (alt / 1000) % 10; } }
        public float altd100 { get { return (alt / 100) % 10; } }

        // accel
        public  float ax { get; set; }
        public  float ay { get; set; }
        public  float az { get; set; }
        // gyro
        public  float gx { get; set; }
        public  float gy { get; set; }
        public  float gz { get; set; }

        //radio
        public  float ch1in { get; set; }
        public  float ch2in { get; set; }
        public  float ch3in { get; set; }
        public  float ch4in { get; set; }
        public  float ch5in { get; set; }
        public  float ch6in { get; set; }
        public  float ch7in { get; set; }
        public  float ch8in { get; set; }

        // motors
        public float ch1out { get; set; }
        public float ch2out { get; set; }
        public float ch3out { get; set; }
        public float ch4out { get; set; }
        public float ch5out { get; set; }
        public float ch6out { get; set; }
        public float ch7out { get; set; }
        public float ch8out { get; set; }

        //nav state
        public float nav_roll { get; set; }
        public float nav_pitch { get; set; }
        public short nav_bearing { get; set; }
        public short target_bearing { get; set; }
        public ushort wp_dist { get { return (ushort)(_wpdist * multiplierdist); } set { _wpdist = value; } }
        public float alt_error { get { return _alt_error * multiplierdist; } set { _alt_error = value; } }
        public float ber_error { get { return (target_bearing - yaw); } set {  } }
        public float aspd_error { get { return _aspd_error * multiplierspeed; } set { _aspd_error = value; } }
        public float xtrack_error { get; set; }
        public int wpno { get; set; }
        public string mode { get; set; }
        public float climbrate { get; set; }
        ushort _wpdist;
        float _aspd_error;
        float _alt_error;

        public float targetaltd100 { get { return ((alt + alt_error) / 100) % 10; } }
        public float targetalt { get { return (alt + alt_error); } }

        //airspeed_error = (airspeed_error - airspeed);
        public float targetairspeed { get { return (airspeed + aspd_error / 100); } }


        //message
        public List<string> messages { get; set; }
        public string message { get { return messages[messages.Count - 1]; } set { } }

        //battery
        public float battery_voltage { get { return _battery_voltage; } set { _battery_voltage = value / 1000; } }
        private float _battery_voltage;
        public float battery_remaining { get { return _battery_remaining; } set { _battery_remaining = value / 1000; } }
        private float _battery_remaining;

        // HIL
        public int hilch1 { get; set; }
        public int hilch2 { get; set; }
        public int hilch3 { get; set; }
        public int hilch4 { get; set; } 

        // rc override
        public ushort rcoverridech1 { get; set; }
        public ushort rcoverridech2 { get; set; }
        public ushort rcoverridech3 { get; set; }
        public ushort rcoverridech4 { get; set; }

        // current firmware
        public MainV2.Firmwares firmware = MainV2.Firmwares.ArduPilotMega;

        // stats
        public ushort packetdrop { get; set; }

        public CurrentState()
        {
            mode = "";
            messages = new List<string>();
        }

        const float rad2deg = (float)(180 / Math.PI);
        const float deg2rad = (float)(1.0 / rad2deg);

        private DateTime lastupdate = DateTime.Now;

        public void UpdateCurrentSettings(System.Windows.Forms.BindingSource bs)
        {
            if (DateTime.Now > lastupdate.AddMilliseconds(19)) // 50 hz
            {

//                Console.WriteLine("Updating CurrentState " + DateTime.Now.Millisecond);
                if (MAVLink.packets[MAVLink.MAVLINK_MSG_ID_STATUSTEXT] != null) // status text 
                {
                    string logdata = DateTime.Now + " " + Encoding.ASCII.GetString(MAVLink.packets[MAVLink.MAVLINK_MSG_ID_STATUSTEXT], 6, MAVLink.packets[MAVLink.MAVLINK_MSG_ID_STATUSTEXT].Length - 6);
                    if (messages.Count > 5)
                    {
                        messages.RemoveAt(0);
                    }
                    messages.Add(logdata + "\n");

                    MAVLink.packets[MAVLink.MAVLINK_MSG_ID_STATUSTEXT] = null;
                }

                if (MAVLink.packets[MAVLink.MAVLINK_MSG_ID_RC_CHANNELS_SCALED] != null) // hil
                {
                    var hil = new ArdupilotMega.MAVLink.__mavlink_rc_channels_scaled_t();

                    object temp = hil;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[MAVLink.MAVLINK_MSG_ID_RC_CHANNELS_SCALED], ref temp, 6);

                    hil = (MAVLink.__mavlink_rc_channels_scaled_t)(temp);

                    hilch1 = hil.chan1_scaled;
                    hilch2 = hil.chan2_scaled;
                    hilch3 = hil.chan3_scaled;
                    hilch4 = hil.chan4_scaled;

                    //MAVLink.packets[MAVLink.MAVLINK_MSG_ID_RC_CHANNELS_SCALED] = null;
                }

                if (MAVLink.packets[MAVLink.MAVLINK_MSG_ID_NAV_CONTROLLER_OUTPUT] != null)
                {
                    MAVLink.__mavlink_nav_controller_output_t nav = new MAVLink.__mavlink_nav_controller_output_t();

                    object temp = nav;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[MAVLink.MAVLINK_MSG_ID_NAV_CONTROLLER_OUTPUT], ref temp, 6);

                    nav = (MAVLink.__mavlink_nav_controller_output_t)(temp);

                    nav_roll = nav.nav_roll;
                    nav_pitch = nav.nav_pitch;
                    nav_bearing = nav.nav_bearing;
                    target_bearing = nav.target_bearing;
                    wp_dist = nav.wp_dist;
                    alt_error = nav.alt_error;
                    aspd_error = nav.aspd_error;
                    xtrack_error = nav.xtrack_error;

                    //MAVLink.packets[MAVLink.MAVLINK_MSG_ID_NAV_CONTROLLER_OUTPUT] = null;
                }

                if (MAVLink.packets[ArdupilotMega.MAVLink.MAVLINK_MSG_ID_SYS_STATUS] != null)
                {
                    ArdupilotMega.MAVLink.__mavlink_sys_status_t sysstatus = new ArdupilotMega.MAVLink.__mavlink_sys_status_t();

                    object temp = sysstatus;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[ArdupilotMega.MAVLink.MAVLINK_MSG_ID_SYS_STATUS], ref temp, 6);

                    sysstatus = (ArdupilotMega.MAVLink.__mavlink_sys_status_t)(temp);

                    string oldmode = mode;

                    switch (sysstatus.mode)
                    {
                        case (byte)ArdupilotMega.MAVLink.MAV_MODE.MAV_MODE_MANUAL:
                            if (firmware == MainV2.Firmwares.ArduPilotMega)
                            {
                                mode = "Manual";
                            }
                            else if ((firmware == MainV2.Firmwares.ArduCopter2) || (firmware == MainV2.Firmwares.MegaPirate))
                            {
                                mode = "Acro";
                            }
                            break;
                        case (byte)ArdupilotMega.MAVLink.MAV_MODE.MAV_MODE_GUIDED:
                            if (firmware == MainV2.Firmwares.ArduPilotMega)
                            {
                                mode = "Guided";
                            }
                            else if ((firmware == MainV2.Firmwares.ArduCopter2) || (firmware == MainV2.Firmwares.MegaPirate))
                            {
                                mode = "Stabilize";
                            }
                            break;
                        case (byte)ArdupilotMega.MAVLink.MAV_MODE.MAV_MODE_TEST1:
                            if (firmware == MainV2.Firmwares.ArduPilotMega)
                            {
                                mode = "Stabilize";
                            }
                            else if ((firmware == MainV2.Firmwares.ArduCopter2) || (firmware == MainV2.Firmwares.MegaPirate))
                            {
                                mode = "Simple";
                            }
                            break;
                        case (byte)ArdupilotMega.MAVLink.MAV_MODE.MAV_MODE_TEST2: 
                            if (firmware == MainV2.Firmwares.ArduPilotMega)
                            { //FBW modes
                                mode = "FBW A"; // fall though  old
                                switch (sysstatus.nav_mode)
                                {
                                    case (byte)1:
                                        mode = "FBW A";
                                        break;
                                    case (byte)2:
                                        mode = "FBW B";
                                        break;
                                    case (byte)3:
                                        mode = "FBW C";
                                        break;
                                }
                            }
                            else if ((firmware == MainV2.Firmwares.ArduCopter2)  || (firmware == MainV2.Firmwares.MegaPirate))
                            {
                                mode = "Alt Hold";
                            }
                            break;
                        case (byte)ArdupilotMega.MAVLink.MAV_MODE.MAV_MODE_TEST3:
                            mode = "FBW B";
                            break;
                        case (byte)ArdupilotMega.MAVLink.MAV_MODE.MAV_MODE_AUTO:
                            switch (sysstatus.nav_mode)
                            {
                                case (byte)ArdupilotMega.MAVLink.MAV_NAV.MAV_NAV_WAYPOINT:
                                    mode = "Auto";
                                    break;
                                case (byte)ArdupilotMega.MAVLink.MAV_NAV.MAV_NAV_RETURNING:
                                    mode = "RTL";
                                    break;
                                case (byte)ArdupilotMega.MAVLink.MAV_NAV.MAV_NAV_HOLD:
                                case (byte)ArdupilotMega.MAVLink.MAV_NAV.MAV_NAV_LOITER:
                                    mode = "Loiter";
                                    break;
                                case (byte)ArdupilotMega.MAVLink.MAV_NAV.MAV_NAV_LIFTOFF:
                                    mode = "Takeoff";
                                    break;
                                case (byte)ArdupilotMega.MAVLink.MAV_NAV.MAV_NAV_LANDING:
                                    mode = "Land";
                                    break;
                            }

                            break;
                    }

                    battery_voltage = sysstatus.vbat;
                    battery_remaining = sysstatus.battery_remaining;

                    packetdrop = sysstatus.packet_drop;

                    if (oldmode != mode && MainV2.speechenable && MainV2.getConfig("speechmodeenabled") == "True")
                    {
                        MainV2.talk.SpeakAsync(Common.speechConversion(MainV2.getConfig("speechmode")));
                    }

                    //MAVLink.packets[ArdupilotMega.MAVLink.MAVLINK_MSG_ID_SYS_STATUS] = null;
                }

                if (MAVLink.packets[MAVLink.MAVLINK_MSG_ID_ATTITUDE] != null)
                {
                    var att = new ArdupilotMega.MAVLink.__mavlink_attitude_t();

                    object temp = att;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[MAVLink.MAVLINK_MSG_ID_ATTITUDE], ref temp, 6);

                    att = (MAVLink.__mavlink_attitude_t)(temp);

                    roll = att.roll * rad2deg;
                    pitch = att.pitch * rad2deg;
                    yaw = att.yaw * rad2deg;

                    //MAVLink.packets[MAVLink.MAVLINK_MSG_ID_ATTITUDE] = null;
                }

                if (MAVLink.packets[MAVLink.MAVLINK_MSG_ID_GPS_RAW] != null)
                {
                    var gps = new MAVLink.__mavlink_gps_raw_t();

                    object temp = gps;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[MAVLink.MAVLINK_MSG_ID_GPS_RAW], ref temp, 6);

                    gps = (MAVLink.__mavlink_gps_raw_t)(temp);

                    lat = gps.lat;
                    lng = gps.lon;
                    //                alt = gps.alt; // using vfr as includes baro calc

                    gpsstatus = gps.fix_type;

                    gpshdop = gps.eph;

                    groundspeed = gps.v;
                    groundcourse = gps.hdg;

                    //MAVLink.packets[MAVLink.MAVLINK_MSG_ID_GPS_RAW] = null;
                }
                if (MAVLink.packets[MAVLink.MAVLINK_MSG_ID_GLOBAL_POSITION_INT] != null)
                {
                    var loc = new MAVLink.__mavlink_global_position_int_t();

                    object temp = loc;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[MAVLink.MAVLINK_MSG_ID_GLOBAL_POSITION_INT], ref temp, 6);

                    loc = (MAVLink.__mavlink_global_position_int_t)(temp);

                    alt = loc.alt / 1000.0f;
                    lat = loc.lat / 10000000.0f;
                    lng = loc.lon / 10000000.0f;

                }
                if (MAVLink.packets[MAVLink.MAVLINK_MSG_ID_GLOBAL_POSITION] != null)
                {
                    var loc = new MAVLink.__mavlink_global_position_t();

                    object temp = loc;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[MAVLink.MAVLINK_MSG_ID_GLOBAL_POSITION], ref temp, 6);

                    loc = (MAVLink.__mavlink_global_position_t)(temp);

                    alt = loc.alt;
                    lat = loc.lat;
                    lng = loc.lon;

                }
                if (MAVLink.packets[ArdupilotMega.MAVLink.MAVLINK_MSG_ID_WAYPOINT_CURRENT] != null)
                {
                    ArdupilotMega.MAVLink.__mavlink_waypoint_current_t wpcur = new ArdupilotMega.MAVLink.__mavlink_waypoint_current_t();

                    object temp = wpcur;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[ArdupilotMega.MAVLink.MAVLINK_MSG_ID_WAYPOINT_CURRENT], ref temp, 6);

                    wpcur = (ArdupilotMega.MAVLink.__mavlink_waypoint_current_t)(temp);

                    int oldwp = wpno;

                    wpno = wpcur.seq;

                    if (oldwp != wpno && MainV2.speechenable && MainV2.getConfig("speechwaypointenabled") == "True")
                    {
                        MainV2.talk.SpeakAsync(Common.speechConversion(MainV2.getConfig("speechwaypoint")));
                    }

                    //MAVLink.packets[ArdupilotMega.MAVLink.MAVLINK_MSG_ID_WAYPOINT_CURRENT] = null;
                }

                if (MAVLink.packets[MAVLink.MAVLINK_MSG_ID_RC_CHANNELS_RAW] != null)
                {
                    var rcin = new MAVLink.__mavlink_rc_channels_raw_t();

                    object temp = rcin;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[MAVLink.MAVLINK_MSG_ID_RC_CHANNELS_RAW], ref temp, 6);

                    rcin = (MAVLink.__mavlink_rc_channels_raw_t)(temp);

                    ch1in = rcin.chan1_raw;
                    ch2in = rcin.chan2_raw;
                    ch3in = rcin.chan3_raw;
                    ch4in = rcin.chan4_raw;
                    ch5in = rcin.chan5_raw;
                    ch6in = rcin.chan6_raw;
                    ch7in = rcin.chan7_raw;
                    ch8in = rcin.chan8_raw;

                    //MAVLink.packets[MAVLink.MAVLINK_MSG_ID_RC_CHANNELS_RAW] = null;
                }

                if (MAVLink.packets[MAVLink.MAVLINK_MSG_ID_SERVO_OUTPUT_RAW] != null)
                {
                    var servoout = new MAVLink.__mavlink_servo_output_raw_t();

                    object temp = servoout;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[MAVLink.MAVLINK_MSG_ID_SERVO_OUTPUT_RAW], ref temp, 6);

                    servoout = (MAVLink.__mavlink_servo_output_raw_t)(temp);

                    ch1out = servoout.servo1_raw;
                    ch2out = servoout.servo2_raw;
                    ch3out = servoout.servo3_raw;
                    ch4out = servoout.servo4_raw;
                    ch5out = servoout.servo5_raw;
                    ch6out = servoout.servo6_raw;
                    ch7out = servoout.servo7_raw;
                    ch8out = servoout.servo8_raw;

                    //MAVLink.packets[MAVLink.MAVLINK_MSG_ID_SERVO_OUTPUT_RAW] = null;
                }

                if (MAVLink.packets[MAVLink.MAVLINK_MSG_ID_RAW_IMU] != null)
                {
                    var imu = new MAVLink.__mavlink_raw_imu_t();

                    object temp = imu;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[MAVLink.MAVLINK_MSG_ID_RAW_IMU], ref temp, 6);

                    imu = (MAVLink.__mavlink_raw_imu_t)(temp);

                    gx = imu.xgyro;
                    gy = imu.ygyro;
                    gz = imu.zgyro;

                    ax = imu.xacc;
                    ay = imu.yacc;
                    az = imu.zacc;

                    //MAVLink.packets[MAVLink.MAVLINK_MSG_ID_RAW_IMU] = null;
                }

                if (MAVLink.packets[MAVLink.MAVLINK_MSG_ID_VFR_HUD] != null)
                {
                    MAVLink.__mavlink_vfr_hud_t vfr = new MAVLink.__mavlink_vfr_hud_t();

                    object temp = vfr;

                    MAVLink.ByteArrayToStructure(MAVLink.packets[MAVLink.MAVLINK_MSG_ID_VFR_HUD], ref temp, 6);

                    vfr = (MAVLink.__mavlink_vfr_hud_t)(temp);

                    groundspeed = vfr.groundspeed;
                    airspeed = vfr.airspeed;

                    alt = vfr.alt; // this might include baro

                    climbrate = vfr.climb;

                    //MAVLink.packets[MAVLink.MAVLINK_MSG_ID_VFR_HUD] = null;
                }
                lastupdate = DateTime.Now;
            }

            //Console.WriteLine(DateTime.Now.Millisecond + " start ");
            // update form
            try
            {
                if (bs != null)
                {
                    //Console.WriteLine(DateTime.Now.Millisecond);
                    bs.DataSource = this;
                    //Console.WriteLine(DateTime.Now.Millisecond + " 1 ");
                    bs.ResetBindings(false);
                    //Console.WriteLine(DateTime.Now.Millisecond + " done ");
                }
            }
            catch { Console.WriteLine("CurrentState Binding error"); }
        }
    }
}
