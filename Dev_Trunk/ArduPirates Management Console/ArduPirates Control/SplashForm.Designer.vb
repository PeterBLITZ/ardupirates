<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashForm
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.VersionLabel = New System.Windows.Forms.Label()
        Me.GroupLabel = New System.Windows.Forms.Label()
        Me.AuthorsLabel = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoEllipsis = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(223, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(322, 71)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "ArduPirates Management Console"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.VersionLabel)
        Me.Panel1.Controls.Add(Me.GroupLabel)
        Me.Panel1.Controls.Add(Me.AuthorsLabel)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(557, 202)
        Me.Panel1.TabIndex = 2
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(163, 175)
        Me.ProgressBar1.MarqueeAnimationSpeed = 10
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(250, 24)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 6
        Me.ProgressBar1.Value = 50
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.WindowsFormsApplication.My.Resources.Resources.logonew
        Me.PictureBox1.Location = New System.Drawing.Point(22, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(195, 157)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'VersionLabel
        '
        Me.VersionLabel.ForeColor = System.Drawing.Color.White
        Me.VersionLabel.Location = New System.Drawing.Point(223, 142)
        Me.VersionLabel.Name = "VersionLabel"
        Me.VersionLabel.Size = New System.Drawing.Size(322, 15)
        Me.VersionLabel.TabIndex = 4
        Me.VersionLabel.Text = "(C)"
        Me.VersionLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupLabel
        '
        Me.GroupLabel.ForeColor = System.Drawing.Color.White
        Me.GroupLabel.Location = New System.Drawing.Point(223, 127)
        Me.GroupLabel.Name = "GroupLabel"
        Me.GroupLabel.Size = New System.Drawing.Size(322, 15)
        Me.GroupLabel.TabIndex = 3
        Me.GroupLabel.Text = "(C)2011 ArduPirates Development Group"
        Me.GroupLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AuthorsLabel
        '
        Me.AuthorsLabel.ForeColor = System.Drawing.Color.White
        Me.AuthorsLabel.Location = New System.Drawing.Point(223, 112)
        Me.AuthorsLabel.Name = "AuthorsLabel"
        Me.AuthorsLabel.Size = New System.Drawing.Size(322, 15)
        Me.AuthorsLabel.TabIndex = 2
        Me.AuthorsLabel.Text = "Author(s): Kim Dalmeijer"
        Me.AuthorsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SplashForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(557, 202)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SplashForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form2"
        Me.TransparencyKey = System.Drawing.Color.Blue
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents VersionLabel As System.Windows.Forms.Label
    Friend WithEvents GroupLabel As System.Windows.Forms.Label
    Friend WithEvents AuthorsLabel As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
End Class
