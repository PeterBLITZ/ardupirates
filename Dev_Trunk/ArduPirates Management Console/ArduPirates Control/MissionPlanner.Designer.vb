<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MissionPlanner
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MissionPlanner))
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.LabelWaypointTitle = New System.Windows.Forms.Label()
        Me.SplitContainerWhole = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerMapWaypoints = New System.Windows.Forms.SplitContainer()
        Me.PanelMap = New System.Windows.Forms.Panel()
        Me.WaypointGridView = New System.Windows.Forms.DataGridView()
        Me.Num = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Latitude = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Longitude = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MoveUp = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MoveDown = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Delete = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.PanelSettings = New System.Windows.Forms.Panel()
        Me.ButtonSaveMission = New System.Windows.Forms.Button()
        Me.ButtonLoadMission = New System.Windows.Forms.Button()
        Me.PanelWaypointProperties = New System.Windows.Forms.Panel()
        Me.TextBoxWaypointTitle = New System.Windows.Forms.TextBox()
        Me.LabelWaypoint = New System.Windows.Forms.Label()
        Me.ButtonSetNewWaypointValues = New System.Windows.Forms.Button()
        Me.ComboBoxCommandType = New System.Windows.Forms.ComboBox()
        Me.LabelWaypointNumber = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelParameter7 = New System.Windows.Forms.Label()
        Me.LabelParameter6 = New System.Windows.Forms.Label()
        Me.LabelParameter5 = New System.Windows.Forms.Label()
        Me.LabelParameter4 = New System.Windows.Forms.Label()
        Me.LabelParameter3 = New System.Windows.Forms.Label()
        Me.LabelParameter2 = New System.Windows.Forms.Label()
        Me.TextBoxParameter1 = New System.Windows.Forms.TextBox()
        Me.TextBoxParameter2 = New System.Windows.Forms.TextBox()
        Me.TextBoxParameter3 = New System.Windows.Forms.TextBox()
        Me.TextBoxParameter4 = New System.Windows.Forms.TextBox()
        Me.TextBoxParameter5 = New System.Windows.Forms.TextBox()
        Me.TextBoxParameter6 = New System.Windows.Forms.TextBox()
        Me.TextBoxParameter7 = New System.Windows.Forms.TextBox()
        Me.LabelParameter1 = New System.Windows.Forms.Label()
        CType(Me.SplitContainerWhole, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerWhole.Panel1.SuspendLayout()
        Me.SplitContainerWhole.Panel2.SuspendLayout()
        Me.SplitContainerWhole.SuspendLayout()
        CType(Me.SplitContainerMapWaypoints, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMapWaypoints.Panel1.SuspendLayout()
        Me.SplitContainerMapWaypoints.Panel2.SuspendLayout()
        Me.SplitContainerMapWaypoints.SuspendLayout()
        CType(Me.WaypointGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelSettings.SuspendLayout()
        Me.PanelWaypointProperties.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'LabelWaypointTitle
        '
        Me.LabelWaypointTitle.AutoSize = True
        Me.LabelWaypointTitle.ForeColor = System.Drawing.Color.White
        Me.LabelWaypointTitle.Location = New System.Drawing.Point(34, 53)
        Me.LabelWaypointTitle.Name = "LabelWaypointTitle"
        Me.LabelWaypointTitle.Size = New System.Drawing.Size(60, 13)
        Me.LabelWaypointTitle.TabIndex = 6
        Me.LabelWaypointTitle.Text = "Description"
        Me.ToolTip1.SetToolTip(Me.LabelWaypointTitle, "Shows when hover over a marker on map")
        '
        'SplitContainerWhole
        '
        Me.SplitContainerWhole.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerWhole.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerWhole.Name = "SplitContainerWhole"
        '
        'SplitContainerWhole.Panel1
        '
        Me.SplitContainerWhole.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.SplitContainerWhole.Panel1.Controls.Add(Me.SplitContainerMapWaypoints)
        '
        'SplitContainerWhole.Panel2
        '
        Me.SplitContainerWhole.Panel2.BackColor = System.Drawing.Color.DimGray
        Me.SplitContainerWhole.Panel2.Controls.Add(Me.PanelSettings)
        Me.SplitContainerWhole.Panel2.Controls.Add(Me.PanelWaypointProperties)
        Me.SplitContainerWhole.Size = New System.Drawing.Size(784, 562)
        Me.SplitContainerWhole.SplitterDistance = 551
        Me.SplitContainerWhole.TabIndex = 0
        '
        'SplitContainerMapWaypoints
        '
        Me.SplitContainerMapWaypoints.Location = New System.Drawing.Point(-7, 2)
        Me.SplitContainerMapWaypoints.Name = "SplitContainerMapWaypoints"
        Me.SplitContainerMapWaypoints.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerMapWaypoints.Panel1
        '
        Me.SplitContainerMapWaypoints.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.SplitContainerMapWaypoints.Panel1.Controls.Add(Me.PanelMap)
        '
        'SplitContainerMapWaypoints.Panel2
        '
        Me.SplitContainerMapWaypoints.Panel2.BackColor = System.Drawing.Color.DimGray
        Me.SplitContainerMapWaypoints.Panel2.Controls.Add(Me.WaypointGridView)
        Me.SplitContainerMapWaypoints.Size = New System.Drawing.Size(555, 558)
        Me.SplitContainerMapWaypoints.SplitterDistance = 339
        Me.SplitContainerMapWaypoints.TabIndex = 7
        '
        'PanelMap
        '
        Me.PanelMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PanelMap.Location = New System.Drawing.Point(76, 35)
        Me.PanelMap.Name = "PanelMap"
        Me.PanelMap.Size = New System.Drawing.Size(356, 266)
        Me.PanelMap.TabIndex = 6
        '
        'WaypointGridView
        '
        Me.WaypointGridView.AllowUserToOrderColumns = True
        Me.WaypointGridView.BackgroundColor = System.Drawing.Color.DimGray
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.WaypointGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.WaypointGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.WaypointGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Num, Me.Latitude, Me.Longitude, Me.MoveUp, Me.MoveDown, Me.Delete})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.WaypointGridView.DefaultCellStyle = DataGridViewCellStyle2
        Me.WaypointGridView.Location = New System.Drawing.Point(-34, 4)
        Me.WaypointGridView.Name = "WaypointGridView"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.WaypointGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.WaypointGridView.ShowEditingIcon = False
        Me.WaypointGridView.Size = New System.Drawing.Size(589, 112)
        Me.WaypointGridView.TabIndex = 4
        '
        'Num
        '
        Me.Num.FillWeight = 10.0!
        Me.Num.HeaderText = "Num"
        Me.Num.Name = "Num"
        Me.Num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Num.Width = 40
        '
        'Latitude
        '
        Me.Latitude.HeaderText = "Latitude"
        Me.Latitude.Name = "Latitude"
        Me.Latitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Longitude
        '
        Me.Longitude.HeaderText = "Longitude"
        Me.Longitude.Name = "Longitude"
        Me.Longitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'MoveUp
        '
        Me.MoveUp.FillWeight = 50.0!
        Me.MoveUp.HeaderText = "Move up"
        Me.MoveUp.Name = "MoveUp"
        Me.MoveUp.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MoveUp.Width = 75
        '
        'MoveDown
        '
        Me.MoveDown.FillWeight = 50.0!
        Me.MoveDown.HeaderText = "Move down"
        Me.MoveDown.Name = "MoveDown"
        Me.MoveDown.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MoveDown.Width = 75
        '
        'Delete
        '
        Me.Delete.FillWeight = 50.0!
        Me.Delete.HeaderText = "Delete"
        Me.Delete.Name = "Delete"
        Me.Delete.Width = 60
        '
        'PanelSettings
        '
        Me.PanelSettings.BackColor = System.Drawing.Color.White
        Me.PanelSettings.Controls.Add(Me.ButtonSaveMission)
        Me.PanelSettings.Controls.Add(Me.ButtonLoadMission)
        Me.PanelSettings.Location = New System.Drawing.Point(7, 3)
        Me.PanelSettings.Name = "PanelSettings"
        Me.PanelSettings.Size = New System.Drawing.Size(222, 300)
        Me.PanelSettings.TabIndex = 7
        '
        'ButtonSaveMission
        '
        Me.ButtonSaveMission.Location = New System.Drawing.Point(0, 274)
        Me.ButtonSaveMission.Name = "ButtonSaveMission"
        Me.ButtonSaveMission.Size = New System.Drawing.Size(80, 23)
        Me.ButtonSaveMission.TabIndex = 4
        Me.ButtonSaveMission.Text = "Save mission"
        Me.ButtonSaveMission.UseVisualStyleBackColor = True
        '
        'ButtonLoadMission
        '
        Me.ButtonLoadMission.Location = New System.Drawing.Point(83, 274)
        Me.ButtonLoadMission.Name = "ButtonLoadMission"
        Me.ButtonLoadMission.Size = New System.Drawing.Size(80, 23)
        Me.ButtonLoadMission.TabIndex = 5
        Me.ButtonLoadMission.Text = "Load mission"
        Me.ButtonLoadMission.UseVisualStyleBackColor = True
        '
        'PanelWaypointProperties
        '
        Me.PanelWaypointProperties.BackColor = System.Drawing.Color.DimGray
        Me.PanelWaypointProperties.Controls.Add(Me.LabelWaypointTitle)
        Me.PanelWaypointProperties.Controls.Add(Me.TextBoxWaypointTitle)
        Me.PanelWaypointProperties.Controls.Add(Me.LabelWaypoint)
        Me.PanelWaypointProperties.Controls.Add(Me.ButtonSetNewWaypointValues)
        Me.PanelWaypointProperties.Controls.Add(Me.ComboBoxCommandType)
        Me.PanelWaypointProperties.Controls.Add(Me.LabelWaypointNumber)
        Me.PanelWaypointProperties.Controls.Add(Me.TableLayoutPanel1)
        Me.PanelWaypointProperties.Location = New System.Drawing.Point(4, 306)
        Me.PanelWaypointProperties.Name = "PanelWaypointProperties"
        Me.PanelWaypointProperties.Size = New System.Drawing.Size(225, 248)
        Me.PanelWaypointProperties.TabIndex = 5
        Me.PanelWaypointProperties.Visible = False
        '
        'TextBoxWaypointTitle
        '
        Me.TextBoxWaypointTitle.Location = New System.Drawing.Point(114, 50)
        Me.TextBoxWaypointTitle.Name = "TextBoxWaypointTitle"
        Me.TextBoxWaypointTitle.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxWaypointTitle.TabIndex = 5
        '
        'LabelWaypoint
        '
        Me.LabelWaypoint.AutoSize = True
        Me.LabelWaypoint.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelWaypoint.ForeColor = System.Drawing.Color.White
        Me.LabelWaypoint.Location = New System.Drawing.Point(27, 0)
        Me.LabelWaypoint.Name = "LabelWaypoint"
        Me.LabelWaypoint.Size = New System.Drawing.Size(115, 24)
        Me.LabelWaypoint.TabIndex = 4
        Me.LabelWaypoint.Text = "Waypoint no"
        '
        'ButtonSetNewWaypointValues
        '
        Me.ButtonSetNewWaypointValues.Location = New System.Drawing.Point(166, 221)
        Me.ButtonSetNewWaypointValues.Name = "ButtonSetNewWaypointValues"
        Me.ButtonSetNewWaypointValues.Size = New System.Drawing.Size(43, 20)
        Me.ButtonSetNewWaypointValues.TabIndex = 3
        Me.ButtonSetNewWaypointValues.Text = "Set"
        Me.ButtonSetNewWaypointValues.UseVisualStyleBackColor = True
        '
        'ComboBoxCommandType
        '
        Me.ComboBoxCommandType.FormattingEnabled = True
        Me.ComboBoxCommandType.Location = New System.Drawing.Point(31, 26)
        Me.ComboBoxCommandType.Name = "ComboBoxCommandType"
        Me.ComboBoxCommandType.Size = New System.Drawing.Size(181, 21)
        Me.ComboBoxCommandType.TabIndex = 2
        '
        'LabelWaypointNumber
        '
        Me.LabelWaypointNumber.AutoSize = True
        Me.LabelWaypointNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelWaypointNumber.ForeColor = System.Drawing.Color.White
        Me.LabelWaypointNumber.Location = New System.Drawing.Point(140, 0)
        Me.LabelWaypointNumber.Name = "LabelWaypointNumber"
        Me.LabelWaypointNumber.Size = New System.Drawing.Size(20, 24)
        Me.LabelWaypointNumber.TabIndex = 1
        Me.LabelWaypointNumber.Text = "0"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LabelParameter7, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelParameter6, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelParameter5, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelParameter4, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelParameter3, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelParameter2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxParameter1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxParameter2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxParameter3, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxParameter4, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxParameter5, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxParameter6, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxParameter7, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelParameter1, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(31, 72)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 8
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(181, 143)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'LabelParameter7
        '
        Me.LabelParameter7.AutoSize = True
        Me.LabelParameter7.ForeColor = System.Drawing.Color.White
        Me.LabelParameter7.Location = New System.Drawing.Point(3, 120)
        Me.LabelParameter7.Name = "LabelParameter7"
        Me.LabelParameter7.Size = New System.Drawing.Size(64, 13)
        Me.LabelParameter7.TabIndex = 13
        Me.LabelParameter7.Tag = "11"
        Me.LabelParameter7.Text = "Parameter 7"
        '
        'LabelParameter6
        '
        Me.LabelParameter6.AutoSize = True
        Me.LabelParameter6.ForeColor = System.Drawing.Color.White
        Me.LabelParameter6.Location = New System.Drawing.Point(3, 100)
        Me.LabelParameter6.Name = "LabelParameter6"
        Me.LabelParameter6.Size = New System.Drawing.Size(64, 13)
        Me.LabelParameter6.TabIndex = 12
        Me.LabelParameter6.Tag = "11"
        Me.LabelParameter6.Text = "Parameter 6"
        '
        'LabelParameter5
        '
        Me.LabelParameter5.AutoSize = True
        Me.LabelParameter5.ForeColor = System.Drawing.Color.White
        Me.LabelParameter5.Location = New System.Drawing.Point(3, 80)
        Me.LabelParameter5.Name = "LabelParameter5"
        Me.LabelParameter5.Size = New System.Drawing.Size(64, 13)
        Me.LabelParameter5.TabIndex = 11
        Me.LabelParameter5.Tag = "11"
        Me.LabelParameter5.Text = "Parameter 5"
        '
        'LabelParameter4
        '
        Me.LabelParameter4.AutoSize = True
        Me.LabelParameter4.ForeColor = System.Drawing.Color.White
        Me.LabelParameter4.Location = New System.Drawing.Point(3, 60)
        Me.LabelParameter4.Name = "LabelParameter4"
        Me.LabelParameter4.Size = New System.Drawing.Size(64, 13)
        Me.LabelParameter4.TabIndex = 10
        Me.LabelParameter4.Tag = "11"
        Me.LabelParameter4.Text = "Parameter 4"
        '
        'LabelParameter3
        '
        Me.LabelParameter3.AutoSize = True
        Me.LabelParameter3.ForeColor = System.Drawing.Color.White
        Me.LabelParameter3.Location = New System.Drawing.Point(3, 40)
        Me.LabelParameter3.Name = "LabelParameter3"
        Me.LabelParameter3.Size = New System.Drawing.Size(64, 13)
        Me.LabelParameter3.TabIndex = 9
        Me.LabelParameter3.Tag = "11"
        Me.LabelParameter3.Text = "Parameter 3"
        '
        'LabelParameter2
        '
        Me.LabelParameter2.AutoSize = True
        Me.LabelParameter2.ForeColor = System.Drawing.Color.White
        Me.LabelParameter2.Location = New System.Drawing.Point(3, 20)
        Me.LabelParameter2.Name = "LabelParameter2"
        Me.LabelParameter2.Size = New System.Drawing.Size(64, 13)
        Me.LabelParameter2.TabIndex = 8
        Me.LabelParameter2.Tag = "11"
        Me.LabelParameter2.Text = "Parameter 2"
        '
        'TextBoxParameter1
        '
        Me.TextBoxParameter1.Location = New System.Drawing.Point(83, 3)
        Me.TextBoxParameter1.Name = "TextBoxParameter1"
        Me.TextBoxParameter1.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxParameter1.TabIndex = 0
        '
        'TextBoxParameter2
        '
        Me.TextBoxParameter2.Location = New System.Drawing.Point(83, 23)
        Me.TextBoxParameter2.Name = "TextBoxParameter2"
        Me.TextBoxParameter2.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxParameter2.TabIndex = 1
        '
        'TextBoxParameter3
        '
        Me.TextBoxParameter3.Location = New System.Drawing.Point(83, 43)
        Me.TextBoxParameter3.Name = "TextBoxParameter3"
        Me.TextBoxParameter3.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxParameter3.TabIndex = 2
        '
        'TextBoxParameter4
        '
        Me.TextBoxParameter4.Location = New System.Drawing.Point(83, 63)
        Me.TextBoxParameter4.Name = "TextBoxParameter4"
        Me.TextBoxParameter4.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxParameter4.TabIndex = 3
        '
        'TextBoxParameter5
        '
        Me.TextBoxParameter5.Location = New System.Drawing.Point(83, 83)
        Me.TextBoxParameter5.Name = "TextBoxParameter5"
        Me.TextBoxParameter5.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxParameter5.TabIndex = 4
        '
        'TextBoxParameter6
        '
        Me.TextBoxParameter6.Location = New System.Drawing.Point(83, 103)
        Me.TextBoxParameter6.Name = "TextBoxParameter6"
        Me.TextBoxParameter6.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxParameter6.TabIndex = 5
        '
        'TextBoxParameter7
        '
        Me.TextBoxParameter7.Location = New System.Drawing.Point(83, 123)
        Me.TextBoxParameter7.Name = "TextBoxParameter7"
        Me.TextBoxParameter7.Size = New System.Drawing.Size(94, 20)
        Me.TextBoxParameter7.TabIndex = 6
        '
        'LabelParameter1
        '
        Me.LabelParameter1.AutoSize = True
        Me.LabelParameter1.ForeColor = System.Drawing.Color.White
        Me.LabelParameter1.Location = New System.Drawing.Point(3, 0)
        Me.LabelParameter1.Name = "LabelParameter1"
        Me.LabelParameter1.Size = New System.Drawing.Size(64, 13)
        Me.LabelParameter1.TabIndex = 7
        Me.LabelParameter1.Tag = "11"
        Me.LabelParameter1.Text = "Parameter 1"
        '
        'MissionPlanner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.SplitContainerWhole)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MissionPlanner"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ArduPirates Mission Planner"
        Me.SplitContainerWhole.Panel1.ResumeLayout(False)
        Me.SplitContainerWhole.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerWhole, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerWhole.ResumeLayout(False)
        Me.SplitContainerMapWaypoints.Panel1.ResumeLayout(False)
        Me.SplitContainerMapWaypoints.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerMapWaypoints, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMapWaypoints.ResumeLayout(False)
        CType(Me.WaypointGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelSettings.ResumeLayout(False)
        Me.PanelWaypointProperties.ResumeLayout(False)
        Me.PanelWaypointProperties.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents SplitContainerWhole As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerMapWaypoints As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelMap As System.Windows.Forms.Panel
    Friend WithEvents WaypointGridView As System.Windows.Forms.DataGridView
    Friend WithEvents PanelSettings As System.Windows.Forms.Panel
    Friend WithEvents ButtonSaveMission As System.Windows.Forms.Button
    Friend WithEvents ButtonLoadMission As System.Windows.Forms.Button
    Friend WithEvents PanelWaypointProperties As System.Windows.Forms.Panel
    Friend WithEvents LabelWaypoint As System.Windows.Forms.Label
    Friend WithEvents ButtonSetNewWaypointValues As System.Windows.Forms.Button
    Friend WithEvents ComboBoxCommandType As System.Windows.Forms.ComboBox
    Friend WithEvents LabelWaypointNumber As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelParameter7 As System.Windows.Forms.Label
    Friend WithEvents LabelParameter6 As System.Windows.Forms.Label
    Friend WithEvents LabelParameter5 As System.Windows.Forms.Label
    Friend WithEvents LabelParameter4 As System.Windows.Forms.Label
    Friend WithEvents LabelParameter3 As System.Windows.Forms.Label
    Friend WithEvents LabelParameter2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxParameter1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxParameter2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxParameter3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxParameter4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxParameter5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxParameter6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxParameter7 As System.Windows.Forms.TextBox
    Friend WithEvents LabelParameter1 As System.Windows.Forms.Label
    Friend WithEvents LabelWaypointTitle As System.Windows.Forms.Label
    Friend WithEvents TextBoxWaypointTitle As System.Windows.Forms.TextBox
    Friend WithEvents Num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Latitude As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Longitude As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MoveUp As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents MoveDown As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Delete As System.Windows.Forms.DataGridViewButtonColumn
End Class
