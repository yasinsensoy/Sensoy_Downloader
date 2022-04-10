Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization

<Serializable> Public Class IndirmeAdresi
#Region "De�erler"
    Private m_url As String
    Private m_protocolProviderType As Type
    Private provider As IProtocolProvider
#End Region

#Region "Y�kleme Komutlar�"
    Public Sub New()
    End Sub

    Public Shared Function FromURL(url As String) As IndirmeAdresi
        Dim rl As New IndirmeAdresi With {
            .Adres = New Uri(url).AbsoluteUri,
            .GirisIzni = False,
            .KullaniciAdi = "",
            .Sifre = ""
        }
        Return rl
    End Function

    Public Shared Function FromURLArray(urls As String()) As IndirmeAdresi()
        Dim result As New List(Of IndirmeAdresi)()
        For i As Integer = 0 To urls.Length - 1
            If IsURL(urls(i)) Then result.Add(FromURL(urls(i)))
        Next
        Return result.ToArray()
    End Function

    Public Shared Function FromURL(url As String, authenticate As Boolean, login As String, password As String) As IndirmeAdresi
        Dim rl As New IndirmeAdresi With {
            .Adres = New Uri(url).AbsoluteUri,
            .GirisIzni = authenticate,
            .KullaniciAdi = login,
            .Sifre = password
        }
        Return rl
    End Function
#End Region

#Region "�zellikler"
    <XmlAttribute("ia_a")> Public Property Adres() As String
        Get
            Return m_url
        End Get
        Set(value As String)
            m_url = value
            BindProtocolProviderType()
        End Set
    End Property

    <XmlAttribute("ia_gi")> Public Property GirisIzni() As Boolean
    <XmlAttribute("ia_ka")> Public Property KullaniciAdi() As String
    <XmlAttribute("ia_�")> Public Property Sifre() As String

    <XmlAttribute("ia_it")> Public Property ProtocolProviderType() As String
        Get
            If m_protocolProviderType Is Nothing Then Return Nothing
            Return m_protocolProviderType.AssemblyQualifiedName
        End Get
        Set(value As String)
            If value Is Nothing Then
                BindProtocolProviderType()
            Else
                m_protocolProviderType = Type.[GetType](value)
            End If
        End Set
    End Property

#End Region

#Region "Komutlar"
    Public Function GetProtocolProvider(downloader As Indirme) As IProtocolProvider
        Return BindProtocolProviderInstance(downloader)
    End Function

    Public Sub BindProtocolProviderType()
        provider = Nothing
        If Not [String].IsNullOrEmpty(Me.Adres) Then
            m_protocolProviderType = ProtocolProviderFactory.GetProviderType(Me.Adres)
        End If
    End Sub

    Public Function BindProtocolProviderInstance(downloader As Indirme) As IProtocolProvider
        If m_protocolProviderType Is Nothing Then
            BindProtocolProviderType()
        End If
        If provider Is Nothing Then
            provider = ProtocolProviderFactory.CreateProvider(m_protocolProviderType, downloader)
        End If
        Return provider
    End Function

    Public Function Clone() As IndirmeAdresi
        Return DirectCast(Me.MemberwiseClone(), IndirmeAdresi)
    End Function

    Public Overrides Function ToString() As String
        Return Me.Adres
    End Function

    Public Shared Function IsURL(url As String) As Boolean
        Dim m As Match = Regex.Match(url, "(?<Protocol>\w+):\/\/(?<Domain>[\w.]+\/?)\S*")
        If m.ToString() <> String.Empty Then
            Return True
        End If
        Return False
    End Function
#End Region
End Class
