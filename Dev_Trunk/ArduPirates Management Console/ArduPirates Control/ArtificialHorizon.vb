Imports System.Drawing

Public Class ArtificialHorizon

    Private g As Graphics

    Private NormalFont As Font = New System.Drawing.Font("sans-serif", 9)
    Private BigFont As Font = New System.Drawing.Font("sans-serif", 9, FontStyle.Bold)

    Private Pen1 As Pen = New Pen(Color.Lime, 2)
    Private Pen2 As Pen = New Pen(Color.Lime, 3)
    Private Pen3 As Pen = New Pen(Color.Lime, 1)
    Private Pen4 As Pen = New Pen(Color.White, 1)
    Private Pen5 As Pen = New Pen(Color.Lime, 2)


    Private _roll_angle As Double
    Public Property roll_angle() As Double
        Get
            Return _roll_angle
        End Get
        Set(ByVal value As Double)
            _roll_angle = value
            Invalidate()
        End Set
    End Property
    Private _pitch_angle As Double
    Public Property pitch_angle() As Double
        Get
            Return _pitch_angle
        End Get
        Set(ByVal value As Double)
            _pitch_angle = value
            Invalidate()
        End Set
    End Property
    Private _yaw_angle As Double
    Public Property yaw_angle() As Double
        Get
            Return _yaw_angle
        End Get
        Set(ByVal value As Double)
            _yaw_angle = value
            Invalidate()
        End Set
    End Property
    Private _altitude As Double
    Public Property altitude() As Double
        Get
            Return _altitude
        End Get
        Set(ByVal value As Double)
            _altitude = value
            Invalidate()
        End Set
    End Property

    Private Function pitch_to_pix(ByVal pitch As Double) As Integer
        Return pitch / 35.0 * Me.Height / 2
        'Return pitch / 45.0 * Me.Height / 2
    End Function

    Private Function yaw_to_pix(ByVal yaw As Double) As Integer
        Return (yaw / 35.0) * Me.Width / 2 + Me.Width / 4
    End Function

    Private Function alt_to_pix(ByVal alt As Double) As Integer
        Return (alt / 45.0) * Me.Height / 2
    End Function

    Overloads Function CopyBitmap(ByVal source As Bitmap) As Bitmap
        Return New Bitmap(source)
    End Function


    Private Sub ArtificialHorizon_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        g = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        'g.Clear(Me.BackColor)


        Dim sin As Double = Math.Sin(roll_angle / 180 * 3.14)

        g.ResetTransform()
        g.FillRegion(New SolidBrush(Me.BackColor), New Region(New Rectangle(0, 0, Me.Width, Me.Height)))

        ' rounded rectangle bordering around the ADI
        Dim borderpath As New Drawing2D.GraphicsPath()
        Dim r As Single = 50
        borderpath.AddArc(0, 0, r, r, 180, 90)
        borderpath.AddArc(Me.Width - r, 0, r, r, 270, 90)
        borderpath.AddArc(Me.Width - r, Me.Height - r, r, r, 0, 90)
        borderpath.AddArc(0, Me.Height - r, r, r, 90, 90)
        borderpath.CloseFigure()
        g.SetClip(borderpath)

        g.TranslateTransform(Me.Width / 2, Me.Height / 2)

        g.RotateTransform(roll_angle)
        g.TranslateTransform(0, pitch_to_pix(pitch_angle))

        ' Draw ground, bottom portion
        Dim b As New System.Drawing.Drawing2D.LinearGradientBrush(New RectangleF(-Me.Width, 0, Me.Height * 2, Me.Width * 2), Color.FromArgb(255, 145, 111, 0), Color.FromArgb(255, 244, 208, 88), Drawing2D.LinearGradientMode.Vertical)
        g.FillRectangle(b, New RectangleF(-Me.Width * 2, +30, Me.Height * 4, Me.Width * 4))

        ' Draw ground, portion nearest the horizon
        b = New System.Drawing.Drawing2D.LinearGradientBrush(New RectangleF(-Me.Width, +1, Me.Height * 2, 31), Color.FromArgb(255, 255, 228, 10), Color.FromArgb(255, 145, 111, 0), Drawing2D.LinearGradientMode.Vertical)
        g.FillRectangle(b, New RectangleF(-Me.Width * 2, +1, Me.Height * 4, +30))

        g.RotateTransform(180) 'Flip around

        ' Draw sky, upper part
        b = New System.Drawing.Drawing2D.LinearGradientBrush(New RectangleF(-Me.Width, 91, Me.Height * 2, Me.Width * 2), Color.FromArgb(255, 58, 117, 181), Color.FromArgb(255, 0, 39, 89), Drawing2D.LinearGradientMode.Vertical)
        g.FillRectangle(b, New RectangleF(-Me.Width * 2, 90, Me.Height * 4, Me.Width * 4))

        'Draw sky, lower part (near horizon)
        b = New System.Drawing.Drawing2D.LinearGradientBrush(New RectangleF(-Me.Width, 0, Me.Width * 2, 93), Color.FromArgb(255, 176, 212, 236), Color.FromArgb(255, 58, 117, 181), Drawing2D.LinearGradientMode.Vertical)
        g.FillRectangle(b, New RectangleF(-Me.Width * 2, 0, Me.Height * 4, 92))

        g.ResetTransform()

        Dim w2 As Single = Me.Width / 2
        Dim s As Single = Me.Width / 38
        Dim m As Single = Me.Height / 28
        Dim path = New Drawing2D.GraphicsPath()
        Dim ww As Single = Me.Width / 38
        Dim p1 As Pen = New Pen(Brushes.Lime, 1)



        path = New Drawing2D.GraphicsPath()
        path.AddPie(New Rectangle(ww * 3, ww * 3, Me.Width - ww * 6, Me.Height - ww * 6), 0, 360)
        g.SetClip(path) 'Do not go beyond this defined rounded area

        'Draw the pitchladder
        g.TranslateTransform(Me.Width / 2, Me.Height / 2)
        g.RotateTransform(roll_angle)
        g.TranslateTransform(0, pitch_to_pix(pitch_angle))
        For i As Integer = -80 To 80 Step 10
            drawpitchline(g, i)
        Next i
        g.ResetTransform()
        g.ResetClip()

        ' Small roll triangle on top
        path.Reset()
        g.TranslateTransform(Me.Width / 2, Me.Height / 2 + m / 2)
        g.RotateTransform(-90 + roll_angle)

        path.AddLine(w2 - ww * 3, 0, w2 - ww * 4, ww)
        path.AddLine(w2 - ww * 4, -ww, w2 - ww * 4, ww)
        path.AddLine(w2 - ww * 4, -ww, w2 - ww * 3, 0)
        g.FillRegion(Brushes.Green, New Region(path))
        g.DrawLine(p1, w2 - ww * 3, 0, w2 - ww * 4, ww)
        g.DrawLine(p1, w2 - ww * 4, -ww, w2 - ww * 4, ww)
        g.DrawLine(p1, w2 - ww * 4, -ww, w2 - ww * 3, 0)

        g.ResetTransform()
        g.ResetClip() 'End triangle

        '*********************
        'g.DrawImage(Clipboard.GetDataObject.GetData(DataFormats.Bitmap), 0, 0)
        'Draw the rollladder
        g.TranslateTransform(Me.Width / 2, Me.Height / 2 + m)
        For i As Integer = -45 To 45 Step 15
            drawrollline(g, i)
        Next
        g.ResetTransform()

        'Draw the yaw ladder on top of the display
        g.SetClip(borderpath) 'Make sure it does not run out of our display
        g.TranslateTransform(yaw_to_pix(-yaw_angle), 0)
        For i As Integer = -180 To 180 Step 10
            drawyawline(g, i)
        Next
        g.ResetClip()
        g.ResetTransform()

        'Draw the 'airplane'
        Dim length As Single = Me.Width / 4
        Dim notch As Single = Me.Width / 30
        Dim halfnotch As Single = Me.Width / 60
        g.TranslateTransform(Me.Width / 2, Me.Height / 2)

        'Airplane: Left pin, bottom half
        Dim aP As New Drawing2D.GraphicsPath
        aP.AddLine(-length, 0, -length + notch * 3, 0)
        aP.AddLine(-length + notch * 2, notch / 4, -length, notch / 4)
        aP.CloseFigure()
        g.FillPath(New SolidBrush(Color.Olive), aP)
        aP.Reset()

        'Airplane: Left pin, top half
        aP.AddLine(-length, 0, -length + notch * 3, 0)
        aP.AddLine(-length + notch * 2, -notch / 4, -length, -notch / 4)
        aP.CloseFigure()
        g.FillPath(New SolidBrush(Color.Yellow), aP)
        aP.Reset()

        'Airplane: Right pin, bottom half
        aP.AddLine(length, 0, length - notch * 3, 0)
        aP.AddLine(length - notch * 2, notch / 4, length, notch / 4)
        aP.CloseFigure()
        g.FillPath(New SolidBrush(Color.Olive), aP)
        aP.Reset()

        'Airplane: Right pin, top half
        aP.AddLine(length, 0, length - notch * 3, 0)
        aP.AddLine(length - notch * 2, -notch / 4, length, -notch / 4)
        aP.CloseFigure()
        g.FillPath(New SolidBrush(Color.Yellow), aP)
        aP.Reset()

        'Airplane: top half of plane
        aP.AddLine(-notch * 4, notch * 2, 0, 0)
        aP.AddLine(notch * 4, notch * 2, 0, notch)
        aP.CloseFigure()
        g.FillPath(New SolidBrush(Color.Yellow), aP)
        aP.Reset()

        'Airplane: bottom half of plane
        aP.AddLine(-notch * 4, notch * 2, 0, notch)
        aP.AddLine(notch * 4, notch * 2, 0, halfnotch * 3)
        aP.CloseFigure()
        g.FillPath(New SolidBrush(Color.Olive), aP)
        aP.Reset()
        g.ResetTransform() 'Done with airplane

        'Now draw the small compass in the lower right corner
        Dim w As Single = Me.Width / 32
        Dim v As Single = Me.Width / 64
        g.TranslateTransform(w * 28, w * 28)

        For I = 0 To 360 Step 30
            g.RotateTransform(30)
            g.DrawLine(Pen3, 0, -w * 2, 0, -w * 3)
        Next
        g.RotateTransform(-30)
        For I = 0 To 360 Step 90
            g.RotateTransform(90)
            g.DrawLine(New Pen(Brushes.Lime, 2), 0, -w * 2, 0, -w * 3)
        Next
        g.RotateTransform(-90)
        g.RotateTransform(yaw_angle)
        g.DrawLine(New Pen(Brushes.Lime, 4), -w, 0, w, 0) 'wings
        g.DrawLine(New Pen(Brushes.Lime, 4), 0, -v, 0, w) 'fuselage
        g.DrawLine(New Pen(Brushes.Lime, 4), -v, w, v, w) 'tail
        g.ResetTransform() 'End of small bottom right compass

        'Draw the altitude ladder on the left side of the display
        g.SetClip(borderpath) 'Make sure it does not run out of our display
        g.TranslateTransform(Me.Width / 2, Me.Height / 2)
        g.TranslateTransform(0, alt_to_pix(altitude))
        For i As Integer = -1000 To 1000 Step 10
            drawaltline(g, i)
        Next i
        g.ResetTransform()
        g.ResetClip()

        'Draw the box, middle left, with the altitude
        g.TranslateTransform(w, Me.Height / 2 + w / 2)
        g.FillRectangle(Brushes.White, w, -w, w * 3, w)
        g.DrawRectangle(New Pen(Brushes.Black, 1), w, -w, w * 3, w)
        aP.Reset()
        aP.AddLine(0, -w / 2, w, -w)
        aP.AddLine(w, 0, 0, -w / 2)
        g.FillPath(New SolidBrush(Color.Black), aP)

        Dim sF As SizeF
        sF = g.MeasureString(Math.Round(altitude, 0) & "m", NormalFont)
        g.DrawString(Math.Round(altitude, 0) & "m", NormalFont, Brushes.Black, w + (w * 3) / 2 - (sF.Width / 2), -w + (w) / 2 - (sF.Height / 2) - 1)
        sF = g.MeasureString("ALT", NormalFont)
        g.DrawString("ALT", NormalFont, Brushes.Black, w + (w * 4) / 2 - (sF.Width / 2), -w - (w) / 2 - (sF.Height / 2) - 1)
        g.ResetTransform()





        'And that's it ! Complete ADI :)
    End Sub

    Private Sub drawpitchline(ByVal g As Graphics, ByVal pitch As Double)
        Dim w As Single = Me.Width / 16
        Dim sF As SizeF
        g.DrawLine(Pen3, -w, pitch_to_pix(-pitch + 5), w, pitch_to_pix(-pitch + 5))
        g.DrawLine(Pen3, -w * 5 / 3, pitch_to_pix(-pitch), w * 5 / 3, pitch_to_pix(-pitch))
        sF = g.MeasureString(pitch, NormalFont)
        If pitch <> 0 Then
            g.DrawString(pitch, NormalFont, Brushes.Lime, -w * 1.7 - sF.Width, pitch_to_pix(-pitch) - sF.Height / 2)
            g.DrawString(pitch, NormalFont, Brushes.Lime, w * 1.7, pitch_to_pix(-pitch) - sF.Height / 2)
        End If
        
    End Sub
    Private Sub drawaltline(ByVal g As Graphics, ByVal alt As Double)
        Dim w As Single = Me.Width / 32
        Dim m As Single = Me.Width / 2
        Dim sF As SizeF
        g.DrawLine(Pen3, -m, alt_to_pix(-alt), -m + w, alt_to_pix(-alt))
        g.DrawLine(Pen3, -m, alt_to_pix(-alt + 5), -m + w / 2, alt_to_pix(-alt + 5))
        sF = g.MeasureString(Math.Round(alt, 0), NormalFont)
        g.DrawString(Math.Round(alt, 0), NormalFont, Brushes.Lime, -m + w + 2, alt_to_pix(-alt) - sF.Height / 2)
        'If alt <> 0 Then g.DrawString("" & alt, NormalFont, Brushes.Lime, Me.ClientRectangle.Left + h + 2, y - sF.Height / 2)

    End Sub

    Private Sub drawyawline(ByVal g As Graphics, ByVal yaw As Double)
        Dim h As Single = Me.Height / 32
        Dim x As Single = yaw_to_pix(yaw)
        Dim sF As SizeF

        g.DrawLine(New Pen(Brushes.Lime, 1), x, Me.ClientRectangle.Top, x, Me.ClientRectangle.Top + h)
        sF = g.MeasureString("" & yaw & Chr(176), NormalFont)
        If yaw <> 0 Then g.DrawString("" & yaw & Chr(176), NormalFont, Brushes.Lime, x - sF.Width / 2, Me.ClientRectangle.Top + h)

    End Sub

    Private Sub drawrollline(ByVal g As Graphics, ByVal a As Single)
        Dim w2 As Single = Me.Width / 2
        Dim sF As SizeF
        g.RotateTransform(a + 90)
        g.TranslateTransform(-w2 + 10, 0)
        g.DrawLine(Pens.Lime, 0, 0, 15, 0)
        'g.DrawEllipse(Pens.Red, New Rectangle(12, -3, 6, 6)) 'Used for visual reference
        g.TranslateTransform(10, 5)
        g.RotateTransform(-a - 90)
        sF = g.MeasureString("" & (a) & Chr(176), NormalFont)
        If a <> 0 Then g.DrawString("" & (a) & Chr(176), NormalFont, Brushes.Lime, -(sF.Width / 3), a / 12.5 + 4)
        g.RotateTransform(+90 + a)
        g.TranslateTransform(-10, -5)
        g.TranslateTransform(+w2 - 10, 0)
        g.RotateTransform(-a - 90)
    End Sub
End Class
