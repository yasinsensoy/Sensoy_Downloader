Imports System.IO
Imports System.Threading
Imports System.Net
Imports Sensoy_Downloader.Parca
Imports System.Threading.Tasks
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization

<Serializable> Public Class Indirme
#Region "Deðerler"
    <NonSerialized> Private anathread As Thread
    Private filestream As Stream
    <NonSerialized> Private ilkstream As BinaryReader
    Private arkaplanogeleri As List(Of ArkaPlandaCalis)
    Private m_sonhatamesaji As String
    Private defaultDownloadProvider As IProtocolProvider

    Public Enum IndirmeDurum
        Hazir
        Baglaniliyor
        Indiriliyor
        Durduruluyor
        Durduruldu
        Tamamlandi
        Hata
    End Enum
#End Region

#Region "Yükleme Komutlarý"
    Public Sub New()
    End Sub

    Private Sub New(_yenilemeadresi As IndirmeAdresi, _adresler As IndirmeAdresi(), _kayityeri As String)
        arkaplanogeleri = New List(Of ArkaPlandaCalis)()
        Adresler = New List(Of IndirmeAdresi)(_adresler)
        KayitYeri = _kayityeri
        defaultDownloadProvider = Adresler(0).BindProtocolProviderInstance(Me)
        YenilemeAdresi = _yenilemeadresi
    End Sub

    Public Sub New(yenilemeadresi As IndirmeAdresi, adreslerr As IndirmeAdresi(), kayityeri As String, parcasayi As Integer, _canliyayin As Boolean, _cookies As String(), _headers As String())
        Me.New(yenilemeadresi, adreslerr, kayityeri)
        Durum = IndirmeDurum.Hazir
        EklenmeTarihi = Date.Now
        ParcaSayisi = parcasayi
        CanliYayin = _canliyayin
        CookieHeader = New List(Of String)(_cookies)
        HeaderString = New List(Of String)(_headers)
        Parcalar = New List(Of Parca)()
    End Sub

    Public Sub New(yenilemeadresi As IndirmeAdresi, adreslerr As IndirmeAdresi(), kayityeri As String, _parcalar As Parca(), sunucubilgi As SunucuDosyaBilgisi,
                   indirdurum As IndirmeDurum, parcasayi As Integer, olusturmatarih As DateTime, sonhatatarih As DateTime, sonhatamesaj As String, _canliyayin As Boolean,
                   _hatasayisi As Integer, _cookies As String(), _headers As String())
        Me.New(yenilemeadresi, adreslerr, kayityeri)
        If indirdurum = IndirmeDurum.Tamamlandi Or indirdurum = IndirmeDurum.Hata Or indirdurum = IndirmeDurum.Hazir Then
            Durum = indirdurum
        Else
            Durum = IndirmeDurum.Durduruldu
        End If
        EklenmeTarihi = olusturmatarih
        SonHataTarihi = sonhatatarih
        m_sonhatamesaji = sonhatamesaj
        SunucuDosyaBilgi = sunucubilgi
        ParcaSayisi = parcasayi
        CanliYayin = _canliyayin
        HataSayisi = _hatasayisi
        CookieHeader = New List(Of String)(_cookies)
        HeaderString = New List(Of String)(_headers)
        Parcalar = New List(Of Parca)(_parcalar)
    End Sub
#End Region

