namespace ArdupilotMega
{
    partial class AC2_Sensor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AC2_Sensor));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.currentStateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timer2serial = new System.Windows.Forms.Timer(this.components);
            this.tabRadio = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.horizontalProgressBar9 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar10 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar11 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar12 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar13 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar14 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar15 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar16 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar8 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar7 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar6 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar5 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar4 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar3 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar2 = new ArdupilotMega.HorizontalProgressBar();
            this.horizontalProgressBar1 = new ArdupilotMega.HorizontalProgressBar();
            this.tabRawSensor = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.CMB_rawupdaterate = new System.Windows.Forms.ComboBox();
            this.aGauge1 = new AGaugeApp.AGauge();
            this.chkax = new System.Windows.Forms.CheckBox();
            this.chkay = new System.Windows.Forms.CheckBox();
            this.chkaz = new System.Windows.Forms.CheckBox();
            this.chkgx = new System.Windows.Forms.CheckBox();
            this.chkgy = new System.Windows.Forms.CheckBox();
            this.chkgz = new System.Windows.Forms.CheckBox();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.Gpitch = new AGaugeApp.AGauge();
            this.Groll = new AGaugeApp.AGauge();
            this.tabOrientation = new System.Windows.Forms.TabPage();
            this.horizontalProgressBar17 = new ArdupilotMega.VerticalProgressBar();
            this.verticalProgressBar7 = new ArdupilotMega.VerticalProgressBar();
            this.verticalProgressBar6 = new ArdupilotMega.VerticalProgressBar();
            this.verticalProgressBar5 = new ArdupilotMega.VerticalProgressBar();
            this.verticalProgressBar4 = new ArdupilotMega.VerticalProgressBar();
            this.progressBar2 = new ArdupilotMega.VerticalProgressBar();
            this.progressBar1 = new ArdupilotMega.VerticalProgressBar();
            this.verticalProgressBar3 = new ArdupilotMega.VerticalProgressBar();
            this.verticalProgressBar2 = new ArdupilotMega.VerticalProgressBar();
            this.verticalProgressBar1 = new ArdupilotMega.VerticalProgressBar();
            this.tabControl = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.currentStateBindingSource)).BeginInit();
            this.tabRadio.SuspendLayout();
            this.tabRawSensor.SuspendLayout();
            this.tabOrientation.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // currentStateBindingSource
            // 
            this.currentStateBindingSource.DataSource = typeof(ArdupilotMega.CurrentState);
            // 
            // timer2serial
            // 
            this.timer2serial.Enabled = true;
            this.timer2serial.Interval = 10;
            this.timer2serial.Tick += new System.EventHandler(this.timer2serial_Tick);
            // 
            // tabRadio
            // 
            this.tabRadio.Controls.Add(this.label2);
            this.tabRadio.Controls.Add(this.label1);
            this.tabRadio.Controls.Add(this.horizontalProgressBar9);
            this.tabRadio.Controls.Add(this.horizontalProgressBar10);
            this.tabRadio.Controls.Add(this.horizontalProgressBar11);
            this.tabRadio.Controls.Add(this.horizontalProgressBar12);
            this.tabRadio.Controls.Add(this.horizontalProgressBar13);
            this.tabRadio.Controls.Add(this.horizontalProgressBar14);
            this.tabRadio.Controls.Add(this.horizontalProgressBar15);
            this.tabRadio.Controls.Add(this.horizontalProgressBar16);
            this.tabRadio.Controls.Add(this.horizontalProgressBar8);
            this.tabRadio.Controls.Add(this.horizontalProgressBar7);
            this.tabRadio.Controls.Add(this.horizontalProgressBar6);
            this.tabRadio.Controls.Add(this.horizontalProgressBar5);
            this.tabRadio.Controls.Add(this.horizontalProgressBar4);
            this.tabRadio.Controls.Add(this.horizontalProgressBar3);
            this.tabRadio.Controls.Add(this.horizontalProgressBar2);
            this.tabRadio.Controls.Add(this.horizontalProgressBar1);
            this.tabRadio.Location = new System.Drawing.Point(4, 22);
            this.tabRadio.Name = "tabRadio";
            this.tabRadio.Size = new System.Drawing.Size(751, 478);
            this.tabRadio.TabIndex = 2;
            this.tabRadio.Text = "Radio";
            this.tabRadio.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(469, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "Servo/Motor OUT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(211, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "Radio IN";
            // 
            // horizontalProgressBar9
            // 
            this.horizontalProgressBar9.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch8out", true));
            this.horizontalProgressBar9.Label = "Radio 8";
            this.horizontalProgressBar9.Location = new System.Drawing.Point(424, 415);
            this.horizontalProgressBar9.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar9.Maximum = 2000;
            this.horizontalProgressBar9.Minimum = 1000;
            this.horizontalProgressBar9.Name = "horizontalProgressBar9";
            this.horizontalProgressBar9.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar9.Step = 1;
            this.horizontalProgressBar9.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar9.TabIndex = 84;
            // 
            // horizontalProgressBar10
            // 
            this.horizontalProgressBar10.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch7out", true));
            this.horizontalProgressBar10.Label = "Radio 7";
            this.horizontalProgressBar10.Location = new System.Drawing.Point(424, 360);
            this.horizontalProgressBar10.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar10.Maximum = 2000;
            this.horizontalProgressBar10.Minimum = 1000;
            this.horizontalProgressBar10.Name = "horizontalProgressBar10";
            this.horizontalProgressBar10.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar10.Step = 1;
            this.horizontalProgressBar10.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar10.TabIndex = 83;
            // 
            // horizontalProgressBar11
            // 
            this.horizontalProgressBar11.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch6out", true));
            this.horizontalProgressBar11.Label = "Radio 6";
            this.horizontalProgressBar11.Location = new System.Drawing.Point(424, 305);
            this.horizontalProgressBar11.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar11.Maximum = 2000;
            this.horizontalProgressBar11.Minimum = 1000;
            this.horizontalProgressBar11.Name = "horizontalProgressBar11";
            this.horizontalProgressBar11.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar11.Step = 1;
            this.horizontalProgressBar11.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar11.TabIndex = 82;
            // 
            // horizontalProgressBar12
            // 
            this.horizontalProgressBar12.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch5out", true));
            this.horizontalProgressBar12.Label = "Radio 5";
            this.horizontalProgressBar12.Location = new System.Drawing.Point(424, 250);
            this.horizontalProgressBar12.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar12.Maximum = 2000;
            this.horizontalProgressBar12.Minimum = 1000;
            this.horizontalProgressBar12.Name = "horizontalProgressBar12";
            this.horizontalProgressBar12.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar12.Step = 1;
            this.horizontalProgressBar12.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar12.TabIndex = 81;
            // 
            // horizontalProgressBar13
            // 
            this.horizontalProgressBar13.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch4out", true));
            this.horizontalProgressBar13.Label = "Radio 4";
            this.horizontalProgressBar13.Location = new System.Drawing.Point(424, 195);
            this.horizontalProgressBar13.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar13.Maximum = 2000;
            this.horizontalProgressBar13.Minimum = 1000;
            this.horizontalProgressBar13.Name = "horizontalProgressBar13";
            this.horizontalProgressBar13.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar13.Step = 1;
            this.horizontalProgressBar13.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar13.TabIndex = 80;
            // 
            // horizontalProgressBar14
            // 
            this.horizontalProgressBar14.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch3out", true));
            this.horizontalProgressBar14.Label = "Radio 3";
            this.horizontalProgressBar14.Location = new System.Drawing.Point(424, 140);
            this.horizontalProgressBar14.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar14.Maximum = 2000;
            this.horizontalProgressBar14.Minimum = 1000;
            this.horizontalProgressBar14.Name = "horizontalProgressBar14";
            this.horizontalProgressBar14.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar14.Step = 1;
            this.horizontalProgressBar14.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar14.TabIndex = 79;
            // 
            // horizontalProgressBar15
            // 
            this.horizontalProgressBar15.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch1out", true));
            this.horizontalProgressBar15.Label = "Radio 1";
            this.horizontalProgressBar15.Location = new System.Drawing.Point(424, 30);
            this.horizontalProgressBar15.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar15.Maximum = 2000;
            this.horizontalProgressBar15.Minimum = 1000;
            this.horizontalProgressBar15.Name = "horizontalProgressBar15";
            this.horizontalProgressBar15.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar15.Step = 1;
            this.horizontalProgressBar15.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar15.TabIndex = 78;
            // 
            // horizontalProgressBar16
            // 
            this.horizontalProgressBar16.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch2out", true));
            this.horizontalProgressBar16.Label = "Radio 2";
            this.horizontalProgressBar16.Location = new System.Drawing.Point(424, 85);
            this.horizontalProgressBar16.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar16.Maximum = 2000;
            this.horizontalProgressBar16.Minimum = 1000;
            this.horizontalProgressBar16.Name = "horizontalProgressBar16";
            this.horizontalProgressBar16.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar16.Step = 1;
            this.horizontalProgressBar16.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar16.TabIndex = 77;
            // 
            // horizontalProgressBar8
            // 
            this.horizontalProgressBar8.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch8in", true));
            this.horizontalProgressBar8.Label = "Radio 8";
            this.horizontalProgressBar8.Location = new System.Drawing.Point(142, 415);
            this.horizontalProgressBar8.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar8.Maximum = 2000;
            this.horizontalProgressBar8.Minimum = 1000;
            this.horizontalProgressBar8.Name = "horizontalProgressBar8";
            this.horizontalProgressBar8.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar8.Step = 1;
            this.horizontalProgressBar8.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar8.TabIndex = 60;
            // 
            // horizontalProgressBar7
            // 
            this.horizontalProgressBar7.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch7in", true));
            this.horizontalProgressBar7.Label = "Radio 7";
            this.horizontalProgressBar7.Location = new System.Drawing.Point(142, 360);
            this.horizontalProgressBar7.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar7.Maximum = 2000;
            this.horizontalProgressBar7.Minimum = 1000;
            this.horizontalProgressBar7.Name = "horizontalProgressBar7";
            this.horizontalProgressBar7.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar7.Step = 1;
            this.horizontalProgressBar7.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar7.TabIndex = 59;
            // 
            // horizontalProgressBar6
            // 
            this.horizontalProgressBar6.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch6in", true));
            this.horizontalProgressBar6.Label = "Radio 6";
            this.horizontalProgressBar6.Location = new System.Drawing.Point(142, 305);
            this.horizontalProgressBar6.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar6.Maximum = 2000;
            this.horizontalProgressBar6.Minimum = 1000;
            this.horizontalProgressBar6.Name = "horizontalProgressBar6";
            this.horizontalProgressBar6.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar6.Step = 1;
            this.horizontalProgressBar6.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar6.TabIndex = 58;
            // 
            // horizontalProgressBar5
            // 
            this.horizontalProgressBar5.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch5in", true));
            this.horizontalProgressBar5.Label = "Radio 5";
            this.horizontalProgressBar5.Location = new System.Drawing.Point(142, 250);
            this.horizontalProgressBar5.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar5.Maximum = 2000;
            this.horizontalProgressBar5.Minimum = 1000;
            this.horizontalProgressBar5.Name = "horizontalProgressBar5";
            this.horizontalProgressBar5.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar5.Step = 1;
            this.horizontalProgressBar5.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar5.TabIndex = 57;
            // 
            // horizontalProgressBar4
            // 
            this.horizontalProgressBar4.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch4in", true));
            this.horizontalProgressBar4.Label = "Radio 4";
            this.horizontalProgressBar4.Location = new System.Drawing.Point(142, 195);
            this.horizontalProgressBar4.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar4.Maximum = 2000;
            this.horizontalProgressBar4.Minimum = 1000;
            this.horizontalProgressBar4.Name = "horizontalProgressBar4";
            this.horizontalProgressBar4.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar4.Step = 1;
            this.horizontalProgressBar4.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar4.TabIndex = 56;
            // 
            // horizontalProgressBar3
            // 
            this.horizontalProgressBar3.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch3in", true));
            this.horizontalProgressBar3.Label = "Radio 3";
            this.horizontalProgressBar3.Location = new System.Drawing.Point(142, 140);
            this.horizontalProgressBar3.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar3.Maximum = 2000;
            this.horizontalProgressBar3.Minimum = 1000;
            this.horizontalProgressBar3.Name = "horizontalProgressBar3";
            this.horizontalProgressBar3.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar3.Step = 1;
            this.horizontalProgressBar3.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar3.TabIndex = 55;
            // 
            // horizontalProgressBar2
            // 
            this.horizontalProgressBar2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch1in", true));
            this.horizontalProgressBar2.Label = "Radio 1";
            this.horizontalProgressBar2.Location = new System.Drawing.Point(142, 30);
            this.horizontalProgressBar2.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar2.Maximum = 2000;
            this.horizontalProgressBar2.Minimum = 1000;
            this.horizontalProgressBar2.Name = "horizontalProgressBar2";
            this.horizontalProgressBar2.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar2.Step = 1;
            this.horizontalProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar2.TabIndex = 40;
            // 
            // horizontalProgressBar1
            // 
            this.horizontalProgressBar1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch2in", true));
            this.horizontalProgressBar1.Label = "Radio 2";
            this.horizontalProgressBar1.Location = new System.Drawing.Point(142, 85);
            this.horizontalProgressBar1.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar1.Maximum = 2000;
            this.horizontalProgressBar1.Minimum = 1000;
            this.horizontalProgressBar1.Name = "horizontalProgressBar1";
            this.horizontalProgressBar1.Size = new System.Drawing.Size(170, 25);
            this.horizontalProgressBar1.Step = 1;
            this.horizontalProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar1.TabIndex = 39;
            // 
            // tabRawSensor
            // 
            this.tabRawSensor.Controls.Add(this.label3);
            this.tabRawSensor.Controls.Add(this.CMB_rawupdaterate);
            this.tabRawSensor.Controls.Add(this.aGauge1);
            this.tabRawSensor.Controls.Add(this.chkax);
            this.tabRawSensor.Controls.Add(this.chkay);
            this.tabRawSensor.Controls.Add(this.chkaz);
            this.tabRawSensor.Controls.Add(this.chkgx);
            this.tabRawSensor.Controls.Add(this.chkgy);
            this.tabRawSensor.Controls.Add(this.chkgz);
            this.tabRawSensor.Controls.Add(this.zg1);
            this.tabRawSensor.Controls.Add(this.Gpitch);
            this.tabRawSensor.Controls.Add(this.Groll);
            this.tabRawSensor.Location = new System.Drawing.Point(4, 22);
            this.tabRawSensor.Name = "tabRawSensor";
            this.tabRawSensor.Padding = new System.Windows.Forms.Padding(3);
            this.tabRawSensor.Size = new System.Drawing.Size(751, 478);
            this.tabRawSensor.TabIndex = 1;
            this.tabRawSensor.Text = "Raw Sensor";
            this.tabRawSensor.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 39);
            this.label3.TabIndex = 44;
            this.label3.Text = "Note: There is a delay \r\nwhen viewing via Xbee\r\n@ 50hz";
            // 
            // CMB_rawupdaterate
            // 
            this.CMB_rawupdaterate.FormattingEnabled = true;
            this.CMB_rawupdaterate.Items.AddRange(new object[] {
            "3",
            "10",
            "50"});
            this.CMB_rawupdaterate.Location = new System.Drawing.Point(651, 154);
            this.CMB_rawupdaterate.Name = "CMB_rawupdaterate";
            this.CMB_rawupdaterate.Size = new System.Drawing.Size(94, 21);
            this.CMB_rawupdaterate.TabIndex = 43;
            this.CMB_rawupdaterate.Text = "Update Speed";
            this.CMB_rawupdaterate.SelectedIndexChanged += new System.EventHandler(this.CMB_rawupdaterate_SelectedIndexChanged);
            // 
            // aGauge1
            // 
            this.aGauge1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.aGauge1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("aGauge1.BackgroundImage")));
            this.aGauge1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.aGauge1.BaseArcColor = System.Drawing.Color.Gray;
            this.aGauge1.BaseArcRadius = 50;
            this.aGauge1.BaseArcStart = 270;
            this.aGauge1.BaseArcSweep = 360;
            this.aGauge1.BaseArcWidth = 2;
            this.aGauge1.basesize = new System.Drawing.Size(170, 170);
            this.aGauge1.Cap_Idx = ((byte)(2));
            this.aGauge1.CapColor = System.Drawing.Color.White;
            this.aGauge1.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.aGauge1.CapPosition = new System.Drawing.Point(10, 10);
            this.aGauge1.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.aGauge1.CapsText = new string[] {
        "Yaw",
        "",
        "",
        "",
        ""};
            this.aGauge1.CapText = "";
            this.aGauge1.Center = new System.Drawing.Point(85, 85);
            this.aGauge1.DataBindings.Add(new System.Windows.Forms.Binding("Value0", this.currentStateBindingSource, "yaw", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.aGauge1.Location = new System.Drawing.Point(446, 9);
            this.aGauge1.MaxValue = 359F;
            this.aGauge1.MinValue = 0F;
            this.aGauge1.Name = "aGauge1";
            this.aGauge1.Need_Idx = ((byte)(3));
            this.aGauge1.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.aGauge1.NeedleColor2 = System.Drawing.Color.DimGray;
            this.aGauge1.NeedleEnabled = false;
            this.aGauge1.NeedleRadius = 80;
            this.aGauge1.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.aGauge1.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray};
            this.aGauge1.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.aGauge1.NeedlesRadius = new int[] {
        50,
        80,
        80,
        80};
            this.aGauge1.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.aGauge1.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.aGauge1.NeedleType = 0;
            this.aGauge1.NeedleWidth = 2;
            this.aGauge1.Range_Idx = ((byte)(0));
            this.aGauge1.RangeColor = System.Drawing.Color.LightGreen;
            this.aGauge1.RangeEnabled = true;
            this.aGauge1.RangeEndValue = 360F;
            this.aGauge1.RangeInnerRadius = 50;
            this.aGauge1.RangeOuterRadius = 60;
            this.aGauge1.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.DimGray,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.aGauge1.RangesEnabled = new bool[] {
        true,
        false,
        false,
        false,
        false};
            this.aGauge1.RangesEndValue = new float[] {
        360F,
        180F,
        0F,
        0F,
        0F};
            this.aGauge1.RangesInnerRadius = new int[] {
        50,
        45,
        50,
        70,
        70};
            this.aGauge1.RangesOuterRadius = new int[] {
        60,
        50,
        60,
        80,
        80};
            this.aGauge1.RangesStartValue = new float[] {
        0F,
        -180F,
        0F,
        0F,
        0F};
            this.aGauge1.RangeStartValue = 0F;
            this.aGauge1.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.aGauge1.ScaleLinesInterInnerRadius = 60;
            this.aGauge1.ScaleLinesInterOuterRadius = 50;
            this.aGauge1.ScaleLinesInterWidth = 1;
            this.aGauge1.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.aGauge1.ScaleLinesMajorInnerRadius = 50;
            this.aGauge1.ScaleLinesMajorOuterRadius = 60;
            this.aGauge1.ScaleLinesMajorStepValue = 45F;
            this.aGauge1.ScaleLinesMajorWidth = 2;
            this.aGauge1.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.aGauge1.ScaleLinesMinorInnerRadius = 50;
            this.aGauge1.ScaleLinesMinorNumOf = 2;
            this.aGauge1.ScaleLinesMinorOuterRadius = 55;
            this.aGauge1.ScaleLinesMinorWidth = 1;
            this.aGauge1.ScaleNumbersColor = System.Drawing.Color.White;
            this.aGauge1.ScaleNumbersFormat = null;
            this.aGauge1.ScaleNumbersRadius = 38;
            this.aGauge1.ScaleNumbersRotation = 0;
            this.aGauge1.ScaleNumbersStartScaleLine = 1;
            this.aGauge1.ScaleNumbersStepScaleLines = 1;
            this.aGauge1.Size = new System.Drawing.Size(170, 170);
            this.aGauge1.TabIndex = 42;
            this.aGauge1.Value = 0F;
            this.aGauge1.Value0 = 0F;
            this.aGauge1.Value1 = 0F;
            this.aGauge1.Value2 = 0F;
            this.aGauge1.Value3 = 0F;
            // 
            // chkax
            // 
            this.chkax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkax.AutoSize = true;
            this.chkax.Checked = true;
            this.chkax.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkax.Location = new System.Drawing.Point(685, 16);
            this.chkax.Name = "chkax";
            this.chkax.Size = new System.Drawing.Size(63, 17);
            this.chkax.TabIndex = 41;
            this.chkax.Text = "Accel X";
            this.chkax.UseVisualStyleBackColor = true;
            // 
            // chkay
            // 
            this.chkay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkay.AutoSize = true;
            this.chkay.Checked = true;
            this.chkay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkay.Location = new System.Drawing.Point(685, 39);
            this.chkay.Name = "chkay";
            this.chkay.Size = new System.Drawing.Size(63, 17);
            this.chkay.TabIndex = 40;
            this.chkay.Text = "Accel Y";
            this.chkay.UseVisualStyleBackColor = true;
            // 
            // chkaz
            // 
            this.chkaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkaz.AutoSize = true;
            this.chkaz.Checked = true;
            this.chkaz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkaz.Location = new System.Drawing.Point(685, 62);
            this.chkaz.Name = "chkaz";
            this.chkaz.Size = new System.Drawing.Size(63, 17);
            this.chkaz.TabIndex = 39;
            this.chkaz.Text = "Accel Z";
            this.chkaz.UseVisualStyleBackColor = true;
            // 
            // chkgx
            // 
            this.chkgx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkgx.AutoSize = true;
            this.chkgx.Checked = true;
            this.chkgx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkgx.Location = new System.Drawing.Point(685, 85);
            this.chkgx.Name = "chkgx";
            this.chkgx.Size = new System.Drawing.Size(58, 17);
            this.chkgx.TabIndex = 38;
            this.chkgx.Text = "Gyro X";
            this.chkgx.UseVisualStyleBackColor = true;
            // 
            // chkgy
            // 
            this.chkgy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkgy.AutoSize = true;
            this.chkgy.Checked = true;
            this.chkgy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkgy.Location = new System.Drawing.Point(685, 108);
            this.chkgy.Name = "chkgy";
            this.chkgy.Size = new System.Drawing.Size(58, 17);
            this.chkgy.TabIndex = 37;
            this.chkgy.Text = "Gyro Y";
            this.chkgy.UseVisualStyleBackColor = true;
            // 
            // chkgz
            // 
            this.chkgz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkgz.AutoSize = true;
            this.chkgz.Checked = true;
            this.chkgz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkgz.Location = new System.Drawing.Point(685, 131);
            this.chkgz.Name = "chkgz";
            this.chkgz.Size = new System.Drawing.Size(58, 17);
            this.chkgz.TabIndex = 36;
            this.chkgz.Text = "Gyro Z";
            this.chkgz.UseVisualStyleBackColor = true;
            // 
            // zg1
            // 
            this.zg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zg1.BackColor = System.Drawing.Color.Transparent;
            this.zg1.Location = new System.Drawing.Point(6, 185);
            this.zg1.Name = "zg1";
            this.zg1.ScrollGrace = 0D;
            this.zg1.ScrollMaxX = 0D;
            this.zg1.ScrollMaxY = 0D;
            this.zg1.ScrollMaxY2 = 0D;
            this.zg1.ScrollMinX = 0D;
            this.zg1.ScrollMinY = 0D;
            this.zg1.ScrollMinY2 = 0D;
            this.zg1.Size = new System.Drawing.Size(742, 290);
            this.zg1.TabIndex = 35;
            // 
            // Gpitch
            // 
            this.Gpitch.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Gpitch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Gpitch.BackgroundImage")));
            this.Gpitch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Gpitch.BaseArcColor = System.Drawing.Color.Gray;
            this.Gpitch.BaseArcRadius = 50;
            this.Gpitch.BaseArcStart = 90;
            this.Gpitch.BaseArcSweep = 360;
            this.Gpitch.BaseArcWidth = 2;
            this.Gpitch.basesize = new System.Drawing.Size(170, 170);
            this.Gpitch.Cap_Idx = ((byte)(2));
            this.Gpitch.CapColor = System.Drawing.Color.White;
            this.Gpitch.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.Gpitch.CapPosition = new System.Drawing.Point(10, 10);
            this.Gpitch.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.Gpitch.CapsText = new string[] {
        "Pitch",
        "",
        "",
        "",
        ""};
            this.Gpitch.CapText = "";
            this.Gpitch.Center = new System.Drawing.Point(85, 85);
            this.Gpitch.DataBindings.Add(new System.Windows.Forms.Binding("Value0", this.currentStateBindingSource, "pitch", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.Gpitch.Location = new System.Drawing.Point(270, 9);
            this.Gpitch.MaxValue = 89F;
            this.Gpitch.MinValue = -90F;
            this.Gpitch.Name = "Gpitch";
            this.Gpitch.Need_Idx = ((byte)(3));
            this.Gpitch.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.Gpitch.NeedleColor2 = System.Drawing.Color.DimGray;
            this.Gpitch.NeedleEnabled = false;
            this.Gpitch.NeedleRadius = 80;
            this.Gpitch.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.Gpitch.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray};
            this.Gpitch.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.Gpitch.NeedlesRadius = new int[] {
        50,
        80,
        80,
        80};
            this.Gpitch.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.Gpitch.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.Gpitch.NeedleType = 0;
            this.Gpitch.NeedleWidth = 2;
            this.Gpitch.Range_Idx = ((byte)(2));
            this.Gpitch.RangeColor = System.Drawing.Color.LightGreen;
            this.Gpitch.RangeEnabled = true;
            this.Gpitch.RangeEndValue = -90F;
            this.Gpitch.RangeInnerRadius = 50;
            this.Gpitch.RangeOuterRadius = 60;
            this.Gpitch.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightSteelBlue,
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.LightGreen,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.Gpitch.RangesEnabled = new bool[] {
        true,
        true,
        true,
        false,
        false};
            this.Gpitch.RangesEndValue = new float[] {
        90F,
        180F,
        -90F,
        0F,
        0F};
            this.Gpitch.RangesInnerRadius = new int[] {
        50,
        50,
        50,
        70,
        70};
            this.Gpitch.RangesOuterRadius = new int[] {
        60,
        60,
        60,
        80,
        80};
            this.Gpitch.RangesStartValue = new float[] {
        -90F,
        90F,
        -180F,
        0F,
        0F};
            this.Gpitch.RangeStartValue = -180F;
            this.Gpitch.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.Gpitch.ScaleLinesInterInnerRadius = 60;
            this.Gpitch.ScaleLinesInterOuterRadius = 50;
            this.Gpitch.ScaleLinesInterWidth = 1;
            this.Gpitch.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.Gpitch.ScaleLinesMajorInnerRadius = 50;
            this.Gpitch.ScaleLinesMajorOuterRadius = 60;
            this.Gpitch.ScaleLinesMajorStepValue = 20F;
            this.Gpitch.ScaleLinesMajorWidth = 2;
            this.Gpitch.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.Gpitch.ScaleLinesMinorInnerRadius = 50;
            this.Gpitch.ScaleLinesMinorNumOf = 9;
            this.Gpitch.ScaleLinesMinorOuterRadius = 55;
            this.Gpitch.ScaleLinesMinorWidth = 1;
            this.Gpitch.ScaleNumbersColor = System.Drawing.Color.White;
            this.Gpitch.ScaleNumbersFormat = null;
            this.Gpitch.ScaleNumbersRadius = 38;
            this.Gpitch.ScaleNumbersRotation = 0;
            this.Gpitch.ScaleNumbersStartScaleLine = 1;
            this.Gpitch.ScaleNumbersStepScaleLines = 1;
            this.Gpitch.Size = new System.Drawing.Size(170, 170);
            this.Gpitch.TabIndex = 34;
            this.Gpitch.Value = 0F;
            this.Gpitch.Value0 = 0F;
            this.Gpitch.Value1 = 0F;
            this.Gpitch.Value2 = 0F;
            this.Gpitch.Value3 = 0F;
            // 
            // Groll
            // 
            this.Groll.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Groll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Groll.BackgroundImage")));
            this.Groll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Groll.BaseArcColor = System.Drawing.Color.Gray;
            this.Groll.BaseArcRadius = 50;
            this.Groll.BaseArcStart = 90;
            this.Groll.BaseArcSweep = 360;
            this.Groll.BaseArcWidth = 2;
            this.Groll.basesize = new System.Drawing.Size(170, 170);
            this.Groll.Cap_Idx = ((byte)(2));
            this.Groll.CapColor = System.Drawing.Color.White;
            this.Groll.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.Groll.CapPosition = new System.Drawing.Point(10, 10);
            this.Groll.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.Groll.CapsText = new string[] {
        "Roll",
        "",
        "",
        "",
        ""};
            this.Groll.CapText = "";
            this.Groll.Center = new System.Drawing.Point(85, 85);
            this.Groll.DataBindings.Add(new System.Windows.Forms.Binding("Value0", this.currentStateBindingSource, "roll", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.Groll.Location = new System.Drawing.Point(94, 9);
            this.Groll.MaxValue = 179F;
            this.Groll.MinValue = -180F;
            this.Groll.Name = "Groll";
            this.Groll.Need_Idx = ((byte)(3));
            this.Groll.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.Groll.NeedleColor2 = System.Drawing.Color.DimGray;
            this.Groll.NeedleEnabled = false;
            this.Groll.NeedleRadius = 80;
            this.Groll.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.Groll.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray};
            this.Groll.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.Groll.NeedlesRadius = new int[] {
        50,
        80,
        80,
        80};
            this.Groll.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.Groll.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.Groll.NeedleType = 0;
            this.Groll.NeedleWidth = 2;
            this.Groll.Range_Idx = ((byte)(2));
            this.Groll.RangeColor = System.Drawing.Color.LightGreen;
            this.Groll.RangeEnabled = true;
            this.Groll.RangeEndValue = -90F;
            this.Groll.RangeInnerRadius = 50;
            this.Groll.RangeOuterRadius = 60;
            this.Groll.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightSteelBlue,
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.LightGreen,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.Groll.RangesEnabled = new bool[] {
        true,
        true,
        true,
        false,
        false};
            this.Groll.RangesEndValue = new float[] {
        90F,
        180F,
        -90F,
        0F,
        0F};
            this.Groll.RangesInnerRadius = new int[] {
        50,
        50,
        50,
        70,
        70};
            this.Groll.RangesOuterRadius = new int[] {
        60,
        60,
        60,
        80,
        80};
            this.Groll.RangesStartValue = new float[] {
        -90F,
        90F,
        -180F,
        0F,
        0F};
            this.Groll.RangeStartValue = -180F;
            this.Groll.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.Groll.ScaleLinesInterInnerRadius = 60;
            this.Groll.ScaleLinesInterOuterRadius = 50;
            this.Groll.ScaleLinesInterWidth = 1;
            this.Groll.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.Groll.ScaleLinesMajorInnerRadius = 50;
            this.Groll.ScaleLinesMajorOuterRadius = 60;
            this.Groll.ScaleLinesMajorStepValue = 30F;
            this.Groll.ScaleLinesMajorWidth = 2;
            this.Groll.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.Groll.ScaleLinesMinorInnerRadius = 50;
            this.Groll.ScaleLinesMinorNumOf = 5;
            this.Groll.ScaleLinesMinorOuterRadius = 55;
            this.Groll.ScaleLinesMinorWidth = 1;
            this.Groll.ScaleNumbersColor = System.Drawing.Color.White;
            this.Groll.ScaleNumbersFormat = null;
            this.Groll.ScaleNumbersRadius = 38;
            this.Groll.ScaleNumbersRotation = 0;
            this.Groll.ScaleNumbersStartScaleLine = 1;
            this.Groll.ScaleNumbersStepScaleLines = 1;
            this.Groll.Size = new System.Drawing.Size(170, 170);
            this.Groll.TabIndex = 33;
            this.Groll.Value = 0F;
            this.Groll.Value0 = 0F;
            this.Groll.Value1 = 0F;
            this.Groll.Value2 = 0F;
            this.Groll.Value3 = 0F;
            // 
            // tabOrientation
            // 
            this.tabOrientation.Controls.Add(this.horizontalProgressBar17);
            this.tabOrientation.Controls.Add(this.verticalProgressBar7);
            this.tabOrientation.Controls.Add(this.verticalProgressBar6);
            this.tabOrientation.Controls.Add(this.verticalProgressBar5);
            this.tabOrientation.Controls.Add(this.verticalProgressBar4);
            this.tabOrientation.Controls.Add(this.progressBar2);
            this.tabOrientation.Controls.Add(this.progressBar1);
            this.tabOrientation.Controls.Add(this.verticalProgressBar3);
            this.tabOrientation.Controls.Add(this.verticalProgressBar2);
            this.tabOrientation.Controls.Add(this.verticalProgressBar1);
            this.tabOrientation.Location = new System.Drawing.Point(4, 22);
            this.tabOrientation.Name = "tabOrientation";
            this.tabOrientation.Padding = new System.Windows.Forms.Padding(3);
            this.tabOrientation.Size = new System.Drawing.Size(751, 478);
            this.tabOrientation.TabIndex = 0;
            this.tabOrientation.Text = "Flight Data";
            this.tabOrientation.UseVisualStyleBackColor = true;
            // 
            // horizontalProgressBar17
            // 
            this.horizontalProgressBar17.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "gz", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.horizontalProgressBar17.Label = "Gyro Z";
            this.horizontalProgressBar17.Location = new System.Drawing.Point(553, 36);
            this.horizontalProgressBar17.MarqueeAnimationSpeed = 1;
            this.horizontalProgressBar17.Maximum = 4000;
            this.horizontalProgressBar17.Minimum = -4000;
            this.horizontalProgressBar17.Name = "horizontalProgressBar17";
            this.horizontalProgressBar17.Size = new System.Drawing.Size(45, 170);
            this.horizontalProgressBar17.Step = 1;
            this.horizontalProgressBar17.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.horizontalProgressBar17.TabIndex = 43;
            // 
            // verticalProgressBar7
            // 
            this.verticalProgressBar7.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch4out", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "1000"));
            this.verticalProgressBar7.Label = "Motor 4";
            this.verticalProgressBar7.Location = new System.Drawing.Point(525, 263);
            this.verticalProgressBar7.MarqueeAnimationSpeed = 1;
            this.verticalProgressBar7.Maximum = 2000;
            this.verticalProgressBar7.Minimum = 1000;
            this.verticalProgressBar7.Name = "verticalProgressBar7";
            this.verticalProgressBar7.Size = new System.Drawing.Size(50, 170);
            this.verticalProgressBar7.Step = 1;
            this.verticalProgressBar7.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.verticalProgressBar7.TabIndex = 42;
            this.verticalProgressBar7.Value = 1500;
            // 
            // verticalProgressBar6
            // 
            this.verticalProgressBar6.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch3out", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "1000"));
            this.verticalProgressBar6.Label = "Motor 3";
            this.verticalProgressBar6.Location = new System.Drawing.Point(400, 263);
            this.verticalProgressBar6.MarqueeAnimationSpeed = 1;
            this.verticalProgressBar6.Maximum = 2000;
            this.verticalProgressBar6.Minimum = 1000;
            this.verticalProgressBar6.Name = "verticalProgressBar6";
            this.verticalProgressBar6.Size = new System.Drawing.Size(50, 170);
            this.verticalProgressBar6.Step = 1;
            this.verticalProgressBar6.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.verticalProgressBar6.TabIndex = 41;
            this.verticalProgressBar6.Value = 1500;
            // 
            // verticalProgressBar5
            // 
            this.verticalProgressBar5.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch2out", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "1000"));
            this.verticalProgressBar5.Label = "Motor 2";
            this.verticalProgressBar5.Location = new System.Drawing.Point(275, 263);
            this.verticalProgressBar5.MarqueeAnimationSpeed = 1;
            this.verticalProgressBar5.Maximum = 2000;
            this.verticalProgressBar5.Minimum = 1000;
            this.verticalProgressBar5.Name = "verticalProgressBar5";
            this.verticalProgressBar5.Size = new System.Drawing.Size(50, 170);
            this.verticalProgressBar5.Step = 1;
            this.verticalProgressBar5.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.verticalProgressBar5.TabIndex = 40;
            this.verticalProgressBar5.Value = 1500;
            // 
            // verticalProgressBar4
            // 
            this.verticalProgressBar4.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ch1out", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "1000"));
            this.verticalProgressBar4.Label = "Motor 1";
            this.verticalProgressBar4.Location = new System.Drawing.Point(150, 263);
            this.verticalProgressBar4.MarqueeAnimationSpeed = 1;
            this.verticalProgressBar4.Maximum = 2000;
            this.verticalProgressBar4.Minimum = 1000;
            this.verticalProgressBar4.Name = "verticalProgressBar4";
            this.verticalProgressBar4.Size = new System.Drawing.Size(50, 170);
            this.verticalProgressBar4.Step = 1;
            this.verticalProgressBar4.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.verticalProgressBar4.TabIndex = 39;
            this.verticalProgressBar4.Value = 1500;
            // 
            // progressBar2
            // 
            this.progressBar2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "gx", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.progressBar2.Label = "Gyro X";
            this.progressBar2.Location = new System.Drawing.Point(400, 36);
            this.progressBar2.MarqueeAnimationSpeed = 1;
            this.progressBar2.Maximum = 4000;
            this.progressBar2.Minimum = -4000;
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(45, 170);
            this.progressBar2.Step = 1;
            this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar2.TabIndex = 38;
            // 
            // progressBar1
            // 
            this.progressBar1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ax", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.progressBar1.Label = "Accel X";
            this.progressBar1.Location = new System.Drawing.Point(126, 36);
            this.progressBar1.MarqueeAnimationSpeed = 1;
            this.progressBar1.Maximum = 1200;
            this.progressBar1.Minimum = -1200;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(45, 170);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 37;
            // 
            // verticalProgressBar3
            // 
            this.verticalProgressBar3.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "gy", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.verticalProgressBar3.Label = "Gyro Y";
            this.verticalProgressBar3.Location = new System.Drawing.Point(478, 36);
            this.verticalProgressBar3.MarqueeAnimationSpeed = 1;
            this.verticalProgressBar3.Maximum = 4000;
            this.verticalProgressBar3.Minimum = -4000;
            this.verticalProgressBar3.Name = "verticalProgressBar3";
            this.verticalProgressBar3.Size = new System.Drawing.Size(45, 170);
            this.verticalProgressBar3.Step = 1;
            this.verticalProgressBar3.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.verticalProgressBar3.TabIndex = 36;
            // 
            // verticalProgressBar2
            // 
            this.verticalProgressBar2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "ay", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.verticalProgressBar2.Label = "Accel Y";
            this.verticalProgressBar2.Location = new System.Drawing.Point(206, 36);
            this.verticalProgressBar2.MarqueeAnimationSpeed = 1;
            this.verticalProgressBar2.Maximum = 1200;
            this.verticalProgressBar2.Minimum = -1200;
            this.verticalProgressBar2.Name = "verticalProgressBar2";
            this.verticalProgressBar2.Size = new System.Drawing.Size(45, 170);
            this.verticalProgressBar2.Step = 1;
            this.verticalProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.verticalProgressBar2.TabIndex = 35;
            // 
            // verticalProgressBar1
            // 
            this.verticalProgressBar1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.currentStateBindingSource, "az", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.verticalProgressBar1.Label = "Accel Z";
            this.verticalProgressBar1.Location = new System.Drawing.Point(283, 36);
            this.verticalProgressBar1.MarqueeAnimationSpeed = 1;
            this.verticalProgressBar1.Maximum = 1200;
            this.verticalProgressBar1.Minimum = -1200;
            this.verticalProgressBar1.Name = "verticalProgressBar1";
            this.verticalProgressBar1.Size = new System.Drawing.Size(45, 170);
            this.verticalProgressBar1.Step = 1;
            this.verticalProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.verticalProgressBar1.TabIndex = 34;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabRawSensor);
            this.tabControl.Controls.Add(this.tabRadio);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(759, 504);
            this.tabControl.TabIndex = 32;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // AC2_Sensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 501);
            this.Controls.Add(this.tabControl);
            this.Name = "AC2_Sensor";
            this.Text = "AC2 Sensor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ACM_Setup_FormClosed);
            this.Load += new System.EventHandler(this.ACM_Setup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.currentStateBindingSource)).EndInit();
            this.tabRadio.ResumeLayout(false);
            this.tabRadio.PerformLayout();
            this.tabRawSensor.ResumeLayout(false);
            this.tabRawSensor.PerformLayout();
            this.tabOrientation.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource currentStateBindingSource;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2serial;
        private System.Windows.Forms.TabPage tabRadio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private HorizontalProgressBar horizontalProgressBar9;
        private HorizontalProgressBar horizontalProgressBar10;
        private HorizontalProgressBar horizontalProgressBar11;
        private HorizontalProgressBar horizontalProgressBar12;
        private HorizontalProgressBar horizontalProgressBar13;
        private HorizontalProgressBar horizontalProgressBar14;
        private HorizontalProgressBar horizontalProgressBar15;
        private HorizontalProgressBar horizontalProgressBar16;
        private HorizontalProgressBar horizontalProgressBar8;
        private HorizontalProgressBar horizontalProgressBar7;
        private HorizontalProgressBar horizontalProgressBar6;
        private HorizontalProgressBar horizontalProgressBar5;
        private HorizontalProgressBar horizontalProgressBar4;
        private HorizontalProgressBar horizontalProgressBar3;
        private HorizontalProgressBar horizontalProgressBar2;
        private HorizontalProgressBar horizontalProgressBar1;
        private System.Windows.Forms.TabPage tabRawSensor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CMB_rawupdaterate;
        private AGaugeApp.AGauge aGauge1;
        private System.Windows.Forms.CheckBox chkax;
        private System.Windows.Forms.CheckBox chkay;
        private System.Windows.Forms.CheckBox chkaz;
        private System.Windows.Forms.CheckBox chkgx;
        private System.Windows.Forms.CheckBox chkgy;
        private System.Windows.Forms.CheckBox chkgz;
        private ZedGraph.ZedGraphControl zg1;
        private AGaugeApp.AGauge Gpitch;
        private AGaugeApp.AGauge Groll;
        private System.Windows.Forms.TabPage tabOrientation;
        private VerticalProgressBar horizontalProgressBar17;
        private VerticalProgressBar verticalProgressBar7;
        private VerticalProgressBar verticalProgressBar6;
        private VerticalProgressBar verticalProgressBar5;
        private VerticalProgressBar verticalProgressBar4;
        private VerticalProgressBar progressBar2;
        private VerticalProgressBar progressBar1;
        private VerticalProgressBar verticalProgressBar3;
        private VerticalProgressBar verticalProgressBar2;
        private VerticalProgressBar verticalProgressBar1;
        private System.Windows.Forms.TabControl tabControl;

    }
}