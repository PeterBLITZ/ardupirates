Imports System.Threading
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports Touchless.Vision.Camera
Imports WebCamLib


Public Class MainForm

    'Variables needed for Serial Communications
    Dim comPorts As Array 'Com ports enumerated into here
    Dim comPortName As String = "COM1" 'This will hold COM1, COM2 or other (the selected one)
    Dim comPortSpeed As String = "9600" 'Holds the baudrate
    Dim comAutoConnect As Boolean = False 'Do you want to automatically connect next time we start ?
    Dim txBuffer As String 'Buffer for data to be sent
    Dim rxBuffer As String 'Buffer for received data
    Dim comOpen As Boolean 'Defines whether the COM port is open
    Dim rxBufferMarker As Integer 'this is the position marker for the rxBuffer
    Dim rxBufferLineOfData As String 'this will be the first complete line of data in the buffer, terminated with a line end.

    'Program Logic variables
    Dim controlThread As Thread
    Dim CurrentView As String
    Dim SerialDataField_Busy As Boolean = False


    'Web browser
    Dim Browser As WebBrowser

    'For ADI demo
    Dim democounter_a As Integer = 0
    Dim democounter_b As Integer = 0
    Dim democounter_c As Integer = 0

    'Flight data variables
    Dim ADI_roll_angle As Double
    Dim ADI_pitch_angle As Double
    Dim ADI_yaw_angle As Double
    Dim CurrentTab As String
    Dim roll_gyro_value As Double
    Dim pitch_gyro_value As Double
    Dim yaw_gyro_value As Double
    Dim accel_roll_value As Double
    Dim accel_pitch_value As Double
    Dim accel_z_value As Double
    Dim press_baro_altitude As Double

    'Transmitter variables
    Dim radio_roll_value As Double = 1500
    Dim radio_pitch_value As Double = 1500
    Dim radio_yaw_value As Double = 1500
    Dim radio_throttle_value As Double = 1500
    Dim radio_aux1_value As Double = 1500
    Dim radio_aux2_value As Double = 1500
    Dim radio_roll_mid_value As Double = 1500
    Dim radio_pitch_mid_value As Double = 1500
    Dim radio_yaw_mid_value As Double = 1500


    'Serial Monitor variables
    Dim SerialDataLine As String

    'Program Logic variables
    Dim currentViewChanged As Boolean

    'Threads

    '----
    Private random As New Random()
    Private pointIndex1 As Integer = 0
    Private pointIndex2 As Integer = 0
    Private pointIndex3 As Integer = 0

    ' Define some variables
    Dim numberOfPointsInChart As Integer = 40
    Dim numberOfPointsAfterRemoval As Integer = 40

    ' Simulate adding new data points
    Dim numberOfPointsAddedMin As Integer = 1
    Dim numberOfPointsAddedMax As Integer = 1
    Dim pointNumber As Integer

    'Camera (live feed)
    Dim frameSource As CameraFrameSource
    Dim latestFrame As Bitmap

    'This function helps prevent flicker while updating the serialdatafield
    <DllImport("user32.dll")> _
    Public Shared Function LockWindowUpdate(ByVal hWndLock As IntPtr) As Boolean
    End Function

    Sub Enumerate_SerialPorts()
        comPorts = IO.Ports.SerialPort.GetPortNames()
        ComboBox_Ports.Items.Clear()
        For i = 0 To UBound(comPorts)
            ComboBox_Ports.Items.Add(comPorts(i))
        Next
        ComboBox_Ports.Sorted = True
        'Set ComboBox1 text to first available port
        ComboBox_Ports.Text = ComboBox_Ports.Items.Item(0)

        'Set SerialPort1 portname to first available port
        Serial.PortName = ComboBox_Ports.Text
        Serial.BaudRate = ComboBox_Baud.Text

        'Set SerialPort1 parity etc.
        Serial.Parity = IO.Ports.Parity.None
        Serial.StopBits = IO.Ports.StopBits.One
        Serial.DataBits = 8

    End Sub

    Sub Enumerate_Cameras()
        For Each cam As Camera In CameraService.AvailableCameras
            ComboBox_SelectCamera.Items.Add(cam)
        Next

        'Found at least one camera, show panel in connection tab
        If ComboBox_SelectCamera.Items.Count > 0 Then
            GroupBoxCamera.Visible = True
        End If
    End Sub

    Private Function RandomNumber(ByVal min As Integer, ByVal max As Integer) As Integer
        Dim random As New Random()
        Return random.Next(min, max)
    End Function 'RandomNumber 
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Me.Text & " v." & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & " build " & My.Application.Info.Version.Build

        'Populate dropdown for camera select
        Enumerate_Cameras()

        'Enumerate available Com ports and add to the list in the statusbar
        Enumerate_SerialPorts()

        'Now automatically select the one we used last time
        comPortName = GetSetting("ArduPirates Management Console", "Communication", "Port")
        comPortSpeed = GetSetting("ArduPirates Management Console", "Communication", "Baudrate")
        comAutoConnect = GetSetting("ArduPirates Management Console", "Communication", "Autoconnect")
        ComboBox_Ports.SelectedIndex = ComboBox_Ports.FindStringExact(comPortName)
        ComboBox_Baud.SelectedIndex = ComboBox_Baud.FindStringExact(comPortSpeed)
        CheckBox_AutoConnect.Checked = comAutoConnect

        SplashForm.Close()

        'Automatically connect if so configured
        If comAutoConnect Then Serial1_Connect()

        'Set starting value of the Mode Select combobox on the PID Tuning tab page
        ComboBox_PIDModeSelect.Text = "Acrobatic Mode"

        'Position all controls in the correct place 
        ArtificialHorizon1.Top = 10
        VisualFlight.Left = 0



    End Sub



    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Send.Click
        rxBuffer = ""

        'Write the data in the 
        If Serial.IsOpen Then Serial.Write(Field_SerialCommand.Text) Else MsgBox("Please connect first !")
        Field_SerialCommand.Focus()

    End Sub
    '*********************************************************************
    '
    '           SERIAL RX RECEIVER
    '
    '*********************************************************************
    Private Sub Serial1_DataReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles Serial.DataReceived
        'This sub gets called automatically when the com port recieves some data

        If Serial.IsOpen Then
            'Move recieved data into the buffer, datastream always ends with a newline
            rxBuffer = rxBuffer & Replace(Serial.ReadExisting, vbLf, "") 'Adds the incoming data to the end of the buffer.
        End If
    End Sub

    '*********************************************************************
    '
    '       HANDLE THE INCOMING SERIAL DATA BUFFER (RXBUFFER)
    '
    '*********************************************************************
    Private Sub Timer_SerialWork_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_SerialWork.Tick
        'We need to handle that data that has been received into the buffer (RX)
        '
        'Don't go here if the Serial port is closed
        If Serial.IsOpen Then


            '
            'The buffer is just one big string with all the incoming data appended to it at the end.
            'Going through it, we remove each line of data terminated with a line end from the
            'beginning of the buffer after handling it. 

            'If there is at least one full data line (terminated with a line end) in the receive buffer, handle it.
            If InStr(rxBuffer, vbCr) > 0 Then
                'There seems to be data in the rxBuffer

                'Check where the first complete line of data at the beginning of the buffer ends.
                'This will be our marker (rxBufferMarker). Everything behind it will be handled in a later pass through this loop.
                rxBufferMarker = InStr(rxBuffer, vbCr)

                'Extract this first line.
                rxBufferLineOfData = Replace(Mid(rxBuffer, 1, rxBufferMarker), vbCr, vbCrLf) 'And also replace the trailing vbCr with vbCrLf

                'Now that we have the line, remove this data from the main rxBuffer
                rxBuffer = Mid(rxBuffer, rxBufferMarker + 1) 'Exclude the vbCr !

                'Check the current visual mode
                Select Case CurrentTab
                    Case "VisualFlight", "SensorPlots"
                        'Disect the incoming data into it's individual variables
                        Dim FlightData() As String = Split(Replace(rxBufferLineOfData, vbCrLf, ""), ",")
                        If (FlightData.Count >= 11) Then '11 values separated by commas, just checking that we are getting the correct data in.
                            ADI_roll_angle = CDbl(FlightData(8))
                            ADI_pitch_angle = CDbl(FlightData(9))
                            ADI_yaw_angle = CDbl(FlightData(10))
                            roll_gyro_value = CDbl(FlightData(0))
                            pitch_gyro_value = CDbl(FlightData(1))
                            yaw_gyro_value = CDbl(FlightData(2))
                            accel_pitch_value = CDbl(FlightData(3))
                            accel_roll_value = CDbl(FlightData(4))
                            accel_z_value = CDbl(FlightData(5))
                            press_baro_altitude = CDbl(FlightData(11))
                        End If
                    Case "SerialMonitor"
                        SerialDataLine = ""
                        SerialDataLine = rxBufferLineOfData
                    Case "Transmitter"
                        'Disect the incoming data into it's individual variables
                        Dim RadioData() As String = Split(Replace(rxBufferLineOfData, vbCrLf, ""), ",")
                        If (RadioData.Count >= 9) Then '9 values separated by commas, just checking that we are getting the correct data in.
                            radio_roll_value = CDbl(RadioData(0))
                            radio_pitch_value = CDbl(RadioData(1))
                            radio_yaw_value = CDbl(RadioData(2))
                            radio_throttle_value = CDbl(RadioData(3))
                            radio_aux1_value = CDbl(RadioData(4))
                            radio_aux2_value = CDbl(RadioData(5))
                            radio_roll_mid_value = CDbl(RadioData(6))
                            radio_pitch_mid_value = CDbl(RadioData(7))
                            radio_yaw_mid_value = CDbl(RadioData(8))
                        End If

                End Select
            End If

            ' We can now assume we've handled the data that was in the buffer, so we can empty it now, ready for the next data load.
            rxBufferLineOfData = ""
            rxBufferMarker = -1

            'Here we handle the data that needs to be sent back out (TX)
            If (Serial.IsOpen) Then
                If txBuffer <> "" Then
                    Serial.Write(txBuffer)
                    txBuffer = ""
                End If
            End If
        Else
            'Serial is not open !
            ToolStripStatusLabel_Connection.Text = "Disconnected"
            Button_Connect.Text = "Connect"
            Button_Send.Enabled = False
            Button_ShowMenu.Enabled = False
        End If
    End Sub

    '*************************** SUB FOR VISUAL WORK QUEUE ******************************************
    Sub Run_VisualWork()
        'Update the radiobutton on the lower bar to show that serial is connected
        If Serial.IsOpen Then 'Check whether we are connected and if so, update the GUI
            ToolStripStatusLabel_Connection.Text = "Connected to " & comPortName & " @ " & comPortSpeed & " bps"

            'Check whether the view has just changed by the user (visual flight, sensor plots or serial monitor etc)
            'If so, send out the appropriate command through our serial link so we receive the so needed data.
            If currentViewChanged Then
                Select Case CurrentView
                    Case "VisualFlight", "SensorPlots"
                        txBuffer = "Q" 'Start sending me flight data
                    Case "Transmitter"
                        txBuffer = "U" 'Start sending me the transmitter data.
                    Case "SerialMonitor"
                        txBuffer = "X" 'Stop sending me any data, I want the serial monitor for myself !
                End Select
                currentViewChanged = False
            End If
        Else
            ' Apparently we are not connected via serial
            ToolStripStatusLabel_Connection.Text = "Disconnected"
        End If

        ' Update the currenttab variable and update the GUI with current mode etc.
        CurrentTab = Tabs.SelectedTab.Name
        ToolStripStatusLabel_ActivePage.Text = CurrentTab

        ' Now, check which mode we are in and update all the fancy visuals (ADI, indicators, etc.)

        Select Case CurrentTab
            Case "SerialMonitor"
                If (SerialDataLine <> "") Then
                    SerialDataField_Busy = True
                    LockWindowUpdate(Me.Handle)

                    If SerialDataField.Text.Length > 30000 Then SerialDataField.Text = "" 'clear if it becomes too big

                    If InStr(SerialDataLine, ". . . .") <> 0 Then
                        SerialDataField.Clear()
                        SerialDataLine = Replace(SerialDataLine, ". . . ." & vbCrLf & "", "")
                    End If
                    SerialDataField.Text = SerialDataField.Text & SerialDataLine
                    SerialDataField_Busy = False
                    LockWindowUpdate(False)

                    SerialDataLine = ""
                End If
            Case "VisualFlight"
                If Not ADIDemoTimer.Enabled Then 'Don't update the ADI if the ADI demo is running
                    ArtificialHorizon1.roll_angle = ADI_roll_angle
                    ArtificialHorizon1.pitch_angle = ADI_pitch_angle
                    ArtificialHorizon1.yaw_angle = ADI_yaw_angle
                    ArtificialHorizon1.altitude = Math.Round(press_baro_altitude / 100)
                End If

                roll_gyro.Value = 1024 - roll_gyro_value
                pitch_gyro.Value = 1024 - pitch_gyro_value
                yaw_gyro.Value = 1024 - yaw_gyro_value

                accel_roll.Value = 1024 - accel_roll_value
                accel_pitch.Value = 1024 - accel_pitch_value
                accel_z.Value = 1024 - accel_z_value

                'Update labels
                Label_roll_accel.Text = accel_roll_value
                Label_pitch_accel.Text = accel_pitch_value
                Label_z_accel.Text = accel_z_value

                Label_roll_gyro.Text = roll_gyro_value
                Label_pitch_gyro.Text = pitch_gyro_value
                Label_yaw_gyro.Text = yaw_gyro_value
            Case "SensorPlots"
                'Nothing here; the sensorplots are drawn by their own individual timers
            Case "Transmitter"
                'Roll
                Slider_radio_roll.Value = radio_roll_value
                If Slider_radio_roll.OutsideValidRange Then
                    Label_Error_Roll.Text = "*ERROR*"
                    ToolTip.SetToolTip(Label_Error_Roll, "Values for your roll channel are outside the valid range of 900-2100." & vbCrLf & "Please check your configuration !")
                    Label_Slider_Errors.Text = Label_Slider_Errors.Text & "Values for your roll channel are outside the valid range of 900-2100." & vbCrLf
                    Button_Restart_Calibration.Enabled = True
                End If
                'Yaw
                Slider_radio_yaw.Value = radio_yaw_value
                If Slider_radio_yaw.OutsideValidRange Then
                    Label_Error_Yaw.Text = "*ERROR*"
                    ToolTip.SetToolTip(Label_Error_Yaw, "Values for your yaw channel are outside the valid range of 900-2100." & vbCrLf & "Please check your configuration !")
                    Label_Slider_Errors.Text = Label_Slider_Errors.Text & "Values for your yaw channel are outside the valid range of 900-2100." & vbCrLf
                    Button_Restart_Calibration.Enabled = True
                End If
                'Throttle
                Slider_radio_throttle.Value = radio_throttle_value
                If Slider_radio_throttle.OutsideValidRange Then
                    Label_Error_Throttle.Text = "*ERROR*"
                    ToolTip.SetToolTip(Label_Error_Throttle, "Values for your throttle channel are outside the valid range of 900-2100." & vbCrLf & "Please check your configuration !")
                    Label_Slider_Errors.Text = Label_Slider_Errors.Text & "Values for your throttle channel are outside the valid range of 900-2100." & vbCrLf
                    Button_Restart_Calibration.Enabled = True
                End If
                'Pitch
                Slider_radio_pitch.Value = radio_pitch_value
                If Slider_radio_pitch.OutsideValidRange Then
                    Label_Error_Pitch.Text = "*ERROR*"
                    ToolTip.SetToolTip(Label_Error_Pitch, "Values for your pitch channel are outside the valid range of 900-2100." & vbCrLf & "Please check your configuration !")
                    Label_Slider_Errors.Text = Label_Slider_Errors.Text & "Values for your pitch channel are outside the valid range of 900-2100." & vbCrLf
                    Button_Restart_Calibration.Enabled = True
                End If
                'AUX1
                Slider_radio_aux1.Value = radio_aux1_value
                If Slider_radio_aux1.OutsideValidRange Then
                    Label_Error_AUX1.Text = "*ERROR*"
                    ToolTip.SetToolTip(Label_Error_AUX1, "Values for your AUX1 channel are outside the valid range of 900-2100." & vbCrLf & "Please check your configuration !")
                    Label_Slider_Errors.Text = Label_Slider_Errors.Text & "Values for your AUX1 channel are outside the valid range of 900-2100." & vbCrLf
                    Button_Restart_Calibration.Enabled = True
                End If
                'AUX2
                Slider_radio_aux2.Value = radio_aux2_value
                If Slider_radio_aux2.OutsideValidRange Then
                    Label_Error_AUX2.Text = "*ERROR*"
                    ToolTip.SetToolTip(Label_Error_AUX2, "Values for your AUX2 channel are outside the valid range of 900-2100." & vbCrLf & "Please check your configuration !")
                    Label_Slider_Errors.Text = Label_Slider_Errors.Text & "Values for your AUX2 channel are outside the valid range of 900-2100." & vbCrLf
                    Button_Restart_Calibration.Enabled = True
                End If
                'Stop the timer if there was an error
                If Label_Slider_Errors.Text <> "" Then
                    Timer_VisualWork.Enabled = False
                    Label_Slider_Errors.Text = "IMPORTANT ! Check your configuration; values should not exceed the 900-2100 range !" & vbCrLf & Label_Slider_Errors.Text
                End If

                'Throw the video data to the ADI
                'Clipboard.SetDataObject(VideoWindow.Image)
        End Select
    End Sub
    Overloads Function CopyBitmap(ByVal source As Bitmap) As Bitmap
        Return New Bitmap(source)
    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ClearScreen.Click
        SerialDataField.Text = ""
        Field_SerialCommand.Focus()
    End Sub
    Private Sub ComboBox_Ports_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Ports.SelectedIndexChanged
        While Serial.IsOpen
            Serial.Close()
        End While
        Serial.PortName = ComboBox_Ports.Text
        comPortName = ComboBox_Ports.Text
        ToolStripStatusLabel_Connection.Text = "Disconnected"
        Button_Connect.Text = "Connect"
        Button_Send.Enabled = False
        Button_ShowMenu.Enabled = False
    End Sub

    Private Sub Serial1_PinChanged(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialPinChangedEventArgs) Handles Serial.PinChanged
        MsgBox(e.ToString)
    End Sub

    Private Sub ComboBox_Baud_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Baud.SelectedIndexChanged
        While Serial.IsOpen
            Serial.Close()
        End While
        Serial.BaudRate = ComboBox_Baud.Text
        comPortSpeed = ComboBox_Baud.Text
        ToolStripStatusLabel_Connection.Text = "Disconnected"
        Button_Connect.Text = "Connect"
        Button_Send.Enabled = False
        Button_ShowMenu.Enabled = False
    End Sub

    Private Sub Tabs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tabs.SelectedIndexChanged
        'On each change of the used view page, we send a serial command to get data
        currentViewChanged = True
        CurrentView = Tabs.SelectedTab.Name
        If CurrentView <> "OnlineSupport" Then
            'Kill the instance of IE to save memory space. You must know, I hate IE ! :)
            Browser.Dispose()
            Browser = Nothing

        End If
        Select Case CurrentView
            Case "SerialMonitor"
                Field_SerialCommand.Focus()
            Case "OnlineSupport"
                '*sigh* We want Online Support via our ArduPirates Google code group. And we need to instantiate
                'Internet Explorer within the application to view our site, http://code.google.com/p/ardupirates/
                Browser = New WebBrowser
                Browser.Parent = OnlineSupport
                Browser.Dock = DockStyle.Fill
                Browser.IsWebBrowserContextMenuEnabled = False
                Browser.ScriptErrorsSuppressed = vbYes
                Browser.WebBrowserShortcutsEnabled = False
                Browser.Show()
                Browser.Navigate("http://code.google.com/p/ardupirates/")
        End Select
    End Sub

    Private Sub SerialMonitor_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SerialMonitor.Enter
        Field_SerialCommand.Focus()
    End Sub

    Private Sub ArtificialHorizon1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Timer_VisualWork_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_VisualWork.Tick
        Run_VisualWork()
        ToolStripStatusLabel_Time.Text = Now.ToString

    End Sub
    Private Sub Timer_Chart1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Chart1.Tick
        '****************************************************************************
        '
        '  Chart for roll, pitch, yaw angles
        '
        '****************************************************************************
        'For pointNumber = 0 To (random.Next(numberOfPointsAddedMin, numberOfPointsAddedMax)) - 1
        pointIndex1 = pointIndex1 + 1
        'Chart1.Series(0).Points.AddXY(pointIndex + 1, random.Next(1000, 5000))
        If CheckBoxPitch.Checked Then Chart1.Series(0).Points.AddXY(pointIndex1 + 1, ADI_pitch_angle)
        If CheckBoxRoll.Checked Then Chart1.Series(1).Points.AddXY(pointIndex1 + 1, ADI_roll_angle)
        If CheckBoxYaw.Checked Then Chart1.Series(2).Points.AddXY(pointIndex1 + 1, ADI_yaw_angle)

        TextBoxPitch.Text = Format(ADI_pitch_angle, "0.0")
        TextBoxRoll.Text = Format(ADI_roll_angle, "0.0")
        TextBoxYaw.Text = Format(ADI_yaw_angle, "0.0")
        'Next pointNumber

        ' Adjust Y & X axis scale
        Chart1.ResetAutoValues()

        ' Keep a constant number of points by removing them from the left
        While Chart1.Series(0).Points.Count > numberOfPointsInChart
            ' Remove data points on the left side
            While Chart1.Series(0).Points.Count > numberOfPointsAfterRemoval
                Chart1.Series(0).Points.RemoveAt(0)
            End While

            ' Adjust X axis scale
            Chart1.ChartAreas("Default").AxisX.Minimum = pointIndex1 - numberOfPointsAfterRemoval
            Chart1.ChartAreas("Default").AxisX.Maximum = Chart1.ChartAreas("Default").AxisX.Minimum + numberOfPointsInChart
        End While
        While Chart1.Series(1).Points.Count > numberOfPointsInChart
            ' Remove data points on the left side
            While Chart1.Series(1).Points.Count > numberOfPointsAfterRemoval
                Chart1.Series(1).Points.RemoveAt(0)
            End While

            ' Adjust X axis scale
            Chart1.ChartAreas("Default").AxisX.Minimum = pointIndex1 - numberOfPointsAfterRemoval
            Chart1.ChartAreas("Default").AxisX.Maximum = Chart1.ChartAreas("Default").AxisX.Minimum + numberOfPointsInChart
        End While
        While Chart1.Series(2).Points.Count > numberOfPointsInChart
            ' Remove data points on the left side
            While Chart1.Series(2).Points.Count > numberOfPointsAfterRemoval
                Chart1.Series(2).Points.RemoveAt(0)
            End While

            ' Adjust X axis scale
            Chart1.ChartAreas("Default").AxisX.Minimum = pointIndex1 - numberOfPointsAfterRemoval
            Chart1.ChartAreas("Default").AxisX.Maximum = Chart1.ChartAreas("Default").AxisX.Minimum + numberOfPointsInChart
        End While

        ' Invalidate chart
        Chart1.Invalidate()

    End Sub

    Private Sub Timer_Chart2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Chart2.Tick
        '****************************************************************************
        '
        '  Chart for roll, pitch, yaw gyros
        '
        '****************************************************************************
        'For pointNumber = 0 To (random.Next(numberOfPointsAddedMin, numberOfPointsAddedMax)) - 1
        pointIndex2 = pointIndex2 + 1
        'Chart2.Series(0).Points.AddXY(pointIndex + 1, random.Next(1000, 5000))
        If CheckBoxPitchGyro.Checked Then Chart2.Series(0).Points.AddXY(pointIndex2 + 1, pitch_gyro_value)
        If CheckBoxRollGyro.Checked Then Chart2.Series(1).Points.AddXY(pointIndex2 + 1, roll_gyro_value)
        If CheckBoxYawGyro.Checked Then Chart2.Series(2).Points.AddXY(pointIndex2 + 1, yaw_gyro_value)

        TextBoxPitchGyro.Text = Format(pitch_gyro_value, "0.0")
        TextBoxRollGyro.Text = Format(roll_gyro_value, "0.0")
        TextBoxYawGyro.Text = Format(yaw_gyro_value, "0.0")
        'Next pointNumber

        ' Adjust Y & X axis scale
        Chart2.ResetAutoValues()

        ' Keep a constant number of points by removing them from the left
        While Chart2.Series(0).Points.Count > numberOfPointsInChart
            ' Remove data points on the left side
            While Chart2.Series(0).Points.Count > numberOfPointsAfterRemoval
                Chart2.Series(0).Points.RemoveAt(0)
            End While

            ' Adjust X axis scale
            Chart2.ChartAreas("Default").AxisX.Minimum = pointIndex2 - numberOfPointsAfterRemoval
            Chart2.ChartAreas("Default").AxisX.Maximum = Chart2.ChartAreas("Default").AxisX.Minimum + numberOfPointsInChart
        End While
        While Chart2.Series(1).Points.Count > numberOfPointsInChart
            ' Remove data points on the left side
            While Chart2.Series(1).Points.Count > numberOfPointsAfterRemoval
                Chart2.Series(1).Points.RemoveAt(0)
            End While

            ' Adjust X axis scale
            Chart2.ChartAreas("Default").AxisX.Minimum = pointIndex2 - numberOfPointsAfterRemoval
            Chart2.ChartAreas("Default").AxisX.Maximum = Chart2.ChartAreas("Default").AxisX.Minimum + numberOfPointsInChart
        End While
        While Chart2.Series(2).Points.Count > numberOfPointsInChart
            ' Remove data points on the left side
            While Chart2.Series(2).Points.Count > numberOfPointsAfterRemoval
                Chart2.Series(2).Points.RemoveAt(0)
            End While

            ' Adjust X axis scale
            Chart2.ChartAreas("Default").AxisX.Minimum = pointIndex2 - numberOfPointsAfterRemoval
            Chart2.ChartAreas("Default").AxisX.Maximum = Chart2.ChartAreas("Default").AxisX.Minimum + numberOfPointsInChart
        End While

        ' Invalidate chart
        Chart2.Invalidate()
    End Sub

    Private Sub Timer_Chart3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Chart3.Tick
        '****************************************************************************
        '
        '  Chart for roll, pitch, Z accelerometers
        '
        '****************************************************************************
        'For pointNumber = 0 To (random.Next(numberOfPointsAddedMin, numberOfPointsAddedMax)) - 1
        pointIndex3 = pointIndex3 + 1
        'Chart3.Series(0).Points.AddXY(pointIndex + 1, random.Next(1000, 5000))
        If CheckBoxPitchAccel.Checked Then Chart3.Series(0).Points.AddXY(pointIndex3 + 1, accel_pitch_value)
        If CheckBoxRollAccel.Checked Then Chart3.Series(1).Points.AddXY(pointIndex3 + 1, accel_roll_value)
        If CheckBoxZAccel.Checked Then Chart3.Series(2).Points.AddXY(pointIndex3 + 1, accel_z_value)

        TextBoxPitchAccel.Text = Format(accel_pitch_value, "0.0")
        TextBoxRollAccel.Text = Format(accel_roll_value, "0.0")
        TextBoxZAccel.Text = Format(accel_z_value, "0.0")
        'Next pointNumber

        ' Adjust Y & X axis scale
        Chart3.ResetAutoValues()

        ' Keep a constant number of points by removing them from the left
        While Chart3.Series(0).Points.Count > numberOfPointsInChart
            ' Remove data points on the left side
            While Chart3.Series(0).Points.Count > numberOfPointsAfterRemoval
                Chart3.Series(0).Points.RemoveAt(0)
            End While

            ' Adjust X axis scale
            Chart3.ChartAreas("Default").AxisX.Minimum = pointIndex3 - numberOfPointsAfterRemoval
            Chart3.ChartAreas("Default").AxisX.Maximum = Chart3.ChartAreas("Default").AxisX.Minimum + numberOfPointsInChart
        End While
        While Chart3.Series(1).Points.Count > numberOfPointsInChart
            ' Remove data points on the left side
            While Chart3.Series(1).Points.Count > numberOfPointsAfterRemoval
                Chart3.Series(1).Points.RemoveAt(0)
            End While

            ' Adjust X axis scale
            Chart3.ChartAreas("Default").AxisX.Minimum = pointIndex3 - numberOfPointsAfterRemoval
            Chart3.ChartAreas("Default").AxisX.Maximum = Chart3.ChartAreas("Default").AxisX.Minimum + numberOfPointsInChart
        End While
        While Chart3.Series(2).Points.Count > numberOfPointsInChart
            ' Remove data points on the left side
            While Chart3.Series(2).Points.Count > numberOfPointsAfterRemoval
                Chart3.Series(2).Points.RemoveAt(0)
            End While

            ' Adjust X axis scale
            Chart3.ChartAreas("Default").AxisX.Minimum = pointIndex3 - numberOfPointsAfterRemoval
            Chart3.ChartAreas("Default").AxisX.Maximum = Chart3.ChartAreas("Default").AxisX.Minimum + numberOfPointsInChart
        End While

        ' Invalidate chart
        Chart3.Invalidate()

    End Sub

    Private Sub Button_Pause_Chart_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Pause_Chart_1.Click
        Timer_Chart1.Enabled = Not Timer_Chart1.Enabled
        If Timer_Chart1.Enabled Then
            Button_Pause_Chart_1.Text = "Pause"
        Else
            Button_Pause_Chart_1.Text = "Resume"
        End If
    End Sub

    Private Sub Button_Pause_Chart_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Pause_Chart_2.Click
        Timer_Chart2.Enabled = Not Timer_Chart2.Enabled
        If Timer_Chart2.Enabled Then
            Button_Pause_Chart_2.Text = "Pause"
        Else
            Button_Pause_Chart_2.Text = "Resume"
        End If
    End Sub

    Private Sub Button_Pause_Chart_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Pause_Chart_3.Click
        Timer_Chart3.Enabled = Not Timer_Chart3.Enabled
        If Timer_Chart3.Enabled Then
            Button_Pause_Chart_3.Text = "Pause"
        Else
            Button_Pause_Chart_3.Text = "Resume"
        End If
    End Sub


    Private Sub ButtonFetchDeclination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFetchDeclination.Click
        TextBoxYourLatitude.Text = Replace(TextBoxYourLatitude.Text, ",", ".")
        TextBoxYourLongitude.Text = Replace(TextBoxYourLongitude.Text, ",", ".")
        If ((IsNumeric(TextBoxYourLatitude.Text)) And (IsNumeric(TextBoxYourLongitude.Text))) Then

            Dim webStream As Stream
            Dim webResponse = ""
            Dim req As HttpWebRequest
            Dim res As HttpWebResponse
            req = WebRequest.Create("http://www.ngdc.noaa.gov/geomagmodels/struts/calcDeclination?minLatStr=" & TextBoxYourLatitude.Text & "&minLonStr=" & TextBoxYourLongitude.Text & "&minLatHemisphere=n&minLonHemisphere=e")
            req.Timeout = 5000
            req.Method = "GET"
            req.ReadWriteTimeout = 5000
            res = Nothing
            Try
                res = req.GetResponse() ' Send Request
            Catch Exception As WebException
                TextBox_MagDecl.Text = "no data"
                MsgBox("Could not retrieve the needed data from the internet." & vbCrLf & "Check your internet connection and proxy settings in IE and try again.")


            Finally
                webStream = res.GetResponseStream() ' Get Response
                If (Not IsNothing(webStream)) Then
                    Dim webStreamReader As New StreamReader(webStream)
                    While webStreamReader.Peek >= 0
                        webResponse = webStreamReader.ReadToEnd()
                    End While
                End If

            End Try
            If webResponse <> "" Then
                Dim Pos1 = InStr(webResponse, "<b>Declination</b> =") + 21 'Find by portions of the HTML code on the result page
                If InStr(webResponse, "error") <> 0 Then GoTo exitdeclination 'NOAA encountered an error, so get out !

                Dim Pos2 = InStr(webResponse, "    changing by ") - 1 'Find by portions of the HTML code on the result page
                Dim retrievedDeclination As String = Mid(webResponse, Pos1, Pos2 - Pos1)
                'MsgBox(retrievedDeclination)
                Dim MinLen = InStr(retrievedDeclination, "' ") - (InStr(retrievedDeclination, "&deg; ") + 6)
                Dim Degrees As String = Strings.Left(retrievedDeclination, InStr(retrievedDeclination, "&deg; ") - 1)
                Dim Minutes As String = Strings.Mid(retrievedDeclination, InStr(retrievedDeclination, "&deg; ") + 6, MinLen)
                Dim EastWest As String = Strings.Mid(retrievedDeclination, InStr(retrievedDeclination, "' ") + 2, 1)
                'MsgBox("Degrees:>" & Degrees & "<Minutes:>" & Minutes & "<East_or_West:>" & EastWest & "<Original:>" & retrievedDeclination & "<")
                Dim DD As Double = Degrees + (Minutes / 60)
                If EastWest = "W" Then
                    DD = -1.0 * DD
                End If
                TextBox_MagDecl.Text = Format(DD, "0.000")
            Else
                TextBox_MagDecl.Text = "no data"
            End If
        Else
