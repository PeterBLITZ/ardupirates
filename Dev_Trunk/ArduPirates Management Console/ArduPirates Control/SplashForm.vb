Public Class SplashForm

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AuthorsLabel.Text = "Author(s):" & My.Application.Info.Trademark
        GroupLabel.Text = My.Application.Info.Copyright
        VersionLabel.Text = "v." & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & " build " & My.Application.Info.Version.Build


    End Sub

    Private Sub VersionLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VersionLabel.Click

    End Sub
End Class