#Region "Özellikler"
    <XmlIgnore> Public Property BoyutHesaplaniyor As Boolean
    <XmlAttribute("i_cy")> Public Property CanliYayin As Boolean
    <XmlAttribute("i_hs")> Public Property HataSayisi As Integer
    <XmlAttribute("i_et")> Public Property EklenmeTarihi As DateTime
    <XmlAttribute("i_sht")> Public Property SonHataTarihi As DateTime
    <XmlAttribute("i_ps")> Public Property ParcaSayisi As Integer
    <XmlAttribute("i_ky")> Public Property KayitYeri As String
    <XmlAttribute("i_d")> Public Property Durum As IndirmeDurum
    <XmlAttribute("i_bti")> Public Property BoyutToplamIs As Integer
    Public Property YenilemeAdresi As IndirmeAdresi
    Public Property SunucuDosyaBilgi As SunucuDosyaBilgisi
    Public Property Adresler As List(Of IndirmeAdresi)
    Public Property Parcalar As List(Of Parca)
    Public Property CookieHeader As List(Of String)
    Public Property HeaderString As List(Of String)

    Private ReadOnly Property Headers As WebHeaderCollection
        Get
            If HeaderString Is Nothing Or HeaderString.Count <= 0 Then Return Nothing
            Dim yeni As New WebHeaderCollection
            For Each header As String In HeaderString
                Dim a As String() = Split(header, ": ")
                yeni.Add(a(0), a(1))
            Next
            Return yeni
        End Get
    End Property

    Private ReadOnly Property Cookies As CookieCollection
        Get
            If CookieHeader Is Nothing Or CookieHeader.Count <= 0 Then Return Nothing
            Dim yeni As New CookieCollection
            For Each cook As String In CookieHeader
                Dim a As String() = Split(cook, "[=]")
                yeni.Add(New Cookie(a(0), a(1), a(2), a(3)))
            Next
            Return yeni
        End Get
    End Property

    Public ReadOnly Property DosyaBoyutu As Long
        Get
            If SunucuDosyaBilgi Is Nothing Then Return 0
            Return SunucuDosyaBilgi.DosyaBoyutu
        End Get
    End Property

    Public ReadOnly Property TamamlananParcaSayisi As Integer
        Get
            Dim tamamlanan__1 As Integer = 0
            For i As Integer = 0 To Parcalar.Count - 1
                If Parcalar(i).Durum = ParcaDurum.Tamamlandi Then tamamlanan__1 += 1
            Next
            Return tamamlanan__1
        End Get
    End Property

    Public ReadOnly Property AktifParcaSayisi As Integer
        Get
            Dim aktif__1 As Integer = 0
            For Each öðe As Parca In Parcalar
                If öðe.Durum = ParcaDurum.Baglaniliyor Or öðe.Durum = ParcaDurum.Indiriliyor Then aktif__1 += 1
            Next
            Return aktif__1
        End Get
    End Property

    Public ReadOnly Property Ilerleme As Double
        Get
            Dim count As Integer = Parcalar.Count
            If count > 0 Then
                Dim ilerleme__1 As Double = 0
                For i As Integer = 0 To Parcalar.Count - 1
                    ilerleme__1 += Parcalar(i).Ilerleme
                Next
                Return ilerleme__1 / count
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property IndirmeHizi As Double
        Get
            Dim hýz__1 As Double = 0
            For i As Integer = 0 To Parcalar.Count - 1
                hýz__1 += Parcalar(i).IndirmeHizi
            Next
            Return hýz__1
        End Get
    End Property

    Public ReadOnly Property Indirilen As Long
        Get
            Dim indirilen__1 As Long = 0
            For i As Integer = 0 To Parcalar.Count - 1
                indirilen__1 += Parcalar(i).Indirilen
            Next
            Return indirilen__1
        End Get
    End Property

    Public ReadOnly Property KalanSure As TimeSpan
        Get
            If IndirmeHizi = 0 Then Return TimeSpan.FromSeconds(0)
            Dim missingTransfer As Double = 0
            For i As Integer = 0 To Parcalar.Count - 1
                missingTransfer += Parcalar(i).KalanBoyut
            Next
            Return TimeSpan.FromSeconds(missingTransfer / IndirmeHizi)
        End Get
    End Property

    <XmlAttribute("i_shm")> Public Property SonHataMesaji As String
        Get
            Return m_sonhatamesaji
        End Get
        Set(value As String)
            m_sonhatamesaji = value
            If value <> "" Then SonHataTarihi = DateTime.Now
        End Set
    End Property
