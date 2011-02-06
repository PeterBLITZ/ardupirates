Public Class ArduCalibrationProgressBar
    Private g As Graphics

    Private min As Integer = 900                      ' Minimum value for progress range
    Private max As Integer = 2100                    ' Maximum value for progress range
    Private val As Integer = 1600                   ' Current progres


    Private barColor As Color = Color.DarkGreen     ' Color of progress meter
    Private calmaxColor As Color = Color.Red           ' Color of calibration markers
    Private calminColor As Color = Color.Lime           ' Color of calibration markers
    Private centerColor As Color = Color.White      ' Color of center calibration marker
    Private bordColor As Color = Color.DarkGray       ' Color of the border
    Private drawvertical As Boolean = False         ' Should be draw vertical or not ?

    Private mincal As Integer = 1500                    'Minimum calibrated value
    Private midcal As Integer = 1500                    'Middle calibrated value or center position
    Private maxcal As Integer = 1500                    'Maximum calibrated value

    Private Sub ArduCalibrationProgressBar_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        g = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Dim NormalFont As Font = New System.Drawing.Font("sans-serif", 9)
        Dim brush As SolidBrush = New SolidBrush(barColor)
        Dim percent As Decimal = (val - min) / (max - min)

        Dim rect As Rectangle = Me.ClientRectangle

        g.FillRegion(New SolidBrush(Me.BackColor), New Region(New Rectangle(0, 0, Me.Width, Me.Height)))

        ' Calculate area for drawing the progress.
        rect.Width = rect.Width * percent
        rect.Inflate(-2, -18)


        ' Draw the progress meter.
        g.FillRectangle(brush, rect)

        ' Draw a three-dimensional border around the control.
        Draw3DBorder(g)

        ' Draw the calibration marks
        ' Max
        Dim mypos As Decimal
        mypos = (maxcal - min) / (max - min) * Me.ClientRectangle.Width
        DrawRoundedRectangle(g, calmaxColor, mypos - 2, 0, 4, Me.ClientRectangle.Height - 1, 1)

        ' Medium or center
        mypos = (midcal - min) / (max - min) * Me.ClientRectangle.Width


        DrawRoundedRectangle(g, centerColor, mypos, 0, 4, Me.ClientRectangle.Height - 1, 1)

        ' Min
        mypos = (mincal - min) / (max - min) * Me.ClientRectangle.Width


        DrawRoundedRectangle(g, calminColor, mypos - 2, 0, 4, Me.ClientRectangle.Height - 1, 1)



        ' Clean up.
        'brush.Dispose()
        'g.Dispose()
    End Sub

    Public Property Minimum() As Integer
        Get
            Return min
        End Get

        Set(ByVal Value As Integer)
            ' Prevent a negative value.
            If (Value < 0) Then
                'min = 0
            End If

            ' Make sure that the minimum value is never set higher than the maximum value.
            If (Value > max) Then
                'min = Value
            End If

            ' Make sure that the value is still in range.
            If (val < min) Then
                ''val = min
            End If
            min = Value

            ' Invalidate the control to get a repaint.
            Invalidate()
        End Set
    End Property

    Public Property Vertical() As Boolean
        Get
            Return drawvertical
        End Get

        Set(ByVal Vertical As Boolean)
            drawvertical = Vertical

            ' Invalidate the control to get a repaint.
            Invalidate()
        End Set
    End Property

    Public Property Maximum() As Integer
        Get
            Return max
        End Get

        Set(ByVal Value As Integer)
            ' Make sure that the maximum value is never set lower than the minimum value.
            If (Value < min) Then
                min = Value
            End If

            max = Value

            ' Make sure that the value is still in range.
            If (val > max) Then
                val = max
            End If

            ' Invalidate the control to get a repaint.
            Invalidate()
        End Set
    End Property

    Public Property Value() As Integer
        Get
            Return val
        End Get

        Set(ByVal Value As Integer)
            Dim oldValue As Integer = val

            ' Make sure that the value does not stray outside the valid range.
            If (Value < min) Then
                val = min
            ElseIf (Value > max) Then
                val = max
            Else
                val = Value
            End If

            'Handle the calibration ticks
            If (Value < mincal) Then mincal = Value
            If (Value > maxcal) Then maxcal = Value
            midcal = ((maxcal - mincal) / 2) + mincal

            Invalidate()
        End Set
    End Property

    Public Property ProgressBarColor() As Color
        Get
            Return barColor
        End Get

        Set(ByVal Value As Color)
            barColor = Value

            ' Invalidate the control to get a repaint.
            Invalidate()
        End Set
    End Property

    Public Property CalibrationMaxColor() As Color
        Get
            Return calmaxColor
        End Get

        Set(ByVal Value As Color)
            calmaxColor = Value

            ' Invalidate the control to get a repaint.
            Invalidate()
        End Set
    End Property
    Public Property CalibrationMinColor() As Color
        Get
            Return calminColor
        End Get

        Set(ByVal Value As Color)
            calminColor = Value

            ' Invalidate the control to get a repaint.
            Invalidate()
        End Set
    End Property

    Public Property CalibrationCenterColor() As Color
        Get
            Return centerColor
        End Get

        Set(ByVal Value As Color)
            centerColor = Value

            ' Invalidate the control to get a repaint.
            Invalidate()
        End Set
    End Property

    Public Property BorderColor() As Color
        Get
            Return bordColor
        End Get

        Set(ByVal Value As Color)
            bordColor = Value

            ' Invalidate the control to get a repaint.
            Invalidate()
        End Set
    End Property

    ReadOnly Property CalibratedMinimum() As Integer
        Get
            Return mincal
        End Get
    End Property

    ReadOnly Property CalibratedMaximum() As Integer
        Get
            Return maxcal
        End Get
    End Property

    ReadOnly Property CalibratedCenter() As Integer
        Get
            Return midcal
        End Get
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Draw3DBorder(ByVal g As Graphics)
        Dim PenWidth As Integer = Pens.White.Width
        Dim bHeight As Integer
        Dim bWidth As Integer
        Dim Offset As Integer

        bHeight = Me.ClientRectangle.Height
        bWidth = Me.ClientRectangle.Width


        Offset = 0

        g.DrawLine(New Pen(bordColor, 1), New Point(Me.ClientRectangle.Left + Offset, Me.ClientRectangle.Top), New Point(Me.ClientRectangle.Left + Offset, bHeight - PenWidth))
        g.DrawLine(New Pen(bordColor, 1), New Point(Me.ClientRectangle.Left, Me.ClientRectangle.Top), New Point(bWidth - PenWidth, Me.ClientRectangle.Top))
        g.DrawLine(New Pen(bordColor, 1), New Point(Me.ClientRectangle.Left, bHeight - PenWidth), New Point(bWidth - PenWidth, bHeight - PenWidth))
        g.DrawLine(New Pen(bordColor, 1), New Point(bWidth - PenWidth + Offset, Me.ClientRectangle.Top), New Point(bWidth - PenWidth + Offset, bHeight - PenWidth))
    End Sub

    Public Sub DrawRoundedRectangle(ByVal objGraphics As Graphics, ByVal Color As Color, ByVal m_intxAxis As Integer, ByVal m_intyAxis As Integer, ByVal m_intWidth As Integer, ByVal m_intHeight As Integer, ByVal m_diameter As Integer)

        Dim BaseRect As New RectangleF(m_intxAxis, m_intyAxis, m_intWidth, m_intHeight)
        Dim ArcRect As New RectangleF(BaseRect.Location, New SizeF(m_diameter, m_diameter))
        'top left Arc
        Dim rrectpath As New Drawing2D.GraphicsPath()
        rrectpath.AddArc(ArcRect, 180, 90)
        rrectpath.AddLine(m_intxAxis + CInt(m_diameter / 2), m_intyAxis, m_intxAxis + m_intWidth - CInt(m_diameter / 2), m_intyAxis)

        ' top right arc
        ArcRect.X = BaseRect.Right - m_diameter
        rrectpath.AddArc(ArcRect, 270, 90)
        rrectpath.AddLine(m_intxAxis + m_intWidth, m_intyAxis + CInt(m_diameter / 2), m_intxAxis + m_intWidth, m_intyAxis + m_intHeight - CInt(m_diameter / 2))

        ' bottom right arc
        ArcRect.Y = BaseRect.Bottom - m_diameter
        rrectpath.AddArc(ArcRect, 0, 90)
        rrectpath.AddLine(m_intxAxis + CInt(m_diameter / 2), m_intyAxis + m_intHeight, m_intxAxis + m_intWidth - CInt(m_diameter / 2), m_intyAxis + m_intHeight)

        ' bottom left arc
        ArcRect.X = BaseRect.Left
        rrectpath.AddArc(ArcRect, 90, 90)
        rrectpath.AddLine(m_intxAxis, m_intyAxis + CInt(m_diameter / 2), m_intxAxis, m_intyAxis + m_intHeight - CInt(m_diameter / 2))

        rrectpath.CloseFigure()


        g.FillPath(New SolidBrush(Color), rrectpath)
        g.DrawPath(New Pen(Color.Black, 1), rrectpath)


    End Sub
End Class