exitdeclination:
            TextBox_MagDecl.Text = "error"
            MsgBox("Please annotate your latitude and longitude in decimal format. Numbers only." & vbCrLf & "Maximums of -90 and 90. Instead of S or W use a negative number.")
        End If

    End Sub

    Private Sub Serial1_Connect()
        If Serial.IsOpen = False Then
            'Set comport and open
            Try
                Serial.PortName = ComboBox_Ports.Text
                Serial.BaudRate = ComboBox_Baud.Text
            Catch ex As Exception
                MsgBox("Please select both a COM port and port speed !")
                GoTo failedtoopencom
            End Try

            Try
                Serial.Open()
            Catch ex As Exception
                MsgBox("An error occurred during connection:" & Chr(13) & Chr(10) & ex.Message, , "Connection error")
                GoTo failedtoopencom
            End Try
            Button_Connect.Text = "Disconnect"
            Button_Send.Enabled = True
            Button_ShowMenu.Enabled = True
            ' Do we need to start by sending a command ? Depends on which tabpage is selected
            While Not Serial.IsOpen
                'Wait until the serial port is really open
                ToolStripStatusLabel_Connection.Text = "Connecting...please wait"
            End While
        Else
failedtoopencom:
            While Serial.IsOpen
                Serial.Close()
            End While
            ToolStripStatusLabel_Connection.Text = "Disconnected"
            Button_Connect.Text = "Connect"
            Button_Send.Enabled = False
            Button_ShowMenu.Enabled = False
        End If
    End Sub
    Private Sub Button_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Connect.Click
        Serial1_Connect()
    End Sub

    Private Sub Button_Exit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub MainForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        ' Save our personal settings for this application
        SaveSetting("ArduPirates Management Console", "Communication", "Port", comPortName)
        SaveSetting("ArduPirates Management Console", "Communication", "Baudrate", comPortSpeed)
        SaveSetting("ArduPirates Management Console", "Communication", "Autoconnect", comAutoConnect)

        'Wait until the Serial port is closed before we exit.
        While Serial.IsOpen
            Serial.Close()
        End While

        'Stop the serial work timer
        While Timer_SerialWork.Enabled
            Timer_SerialWork.Stop()
        End While

        'Kill camera
        DisposeCamera()

        'Ready to kill the main form, all other work has been terminated
        Application.Exit()
    End Sub


    Private Sub Button_Send_calibration_values_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Send_calibration_values.Click
        'Sends the calibration to the APM
        'Serial command 1 stores, serial command 2 retrieves
        'Takes care of regional settings (comma and period)
        'Values to be sent in order: 
        'ch_roll_slope, ch_roll_offset, ch_pitch_slope, ch_pitch_offset, ch_yaw_slope, ch_yaw_offset, 
        'ch_throttle_slope, ch_throttle_offset, ch_aux_slope, ch_aux_offset, ch_aux2_slope, ch_aux2_offset
        '


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ADIDemoTimer.Enabled = Not ADIDemoTimer.Enabled
    End Sub

    Public Function DegreeToRadian(ByVal degree As Double) As Double
        Return (Math.PI / 180) * degree
    End Function

    Public Function RadianToDegree(ByVal radians As Single) As Single
        Return radians * 180.0 / Math.PI
    End Function

    Private Sub ADIDemoTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ADIDemoTimer.Tick
        'Create an automated smooth dampened roll motion
        democounter_a = democounter_a + 1
        If democounter_a > 180 Then democounter_a = -180
        ArtificialHorizon1.roll_angle = RadianToDegree(Math.Sin(DegreeToRadian(democounter_a)))
        'Create and automated smooth dampened pitch motion
        democounter_b = democounter_b + 1.5
        If democounter_b > 180 Then democounter_b = -180
        ArtificialHorizon1.pitch_angle = RadianToDegree(Math.Sin(DegreeToRadian(democounter_b)))

        'Create a unidirectional yaw motion
        ArtificialHorizon1.yaw_angle = ArtificialHorizon1.yaw_angle + 1
        If ArtificialHorizon1.yaw_angle > 180 Then ArtificialHorizon1.yaw_angle = -179

        'Play with altitude
        democounter_c = democounter_c + 1
        If democounter_c > 180 Then democounter_c = -180

        ArtificialHorizon1.altitude = 500 + 2 * RadianToDegree(Math.Cos(DegreeToRadian(democounter_c)))

    End Sub

    Private Sub Serial1_ErrorReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialErrorReceivedEventArgs) Handles Serial.ErrorReceived
        While Serial.IsOpen
            Serial.Close()
        End While
        ToolStripStatusLabel_Connection.Text = "Disconnected"
        Button_Connect.Text = "Connect"
        Button_Send.Enabled = False
        Button_ShowMenu.Enabled = False
    End Sub

    Private Sub CheckBox_AutoConnect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_AutoConnect.CheckedChanged
        comAutoConnect = CheckBox_AutoConnect.Checked
    End Sub

    Private Sub Button_Reset_Sliders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Restart_Calibration.Click
        'Reset the sliders to their defaults
        Slider_radio_pitch.Reset()
        Slider_radio_yaw.Reset()
        Slider_radio_throttle.Reset()
        Slider_radio_roll.Reset()
        Slider_radio_aux1.Reset()
        Slider_radio_aux2.Reset()
        'Reset all the error labels and their tooltips
        Label_Error_Pitch.Text = ""
        ToolTip.SetToolTip(Label_Error_Pitch, "")
        Label_Error_Yaw.Text = ""
        ToolTip.SetToolTip(Label_Error_Yaw, "")
        Label_Error_Throttle.Text = ""
        ToolTip.SetToolTip(Label_Error_Throttle, "")
        Label_Error_Roll.Text = ""
        ToolTip.SetToolTip(Label_Error_Roll, "")
        Label_Error_AUX1.Text = ""
        ToolTip.SetToolTip(Label_Error_AUX1, "")
        Label_Error_AUX2.Text = ""
        ToolTip.SetToolTip(Label_Error_AUX2, "")
        'Reset the cumulative error label
        Label_Slider_Errors.Text = ""
        'Restart the visual work timer
        Timer_VisualWork.Enabled = True
        Button_Restart_Calibration.Enabled = False

    End Sub

    Private Sub Label_Error_AUX2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label_Error_AUX2.Click

    End Sub

    Private Sub Label_Error_AUX1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label_Error_AUX1.Click

    End Sub

    Private Sub Label_Error_Roll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label_Error_Roll.Click

    End Sub

    Private Sub Label_Error_Throttle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label_Error_Throttle.Click

    End Sub

    Private Sub Label_Error_Yaw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label_Error_Yaw.Click

    End Sub

    Private Sub Label_Error_Pitch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label_Error_Pitch.Click

    End Sub


    Private Sub Button_Refresh_serialports_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Refresh_Serialports.Click
        'Enumerate available Com ports and add to the list in the statusbar
        Enumerate_SerialPorts()
    End Sub


    Private Sub Field_SerialCommand_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Field_SerialCommand.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            rxBuffer = ""
            'Write the data to serial
            If Serial.IsOpen Then
                If (Field_SerialCommand.Text = "") Then
                    Serial.Write(vbCrLf)
                Else
                    Serial.Write(Field_SerialCommand.Text)
                End If
            Else
                MsgBox("Please connect first !")
            End If

            Field_SerialCommand.Clear()
            Field_SerialCommand.Focus()
            e.Handled = True
        End If

    End Sub


    Private Sub ComboBox_PIDModeSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_PIDModeSelect.SelectedIndexChanged
        Select Case ComboBox_PIDModeSelect.SelectedItem.ToString
            Case "Acrobatic Mode"
                'Enable/disable controls in the screen, and set default values where needed
                Label_PID_Mode.Text = ComboBox_PIDModeSelect.SelectedItem.ToString
                GroupBox_PID_Roll.Text = "Roll PID Values"
                Label_PID_Special_2.Visible = False
                Label_PID_Special_1.Text = "Transmitter factor:"
                Label_PID_Special_1.Visible = True
                NumericUpDown_PID_Special_1.Visible = True
                NumericUpDown_PID_Special_1.Minimum = 0.01
                NumericUpDown_PID_Special_1.Maximum = 1
                NumericUpDown_PID_Special_2.Visible = False
                GroupBox_PID_Pitch.Visible = True
                GroupBox_PID_Roll.Visible = True
                GroupBox_PID_Yaw.Visible = True
                CheckBox_PID_Magnetometer.Visible = False
            Case "Stable Mode"
                Label_PID_Mode.Text = ComboBox_PIDModeSelect.SelectedItem.ToString
                GroupBox_PID_Roll.Text = "Roll PID Values"
                Label_PID_Special_2.Visible = False
                NumericUpDown_PID_Special_2.Visible = False
                Label_PID_Special_1.Text = "Kp rate:"
                Label_PID_Special_1.Visible = True
                NumericUpDown_PID_Special_1.Visible = True
                GroupBox_PID_Pitch.Visible = True
                GroupBox_PID_Roll.Visible = True
                GroupBox_PID_Yaw.Visible = True
                CheckBox_PID_Magnetometer.Visible = True
            Case "Altitude Hold"
                Label_PID_Mode.Text = ComboBox_PIDModeSelect.SelectedItem.ToString
                GroupBox_PID_Roll.Text = "Altitude Hold PID Values"
                Label_PID_Special_1.Visible = False
                Label_PID_Special_2.Visible = False
                NumericUpDown_PID_Special_1.Visible = False
                NumericUpDown_PID_Special_2.Visible = False
                CheckBox_PID_Magnetometer.Visible = False
                GroupBox_PID_Pitch.Visible = False
                GroupBox_PID_Yaw.Visible = False
            Case "GPS Hold"
                Label_PID_Mode.Text = ComboBox_PIDModeSelect.SelectedItem.ToString
                GroupBox_PID_Roll.Text = "Roll PID Values"
                Label_PID_Special_1.Text = "Maximum angle:"
                Label_PID_Special_2.Text = "Geo correction factor:"
                Label_PID_Special_1.Visible = True
                Label_PID_Special_2.Visible = True
                GroupBox_PID_Yaw.Visible = False
                GroupBox_PID_Pitch.Visible = True
                GroupBox_PID_Roll.Visible = True
                CheckBox_PID_Magnetometer.Visible = False
                NumericUpDown_PID_Special_1.Visible = True
                NumericUpDown_PID_Special_2.Visible = True
            Case "Camera"
                Label_PID_Mode.Text = ComboBox_PIDModeSelect.SelectedItem.ToString




        End Select
    End Sub

    Private Sub Button_Browser_Home_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Browser_Home.Click
        Browser.Navigate("http://code.google.com/p/ardupirates/")

    End Sub

    Private Sub Button_Browser_Back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Browser_Back.Click
        Browser.GoBack()

    End Sub

    Private Sub Button_Browser_Forward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Browser_Forward.Click
        Browser.GoForward()

    End Sub

    Private Sub SerialDataField_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SerialDataField.TextChanged
        If SerialDataField_Busy Then
            SerialDataField.SelectionStart = Len(SerialDataField.Text)
            SerialDataField.ScrollToCaret()
        End If

    End Sub

    Private Sub ToolStripStatusLabel_Connection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel_Connection.Click
        Serial1_Connect()
    End Sub

    Private Sub ToolStripStatusLabel_Connection_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel_Connection.MouseEnter
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub ToolStripStatusLabel_Connection_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel_Connection.MouseLeave
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Button_ShowMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ShowMenu.Click
        txBuffer = "?"
    End Sub


    Private Sub MainForm_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        'Set the ADI in the center of the screen 
        ArtificialHorizon1.Left = (VisualFlight.Width / 2) - (ArtificialHorizon1.Width / 2)

        'Make the ADI as big as possible, but make sure there is room for the gyro and accel panels. Plus add some margin
        ArtificialHorizon1.Width = VisualFlight.Width - (Panel_Gyro_ADI.Width + Panel_Accel_ADI.Width + 25)
        'Keep the ADI a square
        ArtificialHorizon1.Height = ArtificialHorizon1.Width

        'Make sure that the ADI is fitted inside the tab (set 10px margin on top and bottom)
        If ArtificialHorizon1.Height > (VisualFlight.Height - 20) Then
            ArtificialHorizon1.Height = VisualFlight.Height - 20
            'Keep the ADI a square
            ArtificialHorizon1.Width = ArtificialHorizon1.Height
        End If

        'Set the gyro panel to the left of the ADI
        Panel_Gyro_ADI.Left = ArtificialHorizon1.Left - Panel_Gyro_ADI.Width

        'Set the accel panel to the right of the ADI
        Panel_Accel_ADI.Left = ArtificialHorizon1.Left + ArtificialHorizon1.Width + 5

    End Sub


    Private Sub ButtonButtonStartCamera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonStartCamera.Click
        If Not IsNothing(frameSource) Then
            If MessageBox.Show("Please note that some cameras drivers does not want to get disposed of and might crash the application (EasyCap is one of them). " + Chr(13) + "Do you want to change camera?", "Stupid camera drivers warning", MessageBoxButtons.OKCancel, _
                Nothing, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
                LabelCameraShowing.Text = "Initializing camera."
                DisposeCamera()

                StartLiveFeed()
            End If
        Else
            LabelCameraShowing.Text = "Initializing camera."
            DisposeCamera()

            StartLiveFeed()
        End If

    End Sub

    Private Sub DrawLatestImage(ByVal sender As Object, ByVal e As PaintEventArgs)

        If Not IsNothing(latestFrame) Then
            ' Draw the latest image from the active camera
            e.Graphics.DrawImage(latestFrame, 0, 0, 640, 480)

        End If
    End Sub

    Private Sub StartLiveFeed()
        Try
            Dim c As Camera
            c = ComboBox_SelectCamera.SelectedItem
            SetFrameSource(New CameraFrameSource(c))
            frameSource.Camera.CaptureWidth = 640
            frameSource.Camera.CaptureHeight = 480
            frameSource.Camera.Fps = 20

            AddHandler frameSource.NewFrame, AddressOf OnImageCaptured

            AddHandler PictureBoxDisplay.Paint, AddressOf DrawLatestImage

            frameSource.StartFrameCapture()

            LabelCameraShowing.Text = "...streaming live feed..."
        Catch ex As Exception
            ComboBox_SelectCamera.Text = "Select a camera"
            MessageBox.Show("Unable to enable camera: " + frameSource.Camera.Name + "\n\nFull error message: \n" + ex.Message)
        End Try

    End Sub

    Private Sub SetFrameSource(ByVal cameraFrameSource As CameraFrameSource)
        If (frameSource Is cameraFrameSource) Then
            Return
        End If
        frameSource = cameraFrameSource

    End Sub

    Private Sub OnImageCaptured(ByVal frameSource As Touchless.Vision.Contracts.IFrameSource, ByVal frame As Touchless.Vision.Contracts.Frame, ByVal fps As Double)
        latestFrame = frame.Image

        'Update ADI
        ArtificialHorizon1.PictureBoxLiveFeed.Image = latestFrame

        'Update "Live feed" tab
        PictureBoxDisplay.Invalidate()
    End Sub

    Private Sub DisposeCamera()
        Try
            If Not IsNothing(latestFrame) Then
                RemoveHandler frameSource.NewFrame, AddressOf Me.OnImageCaptured
                frameSource.Camera.Dispose()
                RemoveHandler PictureBoxDisplay.Paint, AddressOf DrawLatestImage
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to dispose camera: " + frameSource.Camera.Name + "\n\nFull error message: \n" + ex.Message)
        End Try


    End Sub

    Private Sub CheckBoxShowLiveFeed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxShowLiveFeed.CheckedChanged
        If CheckBoxShowLiveFeed.Checked Then
            ArtificialHorizon1.EnableLiveFeed()
        Else
            ArtificialHorizon1.DisableLiveFeed()
        End If

    End Sub

End Class