#End Region

#Region "Komutlar"
    Private Sub KayitYeriKontrol()
        Dim fileInfo As New FileInfo(KayitYeri)
        If Directory.Exists(fileInfo.DirectoryName) = False Then Directory.CreateDirectory(fileInfo.DirectoryName)
        If fileInfo.Exists = True Then
            Dim count As Integer = 1
            Dim dosyaadi As String = Path.GetFileNameWithoutExtension(KayitYeri)
            Dim uzanti As String = Path.GetExtension(KayitYeri)
            Dim yenikayityeri As String
            Do
                yenikayityeri = Convert.ToString(Path.Combine(fileInfo.DirectoryName, dosyaadi) & String.Format(" ({0})", Math.Max(Interlocked.Increment(count), count - 1))) & uzanti
            Loop While File.Exists(yenikayityeri) = True
            KayitYeri = yenikayityeri
        End If
        Using fs As New FileStream(KayitYeri, FileMode.Create, FileAccess.Write)
            fs.SetLength(Math.Max(DosyaBoyutu, 0))
        End Using
    End Sub

    Private Function ToplamDosyaBoyutu(ByRef segmentCount As Integer) As SunucuDosyaBilgisi
        Dim yeni As SunucuDosyaBilgisi
        If Adresler.Count > 1 Then
            SyncLock Parcalar
                Parcalar.Clear()
            End SyncLock
            Dim hataliliste As New List(Of IndirmeAdresi)(Adresler)
            Dim eklenenparcalar As New List(Of Parca)
            BoyutToplamIs = 0
            Dim toplamboyut As Long = 0
            BoyutHesaplaniyor = True
