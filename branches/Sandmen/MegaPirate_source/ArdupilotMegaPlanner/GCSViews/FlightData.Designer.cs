namespace ArdupilotMega.GCSViews
{
    partial class FlightData
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlightData));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.goHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainH = new System.Windows.Forms.SplitContainer();
            this.SubMainLeft = new System.Windows.Forms.SplitContainer();
            this.hud1 = new hud.HUD();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.BUT_quickmanual = new ArdupilotMega.MyButton();
            this.BUT_quickrtl = new ArdupilotMega.MyButton();
            this.BUT_quickauto = new ArdupilotMega.MyButton();
            this.CMB_setwp = new System.Windows.Forms.ComboBox();
            this.BUT_setwp = new ArdupilotMega.MyButton();
            this.CMB_modes = new System.Windows.Forms.ComboBox();
            this.BUT_setmode = new ArdupilotMega.MyButton();
            this.BUT_clear_track = new ArdupilotMega.MyButton();
            this.CMB_action = new System.Windows.Forms.ComboBox();
            this.CB_tuning = new System.Windows.Forms.CheckBox();
            this.BUT_Homealt = new ArdupilotMega.MyButton();
            this.BUT_RAWSensor = new ArdupilotMega.MyButton();
            this.BUTrestartmission = new ArdupilotMega.MyButton();
            this.BUTactiondo = new ArdupilotMega.MyButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Gvspeed = new AGaugeApp.AGauge();
            this.Gheading = new AGaugeApp.AGauge();
            this.Galt = new AGaugeApp.AGauge();
            this.Gspeed = new AGaugeApp.AGauge();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.TXT_control_mode = new System.Windows.Forms.Label();
            this.TXT_WP = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TXT_wpdist = new System.Windows.Forms.Label();
            this.TXT_bererror = new System.Windows.Forms.Label();
            this.TXT_alterror = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TXT_servoroll = new System.Windows.Forms.Label();
            this.TXT_servopitch = new System.Windows.Forms.Label();
            this.TXT_servorudder = new System.Windows.Forms.Label();
            this.TXT_servothrottle = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.BUT_log2kml = new ArdupilotMega.MyButton();
            this.tracklog = new System.Windows.Forms.TrackBar();
            this.BUT_stoptelemlog = new ArdupilotMega.MyButton();
            this.BUT_playlog = new ArdupilotMega.MyButton();
            this.BUT_loadtelem = new ArdupilotMega.MyButton();
            this.BUT_savetelem = new ArdupilotMega.MyButton();
            this.tableMap = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TXT_lat = new System.Windows.Forms.Label();
            this.Zoomlevel = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.TXT_long = new System.Windows.Forms.Label();
            this.TXT_alt = new System.Windows.Forms.Label();
            this.CHK_autopan = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.ZedGraphTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.MainH.Panel1.SuspendLayout();
            this.MainH.Panel2.SuspendLayout();
            this.MainH.SuspendLayout();
            this.SubMainLeft.Panel1.SuspendLayout();
            this.SubMainLeft.Panel2.SuspendLayout();
            this.SubMainLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tracklog)).BeginInit();
            this.tableMap.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Zoomlevel)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goHereToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // goHereToolStripMenuItem
            // 
            this.goHereToolStripMenuItem.Name = "goHereToolStripMenuItem";
            resources.ApplyResources(this.goHereToolStripMenuItem, "goHereToolStripMenuItem");
            this.goHereToolStripMenuItem.Click += new System.EventHandler(this.goHereToolStripMenuItem_Click);
            // 
            // MainH
            // 
            this.MainH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.MainH, "MainH");
            this.MainH.Name = "MainH";
            // 
            // MainH.Panel1
            // 
            this.MainH.Panel1.Controls.Add(this.SubMainLeft);
            // 
            // MainH.Panel2
            // 
            this.MainH.Panel2.Controls.Add(this.tableMap);
            this.MainH.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.MainH_SplitterMoved);
            // 
            // SubMainLeft
            // 
            this.SubMainLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.SubMainLeft, "SubMainLeft");
            this.SubMainLeft.Name = "SubMainLeft";
            // 
            // SubMainLeft.Panel1
            // 
            this.SubMainLeft.Panel1.Controls.Add(this.hud1);
            this.SubMainLeft.Panel1.Resize += new System.EventHandler(this.SubMainHT_Panel1_Resize);
            // 
            // SubMainLeft.Panel2
            // 
            this.SubMainLeft.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.SubMainLeft.Panel2.Controls.Add(this.tabControl1);
            this.SubMainLeft.Panel2.Resize += new System.EventHandler(this.SubMainLeft_Panel2_Resize);
            // 
            // hud1
            // 
            this.hud1.airspeed = 0F;
            this.hud1.alt = 0F;
            this.hud1.BackColor = System.Drawing.Color.Transparent;
            this.hud1.batterylevel = 0F;
            this.hud1.batteryremaining = 0F;
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("airspeed", this.bindingSource1, "airspeed", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("alt", this.bindingSource1, "alt", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("batterylevel", this.bindingSource1, "battery_voltage", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("batteryremaining", this.bindingSource1, "battery_remaining", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("disttowp", this.bindingSource1, "wp_dist", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("gpsfix", this.bindingSource1, "gpsstatus", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("gpshdop", this.bindingSource1, "gpshdop", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("groundspeed", this.bindingSource1, "groundspeed", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("heading", this.bindingSource1, "yaw", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("mode", this.bindingSource1, "mode", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("navpitch", this.bindingSource1, "nav_pitch", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("navroll", this.bindingSource1, "nav_roll", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("pitch", this.bindingSource1, "pitch", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("roll", this.bindingSource1, "roll", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("targetalt", this.bindingSource1, "targetalt", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("targetheading", this.bindingSource1, "nav_bearing", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("targetspeed", this.bindingSource1, "targetairspeed", true));
            this.hud1.DataBindings.Add(new System.Windows.Forms.Binding("wpno", this.bindingSource1, "wpno", true));
            this.hud1.distscale = 0F;
            this.hud1.disttowp = 0F;
            resources.ApplyResources(this.hud1, "hud1");
            this.hud1.gpsfix = 0F;
            this.hud1.gpshdop = 0F;
            this.hud1.groundspeed = 0F;
            this.hud1.heading = 0F;
            this.hud1.hudcolor = System.Drawing.Color.White;
            this.hud1.mode = "Manual";
            this.hud1.Name = "hud1";
            this.hud1.navpitch = 0F;
            this.hud1.navroll = 0F;
            this.hud1.pitch = 0F;
            this.hud1.roll = 0F;
            this.hud1.speedscale = 0F;
            this.hud1.streamjpg = ((System.IO.MemoryStream)(resources.GetObject("hud1.streamjpg")));
            this.hud1.targetalt = 0F;
            this.hud1.targetheading = 0F;
            this.hud1.targetspeed = 0F;
            this.hud1.wpno = 0;
            this.hud1.Resize += new System.EventHandler(this.hud1_Resize);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ArdupilotMega.CurrentState);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.BUT_quickmanual);
            this.tabPage3.Controls.Add(this.BUT_quickrtl);
            this.tabPage3.Controls.Add(this.BUT_quickauto);
            this.tabPage3.Controls.Add(this.CMB_setwp);
            this.tabPage3.Controls.Add(this.BUT_setwp);
            this.tabPage3.Controls.Add(this.CMB_modes);
            this.tabPage3.Controls.Add(this.BUT_setmode);
            this.tabPage3.Controls.Add(this.BUT_clear_track);
            this.tabPage3.Controls.Add(this.CMB_action);
            this.tabPage3.Controls.Add(this.CB_tuning);
            this.tabPage3.Controls.Add(this.BUT_Homealt);
            this.tabPage3.Controls.Add(this.BUT_RAWSensor);
            this.tabPage3.Controls.Add(this.BUTrestartmission);
            this.tabPage3.Controls.Add(this.BUTactiondo);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // BUT_quickmanual
            // 
            resources.ApplyResources(this.BUT_quickmanual, "BUT_quickmanual");
            this.BUT_quickmanual.Name = "BUT_quickmanual";
            this.BUT_quickmanual.UseVisualStyleBackColor = true;
            this.BUT_quickmanual.Click += new System.EventHandler(this.BUT_quickmanual_Click);
            // 
            // BUT_quickrtl
            // 
            resources.ApplyResources(this.BUT_quickrtl, "BUT_quickrtl");
            this.BUT_quickrtl.Name = "BUT_quickrtl";
            this.BUT_quickrtl.UseVisualStyleBackColor = true;
            this.BUT_quickrtl.Click += new System.EventHandler(this.BUT_quickrtl_Click);
            // 
            // BUT_quickauto
            // 
            resources.ApplyResources(this.BUT_quickauto, "BUT_quickauto");
            this.BUT_quickauto.Name = "BUT_quickauto";
            this.BUT_quickauto.UseVisualStyleBackColor = true;
            this.BUT_quickauto.Click += new System.EventHandler(this.BUT_quickauto_Click);
            // 
            // CMB_setwp
            // 
            this.CMB_setwp.FormattingEnabled = true;
            this.CMB_setwp.Items.AddRange(new object[] {
            resources.GetString("CMB_setwp.Items")});
            resources.ApplyResources(this.CMB_setwp, "CMB_setwp");
            this.CMB_setwp.Name = "CMB_setwp";
            this.CMB_setwp.Click += new System.EventHandler(this.CMB_setwp_Click);
            // 
            // BUT_setwp
            // 
            resources.ApplyResources(this.BUT_setwp, "BUT_setwp");
            this.BUT_setwp.Name = "BUT_setwp";
            this.BUT_setwp.UseVisualStyleBackColor = true;
            this.BUT_setwp.Click += new System.EventHandler(this.BUT_setwp_Click);
            // 
            // CMB_modes
            // 
            this.CMB_modes.FormattingEnabled = true;
            resources.ApplyResources(this.CMB_modes, "CMB_modes");
            this.CMB_modes.Name = "CMB_modes";
            // 
            // BUT_setmode
            // 
            resources.ApplyResources(this.BUT_setmode, "BUT_setmode");
            this.BUT_setmode.Name = "BUT_setmode";
            this.BUT_setmode.UseVisualStyleBackColor = true;
            this.BUT_setmode.Click += new System.EventHandler(this.BUT_setmode_Click);
            // 
            // BUT_clear_track
            // 
            resources.ApplyResources(this.BUT_clear_track, "BUT_clear_track");
            this.BUT_clear_track.Name = "BUT_clear_track";
            this.BUT_clear_track.UseVisualStyleBackColor = true;
            this.BUT_clear_track.Click += new System.EventHandler(this.BUT_clear_track_Click);
            // 
            // CMB_action
            // 
            this.CMB_action.FormattingEnabled = true;
            resources.ApplyResources(this.CMB_action, "CMB_action");
            this.CMB_action.Name = "CMB_action";
            // 
            // CB_tuning
            // 
            resources.ApplyResources(this.CB_tuning, "CB_tuning");
            this.CB_tuning.Name = "CB_tuning";
            this.CB_tuning.UseVisualStyleBackColor = true;
            this.CB_tuning.CheckedChanged += new System.EventHandler(this.CB_tuning_CheckedChanged);
            // 
            // BUT_Homealt
            // 
            resources.ApplyResources(this.BUT_Homealt, "BUT_Homealt");
            this.BUT_Homealt.Name = "BUT_Homealt";
            this.BUT_Homealt.UseVisualStyleBackColor = true;
            this.BUT_Homealt.Click += new System.EventHandler(this.BUT_Homealt_Click);
            // 
            // BUT_RAWSensor
            // 
            resources.ApplyResources(this.BUT_RAWSensor, "BUT_RAWSensor");
            this.BUT_RAWSensor.Name = "BUT_RAWSensor";
            this.BUT_RAWSensor.UseVisualStyleBackColor = true;
            this.BUT_RAWSensor.Click += new System.EventHandler(this.BUT_RAWSensor_Click);
            // 
            // BUTrestartmission
            // 
            resources.ApplyResources(this.BUTrestartmission, "BUTrestartmission");
            this.BUTrestartmission.Name = "BUTrestartmission";
            this.BUTrestartmission.UseVisualStyleBackColor = true;
            this.BUTrestartmission.Click += new System.EventHandler(this.BUTrestartmission_Click);
            // 
            // BUTactiondo
            // 
            resources.ApplyResources(this.BUTactiondo, "BUTactiondo");
            this.BUTactiondo.Name = "BUTactiondo";
            this.BUTactiondo.UseVisualStyleBackColor = true;
            this.BUTactiondo.Click += new System.EventHandler(this.BUTactiondo_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Gvspeed);
            this.tabPage1.Controls.Add(this.Gheading);
            this.tabPage1.Controls.Add(this.Galt);
            this.tabPage1.Controls.Add(this.Gspeed);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Resize += new System.EventHandler(this.tabPage1_Resize);
            // 
            // Gvspeed
            // 
            this.Gvspeed.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.Gvspeed, "Gvspeed");
            this.Gvspeed.BaseArcColor = System.Drawing.Color.Transparent;
            this.Gvspeed.BaseArcRadius = 60;
            this.Gvspeed.BaseArcStart = 20;
            this.Gvspeed.BaseArcSweep = 320;
            this.Gvspeed.BaseArcWidth = 2;
            this.Gvspeed.basesize = new System.Drawing.Size(150, 150);
            this.Gvspeed.Cap_Idx = ((byte)(0));
            this.Gvspeed.CapColor = System.Drawing.Color.White;
            this.Gvspeed.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.Gvspeed.CapPosition = new System.Drawing.Point(65, 85);
            this.Gvspeed.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(65, 85),
        new System.Drawing.Point(30, 55),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.Gvspeed.CapsText = new string[] {
        "VSI",
        "",
        "",
        "",
        ""};
            this.Gvspeed.CapText = "VSI";
            this.Gvspeed.Center = new System.Drawing.Point(75, 75);
            this.Gvspeed.DataBindings.Add(new System.Windows.Forms.Binding("Value0", this.bindingSource1, "climbrate", true));
            this.Gvspeed.MaxValue = 10F;
            this.Gvspeed.MinValue = -10F;
            this.Gvspeed.Name = "Gvspeed";
            this.Gvspeed.Need_Idx = ((byte)(3));
            this.Gvspeed.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.Gvspeed.NeedleColor2 = System.Drawing.Color.White;
            this.Gvspeed.NeedleEnabled = false;
            this.Gvspeed.NeedleRadius = 80;
            this.Gvspeed.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.Gvspeed.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White};
            this.Gvspeed.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.Gvspeed.NeedlesRadius = new int[] {
        50,
        30,
        50,
        80};
            this.Gvspeed.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.Gvspeed.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.Gvspeed.NeedleType = 0;
            this.Gvspeed.NeedleWidth = 2;
            this.Gvspeed.Range_Idx = ((byte)(0));
            this.Gvspeed.RangeColor = System.Drawing.Color.LightGreen;
            this.Gvspeed.RangeEnabled = false;
            this.Gvspeed.RangeEndValue = 360F;
            this.Gvspeed.RangeInnerRadius = 1;
            this.Gvspeed.RangeOuterRadius = 60;
            this.Gvspeed.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.Gvspeed.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.Gvspeed.RangesEndValue = new float[] {
        360F,
        200F,
        150F,
        0F,
        0F};
            this.Gvspeed.RangesInnerRadius = new int[] {
        1,
        1,
        1,
        70,
        70};
            this.Gvspeed.RangesOuterRadius = new int[] {
        60,
        60,
        60,
        80,
        80};
            this.Gvspeed.RangesStartValue = new float[] {
        0F,
        150F,
        75F,
        0F,
        0F};
            this.Gvspeed.RangeStartValue = 0F;
            this.Gvspeed.ScaleLinesInterColor = System.Drawing.Color.White;
            this.Gvspeed.ScaleLinesInterInnerRadius = 52;
            this.Gvspeed.ScaleLinesInterOuterRadius = 60;
            this.Gvspeed.ScaleLinesInterWidth = 1;
            this.Gvspeed.ScaleLinesMajorColor = System.Drawing.Color.White;
            this.Gvspeed.ScaleLinesMajorInnerRadius = 50;
            this.Gvspeed.ScaleLinesMajorOuterRadius = 60;
            this.Gvspeed.ScaleLinesMajorStepValue = 2F;
            this.Gvspeed.ScaleLinesMajorWidth = 2;
            this.Gvspeed.ScaleLinesMinorColor = System.Drawing.Color.White;
            this.Gvspeed.ScaleLinesMinorInnerRadius = 55;
            this.Gvspeed.ScaleLinesMinorNumOf = 9;
            this.Gvspeed.ScaleLinesMinorOuterRadius = 60;
            this.Gvspeed.ScaleLinesMinorWidth = 1;
            this.Gvspeed.ScaleNumbersColor = System.Drawing.Color.White;
            this.Gvspeed.ScaleNumbersFormat = "";
            this.Gvspeed.ScaleNumbersRadius = 42;
            this.Gvspeed.ScaleNumbersRotation = 0;
            this.Gvspeed.ScaleNumbersStartScaleLine = 1;
            this.Gvspeed.ScaleNumbersStepScaleLines = 1;
            this.Gvspeed.Value = 0F;
            this.Gvspeed.Value0 = 0F;
            this.Gvspeed.Value1 = 0F;
            this.Gvspeed.Value2 = 0F;
            this.Gvspeed.Value3 = 0F;
            // 
            // Gheading
            // 
            this.Gheading.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.Gheading, "Gheading");
            this.Gheading.BaseArcColor = System.Drawing.Color.Transparent;
            this.Gheading.BaseArcRadius = 60;
            this.Gheading.BaseArcStart = 270;
            this.Gheading.BaseArcSweep = 360;
            this.Gheading.BaseArcWidth = 2;
            this.Gheading.basesize = new System.Drawing.Size(150, 150);
            this.Gheading.Cap_Idx = ((byte)(0));
            this.Gheading.CapColor = System.Drawing.Color.White;
            this.Gheading.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.Gheading.CapPosition = new System.Drawing.Point(55, 85);
            this.Gheading.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(55, 85),
        new System.Drawing.Point(40, 67),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.Gheading.CapsText = new string[] {
        "Heading",
        "",
        "",
        "",
        ""};
            this.Gheading.CapText = "Heading";
            this.Gheading.Center = new System.Drawing.Point(75, 75);
            this.Gheading.DataBindings.Add(new System.Windows.Forms.Binding("Value0", this.bindingSource1, "yaw", true));
            this.Gheading.DataBindings.Add(new System.Windows.Forms.Binding("Value1", this.bindingSource1, "nav_bearing", true));
            this.Gheading.MaxValue = 359F;
            this.Gheading.MinValue = 0F;
            this.Gheading.Name = "Gheading";
            this.Gheading.Need_Idx = ((byte)(3));
            this.Gheading.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.Gheading.NeedleColor2 = System.Drawing.Color.White;
            this.Gheading.NeedleEnabled = false;
            this.Gheading.NeedleRadius = 80;
            this.Gheading.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Red,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.Gheading.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White};
            this.Gheading.NeedlesEnabled = new bool[] {
        true,
        true,
        false,
        false};
            this.Gheading.NeedlesRadius = new int[] {
        60,
        60,
        80,
        80};
            this.Gheading.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.Gheading.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.Gheading.NeedleType = 0;
            this.Gheading.NeedleWidth = 2;
            this.Gheading.Range_Idx = ((byte)(0));
            this.Gheading.RangeColor = System.Drawing.Color.LightGreen;
            this.Gheading.RangeEnabled = false;
            this.Gheading.RangeEndValue = 360F;
            this.Gheading.RangeInnerRadius = 1;
            this.Gheading.RangeOuterRadius = 60;
            this.Gheading.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.Gheading.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.Gheading.RangesEndValue = new float[] {
        360F,
        200F,
        150F,
        0F,
        0F};
            this.Gheading.RangesInnerRadius = new int[] {
        1,
        1,
        1,
        70,
        70};
            this.Gheading.RangesOuterRadius = new int[] {
        60,
        60,
        60,
        80,
        80};
            this.Gheading.RangesStartValue = new float[] {
        0F,
        150F,
        75F,
        0F,
        0F};
            this.Gheading.RangeStartValue = 0F;
            this.Gheading.ScaleLinesInterColor = System.Drawing.Color.White;
            this.Gheading.ScaleLinesInterInnerRadius = 52;
            this.Gheading.ScaleLinesInterOuterRadius = 60;
            this.Gheading.ScaleLinesInterWidth = 1;
            this.Gheading.ScaleLinesMajorColor = System.Drawing.Color.White;
            this.Gheading.ScaleLinesMajorInnerRadius = 50;
            this.Gheading.ScaleLinesMajorOuterRadius = 60;
            this.Gheading.ScaleLinesMajorStepValue = 45F;
            this.Gheading.ScaleLinesMajorWidth = 2;
            this.Gheading.ScaleLinesMinorColor = System.Drawing.Color.White;
            this.Gheading.ScaleLinesMinorInnerRadius = 55;
            this.Gheading.ScaleLinesMinorNumOf = 9;
            this.Gheading.ScaleLinesMinorOuterRadius = 60;
            this.Gheading.ScaleLinesMinorWidth = 1;
            this.Gheading.ScaleNumbersColor = System.Drawing.Color.White;
            this.Gheading.ScaleNumbersFormat = null;
            this.Gheading.ScaleNumbersRadius = 42;
            this.Gheading.ScaleNumbersRotation = 45;
            this.Gheading.ScaleNumbersStartScaleLine = 1;
            this.Gheading.ScaleNumbersStepScaleLines = 1;
            this.Gheading.Value = 0F;
            this.Gheading.Value0 = 0F;
            this.Gheading.Value1 = 0F;
            this.Gheading.Value2 = 0F;
            this.Gheading.Value3 = 0F;
            // 
            // Galt
            // 
            this.Galt.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.Galt, "Galt");
            this.Galt.BaseArcColor = System.Drawing.Color.Transparent;
            this.Galt.BaseArcRadius = 60;
            this.Galt.BaseArcStart = 270;
            this.Galt.BaseArcSweep = 360;
            this.Galt.BaseArcWidth = 2;
            this.Galt.basesize = new System.Drawing.Size(150, 150);
            this.Galt.Cap_Idx = ((byte)(0));
            this.Galt.CapColor = System.Drawing.Color.White;
            this.Galt.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.Galt.CapPosition = new System.Drawing.Point(68, 85);
            this.Galt.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(68, 85),
        new System.Drawing.Point(30, 55),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.Galt.CapsText = new string[] {
        "Alt",
        "",
        "",
        "",
        ""};
            this.Galt.CapText = "Alt";
            this.Galt.Center = new System.Drawing.Point(75, 75);
            this.Galt.DataBindings.Add(new System.Windows.Forms.Binding("Value0", this.bindingSource1, "altd100", true));
            this.Galt.DataBindings.Add(new System.Windows.Forms.Binding("Value1", this.bindingSource1, "altd1000", true));
            this.Galt.DataBindings.Add(new System.Windows.Forms.Binding("Value2", this.bindingSource1, "targetaltd100", true));
            this.Galt.MaxValue = 9.99F;
            this.Galt.MinValue = 0F;
            this.Galt.Name = "Galt";
            this.Galt.Need_Idx = ((byte)(3));
            this.Galt.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.Galt.NeedleColor2 = System.Drawing.Color.White;
            this.Galt.NeedleEnabled = false;
            this.Galt.NeedleRadius = 80;
            this.Galt.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Red,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.Galt.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White};
            this.Galt.NeedlesEnabled = new bool[] {
        true,
        true,
        true,
        false};
            this.Galt.NeedlesRadius = new int[] {
        50,
        30,
        50,
        80};
            this.Galt.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.Galt.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.Galt.NeedleType = 0;
            this.Galt.NeedleWidth = 2;
            this.Galt.Range_Idx = ((byte)(0));
            this.Galt.RangeColor = System.Drawing.Color.LightGreen;
            this.Galt.RangeEnabled = false;
            this.Galt.RangeEndValue = 360F;
            this.Galt.RangeInnerRadius = 1;
            this.Galt.RangeOuterRadius = 60;
            this.Galt.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.Galt.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.Galt.RangesEndValue = new float[] {
        360F,
        200F,
        150F,
        0F,
        0F};
            this.Galt.RangesInnerRadius = new int[] {
        1,
        1,
        1,
        70,
        70};
            this.Galt.RangesOuterRadius = new int[] {
        60,
        60,
        60,
        80,
        80};
            this.Galt.RangesStartValue = new float[] {
        0F,
        150F,
        75F,
        0F,
        0F};
            this.Galt.RangeStartValue = 0F;
            this.Galt.ScaleLinesInterColor = System.Drawing.Color.White;
            this.Galt.ScaleLinesInterInnerRadius = 52;
            this.Galt.ScaleLinesInterOuterRadius = 60;
            this.Galt.ScaleLinesInterWidth = 1;
            this.Galt.ScaleLinesMajorColor = System.Drawing.Color.White;
            this.Galt.ScaleLinesMajorInnerRadius = 50;
            this.Galt.ScaleLinesMajorOuterRadius = 60;
            this.Galt.ScaleLinesMajorStepValue = 1F;
            this.Galt.ScaleLinesMajorWidth = 2;
            this.Galt.ScaleLinesMinorColor = System.Drawing.Color.White;
            this.Galt.ScaleLinesMinorInnerRadius = 55;
            this.Galt.ScaleLinesMinorNumOf = 9;
            this.Galt.ScaleLinesMinorOuterRadius = 60;
            this.Galt.ScaleLinesMinorWidth = 1;
            this.Galt.ScaleNumbersColor = System.Drawing.Color.White;
            this.Galt.ScaleNumbersFormat = "";
            this.Galt.ScaleNumbersRadius = 42;
            this.Galt.ScaleNumbersRotation = 0;
            this.Galt.ScaleNumbersStartScaleLine = 1;
            this.Galt.ScaleNumbersStepScaleLines = 1;
            this.Galt.Value = 0F;
            this.Galt.Value0 = 0F;
            this.Galt.Value1 = 0F;
            this.Galt.Value2 = 0F;
            this.Galt.Value3 = 0F;
            // 
            // Gspeed
            // 
            this.Gspeed.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.Gspeed, "Gspeed");
            this.Gspeed.BaseArcColor = System.Drawing.Color.Transparent;
            this.Gspeed.BaseArcRadius = 70;
            this.Gspeed.BaseArcStart = 135;
            this.Gspeed.BaseArcSweep = 270;
            this.Gspeed.BaseArcWidth = 2;
            this.Gspeed.basesize = new System.Drawing.Size(150, 150);
            this.Gspeed.Cap_Idx = ((byte)(0));
            this.Gspeed.CapColor = System.Drawing.Color.White;
            this.Gspeed.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.Gspeed.CapPosition = new System.Drawing.Point(58, 85);
            this.Gspeed.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(58, 85),
        new System.Drawing.Point(50, 110),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.Gspeed.CapsText = new string[] {
        "Speed",
        "",
        "",
        "",
        ""};
            this.Gspeed.CapText = "Speed";
            this.Gspeed.Center = new System.Drawing.Point(75, 75);
            this.Gspeed.DataBindings.Add(new System.Windows.Forms.Binding("Value0", this.bindingSource1, "airspeed", true));
            this.Gspeed.DataBindings.Add(new System.Windows.Forms.Binding("Value1", this.bindingSource1, "groundspeed", true));
            this.Gspeed.MaxValue = 60F;
            this.Gspeed.MinValue = 0F;
            this.Gspeed.Name = "Gspeed";
            this.Gspeed.Need_Idx = ((byte)(3));
            this.Gspeed.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.Gspeed.NeedleColor2 = System.Drawing.Color.Brown;
            this.Gspeed.NeedleEnabled = false;
            this.Gspeed.NeedleRadius = 70;
            this.Gspeed.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Red,
        AGaugeApp.AGauge.NeedleColorEnum.Blue,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.Gspeed.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Brown};
            this.Gspeed.NeedlesEnabled = new bool[] {
        true,
        true,
        false,
        false};
            this.Gspeed.NeedlesRadius = new int[] {
        50,
        50,
        70,
        70};
            this.Gspeed.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.Gspeed.NeedlesWidth = new int[] {
        2,
        1,
        2,
        2};
            this.Gspeed.NeedleType = 0;
            this.Gspeed.NeedleWidth = 2;
            this.Gspeed.Range_Idx = ((byte)(2));
            this.Gspeed.RangeColor = System.Drawing.Color.Orange;
            this.Gspeed.RangeEnabled = false;
            this.Gspeed.RangeEndValue = 50F;
            this.Gspeed.RangeInnerRadius = 1;
            this.Gspeed.RangeOuterRadius = 70;
            this.Gspeed.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.Gspeed.RangesEnabled = new bool[] {
        false,
        false,
        false,
        false,
        false};
            this.Gspeed.RangesEndValue = new float[] {
        35F,
        60F,
        50F,
        0F,
        0F};
            this.Gspeed.RangesInnerRadius = new int[] {
        1,
        1,
        1,
        70,
        70};
            this.Gspeed.RangesOuterRadius = new int[] {
        70,
        70,
        70,
        80,
        80};
            this.Gspeed.RangesStartValue = new float[] {
        0F,
        50F,
        35F,
        0F,
        0F};
            this.Gspeed.RangeStartValue = 35F;
            this.Gspeed.ScaleLinesInterColor = System.Drawing.Color.White;
            this.Gspeed.ScaleLinesInterInnerRadius = 52;
            this.Gspeed.ScaleLinesInterOuterRadius = 60;
            this.Gspeed.ScaleLinesInterWidth = 1;
            this.Gspeed.ScaleLinesMajorColor = System.Drawing.Color.White;
            this.Gspeed.ScaleLinesMajorInnerRadius = 50;
            this.Gspeed.ScaleLinesMajorOuterRadius = 60;
            this.Gspeed.ScaleLinesMajorStepValue = 10F;
            this.Gspeed.ScaleLinesMajorWidth = 2;
            this.Gspeed.ScaleLinesMinorColor = System.Drawing.Color.White;
            this.Gspeed.ScaleLinesMinorInnerRadius = 55;
            this.Gspeed.ScaleLinesMinorNumOf = 9;
            this.Gspeed.ScaleLinesMinorOuterRadius = 60;
            this.Gspeed.ScaleLinesMinorWidth = 1;
            this.Gspeed.ScaleNumbersColor = System.Drawing.Color.White;
            this.Gspeed.ScaleNumbersFormat = null;
            this.Gspeed.ScaleNumbersRadius = 42;
            this.Gspeed.ScaleNumbersRotation = 0;
            this.Gspeed.ScaleNumbersStartScaleLine = 1;
            this.Gspeed.ScaleNumbersStepScaleLines = 1;
            this.Gspeed.Value = 0F;
            this.Gspeed.Value0 = 0F;
            this.Gspeed.Value1 = 0F;
            this.Gspeed.Value2 = 0F;
            this.Gspeed.Value3 = 0F;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel3);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.TXT_control_mode);
            this.panel4.Controls.Add(this.TXT_WP);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.TXT_wpdist);
            this.panel4.Controls.Add(this.TXT_bererror);
            this.panel4.Controls.Add(this.TXT_alterror);
            resources.ApplyResources(this.panel4, "panel4");
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
            this.TXT_control_mode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "mode", true));
            resources.ApplyResources(this.TXT_control_mode, "TXT_control_mode");
            this.TXT_control_mode.Name = "TXT_control_mode";
            // 
            // TXT_WP
            // 
            this.TXT_WP.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "wpno", true));
            resources.ApplyResources(this.TXT_WP, "TXT_WP");
            this.TXT_WP.Name = "TXT_WP";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // TXT_wpdist
            // 
            this.TXT_wpdist.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "wp_dist", true));
            resources.ApplyResources(this.TXT_wpdist, "TXT_wpdist");
            this.TXT_wpdist.Name = "TXT_wpdist";
            // 
            // TXT_bererror
            // 
            this.TXT_bererror.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ber_error", true));
            resources.ApplyResources(this.TXT_bererror, "TXT_bererror");
            this.TXT_bererror.Name = "TXT_bererror";
            // 
            // TXT_alterror
            // 
            this.TXT_alterror.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "alt_error", true));
            resources.ApplyResources(this.TXT_alterror, "TXT_alterror");
            this.TXT_alterror.Name = "TXT_alterror";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.TXT_servoroll);
            this.panel3.Controls.Add(this.TXT_servopitch);
            this.panel3.Controls.Add(this.TXT_servorudder);
            this.panel3.Controls.Add(this.TXT_servothrottle);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
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
            // TXT_servoroll
            // 
            this.TXT_servoroll.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ch1out", true));
            resources.ApplyResources(this.TXT_servoroll, "TXT_servoroll");
            this.TXT_servoroll.Name = "TXT_servoroll";
            // 
            // TXT_servopitch
            // 
            this.TXT_servopitch.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ch2out", true));
            resources.ApplyResources(this.TXT_servopitch, "TXT_servopitch");
            this.TXT_servopitch.Name = "TXT_servopitch";
            // 
            // TXT_servorudder
            // 
            this.TXT_servorudder.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ch4out", true));
            resources.ApplyResources(this.TXT_servorudder, "TXT_servorudder");
            this.TXT_servorudder.Name = "TXT_servorudder";
            // 
            // TXT_servothrottle
            // 
            this.TXT_servothrottle.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ch3out", true));
            resources.ApplyResources(this.TXT_servothrottle, "TXT_servothrottle");
            this.TXT_servothrottle.Name = "TXT_servothrottle";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.BUT_log2kml);
            this.tabPage4.Controls.Add(this.tracklog);
            this.tabPage4.Controls.Add(this.BUT_stoptelemlog);
            this.tabPage4.Controls.Add(this.BUT_playlog);
            this.tabPage4.Controls.Add(this.BUT_loadtelem);
            this.tabPage4.Controls.Add(this.BUT_savetelem);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // BUT_log2kml
            // 
            resources.ApplyResources(this.BUT_log2kml, "BUT_log2kml");
            this.BUT_log2kml.Name = "BUT_log2kml";
            this.BUT_log2kml.UseVisualStyleBackColor = true;
            this.BUT_log2kml.Click += new System.EventHandler(this.BUT_log2kml_Click);
            // 
            // tracklog
            // 
            this.tracklog.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.tracklog, "tracklog");
            this.tracklog.Maximum = 100;
            this.tracklog.Name = "tracklog";
            this.tracklog.Scroll += new System.EventHandler(this.tracklog_Scroll);
            // 
            // BUT_stoptelemlog
            // 
            resources.ApplyResources(this.BUT_stoptelemlog, "BUT_stoptelemlog");
            this.BUT_stoptelemlog.Name = "BUT_stoptelemlog";
            this.BUT_stoptelemlog.UseVisualStyleBackColor = true;
            this.BUT_stoptelemlog.Click += new System.EventHandler(this.BUT_stoptelemlog_Click);
            // 
            // BUT_playlog
            // 
            resources.ApplyResources(this.BUT_playlog, "BUT_playlog");
            this.BUT_playlog.Name = "BUT_playlog";
            this.BUT_playlog.UseVisualStyleBackColor = true;
            this.BUT_playlog.Click += new System.EventHandler(this.BUT_playlog_Click);
            // 
            // BUT_loadtelem
            // 
            resources.ApplyResources(this.BUT_loadtelem, "BUT_loadtelem");
            this.BUT_loadtelem.Name = "BUT_loadtelem";
            this.BUT_loadtelem.UseVisualStyleBackColor = true;
            this.BUT_loadtelem.Click += new System.EventHandler(this.BUT_loadtelem_Click);
            // 
            // BUT_savetelem
            // 
            resources.ApplyResources(this.BUT_savetelem, "BUT_savetelem");
            this.BUT_savetelem.Name = "BUT_savetelem";
            this.BUT_savetelem.UseVisualStyleBackColor = true;
            this.BUT_savetelem.Click += new System.EventHandler(this.BUT_savetelem_Click);
            // 
            // tableMap
            // 
            resources.ApplyResources(this.tableMap, "tableMap");
            this.tableMap.Controls.Add(this.panel1, 0, 1);
            this.tableMap.Controls.Add(this.panel2, 0, 0);
            this.tableMap.Name = "tableMap";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TXT_lat);
            this.panel1.Controls.Add(this.Zoomlevel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TXT_long);
            this.panel1.Controls.Add(this.TXT_alt);
            this.panel1.Controls.Add(this.CHK_autopan);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // TXT_lat
            // 
            resources.ApplyResources(this.TXT_lat, "TXT_lat");
            this.TXT_lat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "lat", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "Lat 0"));
            this.TXT_lat.Name = "TXT_lat";
            // 
            // Zoomlevel
            // 
            resources.ApplyResources(this.Zoomlevel, "Zoomlevel");
            this.Zoomlevel.DecimalPlaces = 1;
            this.Zoomlevel.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.Zoomlevel.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.Zoomlevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Zoomlevel.Name = "Zoomlevel";
            this.Zoomlevel.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
            this.Zoomlevel.ValueChanged += new System.EventHandler(this.Zoomlevel_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // TXT_long
            // 
            resources.ApplyResources(this.TXT_long, "TXT_long");
            this.TXT_long.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "lng", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "Lng 0"));
            this.TXT_long.Name = "TXT_long";
            // 
            // TXT_alt
            // 
            resources.ApplyResources(this.TXT_alt, "TXT_alt");
            this.TXT_alt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "alt", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "Alt 0"));
            this.TXT_alt.Name = "TXT_alt";
            // 
            // CHK_autopan
            // 
            resources.ApplyResources(this.CHK_autopan, "CHK_autopan");
            this.CHK_autopan.Checked = true;
            this.CHK_autopan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHK_autopan.Name = "CHK_autopan";
            this.CHK_autopan.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gMapControl1);
            this.panel2.Controls.Add(this.zg1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // gMapControl1
            // 
            this.gMapControl1.BackColor = System.Drawing.Color.Transparent;
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.gMapControl1, "gMapControl1");
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.streamjpg = ((System.IO.MemoryStream)(resources.GetObject("gMapControl1.streamjpg")));
            this.gMapControl1.Zoom = 0D;
            this.gMapControl1.Click += new System.EventHandler(this.gMapControl1_Click);
            this.gMapControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gMapControl1_MouseDown);
            this.gMapControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMapControl1_MouseMove);
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
            // dataGridViewImageColumn1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewImageColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dataGridViewImageColumn1, "dataGridViewImageColumn1");
            this.dataGridViewImageColumn1.Image = global::ArdupilotMega.Properties.Resources.up;
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // dataGridViewImageColumn2
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewImageColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.dataGridViewImageColumn2, "dataGridViewImageColumn2");
            this.dataGridViewImageColumn2.Image = global::ArdupilotMega.Properties.Resources.down;
            this.dataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // ZedGraphTimer
            // 
            this.ZedGraphTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FlightData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainH);
            this.Controls.Add(this.label6);
            this.MinimumSize = new System.Drawing.Size(1008, 461);
            this.Name = "FlightData";
            this.Load += new System.EventHandler(this.GCS_Load);
            this.Resize += new System.EventHandler(this.GCS_Resize);
            this.ParentChanged += new System.EventHandler(this.FlightData_ParentChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.MainH.Panel1.ResumeLayout(false);
            this.MainH.Panel2.ResumeLayout(false);
            this.MainH.ResumeLayout(false);
            this.SubMainLeft.Panel1.ResumeLayout(false);
            this.SubMainLeft.Panel2.ResumeLayout(false);
            this.SubMainLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tracklog)).EndInit();
            this.tableMap.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Zoomlevel)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Timer ZedGraphTimer;
        private System.Windows.Forms.SplitContainer MainH;
        private System.Windows.Forms.SplitContainer SubMainLeft;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem goHereToolStripMenuItem;
        private hud.HUD hud1;
        private MyButton BUT_clear_track;
        private System.Windows.Forms.CheckBox CB_tuning;
        private MyButton BUT_RAWSensor;
        private MyButton BUTactiondo;
        private MyButton BUTrestartmission;
        private System.Windows.Forms.ComboBox CMB_action;
        private MyButton BUT_Homealt;
        private System.Windows.Forms.TrackBar tracklog;
        private MyButton BUT_playlog;
        private MyButton BUT_stoptelemlog;
        private MyButton BUT_savetelem;
        private MyButton BUT_loadtelem;
        private AGaugeApp.AGauge Gheading;
        private AGaugeApp.AGauge Galt;
        private AGaugeApp.AGauge Gspeed;
        private AGaugeApp.AGauge Gvspeed;
        private System.Windows.Forms.TableLayoutPanel tableMap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label TXT_lat;
        private System.Windows.Forms.NumericUpDown Zoomlevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TXT_long;
        private System.Windows.Forms.Label TXT_alt;
        private System.Windows.Forms.CheckBox CHK_autopan;
        private System.Windows.Forms.Panel panel2;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private ZedGraph.ZedGraphControl zg1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label TXT_control_mode;
        private System.Windows.Forms.Label TXT_WP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label TXT_wpdist;
        private System.Windows.Forms.Label TXT_bererror;
        private System.Windows.Forms.Label TXT_alterror;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label TXT_servoroll;
        private System.Windows.Forms.Label TXT_servopitch;
        private System.Windows.Forms.Label TXT_servorudder;
        private System.Windows.Forms.Label TXT_servothrottle;
        private System.Windows.Forms.ComboBox CMB_modes;
        private MyButton BUT_setmode;
        private System.Windows.Forms.ComboBox CMB_setwp;
        private MyButton BUT_setwp;
        private MyButton BUT_quickmanual;
        private MyButton BUT_quickrtl;
        private MyButton BUT_quickauto;
        private MyButton BUT_log2kml;
    }
}