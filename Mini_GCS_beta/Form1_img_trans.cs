using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace Mini_GCS_beta
{
    partial class Form1
    {
        ConcurrentQueue<byte[]> img_raw_buff = new ConcurrentQueue<byte[]>();
        private delegate void AddDataDelegate(string s);
        private AddDataDelegate AddDataObj_terminal;

        private string cmd_img_trans_test(string[] cmd_words)
        {
            double t1 = epoch_now();

            AddDataObj_terminal = new AddDataDelegate(AddDataMethod_terminal);
            string res = "";
            DFrame msg = new DFrame();

            // initiate img transmission
            msg.source_id = gcs.id;
            msg.target_id = 0x03;
            msg.route = 0;
            msg.payload[0] = 10; // img transfer request
            msg.payload[1] = Convert.ToByte('N');
            msg.len = 2;
            msg.send(serial_ch1);

            // wait for first pkg, which tells the size of the img
            byte[] tmp;
            int img_size = 0;
            Int16 pkg_num = 0;
            Int16 block_size = 0;
            int timeout_cnt = 0;
            
            while (timeout_cnt < 300)
            {
                if (img_raw_buff.TryDequeue(out tmp))
                {
                    if (tmp[1] != 0xFF | tmp[2] != 0xFF)
                    {
                        continue;
                    }
                    pkg_num = BitConverter.ToInt16(tmp, 3);
                    img_size = BitConverter.ToInt32(tmp, 5);
                    block_size = BitConverter.ToInt16(tmp, 9);
                    res =  "    img request accepted \r\n";
                    res += "    number of pkg: " + Convert.ToString(pkg_num) + "\r\n";
                    res += "    size of img: " + Convert.ToString(img_size) + "\r\n";
                    res += "    img block size: " + Convert.ToString(block_size) + "\r\n";
                    TextBoxTerminal.Invoke(this.AddDataObj_terminal, res);
                    break;
                }
                else
                {
                    timeout_cnt += 1;
                    Thread.Sleep(1);
                }
            }

            if (timeout_cnt >= 300)
            {
                res = "    timeout\r\n";
                TextBoxTerminal.Invoke(this.AddDataObj_terminal, res);
                return "";
            }

            
            // receive the rest of the img
            Int16 pkg_cnt = 0;
            const int max_try = 10;
            int try_cnt = 0;
            byte[] img_buf = new byte[img_size];
            List<Int16> received_pkg = new List<Int16>();
            msg.payload[1] = Convert.ToByte('R');
            msg.len = 4;
            for (pkg_cnt = 0; pkg_cnt < pkg_num; pkg_cnt++)
            {
                try_cnt = 0;
                while (try_cnt < max_try)
                {

                    msg.payload[2] = (byte)(pkg_cnt & 0x00FF);
                    msg.payload[3] = (byte)(pkg_cnt >> 8);
                    res = "requesting pkg " + Convert.ToString(pkg_cnt) + "\r\n";
                    TextBoxTerminal.Invoke(this.AddDataObj_terminal, res);
                    msg.send(serial_ch1);
                    timeout_cnt = 0;
                    while (timeout_cnt < 400)
                    {
                        if (img_raw_buff.TryDequeue(out tmp))
                        {
                            if (BitConverter.ToInt16(tmp, 1) != pkg_cnt)
                            {
                                continue;
                            }

                            Int16 pkg_cnt_get = BitConverter.ToInt16(tmp, 1);
                            received_pkg.Add(pkg_cnt);

                            // copy received pkg to local buffer
                            int pkg_len = (pkg_cnt == pkg_num - 1) ? img_size % block_size : block_size;
                            Buffer.BlockCopy(tmp,3,
                                             img_buf, pkg_cnt * block_size, pkg_len);

                            res = "    received pkg: " + Convert.ToString(pkg_cnt_get) + "\r\n";
                            TextBoxTerminal.Invoke(this.AddDataObj_terminal, res);
                            break;
                        }
                        else
                        {
                            timeout_cnt += 1;
                            Thread.Sleep(1);
                        }
                    }

                    if (timeout_cnt >= 400)
                    {
                        try_cnt += 1;
                        //Thread.Sleep(1);
                    }
                    else
                        break;
                }
            }


            // check whether the reception is complete


            // request re-transmission of missing pkgs

            // write picture to file
            BinaryWriter img_write = new BinaryWriter(File.Open("try.jpg", FileMode.Create));
            img_write.Write(img_buf);
            img_write.Close();

            res = "time take: " + Convert.ToString(Math.Round(epoch_now() - t1, 2)) + " seconds";
            TextBoxTerminal.Invoke(this.AddDataObj_terminal, res);

            return " ";
        }

        private void AddDataMethod_terminal(string s)
        {
            TextBoxTerminal.AppendText(s);
        }
    }
}
