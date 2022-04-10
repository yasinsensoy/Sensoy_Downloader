Imports System.Net
Imports System.Text.RegularExpressions

Public Class YardimciKomutlar
    Public Shared regx As New Regex("http(s)?://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?", RegexOptions.Compiled)

    Public Shared Function BoyutDuzenle(ByVal size As Int64, Optional ByVal decimals As Int32 = 3) As String
        Dim sizes As String() = {"Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"}
        Dim formattedSize As Double = size
        Dim sizeIndex As Int32 = 0
        While formattedSize >= 1024 And sizeIndex < sizes.Length
            formattedSize /= 1024
            sizeIndex += 1
        End While
        Return String.Format("{0} {1}", Math.Round(formattedSize, decimals).ToString("N3"), sizes(sizeIndex))
    End Function

    Public Shared Function KalanSureDuzenle(sure As TimeSpan) As String
        Dim format As String = ""
        If sure.Days > 0 Then format = sure.Days & " gün"
        If sure.Hours > 0 Then format += If(format = "", "", " ") & sure.Hours & " saat"
        If sure.Minutes > 0 Then format += If(format = "", "", " ") & sure.Minutes & " dak"
        format += If(format = "", "", " ") & sure.Seconds & " san"
        Return format
    End Function

    Public Shared Function KaynakKoduAl(url As String, Optional method As String = WebRequestMethods.Http.Get, Optional ByRef cookie As List(Of String) = Nothing, Optional ByRef cokie As CookieCollection = Nothing, Optional postdegeri As Byte() = Nothing, Optional ByRef header As WebHeaderCollection = Nothing) As Sonuc
        Try
            Dim request As HttpWebRequest = WebRequest.CreateHttp(url)
            request.CookieContainer = New CookieContainer
            If cokie IsNot Nothing Then
                request.CookieContainer.Add(cokie)
            End If
            request.Accept = "*/*"
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 OPR/57.0.3098.116"
            request.Referer = url
            request.Method = method
            request.KeepAlive = True
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
            If method = WebRequestMethods.Http.Post And postdegeri IsNot Nothing Then
                request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8"
                request.ContentLength = postdegeri.Length
                request.GetRequestStream().Write(postdegeri, 0, postdegeri.Length)
                request.GetRequestStream().Close()
            End If
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            If cokie IsNot Nothing Then cokie.Add(response.Cookies)
            If IsNothing(cookie) = False Then
                For Each cook As Cookie In response.Cookies
                    cookie.Add(String.Format("{0}[=]{1}[=]{2}[=]{3}", cook.Name, cook.Value, cook.Path, cook.Domain))
                Next
            End If
            Dim source As String = New System.IO.StreamReader(response.GetResponseStream()).ReadToEnd
            Dim responceurl As Uri = response.ResponseUri
            response.Close()
            Return New Sonuc(source, responceurl)
        Catch ex As WebException
            If IsNothing(ex.Response) = False Then Return New Sonuc(New System.IO.StreamReader(ex.Response.GetResponseStream()).ReadToEnd, ex.Response.ResponseUri) Else Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function IlerlemeDuzenle(ilerleme As Double) As String
        Return "% " & ilerleme.ToString("N2")
    End Function

    Public Shared Function ParcalariGetir(segmentCount As Integer, boyut As Long) As CalculatedSegment()
        Dim segmentSize As Long = boyut / segmentCount
        Dim minboyut As Long = 2 * 1024 * 1024
        Do While segmentSize < minboyut And segmentCount > 1
            segmentCount -= 1
            segmentSize = boyut / segmentCount
        Loop
        Dim startPosition As Long = 0
        Dim segments As New List(Of CalculatedSegment)()
        For i As Integer = 0 To segmentCount - 1
            If segmentCount - 1 = i Then
                segments.Add(New CalculatedSegment(startPosition, boyut))
            Else
                segments.Add(New CalculatedSegment(startPosition, startPosition + segmentSize))
            End If
            startPosition = segments(segments.Count - 1).EndPosition
        Next
        Return segments.ToArray()
    End Function
End Class

Public Class Sonuc
    Public Property kaynakkodu As String
    Public Property url As Uri

    Public Sub New(_kaynakkodu As String, _url As Uri)
        kaynakkodu = _kaynakkodu
        url = _url
    End Sub
End Class