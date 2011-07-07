namespace ArdupilotMega
{
    partial class JoystickSetup
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
            this.CMB_joysticks = new System.Windows.Forms.ComboBox();
            this.CMB_CH1 = new System.Windows.Forms.ComboBox();
            this.CMB_CH2 = new System.Windows.Forms.ComboBox();
            this.CMB_CH3 = new System.Windows.Forms.ComboBox();
            this.CMB_CH4 = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new ArdupilotMega.HorizontalProgressBar();
            this.progressBar2 = new ArdupilotMega.HorizontalProgressBar();
            this.progressBar3 = new ArdupilotMega.HorizontalProgressBar();
            this.progressBar4 = new ArdupilotMega.HorizontalProgressBar();
            this.expo_ch1 = new System.Windows.Forms.TextBox();
            this.expo_ch2 = new System.Windows.Forms.TextBox();
            this.expo_ch3 = new System.Windows.Forms.TextBox();
            this.expo_ch4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.revCH1 = new System.Windows.Forms.CheckBox();
            this.revCH2 = new System.Windows.Forms.CheckBox();
            this.revCH3 = new System.Windows.Forms.CheckBox();
            this.revCH4 = new System.Windows.Forms.CheckBox();
            this.BUT_save = new System.Windows.Forms.Button();
            this.BUT_enable = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // CMB_joysticks
            // 
            this.CMB_joysticks.FormattingEnabled = true;
            this.CMB_joysticks.Location = new System.Drawing.Point(72, 12);
            this.CMB_joysticks.Name = "CMB_joysticks";
            this.CMB_joysticks.Size = new System.Drawing.Size(202, 21);
            this.CMB_joysticks.TabIndex = 0;
            this.CMB_joysticks.Click += new System.EventHandler(this.CMB_joysticks_Click);
            // 
            // CMB_CH1
            // 
            this.CMB_CH1.FormattingEnabled = true;
            this.CMB_CH1.Items.AddRange(new object[] {
            "RZ",
            "X",
            "Y",
            "SL1"});
            this.CMB_CH1.Location = new System.Drawing.Point(72, 66);
            this.CMB_CH1.Name = "CMB_CH1";
            this.CMB_CH1.Size = new System.Drawing.Size(121, 21);
            this.CMB_CH1.TabIndex = 1;
            this.CMB_CH1.Click += new System.EventHandler(this.CMB_CH1_Click);
            // 
            // CMB_CH2
            // 
            this.CMB_CH2.FormattingEnabled = true;
            this.CMB_CH2.Items.AddRange(new object[] {
            "RZ",
            "X",
            "Y",
            "SL1"});
            this.CMB_CH2.Location = new System.Drawing.Point(72, 93);
            this.CMB_CH2.Name = "CMB_CH2";
            this.CMB_CH2.Size = new System.Drawing.Size(121, 21);
            this.CMB_CH2.TabIndex = 2;
            this.CMB_CH2.Click += new System.EventHandler(this.CMB_CH2_Click);
            // 
            // CMB_CH3
            // 
            this.CMB_CH3.FormattingEnabled = true;
            this.CMB_CH3.Items.AddRange(new object[] {
            "RZ",
            "X",
            "Y",
            "SL1"});
            this.CMB_CH3.Location = new System.Drawing.Point(72, 120);
            this.CMB_CH3.Name = "CMB_CH3";
            this.CMB_CH3.Size = new System.Drawing.Size(121, 21);
            this.CMB_CH3.TabIndex = 3;
            this.CMB_CH3.Click += new System.EventHandler(this.CMB_CH3_Click);
            // 
            // CMB_CH4
            // 
            this.CMB_CH4.FormattingEnabled = true;
            this.CMB_CH4.Items.AddRange(new object[] {
            "RZ",
            "X",
            "Y",
            "SL1"});
            this.CMB_CH4.Location = new System.Drawing.Point(72, 147);
            this.CMB_CH4.Name = "CMB_CH4";
            this.CMB_CH4.Size = new System.Drawing.Size(121, 21);
            this.CMB_CH4.TabIndex = 4;
            this.CMB_CH4.Click += new System.EventHandler(this.CMB_CH4_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Label = null;
            this.progressBar1.Location = new System.Drawing.Point(200, 66);
            this.progressBar1.Maximum = 2200;
            this.progressBar1.Minimum = 800;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Value = 800;
            // 
            // progressBar2
            // 
            this.progressBar2.Label = null;
            this.progressBar2.Location = new System.Drawing.Point(199, 93);
            this.progressBar2.Maximum = 2200;
            this.progressBar2.Minimum = 800;
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(100, 23);
            this.progressBar2.TabIndex = 6;
            this.progressBar2.Value = 800;
            // 
            // progressBar3
            // 
            this.progressBar3.Label = null;
            this.progressBar3.Location = new System.Drawing.Point(199, 120);
            this.progressBar3.Maximum = 2200;
            this.progressBar3.Minimum = 800;
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(100, 23);
            this.progressBar3.TabIndex = 7;
            this.progressBar3.Value = 800;
            // 
            // progressBar4
            // 
            this.progressBar4.Label = null;
            this.progressBar4.Location = new System.Drawing.Point(199, 147);
            this.progressBar4.Maximum = 2200;
            this.progressBar4.Minimum = 800;
            this.progressBar4.Name = "progressBar4";
            this.progressBar4.Size = new System.Drawing.Size(100, 23);
            this.progressBar4.TabIndex = 8;
            this.progressBar4.Value = 800;
            // 
            // expo_ch1
            // 
            this.expo_ch1.Location = new System.Drawing.Point(307, 66);
            this.expo_ch1.Name = "expo_ch1";
            this.expo_ch1.Size = new System.Drawing.Size(100, 20);
            this.expo_ch1.TabIndex = 9;
            this.expo_ch1.Text = "30";
            // 
            // expo_ch2
            // 
            this.expo_ch2.Location = new System.Drawing.Point(307, 93);
            this.expo_ch2.Name = "expo_ch2";
            this.expo_ch2.Size = new System.Drawing.Size(100, 20);
            this.expo_ch2.TabIndex = 10;
            this.expo_ch2.Text = "30";
            // 
            // expo_ch3
            // 
            this.expo_ch3.Enabled = false;
            this.expo_ch3.Location = new System.Drawing.Point(307, 120);
            this.expo_ch3.Name = "expo_ch3";
            this.expo_ch3.Size = new System.Drawing.Size(100, 20);
            this.expo_ch3.TabIndex = 11;
            this.expo_ch3.Text = "0";
            // 
            // expo_ch4
            // 
            this.expo_ch4.Location = new System.Drawing.Point(307, 147);
            this.expo_ch4.Name = "expo_ch4";
            this.expo_ch4.Size = new System.Drawing.Size(100, 20);
            this.expo_ch4.TabIndex = 12;
            this.expo_ch4.Text = "30";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Roll";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Pitch";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Throttle";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Rudder";
            // 
            // revCH1
            // 
            this.revCH1.AutoSize = true;
            this.revCH1.Location = new System.Drawing.Point(414, 66);
            this.revCH1.Name = "revCH1";
            this.revCH1.Size = new System.Drawing.Size(15, 14);
            this.revCH1.TabIndex = 17;
            this.revCH1.UseVisualStyleBackColor = true;
            this.revCH1.CheckedChanged += new System.EventHandler(this.revCH1_CheckedChanged);
            // 
            // revCH2
            // 
            this.revCH2.AutoSize = true;
            this.revCH2.Location = new System.Drawing.Point(413, 93);
            this.revCH2.Name = "revCH2";
            this.revCH2.Size = new System.Drawing.Size(15, 14);
            this.revCH2.TabIndex = 18;
            this.revCH2.UseVisualStyleBackColor = true;
            this.revCH2.CheckedChanged += new System.EventHandler(this.revCH2_CheckedChanged);
            // 
            // revCH3
            // 
            this.revCH3.AutoSize = true;
            this.revCH3.Location = new System.Drawing.Point(413, 120);
            this.revCH3.Name = "revCH3";
            this.revCH3.Size = new System.Drawing.Size(15, 14);
            this.revCH3.TabIndex = 19;
            this.revCH3.UseVisualStyleBackColor = true;
            this.revCH3.CheckedChanged += new System.EventHandler(this.revCH3_CheckedChanged);
            // 
            // revCH4
            // 
            this.revCH4.AutoSize = true;
            this.revCH4.Location = new System.Drawing.Point(413, 147);
            this.revCH4.Name = "revCH4";
            this.revCH4.Size = new System.Drawing.Size(15, 14);
            this.revCH4.TabIndex = 20;
            this.revCH4.UseVisualStyleBackColor = true;
            this.revCH4.CheckedChanged += new System.EventHandler(this.revCH4_CheckedChanged);
            // 
            // BUT_save
            // 
            this.BUT_save.Location = new System.Drawing.Point(383, 173);
            this.BUT_save.Name = "BUT_save";
            this.BUT_save.Size = new System.Drawing.Size(75, 23);
            this.BUT_save.TabIndex = 21;
            this.BUT_save.Text = "Save";
            this.BUT_save.UseVisualStyleBackColor = true;
            this.BUT_save.Click += new System.EventHandler(this.BUT_save_Click);
            // 
            // BUT_enable
            // 
            this.BUT_enable.Location = new System.Drawing.Point(280, 12);
            this.BUT_enable.Name = "BUT_enable";
            this.BUT_enable.Size = new System.Drawing.Size(75, 23);
            this.BUT_enable.TabIndex = 22;
            this.BUT_enable.Text = "Enable";
            this.BUT_enable.UseVisualStyleBackColor = true;
            this.BUT_enable.Click += new System.EventHandler(this.BUT_enable_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Joystick";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(307, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Expo";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(197, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Output";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(69, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Controller Axis";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(411, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Reverse";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // JoystickSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 208);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BUT_enable);
            this.Controls.Add(this.BUT_save);
            this.Controls.Add(this.revCH4);
            this.Controls.Add(this.revCH3);
            this.Controls.Add(this.revCH2);
            this.Controls.Add(this.revCH1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.expo_ch4);
            this.Controls.Add(this.expo_ch3);
            this.Controls.Add(this.expo_ch2);
            this.Controls.Add(this.expo_ch1);
            this.Controls.Add(this.progressBar4);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.CMB_CH4);
            this.Controls.Add(this.CMB_CH3);
            this.Controls.Add(this.CMB_CH2);
            this.Controls.Add(this.CMB_CH1);
            this.Controls.Add(this.CMB_joysticks);
            this.MinimumSize = new System.Drawing.Size(495, 246);
            this.Name = "JoystickSetup";
            this.Text = "Joystick";
            this.Load += new System.EventHandler(this.Joystick_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMB_joysticks;
        private System.Windows.Forms.ComboBox CMB_CH1;
        private System.Windows.Forms.ComboBox CMB_CH2;
        private System.Windows.Forms.ComboBox CMB_CH3;
        private System.Windows.Forms.ComboBox CMB_CH4;
        private HorizontalProgressBar progressBar1;
        private HorizontalProgressBar progressBar2;
        private HorizontalProgressBar progressBar3;
        private HorizontalProgressBar progressBar4;
        private System.Windows.Forms.TextBox expo_ch1;
        private System.Windows.Forms.TextBox expo_ch2;
        private System.Windows.Forms.TextBox expo_ch3;
        private System.Windows.Forms.TextBox expo_ch4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox revCH1;
        private System.Windows.Forms.CheckBox revCH2;
        private System.Windows.Forms.CheckBox revCH3;
        private System.Windows.Forms.CheckBox revCH4;
        private System.Windows.Forms.Button BUT_save;
        private System.Windows.Forms.Button BUT_enable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Timer timer1;
    }
}