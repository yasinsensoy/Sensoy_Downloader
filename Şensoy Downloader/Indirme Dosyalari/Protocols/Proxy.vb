Partial Public Class Proxy
    Inherits UserControl

    Public Sub New()
        Me.Text = "Proxy"
        InitializeComponent()
        UseProxy = Protocols.Default.UseProxy
        ProxyAddress = Protocols.Default.ProxyAddress
        ProxyPort = Protocols.Default.ProxyPort
        ProxyByPassOnLocal = Protocols.Default.ProxyByPassOnLocal
        ProxyUserName = Protocols.Default.ProxyUserName
        ProxyPassword = Protocols.Default.ProxyPassword
        ProxyDomain = Protocols.Default.ProxyDomain
        UpdateControls()
    End Sub

    Public Property UseProxy() As Boolean
        Get
            Return chkUseProxy.Checked
        End Get
        Set(value As Boolean)
            chkUseProxy.Checked = value
        End Set
    End Property

    Public Property ProxyAddress() As String
        Get
            Return txtProxtAddress.Text
        End Get
        Set(value As String)
            txtProxtAddress.Text = value
        End Set
    End Property

    Public Property ProxyPort() As Integer
        Get
            Return CInt(numProxyPort.Value)
        End Get
        Set(value As Integer)
            numProxyPort.Value = value
        End Set
    End Property

    Public Property ProxyByPassOnLocal() As Boolean
        Get
            Return chkBypass.Checked
        End Get
        Set(value As Boolean)
            chkBypass.Checked = value
        End Set
    End Property

    Public Property ProxyUserName() As String
        Get
            Return txtUsername.Text
        End Get
        Set(value As String)
            txtUsername.Text = value
        End Set
    End Property

    Public Property ProxyPassword() As String
        Get
            Return txtPassword.Text
        End Get
        Set(value As String)
            txtPassword.Text = value
        End Set
    End Property

    Public Property ProxyDomain() As String
        Get
            Return txtDomain.Text
        End Get
        Set(value As String)
            txtDomain.Text = value
        End Set
    End Property

    Private Sub chkUseProxy_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseProxy.CheckedChanged
        UpdateControls()
    End Sub

    Private Sub UpdateControls()
        For i As Integer = 0 To Me.Controls.Count - 1
            If Me.Controls(i) Is chkUseProxy Then
                Me.Controls(i).Enabled = chkUseProxy.Checked
            End If
        Next
    End Sub
End Class