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
        /*--------------------------------------------------------------------*/
        /*             TextBoxTerminal implementation                         */
        /*--------------------------------------------------------------------*/
        /**
         * special variables
         */
        private int cursor_initial;
        private bool terminal_blocked;
        private string current_station_name = "GCS";
        private List<string> cmd_history = new List<string>();
        private int cmd_history_index = 0;
        private const int cmd_history_max = 20;

        /**
        *  initialize text box terminal
        */
        private void TextBoxTerminal_Init()
        {
            Array.Sort(cmd_list);
            TextBoxTerminal.Clear();
            TextBoxTerminal.Text = "GCS @ GCS: ~$ ";
            TextBoxTerminal.Select(TextBoxTerminal.Text.Length, 0);
            cursor_initial = TextBoxTerminal.Text.Length;
            terminal_blocked = false;
        }


        /**
         *  enable up/down key capture
         */
        private void TextBoxTerminal_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.IsInputKey = true;
            }
        }

        /**
         *  capture special key presses
         */
        private void TextBoxTerminal_KeyDown(object sender, KeyEventArgs e)
        {
            // if focus is NOT of the terminal, don't do anything
            if (TextBoxTerminal.Focus() == false)
                return;

            // when ENTER is pressed
            if (e.KeyCode == Keys.Enter)
            {
                int len = TextBoxTerminal.Text.Length - cursor_initial;
                string sentense = TextBoxTerminal.Text.Substring(cursor_initial, len);
                cmd_history.Add(sentense);
                cmd_history_index = cmd_history.Count - 1;

                if (cmd_history.Count > cmd_history_max)
                {
                    cmd_history.RemoveAt(0);
                }

                backgroundWorker_terminal.RunWorkerAsync(sentense);

                cursor_initial = TextBoxTerminal.Text.Length;
                terminal_blocked = true;
            }

            // when ctrl+l is pressed, clear screen, except for current line
            if (e.Control && e.KeyCode == Keys.L)
            {
                int len = TextBoxTerminal.Text.Length - cursor_initial;
                string sentense = TextBoxTerminal.Text.Substring(cursor_initial, len);

                cursor_initial = 14;

                TextBoxTerminal.Text = current_station_name + " @ GCS: ~$ " + sentense;
                TextBoxTerminal.Select(TextBoxTerminal.Text.Length, 0);
            }

            // up/down key to see previous cmds
            if (e.KeyCode == Keys.Down)
                {
                }
            if (e.KeyCode == Keys.Up)
                {
                }

            if (!e.Control)
                cursor_check();
        }

        private void TextBoxTerminal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        /**
         *  background worker to analyse and execute terminal commands
         */
        private void backgroundWorker_terminal_DoWork(object sender, DoWorkEventArgs e)
        {
            // detect empty cmd
            string cmd = (string)e.Argument;
            if (string.IsNullOrWhiteSpace(cmd))
                return;

            // break down cmd into words
            cmd.Trim();
            string[] cmd_words = cmd.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // call cmd
            string res = call_cmd(cmd_words);

            e.Result = res;
        }

        /**
         *  background worker job completed
         */
        private void backgroundWorker_terminal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            terminal_blocked = false;
            string res = (string)e.Result;
            TextBoxTerminal.Text += res + "\r\n";
            TextBoxTerminal.Text += current_station_name + " @ GCS: ~$ ";
            TextBoxTerminal.Select(TextBoxTerminal.Text.Length, 0);
            cursor_initial = TextBoxTerminal.Text.Length;
            TextBoxTerminal.ScrollToCaret();
        }


        /**
         *  check user input whenever terminal content changes
         */
        private void TextBoxTerminal_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxTerminal.Text.Length < cursor_initial)
            {
                TextBoxTerminal.Text += " ";
                TextBoxTerminal.Select(TextBoxTerminal.Text.Length, 0);
            }
        }

        /**
         *  check cursor location, don't allow it to move back past the current line
         */
        private void TextBoxTerminal_MouseDown(object sender, MouseEventArgs e)
        {
            //cursor_check();
        }

        private void TextBoxTerminal_MouseUp(object sender, MouseEventArgs e)
        {
            //cursor_check();
        }

        private void cursor_check()
        {
            if (TextBoxTerminal.SelectionStart <= cursor_initial)
                TextBoxTerminal.Select(TextBoxTerminal.Text.Length, 0);
        }

    }
}
