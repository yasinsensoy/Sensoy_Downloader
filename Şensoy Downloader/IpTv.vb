Public Class IpTv
    Public link As String

    Private Sub IpTv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim alan As Rectangle = Screen.GetWorkingArea(Me)
        Size = New Size(Convert.ToInt32(alan.Width * 0.6), Convert.ToInt32(alan.Height * 0.7))
        CenterToScreen()

    End Sub
End Class