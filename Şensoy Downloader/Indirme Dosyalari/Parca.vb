Imports System.IO
Imports System.Xml.Serialization

<Serializable> Public Class Parca
    Private m_dosyaboyutu As Long
    Private m_sonhatamesaji As String
    Private m_durum As ParcaDurum
    Private started As Boolean = False
    Private lastReception As DateTime = DateTime.MinValue
    Private m_indirmehizi As Double
    Private start As Long
    Private m_kalansure As TimeSpan = TimeSpan.Zero

    Public Enum ParcaDurum
        Hazir
        Baglaniliyor
        Indiriliyor
        Durduruldu
        Tamamlandi
        Hata
    End Enum

    Public Sub New()
    End Sub

    <XmlAttribute("p_hs")> Public Property HataSayisi() As Integer
    <XmlAttribute("p_sht")> Public Property SonHataTarihi() As DateTime
    <XmlAttribute("p_id")> Public Property Index() As Integer
    <XmlAttribute("p_sbb")> Public Property SabitBaslangicBoyutu() As Long
    <XmlAttribute("p_bb")> Public Property BaslangicBoyutu() As Long
    <XmlAttribute("p_bbyt")> Public Property BitisBoyutu() As Long
    <XmlIgnore> Public Property InputStream() As BinaryReader
    <XmlAttribute("p_ia")> Public Property IndirmeAdresi() As String

    <XmlAttribute("p_d")> Public Property Durum As ParcaDurum
        Get
            Return m_durum
        End Get
        Set(value As ParcaDurum)
            m_durum = value
            Select Case m_durum
                Case ParcaDurum.Indiriliyor
                    BeginWork()
                    Exit Select
                Case ParcaDurum.Baglaniliyor, ParcaDurum.Durduruldu, ParcaDurum.Tamamlandi, ParcaDurum.Hata
                    m_indirmehizi = 0.0
                    m_kalansure = TimeSpan.Zero
                    Exit Select
            End Select
        End Set
    End Property

    <XmlAttribute("p_shm")> Public Property SonHataMesaji As String
        Get
            Return m_sonhatamesaji
        End Get
        Set(value As String)
            m_sonhatamesaji = value
            If value <> "" Then
                SonHataTarihi = DateTime.Now
                HataSayisi += 1
            End If
        End Set
    End Property

    Public ReadOnly Property Indirilen As Long
        Get
            Return BaslangicBoyutu - SabitBaslangicBoyutu
        End Get
    End Property

    <XmlAttribute("p_db")> Public Property DosyaBoyutu As Long
        Get
            Return (If(BitisBoyutu = 0 And SabitBaslangicBoyutu = 0, m_dosyaboyutu, If(BitisBoyutu <= 0, 0, BitisBoyutu - SabitBaslangicBoyutu)))
        End Get
        Set(value As Long)
            m_dosyaboyutu = value
        End Set
    End Property

    Public ReadOnly Property KalanBoyut As Long
        Get
            Return (If(BitisBoyutu <= 0, 0, BitisBoyutu - BaslangicBoyutu))
        End Get
    End Property

    Public ReadOnly Property Ilerleme As Double
        Get
            Return (If(BitisBoyutu <= 0, 0, (CDbl(Indirilen) / CDbl(DosyaBoyutu) * 100.0F)))
        End Get
    End Property

    Public ReadOnly Property IndirmeHizi As Double
        Get
            If Durum = ParcaDurum.Indiriliyor Then
                HesapYap(0)
                Return m_indirmehizi
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property KalanSure As TimeSpan
        Get
            Return m_kalansure
        End Get
    End Property

    Public Sub BeginWork()
        start = BaslangicBoyutu
        lastReception = DateTime.Now
        started = True
    End Sub

    Public Sub HesapYap(size As Long)
        SyncLock Me
            Dim now As DateTime = DateTime.Now
            BaslangicBoyutu += size
            If started Then
                Dim ts As TimeSpan = (now - lastReception)
                If ts.TotalSeconds = 0 Then
                    Return
                End If
                m_indirmehizi = CDbl(BaslangicBoyutu - start) / ts.TotalSeconds
                If m_indirmehizi > 0.0 Then
                    m_kalansure = TimeSpan.FromSeconds(KalanBoyut / m_indirmehizi)
                Else
                    m_kalansure = TimeSpan.Zero
                End If
            Else
                start = BaslangicBoyutu
                lastReception = now
                started = True
            End If
        End SyncLock
    End Sub
End Class