tekrar:
            Dim liste As New List(Of IndirmeAdresi)(hataliliste)
            hataliliste.Clear()
            Parallel.ForEach(liste, Sub(list As IndirmeAdresi)
                                        Try
                                            Dim request As HttpWebRequest = WebRequest.CreateHttp(list.Adres)
                                            request.CookieContainer = New CookieContainer
                                            If Cookies IsNot Nothing Then
                                                request.CookieContainer.Add(Cookies)
                                            End If
                                            request.KeepAlive = True
                                            request.Method = WebRequestMethods.Http.Get
                                            request.AllowAutoRedirect = True
                                            request.Referer = list.Adres
                                            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 OPR/57.0.3098.116"
                                            request.ProtocolVersion = HttpVersion.Version11
                                            If Headers IsNot Nothing Then
                                                For Each name As String In Headers
                                                    Dim value As String = Headers.Get(name)
                                                    If name.ToLower = "referer" Then
                                                        request.Referer = value
                                                    Else
                                                        request.Headers.Add(name, value)
                                                    End If
                                                Next
                                            End If
                                            Dim response As WebResponse = request.GetResponse()
                                            eklenenparcalar.Add(New Parca With {.Index = Adresler.IndexOf(list), .IndirmeAdresi = list.Adres, .DosyaBoyutu = response.ContentLength})
                                            Interlocked.Add(toplamboyut, response.ContentLength)
                                            response.Close()
                                            Interlocked.Increment(BoyutToplamIs)
                                        Catch ex As Exception
                                            hataliliste.Add(list)
                                        End Try
                                    End Sub)
            If hataliliste.Count > 0 Then GoTo tekrar
            If Adresler.Count <> eklenenparcalar.Count Then
                For Each adres As IndirmeAdresi In Adresler
                    Dim var As Boolean = False
                    For Each parçaa As Parca In eklenenparcalar
                        If parçaa.IndirmeAdresi = adres.Adres Then var = True : Exit For
                    Next
                    If var = False Then hataliliste.Add(adres)
                Next
            End If
            If hataliliste.Count > 0 Then GoTo tekrar
            BoyutHesaplaniyor = False
            Parcalar = eklenenparcalar.OrderBy(Function(k) k.Index).ToList
            segmentCount = Adresler.Count
            yeni = defaultDownloadProvider.GetFileInfo(Adresler(0), ilkstream, Cookies, Headers)
            yeni.DosyaBoyutu = toplamboyut
        Else
            yeni = defaultDownloadProvider.GetFileInfo(Adresler(0), ilkstream, Cookies, Headers)
        End If
        Return yeni
    End Function

    Public Sub Durdur()
        If Durum = IndirmeDurum.Baglaniliyor Then
            Parcalar.Clear()
            anathread.Abort()
            anathread = Nothing
            Durum = IndirmeDurum.Durduruldu
        ElseIf Durum = IndirmeDurum.Indiriliyor Then
            Durum = IndirmeDurum.Durduruluyor
            anathread.Abort()
            anathread = Nothing
            Thread.Sleep(2000)
            SyncLock arkaplanogeleri
                For Each arka As ArkaPlandaCalis In arkaplanogeleri
                    arka.Dispose()
                Next
                arkaplanogeleri.Clear()
            End SyncLock
            If filestream IsNot Nothing Then filestream.Close()
            Durum = IndirmeDurum.Durduruldu
        End If
    End Sub

    Public Sub Baslat()
        If Durum = IndirmeDurum.Hazir Or ParcaSayisi <> Parcalar.Count Then
            anathread = New Thread(New ThreadStart(AddressOf ÝndirmeBaþlat))
            anathread.IsBackground = True
            anathread.Start()
        ElseIf Durum = IndirmeDurum.Hata Or Durum = IndirmeDurum.Durduruldu Then
            anathread = New Thread(New ThreadStart(AddressOf ÝndirmeYenidenBaþlat))
            anathread.IsBackground = True
            anathread.Start()
        End If
    End Sub

    Private Sub ÝndirmeBaþlat()
        Try
            SonHataMesaji = ""
            Durum = IndirmeDurum.Baglaniliyor
            If My.Computer.Network.IsAvailable = False Then Throw New Exception("İnternet baðlantınız yok.")
            SunucuDosyaBilgi = ToplamDosyaBoyutu(ParcaSayisi)
            ParçalarýAyarla(ParcaSayisi)
        Catch generatedExceptionName As ThreadAbortException
            Exit Sub
        Catch ex As Exception
            HataSayisi += 1
            SonHataMesaji = ex.Message
            Durum = IndirmeDurum.Hata
        End Try
    End Sub

    Private Sub ÝndirmeYenidenBaþlat()
        Dim yenibilgisunucu As SunucuDosyaBilgisi = Nothing
        Try
            SonHataMesaji = ""
            Durum = IndirmeDurum.Baglaniliyor
            If My.Computer.Network.IsAvailable = False Then Throw New Exception("İnternet baðlantınız yok.")
            If SunucuDosyaBilgi Is Nothing Then
                SunucuDosyaBilgi = ToplamDosyaBoyutu(ParcaSayisi)
                ParçalarýAyarla(ParcaSayisi)
                Exit Try
            End If
            If Adresler.Count > 1 Then
                ParçalarýBaþlat()
            Else
                yenibilgisunucu = defaultDownloadProvider.GetFileInfo(Adresler(0), ilkstream, Cookies, Headers)
                If yenibilgisunucu.DevamEdebilme = False Or yenibilgisunucu.SonDuzenlemeTarihi > SunucuDosyaBilgi.SonDuzenlemeTarihi Or yenibilgisunucu.DosyaBoyutu <> SunucuDosyaBilgi.DosyaBoyutu Then
                    SunucuDosyaBilgi = yenibilgisunucu
                    ParçalarýAyarla(ParcaSayisi)
                Else
                    ParçalarýBaþlat()
                End If
            End If
        Catch generatedExceptionName As ThreadAbortException
            Exit Sub
        Catch ex As Exception
            HataSayisi += 1
            SonHataMesaji = ex.Message
            Durum = IndirmeDurum.Hata
        End Try
    End Sub

    Private Sub ParçalarýAyarla(segmentCount As Integer)
        KayitYeriKontrol()
        SyncLock arkaplanogeleri
            arkaplanogeleri.Clear()
        End SyncLock
        If Adresler.Count > 1 Then
            For i As Integer = 0 To Adresler.Count - 1
                Dim boyut As Long = Parcalar(i).DosyaBoyutu
                Parcalar(i).SabitBaslangicBoyutu = (If(i = 0, 0, Parcalar(i - 1).BitisBoyutu))
                Parcalar(i).BaslangicBoyutu = (If(i = 0, 0, Parcalar(i - 1).BitisBoyutu))
                Parcalar(i).BitisBoyutu = (If(i = 0, boyut, Parcalar(i - 1).BitisBoyutu + boyut))
            Next
        Else
            Dim calculatedSegments As CalculatedSegment()
            If SunucuDosyaBilgi.DevamEdebilme = False Then
                calculatedSegments = New CalculatedSegment() {New CalculatedSegment(0, SunucuDosyaBilgi.DosyaBoyutu)}
            Else
                calculatedSegments = YardimciKomutlar.ParcalariGetir(segmentCount, SunucuDosyaBilgi.DosyaBoyutu)
            End If
            ParcaSayisi = calculatedSegments.Count
            SyncLock Parcalar
                Parcalar.Clear()
            End SyncLock
            For i As Integer = 0 To calculatedSegments.Length - 1
                Dim segment As New Parca()
                segment.Index = i
                segment.IndirmeAdresi = Adresler(0).Adres
                segment.SabitBaslangicBoyutu = calculatedSegments(i).StartPosition
                segment.BaslangicBoyutu = calculatedSegments(i).StartPosition
                segment.BitisBoyutu = calculatedSegments(i).EndPosition
                Parcalar.Add(segment)
            Next
        End If
        ParçalarýBaþlat()
    End Sub

    Private Sub ParçalarýBaþlat()
        Durum = IndirmeDurum.Indiriliyor
        HataSayisi = 0
        If filestream IsNot Nothing Then filestream.Close()
        filestream = File.Open(KayitYeri, FileMode.Open, FileAccess.Write, FileShare.Read)
        Dim baþlatýlan As Integer = 0
        Dim len As Integer = Parcalar.Count - 1
        If Parcalar.Count >= 5 Then
            ParçaBaþlat(Parcalar(Parcalar.Count - 1))
            ParçaBaþlat(Parcalar(Parcalar.Count - 2))
            ParçaBaþlat(Parcalar(Parcalar.Count - 3))
            ParçaBaþlat(Parcalar(Parcalar.Count - 4))
            ParçaBaþlat(Parcalar(Parcalar.Count - 5))
            len = Parcalar.Count - 6
            baþlatýlan = 5
        End If
        For i As Integer = 0 To len
            If IndirmeAyar.Default.EnFazlaAyniAndaIndirilecekSegmentSayisi = 0 Then ParçaBaþlat(Parcalar(i)) : Continue For
            If Parcalar(i).Durum <> ParcaDurum.Tamamlandi Then
                If baþlatýlan >= IndirmeAyar.Default.EnFazlaAyniAndaIndirilecekSegmentSayisi Then Exit For
                ParçaBaþlat(Parcalar(i))
                baþlatýlan += 1
            End If
        Next
    End Sub

    Private Sub ParçaBaþlat(newSegment As Parca)
        If Durum = IndirmeDurum.Durduruluyor Or Durum = IndirmeDurum.Durduruldu Then Exit Sub
        SyncLock arkaplanogeleri
            Dim yeniarkaplan As New ArkaPlandaCalis(newSegment, True)
            AddHandler yeniarkaplan.Calis, AddressOf ParcaCalis
            AddHandler yeniarkaplan.CalismaBitti, AddressOf ParcaBitti
            AddHandler yeniarkaplan.BilgiGuncelle, AddressOf ParcaGuncelle
            arkaplanogeleri.Add(yeniarkaplan)
        End SyncLock
    End Sub

    Private Function ParçaYenidenBaþlat(newSegment As Parca) As Boolean
        Dim baþlattý As Boolean = False
        If newSegment.Durum = ParcaDurum.Hata And (IndirmeAyar.Default.MaxRetries = 0 Or newSegment.HataSayisi < IndirmeAyar.Default.MaxRetries) Then
