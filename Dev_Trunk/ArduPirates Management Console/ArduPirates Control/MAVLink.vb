''' <summary>''' Module for functions to use with MAVLink.
''' Based on a module written by HappyKillmore (happy@happykillmore.com) 
''' Extended for APMC by Krokodil
''' </summary>
''' <remarks></remarks>
Module MAVLink
    Const X25_INIT_CRC As Integer = &HFFFF
    Const X25_VALIDATE_CRC As Integer = &HF0B8

    Const MAVLINK_STX As Integer = &H55
    Const MAVLINK_STX_LEN As Integer = 1    '///< Length of start sign
    Const Ma As Integer = 255               '///< Maximum payload length
    Const MAVLINK_CORE_HEADER_LEN As Integer = 5 '///< Length of core header (of the comm. layer): message length (1 byte) + message sequence (1 byte) + message system id (1 byte) + message component id (1 byte) + message type id (1 byte)
    Const MAVLINK_NUM_HEADER_BYTES As Integer = (MAVLINK_CORE_HEADER_LEN + MAVLINK_STX_LEN) '///< Length of all header bytes, including core and checksum
    Const MAVLINK_NUM_CHECKSUM_BYTES As Integer = 2
    Const MAVLINK_NUM_NON_PAYLOAD_BYTES As Integer = (MAVLINK_NUM_HEADER_BYTES + MAVLINK_NUM_CHECKSUM_BYTES)
    Const MAVLINK_NUM_NON_STX_PAYLOAD_BYTES As Integer = (MAVLINK_NUM_NON_PAYLOAD_BYTES - MAVLINK_STX_LEN)


    Public Function ConvertMavlinkToSingle(ByVal inputString As String) As Single
        Dim sTemp As String = ""
        Dim bByte(0 To 3) As Byte
        Dim nCount As Integer

        For nCount = Len(inputString) To 1 Step -1
            bByte(4 - nCount) = CByte(Asc(inputString.Substring(nCount - 1, 1)))
        Next
        ConvertMavlinkToSingle = BitConverter.ToSingle(bByte, 0)
        If Double.IsNaN(ConvertMavlinkToSingle) Then
            ConvertMavlinkToSingle = 0
        End If
    End Function

    Public Function ConvertMavlinkToInteger(ByVal inputString As String, Optional ByVal is2sCompliment As Boolean = False) As Integer
        If inputString.Length < 2 Then
            ConvertMavlinkToInteger = CInt("&h" & Hex(Asc(Mid(inputString, 1, 1))).PadLeft(2, "0"))
        Else
            Dim stri As String = ("&h" & Hex(Asc(Mid(inputString, 1, 1))).PadLeft(2, "0") & Hex(Asc(Mid(inputString, 2, 1))).PadLeft(2, "0"))
            ConvertMavlinkToInteger = CInt(stri)

        End If


        If is2sCompliment = True Then
            If ConvertMavlinkToInteger > CInt("&h7FFF") Then
                ConvertMavlinkToInteger = -(((2 ^ (4 * 4) - 1) Xor ConvertMavlinkToInteger) + 1)
            End If
        End If
    End Function

    Public Function ConvertMavlinkToInteger32(ByVal inputString As String, Optional ByVal is2sCompliment As Boolean = False) As Integer
        If inputString.Length < 2 Then
            ConvertMavlinkToInteger32 = CInt("&h" & Hex(Asc(Mid(inputString, 1, 1))).PadLeft(2, "0") & Hex(Asc(Mid(inputString, 2, 1))).PadLeft(2, "0") & Hex(Asc(Mid(inputString, 3, 1))).PadLeft(2, "0") & Hex(Asc(Mid(inputString, 4, 1))).PadLeft(2, "0"))
        Else
            ConvertMavlinkToInteger32 = CInt("&h" & Hex(Asc(Mid(inputString, 1, 1))).PadLeft(2, "0") & Hex(Asc(Mid(inputString, 2, 1))).PadLeft(2, "0") & Hex(Asc(Mid(inputString, 3, 1))).PadLeft(2, "0") & Hex(Asc(Mid(inputString, 4, 1))).PadLeft(2, "0"))
        End If

        If is2sCompliment = True Then
            If ConvertMavlinkToInteger32 > CInt("&h7FFFFFFF") Then
                ConvertMavlinkToInteger32 = -(((2 ^ (4 * 4) - 1) Xor ConvertMavlinkToInteger32) + 1)
            End If
        End If
    End Function

    Public Function ConvertMavlinkToLong(ByVal inputString As String, Optional ByVal is2sCompliment As Boolean = False) As Long
        Dim sTemp As String = ""
        Dim bByte(0 To 7) As Byte
        Dim nCount As Integer

        sTemp = ""
        For nCount = 1 To Len(inputString)
            sTemp = sTemp & Hex(Asc(Mid(inputString, nCount, 1))).PadLeft(2, "0")
        Next

        ConvertMavlinkToLong = CLng("&h" & sTemp)
        If is2sCompliment = True Then
            If ConvertMavlinkToLong > CInt("&h7FFFFFFF") Then
                ConvertMavlinkToLong = -(((2 ^ (4 * 4) - 1) Xor ConvertMavlinkToLong) + 1)
            End If
        End If
    End Function

    Public Function ConvertInteger32ToMavlink(ByVal inputValue As Integer, Optional ByVal is2sCompliment As Boolean = False) As String
        Dim sTemp As String
        If inputValue > CInt("&h7FFFFF") Then
            inputValue = -(((2 ^ (4 * 4) - 1) Xor inputValue) + 1)
        End If
        sTemp = Hex(inputValue).PadLeft(8, "0")
        ConvertInteger32ToMavlink = Chr("&H" & Mid(sTemp, 1, 2)) & Chr("&H" & Mid(sTemp, 3, 2)) & Chr("&H" & Mid(sTemp, 5, 2)) & Chr("&H" & Mid(sTemp, 7, 2))
    End Function

    Public Function ConvertIntegerToMavlink(ByVal inputValue As Integer, Optional ByVal is2sCompliment As Boolean = False) As String
        Dim sTemp As String
        If inputValue > CInt("&h7FFF") Then
            inputValue = -(((2 ^ (4 * 4) - 1) Xor inputValue) + 1)
        End If
        sTemp = Hex(inputValue).PadLeft(4, "0")
        ConvertIntegerToMavlink = Chr("&H" & Mid(sTemp, 1, 2)) & Chr("&H" & Mid(sTemp, 3, 2))
    End Function

    Public Function GetMAVlinkDate(ByVal inputValue As Long) As Date
        Dim dTempDate As Date

        Dim cf As System.Globalization.CultureInfo
        cf = New System.Globalization.CultureInfo("en-US")
        dTempDate = DateAdd(DateInterval.Second, inputValue / 1000, dTempDate.Parse("1/1/1970", cf))
        GetMAVlinkDate = dTempDate.Date
    End Function

    Public Function GetMAVlinkTime(ByVal inputValue As Long) As Date
        Dim dTempDate As Date

        Dim cf As System.Globalization.CultureInfo
        cf = New System.Globalization.CultureInfo("en-US")
        dTempDate = DateAdd(DateInterval.Second, inputValue / 1000, dTempDate.Parse("1/1/1970", cf))
        GetMAVlinkTime = dTempDate.ToLongTimeString
    End Function

    Public Function MavlinkScaledToStandard(ByVal inputValue As Integer) As Integer
        MavlinkScaledToStandard = (((inputValue + 10000) / 20000) * 1000) + 1000
    End Function

    Public Function GetMavAction(ByVal inputAction As Integer) As String
        Select Case inputAction

            Case 1
                GetMavAction = "Motor Start"
            Case 2
                GetMavAction = "Launch"
            Case 3
                GetMavAction = "Return"
            Case 4
                GetMavAction = "Emergency Land"
            Case 5
                GetMavAction = "Emergency Kill"
            Case 6
                GetMavAction = "Confirm Kill"
            Case 7
                GetMavAction = "Continue"
            Case 8
                GetMavAction = "Motor Stop"
            Case 9
                GetMavAction = "Halt"
            Case 10
                GetMavAction = "Shutdown"
            Case 11
                GetMavAction = "Reboot"
            Case 12
                GetMavAction = "Manual Mode"
            Case 13
                GetMavAction = "Auto Mode"
            Case 14
                GetMavAction = "Storage Read"
            Case 15
                GetMavAction = "Storage Write"
            Case 16
                GetMavAction = "Calibrate R/C"
            Case 17
                GetMavAction = "Calibrate Gyro"
            Case 18
                GetMavAction = "Calibrate Mag"
            Case 19
                GetMavAction = "Calibrate Acc"
            Case 20
                GetMavAction = "Calibrate Pressure"
            Case 21
                GetMavAction = "Rec Start"
            Case 22
                GetMavAction = "Rec Pause"
            Case 23
                GetMavAction = "Rec Stop"
            Case 24
                GetMavAction = "Take-off"
            Case 25
                GetMavAction = "Navigate"
            Case 26
                GetMavAction = "Land"
            Case 27
                GetMavAction = "Loiter"
        End Select
    End Function

    Public Function GetMavMode(ByVal inputMode As Integer) As String
        Select Case inputMode
            Case 1
                GetMavMode = "Locked"
            Case 2
                GetMavMode = "Manual"
            Case 3
                GetMavMode = "Guided"
            Case 4
                GetMavMode = "Auto"
            Case 5
                GetMavMode = "Test1"
            Case 6
                GetMavMode = "Test2"
            Case 7
                GetMavMode = "Test3"
            Case 8
                GetMavMode = "Ready"
            Case 9
                GetMavMode = "R/C Training"
        End Select
    End Function

    Public Function GetMavState(ByVal inputState As Integer) As String
        Select Case inputState
            Case 0
                GetMavState = "Initializing"
            Case 1
                GetMavState = "Booting"
            Case 2
                GetMavState = "Calibrating"
            Case 3
                GetMavState = "Standby"
            Case 4
                GetMavState = "Active"
            Case 5
                GetMavState = "Critical"
            Case 6
                GetMavState = "Emergency"
            Case 7
                GetMavState = "Power Off"
        End Select
    End Function

    Public Function GetMavNav(ByVal inputNav As Integer) As String
        Select Case inputNav
            Case 0
                GetMavNav = "Grounded"
            Case 1
                GetMavNav = "Lift-off"
            Case 2
                GetMavNav = "Hold"
            Case 3
                GetMavNav = "Waypoint"
            Case 4
                GetMavNav = "Vector"
            Case 5
                GetMavNav = "Returning"
            Case 6
                GetMavNav = "Landing"
            Case 7
                GetMavNav = "Lost"
        End Select
    End Function

    '--------------------------------------MAVlink packets--------------------------------------

    ''' <summary>
    ''' Create a MAVLink "heartbeat" packet
    ''' </summary>
    ''' <returns>The finalized packet</returns>
    ''' <remarks></remarks>
    Function mavlink_msg_heartbeat_pack(ByRef msg As MAVLINK_msg) As String
        msg.msgid = MAVLINK_msg.ID.HEARTBEAT    ' MessageType

        'Create the packet payload (with type, autopilot type and MAVLink version)
        msg.payload = ConvertIntegerToMavlink(MAVLINK_msg.MAV_TYPE.QUADROTOR) _
            & ConvertIntegerToMavlink(MAVLINK_msg.MAV_AUTOPILOT_TYPE.GENERIC) _
            & ConvertIntegerToMavlink(2)

        'Create the final packet
        Return mavlink_finalize_message(msg, msg.sysid, msg.compid)

    End Function


    ''' <summary>
    ''' Create a MAVLink "set_altitude" packet
    ''' </summary>
    ''' <param name="msg">The message to pack</param>
    ''' <param name="target">the system setting for "Altitude"</param>
    ''' <param name="mode">The new altitude in meters</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function mavlink_msg_set_altitude_pack(ByRef msg As MAVLINK_msg, ByVal target As Integer, ByVal mode As Integer) As String
        msg.msgid = MAVLINK_msg.ID.SET_ALTITUDE     ' MessageType

        'Create the packet payload. Target alt and mode
        msg.payload = ConvertIntegerToMavlink(target) _
                    & ConvertIntegerToMavlink(mode)

        Return mavlink_finalize_message(msg, msg.sysid, msg.compid)

    End Function


    ''' <summary>
    ''' Pack a MAVLink packet and create (and append) checksum
    ''' </summary>
    ''' <param name="msg">The MAVLink message to pack</param>
    ''' <param name="system_id">The SystemID</param>
    ''' <param name="component_id">The componentID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mavlink_finalize_message(ByRef msg As MAVLINK_msg, ByVal system_id As Integer, ByVal component_id As Integer) As String

        Dim message As String = ""
        Dim checksum As String = ""
        message = message & Chr(85)             ' "U"
        message = message & Chr(Len(msg.payload))         ' payload length
        message = message & Chr(0)              ' Packet sequence (
        message = message & Chr(system_id)      ' Systemid 
        message = message & Chr(component_id)   ' Componentid
        message = message & Chr(msg.msgid)      ' Message id (Type of message)

        message = message & msg.payload         ' The actual data 

        'msg.seq = msg.seq + 1

        'Create and append the checksum
        message = message & crc_calculate(message)

        Return message

    End Function


    'Private Function crc_accumulate2(ByVal data As UShort, ByRef crcAccum As UShort) As UShort
    '    '/*Accumulate one byte of data into the CRC*/

    '    Dim tmp As UShort

    '    tmp = (data Xor CUShort(crcAccum And &HFF))
    '    tmp = CUShort(tmp Xor (tmp << 4))
    '    crc_accumulate2 = CInt(crcAccum >> 8) Xor (tmp << 8) Xor (tmp << 3) Xor (tmp >> 4)



    '    'tmp=data ^ (uint8_t)(*crcAccum &0xff);
    '    'tmp^= (tmp<<4);
    '    '*crcAccum = (*crcAccum>>8) ^ (tmp<<8) ^ (tmp <<3) ^ (tmp>>4);


    'End Function

    'Public Function crc_calculate2(ByRef buffer As UShort) As UShort
    '    Dim crcTmp As UShort
    '    Dim pTmp As UShort
    '    Dim i As Integer

    '    crcTmp = X25_INIT_CRC

    '    For i = 0 To Len(buffer) + MAVLINK_CORE_HEADER_LEN
    '        crcTmp = crc_accumulate2(i, 1)
    '        'crcTmp = crc_accumulate_(Asc(Mid(InputString, i, 1)), crcTmp)
    '    Next


    'End Function

End Module

Class MAVLINK_msg

    Public Enum ID
        HEARTBEAT = 0
        BOOT = 1
        SYTSTEM_TIME = 2
        PING = 3
        SYSTEM_TIME_UTC = 4
        ACTION_ACK = 9
        ACTION = 10
        SET_MODE = 11
        SET_NAV_MODE = 12
        MAV_CMD_NAV_WAYPOINT = 16
        MAV_CMD_NAV_LOITER_UNLIM = 17
        MAV_CMD_NAV_LOITER_TURNS = 18
        PARAM_REQUEST_READ = 20
        PARAM_REQUEST_LIST = 21
        PARAM_VALUE = 22
        PARAM_SET = 23
        SCALED_IMU = 26
        GPS_STATUS = 27
        RAW_IMU = 28
        ATTUTIDE = 30
        GLOBAL_POSITION = 33
        WAYPOINT = 39
        WAYPOINT_REQUEST = 40
        WAYPOINT_SET_CURRENT = 41
        WAYPOINT_CURRENT = 42
        WAYPOINT_REQUEST_LIST = 43
        WAYPOINT_COUNT = 44
        WAYPOINT_CLEAR_ALL = 45
        WAYPOINT_REACHED = 46
        WAYPOINT_ACK = 47
        SET_ALTITUDE = 65
        VFR_HUD = 74
    End Enum

    Public Enum MAV_TYPE
        QUADROTOR = 2
    End Enum

    Public Enum MAV_AUTOPILOT_TYPE
        GENERIC = 0
        Ardupilot = 3
    End Enum


    Dim _len As UShort         '< Length of payload
    Dim _seq As UShort         '< Sequence of packet
    Dim _sysid As UShort       '< ID of message sender system/aircraft
    Dim _compid As UShort      '< ID of the message sender component
    Dim _msgid As UShort       '< ID of message in payload
    Dim _payload As String      '< Payload data, ALIGNMENT IMPORTANT ON MCU
    Dim _checksum As UShort      '< Checksum 


    Sub New(ByVal msgid As UShort)
        _msgid = msgid
    End Sub

    Public Property checksum As UShort
        Get
            Return _checksum
        End Get
        Set(ByVal value As UShort)
            _checksum = value
        End Set
    End Property

    Public Property payload As String
        Get
            Return _payload
        End Get
        Set(ByVal value As String)
            _payload = value
        End Set
    End Property

    Public Property len As UShort
        Get
            Return _len
        End Get
        Set(ByVal value As UShort)
            _len = value
        End Set
    End Property

    Public Property compid As UShort
        Get
            Return _compid
        End Get
        Set(ByVal value As UShort)
            _compid = value
        End Set
    End Property

    Public Property sysid As UShort
        Get
            Return _sysid
        End Get
        Set(ByVal value As UShort)
            _sysid = value
        End Set
    End Property

    Public Property seq As UShort
        Get
            Return _seq
        End Get
        Set(ByVal value As UShort)
            _seq = value
        End Set
    End Property


    Public Property msgid As UShort
        Get
            Return _msgid
        End Get
        Set(ByVal value As UShort)
            _msgid = value
        End Set
    End Property


End Class
