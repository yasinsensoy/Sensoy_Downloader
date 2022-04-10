Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json.Linq
Imports System.Text
Imports System.Runtime.InteropServices

Public Class Form1
    Public extensions As New List(Of IExtension)
    Private genelcookie As New List(Of String)
    Private genelheader As New List(Of String)
    Private basladi As Boolean = False
    Private kapat As Boolean = False
    Private Const WM_SETTEXT = &HC

    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(hWnd As System.IntPtr) As Integer
    End Function

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        For i As Integer = 0 To extensions.Count - 1
            If TypeOf extensions(i) Is IDisposable Then
                Try
                    DirectCast(extensions(i), IDisposable).Dispose()
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString())
                End Try
            End If
        Next
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_SETTEXT AndAlso m.WParam.ToInt32() = 1 Then
            Dim msj() As String = Split(Marshal.PtrToStringAuto(m.LParam), "[ayrac]")
            If msj.Length > 0 Then
                Dim mirrors As New List(Of IndirmeAdresi)
                mirrors.Add(IndirmeAdresi.FromURL(msj(0)))
                For Each mirrorRl As IndirmeAdresi In mirrors
                    mirrorRl.BindProtocolProviderType()
                    If mirrorRl.ProtocolProviderType Is Nothing Then
                        MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                        Return
                    End If
                Next
                Dim header() As String
                If msj(3) <> 0 Then
                    Dim h As WebHeaderCollection = New WebHeaderCollection
                    h.Add("boyut", msj(3))
                    header = h.ToString().Trim.Replace(vbLf, "").Split(vbCrLf)
                Else
                    header = genelheader.ToArray()
                End If
                Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(mirrors(0), mirrors.ToArray, Path.Combine(DownloadFolder1.Folder, msj(2), Path.GetFileName(msj(0))), Convert.ToInt32(msj(1)), False, False, msj(4), genelcookie.ToArray, header)
            End If
            SetForegroundWindow(Handle)
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ServicePointManager.DefaultConnectionLimit = Integer.MaxValue
        ServicePointManager.CheckCertificateRevocationList = True
        ServicePointManager.MaxServicePoints = Integer.MaxValue
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        extensions.Add(New HttpFtpProtocolExtension())
        extensions.Add(New PersistedListExtension())
        NumericUpDown2.Value = IndirmeAyar.Default.EnFazlaAyniAndaIndirilecekSegmentSayisi
        TextBox24.Text = IndirmeAyar.Default.kadi
        TextBox25.Text = IndirmeAyar.Default.sifre
        TextBox29.Text = IndirmeAyar.Default.sunucu
        basladi = True
        Dim alan As Rectangle = Screen.GetWorkingArea(Me)
        Size = New Size(Convert.ToInt32(alan.Width * 0.8), Convert.ToInt32(alan.Height * 0.8))
        CenterToScreen()
        FlowLayoutPanel1.AutoSize = True
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim header As New WebHeaderCollection
            Dim mirrors As New List(Of IndirmeAdresi)
            Dim headers As String() = TextBox23.Text.Split({vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
            For Each oge As String In headers
                Dim head As String() = Split(oge, ":", 2)
                header.Add(head(0), head(1))
            Next
            Dim son As String = If(New Uri(TextBox9.Text).Query = "", "?start=", "&start=")
            mirrors.Add(IndirmeAdresi.FromURL(TextBox9.Text & If(MaskedTextBox2.MaskCompleted = False, "", son & TimeSpan.Parse(MaskedTextBox2.Text).TotalSeconds)))
            For Each mirrorRl As IndirmeAdresi In mirrors
                mirrorRl.BindProtocolProviderType()
                If mirrorRl.ProtocolProviderType Is Nothing Then
                    MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return
                End If
            Next
            Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(mirrors(0), mirrors.ToArray, Path.Combine(DownloadFolder1.Folder, TextBox1.Text), NumericUpDown1.Value, CheckBox1.Checked, False, "", genelcookie.ToArray, header.ToString().Trim.Replace(vbLf, "").Split({vbCrLf}, StringSplitOptions.RemoveEmptyEntries))
        Catch generatedExceptionName As Exception
            MessageBox.Show("Unknow error, please check your input data.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        TextBox9.Text = TextBox9.Text.Replace("\", "")
        Try
            Try
                TextBox1.Text = Path.GetFileName(New Uri(TextBox9.Text).LocalPath)
            Catch ex As Exception
            End Try
        Catch ex As Exception

        End Try
    End Sub

#Region "ATV İndir"
    Private Sub Tara_Click(sender As Object, e As EventArgs) Handles Tara.Click
        Try
            Dim header As New WebHeaderCollection
            header.Add("X-isApp", "false")
            Dim kaynakkodu As String = WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(Url.Text, "GET", Nothing, Nothing, Nothing, header).kaynakkodu)
            Dim wid As String = TagYakala(kaynakkodu, "data-websiteid=""", """")
            Dim vid As String = TagYakala(kaynakkodu, "data-videoid=""", """")
            If vid = "" And wid = "" Then MsgBox("Video bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
            Dim video As String = Düzenle(YardimciKomutlar.KaynakKoduAl($"https://videojs.tmgrup.com.tr/getvideo/{wid}/{vid}", "GET", Nothing, Nothing, Nothing, header).kaynakkodu)
            Dim url1 As String = TagYakala(video, "VideoSmilUrl"":""", """")
            Dim url2 As String = TagYakala(video, "VideoUrl"":""", """")
            Dim secure As String = Düzenle(YardimciKomutlar.KaynakKoduAl($"https://securevideotoken.tmgrup.com.tr/webtv/secure?url={url1}&url2={url2}").kaynakkodu)
            Dim ad As String = TagYakala(video, "Title"":""", """") & If(TagYakala(video, "Episode"":", ",") = "0", "", " " & TagYakala(video, "Episode"":", ",") & ".Bölüm")
            TextBox6.Text = ad
            Dim playlist As String = TagYakala(secure, "Url"":""", """")
            Dim videolar As String = YardimciKomutlar.KaynakKoduAl(playlist).kaynakkodu.Replace("chunklist_", Split(playlist, "playlist.m3u8")(0) & "chunklist_")
            Dim değerler As New List(Of Değer)()
            değerler.Clear()
            Dim say As Integer = 1
            For Each match As Match In YardimciKomutlar.regx.Matches(videolar)
                Dim kalite As Integer = TagYakala(videolar, "RESOLUTION=", vbLf, say).Split("x")(1)
                değerler.Add(New Değer(kalite.ToString & "p (.ts)", match.Value, kalite))
                say += 1
            Next
            Kalite.DataSource = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub İndirİzle_Click(sender As Object, e As EventArgs) Handles İndirİzle.Click
        Try
            Dim kayıtyeri As String = DownloadFolder1.Folder & "\" & TextBox6.Text & ".ts"
            Dim mirrors As New List(Of IndirmeAdresi)()
            For Each match As Match In YardimciKomutlar.regx.Matches(YardimciKomutlar.KaynakKoduAl(Kalite.SelectedValue).kaynakkodu.Replace("media_", Split(Kalite.SelectedValue, "chunklist_")(0) & "media_"))
                mirrors.Add(IndirmeAdresi.FromURL(match.Value))
            Next
            For Each mirrorRl As IndirmeAdresi In mirrors
                mirrorRl.BindProtocolProviderType()
                If mirrorRl.ProtocolProviderType Is Nothing Then
                    MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return
                End If
            Next
            Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(Url.Text), mirrors.ToArray, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", genelcookie.ToArray, genelheader.ToArray)
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub
#End Region

#Region "Kanal D İndir"
    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Try
            Me.Enabled = False
            Dim kaynakkodu As String = WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(New Uri(TextBox12.Text).ToString).kaynakkodu)
            Dim id As String = TagYakala(kaynakkodu, "data-id=""", """")
            If id = "" Then id = TagYakala(kaynakkodu, "data-id=", " ")
            If id = "" Then id = TagYakala(kaynakkodu, "itemId='", "'")
            If id = "" Then id = TagYakala(kaynakkodu, """contentid"",""", """")
            If id = "" Then MsgBox("Video bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
            Dim link As String = Düzenle(YardimciKomutlar.KaynakKoduAl("https://www.kanald.com.tr/actions/media?id=" & id).kaynakkodu)
            TextBox11.Text = TagYakala(kaynakkodu, "data-title=""", """").Replace("\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace(Chr(34), "").Replace("<", "").Replace(">", "").Replace("|", "")
            If TextBox11.Text = "" Then TextBox11.Text = TagYakala(link, "title"":""", """", 2).Replace("\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace(Chr(34), "").Replace("<", "").Replace(">", "").Replace("|", "")
            Dim sen As String = TagYakala(link, """securePath"":""", """")
            Dim path As String = If(Microsoft.VisualBasic.Left(sen, 4) = "http", sen, TagYakala(link, "defaultServiceUrl"":""", """") & "/" & sen)
            Dim urlbaşlangıç As String = Split(path, "index.m3u8")(0)
            Dim kaliteler As New List(Of String())
            kaliteler.Add({urlbaşlangıç & "128/prog_index.m3u8", "350", " Kbps (288p) (.ts)"})
            kaliteler.Add({urlbaşlangıç & "500/prog_index.m3u8", "750", " Kbps (288p) (.ts)"})
            kaliteler.Add({urlbaşlangıç & "750/prog_index.m3u8", "1000", " Kbps (480p) (.ts)"})
            kaliteler.Add({urlbaşlangıç & "1000/prog_index.m3u8", "1250", " Kbps (480p) (.ts)"})
            kaliteler.Add({urlbaşlangıç & "1500/prog_index.m3u8", "1750", " Kbps (720p) (.ts)"})
            kaliteler.Add({urlbaşlangıç & "2500/prog_index.m3u8", "2750", " Kbps (720p) (.ts)"})
            Dim değerler As New List(Of Değer)()
            değerler.Clear()
            For Each m_kalite() As String In kaliteler
                Dim value As String = YardimciKomutlar.KaynakKoduAl(m_kalite(0)).kaynakkodu.Replace("prog_index", Split(m_kalite(0), ".m3u8")(0)).Replace("?", "")
                If value.Contains(".ts") = True Then değerler.Add(New Değer(m_kalite(1) & m_kalite(2), value, CInt(m_kalite(1))))
            Next
            değerler = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
            ComboBox5.DataSource = değerler
            Me.Enabled = True
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
            Me.Enabled = True
        End Try
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Try
            Dim kayıtyeri As String = DownloadFolder1.Folder & "\" & TextBox11.Text & ".ts"
            Dim mirrors As New List(Of IndirmeAdresi)()
            For Each match As Match In YardimciKomutlar.regx.Matches(ComboBox5.SelectedValue.ToString)
                mirrors.Add(IndirmeAdresi.FromURL(match.Value))
            Next
            For Each mirrorRl As IndirmeAdresi In mirrors
                mirrorRl.BindProtocolProviderType()
                If mirrorRl.ProtocolProviderType Is Nothing Then
                    MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return
                End If
            Next
            Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(TextBox12.Text), mirrors.ToArray, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", genelcookie.ToArray, genelheader.ToArray)
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub
#End Region

#Region "Star TV İndir"
    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Try
            Dim kaynakkodu As String = WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(New Uri(TextBox14.Text).ToString).kaynakkodu)
            Dim video As String = Düzenle(WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(TagYakala(kaynakkodu, "videoUrl = '", "'")).kaynakkodu)).Replace("\", "")
            Dim adı As String = TagYakala(video, "title"":""", """").Replace("\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace(Chr(34), "").Replace("<", "").Replace(">", "").Replace("|", "")
            Dim playlist As String = TagYakala(video, "hls"":""", """")
            Dim filename As String = Path.GetFileName(TagYakala(video, "filename"":""", """"))
            Dim ilk As String = Split(playlist, Path.GetFileName(playlist))(0)
            Dim link As String = WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(playlist).kaynakkodu).Replace(filename, ilk & filename).Replace(filename.Replace("_", "__"), ilk & filename.Replace("_", "__"))
            TextBox13.Text = adı
            Dim değerler As New List(Of Değer)()
            değerler.Clear()
            Dim index As Integer = 0
            For Each match As Match In YardimciKomutlar.regx.Matches(link)
                index += 1
                Dim bandwidth As Integer = CInt(TagYakala(link, "BANDWIDTH=", vbLf, index)) \ 1000
                Dim name As String = bandwidth.ToString & " Kbps"
                Dim value As String = WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(match.Value).kaynakkodu).Replace(filename, ilk & filename).Replace(filename.Replace("_", "__"), ilk & filename.Replace("_", "__"))
                değerler.Add(New Değer(name, value, bandwidth))
            Next
            ComboBox6.DataSource = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Dim kayıtyeri As String = DownloadFolder1.Folder & "\" & TextBox13.Text & ".ts"
        Dim mirrors As New List(Of IndirmeAdresi)()
        For Each match As Match In YardimciKomutlar.regx.Matches(ComboBox6.SelectedValue)
            mirrors.Add(IndirmeAdresi.FromURL(match.Value))
        Next
        For Each mirrorRl As IndirmeAdresi In mirrors
            mirrorRl.BindProtocolProviderType()
            If mirrorRl.ProtocolProviderType Is Nothing Then
                MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Return
            End If
        Next
        Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(TextBox14.Text), mirrors.ToArray, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", genelcookie.ToArray, genelheader.ToArray)
    End Sub
#End Region

#Region "SHOW TV İndir"

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Try
            Dim kaynakkodu As String = WebUtility.UrlDecode(WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(TextBox22.Text).kaynakkodu))
            Dim video As String = TagYakala(kaynakkodu, "ht_files"":[{", "}]}]")
            If video = "" Then MsgBox("Video bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
            Dim parcalar As String() = Split(TagYakala(video, "{""mp4"":[", "]}").Replace("\", ""), "},")
            TextBox16.Text = TagYakala(kaynakkodu, "text=", "&").Replace("\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace(Chr(34), "").Replace("<", "").Replace(">", "").Replace("|", "")
            Dim değerler As New List(Of Değer)()
            değerler.Clear()
            For Each parca As String In parcalar
                Dim kalite As Integer = TagYakala(parca, "name"":""", """")
                Dim link As String = TagYakala(parca, "file"":""", """")
                Dim boyut As Long = ProtocolProviderFactory.GetProvider(link).GetFileSize(IndirmeAdresi.FromURL(link))
                If boyut <= 0 Then Continue For
                değerler.Add(New Değer(kalite & "p (" & YardimciKomutlar.BoyutDuzenle(boyut) & ")", link, kalite))
            Next
            ComboBox4.DataSource = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Try
            Dim mirrors() As IndirmeAdresi = {IndirmeAdresi.FromURL(ComboBox4.SelectedValue)}
            Dim kayıtyeri As String = DownloadFolder1.Folder & "\" & TextBox16.Text & ".mp4"
            For Each mirrorRl As IndirmeAdresi In mirrors
                mirrorRl.BindProtocolProviderType()
                If mirrorRl.ProtocolProviderType Is Nothing Then
                    MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return
                End If
            Next
            Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(TextBox22.Text), mirrors.ToArray, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", genelcookie.ToArray, genelheader.ToArray)
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub
#End Region

#Region "puhutv İndir"
    Private puhutvcookieheader As List(Of String)
    Private puhutvcookie As CookieCollection

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Try
            puhutvcookieheader = New List(Of String)
            puhutvcookie = New CookieCollection
            Dim kaynak As String = YardimciKomutlar.KaynakKoduAl(TextBox8.Text, WebRequestMethods.Http.Get, puhutvcookieheader, puhutvcookie).kaynakkodu
            Dim id As String = Split(TagYakala(kaynak, "player.video.loader(", ")"), ", ")(1).Replace("'", "")
            Dim videoinfo As String = Düzenle(YardimciKomutlar.KaynakKoduAl("https://dygvideo.dygdigital.com/api/video_info?akamai=true&PublisherId=29&ReferenceId=" & id & "&SecretKey=NtvApiSecret2014*", WebRequestMethods.Http.Get, puhutvcookieheader, puhutvcookie).kaynakkodu).Replace("\", "")
            TextBox7.Text = TagYakala(videoinfo, "title"":""", """").Replace("\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace(Chr(34), "").Replace("<", "").Replace(">", "").Replace("|", "")
            Dim hls As String = TagYakala(videoinfo, "hls"":""", """")
            Dim videolar As String = YardimciKomutlar.KaynakKoduAl(hls, WebRequestMethods.Http.Get, puhutvcookieheader, puhutvcookie).kaynakkodu
            Dim değerler As New List(Of Değer)()
            değerler.Clear()
            Dim kaliteler() As String = Split(videolar, vbLf)
            For i As Integer = 0 To kaliteler.Length - 1
                If kaliteler(i).Contains(".m3u8") = False Then Continue For
                Dim kalite As Integer = TagYakala(kaliteler(i - 1), "RESOLUTION=", ",").Split("x")(1)
                Dim url As String = Split(hls, "playlist.m3u8")(0) & kaliteler(i)
                değerler.Add(New Değer(kalite & "p (.ts)", url, kalite))
            Next
            ComboBox3.DataSource = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Try
            Dim kayıtyeri As String = DownloadFolder1.Folder & "\" & TextBox7.Text & ".ts"
            Dim kaynak As String = YardimciKomutlar.KaynakKoduAl(ComboBox3.SelectedValue, WebRequestMethods.Http.Get, puhutvcookieheader, puhutvcookie).kaynakkodu
            Dim mirrors As New List(Of IndirmeAdresi)()
            For Each deger As String In Split(kaynak, vbLf)
                If deger.Contains(".ts") = False Then Continue For
                If deger.Contains("rtuk") = True Then Continue For
                mirrors.Add(IndirmeAdresi.FromURL(Split(ComboBox3.SelectedValue, Path.GetFileName(New Uri(ComboBox3.SelectedValue).LocalPath))(0) & deger))
            Next
            For Each mirrorRl As IndirmeAdresi In mirrors
                mirrorRl.BindProtocolProviderType()
                If mirrorRl.ProtocolProviderType Is Nothing Then
                    MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return
                End If
            Next
            Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(TextBox8.Text), mirrors.ToArray, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", puhutvcookieheader.ToArray, genelheader.ToArray())
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub
#End Region

#Region "mail.ru İndir"
    Private mailrucookieheader As List(Of String)

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            mailrucookieheader = New List(Of String)
            Dim kaynakkodu As String = WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(New Uri(TextBox5.Text).ToString, WebRequestMethods.Http.Get, mailrucookieheader).kaynakkodu)
            Dim videokaynak As String = WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(New Uri(TagYakala(kaynakkodu, "metaUrl"": """, """")).ToString, WebRequestMethods.Http.Get, mailrucookieheader).kaynakkodu)
            Dim adı As String = TagYakala(videokaynak, "title"":""", """").Replace("\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace(Chr(34), "").Replace("<", "").Replace(">", "").Replace("|", "")
            Dim linkler As String = TagYakala(videokaynak, "videos"":[{", "}]")
            If linkler = "" Then MsgBox("Video bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
            TextBox4.Text = adı
            Dim değerler As New List(Of Değer)()
            değerler.Clear()
            Dim index As Integer = 0
            For Each link As String In Split(linkler, "},{")
                Dim name As String = TagYakala(link, "key"":""", """")
                Dim value As String = TagYakala(link, "url"":""", """").Replace("//", "http://")
                Dim sıralama As Integer = Integer.Parse(Regex.Replace(name, "[^\d]", ""))
                değerler.Add(New Değer(name, value, sıralama))
            Next
            ComboBox2.DataSource = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim kayıtyeri As String = Path.Combine(DownloadFolder1.Folder, TextBox4.Text & ".mp4")
        Dim mirrors() As IndirmeAdresi = {IndirmeAdresi.FromURL(ComboBox2.SelectedValue & If(MaskedTextBox1.MaskCompleted = False, "", "&start=" & TimeSpan.Parse(MaskedTextBox1.Text).TotalSeconds), False, "", "")}
        For Each mirrorRl As IndirmeAdresi In mirrors
            mirrorRl.BindProtocolProviderType()
            If mirrorRl.ProtocolProviderType Is Nothing Then
                MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Return
            End If
        Next
        Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(TextBox5.Text), mirrors, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", mailrucookieheader.ToArray, genelheader.ToArray)
    End Sub
#End Region

#Region "FOX İndir"
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Try
            Dim kaynak As String = YardimciKomutlar.KaynakKoduAl(New Uri(TextBox15.Text).ToString).kaynakkodu
            Dim url As String = TagYakala(kaynak, "videoSrc : '", "'")
            If url = "" Then MsgBox("Video bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
            Dim videobilgi As String = YardimciKomutlar.KaynakKoduAl(New Uri(url).ToString).kaynakkodu.Replace("hlssubplaylist-", Split(url, Path.GetFileName(New Uri(url).LocalPath))(0) & "hlssubplaylist-")
            If videobilgi = "" Then MsgBox("Video bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
            Dim adı As String = TagYakala(kaynak, "<h2>", "</h2>", 2).Trim.Replace(vbLf, "").Replace(vbTab, "").Replace("/", ".").Replace("\", ".").Replace(":", "").Replace("*", "").Replace("?", "").Replace(Chr(34), "").Replace("<", "").Replace(">", "").Replace("|", "")
            TextBox10.Text = adı
            Dim değerler As New List(Of Değer)()
            değerler.Clear()
            Dim index As Integer = 0
            For Each match1 As Match In YardimciKomutlar.regx.Matches(videobilgi)
                index += 1
                Dim kalite As Integer = CInt(TagYakala(videobilgi, "RESOLUTION=", vbLf, index).Split("x")(1))
                Dim name As String = kalite.ToString & "p (.ts)"
                Dim value As String = match1.Value
                değerler.Add(New Değer(name, value, kalite))
            Next
            ComboBox7.DataSource = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim kaynakkodu As String = YardimciKomutlar.KaynakKoduAl(New Uri(ComboBox7.SelectedValue).ToString).kaynakkodu.Replace("hlsts-video", Split(ComboBox7.SelectedValue, Path.GetFileName(New Uri(ComboBox7.SelectedValue).LocalPath))(0) & "hlsts-video")
        Dim kayıtyeri As String = DownloadFolder1.Folder & "\" & TextBox10.Text & ".ts"
        Dim mirrors As New List(Of IndirmeAdresi)()
        For Each match As Match In YardimciKomutlar.regx.Matches(kaynakkodu)
            mirrors.Add(IndirmeAdresi.FromURL(match.Value))
        Next
        For Each mirrorRl As IndirmeAdresi In mirrors
            mirrorRl.BindProtocolProviderType()
            If mirrorRl.ProtocolProviderType Is Nothing Then
                MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Return
            End If
        Next
        Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(ComboBox7.SelectedValue), mirrors.ToArray, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", genelcookie.ToArray, genelheader.ToArray)
    End Sub
#End Region

#Region "dizilab İndir"
    Private dizilabcookieheader As CookieCollection
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            dizilabcookieheader = New CookieCollection
            Dim sayfa As String = WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(New Uri(TextBox19.Text).ToString, WebRequestMethods.Http.Get, Nothing, dizilabcookieheader).kaynakkodu).Replace(vbTab, "").Replace(vbCrLf, "").Replace(vbLf, "")
            Dim document As String = TagYakala(sayfa, "<ul class=""sezonlar"">", "</ul>")
            TextBox17.Text = TagYakala(sayfa, "<h1><strong>", "</strong></h1>").Trim()
            Dim değerler As New List(Of Değer)()
            değerler.Clear()
            For Each öğe As String In Split(document, "</li>")
                If öğe.Contains("Sezon") = False Then Continue For
                Dim sezonno As Integer = CInt(TagYakala(öğe, """>", "."))
                Dim bolumcek As String = sayfa
                If öğe.Contains("active") = False Then bolumcek = WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(TagYakala(öğe, "href=""", """"), WebRequestMethods.Http.Get, Nothing, dizilabcookieheader).kaynakkodu)
                Dim bölümler As String = TagYakala(bolumcek, "<ul class=""bolumler"">", "</ul>")
                If bölümler = "" Then Continue For
                değerler.Add(New Değer(sezonno.ToString & ".Sezon", bölümler, sezonno))
            Next
            ComboBox8.DataSource = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            Dim video As String = ComboBox11.SelectedValue
            If video = "" Then MsgBox("Video bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
            Dim diziklasörü As String = Path.Combine(DownloadFolder1.Folder, TextBox17.Text)
            Dim sezonklasörü As String = Path.Combine(diziklasörü, ComboBox8.Text)
            If Directory.Exists(sezonklasörü) = False Then Directory.CreateDirectory(sezonklasörü)
            Dim videokayıtyeri As String = Path.Combine(sezonklasörü, TextBox18.Text & ".mp4")
            Dim mirrors() As IndirmeAdresi = {IndirmeAdresi.FromURL(video)}
            For Each mirrorRl As IndirmeAdresi In mirrors
                mirrorRl.BindProtocolProviderType()
                If mirrorRl.ProtocolProviderType Is Nothing Then
                    MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return
                End If
            Next
            Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(ComboBox9.SelectedValue), mirrors, videokayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, TextBox17.Text & " - " & ComboBox8.Text, genelcookie.ToArray, genelheader.ToArray)
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If ComboBox8.Items.Count = 0 Then MsgBox("Sezon bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
        For i As Integer = ComboBox8.Items.Count - 1 To 0 Step -1
            ComboBox8.SelectedIndex = i
            For a As Integer = ComboBox9.Items.Count - 1 To 0 Step -1
                ComboBox9.SelectedIndex = a
                Button10.PerformClick()
            Next
        Next
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If ComboBox9.Items.Count = 0 Then MsgBox("Bölüm bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
        For a As Integer = ComboBox9.Items.Count - 1 To 0 Step -1
            ComboBox9.SelectedIndex = a
            Button10.PerformClick()
        Next
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        Dim bölümler As String = ComboBox8.SelectedValue.ToString
        Dim değerler As New List(Of Değer)()
        değerler.Clear()
        For Each öğe As String In Split(bölümler, "</li>")
            If öğe.Contains("Bölüm") = False Then Continue For
            Dim bolum As String = TagYakala(öğe, "class=""sari""", "</span>")
            Dim bölümno As Integer = CInt(TagYakala(bolum, """>", "."))
            Dim bölümlinki As String = YardimciKomutlar.regx.Match(bolum).Value
            değerler.Add(New Değer(bölümno & ".Bölüm", bölümlinki, bölümno))
        Next
        ComboBox9.DataSource = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
    End Sub

    Private Sub ComboBox9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged
        TextBox18.Text = ComboBox9.Text
        Dim sayfa As String = WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(ComboBox9.SelectedValue, WebRequestMethods.Http.Get, Nothing, dizilabcookieheader).kaynakkodu)
        Dim yol As String = TagYakala(TagYakala(sayfa, "<div class=""video-iframe"">", "</div>"), "src=""", """")
        yol = If(yol.Substring(0, 4).ToLower = "http", "", "http://dizihitplayer.xyz") & yol
        Dim değerler As New List(Of Değer)()
        değerler.Clear()
        sayfa = WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(yol, WebRequestMethods.Http.Get, Nothing, dizilabcookieheader).kaynakkodu).Replace(vbTab, "").Replace(vbCrLf, "").Replace(vbLf, "").Replace(" ", "")
        Dim kaynaklar As String = TagYakala(sayfa, "sources:[{", "}]")
        For Each öğe As String In Split(kaynaklar, ",{")
            Dim kalite As Integer = CInt(TagYakala(öğe, "label:""", "p"))
            Dim video As String = TagYakala(sayfa, "link:'", "/player") & TagYakala(öğe, "file:""", """")
            Dim boyut As Integer = ProtocolProviderFactory.GetProvider(video).GetFileSize(IndirmeAdresi.FromURL(video, False, "", ""))
            If boyut <= 0 Then Continue For
            değerler.Add(New Değer(kalite & "p (" & YardimciKomutlar.BoyutDuzenle(boyut) & ")", video, boyut))
        Next
        değerler = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
        If değerler.Count <= 0 Then değerler.Add(New Değer("Kaynak Bulunamadı", "", 1))
        ComboBox11.DataSource = değerler
    End Sub
#End Region

#Region "ok.ru İndir"
    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Try
            Dim okrucookieheader As CookieCollection = New CookieCollection
            Dim url As New Uri(TextBox21.Text)
            Dim videoid As String = url.Segments(2)
            Dim videokaynak As String = Düzenle(WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(New Uri("http://ok.ru/dk?cmd=videoPlayerMetadata&mid=" & videoid).ToString, WebRequestMethods.Http.Post, Nothing, okrucookieheader).kaynakkodu)).Replace("\""", """").Replace("\n", "").Replace("&amp;", "&")
            Dim adı As String = TagYakala(videokaynak, """title"":""", """").Replace("\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace(Chr(34), "").Replace("<", "").Replace(">", "").Replace("|", "")
            Dim linkler As String = TagYakala(videokaynak, "videos"":[{", "}]")
            If linkler = "" Then MsgBox("Video bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
            TextBox20.Text = adı
            Dim değerler As New List(Of Değer)()
            değerler.Clear()
            Dim vdsig As String = "&vdsig=" & TagYakala(YardimciKomutlar.KaynakKoduAl("https://vd16.mycdn.me/usr_login", WebRequestMethods.Http.Post, Nothing, okrucookieheader).kaynakkodu, "vtkn"":""", """")
            For Each link As String In Split(linkler, "},{")
                Dim video As String = TagYakala(link, "url"":""", """") & vdsig
                Dim boyut As Long = ProtocolProviderFactory.GetProvider(video).GetFileSize(IndirmeAdresi.FromURL(video, False, "", ""))
                If boyut <= 0 Then Continue For
                Dim name As String = TagYakala(link, "name"":""", """") & " (" & YardimciKomutlar.BoyutDuzenle(boyut) & ")"
                değerler.Add(New Değer(name, video, boyut))
            Next
            ComboBox10.DataSource = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        Dim kayıtyeri As String = Path.Combine(DownloadFolder1.Folder, TextBox20.Text & ".mp4")
        Dim mirrors() As IndirmeAdresi = {IndirmeAdresi.FromURL(ComboBox10.SelectedValue & If(MaskedTextBox3.MaskCompleted = False, "", "&start=" & TimeSpan.Parse(MaskedTextBox3.Text).TotalSeconds), False, "", "")}
        For Each mirrorRl As IndirmeAdresi In mirrors
            mirrorRl.BindProtocolProviderType()
            If mirrorRl.ProtocolProviderType Is Nothing Then
                MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Return
            End If
        Next
        Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(TextBox21.Text), mirrors, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", genelcookie.ToArray, genelheader.ToArray)
    End Sub
#End Region

#Region "acunn İndir"
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim kaynak As String = YardimciKomutlar.KaynakKoduAl(TextBox3.Text).kaynakkodu.Trim()
            Dim videokaynak As String = YardimciKomutlar.KaynakKoduAl("https://www.acunn.com/embed_video.html?id=" & TagYakala(kaynak, "videoId             =   '", "'")).kaynakkodu
            Dim videolink As String = TagYakala(videokaynak, "src:""", """")
            If videolink = "" Then MsgBox("Video bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
            TextBox2.Text = TagYakala(kaynak, """name"": """, """").Replace("\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace(Chr(34), "").Replace("<", "").Replace(">", "").Replace("|", "").Replace("  ", " ")
            Dim video As String = YardimciKomutlar.KaynakKoduAl(videolink).kaynakkodu.Replace("chunklist", Split(videolink, "playlist")(0) & "chunklist")
            Dim değerler As New List(Of Değer)()
            değerler.Clear()
            Dim say As Integer = 1
            For Each match As Match In YardimciKomutlar.regx.Matches(video)
                Dim kalite As String = Split(TagYakala(video, "RESOLUTION=", vbLf, say), "x")(1)
                say += 1
                If kalite = "1080" Then Continue For
                değerler.Add(New Değer(kalite & "p", match.Value, Convert.ToInt64(kalite)))
            Next
            ComboBox1.DataSource = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Dim kaynakkodu As String = YardimciKomutlar.KaynakKoduAl(ComboBox1.SelectedValue).kaynakkodu.Replace("media_", Split(ComboBox1.SelectedValue, "chunklist")(0) & "media_")
            Dim kayıtyeri As String = DownloadFolder1.Folder & "\" & TextBox2.Text & ".ts"
            Dim mirrors As New List(Of IndirmeAdresi)()
            For Each match As Match In YardimciKomutlar.regx.Matches(kaynakkodu)
                mirrors.Add(IndirmeAdresi.FromURL(match.Value, False, "", ""))
            Next
            Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(ComboBox1.SelectedValue, False, "", ""), mirrors.ToArray, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", genelcookie.ToArray, genelheader.ToArray)
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub
#End Region

#Region "Rapid İndir"

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Try
            Dim kaynakkodu As String = WebUtility.UrlDecode(WebUtility.HtmlDecode(YardimciKomutlar.KaynakKoduAl(TextBox28.Text).kaynakkodu))
            Dim video As String = TagYakala(kaynakkodu, "<video", "</video>")
            If video = "" Then MsgBox("Video bulunamadı.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
            Dim parcalar As String() = Split(video, "<source")
            TextBox26.Text = TagYakala(kaynakkodu, "<title>", "</title>").Replace("\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace(Chr(34), "").Replace("<", "").Replace(">", "").Replace("|", "")
            Dim değerler As New List(Of Değer)()
            değerler.Clear()
            For Each parca As String In parcalar
                If parca.Contains(".mp4") = False Then Continue For
                Dim kalite As Integer = TagYakala(parca, "data-res=""", """")
                Dim link As String = TagYakala(parca, "src=""", """")
                Dim boyut As Long = ProtocolProviderFactory.GetProvider(link).GetFileSize(IndirmeAdresi.FromURL(link))
                If boyut <= 0 Then Continue For
                değerler.Add(New Değer(kalite & "p (" & YardimciKomutlar.BoyutDuzenle(boyut) & ")", link, kalite))
            Next
            ComboBox12.DataSource = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            Dim mirrors() As IndirmeAdresi = {IndirmeAdresi.FromURL(ComboBox12.SelectedValue)}
            Dim kayıtyeri As String = DownloadFolder1.Folder & "\" & TextBox26.Text & ".mp4"
            For Each mirrorRl As IndirmeAdresi In mirrors
                mirrorRl.BindProtocolProviderType()
                If mirrorRl.ProtocolProviderType Is Nothing Then
                    MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return
                End If
            Next
            Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(TextBox28.Text), mirrors.ToArray, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", genelcookie.ToArray, genelheader.ToArray)
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub
#End Region

#Region "GoogleDrive Video İndir"
    Private drivecookieheader As List(Of String)
    Private driveheader As WebHeaderCollection
    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Try
            Dim id As String
            Dim videos As String = ""
            Dim items As JArray = Nothing
            Dim url As String = TextBox31.Text
            Dim drivecookie As CookieCollection = New CookieCollection
            drivecookieheader = New List(Of String)
            For index = 1 To 2
                id = YardimciKomutlar.KaynakKoduAl(url, WebRequestMethods.Http.Get, drivecookieheader, drivecookie).kaynakkodu
                videos = Düzenle(TagYakala(id, "window.viewerData = {", "};"))
                items = JArray.Parse(Split(videos, "itemJson: ")(1))
                url = items(16)
            Next
            Dim header As New WebHeaderCollection
            header.Add("x-drive-first-party", "DriveWebUi")
            header.Add("x-json-requested", "true")
            Dim sen As String = Düzenle(YardimciKomutlar.KaynakKoduAl(items(18), WebRequestMethods.Http.Post, drivecookieheader, drivecookie, Encoding.UTF8.GetBytes(""), header).kaynakkodu)
            Dim orjinal As String = TagYakala(sen, "downloadUrl"":""", """")
            If orjinal = "" Then MsgBox("İndirme sınırına ulaştı hatası.", MsgBoxStyle.Exclamation, "Hata") : Exit Sub
            Dim değerler As New List(Of Değer)()
            driveheader = New WebHeaderCollection
            driveheader.Add("boyut", items(25)(2).ToString())
            Dim boyut As Long = ProtocolProviderFactory.GetProvider(orjinal).GetFileSize(IndirmeAdresi.FromURL(orjinal), drivecookie, driveheader)
            değerler.Add(New Değer("Orjinal (" & YardimciKomutlar.BoyutDuzenle(boyut) & ") (." & items(32).ToString() & ")", orjinal, boyut))
            TextBox30.Text = items(1).ToString().Replace("\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace(Chr(34), "").Replace("<", "").Replace(">", "").Replace("|", "")
            Dim videolar As String = TagYakala(videos, """fmt_stream_map"",""", """")
            If videolar <> "" Then
                Dim kaliteler As New Dictionary(Of String, String)
                For Each deger As String In Split(TagYakala(videos, """fmt_list"",""", """"), ",")
                    Dim s() As String = Split(deger, "/")
                    kaliteler.Add(s(0), Split(s(1), "x")(1))
                Next
                For Each deger As String In Split(videolar, ",")
                    Dim s() As String = Split(deger, "|")
                    Dim dosyaboyut As Long = ProtocolProviderFactory.GetProvider(s(1)).GetFileSize(IndirmeAdresi.FromURL(s(1)), drivecookie)
                    If dosyaboyut <= 0 Then Continue For
                    Dim kalite As Integer = kaliteler(s(0))
                    değerler.Add(New Değer(kalite.ToString & "p (" & YardimciKomutlar.BoyutDuzenle(dosyaboyut) & ") (.mp4)", s(1), kalite))
                Next
            End If
            değerler = değerler.OrderByDescending(Function(k) k.Sıralama).ToList
            ComboBox13.DataSource = değerler.ToList
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Try
            Dim kayıtyeri As String = Path.Combine(DownloadFolder1.Folder, TextBox30.Text & "." & TagYakala(ComboBox13.Text, "(.", ")"))
            Dim mirrors() As IndirmeAdresi = {IndirmeAdresi.FromURL(ComboBox13.SelectedValue)}
            For Each mirrorRl As IndirmeAdresi In mirrors
                mirrorRl.BindProtocolProviderType()
                If mirrorRl.ProtocolProviderType Is Nothing Then
                    MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return
                End If
            Next
            Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(TextBox31.Text), mirrors, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", drivecookieheader.ToArray, driveheader.ToString().Trim.Replace(vbLf, "").Split(vbCrLf))
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub
#End Region

    Public Function TagYakala(ByVal veri As String, ByVal başlangıç As String, ByVal bitiş As String, Optional index As Integer = 1) As String
        Dim değer As String = ""
        Try
            Dim ilk As String = Split(veri, başlangıç, -1, CompareMethod.Text)(index)
            değer = Split(ilk, bitiş, -1, CompareMethod.Text)(0)
        Catch ex As Exception
        End Try
        Return değer
    End Function

    Private Function Düzenle(değer As String) As String
        For i As Integer = 32 To 383
            değer = Replace(değer, "\u" & Convert.ToUInt16(ChrW(i)).ToString("X4"), ChrW(i).ToString, 1, -1, CompareMethod.Text)
        Next
        Return değer.Replace("\u2019", "'")
    End Function

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Button12.Enabled = True
            TextBox1.Text = Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim kayıtyeri As String = DownloadFolder1.Folder & "\" & TextBox1.Text & ".ts"
        Dim mirrors As New List(Of IndirmeAdresi)()
        Dim okunan As String = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
        For Each match As Match In YardimciKomutlar.regx.Matches(okunan)
            mirrors.Add(IndirmeAdresi.FromURL(match.Value))
        Next
        For Each mirrorRl As IndirmeAdresi In mirrors
            mirrorRl.BindProtocolProviderType()
            If mirrorRl.ProtocolProviderType Is Nothing Then
                MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Return
            End If
        Next
        Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL("http://www.yasin.com"), mirrors.ToArray, kayıtyeri, NumericUpDown1.Value, CheckBox1.Checked, False, "", genelcookie.ToArray, genelheader.ToArray)
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        If basladi = True Then
            IndirmeAyar.Default.EnFazlaAyniAndaIndirilecekSegmentSayisi = NumericUpDown2.Value
            IndirmeAyar.Default.Save()
            IndirmeAyar.Default.Upgrade()
        End If
    End Sub

    Private Sub MetroButton3_Click(sender As Object, e As EventArgs) Handles MetroButton3.Click
        Try
            Dim temizurl As String = TextBox29.Text & "/live/" & TextBox24.Text & "/" & TextBox25.Text & "/" & TextBox33.Text & ".m3u8"
            Dim kaynak As Sonuc = YardimciKomutlar.KaynakKoduAl(temizurl)
            Dim kayıtyeri As String = DownloadFolder1.Folder & "\Canlı Yayın (" & TextBox33.Text & ").ts"
            If kaynak.kaynakkodu = "" Then
                Dim mirrors As New List(Of IndirmeAdresi)
                mirrors.Add(IndirmeAdresi.FromURL(temizurl.Replace(".m3u8", ".ts")))
                Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(mirrors(0), mirrors.ToArray, kayıtyeri, NumericUpDown1.Value, True, False, "", genelcookie.ToArray, genelheader.ToArray)
            Else
                Dim link As String = Split(kaynak.url.OriginalString, "/live")(0)
                Dim kaynakkodu As String = kaynak.kaynakkodu.Replace("/hlsr", link & "/hlsr")
                Dim mirrors As New List(Of IndirmeAdresi)()
                For Each match As Match In YardimciKomutlar.regx.Matches(kaynakkodu)
                    mirrors.Add(IndirmeAdresi.FromURL(match.Value, False, "", ""))
                Next
                Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(temizurl, True, "/hlsr", link & "/hlsr"), mirrors.ToArray, kayıtyeri, NumericUpDown1.Value, True, True, "", genelcookie.ToArray, genelheader.ToArray)
            End If
        Catch ex As Exception
            MsgBox("Hata oluştu." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub TextBox23_TextChanged(sender As Object, e As EventArgs) Handles TextBox24.TextChanged, TextBox25.TextChanged, TextBox29.TextChanged
        If basladi = True Then
            IndirmeAyar.Default.kadi = TextBox24.Text
            IndirmeAyar.Default.sifre = TextBox25.Text
            IndirmeAyar.Default.sunucu = TextBox29.Text
            IndirmeAyar.Default.Save()
            IndirmeAyar.Default.Upgrade()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            Timer1.Start()
        Else
            Timer1.Stop()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim zaman1 As String = Now.TimeOfDay.ToString("hh\:mm\:ss")
        Dim zaman2 As String = DateTimePicker1.Value.TimeOfDay.ToString("hh\:mm\:ss")
        If zaman1 = zaman2 Then
            IndirmeListesi1.IndirmeBaslat()
            Timer1.Stop()
            If CheckBox3.Checked = True Then Timer2.Start()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If IndirmeListesi1.Kontrol() = True Then
            kapat = True
            Timer2.Stop()
            Application.Exit()
        End If
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If kapat = True Then
            Process.Start("shutdown", "-f -s")
        End If
    End Sub

    Private Sub TextBox27_TextChanged(sender As Object, e As EventArgs) Handles TextBox27.TextChanged
        Dim uri As Uri = Nothing
        If Uri.TryCreate(TextBox27.Text, UriKind.Absolute, uri) = True Then
            Select Case uri.Host
                Case "www.showtv.com.tr"
                    Temizle()
                    Panel8.Visible = True
                    TextBox22.Text = uri.ToString()
                Case "ok.ru"
                    Temizle()
                    Panel4.Visible = True
                    TextBox21.Text = uri.ToString()
                Case "www.kanald.com.tr"
                    Temizle()
                    Panel14.Visible = True
                    TextBox12.Text = uri.ToString()
                Case "my.mail.ru"
                    Temizle()
                    Panel2.Visible = True
                    TextBox5.Text = uri.ToString()
                Case "www.fox.com.tr"
                    Temizle()
                    Panel13.Visible = True
                    TextBox15.Text = uri.ToString()
                Case "puhutv.com"
                    Temizle()
                    Panel12.Visible = True
                    TextBox8.Text = uri.ToString()
                Case "www.atv.com.tr"
                    Temizle()
                    Panel7.Visible = True
                    Url.Text = uri.ToString()
                Case "www.rapidvideo.com"
                    Temizle()
                    Panel1.Visible = True
                    TextBox28.Text = uri.ToString()
                Case "drive.google.com"
                    Temizle()
                    Panel16.Visible = True
                    TextBox31.Text = uri.ToString()
                Case Else
                    Temizle()
            End Select
        Else
            Temizle()
        End If
    End Sub

    Private Sub Temizle()
        For Each control As Control In FlowLayoutPanel2.Controls
            control.Visible = False
        Next
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            Dim okunan As String = File.ReadAllText(OpenFileDialog2.FileName, Encoding.UTF8)
            Dim ogeler As String() = Split(okunan, vbCrLf, -1, CompareMethod.Text)
            For index = 1 To ogeler.Length - 1 Step 2
                Dim link As String = ogeler(index)
                Dim ad As String = ogeler(index - 1)
                Dim kayityeri As String = Path.Combine(DownloadFolder1.Folder, ad & Path.GetExtension(link))
                Dim mirrors() As IndirmeAdresi = {IndirmeAdresi.FromURL(link)}
                For Each mirrorRl As IndirmeAdresi In mirrors
                    mirrorRl.BindProtocolProviderType()
                    If mirrorRl.ProtocolProviderType Is Nothing Then
                        MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                        Return
                    End If
                Next
                Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(IndirmeAdresi.FromURL(link), mirrors, kayityeri, NumericUpDown1.Value, False, False, "", genelcookie.ToArray, genelheader.ToArray)
            Next
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        TextBox23.Visible = CheckBox4.Checked
    End Sub
End Class

Public Class Değer
    Public Property Değer() As String
    Public Property İsim() As String
    Public Property Sıralama() As Long

    Public Sub New(ByVal __isim As String, ByVal __değer As String, Optional ByVal __sıralama As Long = 1)
        Değer = __değer
        İsim = __isim
        Sıralama = __sıralama
    End Sub
End Class