tekrar:
            Dim ts As TimeSpan = DateTime.Now - newSegment.SonHataTarihi
            If ts.TotalSeconds >= IndirmeAyar.Default.RetryDelay Then
                ParçaBaþlat(newSegment)
                baþlattý = True
            Else
                Thread.Sleep(CInt(IndirmeAyar.Default.RetryDelay * 1000 - ts.TotalMilliseconds))
                GoTo tekrar
            End If
        End If
        Return baþlattý
    End Function

    Private Sub ParcaCalis(iþlenecekparça As Parca, arkaplan As ArkaPlandaCalis)
        iþlenecekparça.SonHataMesaji = ""
        Try
            If iþlenecekparça.BitisBoyutu > 0 AndAlso iþlenecekparça.BaslangicBoyutu >= iþlenecekparça.BitisBoyutu Then
                iþlenecekparça.Durum = ParcaDurum.Tamamlandi
                Exit Sub
            End If
            Dim buffSize As Integer = 8192
            Dim buffer As Byte() = New Byte(buffSize - 1) {}
            iþlenecekparça.Durum = ParcaDurum.Baglaniliyor
            Dim location As IndirmeAdresi = IndirmeAdresi.FromURL(iþlenecekparça.IndirmeAdresi, False, "", "")
            Dim provider As IProtocolProvider = location.BindProtocolProviderInstance(Me)
            If Adresler.Count > 1 Then
                iþlenecekparça.InputStream = If(iþlenecekparça.Index = 0 And ilkstream IsNot Nothing, ilkstream, provider.CreateStream(location, iþlenecekparça.Indirilen, iþlenecekparça.DosyaBoyutu, Cookies, Headers))
            Else
                iþlenecekparça.InputStream = If(iþlenecekparça.Index = 0 And ilkstream IsNot Nothing, ilkstream, provider.CreateStream(location, iþlenecekparça.BaslangicBoyutu, iþlenecekparça.BitisBoyutu, Cookies, Headers))
            End If
            iþlenecekparça.Durum = ParcaDurum.Indiriliyor
            iþlenecekparça.HataSayisi = 0
            Dim readSize As Long
            Do
                If Durum = IndirmeDurum.Durduruluyor Then iþlenecekparça.Durum = ParcaDurum.Durduruldu : Exit Do
                readSize = iþlenecekparça.InputStream.Read(buffer, 0, buffSize)
                If readSize = 0 And iþlenecekparça.Indirilen < iþlenecekparça.DosyaBoyutu Then Throw New Exception("Sunucudan baðlantý kesildi.")
                If iþlenecekparça.BitisBoyutu > 0 AndAlso iþlenecekparça.BaslangicBoyutu + readSize > iþlenecekparça.BitisBoyutu Then
                    readSize = iþlenecekparça.KalanBoyut
                    If readSize <= 0 Then iþlenecekparça.BaslangicBoyutu = iþlenecekparça.BitisBoyutu : Exit Do
                End If
                SyncLock filestream
                    filestream.Position = iþlenecekparça.BaslangicBoyutu
                    filestream.Write(buffer, 0, CInt(readSize))
                    filestream.Flush()
                End SyncLock
                iþlenecekparça.HesapYap(readSize)
                If iþlenecekparça.BitisBoyutu > 0 AndAlso iþlenecekparça.BaslangicBoyutu >= iþlenecekparça.BitisBoyutu Then iþlenecekparça.BaslangicBoyutu = iþlenecekparça.BitisBoyutu : Exit Do
                arkaplan.Guncelle()
            Loop While readSize > 0
            If iþlenecekparça.Durum = ParcaDurum.Indiriliyor Then iþlenecekparça.Durum = ParcaDurum.Tamamlandi
        Catch threadex As ThreadAbortException
            iþlenecekparça.Durum = ParcaDurum.Durduruldu
        Catch ex As Exception
            iþlenecekparça.Durum = ParcaDurum.Hata
            iþlenecekparça.SonHataMesaji = ex.Message
        Finally
            iþlenecekparça.InputStream = Nothing
            If iþlenecekparça.Index = 0 Then ilkstream = Nothing
            arkaplan.Guncelle()
        End Try
    End Sub

    Private Sub ParcaBitti(arkaplan As ArkaPlandaCalis)
        SyncLock Parcalar
            If CanliYayin = True Then
                Dim mirrors As List(Of IndirmeAdresi)
                Do
                    Dim kaynakkodu As String = ""
                    Dim say As Integer = 0
