Public Class ArduCalibrationSlider
    Private g As Graphics

    Private min As Integer = 900                      ' Minimum value for progress range
    Private max As Integer = 2100                    ' Maximum value for progress range
    Private val As Integer = 1500                   ' Current progres


    Private barColor As Color = Color.DarkGreen     ' Color of progress meter
    Private calmaxColor As Color = Color.Red           ' Color of calibration markers
    Private calminColor As Color = Color.Lime           ' Color of calibration markers
    Private calcenterColor As Color = Color.White      ' Color of center calibration marker
    Private bordColor As Color = Color.DarkGray       ' Color of the border
    Private drawvertical As Boolean = False         ' Should be draw vertical or not ?

    Private mincal As Integer = 1400                    'Minimum calibrated value
    Private midcal As Integer = 1500                    'Middle calibrated value or center position
    Private maxcal As Integer = 1600                    'Maximum calibrated value

    Private inverted As Boolean = False
    Private bOutsideRange As Boolean = False


    Private Sub ArduCalibrationProgressBar_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        g = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality


        Dim barbrush As SolidBrush = New SolidBrush(barColor)
        Dim percent As Decimal = (val - min) / (max - min)

        Dim rect As Rectangle = Me.ClientRectangle

        g.FillRegion(New SolidBrush(Me.BackColor), New Region(New Rectangle(0, 0, Me.Width, Me.Height)))

        ' Calculate area for drawing the progress.
        If inverted Then
            rect.Height = Me.ClientRectangle.Height - rect.Height * percent
        Else
            rect.Height = rect.Height * percent
        End If

        rect.Inflate(-24, -2)


        ' Draw the progress meter.
        Draw3DSlot(g, barbrush, rect)


        ' Draw a three-dimensional border around the control.
        'Draw3DBorder(g)

        ' Draw the calibration markers
        ' Max
        Dim mypos As Decimal
        If inverted Then
            mypos = ((maxcal - min) / (max - min) * Me.ClientRectangle.Height)
        Else
            mypos = Me.ClientRectangle.Height - ((maxcal - min) / (max - min) * Me.ClientRectangle.Height)
        End If
        DrawSliderControl(g, 1, mypos - 8, 40, 16, 12, maxcal, calmaxColor)

        ' Medium or center
        If inverted Then
            mypos = ((midcal - min) / (max - min) * Me.ClientRectangle.Height)
        Else
            mypos = Me.ClientRectangle.Height - ((midcal - min) / (max - min) * Me.ClientRectangle.Height)
        End If
        DrawSliderControl(g, 1, mypos - 8, 40, 16, 12, midcal, calcenterColor)

        ' Min
        If inverted Then
            mypos = ((mincal - min) / (max - min) * Me.ClientRectangle.Height)
        Else
            mypos = Me.ClientRectangle.Height - ((mincal - min) / (max - min) * Me.ClientRectangle.Height)
        End If
        DrawSliderControl(g, 1, mypos - 8, 40, 16, 12, mincal, calminColor)



        ' Clean up.
        'brush.Dispose()
        'g.Dispose()
    End Sub

    Public Sub Reset()
        min = 900                      ' Minimum value for progress range
        max = 2100                    ' Maximum value for progress range
        val = 1500
        mincal = 1400                    'Minimum calibrated value
        midcal = 1500                    'Middle calibrated value or center position
        maxcal = 1600                    'Maximum calibrated value
        bOutsideRange = False
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

            ' Make sure that the value does not stray outside the valid range(900-2100)
            If Value < 900 Then bOutsideRange = True

            If Value > 2100 Then bOutsideRange = True

            val = Value

            'Handle the calibration ticks
            If (Value < mincal) Then mincal = Value
            If (Value > maxcal) Then maxcal = Value

            If mincal < 900 Then mincal = 900
            If maxcal > 2100 Then maxcal = 2100

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
            Return calcenterColor
        End Get

        Set(ByVal Value As Color)
            calcenterColor = Value

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
    Public Property ShowInverted() As Boolean
        Get
            Return inverted
        End Get

        Set(ByVal Value As Boolean)
            inverted = Value

            ' Invalidate the control to get a repaint.
            Invalidate()
        End Set
    End Property

    ReadOnly Property CalibratedMinimum() As Integer
        Get
            Return mincal
        End Get
    End Property
    ReadOnly Property OutsideValidRange() As Boolean
        Get
            Return bOutsideRange
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

        bHeight = Me.ClientRectangle.Height
        bWidth = Me.ClientRectangle.Width

        'Left
        g.DrawLine(New Pen(Color.White, 1), New Point(Me.ClientRectangle.Left, Me.ClientRectangle.Top), New Point(Me.ClientRectangle.Left, bHeight - PenWidth))
        'top
        g.DrawLine(New Pen(Color.White, 1), New Point(Me.ClientRectangle.Left, Me.ClientRectangle.Top), New Point(bWidth - PenWidth, Me.ClientRectangle.Top))
        'Bottom
        g.DrawLine(New Pen(Color.DarkGray, 1), New Point(Me.ClientRectangle.Left, bHeight - PenWidth), New Point(bWidth - PenWidth, bHeight - PenWidth))
        'Right
        g.DrawLine(New Pen(Color.DarkGray, 1), New Point(bWidth - PenWidth, Me.ClientRectangle.Top), New Point(bWidth - PenWidth, bHeight - PenWidth))
    End Sub

    Private Sub Draw3DSlot(ByVal g As Graphics, ByVal barbrush As SolidBrush, ByVal Rect As RectangleF)
        Dim aHeight As Integer
        Dim aWidth As Integer
        Dim aTop As Integer
        Dim aLeft As Integer

        aHeight = Me.ClientRectangle.Height - 7
        aWidth = Me.ClientRectangle.Width - 45
        aTop = Me.ClientRectangle.Top + 3
        aLeft = Me.ClientRectangle.Left + 22

        'Fill first
        g.FillRectangle(New SolidBrush(Color.White), aLeft, aTop, aWidth, aHeight)
        g.FillRectangle(barbrush, aLeft, aTop + aHeight - Rect.Height - 2, aWidth, Rect.Height + 2)
        'Left line
        g.DrawLine(New Pen(Color.Black, 1), New Point(aLeft, aTop), New Point(aLeft, aTop + aHeight))
        'Top line
        g.DrawLine(New Pen(Color.Black, 1), New Point(aLeft, aTop), New Point(aLeft + aWidth, aTop))
        'Bottom line
        g.DrawLine(New Pen(Color.Black, 1), New Point(aLeft, aTop + aHeight), New Point(aLeft + aWidth, aTop + aHeight))
        'Right line
        g.DrawLine(New Pen(Color.Black, 1), New Point(aLeft + aWidth, aTop), New Point(aLeft + aWidth, aTop + aHeight))

    End Sub

    Public Sub DrawSliderControl(ByVal objGraphics As Graphics, ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer, ByVal d As Integer, ByVal value As Integer, ByVal MainColor As Color)
        Dim NormalFont As Font = New System.Drawing.Font("sans-serif", 9)
        Dim BaseRect As New RectangleF(x, y, w, h)
        Dim ArcRect As New RectangleF(BaseRect.Location, New SizeF(d, d))

        'top left Arc
        Dim rrectpath As New Drawing2D.GraphicsPath()
        Dim rrectpath1 As New Drawing2D.GraphicsPath()
        Dim rrectpath2 As New Drawing2D.GraphicsPath

        'Hightlight side border
        rrectpath1.AddLine(x, y + CInt(d / 2), x, y + h - CInt(d / 2))
        rrectpath1.AddArc(ArcRect, 180, 90)
        rrectpath1.AddLine(x + CInt(d / 2), y, x + w - CInt(d / 2), y)

        'Shadow side border
        ArcRect.X = BaseRect.Right - d
        rrectpath2.AddArc(ArcRect, 270, 90)
        rrectpath2.AddLine(x + w, y + CInt(d / 2), x + w, y + h - CInt(d / 2))
        ArcRect.Y = BaseRect.Bottom - d
        rrectpath2.AddArc(ArcRect, 0, 90)
        rrectpath2.AddLine(x + CInt(d / 2), y + h, x + w - CInt(d / 2), y + h)
        ArcRect.X = BaseRect.Left
        rrectpath2.AddArc(ArcRect, 90, 90)

        'Build complete border
        rrectpath = rrectpath1
        rrectpath.AddPath(rrectpath2, True)
        rrectpath.CloseFigure()

        'Create highlight colors programmatically
        Dim hColor As Color 'Highlights color
        Dim lColor As Color 'Shadow or lowlights color
        hColor = MainColor
        lColor = MainColor

        Dim ccR, ccG, ccB As Integer
        'Create the highlight color
        ccR = hColor.R + 120
        ccG = hColor.G + 120
        ccB = hColor.B + 120
        If ccR > 255 Then ccR = 255
        If ccG > 255 Then ccG = 255
        If ccB > 255 Then ccB = 255
        hColor = Color.FromArgb(ccR, ccG, ccB)
        'Create the lowlight color
        ccR = lColor.R - 80
        ccG = lColor.G - 80
        ccB = lColor.B - 80
        If ccR < 0 Then ccR = 0
        If ccG < 0 Then ccG = 0
        If ccB < 0 Then ccB = 0
        lColor = Color.FromArgb(ccR, ccG, ccB)

        'Create gradient fill brush
        Dim b1 As System.Drawing.Drawing2D.LinearGradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Rectangle(x, y, w, h), MainColor, hColor, -90)

        'Fill path and then draw the border
        g.FillPath(b1, rrectpath)
        g.DrawPath(New Pen(lColor, 1), rrectpath1)
        g.DrawPath(New Pen(lColor, 1), rrectpath2)

        'Write the value
        Dim sF As SizeF
        sF = g.MeasureString(value, NormalFont)
        g.DrawString(value, NormalFont, New SolidBrush(Color.Black), x + (w / 2) - (sF.Width / 2) + 1, y + (h / 2) - (sF.Height / 2) + 1)
        g.DrawString(value, NormalFont, New SolidBrush(Color.White), x + (w / 2) - (sF.Width / 2), y + (h / 2) - (sF.Height / 2))

    End Sub
End Class


