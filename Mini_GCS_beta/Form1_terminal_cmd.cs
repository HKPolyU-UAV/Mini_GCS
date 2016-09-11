using System;
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
        // list of all commands
        string[] cmd_list = {
            "help", "all_cmd", "man",

            "uavshow",

            "take_off",
            "land",
            "goto",
            "mission_start",
            "mission_abort",
            "rtl",
            "arm",
            "gethome",
            "turn"
        };

        /**
         *  call user specified command
         */
        private string call_cmd(string[] cmd_words)
        {
            string res = "";

            if (cmd_words[0] == "help")
            {
                // prind out general help
                res = cmd_help(cmd_words);
            }
            else if (cmd_words[0] == "all_cmd")
            {
                // list all cmds available
                res = cmd_all(cmd_words);
            }
            else if (cmd_words[0] == "man")
            {
                // show command manual
                res = cmd_man(cmd_words);
            }
            else if (cmd_words[0] == "uavshow")
            {
                // show available uavs
                res = cmd_uavshow(cmd_words);
            }
            else if (cmd_words[0] == "arm")
            {
                // arm or disarm the uav
                res = cmd_arm(cmd_words);
            }
            else if (cmd_words[0] == "takeoff")
            {
                // uav takeoff
                res = cmd_takeoff(cmd_words);
            }
            else if (cmd_words[0] == "land")
            {
                // make the uav land 
                res = cmd_land(cmd_words);
            }
            else if (cmd_words[0] == "setmode")
            {
                // make the uav land 
                res = cmd_setmode(cmd_words);
            }
            else if (cmd_words[0] == "goto")
            {
                // go to a location in NED frame
                res = cmd_goto(cmd_words);
            }
            else if (cmd_words[0] == "rtl")
            {
                // return to launch
                res = cmd_rtl(cmd_words);
            }
            else if (cmd_words[0] == "gethome")
            {
                // return to launch
                res = cmd_gethome(cmd_words);
            }
            else if (cmd_words[0] == "turn")
            {
                // return to launch
                res = cmd_turn(cmd_words);
            }
            else if (cmd_words[0] == "imgt")
            {
                // test img transfer reception
                res = cmd_img_trans_test(cmd_words);
            }
            else
            {
                // command is not known
                res = "\r\n    unknown command \r\n";
            }
            
            return res;
        }

        /**
         *  text box terminal general help
         */
        private string cmd_help(string[] cmd_words)
        {
            // prind out general help
            string res;
            res = "\r\n   Operations: ";
            res += "\r\n       press ENTER to execute a command";
            res += "\r\n       press Ctrl+l to clear screen";
            res += "\r\n   About commands: ";
            res += "\r\n       all_cmd: display all commands available";
            res += "\r\n       man cmd: display user manual of command cmd";
            res += "\r\n";
            return res;
        }

        /**
         *  list all available cmds
         */
        private string cmd_all(string[] cmd_words)
        {
            string res;
            res = "\r\n    All commands:\r\n";
            foreach (string s in cmd_list)
            {
                res += "        " + s + "\r\n";
            }
            return res;
        }

        /**
         *  display command manual
         */
        private string cmd_man(string[] cmd_words)
        {
            string res = "";
            if (cmd_words.Length == 1)
            {
                res = "    usage: \r\n";
                res += "        man cmd \r\n";
            }
            else
            {
            }
            
            return res;
        }

        
    }
}
