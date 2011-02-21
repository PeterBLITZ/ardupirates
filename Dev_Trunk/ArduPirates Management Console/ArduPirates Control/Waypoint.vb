
Public Class Waypoint



    Dim num As Integer                                      'Waypoint
    Dim lat As String
    Dim lng As String
    Dim _title As String
    Dim cmdType As String = "CMD_NAV_WAYPOINT"
    Dim _parameters As List(Of String)

    ''' <summary>
    ''' Creates a new Waypoint
    ''' </summary>
    ''' <param name="Latitude">Waypoints latitude</param>
    ''' <param name="Longitude">Waypoints longitude</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Number As Integer, ByVal Latitude As String, ByVal Longitude As String, ByVal mav_cmd As String, Optional ByVal Title As String = "")
        lat = Latitude
        lng = Longitude
        num = Number
        cmdType = mav_cmd

        If Title <> "" Then
            _title = Title
        End If

        _parameters = New List(Of String)
        _parameters.Add("Description")
        For i = 1 To 7
            _parameters.Add("Parameter " + i.ToString)
        Next
    End Sub

    Public Sub New()
        lat = 0
        lng = 0
    End Sub

    Public Property Number As Integer
        Get
            Return num
        End Get

        Set(ByVal Value As Integer)
            num = Value
        End Set
    End Property

    ''' <summary>
    ''' Returns a Google Maps compatible position of the waypoint
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns a Google Maps compatible position of the waypoint</returns>
    ''' <remarks></remarks>
    ''' 
    Public Property Position As String
        Get
            Return "(" + Latitude + ", " + Longitude + ")"
        End Get
        Set(ByVal Value As String)
            Dim tmp As Array
            tmp = Value.Split(",")
            lat = tmp(0).ToString().Replace("(", "").Replace(" ", "")
            lng = tmp(1).ToString().Replace(")", "").Replace(" ", "")
        End Set
    End Property

    Public Property Latitude As String
        Get
            Return lat
        End Get

        Set(ByVal Value As String)
            lat = Value
        End Set
    End Property

    Public Property Longitude As String
        Get
            Return lng
        End Get

        Set(ByVal Value As String)
            lng = Value
        End Set
    End Property

    Public Property Title As String
        Get
            Return _title
        End Get

        Set(ByVal Value As String)
            _title = Value
        End Set
    End Property

    Public Property CommandType As String
        Get
            Return cmdType
        End Get
        Set(ByVal value As String)
            cmdType = value
        End Set
    End Property


    Public Property Parameters As List(Of String)
        Get
            Return _parameters
        End Get
        Set(ByVal value As List(Of String))
            _parameters = value
        End Set
    End Property


End Class



Public Class MAV_command

    Dim _description As String
    Dim _parameters As List(Of String)
    Dim _name As String

    Public Sub New(ByVal Name As String, ByVal Description As String, ByVal Parameters As List(Of String))
        _name = Name
        _description = Description
        _parameters = Parameters
    End Sub

    Public ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property Description As String
        Get
            Return _description
        End Get
    End Property

    Public Property Parameters As List(Of String)
        Get
            Return _parameters
        End Get
        Set(ByVal value As List(Of String))
            _parameters  =value
        End Set
    End Property

End Class