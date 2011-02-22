Imports System
Imports System.Windows.Forms
Imports System.Security.Permissions
Imports System.Xml
Imports System.Xml.Serialization


<System.Runtime.InteropServices.ComVisibleAttribute(True)> _
Public Class MissionPlanner
    Inherits Form

    Private webBrowser1 As New WebBrowser()


    Dim currentLat As String
    Dim currentLng As String
    Dim currentTitle As String
    Dim currentnum As String

    Dim WaypointList As List(Of Waypoint)
    Dim MAV_commands_NAV As List(Of MAV_command)

    Public Sub New()

        InitializeComponent()


    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) _
        Handles Me.Load

        webBrowser1.Dock = DockStyle.Fill
        webBrowser1.AllowWebBrowserDrop = False
        webBrowser1.IsWebBrowserContextMenuEnabled = False
        webBrowser1.WebBrowserShortcutsEnabled = False
        webBrowser1.ObjectForScripting = Me
        ' Uncomment the following line when you are finished debugging.
        'webBrowser1.ScriptErrorsSuppressed = True

        webBrowser1.Navigate(System.Environment.CurrentDirectory + "\MissionPlanner.html")
        webBrowser1.Parent = PanelMap       'Put browser in a panel to be able to better control size

        MAV_commands_NAV = New List(Of MAV_command)
        WaypointList = New List(Of Waypoint)


        PopulateCommandTypeList()

        ResizeForm()
    End Sub

    ''' <summary>
    ''' Adds a waypoint to the list of waypoints
    ''' </summary>
    ''' <param name="WaypointData">The string with waypoint data</param>
    ''' <remarks></remarks>
    Public Sub AddWaypoint(ByVal WaypointData As String)

        'Got data for new waypoint
        If (Not String.IsNullOrEmpty(WaypointData)) Then

            'store values in an array
            Dim waypointValues As Array
            Dim LatLng As Array
            Dim newWaypoint As Waypoint
            Dim waypointNumber As Integer
            'remove the parenthesis Google Maps surronds the coordinates with
            WaypointData = WaypointData.Replace("(", "").Replace(")", "")

            'split the WaypointData-string
            'The string looks like this:
            '<Number>:(<Latitude>, <Longitude>:<Title>) 
            waypointValues = WaypointData.ToString().Split(":")
            waypointNumber = waypointValues(0)
            'split marker position into Latitude and Longitude
            LatLng = waypointValues(1).ToString().Split(",")

            'Create the new waypoint object
            newWaypoint = New Waypoint(
                    waypointValues(0), LatLng(0), LatLng(1), "NAV_WAYPOINT", waypointValues(2)
                    )

            'Add waypoint to list of waypoints (set waypoint command type to default)
            'See if we should insert the new waypoint at the end of the list or insert between two waypoints
            If waypointNumber = 0 Then
                WaypointList.Add(newWaypoint) 'add last in list
            Else
                WaypointList.Insert(waypointNumber, newWaypoint) 'insert between two waypoints
            End If

            waypointValues = Nothing

            'Populate and refresh the waypointlist, and mark the added as  selected 
            UpdateWayPointList(waypointNumber - 1, False)
        End If

    End Sub

    ''' <summary>
    ''' Deletes a waypoint from list of waypoints
    ''' </summary>
    ''' <param name="waypointnumber">The place in the list to remove waypoint</param>
    ''' <remarks></remarks>
    Public Sub DeleteWaypoint(ByVal waypointnumber As Integer)
        WaypointList.RemoveAt(waypointnumber)
        UpdateWayPointList(-1, False)
    End Sub

    ''' <summary>
    ''' Updates the waypoint with new position
    ''' </summary>
    ''' <param name="waypointNumber">The waypoint to update</param>
    ''' <param name="markerPosition">The new position</param>
    ''' <remarks></remarks>
    Public Sub UpdateWaypoint(ByVal waypointNumber As Integer, ByVal markerPosition As String)

        'Set waypoints new position
        WaypointList.Item(waypointNumber).Position = markerPosition

        'Set moved marker as selected waypoint
        SelectedWaypoint(waypointNumber)

        'set the waypoint as selected
        UpdateWayPointList(waypointNumber, False)
    End Sub

    ''' <summary>
    ''' Updates the list of waypoints in the grid view
    ''' </summary>
    ''' <param name="selectedWaypoint">The waypoint to mark as selected. -1 select none. -10 updates Google maps</param>
    ''' <remarks></remarks>
    Public Sub UpdateWayPointList(ByVal selectedWaypoint As Integer, ByRef UpdateGoogleMaps As Boolean)
        Dim waypoints As String = ""


        'Crappy way of populating the gridview.
        'I thought you could just use WaypointGridView.Datasource = Waypoints and then run databind, but I did not get that ot work. 
        'Feel free to improve ;-)

        WaypointGridView.Rows.Clear()

        Dim row As Integer = 0
        Dim coll As Integer = 0

        For Each Waypoint In WaypointList
            row = WaypointGridView.Rows.Add()

            WaypointGridView.Rows.Item(row).Cells("Num").Value = row
            WaypointGridView.Rows.Item(row).Cells("Latitude").Value = Waypoint.Latitude
            WaypointGridView.Rows.Item(row).Cells("Longitude").Value = Waypoint.Longitude
            'WaypointGridView.Rows.Item(row).Cells("Command").Value = "NAV_WAYPOINT"
            WaypointGridView.Rows.Item(row).Cells("MoveUp").Value = "Up"
            WaypointGridView.Rows.Item(row).Cells("MoveDown").Value = "Down"
            WaypointGridView.Rows.Item(row).Cells("Delete").Value = "Delete"

            Waypoint.Number = row

            waypoints += row.ToString() + ":" + Waypoint.Latitude.ToString() + ":" + Waypoint.Longitude.ToString() + ":" + Waypoint.Title + ";"

            row = row + 1
        Next

        'user selected a waypoint, mark it and make sure user sees it after updating
        If selectedWaypoint > -1 Then

            WaypointGridView.Rows.Item(selectedWaypoint).DefaultCellStyle.BackColor = Color.Coral

            TextBoxParameter5.Text = WaypointList(selectedWaypoint).Latitude
            TextBoxParameter6.Text = WaypointList(selectedWaypoint).Longitude


            'start render the grid
            WaypointGridView.FirstDisplayedScrollingRowIndex() = selectedWaypoint
        End If

        'Added new waypoint, set newly created waypoint as selected
        If selectedWaypoint = -1 Then
            If (WaypointGridView.Rows.Count > 1) Then
                WaypointGridView.Rows.Item(WaypointGridView.Rows.Count - 2).DefaultCellStyle.BackColor = Color.Coral
                ShowWaypointProperties(WaypointGridView.Rows.Count - 2)
            Else
                WaypointGridView.Rows.Item(0).DefaultCellStyle.BackColor = Color.Coral
            End If

            WaypointGridView.FirstDisplayedScrollingRowIndex() = WaypointGridView.Rows.Count - 1
        End If

        'Send waypoints to Google Maps
        If UpdateGoogleMaps Then

            webBrowser1.Document.InvokeScript("createMarkers", New Object() {waypoints})
        End If


        'MessageBox.Show(waypoints)

    End Sub

    ''' <summary>
    ''' A waypoint is selected, mark it in list and show properties panel
    ''' </summary>
    ''' <param name="waypointNum">the number of the selected waypoint</param>
    ''' <remarks>Called from html-file</remarks>
    Public Sub SelectedWaypoint(ByVal waypointNum As Integer)
        'Show waypoint properties panel
        ShowWaypointProperties(waypointNum)

        UpdateWayPointList(waypointNum, False)
    End Sub

    Public Sub ClearWayPoints()
        WaypointGridView.Rows.Clear()
        WaypointList.Clear()
    End Sub

    Private Sub WaypointGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles WaypointGridView.CellContentClick

        If e.RowIndex < WaypointList.Count And e.RowIndex > -1 Then

            'User marked a line, set to different colour
            SelectedWaypoint(e.RowIndex)


            webBrowser1.Document.InvokeScript("selectedWaypoint", New Object() {Nothing, e.RowIndex})
            If (e.RowIndex = 0 And IsNothing(WaypointGridView.Rows.Item(0).Cells("Delete").Value)) Then
                MessageBox.Show("why?")
                Return
            Else
                'Up
                If (e.ColumnIndex = WaypointGridView.ColumnCount - 3) Then
                    'Move selected waypoint up in the list
                    If (e.RowIndex > 0) Then 'Don´t move the top waypoint upward
                        SwapWaypointPositions(e.RowIndex, 1)
                    End If
                End If

                'Down
                If (e.ColumnIndex = WaypointGridView.ColumnCount - 2) Then
                    If (e.RowIndex < WaypointList.Count - 1) Then 'Don´t move the bottom waypoint down
                        SwapWaypointPositions(e.RowIndex, -1)
                    End If
                End If '


                'User clicked "Delete" button
                If (e.ColumnIndex = WaypointGridView.ColumnCount - 1) Then


                    If (e.RowIndex > -1) Then
                        'Ask if user is sure of the removal
                        If MessageBox.Show("Really delete waypoint?", "Really?", MessageBoxButtons.OKCancel, _
                                Nothing, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
                            'Remove the waypoint 
                            WaypointList.RemoveAt(e.RowIndex)
                            webBrowser1.Document.InvokeScript("deleteWaypoint", New Object() {e.RowIndex})
                            UpdateWayPointList(-1, True)
                        End If
                    End If
                End If
            End If
        End If

    End Sub


    Private Sub MissionPlanner_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        PanelMap.Top = 0
        PanelMap.Left = 0
        ResizeForm()
    End Sub

    Private Sub ButtonSaveMission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSaveMission.Click

        If (WaypointList.Count < 1) Then
            MessageBox.Show("No waypoints to save")
        Else
            SaveFileDialog1.Title = "Save mission file"
            SaveFileDialog1.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*"

            SaveFileDialog1.ShowDialog()

            If SaveFileDialog1.FileName <> "" Then

                Dim writer As New XmlTextWriter(SaveFileDialog1.FileName, Nothing)

                ' opens the document 
                writer.WriteStartDocument()
                writer.WriteStartElement("root")
                writer.WriteStartElement("Waypoints")


                For Each wp As Waypoint In WaypointList
                    writer.WriteStartElement("Waypoint")

                    writer.WriteStartElement("Number", " ")
                    writer.WriteString(wp.Number)
                    writer.WriteEndElement()

                    writer.WriteStartElement("Latitude", " ")
                    writer.WriteString(wp.Latitude)
                    writer.WriteEndElement()

                    writer.WriteStartElement("Longitude", " ")
                    writer.WriteString(wp.Longitude)
                    writer.WriteEndElement()

                    writer.WriteStartElement("Title", " ")
                    writer.WriteString(wp.Title)
                    writer.WriteEndElement()

                    writer.WriteStartElement("CommandType", " ")
                    writer.WriteString(wp.CommandType)
                    writer.WriteEndElement()

                    For i = 0 To wp.Parameters.Count - 1
                        writer.WriteStartElement("Parameter" + i.ToString, " ")
                        writer.WriteString(wp.Parameters(i).ToString)
                        writer.WriteEndElement()
                    Next

                    writer.WriteEndElement()
                Next

                writer.WriteEndDocument()

                writer.Close()
                MessageBox.Show("Mission saved.")
            End If

        End If
    End Sub

    Private Sub ButtonLoadMission_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoadMission.Click

        Dim myStream As Stream = Nothing

        OpenFileDialog1.InitialDirectory = System.Environment.CurrentDirectory
        OpenFileDialog1.Filter = "All files (*.*)|*.*|Xml files (*.xml)|*.xml"
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.RestoreDirectory = True

        Dim xmldoc As New XmlDocument()
        Dim xmlnode As XmlNodeList
        Dim i As Integer
        Dim strWaypoints As String = ""
        Dim dialogResult As DialogResult
        Dim waypoint As New Waypoint

        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            'ask user if we should append these waypoint to existing ones
            'Only show dialog if list is not empty
            If WaypointList.Count > 0 Then
                dialogResult = MessageBox.Show("Append waypoints to existing ones?" + Chr(13) + _
                                               "Click 'Yes' to append loaded waypoints to existing mission." + Chr(13) _
                                               + "Click 'No' to clear existing mission and load mission from file." + Chr(13) _
                                               + "Click 'Cancel' to abort loading of mission.", "Append waypoints?", MessageBoxButtons.YesNoCancel, Nothing, MessageBoxDefaultButton.Button1)

            End If

            'User did not select to abort loading
            If Not dialogResult = Windows.Forms.DialogResult.Cancel Then

                'user slected "No" if we should append waypoints to existing ones. Clear list 
                If dialogResult = Windows.Forms.DialogResult.No Then
                    WaypointList.Clear()
                End If

                'Load waypoints and add them to list
                Try
                    myStream = OpenFileDialog1.OpenFile()
                    xmldoc.Load(myStream)
                    xmlnode = xmldoc.GetElementsByTagName("Waypoint")


                    For i = 0 To xmlnode.Count - 1

                        waypoint = New Waypoint(
                            xmlnode(i).ChildNodes.Item(0).InnerText(),
                            xmlnode(i).ChildNodes.Item(1).InnerText(),
                            xmlnode(i).ChildNodes.Item(2).InnerText(),
                            xmlnode(i).ChildNodes.Item(4).InnerText(),
                            xmlnode(i).ChildNodes.Item(3).InnerText()
                        )

                        Dim parameters As New List(Of String)
                        ' parameters.Add("")              'Description
                        For j = 1 To 8
                            parameters.Add(xmlnode(i).ChildNodes.Item(4 + j).InnerText())
                        Next
                        'Console.WriteLine(parameters)
                        waypoint.Parameters = parameters

                        WaypointList.Add(waypoint)

                        'strWaypoints += xmlnode(i).ChildNodes.Item(0).InnerText.Trim() + ":" + xmlnode(i).ChildNodes.Item(1).InnerText.Trim() + ":" + xmlnode(i).ChildNodes.Item(2).InnerText.Trim()
                    Next
                Catch ex As Exception
                    'MessageBox.Show("Could not load mission! Reason: " + ex.Message)
                End Try

                'update gridview and send points to Google Maps
                UpdateWayPointList(-1, True)
                myStream.Close()
            End If
            'If dialogResult Then
            '    'don´t append, so clear earlier waypoints
            '    WaypointList.Clear()
            'End If

        End If

    End Sub


    Private Sub SplitContainer1_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainerWhole.SplitterMoved
        ResizeForm()
    End Sub

    Private Sub ResizeForm()
        SplitContainerMapWaypoints.Width = SplitContainerWhole.Panel1.Width
        SplitContainerMapWaypoints.Height = SplitContainerWhole.Panel1.Height

        PanelSettings.Left = 0
        PanelSettings.Top = 0
        PanelSettings.Width = SplitContainerWhole.Panel2.Width

        PanelMap.Top = 0
        PanelMap.Left = 0
        PanelMap.Width = SplitContainerMapWaypoints.Panel1.Width + 15
        PanelMap.Height = SplitContainerMapWaypoints.Panel1.Height

        WaypointGridView.Top = 0
        WaypointGridView.Width = PanelMap.Width + 20
        WaypointGridView.Height = SplitContainerMapWaypoints.Panel2.Height

        'PanelWaypointProperties.Height = SplitContainerWhole.Panel2.Height
        'PanelWaypointProperties.Top = 20

        PanelWaypointProperties.Left = SplitContainerWhole.Panel2.Width - PanelWaypointProperties.Width - 10
        PanelWaypointProperties.Top = SplitContainerWhole.Panel2.Height - PanelWaypointProperties.Height - 10


    End Sub

    Private Sub SwapWaypointPositions(ByVal row As Integer, ByVal direction As Integer)
        Dim tmp As Waypoint

        tmp = WaypointList.Item(row - direction)
        WaypointList.Item(row - direction) = WaypointList.Item(row)
        WaypointList.Item(row) = tmp

        UpdateWayPointList(-1, True)

        tmp = Nothing
    End Sub

    Private Sub ShowWaypointProperties(ByVal waypointnum As Integer)

        Dim parameterDescription As New List(Of String)     'Descriptions for each parameter
        Dim parameters As New List(Of String)               'Value for each parameter

        PanelWaypointProperties.Visible = False

        'Get all MAV_Command types
        For Each CommandType As MAV_command In MAV_commands_NAV
            ' If it is the same as selected waypoint, populate description list with its parameterdescriptions
            If WaypointList(waypointnum).CommandType.Equals(CommandType.Name) Then
                'Found the correct on, fill list
                For Each parameter As String In CommandType.Parameters
                    parameterDescription.Add(parameter)
                Next
            End If
        Next


        parameters = WaypointList(waypointnum).Parameters

        LabelParameter1.Text = parameterDescription(0)
        TextBoxParameter1.Text = parameters(1)

        LabelParameter2.Text = parameterDescription(1)
        TextBoxParameter2.Text = parameters(2)

        LabelParameter3.Text = parameterDescription(2)
        TextBoxParameter3.Text = parameters(3)

        LabelParameter4.Text = parameterDescription(3)
        TextBoxParameter4.Text = parameters(4)


        LabelParameter5.Text = parameterDescription(4)
        TextBoxParameter5.Text = parameters(5)

        If parameterDescription(4).Equals("Latitude") Then
            TextBoxParameter5.Text = WaypointList(waypointnum).Latitude
        End If


        LabelParameter6.Text = parameterDescription(5)
        TextBoxParameter6.Text = parameters(6)

        If parameterDescription(5).Equals("Longitude") Then
            TextBoxParameter6.Text = WaypointList(waypointnum).Longitude.Replace(" ", "")
        End If


        LabelParameter7.Text = parameterDescription(6)
        TextBoxParameter7.Text = parameters(7)

        'For i = 1 To parameterDescription.Count
        '    'get the label for current parameter and set parameter text
        '    controlName = "LabelParameter" + i.ToString()
        '    currentControl = TableLayoutPanel1.Controls(controlName)
        '    currentControl.Text = parameterDescription(i)

        '    'If parameters(i - 1) <> "" Then
        '    'get the textbox for current parameter
        '    controlName = "TextBoxParameter" + i.ToString()
        '    currentControl = TableLayoutPanel1.Controls(controlName)
        '    currentControl.Text = parameters(i)


        '    If parameterDescription(i).Equals("Latitude") Then
        '        currentControl.Text = WaypointList(waypointnum).Latitude
        '    End If
        '    If parameterDescription(i).Equals("Longitude") Then
        '        currentControl.Text = WaypointList(waypointnum).Longitude.Replace(" ", "")
        '    End If
        '    'End If


        'Next
        ComboBoxCommandType.Text = WaypointList(waypointnum).CommandType.ToString
        TextBoxWaypointTitle.Text = WaypointList(waypointnum).Title

        If waypointnum > -1 Then
            PanelWaypointProperties.Visible = True
            LabelWaypointNumber.Text = (WaypointList.Item(waypointnum).Number + 1).ToString()
        End If



    End Sub

    ''' <summary>
    ''' User changed the waypoint parameters, update waypoint values
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonSetNewWaypointValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSetNewWaypointValues.Click

        currentnum = CInt(LabelWaypointNumber.Text)
        Dim parameters As New List(Of String)

        If currentnum > 0 Then


            'set waypoints commandtype to selected value

            WaypointList(currentnum - 1).CommandType = ComboBoxCommandType.Text

            WaypointList(currentnum - 1).Title = TextBoxWaypointTitle.Text

            Dim currentControl As Control
            parameters.Add("") 'description
            For i = 1 To 7
                currentControl = TableLayoutPanel1.Controls("TextBoxParameter" + i.ToString())
                parameters.Add(currentControl.Text)

                currentControl = TableLayoutPanel1.Controls("LabelParameter" + i.ToString())

                If currentControl.Text.Equals("Latitude") Then
                    currentControl = TableLayoutPanel1.Controls("TextBoxParameter" + i.ToString())
                    WaypointList(currentnum - 1).Latitude = currentControl.Text
                End If
                If currentControl.Text.Equals("Longitude") Then
                    currentControl = TableLayoutPanel1.Controls("TextBoxParameter" + i.ToString())
                    WaypointList(currentnum - 1).Longitude = currentControl.Text
                End If
            Next

            WaypointList(currentnum - 1).Parameters = parameters

            UpdateWayPointList(currentnum - 1, True)
        End If
    End Sub


    ''' <summary>
    ''' User changed the CommandType in the dropdown, update parameter descriptions next to textboxes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub ComboBoxCommandType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)


        Dim parameterDescription As New List(Of String)     'Descriptions for each parameter
        Dim controlName As String
        Dim currentControl As Control

        '
        For Each mavcmd As MAV_command In MAV_commands_NAV
            If mavcmd.Name.Equals(ComboBoxCommandType.Text) Then
                parameterDescription = mavcmd.Parameters
                Exit For
            End If
        Next


        For i = 1 To 7
            'get the label for current parameter and set parameter text
            controlName = "LabelParameter" + i.ToString()
            currentControl = TableLayoutPanel1.Controls(controlName)
            currentControl.Text = parameterDescription(i - 1)

            'get the textbox for current parameter
            controlName = "TextBoxParameter" + i.ToString()
            currentControl = TableLayoutPanel1.Controls(controlName)



            ' ComboBoxCommandType.Text = currentWaypoint.CommandType.ToString
            ' MessageBox.Show(currentWaypoint.CommandType.ToString)
        Next
    End Sub

    Private Sub PopulateCommandTypeList()

        Dim myStream As Stream = Nothing

        Dim file As String = System.Environment.CurrentDirectory + "\common.xml"

        Dim xmldoc As New XmlDocument()
        Dim i As Integer
        Dim params As XmlNodeList



        Dim name As String = ""
        Dim description As String
        Dim CMDparameters As New List(Of String)
        Dim tmp As String = ""
        Dim parameterCu As String
        MAV_commands_NAV.Clear()
        Try

            myStream = New FileStream(file, FileMode.Open)
            xmldoc.Load(myStream)
            'xmlnode = xmldoc.GetElementsByTagName("MAV_CMD")
            Dim elemList As XmlNodeList = xmldoc.SelectNodes("//entry")

            'add those elements containing "NAV" (for navigation commands)
            CMDparameters.Clear()


            For i = 0 To elemList.Count - 1

                If (elemList(i).Attributes(0).InnerText.Contains("NAV")) Then

                    name = elemList(i).Attributes(0).InnerText.ToString


                    params = elemList(i).ChildNodes()
                    description = params(0).InnerText.ToString
                    CMDparameters = New List(Of String)

                    For j = 1 To 7
                        parameterCu = params(j).InnerText.ToString
                        CMDparameters.Add(parameterCu)

                    Next

                    'Create MAV_commands
                    Dim Mavcmd = New MAV_command(name.Replace("MAV_CMD_", ""), description, CMDparameters)
                    MAV_commands_NAV.Add(Mavcmd)

                    'add to dropdown, rempve "MAV_CMD_"
                    ComboBoxCommandType.Items.Add(Mavcmd.Name.Replace("MAV_CMD_", ""))

                End If
            Next

        Catch ex As Exception
            MessageBox.Show(" Reason @ : " + i.ToString + Chr(13) + ex.Message)
        End Try



    End Sub

    Private Sub SplitContainer2_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainerMapWaypoints.SplitterMoved
        ResizeForm()
    End Sub


    Private Sub SplitContainerWhole_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SplitContainerWhole.Resize
        ResizeForm()
    End Sub

End Class