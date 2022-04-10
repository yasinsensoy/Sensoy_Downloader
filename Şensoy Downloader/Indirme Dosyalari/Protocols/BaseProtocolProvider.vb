Imports System.Collections.Generic
Imports System.Text
Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Public Class BaseProtocolProvider
    Shared Sub New()
        ServicePointManager.DefaultConnectionLimit = Integer.MaxValue
    End Sub

    Protected Function GetHttpRequest(location As IndirmeAdresi, cookies As CookieCollection, header As WebHeaderCollection) As HttpWebRequest
        Dim request As HttpWebRequest = WebRequest.CreateHttp(location.Adres)
        request.Timeout = 30000
        request.CookieContainer = New CookieContainer
        SetProxyHttp(request)
        request.Method = WebRequestMethods.File.DownloadFile
        request.AllowAutoRedirect = True
        request.Referer = location.Adres
        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 OPR/57.0.3098.116"
        request.ProtocolVersion = HttpVersion.Version11
        If cookies IsNot Nothing Then request.CookieContainer.Add(cookies)
        If header IsNot Nothing Then
            For Each name As String In header
                Dim value As String = header.Get(name)
                If name.ToLower = "referer" Then
                    request.Referer = value
                Else
                    request.Headers.Add(name, value)
                End If
            Next
        End If
        Return request
    End Function

    Protected Function GetFtpRequest(location As IndirmeAdresi) As FtpWebRequest
        Dim request As FtpWebRequest = CType(WebRequest.Create(location.Adres), FtpWebRequest)
        request.Timeout = 30000
        SetProxyFtp(request)
        Return request
    End Function

    Protected Sub SetProxyHttp(request As HttpWebRequest)
        If HttpFtpProtocolExtension.parameters.UseProxy Then
            Dim proxy As New WebProxy(HttpFtpProtocolExtension.parameters.ProxyAddress, HttpFtpProtocolExtension.parameters.ProxyPort)
            proxy.BypassProxyOnLocal = HttpFtpProtocolExtension.parameters.ProxyByPassOnLocal
            request.Proxy = proxy
            If Not [String].IsNullOrEmpty(HttpFtpProtocolExtension.parameters.ProxyUserName) Then
                request.Proxy.Credentials = New NetworkCredential(HttpFtpProtocolExtension.parameters.ProxyUserName, HttpFtpProtocolExtension.parameters.ProxyPassword, HttpFtpProtocolExtension.parameters.ProxyDomain)
            End If
        Else
            request.Proxy.GetProxy(New Uri("http://www.google.com"))
        End If
    End Sub

    Protected Sub SetProxyFtp(request As FtpWebRequest)
        If HttpFtpProtocolExtension.parameters.UseProxy Then
            Dim proxy As New WebProxy(HttpFtpProtocolExtension.parameters.ProxyAddress, HttpFtpProtocolExtension.parameters.ProxyPort)
            proxy.BypassProxyOnLocal = HttpFtpProtocolExtension.parameters.ProxyByPassOnLocal
            request.Proxy = proxy
            If Not [String].IsNullOrEmpty(HttpFtpProtocolExtension.parameters.ProxyUserName) Then
                request.Proxy.Credentials = New NetworkCredential(HttpFtpProtocolExtension.parameters.ProxyUserName, HttpFtpProtocolExtension.parameters.ProxyPassword, HttpFtpProtocolExtension.parameters.ProxyDomain)
            End If
        Else
            request.Proxy.GetProxy(New Uri("http://www.google.com"))
        End If
    End Sub
End Class