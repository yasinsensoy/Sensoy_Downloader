Imports System.Collections.Generic
Imports System.Text

Class HttpFtpProtocolParametersSettingsProxy
    Implements IHttpFtpProtocolParameters

    Public Property ProxyAddress() As String Implements IHttpFtpProtocolParameters.ProxyAddress
        Get
            Return Protocols.Default.ProxyAddress
        End Get
        Set(value As String)
            Protocols.Default.ProxyAddress = value
        End Set
    End Property

    Public Property ProxyUserName() As String Implements IHttpFtpProtocolParameters.ProxyUserName
        Get
            Return Protocols.Default.ProxyUserName
        End Get
        Set(value As String)
            Protocols.Default.ProxyUserName = value
        End Set
    End Property

    Public Property ProxyPassword() As String Implements IHttpFtpProtocolParameters.ProxyPassword
        Get
            Return Protocols.Default.ProxyPassword
        End Get
        Set(value As String)
            Protocols.Default.ProxyPassword = value
        End Set
    End Property

    Public Property ProxyDomain() As String Implements IHttpFtpProtocolParameters.ProxyDomain
        Get
            Return Protocols.Default.ProxyDomain
        End Get
        Set(value As String)
            Protocols.Default.ProxyDomain = value
        End Set
    End Property

    Public Property UseProxy() As Boolean Implements IHttpFtpProtocolParameters.UseProxy
        Get
            Return Protocols.Default.UseProxy
        End Get
        Set(value As Boolean)
            Protocols.Default.UseProxy = value
        End Set
    End Property

    Public Property ProxyByPassOnLocal() As Boolean Implements IHttpFtpProtocolParameters.ProxyByPassOnLocal
        Get
            Return Protocols.Default.ProxyByPassOnLocal
        End Get
        Set(value As Boolean)
            Protocols.Default.ProxyByPassOnLocal = value
        End Set
    End Property

    Public Property ProxyPort() As Integer Implements IHttpFtpProtocolParameters.ProxyPort
        Get
            Return Protocols.Default.ProxyPort
        End Get
        Set(value As Integer)
            Protocols.Default.ProxyPort = value
        End Set
    End Property
End Class
