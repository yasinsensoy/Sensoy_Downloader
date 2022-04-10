Imports System.Collections.Generic
Imports System.Text

Public Interface IHttpFtpProtocolParameters
    Property ProxyAddress() As String

    Property ProxyUserName() As String

    Property ProxyPassword() As String

    Property ProxyDomain() As String

    Property UseProxy() As Boolean

    Property ProxyByPassOnLocal() As Boolean

    Property ProxyPort() As Integer
End Interface
