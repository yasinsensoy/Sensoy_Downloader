Imports Sensoy_Downloader.Indirme

Public Class KuyrukYonetici
    Private Shared m_yönetici As New KuyrukYonetici
    Public Shared mapItemToDownload As New Hashtable()
    Public Shared mapDownloadToItem As New Hashtable()
    Private m_kuyruklar As New List(Of IndirmeKuyruklar)
    Public Event İndirmeEklendi As EventHandler(Of DownloaderEventArgs)
    Public Event KuyrukEklendi(kuyruk As IndirmeKuyruklar)

    Public Shared ReadOnly Property Yönetici() As KuyrukYonetici
        Get
            Return m_yönetici
        End Get
    End Property

    Public ReadOnly Property Kuyruklar() As List(Of IndirmeKuyruklar)
        Get
            Return m_kuyruklar
        End Get
    End Property

    Public Function Ekle(yenilemeadresi As IndirmeAdresi, adreslerr As IndirmeAdresi(), kayıtyeri As String, parçasayı As Integer, şimdibaşlat As Boolean,
                         canlıyayın As Boolean, kuyrukadı As String, cookies As String(), headers As String()) As Indirme
        Dim d As New Indirme(yenilemeadresi, adreslerr, kayıtyeri, parçasayı, canlıyayın, cookies, headers)
        Ekle(d, şimdibaşlat, kuyrukadı)
        Return d
    End Function

    Public Function Ekle(yenilemeadresi As IndirmeAdresi, adreslerr As IndirmeAdresi(), kayıtyeri As String, parçalarr As Parca(), sunucubilgi As SunucuDosyaBilgisi,
                         indirdurum As IndirmeDurum, parçasayı As Integer, oluşturmatarih As DateTime, sonhatatarih As DateTime, sonhatamesaj As String, şimdibaşlat As Boolean,
                         canlıyayın As Boolean, hatasayısı As Integer, kuyrukadı As String, cookies As String(), headers As String()) As Indirme
        Dim d As New Indirme(yenilemeadresi, adreslerr, kayıtyeri, parçalarr, sunucubilgi, indirdurum, parçasayı, oluşturmatarih, sonhatatarih, sonhatamesaj, canlıyayın, hatasayısı, cookies, headers)
        Ekle(d, şimdibaşlat, kuyrukadı)
        Return d
    End Function

    Public Sub Ekle(downloader As Indirme, autoStart As Boolean, kuyrukadı As String)
        If kuyrukadı = "" Then kuyrukadı = "Ana İndirme"
        Dim kuyruk As IndirmeKuyruklar = Nothing
        For Each öğe As IndirmeKuyruklar In m_kuyruklar
            If öğe.Adi = kuyrukadı Then kuyruk = öğe : Exit For
        Next
        If kuyruk Is Nothing Then
            kuyruk = New IndirmeKuyruklar(downloader, kuyrukadı)
            Kuyruklar.Add(kuyruk)
            YükleKuyrukEklendi(kuyruk)
        Else
            Using kuyruk.LockDownloadList(True)
                kuyruk.İndirmeler.Add(downloader)
            End Using
        End If
        YükleİndirmeEklendi(downloader, autoStart)
        If autoStart = True Then downloader.Baslat()
    End Sub

    Protected Overridable Sub YükleİndirmeEklendi(d As İndirme, willStart As Boolean)
        RaiseEvent İndirmeEklendi(Me, New DownloaderEventArgs(d, willStart))
    End Sub

    Protected Overridable Sub YükleKuyrukEklendi(kuyruk As İndirmeKuyruklar)
        RaiseEvent KuyrukEklendi(kuyruk)
    End Sub
End Class