tekrar:
                    Try
                        kaynakkodu = YardimciKomutlar.KaynakKoduAl(YenilemeAdresi.Adres).kaynakkodu
                    Catch ex As Exception
                        say += 1
                        If say = 6 Then
                            GoTo bitir
                        End If
                        GoTo tekrar
                    End Try
                    If YenilemeAdresi.GirisIzni = True Then
                        kaynakkodu = kaynakkodu.Replace(YenilemeAdresi.KullaniciAdi, YenilemeAdresi.Sifre)
                    End If
                    mirrors = New List(Of IndirmeAdresi)()
                    For Each match As Match In YardimciKomutlar.regx.Matches(kaynakkodu)
                        Dim var As Boolean = False
                        For Each parca As Parca In Parcalar
                            If Path.GetFileName(New Uri(parca.IndirmeAdresi).LocalPath) = Path.GetFileName(New Uri(match.Value).LocalPath) Then var = True : Exit For
                        Next
                        If var = False Then
                            mirrors.Add(IndirmeAdresi.FromURL(match.Value, False, "", ""))
                        End If
                    Next
                Loop While mirrors.Count <= 0
                Dim baþlangýç As Integer = Parcalar.Count
                Adresler.AddRange(mirrors)
                Dim hatalýliste As New List(Of IndirmeAdresi)(mirrors)
                Dim eklenenparçalar As New List(Of Parca)
                Dim toplamboyut As Long = 0
                BoyutHesaplaniyor = True
                Dim say1 As Integer = 0
                Do
                    say1 += 1
                    If say1 = 10 Then
                        Adresler.RemoveRange(baþlangýç, mirrors.Count)
                        GoTo bitir
                    End If
                    Dim liste As New List(Of IndirmeAdresi)(hatalýliste)
                    hatalýliste.Clear()
                    Parallel.ForEach(liste, Sub(list As IndirmeAdresi)
                                                Try
                                                    Dim request As HttpWebRequest = WebRequest.CreateHttp(list.Adres)
                                                    request.CookieContainer = New CookieContainer
                                                    If Cookies IsNot Nothing Then
                                                        request.CookieContainer.Add(Cookies)
                                                    End If
                                                    request.KeepAlive = True
                                                    request.Method = WebRequestMethods.Http.Get
                                                    request.AllowAutoRedirect = True
                                                    request.Referer = list.Adres
                                                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 OPR/57.0.3098.116"
                                                    request.ProtocolVersion = HttpVersion.Version11
                                                    If Headers IsNot Nothing Then
                                                        For Each name As String In Headers
                                                            Dim value As String = Headers.Get(name)
                                                            If name.ToLower = "referer" Then
                                                                request.Referer = value
                                                            Else
                                                                request.Headers.Add(name, value)
                                                            End If
                                                        Next
                                                    End If
                                                    Dim response As WebResponse = request.GetResponse()
                                                    eklenenparçalar.Add(New Parca With {.Index = baþlangýç + mirrors.IndexOf(list), .IndirmeAdresi = list.Adres, .DosyaBoyutu = response.ContentLength})
                                                    Interlocked.Add(toplamboyut, response.ContentLength)
                                                    response.Close()
                                                    Interlocked.Increment(BoyutToplamIs)
                                                Catch ex As Exception
                                                    hatalýliste.Add(list)
                                                End Try
                                            End Sub)
                Loop While hatalýliste.Count > 0
                BoyutHesaplaniyor = False
                Parcalar.AddRange(eklenenparçalar.OrderBy(Function(k) k.Index).ToList)
                ParcaSayisi += eklenenparçalar.Count
                SunucuDosyaBilgi.DosyaBoyutu += toplamboyut
                For i As Integer = baþlangýç To Parcalar.Count - 1
                    Dim boyut As Long = Parcalar(i).DosyaBoyutu
                    Parcalar(i).SabitBaslangicBoyutu = (If(i = 0, 0, Parcalar(i - 1).BitisBoyutu))
                    Parcalar(i).BaslangicBoyutu = (If(i = 0, 0, Parcalar(i - 1).BitisBoyutu))
                    Parcalar(i).BitisBoyutu = (If(i = 0, boyut, Parcalar(i - 1).BitisBoyutu + boyut))
                Next
