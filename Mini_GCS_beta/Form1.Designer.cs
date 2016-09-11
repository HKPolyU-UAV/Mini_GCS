namespace Mini_GCS_beta
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serial_ch1 = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorker_serial_ch1 = new System.ComponentModel.BackgroundWorker();
            this.TextBoxTerminal = new System.Windows.Forms.TextBox();
            this.backgroundWorker_terminal = new System.ComponentModel.BackgroundWorker();
            this.timer_uav = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Readmap = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.label_mom_bat = new System.Windows.Forms.Label();
            this.button_mom_go = new System.Windows.Forms.Button();
            this.label_mom_fix = new System.Windows.Forms.Label();
            this.label_mom_vfr = new System.Windows.Forms.Label();
            this.label_mom_att = new System.Windows.Forms.Label();
            this.label_mom_mode = new System.Windows.Forms.Label();
            this.label_mom_arm = new System.Windows.Forms.Label();
            this.label_mom_conn = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_son_bat = new System.Windows.Forms.Label();
            this.button_son_go = new System.Windows.Forms.Button();
            this.label_son_fix = new System.Windows.Forms.Label();
            this.label_son_vfr = new System.Windows.Forms.Label();
            this.label_son_att = new System.Windows.Forms.Label();
            this.label_son_mode = new System.Windows.Forms.Label();
            this.label_son_arm = new System.Windows.Forms.Label();
            this.label_son_conn = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.timer_GPS = new System.Windows.Forms.Timer(this.components);
            this.timer_flush = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // serial_ch1
            // 
            this.serial_ch1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serial_ch1_DataReceived);
            // 
            // backgroundWorker_serial_ch1
            // 
            this.backgroundWorker_serial_ch1.WorkerSupportsCancellation = true;
            this.backgroundWorker_serial_ch1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_serial_ch1_DoWork);
            // 
            // TextBoxTerminal
            // 
            this.TextBoxTerminal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TextBoxTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxTerminal.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextBoxTerminal.ForeColor = System.Drawing.Color.Lime;
            this.TextBoxTerminal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextBoxTerminal.Location = new System.Drawing.Point(0, 0);
            this.TextBoxTerminal.Multiline = true;
            this.TextBoxTerminal.Name = "TextBoxTerminal";
            this.TextBoxTerminal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxTerminal.Size = new System.Drawing.Size(522, 334);
            this.TextBoxTerminal.TabIndex = 6;
            this.TextBoxTerminal.TextChanged += new System.EventHandler(this.TextBoxTerminal_TextChanged);
            this.TextBoxTerminal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxTerminal_KeyDown);
            this.TextBoxTerminal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxTerminal_KeyPress);
            this.TextBoxTerminal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextBoxTerminal_MouseDown);
            this.TextBoxTerminal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TextBoxTerminal_MouseUp);
            this.TextBoxTerminal.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TextBoxTerminal_PreviewKeyDown);
            // 
            // backgroundWorker_terminal
            // 
            this.backgroundWorker_terminal.WorkerSupportsCancellation = true;
            this.backgroundWorker_terminal.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_terminal_DoWork);
            this.backgroundWorker_terminal.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_terminal_RunWorkerCompleted);
            // 
            // timer_uav
            // 
            this.timer_uav.Enabled = true;
            this.timer_uav.Interval = 250;
            this.timer_uav.Tick += new System.EventHandler(this.timer_uav_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(244, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 29);
            this.button1.TabIndex = 4;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(333, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 29);
            this.button2.TabIndex = 5;
            this.button2.Text = "Find Port";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Baud Rate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Port";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(129, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(109, 21);
            this.textBox1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 18);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(110, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(5, 29);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(15);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.Readmap);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1065, 640);
            this.splitContainer1.SplitterDistance = 53;
            this.splitContainer1.TabIndex = 9;
            // 
            // Readmap
            // 
            this.Readmap.Location = new System.Drawing.Point(422, 10);
            this.Readmap.Name = "Readmap";
            this.Readmap.Size = new System.Drawing.Size(83, 29);
            this.Readmap.TabIndex = 10;
            this.Readmap.Text = "Readmap";
            this.Readmap.UseVisualStyleBackColor = true;
            this.Readmap.Click += new System.EventHandler(this.Readmap_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label8.Location = new System.Drawing.Point(949, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "Current Position";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label7.Location = new System.Drawing.Point(703, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "Last Position";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(703, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "##";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(949, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "##";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.gmap);
            this.splitContainer2.Size = new System.Drawing.Size(1065, 583);
            this.splitContainer2.SplitterDistance = 524;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.TextBoxTerminal);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.AutoScroll = true;
            this.splitContainer3.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer3.Size = new System.Drawing.Size(524, 583);
            this.splitContainer3.SplitterDistance = 336;
            this.splitContainer3.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(522, 241);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(514, 215);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "UAV Status";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label_mom_bat);
            this.splitContainer4.Panel1.Controls.Add(this.button_mom_go);
            this.splitContainer4.Panel1.Controls.Add(this.label_mom_fix);
            this.splitContainer4.Panel1.Controls.Add(this.label_mom_vfr);
            this.splitContainer4.Panel1.Controls.Add(this.label_mom_att);
            this.splitContainer4.Panel1.Controls.Add(this.label_mom_mode);
            this.splitContainer4.Panel1.Controls.Add(this.label_mom_arm);
            this.splitContainer4.Panel1.Controls.Add(this.label_mom_conn);
            this.splitContainer4.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.label_son_bat);
            this.splitContainer4.Panel2.Controls.Add(this.button_son_go);
            this.splitContainer4.Panel2.Controls.Add(this.label_son_fix);
            this.splitContainer4.Panel2.Controls.Add(this.label_son_vfr);
            this.splitContainer4.Panel2.Controls.Add(this.label_son_att);
            this.splitContainer4.Panel2.Controls.Add(this.label_son_mode);
            this.splitContainer4.Panel2.Controls.Add(this.label_son_arm);
            this.splitContainer4.Panel2.Controls.Add(this.label_son_conn);
            this.splitContainer4.Panel2.Controls.Add(this.label4);
            this.splitContainer4.Size = new System.Drawing.Size(508, 209);
            this.splitContainer4.SplitterDistance = 248;
            this.splitContainer4.TabIndex = 0;
            // 
            // label_mom_bat
            // 
            this.label_mom_bat.BackColor = System.Drawing.Color.LightGray;
            this.label_mom_bat.Location = new System.Drawing.Point(130, 172);
            this.label_mom_bat.Name = "label_mom_bat";
            this.label_mom_bat.Size = new System.Drawing.Size(100, 17);
            this.label_mom_bat.TabIndex = 14;
            this.label_mom_bat.Text = "battery: 000%";
            this.label_mom_bat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_mom_go
            // 
            this.button_mom_go.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_mom_go.Location = new System.Drawing.Point(5, 142);
            this.button_mom_go.Name = "button_mom_go";
            this.button_mom_go.Size = new System.Drawing.Size(88, 47);
            this.button_mom_go.TabIndex = 11;
            this.button_mom_go.Text = "Go!";
            this.button_mom_go.UseVisualStyleBackColor = true;
            this.button_mom_go.Click += new System.EventHandler(this.button_mom_go_Click);
            // 
            // label_mom_fix
            // 
            this.label_mom_fix.BackColor = System.Drawing.Color.Red;
            this.label_mom_fix.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_mom_fix.Location = new System.Drawing.Point(9, 41);
            this.label_mom_fix.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label_mom_fix.Name = "label_mom_fix";
            this.label_mom_fix.Padding = new System.Windows.Forms.Padding(3);
            this.label_mom_fix.Size = new System.Drawing.Size(193, 24);
            this.label_mom_fix.TabIndex = 6;
            this.label_mom_fix.Text = "GPS Fix: No Fix";
            this.label_mom_fix.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_mom_vfr
            // 
            this.label_mom_vfr.BackColor = System.Drawing.SystemColors.Control;
            this.label_mom_vfr.Location = new System.Drawing.Point(95, 95);
            this.label_mom_vfr.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label_mom_vfr.Name = "label_mom_vfr";
            this.label_mom_vfr.Size = new System.Drawing.Size(107, 44);
            this.label_mom_vfr.TabIndex = 5;
            this.label_mom_vfr.Text = "heading: 000.00\r\nair speed: 00.00\r\ngnd speed: 00.00";
            this.label_mom_vfr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_mom_att
            // 
            this.label_mom_att.BackColor = System.Drawing.SystemColors.Control;
            this.label_mom_att.Location = new System.Drawing.Point(8, 95);
            this.label_mom_att.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label_mom_att.Name = "label_mom_att";
            this.label_mom_att.Size = new System.Drawing.Size(85, 44);
            this.label_mom_att.TabIndex = 4;
            this.label_mom_att.Text = "Roll: 000.00\r\nPitch: 000.00\r\nYaw: 000.00";
            this.label_mom_att.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_mom_mode
            // 
            this.label_mom_mode.BackColor = System.Drawing.SystemColors.Control;
            this.label_mom_mode.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_mom_mode.Location = new System.Drawing.Point(9, 68);
            this.label_mom_mode.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label_mom_mode.Name = "label_mom_mode";
            this.label_mom_mode.Padding = new System.Windows.Forms.Padding(3);
            this.label_mom_mode.Size = new System.Drawing.Size(193, 24);
            this.label_mom_mode.TabIndex = 3;
            this.label_mom_mode.Text = "Mode: 16 position hold";
            this.label_mom_mode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_mom_arm
            // 
            this.label_mom_arm.BackColor = System.Drawing.Color.Red;
            this.label_mom_arm.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_mom_arm.Location = new System.Drawing.Point(120, 21);
            this.label_mom_arm.Name = "label_mom_arm";
            this.label_mom_arm.Size = new System.Drawing.Size(78, 17);
            this.label_mom_arm.TabIndex = 2;
            this.label_mom_arm.Text = "Disarmed";
            this.label_mom_arm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_mom_conn
            // 
            this.label_mom_conn.BackColor = System.Drawing.Color.Red;
            this.label_mom_conn.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_mom_conn.Location = new System.Drawing.Point(9, 22);
            this.label_mom_conn.Name = "label_mom_conn";
            this.label_mom_conn.Size = new System.Drawing.Size(106, 16);
            this.label_mom_conn.TabIndex = 1;
            this.label_mom_conn.Text = "Disconnected";
            this.label_mom_conn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.DarkGray;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "UAV: mom";
            // 
            // label_son_bat
            // 
            this.label_son_bat.BackColor = System.Drawing.Color.LightGray;
            this.label_son_bat.Location = new System.Drawing.Point(138, 172);
            this.label_son_bat.Name = "label_son_bat";
            this.label_son_bat.Size = new System.Drawing.Size(99, 17);
            this.label_son_bat.TabIndex = 13;
            this.label_son_bat.Text = "battery: 000%";
            this.label_son_bat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_son_go
            // 
            this.button_son_go.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_son_go.Location = new System.Drawing.Point(5, 142);
            this.button_son_go.Name = "button_son_go";
            this.button_son_go.Size = new System.Drawing.Size(91, 47);
            this.button_son_go.TabIndex = 12;
            this.button_son_go.Text = "Go!";
            this.button_son_go.UseVisualStyleBackColor = true;
            this.button_son_go.Click += new System.EventHandler(this.button_son_go_Click);
            // 
            // label_son_fix
            // 
            this.label_son_fix.BackColor = System.Drawing.Color.Red;
            this.label_son_fix.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_son_fix.Location = new System.Drawing.Point(11, 41);
            this.label_son_fix.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label_son_fix.Name = "label_son_fix";
            this.label_son_fix.Size = new System.Drawing.Size(193, 24);
            this.label_son_fix.TabIndex = 7;
            this.label_son_fix.Text = "GPS Fix: No Fix";
            this.label_son_fix.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_son_vfr
            // 
            this.label_son_vfr.BackColor = System.Drawing.SystemColors.Control;
            this.label_son_vfr.Location = new System.Drawing.Point(97, 95);
            this.label_son_vfr.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label_son_vfr.Name = "label_son_vfr";
            this.label_son_vfr.Size = new System.Drawing.Size(107, 44);
            this.label_son_vfr.TabIndex = 6;
            this.label_son_vfr.Text = "heading: 000.00\r\nair speed: 00.00\r\ngnd speed: 00.00";
            this.label_son_vfr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_son_att
            // 
            this.label_son_att.BackColor = System.Drawing.SystemColors.Control;
            this.label_son_att.Location = new System.Drawing.Point(6, 95);
            this.label_son_att.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label_son_att.Name = "label_son_att";
            this.label_son_att.Size = new System.Drawing.Size(85, 44);
            this.label_son_att.TabIndex = 6;
            this.label_son_att.Text = "Roll: 000.00\r\nPitch: 000.00\r\nYaw: 000.00";
            this.label_son_att.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_son_mode
            // 
            this.label_son_mode.BackColor = System.Drawing.SystemColors.Control;
            this.label_son_mode.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_son_mode.Location = new System.Drawing.Point(11, 68);
            this.label_son_mode.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label_son_mode.Name = "label_son_mode";
            this.label_son_mode.Size = new System.Drawing.Size(193, 24);
            this.label_son_mode.TabIndex = 5;
            this.label_son_mode.Text = "Mode: 16 position hold";
            this.label_son_mode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_son_arm
            // 
            this.label_son_arm.BackColor = System.Drawing.Color.Red;
            this.label_son_arm.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_son_arm.Location = new System.Drawing.Point(122, 21);
            this.label_son_arm.Name = "label_son_arm";
            this.label_son_arm.Size = new System.Drawing.Size(75, 17);
            this.label_son_arm.TabIndex = 4;
            this.label_son_arm.Text = "Disarmed";
            this.label_son_arm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_son_conn
            // 
            this.label_son_conn.BackColor = System.Drawing.Color.Red;
            this.label_son_conn.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_son_conn.Location = new System.Drawing.Point(11, 22);
            this.label_son_conn.Name = "label_son_conn";
            this.label_son_conn.Size = new System.Drawing.Size(105, 16);
            this.label_son_conn.TabIndex = 3;
            this.label_son_conn.Text = "Disconnected";
            this.label_son_conn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.DarkGray;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "UAV: son";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(513, 215);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(0, 0);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 23;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(535, 581);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 17D;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.Location = new System.Drawing.Point(5, 5);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1065, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // timer_GPS
            // 
            this.timer_GPS.Enabled = true;
            this.timer_GPS.Interval = 1000;
            this.timer_GPS.Tick += new System.EventHandler(this.timer_GPS_Tick);
            // 
            // timer_flush
            // 
            this.timer_flush.Enabled = true;
            this.timer_flush.Interval = 500;
            this.timer_flush.Tick += new System.EventHandler(this.timer_flush_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1075, 674);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Mini GCS Beta";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serial_ch1;
        private System.ComponentModel.BackgroundWorker backgroundWorker_serial_ch1;
        private System.Windows.Forms.TextBox TextBoxTerminal;
        private System.ComponentModel.BackgroundWorker backgroundWorker_terminal;
        private System.Windows.Forms.Timer timer_uav;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_mom_arm;
        private System.Windows.Forms.Label label_mom_conn;
        private System.Windows.Forms.Label label_son_arm;
        private System.Windows.Forms.Label label_son_conn;
        private System.Windows.Forms.Label label_mom_mode;
        private System.Windows.Forms.Label label_son_mode;
        private System.Windows.Forms.Label label_mom_att;
        private System.Windows.Forms.Label label_son_att;
        private System.Windows.Forms.Label label_mom_vfr;
        private System.Windows.Forms.Label label_son_vfr;
        private System.Windows.Forms.Label label_mom_fix;
        private System.Windows.Forms.Label label_son_fix;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.Timer timer_GPS;
        private System.Windows.Forms.Button button_mom_go;
        private System.Windows.Forms.Button button_son_go;
        private System.Windows.Forms.Timer timer_flush;
        private System.Windows.Forms.Label label_mom_bat;
        private System.Windows.Forms.Label label_son_bat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Readmap;
    }
}

