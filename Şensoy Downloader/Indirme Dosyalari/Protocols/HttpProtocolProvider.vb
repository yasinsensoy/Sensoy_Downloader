Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Public Class HttpProtocolProvider
    Inherits BaseProtocolProvider
    Implements IProtocolProvider

    Shared Sub New()
        ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf certificateCallBack)
    End Sub

    Private Shared Function certificateCallBack(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Sub FillCredentials(ByRef request As HttpWebRequest, rl As IndirmeAdresi)
        If rl.GirisIzni Then
            Dim login As String = rl.KullaniciAdi
            Dim domain As String = String.Empty
            Dim slashIndex As Integer = login.IndexOf("\"c)
            If slashIndex >= 0 Then
                domain = login.Substring(0, slashIndex)
                login = login.Substring(slashIndex + 1)
            End If
            Dim myCred As New NetworkCredential(login, rl.Sifre)
            myCred.Domain = domain
            request.Credentials = myCred
        End If
    End Sub

    Public Overridable Sub Initialize(downloader As Indirme) Implements IProtocolProvider.Initialize
    End Sub

    Public Overridable Function CreateStream(ByRef rl As IndirmeAdresi, initialPosition As Long, endPosition As Long, Optional cookies As CookieCollection = Nothing, Optional ByRef header As WebHeaderCollection = Nothing) As BinaryReader Implements IProtocolProvider.CreateStream
        Dim request As HttpWebRequest = GetHttpRequest(rl, cookies, header)
        FillCredentials(request, rl)
        If initialPosition <> 0 Then
            If endPosition = 0 Then
                request.AddRange(initialPosition)
            Else
                request.AddRange(initialPosition, endPosition)
            End If
        End If
        Dim response As WebResponse = request.GetResponse()
        rl.Adres = response.ResponseUri.ToString
        Return New BinaryReader(response.GetResponseStream)
    End Function

    Public Overridable Function GetFileInfo(ByRef rl As IndirmeAdresi, ByRef stream As BinaryReader, Optional cookies As CookieCollection = Nothing, Optional ByRef header As WebHeaderCollection = Nothing) As SunucuDosyaBilgisi Implements IProtocolProvider.GetFileInfo
        Dim request As HttpWebRequest = GetHttpRequest(rl, cookies, header)
        FillCredentials(request, rl)
        Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
        Dim result As New SunucuDosyaBilgisi()
        result.MimeType = response.ContentType
        result.SonDuzenlemeTarihi = response.LastModified
        result.DosyaBoyutu = response.ContentLength
        If header IsNot Nothing And result.DosyaBoyutu <= 0 Then
            If header("boyut") IsNot Nothing Then result.DosyaBoyutu = header("boyut")
        End If
        result.DevamEdebilme = True '[String].Compare(response.Headers("Accept-Ranges"), "bytes", True) = 0
        rl.Adres = response.ResponseUri.ToString
        stream = New BinaryReader(response.GetResponseStream)
        Return result
    End Function

    Public Function GetFileSize(ByRef rl As IndirmeAdresi, Optional cookies As CookieCollection = Nothing, Optional ByRef header As WebHeaderCollection = Nothing) As Long Implements IProtocolProvider.GetFileSize
        Dim boyut As Long = 0
        Try
            Dim request As HttpWebRequest = GetHttpRequest(rl, cookies, header)
            FillCredentials(request, rl)
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            boyut = response.ContentLength
            If header IsNot Nothing And boyut <= 0 Then
                If header("boyut") IsNot Nothing Then boyut = header("boyut")
            End If
            rl.Adres = response.ResponseUri.ToString
            response.Close()
        Catch ex As Exception
        End Try
        Return boyut
    End Function
End Class
