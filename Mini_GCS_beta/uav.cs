using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mini_GCS_beta
{
    class uav
    {
        public string name;
        public byte id;
        public string status;
        public double last_msg_time;

        public bool armed;
        public int custom_mode;
        public byte base_mode_flag;
        public byte gps_fix_type;

        public double lat, lon, alt, vx, vy, vz;
        public double lat_home, lon_home, alt_home;
        public float roll, pitch, yaw;
        public float gspeed, aspeed; // ground & air speed
        public Int16 heading;
        public UInt16 throttle;

        public float bat_volt, bat_amp;
        public int bat_remaining;

        public bool flush_disarm, flush_vel;

        public uav()
        {
            lat = 0; lon = 0; alt = 0;
            vx = 0; vy = 0; vz = 0;
            roll = 0; pitch = 0; yaw = 0;
            heading = 0;
        }
    }


}
