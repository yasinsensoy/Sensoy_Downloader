Imports System.Collections.Generic
Imports System.Text
Imports System.Net
Imports System.IO

Public Class FtpProtocolProvider
    Inherits BaseProtocolProvider
    Implements IProtocolProvider

    Private Sub FillCredentials(ByRef request As FtpWebRequest, rl As IndirmeAdresi)
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

    Public Sub Initialize(downloader As Indirme) Implements IProtocolProvider.Initialize
    End Sub

    Public Function CreateStream(ByRef rl As IndirmeAdresi, initialPosition As Long, endPosition As Long, Optional cookies As CookieCollection = Nothing, Optional ByRef header As WebHeaderCollection = Nothing) As BinaryReader Implements IProtocolProvider.CreateStream
        Dim request As FtpWebRequest = GetFtpRequest(rl)
        FillCredentials(request, rl)
        request.Method = WebRequestMethods.Ftp.DownloadFile
        request.ContentOffset = initialPosition
        Dim response As WebResponse = request.GetResponse()
        Return New BinaryReader(response.GetResponseStream)
    End Function

    Public Function GetFileInfo(ByRef rl As IndirmeAdresi, ByRef stream As BinaryReader, Optional cookies As CookieCollection = Nothing, Optional ByRef header As WebHeaderCollection = Nothing) As SunucuDosyaBilgisi Implements IProtocolProvider.GetFileInfo
        Dim request As FtpWebRequest
        Dim result As New SunucuDosyaBilgisi()
        result.DevamEdebilme = True
        request = GetFtpRequest(rl)
        request.Method = WebRequestMethods.Ftp.GetFileSize
        FillCredentials(request, rl)
        Using response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            result.DosyaBoyutu = response.ContentLength
        End Using
        request = GetFtpRequest(rl)
        request.Method = WebRequestMethods.Ftp.GetDateTimestamp
        FillCredentials(request, rl)
        Using response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            result.SonDuzenlemeTarihi = response.LastModified
        End Using
        Return result
    End Function

    Public Function GetFileSize(ByRef rl As IndirmeAdresi, Optional cookies As CookieCollection = Nothing, Optional ByRef header As WebHeaderCollection = Nothing) As Long Implements IProtocolProvider.GetFileSize
        Dim boyut As Long = 0
        Dim request As FtpWebRequest = GetFtpRequest(rl)
        FillCredentials(request, rl)
        Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
        boyut = response.ContentLength
        response.Close()
        Return boyut
    End Function
End Class
