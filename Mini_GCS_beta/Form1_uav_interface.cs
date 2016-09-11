using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace Mini_GCS_beta
{
    partial class Form1 
    {
        /**
         *  Private variables
         */
        private string[] uav_status_enum = {"connected ok", "disconnected", "self"};
        private List<uav> uav_list = new List<uav>();
        private int uav_timeout_sec = 5;
        private double last_heartbeat;
        private DFrame msg_cmd_ack = new DFrame();
        private bool msg_cmd_ack_received = false;
        private ConcurrentQueue<DFrame> df_msg = new ConcurrentQueue<DFrame>();
        private uav gcs;
        private AutoResetEvent uav_cmd_wait = new AutoResetEvent(false);

        

        /**
         *  Initialize uav interface
         */
        private void uav_interface_init()
        {
            
            // add ourselves
            uav_list.Add(new uav());
            uav_list[0].name = "GCS";
            uav_list[0].status = "self";
            uav_list[0].id = 0x01;
            uav_list[0].last_msg_time = -1;
            uav_list[0].armed = false;
            gcs = uav_list[0];

            // add uav_mom
            uav_list.Add(new uav());
            uav_list[1].name = "mom";
            uav_list[1].status = "disconnected";
            uav_list[1].id = 0x02;
            uav_list[1].last_msg_time = 1;
            uav_list[1].armed = false;
            uav_list[1].alt_home = 0;
            uav_list[1].lon_home = 0;
            uav_list[1].lat_home = 0;
            uav_list[1].flush_disarm = false;
            uav_list[1].flush_vel = false;

            // add uav_son
            uav_list.Add(new uav());
            uav_list[2].name = "son";
            uav_list[2].status = "disconnected";
            uav_list[2].id = 0x03;
            uav_list[2].last_msg_time = 1;
            uav_list[2].armed = false;
            uav_list[2].alt_home = 0;
            uav_list[2].lon_home = 0;
            uav_list[2].lat_home = 0;
            uav_list[2].flush_disarm = false;
            uav_list[2].flush_vel = false;

            // subscribe datalink msgs
            //DFrame_sub_id = DFrame_sub_manager.register_new_subscriber(20);
            //backgroundWorker_uav_interface.RunWorkerAsync(DFrame_sub_id);


            last_heartbeat = epoch_now();
            timer_uav.Enabled = true;
        }

        /**
         *  Check uav status a few times per second, using timer tick
         */
        private void timer_uav_Tick(object sender, EventArgs e)        {
            DFrame msg = new DFrame();
            double time_now = epoch_now();

            // broadcast a heartbeat
            if (time_now - last_heartbeat >= 1)
            {
                last_heartbeat = time_now;
                msg.source_id = gcs.id;
                msg.target_id = 0x00;
                msg.len = 1;
                msg.route = 0;
                msg.payload[0] = 0x00;

                if(serial_ch1.IsOpen)
                    msg.send(serial_ch1);
            }
            
            // check uav connection status
            foreach (uav u in uav_list)
            {
                if (u.status != "self")
                {
                    Label label_conn, label_mode, label_arm, label_att, label_vfr, label_fix, label_bat;
                    if (u.name == "son")
                    {
                        label_conn = label_son_conn;
                        label_arm = label_son_arm;
                        label_mode = label_son_mode;
                        label_att = label_son_att;
                        label_vfr = label_son_vfr;
                        label_fix = label_son_fix;
                        label_bat = label_son_bat;
                    }
                    else
                    {
                        label_conn = label_mom_conn;
                        label_arm = label_mom_arm;
                        label_mode = label_mom_mode;
                        label_att = label_mom_att;
                        label_vfr = label_mom_vfr;
                        label_fix = label_mom_fix;
                        label_bat = label_mom_bat;
                    }

                    // detect uav connection
                    if (time_now - u.last_msg_time >= uav_timeout_sec)
                    {
                        if (u.status == "connected")
                        {
                            // issue a warning if connection is lost halfway
                        }
                        u.status = "disconnected";
                        label_conn.Text = "Disconnected";
                        label_conn.BackColor = System.Drawing.Color.Red;
                       
                    }
                    else
                    {
                        u.status = "connected";
                        label_conn.Text = "Connected";
                        label_conn.BackColor = System.Drawing.Color.Lime;
                    }

                    // uav arm status
                    if ((u.base_mode_flag>>7)!=0)
                    {
                        label_arm.Text = "Armed";
                        label_arm.BackColor = System.Drawing.Color.Lime;                    
                    }
                    else
                    {
                        label_arm.Text = "Disrmed";
                        label_arm.BackColor = System.Drawing.Color.Red;
                    }

                    //uav mode update
                    string mode;
                    if (u.custom_mode == 0)
                        mode = "Mode: 0 stabilize";
                    else if (u.custom_mode == 2)
                        mode = "Mode: 2 altitude hold";
                    else if (u.custom_mode == 3)
                        mode = "Mode: 3 auto";
                    else if (u.custom_mode == 4)
                        mode = "Mode: 4 guided";
                    else if (u.custom_mode == 5)
                        mode = "Mode: 5 loiter";
                    else if (u.custom_mode == 6)
                        mode = "Mode: 6 RTL";
                    else if (u.custom_mode == 9)
                        mode = "Mode: 9 land";
                    else if (u.custom_mode == 16)
                        mode = "Mode: 16 position hold";
                    else
                        mode = "Mode: unknown";

                    label_mode.Text = mode;

                    // GPD fix update
                    string type = "No Info";
                    if (u.gps_fix_type == 0 | u.gps_fix_type == 1)
                    {
                        type = "No Fix";
                        label_fix.BackColor = System.Drawing.Color.Red;
                    }
                    else if (u.gps_fix_type == 2)
                    {
                        type = "2D Fixed";
                        label_fix.BackColor = System.Drawing.Color.Red;
                    }
                    else if (u.gps_fix_type == 3)
                    {
                        type = "3D Fixed";
                        label_fix.BackColor = System.Drawing.Color.Lime;
                    }
                    else if (u.gps_fix_type == 4)
                    {
                        type = "DGPS";
                        label_fix.BackColor = System.Drawing.Color.Red;
                    }

                    label_fix.Text = "GPS Fix: " + type;

                    // update UAV attutude
                    label_att.Text = "Roll: " + Convert.ToString(Math.Round(u.roll*180/Math.PI, 2)) + "\r\n" +
                                        "Pitch: " + Convert.ToString(Math.Round(u.pitch * 180 / Math.PI, 2)) + "\r\n" +
                                        "Yaw: " + Convert.ToString(Math.Round(u.yaw * 180 / Math.PI, 2));
                    // update UAV VFR
                    label_vfr.Text = "heading: " + Convert.ToString(u.heading) + "\r\n" +
                                        "air speed: " + Convert.ToString(Math.Round(u.aspeed, 2)) + "\r\n" +
                                        "gnd speed: " + Convert.ToString(Math.Round(u.gspeed, 2));

                    label_bat.Text = "battery: " + Convert.ToString(u.bat_remaining) + "%";

                }
            }// end of foreachss
        }


        /**
         *  check the content of DFrame msg and do related actions 
         */
        private ConcurrentQueue<byte> hehe = new ConcurrentQueue<byte>();
        private void dframe_decoder(DFrame msg)
        {

            // update msg reception time
            foreach (uav u in uav_list)
                if (u.id == msg.source_id)
                {
                    lock (u)
                    {
                        u.last_msg_time = epoch_now();

                        // attitude
                        if (msg.payload[0] == 30)
                        {
                            u.roll = BitConverter.ToSingle(msg.payload, 1);
                            u.pitch = BitConverter.ToSingle(msg.payload, 5);
                            u.yaw = BitConverter.ToSingle(msg.payload, 9);
                        }
                        // global position int
                        if (msg.payload[0] == 33)
                        {
                            u.lat = (float)(BitConverter.ToInt32(msg.payload, 1)) / 10000000;
                            u.lon = (float)(BitConverter.ToInt32(msg.payload, 5)) / 10000000;
                            u.alt = (float)(BitConverter.ToInt32(msg.payload, 9)) / 1000;
                            u.vx = (float)(BitConverter.ToInt32(msg.payload, 13)) / 100;
                            u.vy = (float)(BitConverter.ToInt32(msg.payload, 15)) / 100;
                            u.vz = (float)(BitConverter.ToInt32(msg.payload, 17)) / 100;
                        }
                        // VFR hud
                        if (msg.payload[0] == 74)
                        {
                            u.aspeed = BitConverter.ToSingle(msg.payload, 1);
                            u.gspeed = BitConverter.ToSingle(msg.payload, 5);
                            u.heading = BitConverter.ToInt16(msg.payload, 9);
                            u.throttle = BitConverter.ToUInt16(msg.payload, 11);
                        }
                        // battery
                        if (msg.payload[0] == 75)
                        {
                            u.bat_volt = (float)(BitConverter.ToInt16(msg.payload, 1) / 100);
                            u.bat_amp = (float)(BitConverter.ToInt16(msg.payload, 3) / 100);
                            u.bat_remaining = (int)BitConverter.ToInt16(msg.payload, 5);
                        }
                        // heartbeat
                        if (msg.payload[0] == 0)
                        {
                            u.custom_mode = (int)BitConverter.ToUInt32(msg.payload, 1);
                            u.base_mode_flag = msg.payload[5];
                            u.gps_fix_type = msg.payload[7];
                        }
                        // home location 
                        if (msg.payload[0] == 242)
                        {

                        }
                        // img
                        if (msg.payload[0] == 11)
                        {
                            img_raw_buff.Enqueue(msg.payload);
                        }
                    }
                }

            // look for pending replies
            lock (msg_cmd_ack)
            {
                if (msg.payload[0] == 3 && !msg_cmd_ack_received)
                {
                    msg_cmd_ack_received = true;
                    msg_cmd_ack = msg;
                    uav_cmd_wait.Set();
                }
            }
        }


        /**
         *  Return current epoch seconds
         */
        private double epoch_now()
        {
            return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }

/*------------------------------------------------------------
 *                UAV related commands
 -------------------------------------------------------------*/

        /**
         *  Show all available uavs
         */
        private string cmd_uavshow(string[] cmd_words)
        {
            string res;
            res =  "\r\n    All uavs (including GCS): \r\n";
            res += "\r\n        Name \t\t Status \r\n";
            res += "    ------------------------------\r\n";
            foreach (uav u in uav_list)
            {
                string tab = (u.name.Length <= 6) ? " \t\t " : " \t ";
                res += "        " + u.name + tab + u.status + "\r\n";
            }
            return res;
        }


        /**
         *  Arm or disarm the uav
         */
        private string cmd_arm(string[] cmd_words)
        {
            string res = "";
            bool action = false;

            // check input
            if (cmd_words.Length < 3)
            {
                res =  "    Usage:\r\n";
                res += "        arm uav_name status: check arming status of uav_name\r\n";
                res += "        arm uav_name arm: arm uav_name\r\n";
                res += "        arm uav_name disarm: disarm uav_name\r\n";
                return res;
            }

            // check uav_name validity
            uav utmp = new uav();
            bool tfound = false;
            foreach(uav u in uav_list)
                if (u.name == cmd_words[1] && u.status != "self")
                {
                    utmp = u;
                    tfound = true;
                }
            if (!tfound)
            {
                res =  "    uav_name is invalid\r\n";
                res += "    use showuav to list available uav_names\r\n";
                return res;
            }


            // check operation validity
            if (cmd_words[2] == "status")
            {
                res = "    " + cmd_words[1] + " is ";
                res += (utmp.armed) ? "armed \r\n" : "not armed\r\n";
            }
            else if (cmd_words[2] == "arm")
            {
                if (utmp.status == "disconnected")
                {
                    //res = "    " + utmp.name + " must be connected first.\r\n";
                    //return res;
                }
                action = true;
            }
            else if (cmd_words[2] == "disarm")
            {
                if (utmp.status == "disconnected")
                {
                    //res = "    " + utmp.name + " must be connected first.\r\n";
                    //return res;
                }
                action = false;
            }
            else
            {
                res =  "    action in invalid\r\n";
                res += "    Usage:\r\n";
                res += "        arm uav_name status: check arming status of uav_name\r\n";
                res += "        arm uav_name arm: arm uav_name\r\n";
                res += "        arm uav_name disarm: disarm uav_name\r\n";
                return res;
            }
            
            // construct and send cmd to uav
            DFrame msg = new DFrame();
            msg.source_id = gcs.id;
            msg.target_id = utmp.id;
            msg.route = 1;
            msg.payload[0] = 0x02; // this is a cmd msg
            msg.payload[1] = 0x06; // cmd_id is 0x06 (arm)
            msg.payload[2] = (action) ? Convert.ToByte('T') : Convert.ToByte('F');

            msg.len = 3;
           
            
            // wait for uav reply
            lock (msg_cmd_ack)
            {
                msg_cmd_ack_received = false;
                msg.send(serial_ch1);
            }
            uav_cmd_wait.WaitOne(2000);
            lock (msg_cmd_ack)
            {
                if (msg_cmd_ack_received)
                {
                    if (msg_cmd_ack.payload[2] == 84)
                    {
                        res = "    action successful, retval: ";
                        res += Convert.ToString(msg_cmd_ack.payload[3]) + "\r\n";
                    }
                    else if (msg_cmd_ack.payload[2] == 70)
                    {
                        res = "    action failed, retval: ";
                        res += Convert.ToString(msg_cmd_ack.payload[3]);
                        res += "\r\n               meaning: ";
                        res += cmd_ack_explain(Convert.ToInt32(Convert.ToString(msg_cmd_ack.payload[3])));
                    }
                    else
                        res = "    unknown reply";
                }
                else
                {
                    res = "    timeout";
                }
            }

            return res;
        }


        /**
        *   uav takeoff
        */
        private string cmd_takeoff(string[] cmd_words)
        {
            string res = "";

            // check input
            if (cmd_words.Length < 3)
            {
                res = "    Usage:\r\n";
                res += "        takeoff uav_name alt: command uav_name to take off ac current location\r\n";
                res += "                              and fly to target altitude given by alt (uint: meter)\r\n";
                return res;
            }

            // check uav_name validity
            uav utmp = new uav();
            bool tfound = false;
            foreach (uav u in uav_list)
                if (u.name == cmd_words[1] && u.status != "self")
                {
                    utmp = u;
                    tfound = true;
                    if (utmp.status == "disconnected")
                    {
                        res = "    " + utmp.name + " must be connected first.\r\n";
                        return res;
                    }
                }
            if (!tfound)
            {
                res = "    uav_name is invalid\r\n";
                res += "    use showuav to list available uav_names\r\n";
                return res;
            }

            float target_alt;
            float.TryParse(cmd_words[2], out target_alt);
            byte[] he = BitConverter.GetBytes(target_alt);

            // construct and send cmd to uav
            DFrame msg = new DFrame();
            msg.source_id = gcs.id;
            msg.target_id = utmp.id;
            msg.route = 1;
            msg.payload[0] = 0x02; // this is a cmd msg
            msg.payload[1] = 0x00; // cmd_id is 0x00 (takeoff)
            msg.payload[2] = he[0]; // target alt, 4-byte float;
            msg.payload[3] = he[1];
            msg.payload[4] = he[2];
            msg.payload[5] = he[3];

            msg.len = 6;

            // wait for uav reply
            lock (this)
            {
                msg.send(serial_ch1);
                msg_cmd_ack_received = false;
            }
            uav_cmd_wait.WaitOne(5000);
            lock (msg_cmd_ack)
            {
                if (msg_cmd_ack_received)
                {
                    if (msg_cmd_ack.payload[2] == 84)
                    {
                        res = "    action successful, retval: ";
                        res += Convert.ToString(msg_cmd_ack.payload[3]) + "\r\n";
                    }
                    else if (msg_cmd_ack.payload[2] == 70)
                    {
                        res = "    action failed, retval: ";
                        res += Convert.ToString(msg_cmd_ack.payload[3]);
                        res += "\r\n               meaning: ";
                        res += cmd_ack_explain(Convert.ToInt32(Convert.ToString(msg_cmd_ack.payload[3])));
                    }
                    else
                        res = "    unknown reply";
                }
                else
                {
                    res = "    timeout";
                }
            }

            return res;
        }


        /**
         *   Land the uav
         */
        private string cmd_land(string[] cmd_words)
        {
            string res = "";

            // check input
            if (cmd_words.Length < 2)
            {
                res = "    Usage:\r\n";
                res += "        land uav_name: command uav_name to land where it is now\r\n";
                return res;
            }

            // check uav_name validity
            uav utmp = new uav();
            bool tfound = false;
            foreach (uav u in uav_list)
                if (u.name == cmd_words[1] && u.status != "self")
                {
                    utmp = u;
                    tfound = true;
                    if (utmp.status == "disconnected")
                    {
                        res = "    " + utmp.name + " must be connected first.\r\n";
                        return res;
                    }
                }
            if (!tfound)
            {
                res = "    uav_name is invalid\r\n";
                res += "    use showuav to list available uav_names\r\n";
                return res;
            }

            // construct and send cmd to uav
            DFrame msg = new DFrame();
            msg.source_id = gcs.id;
            msg.target_id = utmp.id;
            msg.route = 1;
            msg.payload[0] = 0x02; // this is a cmd msg
            msg.payload[1] = 0x01; // cmd_id is 0x01 (land)

            msg.len = 2;

            // wait for uav reply
            lock (this)
            {
                msg.send(serial_ch1);
                msg_cmd_ack_received = false;
            }
            uav_cmd_wait.WaitOne(5000);
            lock (msg_cmd_ack)
            {
                if (msg_cmd_ack_received)
                {
                    if (msg_cmd_ack.payload[2] == 84)
                    {
                        res = "    action successful, retval: ";
                        res += Convert.ToString(msg_cmd_ack.payload[3]) + "\r\n";
                    }
                    else if (msg_cmd_ack.payload[2] == 70)
                    {
                        res = "    action failed, retval: ";
                        res += Convert.ToString(msg_cmd_ack.payload[3]);
                        res += "\r\n               meaning: ";
                        res += cmd_ack_explain(Convert.ToInt32(Convert.ToString(msg_cmd_ack.payload[3])));
                    }
                    else
                        res = "    unknown reply";
                }
                else
                {
                    res = "    timeout";
                }
            }
            return res;
        }


        /**
       *   go to a position relative to local NED
       */
        private string cmd_goto(string[] cmd_words)
        {
            string res = "";

            // check input
            if (cmd_words.Length < 3)
            {
                res = "    too few arguments";
                return res;
            }

            // check uav_name validity
            uav utmp = new uav();
            bool tfound = false;
            foreach (uav u in uav_list)
                if (u.name == cmd_words[1] && u.status != "self")
                {
                    utmp = u;
                    tfound = true;
                    if (utmp.status == "disconnected")
                    {
                        res = "    " + utmp.name + " must be connected first.\r\n";
                        return res;
                    }
                }
            if (!tfound)
            {
                res = "    uav_name is invalid\r\n";
                res += "    use showuav to list available uav_names\r\n";
                return res;
            }

            double[] param = new double[6];
            double.TryParse(cmd_words[2], out param[0]); // x, y, z
            double.TryParse(cmd_words[3], out param[1]);
            double.TryParse(cmd_words[4], out param[2]);
            double.TryParse(cmd_words[5], out param[3]); // vx, vy, vz
            double.TryParse(cmd_words[6], out param[4]);
            double.TryParse(cmd_words[7], out param[5]);
            

            // construct and send cmd to uav
            DFrame msg = new DFrame();
            msg.source_id = gcs.id;
            msg.target_id = utmp.id;
            msg.route = 1;
            msg.payload[0] = 0x02; // this is a cmd msg
            msg.payload[1] = 0x02; // cmd_id is 0x02 (goto)

            int i = 0;
            foreach (double dd in param)
            {
                byte[] he = BitConverter.GetBytes(dd);
                msg.payload[2+i*8] = he[0];
                msg.payload[3+i*8] = he[1];
                msg.payload[4+i*8] = he[2];
                msg.payload[5+i*8] = he[3];
                msg.payload[6+i*8] = he[4];
                msg.payload[7+i*8] = he[5];
                msg.payload[8+i*8] = he[6];
                msg.payload[9+i*8] = he[7];
                i += 1;
            }

            msg.len = 50;
            msg.send(serial_ch1);
            res = "    command transmitted \r\n";
            return res;
        }

        /**
         *   return to launch
         */
        private string cmd_rtl(string[] cmd_words)
        {
            string res = "";

            // check input
            if (cmd_words.Length < 2)
            {
                res = "    usage:\r\n";
                res += "        rtl uav_name: ask uav_name to return to launch";
                return res;
            }

            // check uav_name validity
            uav utmp = new uav();
            bool tfound = false;
            foreach (uav u in uav_list)
                if (u.name == cmd_words[1] && u.status != "self")
                {
                    utmp = u;
                    tfound = true;
                    if (utmp.status == "disconnected")
                    {
                        res = "    " + utmp.name + " must be connected first.\r\n";
                        //return res;
                    }
                }
            if (!tfound)
            {
                res = "    uav_name is invalid\r\n";
                res += "    use showuav to list available uav_names\r\n";
                return res;
            }

            // construct and send cmd to uav
            DFrame msg = new DFrame();
            msg.source_id = gcs.id;
            msg.target_id = utmp.id;
            msg.route = 1;
            msg.payload[0] = 0x02; // this is a cmd msg
            msg.payload[1] = 0x05; // cmd_id is 0x05 (rtl)
            msg.len = 2;

            // wait for uav reply
            lock (this)
            {
                msg.send(serial_ch1);
                msg_cmd_ack_received = false;
            }
            uav_cmd_wait.WaitOne(5000);

            // analyse reply
            if (msg_cmd_ack_received)
            {
                if (msg_cmd_ack.payload[2] == 84)
                {
                    res = "    action succeeded \r\n";
                }
                else if (msg_cmd_ack.payload[2] == 70)
                {
                    res = "    action failed \r\n";
                }
                else
                    res = "    unknown reply";
            }
            else
            {
                res = "    timeout";
            }

            return res;
        }


        /**
         *   change uav mode
         */
        private string cmd_setmode(string[] cmd_words)
        {
            string res = "";

            // check input
            if (cmd_words.Length < 2)
            {
                res =  "    setmode uav_name mode: set the mode of uav_name\r\n";
                res += "    setmode -l: list all available mode";
                return res;
            }

            // list available modes
            if (cmd_words[1] == "-l")
            {
                res =  "  list of all modes";
                res += "  setmode accepts either num or string \r\n";
                res += "    num    |string    |explanation   \r\n";
                res += "    -------|----------|------------- \r\n";
                res += "    0      |stabilize |              \r\n";
                res += "    2      |althold   |altitude hold \r\n";
                res += "    3      |auto      |              \r\n";
                res += "    4      |guided    |              \r\n";
                res += "    5      |loiter    |              \r\n";
                res += "    6      |rtl       |              \r\n";
                res += "    9      |land      |              \r\n";
                res += "    16     |poshold   |position hold \r\n";
                return res;
            }

            // check uav_name validity
            uav utmp = new uav();
            bool tfound = false;
            foreach (uav u in uav_list)
                if (u.name == cmd_words[1] && u.status != "self")
                {
                    utmp = u;
                    tfound = true;
                    if (utmp.status == "disconnected")
                    {
                        res = "    " + utmp.name + " must be connected first.\r\n";
                        return res;
                    }
                }
            if (!tfound)
            {
                res = "    uav_name is invalid\r\n";
                res += "    use showuav to list available uav_names\r\n";
                return res;
            }

            // retrive mode
            byte mav_mode = 0;
            if (cmd_words[2] == "stabilize" || cmd_words[2] == "0")
                mav_mode = 0; // stabilize mode APM
            else if (cmd_words[2] == "althold" || cmd_words[2] == "2")
                mav_mode = 2; // altitude hold mode APM
            else if (cmd_words[2] == "auto" || cmd_words[2] == "3")
                mav_mode = 3; // auto mode APM
            else if (cmd_words[2] == "guided" || cmd_words[2] == "4")
                mav_mode = 4; // guided mode APM
            else if (cmd_words[2] == "loiter" || cmd_words[2] == "5")
                mav_mode = 5; // loiter mode APM
            else if (cmd_words[2] == "rtl" || cmd_words[2] == "6")
                mav_mode = 6; // return to launch (RTL) mode APM
            else if (cmd_words[2] == "land" || cmd_words[2] == "9")
                mav_mode = 9; // land mode APM
            else if (cmd_words[2] == "poshold" || cmd_words[2] == "16")
                mav_mode = 16; // position hold mode APM
            else
            {
                res = "    unknown mode \r\n";
                return res;
            }

            // construct and send cmd to uav
            DFrame msg = new DFrame();
            msg.source_id = gcs.id;
            msg.target_id = utmp.id;
            msg.route = 1;
            msg.payload[0] = 0x02; // this is a cmd msg
            msg.payload[1] = 0x07; // cmd_id is 0x07 (setmode)
            msg.payload[2] = mav_mode;   // target mode, see MAV_MODE
            msg.len = 3;

            // wait for uav reply
            lock (this)
            {
                msg.send(serial_ch1);
                msg_cmd_ack_received = false;
            }
            uav_cmd_wait.WaitOne(5000);

            // analyse reply
            if (msg_cmd_ack_received)
            {
                if (msg_cmd_ack.payload[2] == 84)
                {
                    res = "    action succeeded \r\n";
                }
                else if (msg_cmd_ack.payload[2] == 70)
                {
                    res = "    action failed \r\n";
                }
                else
                    res = "    unknown reply";
            }
            else
            {
                res = "    timeout";
            }

            return res;
        }


        /**
         *   request home location of uav 
         */
        private string cmd_gethome(string[] cmd_words)
        {
            string res = "";

            // check input
            if (cmd_words.Length < 2)
            {
                res = "    usage: gethome uav_name";
                return res;
            }

            // check uav_name validity
            uav utmp = new uav();
            bool tfound = false;
            foreach (uav u in uav_list)
                if (u.name == cmd_words[1] && u.status != "self")
                {
                    utmp = u;
                    tfound = true;
                    if (utmp.status == "disconnected")
                    {
                        res = "    " + utmp.name + " must be connected first.\r\n";
                        return res;
                    }
                }
            if (!tfound)
            {
                res = "    uav_name is invalid\r\n";
                res += "    use showuav to list available uav_names\r\n";
                return res;
            }

            // construct and send cmd to uav
            DFrame msg = new DFrame();
            msg.source_id = gcs.id;
            msg.target_id = utmp.id;
            msg.route = 1;
            msg.payload[0] = 0x02; // this is a cmd msg
            msg.payload[1] = 8; // cmd_id is 0x08 (gethome)
            msg.len = 2;

            // wait for uav reply
            lock (this)
            {
                msg.send(serial_ch1);
                msg_cmd_ack_received = false;
            }
            uav_cmd_wait.WaitOne(5000);

            // analyse reply
            if (msg_cmd_ack_received)
            {
                if (msg_cmd_ack.payload[2] == 84)
                {
                    res = "    action succeeded \r\n";
                }
                else if (msg_cmd_ack.payload[2] == 70)
                {
                    res = "    action failed:  ";
                    res += Convert.ToString(msg_cmd_ack.payload[3]);
                    res += "\r\n               meaning: ";
                    res += cmd_ack_explain(Convert.ToInt32(Convert.ToString(msg_cmd_ack.payload[3])));
                }
                else
                    res = "    unknown reply";
            }
            else
            {
                res = "    timeout";
            }

            return res;
        }

        /**
         *   ask uav to change its yaw 
         */
        private string cmd_turn(string[] cmd_words)
        {
            string res = "";

            // check input
            if (cmd_words.Length < 4)
            {
                res = "    change the UAV's yaw (heading)\r\n";
                res += "    usage: turn uav_name direction deg \r\n";
                res += "               uav_name: name of uav to turn\r\n";
                res += "               direction: abs = absolute \r\n";
                res += "                          cw = relative, clockwise \r\n";
                res += "                          ccw = relative, counter clockwise \r\n";
                res += "               deg: number of degrees to turn (0~360)\r\n";
                return res;
            }

            // check uav_name validity
            uav utmp = new uav();
            bool tfound = false;
            foreach (uav u in uav_list)
                if (u.name == cmd_words[1] && u.status != "self")
                {
                    utmp = u;
                    tfound = true;
                    if (utmp.status == "disconnected")
                    {
                        res = "    " + utmp.name + " must be connected first.\r\n";
                        return res;
                    }
                }
            if (!tfound)
            {
                res = "    uav_name is invalid\r\n";
                res += "    use showuav to list available uav_names\r\n";
                return res;
            }

            // construct and send cmd to uav
            DFrame msg = new DFrame();
            msg.source_id = gcs.id;
            msg.target_id = utmp.id;
            msg.route = 1;
            msg.payload[0] = 0x02; // this is a cmd msg
            msg.payload[1] = 10; // cmd_id is 0x08 (gethome)

            if (cmd_words[2] == "abs")
                msg.payload[2] = 0;
            else if (cmd_words[2] == "cw")
                msg.payload[2] = 1;
            else if (cmd_words[2] == "ccw")
                msg.payload[2] = 2;
            else
            {
                res = "    unknown turning direction\r\n";
                res += "      use one of the following: abs, cw, cww\r\n";
                return res;
            }

            UInt16 deg;
            UInt16.TryParse(cmd_words[3], out deg);
            if (deg > 360)
            {
                res = "    angle of turn: 0~360 \r\n";
                return res;
            }

            byte[] he = BitConverter.GetBytes(deg);
            msg.payload[3] = he[0];
            msg.payload[4] = he[1];
            msg.len = 5;

            // wait for uav reply
            lock (this)
            {
                msg.send(serial_ch1);
                msg_cmd_ack_received = false;
            }
            uav_cmd_wait.WaitOne(5000);

            // analyse reply
            if (msg_cmd_ack_received)
            {
                if (msg_cmd_ack.payload[2] == 84)
                {
                    res = "    action succeeded \r\n";
                }
                else if (msg_cmd_ack.payload[2] == 70)
                {
                    res = "    action failed:  ";
                    res += Convert.ToString(msg_cmd_ack.payload[3]);
                    res += "\r\n               meaning: ";
                    res += cmd_ack_explain(Convert.ToInt32(Convert.ToString(msg_cmd_ack.payload[3])));
                }
                else
                    res = "    unknown reply";
            }
            else
            {
                res = "    timeout";
            }

            return res;
        }


        /*
         *  send vel cmd to UAV every 0.5s to keep it moving
         */
        private void timer_flush_Tick(object sender, EventArgs e)
        { 
            DFrame msg = new DFrame();
            msg.source_id = gcs.id;
            msg.route = 1;
            msg.payload[0] = 0x02; // this is a cmd msg

            foreach (uav u in uav_list)
            {
                msg.target_id = u.id;
                if (u.flush_vel == true)
                {
                    msg.payload[1] = 12; // vel command
                    float vel = 1;
                    byte[] he = BitConverter.GetBytes(vel);
                    msg.payload[2] = he[0];
                    msg.payload[3] = he[1];
                    msg.payload[4] = he[2];
                    msg.payload[5] = he[3];
                    msg.len = 6;
                    msg.send(serial_ch1);
                }
            }
        }

        private void button_mom_go_Click(object sender, EventArgs e)
        {
            if (button_mom_go.Text == "Go!")
            {
                button_mom_go.Text = "Stop";
                /*
                uav_list[1].flush_vel = true;
                timer_flush.Enabled = true; // start flushing
                */
            }
            else
            {
                button_mom_go.Text = "Go!";
                /*
                uav_list[1].flush_vel = false;
                timer_flush.Enabled = false; // stop flushing
                */
            }
        }

        private void button_son_go_Click(object sender, EventArgs e)
        {
            if (button_son_go.Text == "Go!")
            {
                button_son_go.Text = "Stop";
                /*
                uav_list[2].flush_vel = true;
                timer_flush.Enabled = true;
                */
            }
            else
            {
                button_son_go.Text = "Go!";
                /*
                uav_list[2].flush_vel = false;
                timer_flush.Enabled = false;
                */
            }
        }
        /**
         *  MAVLink COMMAND_ACK return value explanation
         */
        private string cmd_ack_explain(int res)
        {
            string expl = "";
            switch (res)
            {
                case 0: expl = "command ACCEPTED and EXECUTED";
                    break;
                case 1: expl = "command TEMPORARY REJECTED/DENIED";
                    break;
                case 2: expl = "command PERMANENTLY DENIED";
                    break;
                case 3: expl = "command UNKNOWN/UNSUPPORTED";
                    break;
                case 4: expl = "command executed, but failed";
                    break;
                default: expl = "unknown return value";
                    break;
            }
            return expl;
        }
    }
}
