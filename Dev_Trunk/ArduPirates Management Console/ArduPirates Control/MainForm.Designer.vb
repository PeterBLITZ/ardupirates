<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title2 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series7 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series8 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series9 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title3 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Me.Serial = New System.IO.Ports.SerialPort(Me.components)
        Me.Status = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel_Connection = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel_ActivePage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel_Time = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Tabs = New System.Windows.Forms.TabControl()
        Me.Connection = New System.Windows.Forms.TabPage()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBoxMagDecl = New System.Windows.Forms.GroupBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBoxYourLongitude = New System.Windows.Forms.TextBox()
        Me.TextBoxYourLatitude = New System.Windows.Forms.TextBox()
        Me.TextBox_MagDecl = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ButtonFetchDeclination = New System.Windows.Forms.Button()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Button_Refresh_Serialports = New System.Windows.Forms.Button()
        Me.CheckBox_AutoConnect = New System.Windows.Forms.CheckBox()
        Me.Button_Connect = New System.Windows.Forms.Button()
        Me.ComboBox_Baud = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.ComboBox_Ports = New System.Windows.Forms.ComboBox()
        Me.VisualFlight = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label_yaw_gyro = New System.Windows.Forms.Label()
        Me.Label_pitch_gyro = New System.Windows.Forms.Label()
        Me.Label_roll_gyro = New System.Windows.Forms.Label()
        Me.Label_z_accel = New System.Windows.Forms.Label()
        Me.Label_pitch_accel = New System.Windows.Forms.Label()
        Me.Label_roll_accel = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SensorPlots = New System.Windows.Forms.TabPage()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Chart3 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart2 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button_Pause_Chart_2 = New System.Windows.Forms.Button()
        Me.CheckBoxYawGyro = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CheckBoxRollGyro = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CheckBoxPitchGyro = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxYawGyro = New System.Windows.Forms.TextBox()
        Me.TextBoxPitchGyro = New System.Windows.Forms.TextBox()
        Me.TextBoxRollGyro = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Button_Pause_Chart_1 = New System.Windows.Forms.Button()
        Me.LabelPitch = New System.Windows.Forms.Label()
        Me.CheckBoxYaw = New System.Windows.Forms.CheckBox()
        Me.LabelRoll = New System.Windows.Forms.Label()
        Me.CheckBoxRoll = New System.Windows.Forms.CheckBox()
        Me.LabelYaw = New System.Windows.Forms.Label()
        Me.CheckBoxPitch = New System.Windows.Forms.CheckBox()
        Me.TextBoxPitch = New System.Windows.Forms.TextBox()
        Me.TextBoxYaw = New System.Windows.Forms.TextBox()
        Me.TextBoxRoll = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxZAccel = New System.Windows.Forms.CheckBox()
        Me.Button_Pause_Chart_3 = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.CheckBoxRollAccel = New System.Windows.Forms.CheckBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.CheckBoxPitchAccel = New System.Windows.Forms.CheckBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TextBoxZAccel = New System.Windows.Forms.TextBox()
        Me.TextBoxPitchAccel = New System.Windows.Forms.TextBox()
        Me.TextBoxRollAccel = New System.Windows.Forms.TextBox()
        Me.SerialMonitor = New System.Windows.Forms.TabPage()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.SerialDataField = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button_ClearScreen = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button_Send = New System.Windows.Forms.Button()
        Me.Field_SerialCommand = New System.Windows.Forms.TextBox()
        Me.Transmitter = New System.Windows.Forms.TabPage()
        Me.Label_Slider_Errors = New System.Windows.Forms.Label()
        Me.Button_Restart_Calibration = New System.Windows.Forms.Button()
        Me.Button_Send_calibration_values = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label_Error_Pitch = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label_Error_Yaw = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label_Error_Throttle = New System.Windows.Forms.Label()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.Label_Error_AUX2 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.Label_Error_AUX1 = New System.Windows.Forms.Label()
        Me.Roll = New System.Windows.Forms.GroupBox()
        Me.Label_Error_Roll = New System.Windows.Forms.Label()
        Me.PIDTuning = New System.Windows.Forms.TabPage()
        Me.SplitContainerPID = New System.Windows.Forms.SplitContainer()
        Me.Label_PID_Mode = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.ComboBox_PIDModeSelect = New System.Windows.Forms.ComboBox()
        Me.CheckBox_PID_Magnetometer = New System.Windows.Forms.CheckBox()
        Me.Label_PID_Special_2 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PID_Special_2 = New System.Windows.Forms.NumericUpDown()
        Me.Label_PID_Special_1 = New System.Windows.Forms.Label()
        Me.NumericUpDown_PID_Special_1 = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox_PID_Yaw = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown_PID_Yaw_D = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown_PID_Yaw_I = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown_PID_Yaw_P = New System.Windows.Forms.NumericUpDown()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.Button16 = New System.Windows.Forms.Button()
        Me.Button17 = New System.Windows.Forms.Button()
        Me.Button18 = New System.Windows.Forms.Button()
        Me.Button19 = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.GroupBox_PID_Pitch = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown_PID_Pitch_D = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown_PID_Pitch_I = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown_PID_Pitch_P = New System.Windows.Forms.NumericUpDown()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.GroupBox_PID_Roll = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown_PID_Roll_D = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown_PID_Roll_I = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown_PID_Roll_P = New System.Windows.Forms.NumericUpDown()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.OnlineSupport = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button_Browser_Forward = New System.Windows.Forms.Button()
        Me.Button_Browser_Back = New System.Windows.Forms.Button()
        Me.Button_Browser_Home = New System.Windows.Forms.Button()
        Me.TabImages = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer_SerialWork = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_VisualWork = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_Chart1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_Chart2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_Chart3 = New System.Windows.Forms.Timer(Me.components)
        Me.ADIDemoTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.TrayBarIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Button_ShowMenu = New System.Windows.Forms.Button()
        Me.ArtificialHorizon1 = New WindowsFormsApplication.ArtificialHorizon()
        Me.yaw_gyro = New WindowsFormsApplication.ArduProgressBar()
        Me.pitch_gyro = New WindowsFormsApplication.ArduProgressBar()
        Me.roll_gyro = New WindowsFormsApplication.ArduProgressBar()
        Me.accel_z = New WindowsFormsApplication.ArduProgressBar()
        Me.accel_pitch = New WindowsFormsApplication.ArduProgressBar()
        Me.accel_roll = New WindowsFormsApplication.ArduProgressBar()
        Me.Slider_radio_pitch = New WindowsFormsApplication.ArduCalibrationSlider()
        Me.Slider_radio_yaw = New WindowsFormsApplication.ArduCalibrationSlider()
        Me.Slider_radio_throttle = New WindowsFormsApplication.ArduCalibrationSlider()
        Me.Slider_radio_aux2 = New WindowsFormsApplication.ArduCalibrationSlider()
        Me.Slider_radio_aux1 = New WindowsFormsApplication.ArduCalibrationSlider()
        Me.Slider_radio_roll = New WindowsFormsApplication.ArduCalibrationSlider()
        Me.Status.SuspendLayout()
        Me.Tabs.SuspendLayout()
        Me.Connection.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxMagDecl.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.VisualFlight.SuspendLayout()
        Me.SensorPlots.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SerialMonitor.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Transmitter.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.Roll.SuspendLayout()
        Me.PIDTuning.SuspendLayout()
        CType(Me.SplitContainerPID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerPID.Panel1.SuspendLayout()
        Me.SplitContainerPID.Panel2.SuspendLayout()
        Me.SplitContainerPID.SuspendLayout()
        CType(Me.NumericUpDown_PID_Special_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_PID_Special_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_PID_Yaw.SuspendLayout()
        CType(Me.NumericUpDown_PID_Yaw_D, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_PID_Yaw_I, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_PID_Yaw_P, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_PID_Pitch.SuspendLayout()
        CType(Me.NumericUpDown_PID_Pitch_D, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_PID_Pitch_I, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_PID_Pitch_P, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_PID_Roll.SuspendLayout()
        CType(Me.NumericUpDown_PID_Roll_D, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_PID_Roll_I, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown_PID_Roll_P, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.OnlineSupport.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Serial
        '
        Me.Serial.BaudRate = 115200
        Me.Serial.DtrEnable = True
        '
        'Status
        '
        Me.Status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel_Connection, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel_ActivePage, Me.ToolStripStatusLabel_Time})
        Me.Status.Location = New System.Drawing.Point(0, 540)
        Me.Status.Name = "Status"
        Me.Status.ShowItemToolTips = True
        Me.Status.Size = New System.Drawing.Size(784, 22)
        Me.Status.SizingGrip = False
        Me.Status.TabIndex = 0
        Me.Status.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel_Connection
        '
        Me.ToolStripStatusLabel_Connection.AutoSize = False
        Me.ToolStripStatusLabel_Connection.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.ToolStripStatusLabel_Connection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel_Connection.Name = "ToolStripStatusLabel_Connection"
        Me.ToolStripStatusLabel_Connection.Size = New System.Drawing.Size(250, 17)
        Me.ToolStripStatusLabel_Connection.Text = "Not connected"
        Me.ToolStripStatusLabel_Connection.ToolTipText = "Click to connect/disconnect"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.AutoSize = False
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(50, 17)
        '
        'ToolStripStatusLabel_ActivePage
        '
        Me.ToolStripStatusLabel_ActivePage.AutoSize = False
        Me.ToolStripStatusLabel_ActivePage.Name = "ToolStripStatusLabel_ActivePage"
        Me.ToolStripStatusLabel_ActivePage.Size = New System.Drawing.Size(1, 17)
        Me.ToolStripStatusLabel_ActivePage.Text = "FlightData"
        Me.ToolStripStatusLabel_ActivePage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripStatusLabel_Time
        '
        Me.ToolStripStatusLabel_Time.AutoSize = False
        Me.ToolStripStatusLabel_Time.Name = "ToolStripStatusLabel_Time"
        Me.ToolStripStatusLabel_Time.Size = New System.Drawing.Size(468, 17)
        Me.ToolStripStatusLabel_Time.Spring = True
        Me.ToolStripStatusLabel_Time.Text = "----"
        Me.ToolStripStatusLabel_Time.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Tabs
        '
        Me.Tabs.Controls.Add(Me.Connection)
        Me.Tabs.Controls.Add(Me.VisualFlight)
        Me.Tabs.Controls.Add(Me.SensorPlots)
        Me.Tabs.Controls.Add(Me.SerialMonitor)
        Me.Tabs.Controls.Add(Me.Transmitter)
        Me.Tabs.Controls.Add(Me.PIDTuning)
        Me.Tabs.Controls.Add(Me.OnlineSupport)
        Me.Tabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabs.HotTrack = True
        Me.Tabs.ImageList = Me.TabImages
        Me.Tabs.Location = New System.Drawing.Point(0, 0)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(784, 540)
        Me.Tabs.TabIndex = 3
        '
        'Connection
        '
        Me.Connection.BackColor = System.Drawing.Color.DimGray
        Me.Connection.Controls.Add(Me.TextBox1)
        Me.Connection.Controls.Add(Me.Label32)
        Me.Connection.Controls.Add(Me.PictureBox1)
        Me.Connection.Controls.Add(Me.GroupBoxMagDecl)
        Me.Connection.Controls.Add(Me.GroupBox9)
        Me.Connection.ImageKey = "(none)"
        Me.Connection.Location = New System.Drawing.Point(4, 23)
        Me.Connection.Name = "Connection"
        Me.Connection.Padding = New System.Windows.Forms.Padding(3)
        Me.Connection.Size = New System.Drawing.Size(776, 513)
        Me.Connection.TabIndex = 5
        Me.Connection.Text = "Connection"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.DimGray
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(136, 44)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(632, 272)
        Me.TextBox1.TabIndex = 18
        Me.TextBox1.Text = resources.GetString("TextBox1.Text")
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.White
        Me.Label32.Location = New System.Drawing.Point(130, 7)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(489, 33)
        Me.Label32.TabIndex = 17
        Me.Label32.Text = "ArduPirates Management Console"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.WindowsFormsApplication.My.Resources.Resources.ArduPirates
        Me.PictureBox1.Location = New System.Drawing.Point(9, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(114, 113)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'GroupBoxMagDecl
        '
        Me.GroupBoxMagDecl.BackColor = System.Drawing.Color.Transparent
        Me.GroupBoxMagDecl.Controls.Add(Me.Label14)
        Me.GroupBoxMagDecl.Controls.Add(Me.Label13)
        Me.GroupBoxMagDecl.Controls.Add(Me.Label12)
        Me.GroupBoxMagDecl.Controls.Add(Me.TextBoxYourLongitude)
        Me.GroupBoxMagDecl.Controls.Add(Me.TextBoxYourLatitude)
        Me.GroupBoxMagDecl.Controls.Add(Me.TextBox_MagDecl)
        Me.GroupBoxMagDecl.Controls.Add(Me.Label11)
        Me.GroupBoxMagDecl.Controls.Add(Me.ButtonFetchDeclination)
        Me.GroupBoxMagDecl.ForeColor = System.Drawing.Color.White
        Me.GroupBoxMagDecl.Location = New System.Drawing.Point(546, 339)
        Me.GroupBoxMagDecl.Name = "GroupBoxMagDecl"
        Me.GroupBoxMagDecl.Size = New System.Drawing.Size(222, 162)
        Me.GroupBoxMagDecl.TabIndex = 15
        Me.GroupBoxMagDecl.TabStop = False
        Me.GroupBoxMagDecl.Text = "Magnetic declination"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 19)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(121, 13)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "Enter your position: "
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(3, 70)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(57, 13)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "Longitude:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(3, 44)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(48, 13)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "Latitude:"
        '
        'TextBoxYourLongitude
        '
        Me.TextBoxYourLongitude.Location = New System.Drawing.Point(103, 70)
        Me.TextBoxYourLongitude.Name = "TextBoxYourLongitude"
        Me.TextBoxYourLongitude.Size = New System.Drawing.Size(63, 20)
        Me.TextBoxYourLongitude.TabIndex = 4
        Me.TextBoxYourLongitude.Text = "5.97"
        '
        'TextBoxYourLatitude
        '
        Me.TextBoxYourLatitude.Location = New System.Drawing.Point(103, 44)
        Me.TextBoxYourLatitude.Name = "TextBoxYourLatitude"
        Me.TextBoxYourLatitude.Size = New System.Drawing.Size(63, 20)
        Me.TextBoxYourLatitude.TabIndex = 3
        Me.TextBoxYourLatitude.Text = "51.95"
        '
        'TextBox_MagDecl
        '
        Me.TextBox_MagDecl.Location = New System.Drawing.Point(103, 96)
        Me.TextBox_MagDecl.Name = "TextBox_MagDecl"
        Me.TextBox_MagDecl.ReadOnly = True
        Me.TextBox_MagDecl.Size = New System.Drawing.Size(63, 20)
        Me.TextBox_MagDecl.TabIndex = 2
        Me.TextBox_MagDecl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 96)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(63, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Declination:"
        '
        'ButtonFetchDeclination
        '
        Me.ButtonFetchDeclination.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonFetchDeclination.ForeColor = System.Drawing.Color.Black
        Me.ButtonFetchDeclination.Location = New System.Drawing.Point(32, 122)
        Me.ButtonFetchDeclination.Name = "ButtonFetchDeclination"
        Me.ButtonFetchDeclination.Size = New System.Drawing.Size(134, 23)
        Me.ButtonFetchDeclination.TabIndex = 0
        Me.ButtonFetchDeclination.Text = "Get declination online"
        Me.ButtonFetchDeclination.UseVisualStyleBackColor = False
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Button_Refresh_Serialports)
        Me.GroupBox9.Controls.Add(Me.CheckBox_AutoConnect)
        Me.GroupBox9.Controls.Add(Me.Button_Connect)
        Me.GroupBox9.Controls.Add(Me.ComboBox_Baud)
        Me.GroupBox9.Controls.Add(Me.Label21)
        Me.GroupBox9.Controls.Add(Me.Label20)
        Me.GroupBox9.Controls.Add(Me.ComboBox_Ports)
        Me.GroupBox9.ForeColor = System.Drawing.Color.White
        Me.GroupBox9.Location = New System.Drawing.Point(8, 345)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(222, 162)
        Me.GroupBox9.TabIndex = 12
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Serial Connection"
        '
        'Button_Refresh_Serialports
        '
        Me.Button_Refresh_Serialports.Image = Global.WindowsFormsApplication.My.Resources.Resources.refresh
        Me.Button_Refresh_Serialports.Location = New System.Drawing.Point(168, 34)
        Me.Button_Refresh_Serialports.Name = "Button_Refresh_Serialports"
        Me.Button_Refresh_Serialports.Size = New System.Drawing.Size(26, 26)
        Me.Button_Refresh_Serialports.TabIndex = 17
        Me.Button_Refresh_Serialports.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button_Refresh_Serialports.UseVisualStyleBackColor = True
        '
        'CheckBox_AutoConnect
        '
        Me.CheckBox_AutoConnect.AutoSize = True
        Me.CheckBox_AutoConnect.Location = New System.Drawing.Point(12, 139)
        Me.CheckBox_AutoConnect.Name = "CheckBox_AutoConnect"
        Me.CheckBox_AutoConnect.Size = New System.Drawing.Size(175, 17)
        Me.CheckBox_AutoConnect.TabIndex = 16
        Me.CheckBox_AutoConnect.Text = "Automatically connect next time"
        Me.ToolTip.SetToolTip(Me.CheckBox_AutoConnect, "Check me to automatically connect to serial the next time this program starts.")
        Me.CheckBox_AutoConnect.UseVisualStyleBackColor = True
        '
        'Button_Connect
        '
        Me.Button_Connect.ForeColor = System.Drawing.Color.Black
        Me.Button_Connect.Location = New System.Drawing.Point(91, 94)
        Me.Button_Connect.Name = "Button_Connect"
        Me.Button_Connect.Size = New System.Drawing.Size(75, 23)
        Me.Button_Connect.TabIndex = 14
        Me.Button_Connect.Text = "Connect"
        Me.ToolTip.SetToolTip(Me.Button_Connect, "Click to connect using the selected port and speed")
        Me.Button_Connect.UseVisualStyleBackColor = True
        '
        'ComboBox_Baud
        '
        Me.ComboBox_Baud.FormattingEnabled = True
        Me.ComboBox_Baud.Items.AddRange(New Object() {"115200", "57600", "38400", "19200", "9600", "4800", "2400"})
        Me.ComboBox_Baud.Location = New System.Drawing.Point(81, 67)
        Me.ComboBox_Baud.Name = "ComboBox_Baud"
        Me.ComboBox_Baud.Size = New System.Drawing.Size(85, 21)
        Me.ComboBox_Baud.TabIndex = 15
        Me.ComboBox_Baud.Text = "115200"
        Me.ToolTip.SetToolTip(Me.ComboBox_Baud, "Select the baud rate (speed) at which you wish to connect.")
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(9, 67)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(61, 13)
        Me.Label21.TabIndex = 1
        Me.Label21.Text = "Port speed:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(9, 36)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(57, 13)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Serial port:"
        '
        'ComboBox_Ports
        '
        Me.ComboBox_Ports.FormattingEnabled = True
        Me.ComboBox_Ports.Location = New System.Drawing.Point(81, 36)
        Me.ComboBox_Ports.Name = "ComboBox_Ports"
        Me.ComboBox_Ports.Size = New System.Drawing.Size(85, 21)
        Me.ComboBox_Ports.TabIndex = 12
        Me.ToolTip.SetToolTip(Me.ComboBox_Ports, "Select the COM port where your APM is connected to.")
        '
        'VisualFlight
        '
        Me.VisualFlight.BackColor = System.Drawing.Color.Black
        Me.VisualFlight.Controls.Add(Me.ArtificialHorizon1)
        Me.VisualFlight.Controls.Add(Me.Button1)
        Me.VisualFlight.Controls.Add(Me.Label_yaw_gyro)
        Me.VisualFlight.Controls.Add(Me.Label_pitch_gyro)
        Me.VisualFlight.Controls.Add(Me.Label_roll_gyro)
        Me.VisualFlight.Controls.Add(Me.Label_z_accel)
        Me.VisualFlight.Controls.Add(Me.Label_pitch_accel)
        Me.VisualFlight.Controls.Add(Me.Label_roll_accel)
        Me.VisualFlight.Controls.Add(Me.Label16)
        Me.VisualFlight.Controls.Add(Me.Label7)
        Me.VisualFlight.Controls.Add(Me.Label15)
        Me.VisualFlight.Controls.Add(Me.Label6)
        Me.VisualFlight.Controls.Add(Me.Label5)
        Me.VisualFlight.Controls.Add(Me.Label4)
        Me.VisualFlight.Controls.Add(Me.Label3)
        Me.VisualFlight.Controls.Add(Me.Label2)
        Me.VisualFlight.Controls.Add(Me.yaw_gyro)
        Me.VisualFlight.Controls.Add(Me.pitch_gyro)
        Me.VisualFlight.Controls.Add(Me.roll_gyro)
        Me.VisualFlight.Controls.Add(Me.accel_z)
        Me.VisualFlight.Controls.Add(Me.accel_pitch)
        Me.VisualFlight.Controls.Add(Me.accel_roll)
        Me.VisualFlight.ImageKey = "(none)"
        Me.VisualFlight.Location = New System.Drawing.Point(4, 23)
        Me.VisualFlight.Margin = New System.Windows.Forms.Padding(0)
        Me.VisualFlight.Name = "VisualFlight"
        Me.VisualFlight.Size = New System.Drawing.Size(776, 513)
        Me.VisualFlight.TabIndex = 0
        Me.VisualFlight.Text = "Visual Flight"
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.Location = New System.Drawing.Point(743, 478)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(25, 23)
        Me.Button1.TabIndex = 34
        Me.ToolTip.SetToolTip(Me.Button1, "Show running demo of the ADI")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label_yaw_gyro
        '
        Me.Label_yaw_gyro.BackColor = System.Drawing.Color.Black
        Me.Label_yaw_gyro.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_yaw_gyro.ForeColor = System.Drawing.Color.White
        Me.Label_yaw_gyro.Location = New System.Drawing.Point(9, 116)
        Me.Label_yaw_gyro.Name = "Label_yaw_gyro"
        Me.Label_yaw_gyro.Size = New System.Drawing.Size(38, 13)
        Me.Label_yaw_gyro.TabIndex = 33
        Me.Label_yaw_gyro.Text = "---"
        Me.Label_yaw_gyro.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label_pitch_gyro
        '
        Me.Label_pitch_gyro.BackColor = System.Drawing.Color.Black
        Me.Label_pitch_gyro.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_pitch_gyro.ForeColor = System.Drawing.Color.White
        Me.Label_pitch_gyro.Location = New System.Drawing.Point(9, 72)
        Me.Label_pitch_gyro.Name = "Label_pitch_gyro"
        Me.Label_pitch_gyro.Size = New System.Drawing.Size(38, 13)
        Me.Label_pitch_gyro.TabIndex = 32
        Me.Label_pitch_gyro.Text = "---"
        Me.Label_pitch_gyro.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label_roll_gyro
        '
        Me.Label_roll_gyro.BackColor = System.Drawing.Color.Black
        Me.Label_roll_gyro.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_roll_gyro.ForeColor = System.Drawing.Color.White
        Me.Label_roll_gyro.Location = New System.Drawing.Point(9, 30)
        Me.Label_roll_gyro.Name = "Label_roll_gyro"
        Me.Label_roll_gyro.Size = New System.Drawing.Size(38, 13)
        Me.Label_roll_gyro.TabIndex = 31
        Me.Label_roll_gyro.Text = "---"
        Me.Label_roll_gyro.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label_z_accel
        '
        Me.Label_z_accel.BackColor = System.Drawing.Color.Black
        Me.Label_z_accel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_z_accel.ForeColor = System.Drawing.Color.White
        Me.Label_z_accel.Location = New System.Drawing.Point(628, 115)
        Me.Label_z_accel.Name = "Label_z_accel"
        Me.Label_z_accel.Size = New System.Drawing.Size(38, 13)
        Me.Label_z_accel.TabIndex = 30
        Me.Label_z_accel.Text = "---"
        Me.Label_z_accel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label_pitch_accel
        '
        Me.Label_pitch_accel.BackColor = System.Drawing.Color.Black
        Me.Label_pitch_accel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_pitch_accel.ForeColor = System.Drawing.Color.White
        Me.Label_pitch_accel.Location = New System.Drawing.Point(628, 72)
        Me.Label_pitch_accel.Name = "Label_pitch_accel"
        Me.Label_pitch_accel.Size = New System.Drawing.Size(38, 13)
        Me.Label_pitch_accel.TabIndex = 29
        Me.Label_pitch_accel.Text = "---"
        Me.Label_pitch_accel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label_roll_accel
        '
        Me.Label_roll_accel.BackColor = System.Drawing.Color.Black
        Me.Label_roll_accel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_roll_accel.ForeColor = System.Drawing.Color.White
        Me.Label_roll_accel.Location = New System.Drawing.Point(628, 30)
        Me.Label_roll_accel.Name = "Label_roll_accel"
        Me.Label_roll_accel.Size = New System.Drawing.Size(38, 13)
        Me.Label_roll_accel.TabIndex = 28
        Me.Label_roll_accel.Text = "---"
        Me.Label_roll_accel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Black
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.LimeGreen
        Me.Label16.Location = New System.Drawing.Point(94, 116)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(51, 13)
        Me.Label16.TabIndex = 26
        Me.Label16.Text = "YAW"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Black
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.LimeGreen
        Me.Label7.Location = New System.Drawing.Point(94, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "PITCH"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Black
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.LimeGreen
        Me.Label15.Location = New System.Drawing.Point(94, 30)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(51, 13)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "ROLL"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Black
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Lime
        Me.Label6.Location = New System.Drawing.Point(9, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "GYRO"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Black
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.LimeGreen
        Me.Label5.Location = New System.Drawing.Point(713, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Z"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Black
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.LimeGreen
        Me.Label4.Location = New System.Drawing.Point(713, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "PITCH"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Black
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.LimeGreen
        Me.Label3.Location = New System.Drawing.Point(713, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "ROLL"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Black
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Lime
        Me.Label2.Location = New System.Drawing.Point(628, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "ACCELEROMETER"
        '
        'SensorPlots
        '
        Me.SensorPlots.BackColor = System.Drawing.Color.DimGray
        Me.SensorPlots.Controls.Add(Me.Panel5)
        Me.SensorPlots.Controls.Add(Me.Panel4)
        Me.SensorPlots.ImageKey = "(none)"
        Me.SensorPlots.Location = New System.Drawing.Point(4, 23)
        Me.SensorPlots.Name = "SensorPlots"
        Me.SensorPlots.Padding = New System.Windows.Forms.Padding(3)
        Me.SensorPlots.Size = New System.Drawing.Size(776, 513)
        Me.SensorPlots.TabIndex = 3
        Me.SensorPlots.Text = "Sensor Plots"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Chart3)
        Me.Panel5.Controls.Add(Me.Chart2)
        Me.Panel5.Controls.Add(Me.Chart1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(635, 507)
        Me.Panel5.TabIndex = 8
        '
        'Chart3
        '
        Me.Chart3.BackColor = System.Drawing.Color.DimGray
        ChartArea1.AxisX.LabelStyle.Enabled = False
        ChartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea1.AxisX.MajorGrid.Interval = 0.0R
        ChartArea1.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray
        ChartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea1.AxisY.Interval = 512.0R
        ChartArea1.AxisY.IsLabelAutoFit = False
        ChartArea1.AxisY.IsStartedFromZero = False
        ChartArea1.AxisY.LabelStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ChartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea1.AxisY.MajorGrid.Interval = 1024.0R
        ChartArea1.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisY.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea1.AxisY.Maximum = 2048.0R
        ChartArea1.AxisY.Minimum = -2048.0R
        ChartArea1.AxisY.ScaleView.Zoomable = False
        ChartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        ChartArea1.BackSecondaryColor = System.Drawing.Color.Silver
        ChartArea1.InnerPlotPosition.Auto = False
        ChartArea1.InnerPlotPosition.Height = 100.0!
        ChartArea1.InnerPlotPosition.Width = 90.0!
        ChartArea1.InnerPlotPosition.X = 7.0!
        ChartArea1.Name = "Default"
        ChartArea1.Position.Auto = False
        ChartArea1.Position.Height = 94.0!
        ChartArea1.Position.Width = 100.0!
        ChartArea1.Position.Y = 3.0!
        ChartArea1.ShadowOffset = 3
        Me.Chart3.ChartAreas.Add(ChartArea1)
        Me.Chart3.Dock = System.Windows.Forms.DockStyle.Top
        Legend1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Legend1.BorderColor = System.Drawing.Color.Gray
        Legend1.Name = "Legend1"
        Legend1.Position.Auto = False
        Legend1.Position.Height = 34.0!
        Legend1.Position.Width = 14.0!
        Legend1.Position.X = 82.0!
        Legend1.Position.Y = 5.0!
        Legend1.ShadowOffset = 2
        Me.Chart3.Legends.Add(Legend1)
        Me.Chart3.Location = New System.Drawing.Point(0, 338)
        Me.Chart3.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.Chart3.Name = "Chart3"
        Me.Chart3.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Chart3.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright
        Series1.ChartArea = "Default"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Series1.Color = System.Drawing.Color.Red
        Series1.Legend = "Legend1"
        Series1.Name = "Pitch"
        Series1.ShadowOffset = 1
        Series2.ChartArea = "Default"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Series2.Color = System.Drawing.Color.Lime
        Series2.Legend = "Legend1"
        Series2.Name = "Roll"
        Series2.ShadowOffset = 1
        Series3.ChartArea = "Default"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Series3.Color = System.Drawing.Color.Cyan
        Series3.Legend = "Legend1"
        Series3.Name = "Z"
        Series3.ShadowOffset = 1
        Me.Chart3.Series.Add(Series1)
        Me.Chart3.Series.Add(Series2)
        Me.Chart3.Series.Add(Series3)
        Me.Chart3.Size = New System.Drawing.Size(635, 169)
        Me.Chart3.SuppressExceptions = True
        Me.Chart3.TabIndex = 10
        Me.Chart3.Text = "Chart3"
        Title1.Alignment = System.Drawing.ContentAlignment.TopLeft
        Title1.DockedToChartArea = "Default"
        Title1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left
        Title1.DockingOffset = -2
        Title1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title1.Name = "Accelerometer"
        Title1.Text = "Accelerometer"
        Me.Chart3.Titles.Add(Title1)
        Me.ToolTip.SetToolTip(Me.Chart3, "Graphical view of accelerometer sensor values")
        '
        'Chart2
        '
        Me.Chart2.BackColor = System.Drawing.Color.DimGray
        ChartArea2.AxisX.LabelStyle.Enabled = False
        ChartArea2.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea2.AxisX.MajorGrid.Interval = 0.0R
        ChartArea2.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray
        ChartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea2.AxisY.Interval = 512.0R
        ChartArea2.AxisY.IsLabelAutoFit = False
        ChartArea2.AxisY.IsStartedFromZero = False
        ChartArea2.AxisY.LabelStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ChartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea2.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea2.AxisY.MajorGrid.Interval = 1024.0R
        ChartArea2.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea2.AxisY.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea2.AxisY.Maximum = 2048.0R
        ChartArea2.AxisY.Minimum = -2048.0R
        ChartArea2.AxisY.ScaleView.Zoomable = False
        ChartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        ChartArea2.BackSecondaryColor = System.Drawing.Color.Silver
        ChartArea2.InnerPlotPosition.Auto = False
        ChartArea2.InnerPlotPosition.Height = 100.0!
        ChartArea2.InnerPlotPosition.Width = 90.0!
        ChartArea2.InnerPlotPosition.X = 7.0!
        ChartArea2.Name = "Default"
        ChartArea2.Position.Auto = False
        ChartArea2.Position.Height = 94.0!
        ChartArea2.Position.Width = 100.0!
        ChartArea2.Position.Y = 3.0!
        ChartArea2.ShadowOffset = 3
        Me.Chart2.ChartAreas.Add(ChartArea2)
        Me.Chart2.Dock = System.Windows.Forms.DockStyle.Top
        Legend2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Legend2.BorderColor = System.Drawing.Color.Gray
        Legend2.Name = "Legend1"
        Legend2.Position.Auto = False
        Legend2.Position.Height = 34.0!
        Legend2.Position.Width = 14.0!
        Legend2.Position.X = 82.0!
        Legend2.Position.Y = 5.0!
        Legend2.ShadowOffset = 2
        Me.Chart2.Legends.Add(Legend2)
        Me.Chart2.Location = New System.Drawing.Point(0, 169)
        Me.Chart2.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.Chart2.Name = "Chart2"
        Me.Chart2.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright
        Series4.ChartArea = "Default"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Series4.Color = System.Drawing.Color.Red
        Series4.Legend = "Legend1"
        Series4.Name = "Pitch"
        Series4.ShadowOffset = 1
        Series5.ChartArea = "Default"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Series5.Color = System.Drawing.Color.Lime
        Series5.Legend = "Legend1"
        Series5.Name = "Roll"
        Series5.ShadowOffset = 1
        Series6.ChartArea = "Default"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Series6.Color = System.Drawing.Color.Cyan
        Series6.Legend = "Legend1"
        Series6.Name = "Yaw"
        Series6.ShadowOffset = 1
        Me.Chart2.Series.Add(Series4)
        Me.Chart2.Series.Add(Series5)
        Me.Chart2.Series.Add(Series6)
        Me.Chart2.Size = New System.Drawing.Size(635, 169)
        Me.Chart2.SuppressExceptions = True
        Me.Chart2.TabIndex = 9
        Me.Chart2.Text = "Chart2"
        Title2.Alignment = System.Drawing.ContentAlignment.TopLeft
        Title2.DockedToChartArea = "Default"
        Title2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left
        Title2.DockingOffset = -2
        Title2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title2.Name = "Gyro"
        Title2.Text = "Gyro"
        Me.Chart2.Titles.Add(Title2)
        Me.ToolTip.SetToolTip(Me.Chart2, "Graphical view of gyro sensor values")
        '
        'Chart1
        '
        Me.Chart1.BackColor = System.Drawing.Color.DimGray
        ChartArea3.AxisX.Crossing = -1.7976931348623157E+308R
        ChartArea3.AxisX.IsMarksNextToAxis = False
        ChartArea3.AxisX.LabelStyle.Enabled = False
        ChartArea3.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea3.AxisX.MajorGrid.Interval = 0.0R
        ChartArea3.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray
        ChartArea3.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea3.AxisX.ScaleView.Zoomable = False
        ChartArea3.AxisX.ScrollBar.Enabled = False
        ChartArea3.AxisY.Interval = 45.0R
        ChartArea3.AxisY.IsLabelAutoFit = False
        ChartArea3.AxisY.IsStartedFromZero = False
        ChartArea3.AxisY.LabelStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        ChartArea3.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea3.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea3.AxisY.MajorGrid.Interval = 45.0R
        ChartArea3.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea3.AxisY.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea3.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
        ChartArea3.AxisY.Maximum = 90.0R
        ChartArea3.AxisY.Minimum = -90.0R
        ChartArea3.BackColor = System.Drawing.Color.White
        ChartArea3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        ChartArea3.BackSecondaryColor = System.Drawing.Color.Silver
        ChartArea3.InnerPlotPosition.Auto = False
        ChartArea3.InnerPlotPosition.Height = 100.0!
        ChartArea3.InnerPlotPosition.Width = 90.0!
        ChartArea3.InnerPlotPosition.X = 7.0!
        ChartArea3.Name = "Default"
        ChartArea3.Position.Auto = False
        ChartArea3.Position.Height = 94.0!
        ChartArea3.Position.Width = 100.0!
        ChartArea3.Position.Y = 3.0!
        ChartArea3.ShadowOffset = 3
        Me.Chart1.ChartAreas.Add(ChartArea3)
        Me.Chart1.Dock = System.Windows.Forms.DockStyle.Top
        Legend3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Legend3.BorderColor = System.Drawing.Color.Gray
        Legend3.Name = "Legend1"
        Legend3.Position.Auto = False
        Legend3.Position.Height = 34.0!
        Legend3.Position.Width = 14.0!
        Legend3.Position.X = 82.0!
        Legend3.Position.Y = 5.0!
        Legend3.ShadowOffset = 2
        Me.Chart1.Legends.Add(Legend3)
        Me.Chart1.Location = New System.Drawing.Point(0, 0)
        Me.Chart1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright
        Series7.ChartArea = "Default"
        Series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Series7.Color = System.Drawing.Color.Red
        Series7.Legend = "Legend1"
        Series7.Name = "Pitch"
        Series7.ShadowOffset = 1
        Series8.ChartArea = "Default"
        Series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Series8.Color = System.Drawing.Color.Lime
        Series8.Legend = "Legend1"
        Series8.Name = "Roll"
        Series8.ShadowOffset = 1
        Series9.ChartArea = "Default"
        Series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Series9.Color = System.Drawing.Color.Cyan
        Series9.Legend = "Legend1"
        Series9.Name = "Yaw"
        Series9.ShadowOffset = 1
        Me.Chart1.Series.Add(Series7)
        Me.Chart1.Series.Add(Series8)
        Me.Chart1.Series.Add(Series9)
        Me.Chart1.Size = New System.Drawing.Size(635, 169)
        Me.Chart1.SuppressExceptions = True
        Me.Chart1.TabIndex = 7
        Me.Chart1.Text = "Chart1"
        Title3.Alignment = System.Drawing.ContentAlignment.TopLeft
        Title3.DockedToChartArea = "Default"
        Title3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left
        Title3.DockingOffset = -2
        Title3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title3.Name = "Title1"
        Title3.Text = "Attitude"
        Title3.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270
        Me.Chart1.Titles.Add(Title3)
        Me.ToolTip.SetToolTip(Me.Chart1, "Graphical view of pitch, roll and yaw angles")
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.GroupBox1)
        Me.Panel4.Controls.Add(Me.GroupBox3)
        Me.Panel4.Controls.Add(Me.GroupBox2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Location = New System.Drawing.Point(638, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(135, 507)
        Me.Panel4.TabIndex = 7
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button_Pause_Chart_2)
        Me.GroupBox1.Controls.Add(Me.CheckBoxYawGyro)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.CheckBoxRollGyro)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.CheckBoxPitchGyro)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.TextBoxYawGyro)
        Me.GroupBox1.Controls.Add(Me.TextBoxPitchGyro)
        Me.GroupBox1.Controls.Add(Me.TextBoxRollGyro)
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(0, 169)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(135, 169)
        Me.GroupBox1.TabIndex = 27
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Gyro"
        '
        'Button_Pause_Chart_2
        '
        Me.Button_Pause_Chart_2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button_Pause_Chart_2.ForeColor = System.Drawing.Color.Black
        Me.Button_Pause_Chart_2.Location = New System.Drawing.Point(9, 19)
        Me.Button_Pause_Chart_2.Name = "Button_Pause_Chart_2"
        Me.Button_Pause_Chart_2.Size = New System.Drawing.Size(75, 23)
        Me.Button_Pause_Chart_2.TabIndex = 6
        Me.Button_Pause_Chart_2.Text = "Pause"
        Me.ToolTip.SetToolTip(Me.Button_Pause_Chart_2, "Pause updating the Gyro graph")
        Me.Button_Pause_Chart_2.UseVisualStyleBackColor = False
        '
        'CheckBoxYawGyro
        '
        Me.CheckBoxYawGyro.AutoSize = True
        Me.CheckBoxYawGyro.Checked = True
        Me.CheckBoxYawGyro.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxYawGyro.Location = New System.Drawing.Point(104, 97)
        Me.CheckBoxYawGyro.Name = "CheckBoxYawGyro"
        Me.CheckBoxYawGyro.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxYawGyro.TabIndex = 26
        Me.ToolTip.SetToolTip(Me.CheckBoxYawGyro, "Toggle display of this value ")
        Me.CheckBoxYawGyro.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(6, 45)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 17)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Pitch:"
        '
        'CheckBoxRollGyro
        '
        Me.CheckBoxRollGyro.AutoSize = True
        Me.CheckBoxRollGyro.Checked = True
        Me.CheckBoxRollGyro.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRollGyro.Location = New System.Drawing.Point(104, 71)
        Me.CheckBoxRollGyro.Name = "CheckBoxRollGyro"
        Me.CheckBoxRollGyro.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxRollGyro.TabIndex = 25
        Me.ToolTip.SetToolTip(Me.CheckBoxRollGyro, "Toggle display of this value ")
        Me.CheckBoxRollGyro.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(6, 71)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 17)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Roll:"
        '
        'CheckBoxPitchGyro
        '
        Me.CheckBoxPitchGyro.AutoSize = True
        Me.CheckBoxPitchGyro.Checked = True
        Me.CheckBoxPitchGyro.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxPitchGyro.Location = New System.Drawing.Point(104, 45)
        Me.CheckBoxPitchGyro.Name = "CheckBoxPitchGyro"
        Me.CheckBoxPitchGyro.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxPitchGyro.TabIndex = 24
        Me.ToolTip.SetToolTip(Me.CheckBoxPitchGyro, "Toggle display of this value ")
        Me.CheckBoxPitchGyro.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(6, 97)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 17)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Yaw:"
        '
        'TextBoxYawGyro
        '
        Me.TextBoxYawGyro.Location = New System.Drawing.Point(55, 97)
        Me.TextBoxYawGyro.Name = "TextBoxYawGyro"
        Me.TextBoxYawGyro.Size = New System.Drawing.Size(42, 20)
        Me.TextBoxYawGyro.TabIndex = 23
        Me.ToolTip.SetToolTip(Me.TextBoxYawGyro, "Yaw gyro sensor value")
        '
        'TextBoxPitchGyro
        '
        Me.TextBoxPitchGyro.Location = New System.Drawing.Point(55, 45)
        Me.TextBoxPitchGyro.Name = "TextBoxPitchGyro"
        Me.TextBoxPitchGyro.Size = New System.Drawing.Size(42, 20)
        Me.TextBoxPitchGyro.TabIndex = 21
        Me.ToolTip.SetToolTip(Me.TextBoxPitchGyro, "Pitch gyro sensor value")
        '
        'TextBoxRollGyro
        '
        Me.TextBoxRollGyro.Location = New System.Drawing.Point(55, 71)
        Me.TextBoxRollGyro.Name = "TextBoxRollGyro"
        Me.TextBoxRollGyro.Size = New System.Drawing.Size(42, 20)
        Me.TextBoxRollGyro.TabIndex = 22
        Me.ToolTip.SetToolTip(Me.TextBoxRollGyro, "Roll gyro sensor value")
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Button_Pause_Chart_1)
        Me.GroupBox3.Controls.Add(Me.LabelPitch)
        Me.GroupBox3.Controls.Add(Me.CheckBoxYaw)
        Me.GroupBox3.Controls.Add(Me.LabelRoll)
        Me.GroupBox3.Controls.Add(Me.CheckBoxRoll)
        Me.GroupBox3.Controls.Add(Me.LabelYaw)
        Me.GroupBox3.Controls.Add(Me.CheckBoxPitch)
        Me.GroupBox3.Controls.Add(Me.TextBoxPitch)
        Me.GroupBox3.Controls.Add(Me.TextBoxYaw)
        Me.GroupBox3.Controls.Add(Me.TextBoxRoll)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(135, 166)
        Me.GroupBox3.TabIndex = 29
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Attitude"
        '
        'Button_Pause_Chart_1
        '
        Me.Button_Pause_Chart_1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button_Pause_Chart_1.ForeColor = System.Drawing.Color.Black
        Me.Button_Pause_Chart_1.Location = New System.Drawing.Point(9, 24)
        Me.Button_Pause_Chart_1.Name = "Button_Pause_Chart_1"
        Me.Button_Pause_Chart_1.Size = New System.Drawing.Size(75, 23)
        Me.Button_Pause_Chart_1.TabIndex = 8
        Me.Button_Pause_Chart_1.Text = "Pause"
        Me.ToolTip.SetToolTip(Me.Button_Pause_Chart_1, "Pause updating the Attitude graph")
        Me.Button_Pause_Chart_1.UseVisualStyleBackColor = False
        '
        'LabelPitch
        '
        Me.LabelPitch.ForeColor = System.Drawing.Color.White
        Me.LabelPitch.Location = New System.Drawing.Point(6, 50)
        Me.LabelPitch.Name = "LabelPitch"
        Me.LabelPitch.Size = New System.Drawing.Size(42, 17)
        Me.LabelPitch.TabIndex = 9
        Me.LabelPitch.Text = "Pitch:"
        '
        'CheckBoxYaw
        '
        Me.CheckBoxYaw.AutoSize = True
        Me.CheckBoxYaw.Checked = True
        Me.CheckBoxYaw.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxYaw.Location = New System.Drawing.Point(104, 102)
        Me.CheckBoxYaw.Name = "CheckBoxYaw"
        Me.CheckBoxYaw.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxYaw.TabIndex = 17
        Me.ToolTip.SetToolTip(Me.CheckBoxYaw, "Toggle display of this value ")
        Me.CheckBoxYaw.UseVisualStyleBackColor = True
        '
        'LabelRoll
        '
        Me.LabelRoll.ForeColor = System.Drawing.Color.White
        Me.LabelRoll.Location = New System.Drawing.Point(6, 76)
        Me.LabelRoll.Name = "LabelRoll"
        Me.LabelRoll.Size = New System.Drawing.Size(42, 17)
        Me.LabelRoll.TabIndex = 10
        Me.LabelRoll.Text = "Roll:"
        '
        'CheckBoxRoll
        '
        Me.CheckBoxRoll.AutoSize = True
        Me.CheckBoxRoll.Checked = True
        Me.CheckBoxRoll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRoll.Location = New System.Drawing.Point(104, 76)
        Me.CheckBoxRoll.Name = "CheckBoxRoll"
        Me.CheckBoxRoll.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxRoll.TabIndex = 16
        Me.ToolTip.SetToolTip(Me.CheckBoxRoll, "Toggle display of this value ")
        Me.CheckBoxRoll.UseVisualStyleBackColor = True
        '
        'LabelYaw
        '
        Me.LabelYaw.ForeColor = System.Drawing.Color.White
        Me.LabelYaw.Location = New System.Drawing.Point(6, 102)
        Me.LabelYaw.Name = "LabelYaw"
        Me.LabelYaw.Size = New System.Drawing.Size(42, 17)
        Me.LabelYaw.TabIndex = 11
        Me.LabelYaw.Text = "Yaw:"
        '
        'CheckBoxPitch
        '
        Me.CheckBoxPitch.AutoSize = True
        Me.CheckBoxPitch.Checked = True
        Me.CheckBoxPitch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxPitch.Location = New System.Drawing.Point(104, 50)
        Me.CheckBoxPitch.Name = "CheckBoxPitch"
        Me.CheckBoxPitch.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxPitch.TabIndex = 15
        Me.ToolTip.SetToolTip(Me.CheckBoxPitch, "Toggle display of this value ")
        Me.CheckBoxPitch.UseVisualStyleBackColor = True
        '
        'TextBoxPitch
        '
        Me.TextBoxPitch.Location = New System.Drawing.Point(55, 50)
        Me.TextBoxPitch.Name = "TextBoxPitch"
        Me.TextBoxPitch.Size = New System.Drawing.Size(42, 20)
        Me.TextBoxPitch.TabIndex = 12
        Me.ToolTip.SetToolTip(Me.TextBoxPitch, "Pitch angle value")
        '
        'TextBoxYaw
        '
        Me.TextBoxYaw.Location = New System.Drawing.Point(55, 102)
        Me.TextBoxYaw.Name = "TextBoxYaw"
        Me.TextBoxYaw.Size = New System.Drawing.Size(42, 20)
        Me.TextBoxYaw.TabIndex = 14
        Me.ToolTip.SetToolTip(Me.TextBoxYaw, "Yaw angle value")
        '
        'TextBoxRoll
        '
        Me.TextBoxRoll.Location = New System.Drawing.Point(55, 76)
        Me.TextBoxRoll.Name = "TextBoxRoll"
        Me.TextBoxRoll.Size = New System.Drawing.Size(42, 20)
        Me.TextBoxRoll.TabIndex = 13
        Me.ToolTip.SetToolTip(Me.TextBoxRoll, "Roll angle value")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CheckBoxZAccel)
        Me.GroupBox2.Controls.Add(Me.Button_Pause_Chart_3)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.CheckBoxRollAccel)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.CheckBoxPitchAccel)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.TextBoxZAccel)
        Me.GroupBox2.Controls.Add(Me.TextBoxPitchAccel)
        Me.GroupBox2.Controls.Add(Me.TextBoxRollAccel)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(0, 337)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(135, 170)
        Me.GroupBox2.TabIndex = 28
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Accelerometer"
        '
        'CheckBoxZAccel
        '
        Me.CheckBoxZAccel.AutoSize = True
        Me.CheckBoxZAccel.Checked = True
        Me.CheckBoxZAccel.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxZAccel.Location = New System.Drawing.Point(104, 97)
        Me.CheckBoxZAccel.Name = "CheckBoxZAccel"
        Me.CheckBoxZAccel.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxZAccel.TabIndex = 26
        Me.ToolTip.SetToolTip(Me.CheckBoxZAccel, "Toggle display of this value ")
        Me.CheckBoxZAccel.UseVisualStyleBackColor = True
        '
        'Button_Pause_Chart_3
        '
        Me.Button_Pause_Chart_3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button_Pause_Chart_3.ForeColor = System.Drawing.Color.Black
        Me.Button_Pause_Chart_3.Location = New System.Drawing.Point(9, 19)
        Me.Button_Pause_Chart_3.Name = "Button_Pause_Chart_3"
        Me.Button_Pause_Chart_3.Size = New System.Drawing.Size(75, 23)
        Me.Button_Pause_Chart_3.TabIndex = 6
        Me.Button_Pause_Chart_3.Text = "Pause"
        Me.ToolTip.SetToolTip(Me.Button_Pause_Chart_3, "Pause updating the Accelerometer graph")
        Me.Button_Pause_Chart_3.UseVisualStyleBackColor = False
        '
        'Label17
        '
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.Location = New System.Drawing.Point(6, 45)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(42, 17)
        Me.Label17.TabIndex = 18
        Me.Label17.Text = "Pitch:"
        '
        'CheckBoxRollAccel
        '
        Me.CheckBoxRollAccel.AutoSize = True
        Me.CheckBoxRollAccel.Checked = True
        Me.CheckBoxRollAccel.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRollAccel.Location = New System.Drawing.Point(104, 71)
        Me.CheckBoxRollAccel.Name = "CheckBoxRollAccel"
        Me.CheckBoxRollAccel.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxRollAccel.TabIndex = 25
        Me.ToolTip.SetToolTip(Me.CheckBoxRollAccel, "Toggle display of this value ")
        Me.CheckBoxRollAccel.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.Color.White
        Me.Label18.Location = New System.Drawing.Point(6, 71)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(42, 17)
        Me.Label18.TabIndex = 19
        Me.Label18.Text = "Roll:"
        '
        'CheckBoxPitchAccel
        '
        Me.CheckBoxPitchAccel.AutoSize = True
        Me.CheckBoxPitchAccel.Checked = True
        Me.CheckBoxPitchAccel.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxPitchAccel.Location = New System.Drawing.Point(104, 45)
        Me.CheckBoxPitchAccel.Name = "CheckBoxPitchAccel"
        Me.CheckBoxPitchAccel.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxPitchAccel.TabIndex = 24
        Me.ToolTip.SetToolTip(Me.CheckBoxPitchAccel, "Toggle display of this value ")
        Me.CheckBoxPitchAccel.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(6, 97)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(42, 17)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "Z:"
        '
        'TextBoxZAccel
        '
        Me.TextBoxZAccel.Location = New System.Drawing.Point(55, 97)
        Me.TextBoxZAccel.Name = "TextBoxZAccel"
        Me.TextBoxZAccel.Size = New System.Drawing.Size(42, 20)
        Me.TextBoxZAccel.TabIndex = 23
        Me.ToolTip.SetToolTip(Me.TextBoxZAccel, "Z accelerometer (vertical) sensor value")
        '
        'TextBoxPitchAccel
        '
        Me.TextBoxPitchAccel.Location = New System.Drawing.Point(55, 45)
        Me.TextBoxPitchAccel.Name = "TextBoxPitchAccel"
        Me.TextBoxPitchAccel.Size = New System.Drawing.Size(42, 20)
        Me.TextBoxPitchAccel.TabIndex = 21
        Me.ToolTip.SetToolTip(Me.TextBoxPitchAccel, "Pitch accelerometer sensor value")
        '
        'TextBoxRollAccel
        '
        Me.TextBoxRollAccel.Location = New System.Drawing.Point(55, 71)
        Me.TextBoxRollAccel.Name = "TextBoxRollAccel"
        Me.TextBoxRollAccel.Size = New System.Drawing.Size(42, 20)
        Me.TextBoxRollAccel.TabIndex = 22
        Me.ToolTip.SetToolTip(Me.TextBoxRollAccel, "Roll accelerometer sensor value")
        '
        'SerialMonitor
        '
        Me.SerialMonitor.Controls.Add(Me.Panel3)
        Me.SerialMonitor.Controls.Add(Me.Panel2)
        Me.SerialMonitor.ImageKey = "(none)"
        Me.SerialMonitor.Location = New System.Drawing.Point(4, 23)
        Me.SerialMonitor.Name = "SerialMonitor"
        Me.SerialMonitor.Padding = New System.Windows.Forms.Padding(3)
        Me.SerialMonitor.Size = New System.Drawing.Size(776, 513)
        Me.SerialMonitor.TabIndex = 1
        Me.SerialMonitor.Text = "Serial Monitor"
        Me.SerialMonitor.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.SerialDataField)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(770, 472)
        Me.Panel3.TabIndex = 2
        '
        'SerialDataField
        '
        Me.SerialDataField.BackColor = System.Drawing.Color.DimGray
        Me.SerialDataField.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SerialDataField.CausesValidation = False
        Me.SerialDataField.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SerialDataField.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SerialDataField.ForeColor = System.Drawing.Color.Lime
        Me.SerialDataField.HideSelection = False
        Me.SerialDataField.Location = New System.Drawing.Point(0, 0)
        Me.SerialDataField.Multiline = True
        Me.SerialDataField.Name = "SerialDataField"
        Me.SerialDataField.ReadOnly = True
        Me.SerialDataField.Size = New System.Drawing.Size(768, 470)
        Me.SerialDataField.TabIndex = 1
        Me.ToolTip.SetToolTip(Me.SerialDataField, "Here you see all the data that comes in via the serial connection")
        Me.SerialDataField.WordWrap = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DimGray
        Me.Panel2.Controls.Add(Me.Button_ShowMenu)
        Me.Panel2.Controls.Add(Me.Button_ClearScreen)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Button_Send)
        Me.Panel2.Controls.Add(Me.Field_SerialCommand)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.ForeColor = System.Drawing.Color.LimeGreen
        Me.Panel2.Location = New System.Drawing.Point(3, 475)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(770, 35)
        Me.Panel2.TabIndex = 1
        '
        'Button_ClearScreen
        '
        Me.Button_ClearScreen.ForeColor = System.Drawing.Color.Black
        Me.Button_ClearScreen.Location = New System.Drawing.Point(677, 6)
        Me.Button_ClearScreen.Name = "Button_ClearScreen"
        Me.Button_ClearScreen.Size = New System.Drawing.Size(88, 23)
        Me.Button_ClearScreen.TabIndex = 3
        Me.Button_ClearScreen.Text = "Clear Screen"
        Me.ToolTip.SetToolTip(Me.Button_ClearScreen, "Click to clear the incoming data screen.")
        Me.Button_ClearScreen.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Lime
        Me.Label1.Location = New System.Drawing.Point(6, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(168, 18)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Enter command:\>"
        '
        'Button_Send
        '
        Me.Button_Send.Enabled = False
        Me.Button_Send.ForeColor = System.Drawing.Color.Black
        Me.Button_Send.Location = New System.Drawing.Point(515, 6)
        Me.Button_Send.Name = "Button_Send"
        Me.Button_Send.Size = New System.Drawing.Size(75, 23)
        Me.Button_Send.TabIndex = 1
        Me.Button_Send.Text = "Send"
        Me.ToolTip.SetToolTip(Me.Button_Send, "Click to send your command")
        Me.Button_Send.UseVisualStyleBackColor = True
        '
        'Field_SerialCommand
        '
        Me.Field_SerialCommand.AcceptsReturn = True
        Me.Field_SerialCommand.AcceptsTab = True
        Me.Field_SerialCommand.BackColor = System.Drawing.Color.DimGray
        Me.Field_SerialCommand.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Field_SerialCommand.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Field_SerialCommand.ForeColor = System.Drawing.Color.Lime
        Me.Field_SerialCommand.Location = New System.Drawing.Point(180, 6)
        Me.Field_SerialCommand.Name = "Field_SerialCommand"
        Me.Field_SerialCommand.Size = New System.Drawing.Size(329, 19)
        Me.Field_SerialCommand.TabIndex = 0
        Me.ToolTip.SetToolTip(Me.Field_SerialCommand, "Enter your command and press enter or click send to send your command over the se" & _
                "rial connection.")
        '
        'Transmitter
        '
        Me.Transmitter.BackColor = System.Drawing.Color.DimGray
        Me.Transmitter.Controls.Add(Me.Label_Slider_Errors)
        Me.Transmitter.Controls.Add(Me.Button_Restart_Calibration)
        Me.Transmitter.Controls.Add(Me.Button_Send_calibration_values)
        Me.Transmitter.Controls.Add(Me.GroupBox6)
        Me.Transmitter.Controls.Add(Me.GroupBox5)
        Me.Transmitter.Controls.Add(Me.GroupBox4)
        Me.Transmitter.Controls.Add(Me.GroupBox8)
        Me.Transmitter.Controls.Add(Me.GroupBox7)
        Me.Transmitter.Controls.Add(Me.Roll)
        Me.Transmitter.ImageKey = "(none)"
        Me.Transmitter.Location = New System.Drawing.Point(4, 23)
        Me.Transmitter.Name = "Transmitter"
        Me.Transmitter.Padding = New System.Windows.Forms.Padding(3)
        Me.Transmitter.Size = New System.Drawing.Size(776, 513)
        Me.Transmitter.TabIndex = 6
        Me.Transmitter.Text = "Transmitter"
        '
        'Label_Slider_Errors
        '
        Me.Label_Slider_Errors.AutoEllipsis = True
        Me.Label_Slider_Errors.BackColor = System.Drawing.Color.Black
        Me.Label_Slider_Errors.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Slider_Errors.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Slider_Errors.ForeColor = System.Drawing.Color.Red
        Me.Label_Slider_Errors.Location = New System.Drawing.Point(6, 407)
        Me.Label_Slider_Errors.Name = "Label_Slider_Errors"
        Me.Label_Slider_Errors.Size = New System.Drawing.Size(566, 103)
        Me.Label_Slider_Errors.TabIndex = 20
        '
        'Button_Restart_Calibration
        '
        Me.Button_Restart_Calibration.Enabled = False
        Me.Button_Restart_Calibration.Location = New System.Drawing.Point(661, 410)
        Me.Button_Restart_Calibration.Name = "Button_Restart_Calibration"
        Me.Button_Restart_Calibration.Size = New System.Drawing.Size(103, 23)
        Me.Button_Restart_Calibration.TabIndex = 19
        Me.Button_Restart_Calibration.Text = "Restart calibration"
        Me.ToolTip.SetToolTip(Me.Button_Restart_Calibration, "Click here to reset your sliders after a calibration error")
        Me.Button_Restart_Calibration.UseVisualStyleBackColor = True
        '
        'Button_Send_calibration_values
        '
        Me.Button_Send_calibration_values.Location = New System.Drawing.Point(661, 381)
        Me.Button_Send_calibration_values.Name = "Button_Send_calibration_values"
        Me.Button_Send_calibration_values.Size = New System.Drawing.Size(103, 23)
        Me.Button_Send_calibration_values.TabIndex = 18
        Me.Button_Send_calibration_values.Text = "Calibrate"
        Me.ToolTip.SetToolTip(Me.Button_Send_calibration_values, "Click to start calibrating your transmitter")
        Me.Button_Send_calibration_values.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.Controls.Add(Me.Label_Error_Pitch)
        Me.GroupBox6.Controls.Add(Me.Slider_radio_pitch)
        Me.GroupBox6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox6.ForeColor = System.Drawing.Color.White
        Me.GroupBox6.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(89, 398)
        Me.GroupBox6.TabIndex = 17
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Pitch"
        '
        'Label_Error_Pitch
        '
        Me.Label_Error_Pitch.BackColor = System.Drawing.Color.Black
        Me.Label_Error_Pitch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Error_Pitch.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Error_Pitch.ForeColor = System.Drawing.Color.Red
        Me.Label_Error_Pitch.Location = New System.Drawing.Point(8, 369)
        Me.Label_Error_Pitch.Name = "Label_Error_Pitch"
        Me.Label_Error_Pitch.Size = New System.Drawing.Size(73, 20)
        Me.Label_Error_Pitch.TabIndex = 6
        Me.Label_Error_Pitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.Label_Error_Yaw)
        Me.GroupBox5.Controls.Add(Me.Slider_radio_yaw)
        Me.GroupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox5.ForeColor = System.Drawing.Color.White
        Me.GroupBox5.Location = New System.Drawing.Point(103, 6)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(89, 398)
        Me.GroupBox5.TabIndex = 16
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Yaw"
        '
        'Label_Error_Yaw
        '
        Me.Label_Error_Yaw.BackColor = System.Drawing.Color.Black
        Me.Label_Error_Yaw.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Error_Yaw.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Error_Yaw.ForeColor = System.Drawing.Color.Red
        Me.Label_Error_Yaw.Location = New System.Drawing.Point(8, 369)
        Me.Label_Error_Yaw.Name = "Label_Error_Yaw"
        Me.Label_Error_Yaw.Size = New System.Drawing.Size(73, 20)
        Me.Label_Error_Yaw.TabIndex = 7
        Me.Label_Error_Yaw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.Label_Error_Throttle)
        Me.GroupBox4.Controls.Add(Me.Slider_radio_throttle)
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox4.ForeColor = System.Drawing.Color.White
        Me.GroupBox4.Location = New System.Drawing.Point(198, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(89, 398)
        Me.GroupBox4.TabIndex = 15
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Throttle"
        '
        'Label_Error_Throttle
        '
        Me.Label_Error_Throttle.BackColor = System.Drawing.Color.Black
        Me.Label_Error_Throttle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Error_Throttle.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Error_Throttle.ForeColor = System.Drawing.Color.Red
        Me.Label_Error_Throttle.Location = New System.Drawing.Point(8, 369)
        Me.Label_Error_Throttle.Name = "Label_Error_Throttle"
        Me.Label_Error_Throttle.Size = New System.Drawing.Size(73, 20)
        Me.Label_Error_Throttle.TabIndex = 8
        Me.Label_Error_Throttle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox8
        '
        Me.GroupBox8.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox8.Controls.Add(Me.Label_Error_AUX2)
        Me.GroupBox8.Controls.Add(Me.Slider_radio_aux2)
        Me.GroupBox8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox8.ForeColor = System.Drawing.Color.White
        Me.GroupBox8.Location = New System.Drawing.Point(483, 6)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(89, 398)
        Me.GroupBox8.TabIndex = 14
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "AUX2"
        '
        'Label_Error_AUX2
        '
        Me.Label_Error_AUX2.BackColor = System.Drawing.Color.Black
        Me.Label_Error_AUX2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Error_AUX2.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Error_AUX2.ForeColor = System.Drawing.Color.Red
        Me.Label_Error_AUX2.Location = New System.Drawing.Point(8, 369)
        Me.Label_Error_AUX2.Name = "Label_Error_AUX2"
        Me.Label_Error_AUX2.Size = New System.Drawing.Size(73, 20)
        Me.Label_Error_AUX2.TabIndex = 8
        Me.Label_Error_AUX2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox7
        '
        Me.GroupBox7.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox7.Controls.Add(Me.Label_Error_AUX1)
        Me.GroupBox7.Controls.Add(Me.Slider_radio_aux1)
        Me.GroupBox7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox7.ForeColor = System.Drawing.Color.White
        Me.GroupBox7.Location = New System.Drawing.Point(388, 6)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(89, 398)
        Me.GroupBox7.TabIndex = 11
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "AUX1"
        '
        'Label_Error_AUX1
        '
        Me.Label_Error_AUX1.BackColor = System.Drawing.Color.Black
        Me.Label_Error_AUX1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Error_AUX1.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Error_AUX1.ForeColor = System.Drawing.Color.Red
        Me.Label_Error_AUX1.Location = New System.Drawing.Point(8, 369)
        Me.Label_Error_AUX1.Name = "Label_Error_AUX1"
        Me.Label_Error_AUX1.Size = New System.Drawing.Size(73, 20)
        Me.Label_Error_AUX1.TabIndex = 8
        Me.Label_Error_AUX1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Roll
        '
        Me.Roll.BackColor = System.Drawing.Color.Transparent
        Me.Roll.Controls.Add(Me.Label_Error_Roll)
        Me.Roll.Controls.Add(Me.Slider_radio_roll)
        Me.Roll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Roll.ForeColor = System.Drawing.Color.White
        Me.Roll.Location = New System.Drawing.Point(293, 6)
        Me.Roll.Name = "Roll"
        Me.Roll.Size = New System.Drawing.Size(89, 398)
        Me.Roll.TabIndex = 7
        Me.Roll.TabStop = False
        Me.Roll.Text = "Roll"
        '
        'Label_Error_Roll
        '
        Me.Label_Error_Roll.BackColor = System.Drawing.Color.Black
        Me.Label_Error_Roll.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Error_Roll.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Error_Roll.ForeColor = System.Drawing.Color.Red
        Me.Label_Error_Roll.Location = New System.Drawing.Point(8, 369)
        Me.Label_Error_Roll.Name = "Label_Error_Roll"
        Me.Label_Error_Roll.Size = New System.Drawing.Size(73, 20)
        Me.Label_Error_Roll.TabIndex = 8
        Me.Label_Error_Roll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PIDTuning
        '
        Me.PIDTuning.BackColor = System.Drawing.Color.DimGray
        Me.PIDTuning.Controls.Add(Me.SplitContainerPID)
        Me.PIDTuning.ImageKey = "(none)"
        Me.PIDTuning.Location = New System.Drawing.Point(4, 23)
        Me.PIDTuning.Name = "PIDTuning"
        Me.PIDTuning.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.PIDTuning.Size = New System.Drawing.Size(776, 513)
        Me.PIDTuning.TabIndex = 4
        Me.PIDTuning.Text = "PID Tuning"
        '
        'SplitContainerPID
        '
        Me.SplitContainerPID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerPID.Location = New System.Drawing.Point(0, 5)
        Me.SplitContainerPID.Name = "SplitContainerPID"
        '
        'SplitContainerPID.Panel1
        '
        Me.SplitContainerPID.Panel1.Controls.Add(Me.Label_PID_Mode)
        '
        'SplitContainerPID.Panel2
        '
        Me.SplitContainerPID.Panel2.Controls.Add(Me.Label33)
        Me.SplitContainerPID.Panel2.Controls.Add(Me.ComboBox_PIDModeSelect)
        Me.SplitContainerPID.Panel2.Controls.Add(Me.CheckBox_PID_Magnetometer)
        Me.SplitContainerPID.Panel2.Controls.Add(Me.Label_PID_Special_2)
        Me.SplitContainerPID.Panel2.Controls.Add(Me.NumericUpDown_PID_Special_2)
        Me.SplitContainerPID.Panel2.Controls.Add(Me.Label_PID_Special_1)
        Me.SplitContainerPID.Panel2.Controls.Add(Me.NumericUpDown_PID_Special_1)
        Me.SplitContainerPID.Panel2.Controls.Add(Me.GroupBox_PID_Yaw)
        Me.SplitContainerPID.Panel2.Controls.Add(Me.GroupBox_PID_Pitch)
        Me.SplitContainerPID.Panel2.Controls.Add(Me.Label26)
        Me.SplitContainerPID.Panel2.Controls.Add(Me.GroupBox_PID_Roll)
        Me.SplitContainerPID.Size = New System.Drawing.Size(776, 508)
        Me.SplitContainerPID.SplitterDistance = 406
        Me.SplitContainerPID.TabIndex = 6
        '
        'Label_PID_Mode
        '
        Me.Label_PID_Mode.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_PID_Mode.ForeColor = System.Drawing.Color.White
        Me.Label_PID_Mode.Location = New System.Drawing.Point(8, 7)
        Me.Label_PID_Mode.Name = "Label_PID_Mode"
        Me.Label_PID_Mode.Size = New System.Drawing.Size(395, 23)
        Me.Label_PID_Mode.TabIndex = 0
        Me.Label_PID_Mode.Text = "Acrobatic Mode"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.White
        Me.Label33.Location = New System.Drawing.Point(128, 9)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(109, 13)
        Me.Label33.TabIndex = 20
        Me.Label33.Text = "Select your mode:"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ComboBox_PIDModeSelect
        '
        Me.ComboBox_PIDModeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_PIDModeSelect.Items.AddRange(New Object() {"Acrobatic Mode", "Stable Mode", "Altitude Hold", "GPS Hold", "Camera"})
        Me.ComboBox_PIDModeSelect.Location = New System.Drawing.Point(242, 9)
        Me.ComboBox_PIDModeSelect.Name = "ComboBox_PIDModeSelect"
        Me.ComboBox_PIDModeSelect.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_PIDModeSelect.TabIndex = 19
        '
        'CheckBox_PID_Magnetometer
        '
        Me.CheckBox_PID_Magnetometer.AutoSize = True
        Me.CheckBox_PID_Magnetometer.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox_PID_Magnetometer.ForeColor = System.Drawing.Color.White
        Me.CheckBox_PID_Magnetometer.Location = New System.Drawing.Point(234, 451)
        Me.CheckBox_PID_Magnetometer.Name = "CheckBox_PID_Magnetometer"
        Me.CheckBox_PID_Magnetometer.Size = New System.Drawing.Size(129, 17)
        Me.CheckBox_PID_Magnetometer.TabIndex = 18
        Me.CheckBox_PID_Magnetometer.Text = "Enable magnetometer"
        Me.CheckBox_PID_Magnetometer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox_PID_Magnetometer.UseVisualStyleBackColor = True
        Me.CheckBox_PID_Magnetometer.Visible = False
        '
        'Label_PID_Special_2
        '
        Me.Label_PID_Special_2.ForeColor = System.Drawing.Color.White
        Me.Label_PID_Special_2.Location = New System.Drawing.Point(4, 425)
        Me.Label_PID_Special_2.Name = "Label_PID_Special_2"
        Me.Label_PID_Special_2.Size = New System.Drawing.Size(233, 20)
        Me.Label_PID_Special_2.TabIndex = 17
        Me.Label_PID_Special_2.Text = "Geo correction factor:"
        Me.Label_PID_Special_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label_PID_Special_2.Visible = False
        '
        'NumericUpDown_PID_Special_2
        '
        Me.NumericUpDown_PID_Special_2.DecimalPlaces = 3
        Me.NumericUpDown_PID_Special_2.Increment = New Decimal(New Integer() {5, 0, 0, 196608})
        Me.NumericUpDown_PID_Special_2.Location = New System.Drawing.Point(243, 425)
        Me.NumericUpDown_PID_Special_2.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_PID_Special_2.Minimum = New Decimal(New Integer() {10, 0, 0, 196608})
        Me.NumericUpDown_PID_Special_2.Name = "NumericUpDown_PID_Special_2"
        Me.NumericUpDown_PID_Special_2.Size = New System.Drawing.Size(120, 20)
        Me.NumericUpDown_PID_Special_2.TabIndex = 16
        Me.NumericUpDown_PID_Special_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDown_PID_Special_2.Value = New Decimal(New Integer() {10, 0, 0, 196608})
        Me.NumericUpDown_PID_Special_2.Visible = False
        '
        'Label_PID_Special_1
        '
        Me.Label_PID_Special_1.ForeColor = System.Drawing.Color.White
        Me.Label_PID_Special_1.Location = New System.Drawing.Point(4, 399)
        Me.Label_PID_Special_1.Name = "Label_PID_Special_1"
        Me.Label_PID_Special_1.Size = New System.Drawing.Size(233, 20)
        Me.Label_PID_Special_1.TabIndex = 15
        Me.Label_PID_Special_1.Text = "Transmitter factor:"
        Me.Label_PID_Special_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'NumericUpDown_PID_Special_1
        '
        Me.NumericUpDown_PID_Special_1.DecimalPlaces = 3
        Me.NumericUpDown_PID_Special_1.Increment = New Decimal(New Integer() {5, 0, 0, 196608})
        Me.NumericUpDown_PID_Special_1.Location = New System.Drawing.Point(243, 399)
        Me.NumericUpDown_PID_Special_1.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown_PID_Special_1.Minimum = New Decimal(New Integer() {10, 0, 0, 196608})
        Me.NumericUpDown_PID_Special_1.Name = "NumericUpDown_PID_Special_1"
        Me.NumericUpDown_PID_Special_1.Size = New System.Drawing.Size(120, 20)
        Me.NumericUpDown_PID_Special_1.TabIndex = 14
        Me.NumericUpDown_PID_Special_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDown_PID_Special_1.Value = New Decimal(New Integer() {10, 0, 0, 196608})
        '
        'GroupBox_PID_Yaw
        '
        Me.GroupBox_PID_Yaw.Controls.Add(Me.NumericUpDown_PID_Yaw_D)
        Me.GroupBox_PID_Yaw.Controls.Add(Me.NumericUpDown_PID_Yaw_I)
        Me.GroupBox_PID_Yaw.Controls.Add(Me.NumericUpDown_PID_Yaw_P)
        Me.GroupBox_PID_Yaw.Controls.Add(Me.Button14)
        Me.GroupBox_PID_Yaw.Controls.Add(Me.Button15)
        Me.GroupBox_PID_Yaw.Controls.Add(Me.Button16)
        Me.GroupBox_PID_Yaw.Controls.Add(Me.Button17)
        Me.GroupBox_PID_Yaw.Controls.Add(Me.Button18)
        Me.GroupBox_PID_Yaw.Controls.Add(Me.Button19)
        Me.GroupBox_PID_Yaw.Controls.Add(Me.Label29)
        Me.GroupBox_PID_Yaw.Controls.Add(Me.Label30)
        Me.GroupBox_PID_Yaw.Controls.Add(Me.Label31)
        Me.GroupBox_PID_Yaw.ForeColor = System.Drawing.Color.White
        Me.GroupBox_PID_Yaw.Location = New System.Drawing.Point(3, 278)
        Me.GroupBox_PID_Yaw.Name = "GroupBox_PID_Yaw"
        Me.GroupBox_PID_Yaw.Size = New System.Drawing.Size(360, 115)
        Me.GroupBox_PID_Yaw.TabIndex = 13
        Me.GroupBox_PID_Yaw.TabStop = False
        Me.GroupBox_PID_Yaw.Text = "Yaw PID Values"
        '
        'NumericUpDown_PID_Yaw_D
        '
        Me.NumericUpDown_PID_Yaw_D.DecimalPlaces = 3
        Me.NumericUpDown_PID_Yaw_D.Increment = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Yaw_D.Location = New System.Drawing.Point(116, 79)
        Me.NumericUpDown_PID_Yaw_D.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.NumericUpDown_PID_Yaw_D.Minimum = New Decimal(New Integer() {20, 0, 0, -2147483648})
        Me.NumericUpDown_PID_Yaw_D.Name = "NumericUpDown_PID_Yaw_D"
        Me.NumericUpDown_PID_Yaw_D.Size = New System.Drawing.Size(101, 20)
        Me.NumericUpDown_PID_Yaw_D.TabIndex = 20
        Me.NumericUpDown_PID_Yaw_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip.SetToolTip(Me.NumericUpDown_PID_Yaw_D, "Use your arrow keys to increase and decrease.")
        '
        'NumericUpDown_PID_Yaw_I
        '
        Me.NumericUpDown_PID_Yaw_I.DecimalPlaces = 3
        Me.NumericUpDown_PID_Yaw_I.Increment = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Yaw_I.Location = New System.Drawing.Point(115, 53)
        Me.NumericUpDown_PID_Yaw_I.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.NumericUpDown_PID_Yaw_I.Name = "NumericUpDown_PID_Yaw_I"
        Me.NumericUpDown_PID_Yaw_I.Size = New System.Drawing.Size(101, 20)
        Me.NumericUpDown_PID_Yaw_I.TabIndex = 19
        Me.NumericUpDown_PID_Yaw_I.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip.SetToolTip(Me.NumericUpDown_PID_Yaw_I, "Use your arrow keys to increase and decrease.")
        '
        'NumericUpDown_PID_Yaw_P
        '
        Me.NumericUpDown_PID_Yaw_P.DecimalPlaces = 3
        Me.NumericUpDown_PID_Yaw_P.Increment = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Yaw_P.Location = New System.Drawing.Point(115, 27)
        Me.NumericUpDown_PID_Yaw_P.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.NumericUpDown_PID_Yaw_P.Minimum = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Yaw_P.Name = "NumericUpDown_PID_Yaw_P"
        Me.NumericUpDown_PID_Yaw_P.Size = New System.Drawing.Size(101, 20)
        Me.NumericUpDown_PID_Yaw_P.TabIndex = 18
        Me.NumericUpDown_PID_Yaw_P.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip.SetToolTip(Me.NumericUpDown_PID_Yaw_P, "Use your arrow keys to increase and decrease.")
        Me.NumericUpDown_PID_Yaw_P.Value = New Decimal(New Integer() {1950, 0, 0, 196608})
        '
        'Button14
        '
        Me.Button14.ForeColor = System.Drawing.Color.Black
        Me.Button14.Location = New System.Drawing.Point(279, 76)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(54, 23)
        Me.Button14.TabIndex = 11
        Me.Button14.Text = "+ (F6)"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'Button15
        '
        Me.Button15.ForeColor = System.Drawing.Color.Black
        Me.Button15.Location = New System.Drawing.Point(219, 76)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(54, 23)
        Me.Button15.TabIndex = 10
        Me.Button15.Text = "- (F5)"
        Me.Button15.UseVisualStyleBackColor = True
        '
        'Button16
        '
        Me.Button16.ForeColor = System.Drawing.Color.Black
        Me.Button16.Location = New System.Drawing.Point(279, 50)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(54, 23)
        Me.Button16.TabIndex = 9
        Me.Button16.Text = "+ (F4)"
        Me.Button16.UseVisualStyleBackColor = True
        '
        'Button17
        '
        Me.Button17.ForeColor = System.Drawing.Color.Black
        Me.Button17.Location = New System.Drawing.Point(219, 50)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(54, 23)
        Me.Button17.TabIndex = 8
        Me.Button17.Text = "- (F3)"
        Me.Button17.UseVisualStyleBackColor = True
        '
        'Button18
        '
        Me.Button18.ForeColor = System.Drawing.Color.Black
        Me.Button18.Location = New System.Drawing.Point(279, 24)
        Me.Button18.Name = "Button18"
        Me.Button18.Size = New System.Drawing.Size(54, 23)
        Me.Button18.TabIndex = 7
        Me.Button18.Text = "+ (F2)"
        Me.Button18.UseVisualStyleBackColor = True
        '
        'Button19
        '
        Me.Button19.ForeColor = System.Drawing.Color.Black
        Me.Button19.Location = New System.Drawing.Point(219, 24)
        Me.Button19.Name = "Button19"
        Me.Button19.Size = New System.Drawing.Size(54, 23)
        Me.Button19.TabIndex = 6
        Me.Button19.Text = "- (F1)"
        Me.Button19.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(7, 78)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(100, 18)
        Me.Label29.TabIndex = 2
        Me.Label29.Text = "Derivative:"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(7, 52)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(100, 18)
        Me.Label30.TabIndex = 1
        Me.Label30.Text = "Integral:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.Location = New System.Drawing.Point(7, 29)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(100, 18)
        Me.Label31.TabIndex = 0
        Me.Label31.Text = "Proportional:"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox_PID_Pitch
        '
        Me.GroupBox_PID_Pitch.Controls.Add(Me.NumericUpDown_PID_Pitch_D)
        Me.GroupBox_PID_Pitch.Controls.Add(Me.NumericUpDown_PID_Pitch_I)
        Me.GroupBox_PID_Pitch.Controls.Add(Me.NumericUpDown_PID_Pitch_P)
        Me.GroupBox_PID_Pitch.Controls.Add(Me.Button8)
        Me.GroupBox_PID_Pitch.Controls.Add(Me.Button9)
        Me.GroupBox_PID_Pitch.Controls.Add(Me.Button10)
        Me.GroupBox_PID_Pitch.Controls.Add(Me.Button11)
        Me.GroupBox_PID_Pitch.Controls.Add(Me.Button12)
        Me.GroupBox_PID_Pitch.Controls.Add(Me.Button13)
        Me.GroupBox_PID_Pitch.Controls.Add(Me.Label22)
        Me.GroupBox_PID_Pitch.Controls.Add(Me.Label27)
        Me.GroupBox_PID_Pitch.Controls.Add(Me.Label28)
        Me.GroupBox_PID_Pitch.ForeColor = System.Drawing.Color.White
        Me.GroupBox_PID_Pitch.Location = New System.Drawing.Point(4, 157)
        Me.GroupBox_PID_Pitch.Name = "GroupBox_PID_Pitch"
        Me.GroupBox_PID_Pitch.Size = New System.Drawing.Size(360, 115)
        Me.GroupBox_PID_Pitch.TabIndex = 12
        Me.GroupBox_PID_Pitch.TabStop = False
        Me.GroupBox_PID_Pitch.Text = "Pitch PID Values"
        '
        'NumericUpDown_PID_Pitch_D
        '
        Me.NumericUpDown_PID_Pitch_D.DecimalPlaces = 3
        Me.NumericUpDown_PID_Pitch_D.Increment = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Pitch_D.Location = New System.Drawing.Point(114, 79)
        Me.NumericUpDown_PID_Pitch_D.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.NumericUpDown_PID_Pitch_D.Minimum = New Decimal(New Integer() {20, 0, 0, -2147483648})
        Me.NumericUpDown_PID_Pitch_D.Name = "NumericUpDown_PID_Pitch_D"
        Me.NumericUpDown_PID_Pitch_D.Size = New System.Drawing.Size(101, 20)
        Me.NumericUpDown_PID_Pitch_D.TabIndex = 20
        Me.NumericUpDown_PID_Pitch_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip.SetToolTip(Me.NumericUpDown_PID_Pitch_D, "Use your arrow keys to increase and decrease.")
        '
        'NumericUpDown_PID_Pitch_I
        '
        Me.NumericUpDown_PID_Pitch_I.DecimalPlaces = 3
        Me.NumericUpDown_PID_Pitch_I.Increment = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Pitch_I.Location = New System.Drawing.Point(113, 53)
        Me.NumericUpDown_PID_Pitch_I.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.NumericUpDown_PID_Pitch_I.Name = "NumericUpDown_PID_Pitch_I"
        Me.NumericUpDown_PID_Pitch_I.Size = New System.Drawing.Size(101, 20)
        Me.NumericUpDown_PID_Pitch_I.TabIndex = 19
        Me.NumericUpDown_PID_Pitch_I.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip.SetToolTip(Me.NumericUpDown_PID_Pitch_I, "Use your arrow keys to increase and decrease.")
        '
        'NumericUpDown_PID_Pitch_P
        '
        Me.NumericUpDown_PID_Pitch_P.DecimalPlaces = 3
        Me.NumericUpDown_PID_Pitch_P.Increment = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Pitch_P.Location = New System.Drawing.Point(113, 27)
        Me.NumericUpDown_PID_Pitch_P.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.NumericUpDown_PID_Pitch_P.Minimum = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Pitch_P.Name = "NumericUpDown_PID_Pitch_P"
        Me.NumericUpDown_PID_Pitch_P.Size = New System.Drawing.Size(101, 20)
        Me.NumericUpDown_PID_Pitch_P.TabIndex = 18
        Me.NumericUpDown_PID_Pitch_P.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip.SetToolTip(Me.NumericUpDown_PID_Pitch_P, "Use your arrow keys to increase and decrease.")
        Me.NumericUpDown_PID_Pitch_P.Value = New Decimal(New Integer() {1950, 0, 0, 196608})
        '
        'Button8
        '
        Me.Button8.ForeColor = System.Drawing.Color.Black
        Me.Button8.Location = New System.Drawing.Point(279, 76)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(54, 23)
        Me.Button8.TabIndex = 11
        Me.Button8.Text = "+ (F6)"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.ForeColor = System.Drawing.Color.Black
        Me.Button9.Location = New System.Drawing.Point(219, 76)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(54, 23)
        Me.Button9.TabIndex = 10
        Me.Button9.Text = "- (F5)"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.ForeColor = System.Drawing.Color.Black
        Me.Button10.Location = New System.Drawing.Point(279, 50)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(54, 23)
        Me.Button10.TabIndex = 9
        Me.Button10.Text = "+ (F4)"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.ForeColor = System.Drawing.Color.Black
        Me.Button11.Location = New System.Drawing.Point(219, 50)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(54, 23)
        Me.Button11.TabIndex = 8
        Me.Button11.Text = "- (F3)"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.ForeColor = System.Drawing.Color.Black
        Me.Button12.Location = New System.Drawing.Point(279, 24)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(54, 23)
        Me.Button12.TabIndex = 7
        Me.Button12.Text = "+ (F2)"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Button13
        '
        Me.Button13.ForeColor = System.Drawing.Color.Black
        Me.Button13.Location = New System.Drawing.Point(219, 24)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(54, 23)
        Me.Button13.TabIndex = 6
        Me.Button13.Text = "- (F1)"
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(7, 78)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(100, 18)
        Me.Label22.TabIndex = 2
        Me.Label22.Text = "Derivative:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(7, 52)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(100, 18)
        Me.Label27.TabIndex = 1
        Me.Label27.Text = "Integral:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.Location = New System.Drawing.Point(7, 29)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(100, 18)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "Proportional:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Black
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label26.ForeColor = System.Drawing.Color.White
        Me.Label26.Location = New System.Drawing.Point(4, 476)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(360, 32)
        Me.Label26.TabIndex = 7
        Me.Label26.Text = "Ready for action..."
        '
        'GroupBox_PID_Roll
        '
        Me.GroupBox_PID_Roll.Controls.Add(Me.NumericUpDown_PID_Roll_D)
        Me.GroupBox_PID_Roll.Controls.Add(Me.NumericUpDown_PID_Roll_I)
        Me.GroupBox_PID_Roll.Controls.Add(Me.NumericUpDown_PID_Roll_P)
        Me.GroupBox_PID_Roll.Controls.Add(Me.Button6)
        Me.GroupBox_PID_Roll.Controls.Add(Me.Button7)
        Me.GroupBox_PID_Roll.Controls.Add(Me.Button4)
        Me.GroupBox_PID_Roll.Controls.Add(Me.Button5)
        Me.GroupBox_PID_Roll.Controls.Add(Me.Button3)
        Me.GroupBox_PID_Roll.Controls.Add(Me.Button2)
        Me.GroupBox_PID_Roll.Controls.Add(Me.Label25)
        Me.GroupBox_PID_Roll.Controls.Add(Me.Label24)
        Me.GroupBox_PID_Roll.Controls.Add(Me.Label23)
        Me.GroupBox_PID_Roll.ForeColor = System.Drawing.Color.White
        Me.GroupBox_PID_Roll.Location = New System.Drawing.Point(4, 36)
        Me.GroupBox_PID_Roll.Name = "GroupBox_PID_Roll"
        Me.GroupBox_PID_Roll.Size = New System.Drawing.Size(360, 115)
        Me.GroupBox_PID_Roll.TabIndex = 6
        Me.GroupBox_PID_Roll.TabStop = False
        Me.GroupBox_PID_Roll.Text = "Roll PID Values"
        '
        'NumericUpDown_PID_Roll_D
        '
        Me.NumericUpDown_PID_Roll_D.DecimalPlaces = 3
        Me.NumericUpDown_PID_Roll_D.Increment = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Roll_D.Location = New System.Drawing.Point(113, 78)
        Me.NumericUpDown_PID_Roll_D.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.NumericUpDown_PID_Roll_D.Minimum = New Decimal(New Integer() {20, 0, 0, -2147483648})
        Me.NumericUpDown_PID_Roll_D.Name = "NumericUpDown_PID_Roll_D"
        Me.NumericUpDown_PID_Roll_D.Size = New System.Drawing.Size(101, 20)
        Me.NumericUpDown_PID_Roll_D.TabIndex = 17
        Me.NumericUpDown_PID_Roll_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip.SetToolTip(Me.NumericUpDown_PID_Roll_D, "Use your arrow keys to increase and decrease.")
        '
        'NumericUpDown_PID_Roll_I
        '
        Me.NumericUpDown_PID_Roll_I.DecimalPlaces = 3
        Me.NumericUpDown_PID_Roll_I.Increment = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Roll_I.Location = New System.Drawing.Point(112, 52)
        Me.NumericUpDown_PID_Roll_I.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.NumericUpDown_PID_Roll_I.Name = "NumericUpDown_PID_Roll_I"
        Me.NumericUpDown_PID_Roll_I.Size = New System.Drawing.Size(101, 20)
        Me.NumericUpDown_PID_Roll_I.TabIndex = 16
        Me.NumericUpDown_PID_Roll_I.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip.SetToolTip(Me.NumericUpDown_PID_Roll_I, "Use your arrow keys to increase and decrease.")
        '
        'NumericUpDown_PID_Roll_P
        '
        Me.NumericUpDown_PID_Roll_P.DecimalPlaces = 3
        Me.NumericUpDown_PID_Roll_P.Increment = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Roll_P.Location = New System.Drawing.Point(112, 26)
        Me.NumericUpDown_PID_Roll_P.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.NumericUpDown_PID_Roll_P.Minimum = New Decimal(New Integer() {50, 0, 0, 196608})
        Me.NumericUpDown_PID_Roll_P.Name = "NumericUpDown_PID_Roll_P"
        Me.NumericUpDown_PID_Roll_P.Size = New System.Drawing.Size(101, 20)
        Me.NumericUpDown_PID_Roll_P.TabIndex = 15
        Me.NumericUpDown_PID_Roll_P.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip.SetToolTip(Me.NumericUpDown_PID_Roll_P, "Use your arrow keys to increase and decrease.")
        Me.NumericUpDown_PID_Roll_P.Value = New Decimal(New Integer() {1950, 0, 0, 196608})
        '
        'Button6
        '
        Me.Button6.ForeColor = System.Drawing.Color.Black
        Me.Button6.Location = New System.Drawing.Point(279, 76)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(54, 23)
        Me.Button6.TabIndex = 11
        Me.Button6.Text = "+ (F6)"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.ForeColor = System.Drawing.Color.Black
        Me.Button7.Location = New System.Drawing.Point(219, 76)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(54, 23)
        Me.Button7.TabIndex = 10
        Me.Button7.Text = "- (F5)"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.ForeColor = System.Drawing.Color.Black
        Me.Button4.Location = New System.Drawing.Point(279, 50)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(54, 23)
        Me.Button4.TabIndex = 9
        Me.Button4.Text = "+ (F4)"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.ForeColor = System.Drawing.Color.Black
        Me.Button5.Location = New System.Drawing.Point(219, 50)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(54, 23)
        Me.Button5.TabIndex = 8
        Me.Button5.Text = "- (F3)"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.ForeColor = System.Drawing.Color.Black
        Me.Button3.Location = New System.Drawing.Point(279, 24)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(54, 23)
        Me.Button3.TabIndex = 7
        Me.Button3.Text = "+ (F2)"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.ForeColor = System.Drawing.Color.Black
        Me.Button2.Location = New System.Drawing.Point(219, 24)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(54, 23)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "- (F1)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(7, 78)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(100, 18)
        Me.Label25.TabIndex = 2
        Me.Label25.Text = "Derivative:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(7, 52)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(100, 18)
        Me.Label24.TabIndex = 1
        Me.Label24.Text = "Integral:"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(7, 29)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(100, 18)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "Proportional:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'OnlineSupport
        '
        Me.OnlineSupport.Controls.Add(Me.Panel1)
        Me.OnlineSupport.ImageKey = "(none)"
        Me.OnlineSupport.Location = New System.Drawing.Point(4, 23)
        Me.OnlineSupport.Name = "OnlineSupport"
        Me.OnlineSupport.Padding = New System.Windows.Forms.Padding(3)
        Me.OnlineSupport.Size = New System.Drawing.Size(776, 513)
        Me.OnlineSupport.TabIndex = 7
        Me.OnlineSupport.Text = "Online Support"
        Me.OnlineSupport.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Button_Browser_Forward)
        Me.Panel1.Controls.Add(Me.Button_Browser_Back)
        Me.Panel1.Controls.Add(Me.Button_Browser_Home)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(770, 28)
        Me.Panel1.TabIndex = 0
        '
        'Button_Browser_Forward
        '
        Me.Button_Browser_Forward.Location = New System.Drawing.Point(170, 2)
        Me.Button_Browser_Forward.Name = "Button_Browser_Forward"
        Me.Button_Browser_Forward.Size = New System.Drawing.Size(75, 23)
        Me.Button_Browser_Forward.TabIndex = 2
        Me.Button_Browser_Forward.Text = "Forward"
        Me.Button_Browser_Forward.UseVisualStyleBackColor = True
        '
        'Button_Browser_Back
        '
        Me.Button_Browser_Back.Location = New System.Drawing.Point(88, 2)
        Me.Button_Browser_Back.Name = "Button_Browser_Back"
        Me.Button_Browser_Back.Size = New System.Drawing.Size(75, 23)
        Me.Button_Browser_Back.TabIndex = 1
        Me.Button_Browser_Back.Text = "Back"
        Me.Button_Browser_Back.UseVisualStyleBackColor = True
        '
        'Button_Browser_Home
        '
        Me.Button_Browser_Home.Location = New System.Drawing.Point(6, 2)
        Me.Button_Browser_Home.Name = "Button_Browser_Home"
        Me.Button_Browser_Home.Size = New System.Drawing.Size(75, 23)
        Me.Button_Browser_Home.TabIndex = 0
        Me.Button_Browser_Home.Text = "Home"
        Me.Button_Browser_Home.UseVisualStyleBackColor = True
        '
        'TabImages
        '
        Me.TabImages.ImageStream = CType(resources.GetObject("TabImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TabImages.TransparentColor = System.Drawing.Color.Transparent
        Me.TabImages.Images.SetKeyName(0, "Pirates_0.ico")
        Me.TabImages.Images.SetKeyName(1, "Pirates_1.ico")
        Me.TabImages.Images.SetKeyName(2, "Pirates_2.ico")
        Me.TabImages.Images.SetKeyName(3, "Pirates_3.ico")
        Me.TabImages.Images.SetKeyName(4, "Pirates_4.ico")
        Me.TabImages.Images.SetKeyName(5, "Pirates_5.ico")
        Me.TabImages.Images.SetKeyName(6, "Pirates_6.ico")
        Me.TabImages.Images.SetKeyName(7, "Pirates_7.ico")
        Me.TabImages.Images.SetKeyName(8, "Pirates_8.ico")
        Me.TabImages.Images.SetKeyName(9, "Pirates_9.ico")
        '
        'Timer_SerialWork
        '
        Me.Timer_SerialWork.Enabled = True
        Me.Timer_SerialWork.Interval = 10
        '
        'Timer_VisualWork
        '
        Me.Timer_VisualWork.Enabled = True
        Me.Timer_VisualWork.Interval = 5
        '
        'Timer_Chart1
        '
        Me.Timer_Chart1.Enabled = True
        '
        'Timer_Chart2
        '
        Me.Timer_Chart2.Enabled = True
        '
        'Timer_Chart3
        '
        Me.Timer_Chart3.Enabled = True
        '
        'ADIDemoTimer
        '
        Me.ADIDemoTimer.Interval = 50
        '
        'TrayBarIcon
        '
        Me.TrayBarIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.TrayBarIcon.BalloonTipText = "... is currently running."
        Me.TrayBarIcon.BalloonTipTitle = "ArduPirates Management Console"
        Me.TrayBarIcon.Icon = CType(resources.GetObject("TrayBarIcon.Icon"), System.Drawing.Icon)
        Me.TrayBarIcon.Text = "ArduPirates Management Console"
        Me.TrayBarIcon.Visible = True
        '
        'Button_ShowMenu
        '
        Me.Button_ShowMenu.ForeColor = System.Drawing.Color.Black
        Me.Button_ShowMenu.Location = New System.Drawing.Point(596, 6)
        Me.Button_ShowMenu.Name = "Button_ShowMenu"
        Me.Button_ShowMenu.Size = New System.Drawing.Size(75, 23)
        Me.Button_ShowMenu.TabIndex = 4
        Me.Button_ShowMenu.Text = "Menu"
        Me.ToolTip.SetToolTip(Me.Button_ShowMenu, "Click to show the CLI main menu")
        Me.Button_ShowMenu.UseVisualStyleBackColor = True
        '
        'ArtificialHorizon1
        '
        Me.ArtificialHorizon1.altitude = -64.0R
        Me.ArtificialHorizon1.AutoScroll = True
        Me.ArtificialHorizon1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.ArtificialHorizon1.BackColor = System.Drawing.Color.Transparent
        Me.ArtificialHorizon1.ForeColor = System.Drawing.Color.Transparent
        Me.ArtificialHorizon1.Location = New System.Drawing.Point(151, 28)
        Me.ArtificialHorizon1.Name = "ArtificialHorizon1"
        Me.ArtificialHorizon1.pitch_angle = 0.0R
        Me.ArtificialHorizon1.roll_angle = 0.0R
        Me.ArtificialHorizon1.Size = New System.Drawing.Size(473, 473)
        Me.ArtificialHorizon1.TabIndex = 35
        Me.ToolTip.SetToolTip(Me.ArtificialHorizon1, "ADI or Attitude Director Indicator or Artificial Horizon")
        Me.ArtificialHorizon1.yaw_angle = 23.0R
        '
        'yaw_gyro
        '
        Me.yaw_gyro.BackColor = System.Drawing.Color.Transparent
        Me.yaw_gyro.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.yaw_gyro.ForeColor = System.Drawing.Color.Transparent
        Me.yaw_gyro.Location = New System.Drawing.Point(11, 132)
        Me.yaw_gyro.Maximum = 2048
        Me.yaw_gyro.Minimum = 0
        Me.yaw_gyro.Name = "yaw_gyro"
        Me.yaw_gyro.ProgressBarColor = System.Drawing.Color.DarkGreen
        Me.yaw_gyro.Size = New System.Drawing.Size(134, 23)
        Me.yaw_gyro.TabIndex = 17
        Me.ToolTip.SetToolTip(Me.yaw_gyro, "Yaw gyro")
        Me.yaw_gyro.Value = 60
        '
        'pitch_gyro
        '
        Me.pitch_gyro.BackColor = System.Drawing.Color.Transparent
        Me.pitch_gyro.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.pitch_gyro.ForeColor = System.Drawing.Color.Transparent
        Me.pitch_gyro.Location = New System.Drawing.Point(11, 89)
        Me.pitch_gyro.Maximum = 2048
        Me.pitch_gyro.Minimum = 0
        Me.pitch_gyro.Name = "pitch_gyro"
        Me.pitch_gyro.ProgressBarColor = System.Drawing.Color.DarkGreen
        Me.pitch_gyro.Size = New System.Drawing.Size(134, 23)
        Me.pitch_gyro.TabIndex = 16
        Me.ToolTip.SetToolTip(Me.pitch_gyro, "Pitch gyro")
        Me.pitch_gyro.Value = 60
        '
        'roll_gyro
        '
        Me.roll_gyro.BackColor = System.Drawing.Color.Transparent
        Me.roll_gyro.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.roll_gyro.ForeColor = System.Drawing.Color.Transparent
        Me.roll_gyro.Location = New System.Drawing.Point(11, 46)
        Me.roll_gyro.Maximum = 2048
        Me.roll_gyro.Minimum = 0
        Me.roll_gyro.Name = "roll_gyro"
        Me.roll_gyro.ProgressBarColor = System.Drawing.Color.DarkGreen
        Me.roll_gyro.Size = New System.Drawing.Size(134, 23)
        Me.roll_gyro.TabIndex = 15
        Me.ToolTip.SetToolTip(Me.roll_gyro, "Roll gyro")
        Me.roll_gyro.Value = 60
        '
        'accel_z
        '
        Me.accel_z.BackColor = System.Drawing.Color.Transparent
        Me.accel_z.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.accel_z.ForeColor = System.Drawing.Color.Transparent
        Me.accel_z.Location = New System.Drawing.Point(630, 132)
        Me.accel_z.Maximum = 2048
        Me.accel_z.Minimum = 0
        Me.accel_z.Name = "accel_z"
        Me.accel_z.ProgressBarColor = System.Drawing.Color.DarkGreen
        Me.accel_z.Size = New System.Drawing.Size(134, 23)
        Me.accel_z.TabIndex = 14
        Me.ToolTip.SetToolTip(Me.accel_z, "Z accelerometer")
        Me.accel_z.Value = 60
        '
        'accel_pitch
        '
        Me.accel_pitch.BackColor = System.Drawing.Color.Transparent
        Me.accel_pitch.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.accel_pitch.ForeColor = System.Drawing.Color.Transparent
        Me.accel_pitch.Location = New System.Drawing.Point(630, 89)
        Me.accel_pitch.Maximum = 2048
        Me.accel_pitch.Minimum = 0
        Me.accel_pitch.Name = "accel_pitch"
        Me.accel_pitch.ProgressBarColor = System.Drawing.Color.DarkGreen
        Me.accel_pitch.Size = New System.Drawing.Size(134, 23)
        Me.accel_pitch.TabIndex = 13
        Me.ToolTip.SetToolTip(Me.accel_pitch, "Pitch accelerometer")
        Me.accel_pitch.Value = 60
        '
        'accel_roll
        '
        Me.accel_roll.BackColor = System.Drawing.Color.Transparent
        Me.accel_roll.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.accel_roll.ForeColor = System.Drawing.Color.Transparent
        Me.accel_roll.Location = New System.Drawing.Point(630, 46)
        Me.accel_roll.Maximum = 2048
        Me.accel_roll.Minimum = 0
        Me.accel_roll.Name = "accel_roll"
        Me.accel_roll.ProgressBarColor = System.Drawing.Color.DarkGreen
        Me.accel_roll.Size = New System.Drawing.Size(134, 23)
        Me.accel_roll.TabIndex = 12
        Me.ToolTip.SetToolTip(Me.accel_roll, "Roll accelerometer")
        Me.accel_roll.Value = 1500
        '
        'Slider_radio_pitch
        '
        Me.Slider_radio_pitch.BackColor = System.Drawing.Color.Transparent
        Me.Slider_radio_pitch.BorderColor = System.Drawing.Color.Maroon
        Me.Slider_radio_pitch.CalibrationCenterColor = System.Drawing.Color.Gray
        Me.Slider_radio_pitch.CalibrationMaxColor = System.Drawing.Color.SteelBlue
        Me.Slider_radio_pitch.CalibrationMinColor = System.Drawing.Color.LightSeaGreen
        Me.Slider_radio_pitch.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Slider_radio_pitch.Location = New System.Drawing.Point(10, 16)
        Me.Slider_radio_pitch.Maximum = 2100
        Me.Slider_radio_pitch.Minimum = 900
        Me.Slider_radio_pitch.Name = "Slider_radio_pitch"
        Me.Slider_radio_pitch.ProgressBarColor = System.Drawing.Color.CadetBlue
        Me.Slider_radio_pitch.ShowInverted = True
        Me.Slider_radio_pitch.Size = New System.Drawing.Size(70, 350)
        Me.Slider_radio_pitch.TabIndex = 5
        Me.ToolTip.SetToolTip(Me.Slider_radio_pitch, "Pitch channel - controls forward/backward motion")
        Me.Slider_radio_pitch.Value = 1500
        Me.Slider_radio_pitch.Vertical = False
        '
        'Slider_radio_yaw
        '
        Me.Slider_radio_yaw.BackColor = System.Drawing.Color.Transparent
        Me.Slider_radio_yaw.BorderColor = System.Drawing.Color.LightGray
        Me.Slider_radio_yaw.CalibrationCenterColor = System.Drawing.Color.Gray
        Me.Slider_radio_yaw.CalibrationMaxColor = System.Drawing.Color.SteelBlue
        Me.Slider_radio_yaw.CalibrationMinColor = System.Drawing.Color.LightSeaGreen
        Me.Slider_radio_yaw.Location = New System.Drawing.Point(10, 16)
        Me.Slider_radio_yaw.Maximum = 2100
        Me.Slider_radio_yaw.Minimum = 900
        Me.Slider_radio_yaw.Name = "Slider_radio_yaw"
        Me.Slider_radio_yaw.ProgressBarColor = System.Drawing.Color.CadetBlue
        Me.Slider_radio_yaw.ShowInverted = False
        Me.Slider_radio_yaw.Size = New System.Drawing.Size(70, 350)
        Me.Slider_radio_yaw.TabIndex = 4
        Me.ToolTip.SetToolTip(Me.Slider_radio_yaw, "Yaw channel - controls left/right motion (naysaying)")
        Me.Slider_radio_yaw.Value = 1500
        Me.Slider_radio_yaw.Vertical = False
        '
        'Slider_radio_throttle
        '
        Me.Slider_radio_throttle.BackColor = System.Drawing.Color.Transparent
        Me.Slider_radio_throttle.BorderColor = System.Drawing.Color.LightGray
        Me.Slider_radio_throttle.CalibrationCenterColor = System.Drawing.Color.Gray
        Me.Slider_radio_throttle.CalibrationMaxColor = System.Drawing.Color.SteelBlue
        Me.Slider_radio_throttle.CalibrationMinColor = System.Drawing.Color.LightSeaGreen
        Me.Slider_radio_throttle.Location = New System.Drawing.Point(9, 16)
        Me.Slider_radio_throttle.Maximum = 2100
        Me.Slider_radio_throttle.Minimum = 900
        Me.Slider_radio_throttle.Name = "Slider_radio_throttle"
        Me.Slider_radio_throttle.ProgressBarColor = System.Drawing.Color.CadetBlue
        Me.Slider_radio_throttle.ShowInverted = False
        Me.Slider_radio_throttle.Size = New System.Drawing.Size(70, 350)
        Me.Slider_radio_throttle.TabIndex = 3
        Me.ToolTip.SetToolTip(Me.Slider_radio_throttle, "Throttle channel - controls up/down motion")
        Me.Slider_radio_throttle.Value = 1500
        Me.Slider_radio_throttle.Vertical = False
        '
        'Slider_radio_aux2
        '
        Me.Slider_radio_aux2.BackColor = System.Drawing.Color.Transparent
        Me.Slider_radio_aux2.BorderColor = System.Drawing.Color.LightGray
        Me.Slider_radio_aux2.CalibrationCenterColor = System.Drawing.Color.Gray
        Me.Slider_radio_aux2.CalibrationMaxColor = System.Drawing.Color.SteelBlue
        Me.Slider_radio_aux2.CalibrationMinColor = System.Drawing.Color.LightSeaGreen
        Me.Slider_radio_aux2.Enabled = False
        Me.Slider_radio_aux2.Location = New System.Drawing.Point(10, 16)
        Me.Slider_radio_aux2.Maximum = 2100
        Me.Slider_radio_aux2.Minimum = 900
        Me.Slider_radio_aux2.Name = "Slider_radio_aux2"
        Me.Slider_radio_aux2.ProgressBarColor = System.Drawing.Color.CadetBlue
        Me.Slider_radio_aux2.ShowInverted = False
        Me.Slider_radio_aux2.Size = New System.Drawing.Size(70, 350)
        Me.Slider_radio_aux2.TabIndex = 1
        Me.ToolTip.SetToolTip(Me.Slider_radio_aux2, "AUX2 channel - controls GPS hold mode")
        Me.Slider_radio_aux2.Value = 1500
        Me.Slider_radio_aux2.Vertical = False
        '
        'Slider_radio_aux1
        '
        Me.Slider_radio_aux1.BackColor = System.Drawing.Color.Transparent
        Me.Slider_radio_aux1.BorderColor = System.Drawing.Color.LightGray
        Me.Slider_radio_aux1.CalibrationCenterColor = System.Drawing.Color.Gray
        Me.Slider_radio_aux1.CalibrationMaxColor = System.Drawing.Color.SteelBlue
        Me.Slider_radio_aux1.CalibrationMinColor = System.Drawing.Color.LightSeaGreen
        Me.Slider_radio_aux1.Location = New System.Drawing.Point(10, 16)
        Me.Slider_radio_aux1.Maximum = 2100
        Me.Slider_radio_aux1.Minimum = 900
        Me.Slider_radio_aux1.Name = "Slider_radio_aux1"
        Me.Slider_radio_aux1.ProgressBarColor = System.Drawing.Color.CadetBlue
        Me.Slider_radio_aux1.ShowInverted = False
        Me.Slider_radio_aux1.Size = New System.Drawing.Size(70, 350)
        Me.Slider_radio_aux1.TabIndex = 2
        Me.ToolTip.SetToolTip(Me.Slider_radio_aux1, "AUX1 channel - controls altitude hold mode")
        Me.Slider_radio_aux1.Value = 1500
        Me.Slider_radio_aux1.Vertical = False
        '
        'Slider_radio_roll
        '
        Me.Slider_radio_roll.BackColor = System.Drawing.Color.Transparent
        Me.Slider_radio_roll.BorderColor = System.Drawing.Color.LightGray
        Me.Slider_radio_roll.CalibrationCenterColor = System.Drawing.Color.Gray
        Me.Slider_radio_roll.CalibrationMaxColor = System.Drawing.Color.SteelBlue
        Me.Slider_radio_roll.CalibrationMinColor = System.Drawing.Color.LightSeaGreen
        Me.Slider_radio_roll.Location = New System.Drawing.Point(10, 16)
        Me.Slider_radio_roll.Maximum = 2100
        Me.Slider_radio_roll.Minimum = 900
        Me.Slider_radio_roll.Name = "Slider_radio_roll"
        Me.Slider_radio_roll.ProgressBarColor = System.Drawing.Color.CadetBlue
        Me.Slider_radio_roll.ShowInverted = False
        Me.Slider_radio_roll.Size = New System.Drawing.Size(70, 350)
        Me.Slider_radio_roll.TabIndex = 6
        Me.ToolTip.SetToolTip(Me.Slider_radio_roll, "Roll channel - controls sideways roll motion")
        Me.Slider_radio_roll.Value = 1500
        Me.Slider_radio_roll.Vertical = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.Tabs)
        Me.Controls.Add(Me.Status)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ArduPirates Management Console"
        Me.Status.ResumeLayout(False)
        Me.Status.PerformLayout()
        Me.Tabs.ResumeLayout(False)
        Me.Connection.ResumeLayout(False)
        Me.Connection.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxMagDecl.ResumeLayout(False)
        Me.GroupBoxMagDecl.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.VisualFlight.ResumeLayout(False)
        Me.VisualFlight.PerformLayout()
        Me.SensorPlots.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.SerialMonitor.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Transmitter.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.Roll.ResumeLayout(False)
        Me.PIDTuning.ResumeLayout(False)
        Me.SplitContainerPID.Panel1.ResumeLayout(False)
        Me.SplitContainerPID.Panel2.ResumeLayout(False)
        Me.SplitContainerPID.Panel2.PerformLayout()
        CType(Me.SplitContainerPID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerPID.ResumeLayout(False)
        CType(Me.NumericUpDown_PID_Special_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_PID_Special_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_PID_Yaw.ResumeLayout(False)
        CType(Me.NumericUpDown_PID_Yaw_D, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_PID_Yaw_I, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_PID_Yaw_P, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_PID_Pitch.ResumeLayout(False)
        CType(Me.NumericUpDown_PID_Pitch_D, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_PID_Pitch_I, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_PID_Pitch_P, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_PID_Roll.ResumeLayout(False)
        CType(Me.NumericUpDown_PID_Roll_D, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_PID_Roll_I, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown_PID_Roll_P, System.ComponentModel.ISupportInitialize).EndInit()
        Me.OnlineSupport.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Serial As System.IO.Ports.SerialPort
    Friend WithEvents Status As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel_Connection As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents VisualFlight As System.Windows.Forms.TabPage
    Friend WithEvents SerialMonitor As System.Windows.Forms.TabPage
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents SerialDataField As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button_Send As System.Windows.Forms.Button
    Friend WithEvents Field_SerialCommand As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStripStatusLabel_ActivePage As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Button_ClearScreen As System.Windows.Forms.Button
    Friend WithEvents Timer_SerialWork As System.Windows.Forms.Timer
    Friend WithEvents Timer_VisualWork As System.Windows.Forms.Timer
    Friend WithEvents SensorPlots As System.Windows.Forms.TabPage
    Friend WithEvents Timer_Chart1 As System.Windows.Forms.Timer
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Chart2 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Button_Pause_Chart_1 As System.Windows.Forms.Button
    Friend WithEvents Button_Pause_Chart_3 As System.Windows.Forms.Button
    Friend WithEvents TextBoxYaw As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxRoll As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPitch As System.Windows.Forms.TextBox
    Friend WithEvents LabelYaw As System.Windows.Forms.Label
    Friend WithEvents LabelRoll As System.Windows.Forms.Label
    Friend WithEvents LabelPitch As System.Windows.Forms.Label
    Friend WithEvents CheckBoxYaw As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxRoll As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPitch As System.Windows.Forms.CheckBox
    Friend WithEvents Timer_Chart2 As System.Windows.Forms.Timer
    Friend WithEvents Timer_Chart3 As System.Windows.Forms.Timer
    Friend WithEvents CheckBoxYawGyro As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxRollGyro As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPitchGyro As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxYawGyro As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxRollGyro As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPitchGyro As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents PIDTuning As System.Windows.Forms.TabPage
    Friend WithEvents accel_z As WindowsFormsApplication.ArduProgressBar
    Friend WithEvents accel_pitch As WindowsFormsApplication.ArduProgressBar
    Friend WithEvents accel_roll As WindowsFormsApplication.ArduProgressBar
    Friend WithEvents yaw_gyro As WindowsFormsApplication.ArduProgressBar
    Friend WithEvents pitch_gyro As WindowsFormsApplication.ArduProgressBar
    Friend WithEvents roll_gyro As WindowsFormsApplication.ArduProgressBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Connection As System.Windows.Forms.TabPage
    Friend WithEvents ComboBox_Baud As System.Windows.Forms.ComboBox
    Friend WithEvents Button_Connect As System.Windows.Forms.Button
    Friend WithEvents ComboBox_Ports As System.Windows.Forms.ComboBox
    Friend WithEvents Label_yaw_gyro As System.Windows.Forms.Label
    Friend WithEvents Label_pitch_gyro As System.Windows.Forms.Label
    Friend WithEvents Label_roll_gyro As System.Windows.Forms.Label
    Friend WithEvents Label_z_accel As System.Windows.Forms.Label
    Friend WithEvents Label_pitch_accel As System.Windows.Forms.Label
    Friend WithEvents Label_roll_accel As System.Windows.Forms.Label
    Friend WithEvents TabImages As System.Windows.Forms.ImageList
    Friend WithEvents Transmitter As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button_Pause_Chart_2 As System.Windows.Forms.Button
    Friend WithEvents CheckBoxZAccel As System.Windows.Forms.CheckBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxRollAccel As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxPitchAccel As System.Windows.Forms.CheckBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TextBoxZAccel As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPitchAccel As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxRollAccel As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Chart3 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Slider_radio_aux2 As WindowsFormsApplication.ArduCalibrationSlider
    Friend WithEvents Slider_radio_roll As WindowsFormsApplication.ArduCalibrationSlider
    Friend WithEvents Slider_radio_pitch As WindowsFormsApplication.ArduCalibrationSlider
    Friend WithEvents Slider_radio_yaw As WindowsFormsApplication.ArduCalibrationSlider
    Friend WithEvents Slider_radio_throttle As WindowsFormsApplication.ArduCalibrationSlider
    Friend WithEvents Slider_radio_aux1 As WindowsFormsApplication.ArduCalibrationSlider
    Friend WithEvents OnlineSupport As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Roll As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Button_Send_calibration_values As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ADIDemoTimer As System.Windows.Forms.Timer
    Friend WithEvents ArtificialHorizon1 As WindowsFormsApplication.ArtificialHorizon
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents CheckBox_AutoConnect As System.Windows.Forms.CheckBox
    Friend WithEvents ToolStripStatusLabel_Time As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label_Error_Pitch As System.Windows.Forms.Label
    Friend WithEvents Label_Error_Yaw As System.Windows.Forms.Label
    Friend WithEvents Label_Error_Throttle As System.Windows.Forms.Label
    Friend WithEvents Label_Error_AUX2 As System.Windows.Forms.Label
    Friend WithEvents Label_Error_AUX1 As System.Windows.Forms.Label
    Friend WithEvents Label_Error_Roll As System.Windows.Forms.Label
    Friend WithEvents Button_Restart_Calibration As System.Windows.Forms.Button
    Friend WithEvents Label_Slider_Errors As System.Windows.Forms.Label
    Friend WithEvents Button_Refresh_Serialports As System.Windows.Forms.Button
    Friend WithEvents GroupBoxMagDecl As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBoxYourLongitude As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxYourLatitude As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_MagDecl As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ButtonFetchDeclination As System.Windows.Forms.Button
    Friend WithEvents SplitContainerPID As System.Windows.Forms.SplitContainer
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents GroupBox_PID_Roll As System.Windows.Forms.GroupBox
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents GroupBox_PID_Pitch As System.Windows.Forms.GroupBox
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents GroupBox_PID_Yaw As System.Windows.Forms.GroupBox
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents Button16 As System.Windows.Forms.Button
    Friend WithEvents Button17 As System.Windows.Forms.Button
    Friend WithEvents Button18 As System.Windows.Forms.Button
    Friend WithEvents Button19 As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown_PID_Special_1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents CheckBox_PID_Magnetometer As System.Windows.Forms.CheckBox
    Friend WithEvents Label_PID_Special_2 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown_PID_Special_2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label_PID_Special_1 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown_PID_Roll_P As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown_PID_Roll_D As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown_PID_Roll_I As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_PIDModeSelect As System.Windows.Forms.ComboBox
    Friend WithEvents Label_PID_Mode As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown_PID_Yaw_D As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown_PID_Yaw_I As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown_PID_Yaw_P As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown_PID_Pitch_D As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown_PID_Pitch_I As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown_PID_Pitch_P As System.Windows.Forms.NumericUpDown
    Friend WithEvents TrayBarIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button_Browser_Forward As System.Windows.Forms.Button
    Friend WithEvents Button_Browser_Back As System.Windows.Forms.Button
    Friend WithEvents Button_Browser_Home As System.Windows.Forms.Button
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Button_ShowMenu As System.Windows.Forms.Button

End Class
