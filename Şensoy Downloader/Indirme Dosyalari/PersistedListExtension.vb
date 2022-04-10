Imports System.Threading
Imports System.IO
Imports System.Xml.Serialization
Imports Sensoy_Downloader.Parca

Public Class PersistedListExtension
    Implements IExtension
    Implements IDisposable

    Private Const SaveListIntervalInSeconds As Integer = 10
    Private serializer As XmlSerializer
    Private timer As System.Threading.Timer
    Private disposedValue As Boolean

    Public ReadOnly Property Name() As String Implements IExtension.Name
        Get
            Return "Persisted Download List"
        End Get
    End Property

    Public ReadOnly Property UIExtension() As IUIExtension Implements IExtension.UIExtension
        Get
            Return Nothing
        End Get
    End Property

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If timer IsNot Nothing Then
                    timer.Dispose()
                    timer = Nothing
                End If
                '�ndirmeY�neticisi.Y�netici.PauseAll()
                PersistList()
            End If
        End If
        disposedValue = True
    End Sub

    Private Sub PersistList()
        SaveObjects(KuyrukYonetici.Yönetici.Kuyruklar.ToArray)
    End Sub

    Private Function GetDatabaseFile() As String
        Return Path.Combine(System.Windows.Forms.Application.StartupPath, "downloads.xml")
    End Function

    Private Sub LoadSavedList()
        Using New MyStopwatch("Reading download list")
            If File.Exists(GetDatabaseFile()) = True Then
                Try
                    Using fs As New StreamReader(GetDatabaseFile)
                        Dim downloads As IndirmeKuyruklar() = (DirectCast(serializer.Deserialize(fs), IndirmeKuyruklar()))
                        LoadPersistedObjects(downloads)
                    End Using
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End If
        End Using
    End Sub

    Private Sub SaveObjects(downloads As IndirmeKuyruklar())
        Using New MyStopwatch("Saving download list")
            Try
                Using fs As New StreamWriter(GetDatabaseFile)
                    serializer.Serialize(fs, downloads)
                End Using
            Catch ex As Exception
                Console.WriteLine(ex.ToString())
            End Try
        End Using
    End Sub

    Private Shared Sub LoadPersistedObjects(downloads As IndirmeKuyruklar())
        For Each yeni As IndirmeKuyruklar In downloads
            For Each d As Indirme In yeni.Indirmeler
                For Each parcaa As Parca In d.Parcalar
                    If parcaa.Durum = ParcaDurum.Hata Or parcaa.KalanBoyut > 0 Then parcaa.Durum = ParcaDurum.Durduruldu
                Next
                Dim download As Indirme = KuyrukYonetici.Yönetici.Ekle(d.YenilemeAdresi, d.Adresler.ToArray, d.KayitYeri, d.Parcalar.ToArray, d.SunucuDosyaBilgi, d.Durum, d.ParcaSayisi, d.EklenmeTarihi, d.SonHataTarihi, d.SonHataMesaji, False, d.CanliYayin, d.HataSayisi, yeni.Adi, d.CookieHeader.ToArray, d.HeaderString.ToArray)
            Next
        Next
    End Sub

    Public Sub New()
        serializer = New XmlSerializer(GetType(IndirmeKuyruklar()))
        LoadSavedList()
        timer = New Timer(New TimerCallback(AddressOf PersistList), Nothing, 1, SaveListIntervalInSeconds * 1000)
    End Sub
End Class