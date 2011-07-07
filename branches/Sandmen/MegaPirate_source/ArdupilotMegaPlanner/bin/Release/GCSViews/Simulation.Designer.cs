namespace ArdupilotMega.GCSViews
{
    partial class Simulation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Simulation));
            this.CHKREV_roll = new System.Windows.Forms.CheckBox();
            this.CHKREV_pitch = new System.Windows.Forms.CheckBox();
            this.CHKREV_rudder = new System.Windows.Forms.CheckBox();
            this.GPSrate = new System.Windows.Forms.ComboBox();
            this.ConnectComPort = new ArdupilotMega.MyButton();
            this.OutputLog = new System.Windows.Forms.RichTextBox();
            this.TXT_roll = new System.Windows.Forms.Label();
            this.TXT_pitch = new System.Windows.Forms.Label();
            this.TXT_heading = new System.Windows.Forms.Label();
            this.TXT_wpdist = new System.Windows.Forms.Label();
            this.currentStateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TXT_bererror = new System.Windows.Forms.Label();
            this.TXT_alterror = new System.Windows.Forms.Label();
            this.TXT_lat = new System.Windows.Forms.Label();
            this.TXT_long = new System.Windows.Forms.Label();
            this.TXT_alt = new System.Windows.Forms.Label();
            this.SaveSettings = new ArdupilotMega.MyButton();
            this.RAD_softXplanes = new System.Windows.Forms.RadioButton();
            this.RAD_softFlightGear = new System.Windows.Forms.RadioButton();
            this.TXT_servoroll = new System.Windows.Forms.Label();
            this.TXT_servopitch = new System.Windows.Forms.Label();
            this.TXT_servorudder = new System.Windows.Forms.Label();
            this.TXT_servothrottle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.TXT_yaw = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.TXT_control_mode = new System.Windows.Forms.Label();
            this.TXT_WP = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.TXT_throttlegain = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.TXT_ruddergain = new System.Windows.Forms.TextBox();
            this.TXT_pitchgain = new System.Windows.Forms.TextBox();
            this.TXT_rollgain = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.CHKdisplayall = new System.Windows.Forms.CheckBox();
            this.CHKgraphroll = new System.Windows.Forms.CheckBox();
            this.CHKgraphpitch = new System.Windows.Forms.CheckBox();
            this.CHKgraphrudder = new System.Windows.Forms.CheckBox();
            this.CHKgraphthrottle = new System.Windows.Forms.CheckBox();
            this.but_advsettings = new ArdupilotMega.MyButton();
            this.chkSensor = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.currentStateBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // CHKREV_roll
            // 
            resources.ApplyResources(this.CHKREV_roll, "CHKREV_roll");
            this.CHKREV_roll.Name = "CHKREV_roll";
            this.CHKREV_roll.UseVisualStyleBackColor = true;
            this.CHKREV_roll.CheckedChanged += new System.EventHandler(this.CHKREV_roll_CheckedChanged);
            // 
            // CHKREV_pitch
            // 
            resources.ApplyResources(this.CHKREV_pitch, "CHKREV_pitch");
            this.CHKREV_pitch.Name = "CHKREV_pitch";
            this.CHKREV_pitch.UseVisualStyleBackColor = true;
            this.CHKREV_pitch.CheckedChanged += new System.EventHandler(this.CHKREV_pitch_CheckedChanged);
            // 
            // CHKREV_rudder
            // 
            resources.ApplyResources(this.CHKREV_rudder, "CHKREV_rudder");
            this.CHKREV_rudder.Name = "CHKREV_rudder";
            this.CHKREV_rudder.UseVisualStyleBackColor = true;
            this.CHKREV_rudder.CheckedChanged += new System.EventHandler(this.CHKREV_rudder_CheckedChanged);
            // 
            // GPSrate
            // 
            resources.ApplyResources(this.GPSrate, "GPSrate");
            this.GPSrate.FormattingEnabled = true;
            this.GPSrate.Items.AddRange(new object[] {
            resources.GetString("GPSrate.Items"),
            resources.GetString("GPSrate.Items1"),
            resources.GetString("GPSrate.Items2"),
            resources.GetString("GPSrate.Items3"),
            resources.GetString("GPSrate.Items4"),
            resources.GetString("GPSrate.Items5"),
            resources.GetString("GPSrate.Items6")});
            this.GPSrate.Name = "GPSrate";
            this.GPSrate.SelectedIndexChanged += new System.EventHandler(this.GPSrate_SelectedIndexChanged);
            this.GPSrate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GPSrate_KeyDown);
            this.GPSrate.Leave += new System.EventHandler(this.GPSrate_Leave);
            // 
            // ConnectComPort
            // 
            resources.ApplyResources(this.ConnectComPort, "ConnectComPort");
            this.ConnectComPort.Name = "ConnectComPort";
            this.ConnectComPort.UseVisualStyleBackColor = true;
            this.ConnectComPort.Click += new System.EventHandler(this.ConnectComPort_Click);
            // 
            // OutputLog
            // 
            resources.ApplyResources(this.OutputLog, "OutputLog");
            this.OutputLog.Name = "OutputLog";
            this.OutputLog.TextChanged += new System.EventHandler(this.OutputLog_TextChanged);
            // 
            // TXT_roll
            // 
            resources.ApplyResources(this.TXT_roll, "TXT_roll");
            this.TXT_roll.Name = "TXT_roll";
            // 
            // TXT_pitch
            // 
            resources.ApplyResources(this.TXT_pitch, "TXT_pitch");
            this.TXT_pitch.Name = "TXT_pitch";
            // 
            // TXT_heading
            // 
            resources.ApplyResources(this.TXT_heading, "TXT_heading");
            this.TXT_heading.Name = "TXT_heading";
            // 
            // TXT_wpdist
            // 
            resources.ApplyResources(this.TXT_wpdist, "TXT_wpdist");
            this.TXT_wpdist.Name = "TXT_wpdist";
            // 
            // currentStateBindingSource
            // 
            this.currentStateBindingSource.DataSource = typeof(ArdupilotMega.CurrentState);
            // 
            // TXT_bererror
            // 
            resources.ApplyResources(this.TXT_bererror, "TXT_bererror");
            this.TXT_bererror.Name = "TXT_bererror";
            // 
            // TXT_alterror
            // 
            resources.ApplyResources(this.TXT_alterror, "TXT_alterror");
            this.TXT_alterror.Name = "TXT_alterror";
            // 
            // TXT_lat
            // 
            resources.ApplyResources(this.TXT_lat, "TXT_lat");
            this.TXT_lat.Name = "TXT_lat";
            // 
            // TXT_long
            // 
            resources.ApplyResources(this.TXT_long, "TXT_long");
            this.TXT_long.Name = "TXT_long";
            // 
            // TXT_alt
            // 
            resources.ApplyResources(this.TXT_alt, "TXT_alt");
            this.TXT_alt.Name = "TXT_alt";
            // 
            // SaveSettings
            // 
            resources.ApplyResources(this.SaveSettings, "SaveSettings");
            this.SaveSettings.Name = "SaveSettings";
            this.SaveSettings.UseVisualStyleBackColor = true;
            this.SaveSettings.Click += new System.EventHandler(this.SaveSettings_Click);
            // 
            // RAD_softXplanes
            // 
            resources.ApplyResources(this.RAD_softXplanes, "RAD_softXplanes");
            this.RAD_softXplanes.Checked = true;
            this.RAD_softXplanes.Name = "RAD_softXplanes";
            this.RAD_softXplanes.TabStop = true;
            this.RAD_softXplanes.UseVisualStyleBackColor = true;
            this.RAD_softXplanes.CheckedChanged += new System.EventHandler(this.RAD_softXplanes_CheckedChanged);
            // 
            // RAD_softFlightGear
            // 
            resources.ApplyResources(this.RAD_softFlightGear, "RAD_softFlightGear");
            this.RAD_softFlightGear.Name = "RAD_softFlightGear";
            this.RAD_softFlightGear.UseVisualStyleBackColor = true;
            this.RAD_softFlightGear.CheckedChanged += new System.EventHandler(this.RAD_softFlightGear_CheckedChanged);
            // 
            // TXT_servoroll
            // 
            resources.ApplyResources(this.TXT_servoroll, "TXT_servoroll");
            this.TXT_servoroll.Name = "TXT_servoroll";
            // 
            // TXT_servopitch
            // 
            resources.ApplyResources(this.TXT_servopitch, "TXT_servopitch");
            this.TXT_servopitch.Name = "TXT_servopitch";
            // 
            // TXT_servorudder
            // 
            resources.ApplyResources(this.TXT_servorudder, "TXT_servorudder");
            this.TXT_servorudder.Name = "TXT_servorudder";
            // 
            // TXT_servothrottle
            // 
            resources.ApplyResources(this.TXT_servothrottle, "TXT_servothrottle");
            this.TXT_servothrottle.Name = "TXT_servothrottle";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TXT_lat);
            this.panel1.Controls.Add(this.TXT_long);
            this.panel1.Controls.Add(this.TXT_alt);
            this.panel1.Name = "panel1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.label30);
            this.panel2.Controls.Add(this.TXT_yaw);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.TXT_roll);
            this.panel2.Controls.Add(this.TXT_pitch);
            this.panel2.Controls.Add(this.TXT_heading);
            this.panel2.Name = "panel2";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // TXT_yaw
            // 
            resources.ApplyResources(this.TXT_yaw, "TXT_yaw");
            this.TXT_yaw.Name = "TXT_yaw";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.TXT_servoroll);
            this.panel3.Controls.Add(this.TXT_servopitch);
            this.panel3.Controls.Add(this.TXT_servorudder);
            this.panel3.Controls.Add(this.TXT_servothrottle);
            this.panel3.Name = "panel3";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.TXT_control_mode);
            this.panel4.Controls.Add(this.TXT_WP);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.TXT_wpdist);
            this.panel4.Controls.Add(this.TXT_bererror);
            this.panel4.Controls.Add(this.TXT_alterror);
            this.panel4.Name = "panel4";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // TXT_control_mode
            // 
            resources.ApplyResources(this.TXT_control_mode, "TXT_control_mode");
            this.TXT_control_mode.Name = "TXT_control_mode";
            // 
            // TXT_WP
            // 
            resources.ApplyResources(this.TXT_WP, "TXT_WP");
            this.TXT_WP.Name = "TXT_WP";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // panel5
            // 
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Controls.Add(this.ConnectComPort);
            this.panel5.Name = "panel5";
            // 
            // zg1
            // 
            resources.ApplyResources(this.zg1, "zg1");
            this.zg1.Name = "zg1";
            this.zg1.ScrollGrace = 0D;
            this.zg1.ScrollMaxX = 0D;
            this.zg1.ScrollMaxY = 0D;
            this.zg1.ScrollMaxY2 = 0D;
            this.zg1.ScrollMinX = 0D;
            this.zg1.ScrollMinY = 0D;
            this.zg1.ScrollMinY2 = 0D;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel6
            // 
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Controls.Add(this.label28);
            this.panel6.Controls.Add(this.label29);
            this.panel6.Controls.Add(this.label27);
            this.panel6.Controls.Add(this.label25);
            this.panel6.Controls.Add(this.TXT_throttlegain);
            this.panel6.Controls.Add(this.label24);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.TXT_ruddergain);
            this.panel6.Controls.Add(this.TXT_pitchgain);
            this.panel6.Controls.Add(this.TXT_rollgain);
            this.panel6.Name = "panel6";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // TXT_throttlegain
            // 
            resources.ApplyResources(this.TXT_throttlegain, "TXT_throttlegain");
            this.TXT_throttlegain.Name = "TXT_throttlegain";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // TXT_ruddergain
            // 
            resources.ApplyResources(this.TXT_ruddergain, "TXT_ruddergain");
            this.TXT_ruddergain.Name = "TXT_ruddergain";
            // 
            // TXT_pitchgain
            // 
            resources.ApplyResources(this.TXT_pitchgain, "TXT_pitchgain");
            this.TXT_pitchgain.Name = "TXT_pitchgain";
            // 
            // TXT_rollgain
            // 
            resources.ApplyResources(this.TXT_rollgain, "TXT_rollgain");
            this.TXT_rollgain.Name = "TXT_rollgain";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // CHKdisplayall
            // 
            resources.ApplyResources(this.CHKdisplayall, "CHKdisplayall");
            this.CHKdisplayall.Name = "CHKdisplayall";
            this.CHKdisplayall.UseVisualStyleBackColor = true;
            // 
            // CHKgraphroll
            // 
            resources.ApplyResources(this.CHKgraphroll, "CHKgraphroll");
            this.CHKgraphroll.Checked = true;
            this.CHKgraphroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKgraphroll.Name = "CHKgraphroll";
            this.CHKgraphroll.UseVisualStyleBackColor = true;
            // 
            // CHKgraphpitch
            // 
            resources.ApplyResources(this.CHKgraphpitch, "CHKgraphpitch");
            this.CHKgraphpitch.Checked = true;
            this.CHKgraphpitch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKgraphpitch.Name = "CHKgraphpitch";
            this.CHKgraphpitch.UseVisualStyleBackColor = true;
            // 
            // CHKgraphrudder
            // 
            resources.ApplyResources(this.CHKgraphrudder, "CHKgraphrudder");
            this.CHKgraphrudder.Checked = true;
            this.CHKgraphrudder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKgraphrudder.Name = "CHKgraphrudder";
            this.CHKgraphrudder.UseVisualStyleBackColor = true;
            // 
            // CHKgraphthrottle
            // 
            resources.ApplyResources(this.CHKgraphthrottle, "CHKgraphthrottle");
            this.CHKgraphthrottle.Checked = true;
            this.CHKgraphthrottle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKgraphthrottle.Name = "CHKgraphthrottle";
            this.CHKgraphthrottle.UseVisualStyleBackColor = true;
            // 
            // but_advsettings
            // 
            resources.ApplyResources(this.but_advsettings, "but_advsettings");
            this.but_advsettings.Name = "but_advsettings";
            this.but_advsettings.UseVisualStyleBackColor = true;
            this.but_advsettings.Click += new System.EventHandler(this.but_advsettings_Click);
            // 
            // chkSensor
            // 
            resources.ApplyResources(this.chkSensor, "chkSensor");
            this.chkSensor.Name = "chkSensor";
            this.chkSensor.UseVisualStyleBackColor = true;
            // 
            // Simulation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkSensor);
            this.Controls.Add(this.but_advsettings);
            this.Controls.Add(this.CHKgraphthrottle);
            this.Controls.Add(this.CHKgraphrudder);
            this.Controls.Add(this.CHKgraphpitch);
            this.Controls.Add(this.CHKgraphroll);
            this.Controls.Add(this.CHKdisplayall);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.zg1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RAD_softFlightGear);
            this.Controls.Add(this.RAD_softXplanes);
            this.Controls.Add(this.SaveSettings);
            this.Controls.Add(this.OutputLog);
            this.Controls.Add(this.GPSrate);
            this.Controls.Add(this.CHKREV_rudder);
            this.Controls.Add(this.CHKREV_pitch);
            this.Controls.Add(this.CHKREV_roll);
            this.Name = "Simulation";
            this.Load += new System.EventHandler(this.ArdupilotSim_Load);
            ((System.ComponentModel.ISupportInitialize)(this.currentStateBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CHKREV_roll;
        private System.Windows.Forms.CheckBox CHKREV_pitch;
        private System.Windows.Forms.CheckBox CHKREV_rudder;
        private System.Windows.Forms.ComboBox GPSrate;
        private MyButton ConnectComPort;
        private System.Windows.Forms.RichTextBox OutputLog;
        private System.Windows.Forms.Label TXT_roll;
        private System.Windows.Forms.Label TXT_pitch;
        private System.Windows.Forms.Label TXT_heading;
        private System.Windows.Forms.Label TXT_wpdist;
        private System.Windows.Forms.Label TXT_bererror;
        private System.Windows.Forms.Label TXT_alterror;
        private System.Windows.Forms.Label TXT_lat;
        private System.Windows.Forms.Label TXT_long;
        private System.Windows.Forms.Label TXT_alt;
        private MyButton SaveSettings;
        private System.Windows.Forms.RadioButton RAD_softXplanes;
        private System.Windows.Forms.RadioButton RAD_softFlightGear;
        private System.Windows.Forms.Label TXT_servoroll;
        private System.Windows.Forms.Label TXT_servopitch;
        private System.Windows.Forms.Label TXT_servorudder;
        private System.Windows.Forms.Label TXT_servothrottle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label TXT_WP;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label TXT_control_mode;
        private ZedGraph.ZedGraphControl zg1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox TXT_ruddergain;
        private System.Windows.Forms.TextBox TXT_pitchgain;
        private System.Windows.Forms.TextBox TXT_rollgain;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox TXT_throttlegain;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox CHKdisplayall;
        private System.Windows.Forms.CheckBox CHKgraphroll;
        private System.Windows.Forms.CheckBox CHKgraphpitch;
        private System.Windows.Forms.CheckBox CHKgraphrudder;
        private System.Windows.Forms.CheckBox CHKgraphthrottle;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label TXT_yaw;
        private MyButton but_advsettings;
        private System.Windows.Forms.CheckBox chkSensor;
        private System.Windows.Forms.BindingSource currentStateBindingSource;
    }
}