bitir:
            End If
            Dim baþlatýlacaklar As New List(Of Parca)
            Dim _parcalar As New List(Of Parca)(Parcalar)
            For Each parçaa As Parca In _parcalar
                If parçaa.Durum = ParcaDurum.Hata Or parçaa.Durum = ParcaDurum.Hazir Or parçaa.Durum = ParcaDurum.Durduruldu Then
                    baþlatýlacaklar.Add(parçaa)
                End If
            Next
            For Each baþlla As Parca In baþlatýlacaklar
                If IndirmeAyar.Default.EnFazlaAyniAndaIndirilecekSegmentSayisi <= AktifParcaSayisi Then Exit For
                If baþlla.Durum = ParcaDurum.Hata Then
                    ParçaYenidenBaþlat(baþlla)
                Else
                    ParçaBaþlat(baþlla)
                End If
            Next
            Dim kontrol As Boolean = True
            Dim hatavar As Boolean = False
            For i As Integer = 0 To Parcalar.Count - 1
                If Parcalar(i).Durum = ParcaDurum.Hata Then
                    If ParçaYenidenBaþlat(Parcalar(i)) = True Then
                        kontrol = False : Exit For
                    Else
                        hatavar = True
                    End If
                ElseIf Parcalar(i).Durum <> ParcaDurum.Tamamlandi Then
                    kontrol = False : Exit For
                End If
            Next
            If kontrol = True Then
                If filestream IsNot Nothing Then filestream.Close()
                If hatavar = True Then Durum = IndirmeDurum.Hata Else Durum = IndirmeDurum.Tamamlandi
                For Each kuyruk As IndirmeKuyruklar In KuyrukYonetici.Yönetici.Kuyruklar
                    Dim çýk As Boolean = False
                    For Each d As Indirme In kuyruk.Indirmeler
                        If d.Durum = IndirmeDurum.Durduruldu Or d.Durum = IndirmeDurum.Hazir Or d.Durum = IndirmeDurum.Hata Then
                            If d.Durum = IndirmeDurum.Hata Then
                                d.Baslat()
                                Thread.Sleep(1000)
                                Continue For
                            Else
                                d.Baslat()
                                çýk = True
                                Exit For
                            End If
                        End If
                    Next
                    If çýk = True Then Exit For
                Next
            End If
        End SyncLock
    End Sub

    Private Sub ParcaGuncelle()

    End Sub
#End Region
End Class