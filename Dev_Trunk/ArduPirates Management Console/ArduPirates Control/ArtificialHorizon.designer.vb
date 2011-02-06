<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ArtificialHorizon
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ImageIn = New System.Windows.Forms.PictureBox()
        CType(Me.ImageIn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageIn
        '
        Me.ImageIn.Image = Global.WindowsFormsApplication.My.Resources.Resources.logo
        Me.ImageIn.Location = New System.Drawing.Point(3, 3)
        Me.ImageIn.Name = "ImageIn"
        Me.ImageIn.Size = New System.Drawing.Size(334, 334)
        Me.ImageIn.TabIndex = 0
        Me.ImageIn.TabStop = False
        Me.ImageIn.Visible = False
        '
        'ArtificialHorizon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.Controls.Add(Me.ImageIn)
        Me.DoubleBuffered = True
        Me.Name = "ArtificialHorizon"
        Me.Size = New System.Drawing.Size(340, 340)
        CType(Me.ImageIn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ImageIn As System.Windows.Forms.PictureBox

End Class
