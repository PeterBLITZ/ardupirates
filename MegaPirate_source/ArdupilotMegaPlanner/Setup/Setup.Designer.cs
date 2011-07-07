namespace ArdupilotMega.Setup
{
    partial class Setup
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BUT_Calibrateradio = new ArdupilotMega.MyButton();
            this.BAR8 = new ArdupilotMega.HorizontalProgressBar();
            this.currentStateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BAR7 = new ArdupilotMega.HorizontalProgressBar();
            this.BAR6 = new ArdupilotMega.HorizontalProgressBar();
            this.BAR5 = new ArdupilotMega.HorizontalProgressBar();
            this.BARpitch = new ArdupilotMega.VerticalProgressBar();
            this.BARthrottle = new ArdupilotMega.VerticalProgressBar();
            this.BARyaw = new ArdupilotMega.HorizontalProgressBar();
            this.BARroll = new ArdupilotMega.HorizontalProgressBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentStateBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(650, 395);
            this.tabControl1.TabIndex = 93;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.BUT_Calibrateradio);
            this.tabPage1.Controls.Add(this.BAR8);
            this.tabPage1.Controls.Add(this.BAR7);
            this.tabPage1.Controls.Add(this.BAR6);
            this.tabPage1.Controls.Add(this.BAR5);
            this.tabPage1.Controls.Add(this.BARpitch);
            this.tabPage1.Controls.Add(this.BARthrottle);
            this.tabPage1.Controls.Add(this.BARyaw);
            this.tabPage1.Controls.Add(this.BARroll);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(642, 369);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Radio";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // BUT_Calibrateradio
            // 
            this.BUT_Calibrateradio.Location = new System.Drawing.Point(482, 340);
            this.BUT_Calibrateradio.Name = "BUT_Calibrateradio";
            this.BUT_Calibrateradio.Size = new System.Drawing.Size(134, 23);
            this.BUT_Calibrateradio.TabIndex = 102;
            this.BUT_Calibrateradio.Text = "Calibrate Radio";
            this.BUT_Calibrateradio.UseVisualStyleBackColor = true;
            this.BUT_Calibrateradio.Click += new System.EventHandler(this.BUT_Calibrateradio_Click);
            // 
            // BAR8
            // 
            this.BAR8.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch8in", true));
            this.BAR8.Label = "Radio 8";
            this.BAR8.Location = new System.Drawing.Point(446, 240);
            this.BAR8.MarqueeAnimationSpeed = 1;
            this.BAR8.Maximum = 2200;
            this.BAR8.maxline = 0;
            this.BAR8.Minimum = 800;
            this.BAR8.minline = 0;
            this.BAR8.Name = "BAR8";
            this.BAR8.Size = new System.Drawing.Size(170, 25);
            this.BAR8.Step = 1;
            this.BAR8.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BAR8.TabIndex = 101;
            this.BAR8.Value = 1500;
            // 
            // currentStateBindingSource
            // 
            this.currentStateBindingSource.DataSource = typeof(ArdupilotMega.CurrentState);
            // 
            // BAR7
            // 
            this.BAR7.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch7in", true));
            this.BAR7.Label = "Radio 7";
            this.BAR7.Location = new System.Drawing.Point(446, 185);
            this.BAR7.MarqueeAnimationSpeed = 1;
            this.BAR7.Maximum = 2200;
            this.BAR7.maxline = 0;
            this.BAR7.Minimum = 800;
            this.BAR7.minline = 0;
            this.BAR7.Name = "BAR7";
            this.BAR7.Size = new System.Drawing.Size(170, 25);
            this.BAR7.Step = 1;
            this.BAR7.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BAR7.TabIndex = 100;
            this.BAR7.Value = 1500;
            // 
            // BAR6
            // 
            this.BAR6.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch6in", true));
            this.BAR6.Label = "Radio 6";
            this.BAR6.Location = new System.Drawing.Point(446, 130);
            this.BAR6.MarqueeAnimationSpeed = 1;
            this.BAR6.Maximum = 2200;
            this.BAR6.maxline = 0;
            this.BAR6.Minimum = 800;
            this.BAR6.minline = 0;
            this.BAR6.Name = "BAR6";
            this.BAR6.Size = new System.Drawing.Size(170, 25);
            this.BAR6.Step = 1;
            this.BAR6.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BAR6.TabIndex = 99;
            this.BAR6.Value = 1500;
            // 
            // BAR5
            // 
            this.BAR5.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch5in", true));
            this.BAR5.Label = "Radio 5";
            this.BAR5.Location = new System.Drawing.Point(446, 75);
            this.BAR5.MarqueeAnimationSpeed = 1;
            this.BAR5.Maximum = 2200;
            this.BAR5.maxline = 0;
            this.BAR5.Minimum = 800;
            this.BAR5.minline = 0;
            this.BAR5.Name = "BAR5";
            this.BAR5.Size = new System.Drawing.Size(170, 25);
            this.BAR5.Step = 1;
            this.BAR5.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BAR5.TabIndex = 98;
            this.BAR5.Value = 1500;
            // 
            // BARpitch
            // 
            this.BARpitch.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch2in", true));
            this.BARpitch.Label = "Pitch";
            this.BARpitch.Location = new System.Drawing.Point(143, 61);
            this.BARpitch.Maximum = 2200;
            this.BARpitch.maxline = 0;
            this.BARpitch.Minimum = 800;
            this.BARpitch.minline = 0;
            this.BARpitch.Name = "BARpitch";
            this.BARpitch.Size = new System.Drawing.Size(42, 211);
            this.BARpitch.TabIndex = 96;
            this.BARpitch.Value = 1500;
            // 
            // BARthrottle
            // 
            this.BARthrottle.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch3in", true));
            this.BARthrottle.Label = "Throttle";
            this.BARthrottle.Location = new System.Drawing.Point(359, 61);
            this.BARthrottle.Maximum = 2200;
            this.BARthrottle.maxline = 0;
            this.BARthrottle.Minimum = 800;
            this.BARthrottle.minline = 0;
            this.BARthrottle.Name = "BARthrottle";
            this.BARthrottle.Size = new System.Drawing.Size(42, 211);
            this.BARthrottle.TabIndex = 95;
            this.BARthrottle.Value = 1500;
            // 
            // BARyaw
            // 
            this.BARyaw.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch4in", true));
            this.BARyaw.Label = "Yaw";
            this.BARyaw.Location = new System.Drawing.Point(21, 304);
            this.BARyaw.Maximum = 2200;
            this.BARyaw.maxline = 0;
            this.BARyaw.Minimum = 800;
            this.BARyaw.minline = 0;
            this.BARyaw.Name = "BARyaw";
            this.BARyaw.Size = new System.Drawing.Size(288, 23);
            this.BARyaw.TabIndex = 94;
            this.BARyaw.Value = 1500;
            // 
            // BARroll
            // 
            this.BARroll.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch1in", true));
            this.BARroll.Label = "Roll";
            this.BARroll.Location = new System.Drawing.Point(21, 6);
            this.BARroll.Maximum = 2200;
            this.BARroll.maxline = 0;
            this.BARroll.Minimum = 800;
            this.BARroll.minline = 0;
            this.BARroll.Name = "BARroll";
            this.BARroll.Size = new System.Drawing.Size(288, 23);
            this.BARroll.TabIndex = 93;
            this.BARroll.Value = 1500;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(642, 369);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Hardware";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(642, 369);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ArduCopter2";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 419);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Setup";
            this.Text = "RadioSetup";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentStateBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private HorizontalProgressBar BAR8;
        private HorizontalProgressBar BAR7;
        private HorizontalProgressBar BAR6;
        private HorizontalProgressBar BAR5;
        private VerticalProgressBar BARpitch;
        private VerticalProgressBar BARthrottle;
        private HorizontalProgressBar BARyaw;
        private HorizontalProgressBar BARroll;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private MyButton BUT_Calibrateradio;
        private System.Windows.Forms.BindingSource currentStateBindingSource;

    }
}