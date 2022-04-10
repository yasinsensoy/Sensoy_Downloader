Imports System.IO
Imports System.Threading

Public Class IndirmeListesi
    Private timer As Timer

    Public Sub New()
        InitializeComponent()
        AddHandler KuyrukYonetici.Yönetici.İndirmeEklendi, AddressOf Yönetici_Ekle
        AddHandler KuyrukYonetici.Yönetici.KuyrukEklendi, AddressOf Kuyruk_Ekle
        For Each kuyruk As IndirmeKuyruklar In KuyrukYonetici.Yönetici.Kuyruklar
            For Each indir As Indirme In kuyruk.Indirmeler
                İndirmeEkle(indir)
            Next
        Next
        ımageList1 = FileTypeImageList.GetSharedInstance()
    End Sub

    Private Sub Kuyruk_Ekle(kuyruk As IndirmeKuyruklar)
        Me.BeginInvoke(DirectCast(Sub() If TreeView1.Nodes("Kuyruklar").Nodes.ContainsKey(kuyruk.Adi) = False Then TreeView1.Nodes("Kuyruklar").Nodes.Add(kuyruk.Adi, kuyruk.Adi), MethodInvoker))
    End Sub

    Private Sub Yönetici_Ekle(sender As Object, e As DownloaderEventArgs)
        Me.BeginInvoke(DirectCast(Sub() İndirmeEkle(e.Downloader), MethodInvoker))
    End Sub

    Public Sub IndirmeBaslat()
        If İndirmelerListe.Rows.Count > 0 Then
            Dim item As DataGridViewRow = İndirmelerListe.Rows(0)
            Dim d As Indirme = (DirectCast(KuyrukYonetici.mapItemToDownload(item), Indirme))
            d.Baslat()
        End If
    End Sub

    Public Function Kontrol() As Boolean
        If KuyrukYonetici.Yönetici.Kuyruklar.Count > 0 Then
            Dim bitti As Boolean = True
            Dim cik As Boolean = False
            For Each kuyruk As IndirmeKuyruklar In KuyrukYonetici.Yönetici.Kuyruklar
                For Each d As Indirme In kuyruk.Indirmeler
                    If d.Durum <> Indirme.IndirmeDurum.Tamamlandi Then
                        bitti = False
                        cik = True
                        Exit For
                    End If
                Next
                If cik = True Then Exit For
            Next
            Return bitti
        Else
            Return True
        End If
    End Function

    Private Sub İndirmeEkle(d As Indirme)
        If timer IsNot Nothing Then timer.Dispose()
        Dim item As DataGridViewRow = İndirmelerListe.Rows(İndirmelerListe.Rows.Add())
        Dim imageindex As Integer = FileTypeImageList.GetImageIndexByExtention(Path.GetExtension(d.KayitYeri))
        item.Cells("DosyaAdı").Value = Path.GetFileName(d.KayitYeri)
        DirectCast(item.Cells("DosyaAdı"), TextAndImageCell).Image = ımageList1.Images(imageindex)
        item.Cells("Boyut").Value = If(d.DosyaBoyutu = 0, "", YardimciKomutlar.BoyutDuzenle(d.DosyaBoyutu))
        item.Cells("İndirilen").Value = If(d.Indirilen = 0, "", YardimciKomutlar.BoyutDuzenle(d.Indirilen) & " ( " & YardimciKomutlar.IlerlemeDuzenle(d.Ilerleme) & " )")
        item.Cells("İndirmeHızı").Value = ""
        item.Cells("KalanSüre").Value = ""
        item.Cells("Durum").Value = If(d.Durum <> Indirme.IndirmeDurum.Hata, d.Durum.ToString(), d.Durum.ToString() + ", " + d.SonHataMesaji)
        item.Cells("DevamEdebilme").Value = If(d.SunucuDosyaBilgi Is Nothing, "", If(d.SunucuDosyaBilgi.DevamEdebilme, "Evet", "Hayır"))
        item.Cells("Parçalar").Value = ""
        item.Cells("HataSayısı").Value = d.HataSayisi
        item.Cells("EklenmeTarihi").Value = d.EklenmeTarihi
        item.Cells("SonHataTarihi").Value = If(d.SonHataTarihi.Ticks = 0, "", d.SonHataTarihi)
        item.Cells("KayıtYeri").Value = d.KayitYeri
        item.Cells("İndirmeYeri").Value = d.YenilemeAdresi.Adres
        KuyrukYonetici.mapDownloadToItem(d) = item
        KuyrukYonetici.mapItemToDownload(item) = d
        timer = New Timer(New TimerCallback(Sub() Me.BeginInvoke(DirectCast(AddressOf İndirmeleriGüncelle, MethodInvoker))), Nothing, 1, 100)
    End Sub

    Private Sub İndirmeleriGüncelle()
        Try
            For Each item As DataGridViewRow In İndirmelerListe.Rows
                If item Is Nothing Then Continue For
                Dim d As Indirme = TryCast(KuyrukYonetici.mapItemToDownload(item), Indirme)
                If d Is Nothing Then Continue For
                Dim uzantıdeğişti As Boolean = Path.GetExtension(d.KayitYeri) <> Path.GetExtension(item.Cells("KayıtYeri").Value.ToString)
                item.Cells("DosyaAdı").Value = Path.GetFileName(d.KayitYeri)
                If uzantıdeğişti = True Then
                    Dim imageindex As Integer = FileTypeImageList.GetImageIndexByExtention(Path.GetExtension(d.KayitYeri))
                    DirectCast(item.Cells("DosyaAdı"), TextAndImageCell).Image = ımageList1.Images(imageindex)
                End If
                item.Cells("Boyut").Value = If(d.BoyutHesaplaniyor, "Hesaplanıyor ( " & d.Adresler.Count & "\" & d.BoyutToplamIs & " )", If(d.DosyaBoyutu = 0, "", YardimciKomutlar.BoyutDuzenle(d.DosyaBoyutu)))
                item.Cells("İndirilen").Value = If(d.Indirilen = 0, "", YardimciKomutlar.BoyutDuzenle(d.Indirilen) & " ( " & YardimciKomutlar.IlerlemeDuzenle(d.Ilerleme) & " )")
                item.Cells("İndirmeHızı").Value = If(d.IndirmeHizi = 0, "", YardimciKomutlar.BoyutDuzenle(d.IndirmeHizi) & "/san")
                item.Cells("KalanSüre").Value = If(d.KalanSure.TotalSeconds = 0, "", YardimciKomutlar.KalanSureDuzenle(d.KalanSure))
                item.Cells("Durum").Value = If(d.Durum <> Indirme.IndirmeDurum.Hata, d.Durum.ToString(), d.Durum.ToString() + ", " + d.SonHataMesaji)
                item.Cells("DevamEdebilme").Value = If(d.SunucuDosyaBilgi Is Nothing, "", If(d.SunucuDosyaBilgi.DevamEdebilme, "Evet", "Hayır"))
                item.Cells("Parçalar").Value = d.ParcaSayisi & "\" & d.TamamlananParcaSayisi & "\" & d.AktifParcaSayisi
                item.Cells("HataSayısı").Value = d.HataSayisi
                item.Cells("EklenmeTarihi").Value = d.EklenmeTarihi
                item.Cells("SonHataTarihi").Value = If(d.SonHataTarihi.Ticks = 0, "", d.SonHataTarihi)
                item.Cells("KayıtYeri").Value = d.KayitYeri
                item.Cells("İndirmeYeri").Value = d.YenilemeAdresi.Adres
            Next
            If İndirmelerListe.SelectedRows.Count = 1 Then
                Dim d1 As Indirme = TryCast(KuyrukYonetici.mapItemToDownload(İndirmelerListe.SelectedRows(0)), Indirme)
                If d1.Parcalar.Count = ParçalarListe.Rows.Count Then
                    ParçalarıGüncelle(d1)
                Else
                    ParçalarıEkle(d1)
                End If
            Else
                BlockedProgressBar1.BlockList.Clear()
                BlockedProgressBar1.Refresh()
                ParçalarListe.Rows.Clear()
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub ParçalarıGüncelle(d As Indirme)
        Try
            For i As Integer = 0 To d.Parcalar.Count - 1
                Dim item As DataGridViewRow = ParçalarListe.Rows(i)
                item.Cells("Parçaİd").Value = d.Parcalar(i).Index
                item.Cells("ParçaBoyut").Value = If(d.Parcalar(i).DosyaBoyutu = 0, "", YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).DosyaBoyutu))
                item.Cells("Parçaİndirilen").Value = If(d.Parcalar(i).Indirilen = 0, "", YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).Indirilen) & " ( " & YardimciKomutlar.IlerlemeDuzenle(d.Parcalar(i).Ilerleme) & " )")
                item.Cells("BaşlangıçBoyutu").Value = If(d.Parcalar(i).Index = 0, YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).SabitBaslangicBoyutu), If(d.Parcalar(i).SabitBaslangicBoyutu = 0, "", YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).SabitBaslangicBoyutu)))
                item.Cells("BitişBoyutu").Value = If(d.Parcalar(i).BitisBoyutu = 0, "", YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).BitisBoyutu))
                item.Cells("ParçaİndirmeHızı").Value = If(d.Parcalar(i).IndirmeHizi = 0, "", YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).IndirmeHizi) & "/san")
                item.Cells("ParçaKalanSüre").Value = If(d.Parcalar(i).KalanSure.TotalSeconds = 0, "", YardimciKomutlar.KalanSureDuzenle(d.Parcalar(i).KalanSure))
                item.Cells("ParçaHataSayısı").Value = d.Parcalar(i).HataSayisi
                item.Cells("ParçaDurum").Value = If(d.Parcalar(i).Durum <> Parca.ParcaDurum.Hata, d.Parcalar(i).Durum.ToString(), d.Parcalar(i).Durum.ToString() & ", " & d.Parcalar(i).SonHataMesaji)
                item.Cells("ParçaSonHataTarihi").Value = If(d.Parcalar(i).SonHataTarihi.Ticks = 0, "", d.Parcalar(i).SonHataTarihi)
                item.Cells("ParçaİndirmeYeri").Value = d.Parcalar(i).IndirmeAdresi
                BlockedProgressBar1.BlockList(i).Boyut = d.Parcalar(i).DosyaBoyutu
                BlockedProgressBar1.BlockList(i).Ilerleme = d.Parcalar(i).Indirilen
            Next
            BlockedProgressBar1.Refresh()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub ParçalarıEkle(d As Indirme)
        Try
            ParçalarListe.Rows.Clear()
            Dim blocks As New List(Of Block)()
            For i As Integer = 0 To d.Parcalar.Count - 1
                Dim item As DataGridViewRow = ParçalarListe.Rows(ParçalarListe.Rows.Add())
                item.Cells("Parçaİd").Value = d.Parcalar(i).Index
                item.Cells("ParçaBoyut").Value = If(d.Parcalar(i).DosyaBoyutu = 0, "", YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).DosyaBoyutu))
                item.Cells("Parçaİndirilen").Value = If(d.Parcalar(i).Indirilen = 0, "", YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).Indirilen) & " ( " & YardimciKomutlar.IlerlemeDuzenle(d.Parcalar(i).Ilerleme) & " )")
                item.Cells("BaşlangıçBoyutu").Value = If(d.Parcalar(i).Index = 0, YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).SabitBaslangicBoyutu), If(d.Parcalar(i).SabitBaslangicBoyutu = 0, "", YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).SabitBaslangicBoyutu)))
                item.Cells("BitişBoyutu").Value = If(d.Parcalar(i).BitisBoyutu = 0, "", YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).BitisBoyutu))
                item.Cells("ParçaİndirmeHızı").Value = If(d.Parcalar(i).IndirmeHizi = 0, "", YardimciKomutlar.BoyutDuzenle(d.Parcalar(i).IndirmeHizi) & "/san")
                item.Cells("ParçaKalanSüre").Value = If(d.Parcalar(i).KalanSure.TotalSeconds = 0, "", YardimciKomutlar.KalanSureDuzenle(d.Parcalar(i).KalanSure))
                item.Cells("ParçaHataSayısı").Value = d.Parcalar(i).HataSayisi
                item.Cells("ParçaDurum").Value = If(d.Parcalar(i).Durum <> Parca.ParcaDurum.Hata, d.Parcalar(i).Durum.ToString(), d.Parcalar(i).Durum.ToString() & ", " & d.Parcalar(i).SonHataMesaji)
                item.Cells("ParçaSonHataTarihi").Value = If(d.Parcalar(i).SonHataTarihi.Ticks = 0, "", d.Parcalar(i).SonHataTarihi)
                item.Cells("ParçaİndirmeYeri").Value = d.Parcalar(i).IndirmeAdresi
                blocks.Add(New Block(d.Parcalar(i).DosyaBoyutu, d.Parcalar(i).Indirilen))
            Next
            BlockedProgressBar1.BlockList = blocks
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub İndirmelerListeMenü_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles İndirmelerListeMenü.Opening
        KuyruğaTaşıMenuItem.DropDownItems.Clear()
        For Each kuyruk As IndirmeKuyruklar In KuyrukYonetici.Yönetici.Kuyruklar
            AddHandler KuyruğaTaşıMenuItem.DropDownItems.Add(kuyruk.Adi).Click, AddressOf KuyrukTaşı
        Next
    End Sub

    Private Sub KuyrukTaşı(sender As Object, e As EventArgs)
        Dim text As String = DirectCast(sender, ToolStripItem).Text
    End Sub

    Private Sub DevamEtMenuItem_Click(sender As Object, e As EventArgs) Handles DevamEtMenuItem.Click
        For Each item As DataGridViewRow In İndirmelerListe.SelectedRows
            Dim d As Indirme = (DirectCast(KuyrukYonetici.mapItemToDownload(item), Indirme))
            d.Baslat()
        Next
    End Sub

    Private Sub DurdurMenuItem_Click(sender As Object, e As EventArgs) Handles DurdurMenuItem.Click
        ThreadPool.QueueUserWorkItem(Sub()
                                         For Each item As DataGridViewRow In İndirmelerListe.SelectedRows
                                             Dim d As Indirme = (DirectCast(KuyrukYonetici.mapItemToDownload(item), Indirme))
                                             d.Durdur()
                                         Next
                                     End Sub)
    End Sub

    Private Sub SilMenuItem_Click(sender As Object, e As EventArgs) Handles SilMenuItem.Click
        For Each item As DataGridViewRow In İndirmelerListe.SelectedRows
            Dim d As Indirme = (DirectCast(KuyrukYonetici.mapItemToDownload(item), Indirme))
            d.Durdur()
            İndirmelerListe.Rows.Remove(item)
            KuyrukYonetici.mapItemToDownload.Remove(item)
            KuyrukYonetici.mapDownloadToItem.Remove(d)
            For Each kuyruk As IndirmeKuyruklar In KuyrukYonetici.Yönetici.Kuyruklar
                For Each indir As Indirme In kuyruk.Indirmeler
                    If d.Equals(indir) = True Then kuyruk.Indirmeler.Remove(d) : Exit For
                Next
            Next
        Next
    End Sub

    Private Sub KlasörüAçMenuItem_Click(sender As Object, e As EventArgs) Handles KlasörüAçMenuItem.Click
        Dim d As Indirme = DirectCast(KuyrukYonetici.mapItemToDownload(İndirmelerListe.SelectedRows(0)), Indirme)
        Dim yer As String = If(File.Exists(d.KayitYeri), "/select," & d.KayitYeri, Path.GetDirectoryName(d.KayitYeri))
        Process.Start("explorer.exe", yer)
    End Sub

    Private Sub AçMenuItem_Click(sender As Object, e As EventArgs) Handles AçMenuItem.Click
        Dim d As Indirme = DirectCast(KuyrukYonetici.mapItemToDownload(İndirmelerListe.SelectedRows(0)), Indirme)
        If File.Exists(d.KayitYeri) = True Then Process.Start(d.KayitYeri)
    End Sub

    Private Sub BirlikteAçMenuItem_Click(sender As Object, e As EventArgs) Handles BirlikteAçMenuItem.Click
        Dim d As Indirme = DirectCast(KuyrukYonetici.mapItemToDownload(İndirmelerListe.SelectedRows(0)), Indirme)
        If File.Exists(d.KayitYeri) = True Then
            Process.Start("rundll32.exe", "shell32.dll, OpenAs_RunDLL " & d.KayitYeri)
        End If
    End Sub

    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        For Each item As DataGridViewRow In İndirmelerListe.Rows
            item.Visible = False
        Next
        If e.Node.Text = "Tüm İndirmeler" Or e.Node.Text = "Kuyruklar" Then
            For Each item As DataGridViewRow In İndirmelerListe.Rows
                item.Visible = True
            Next
        ElseIf e.Node.Text = "Tamamlananlar" Or e.Node.Text = "Tamamlanmayanlar" Then
            For Each kuyruk As IndirmeKuyruklar In KuyrukYonetici.Yönetici.Kuyruklar
                For Each indir As Indirme In kuyruk.Indirmeler
                    If (indir.Durum = Indirme.IndirmeDurum.Tamamlandi And e.Node.Text = "Tamamlananlar") Or (indir.Durum <> Indirme.IndirmeDurum.Tamamlandi And e.Node.Text = "Tamamlanmayanlar") Then
                        DirectCast(KuyrukYonetici.mapDownloadToItem(indir), DataGridViewRow).Visible = True
                    End If
                Next
            Next
        Else
            For Each kuyruk As IndirmeKuyruklar In KuyrukYonetici.Yönetici.Kuyruklar
                If kuyruk.Adi = e.Node.Text Then
                    For Each indir As Indirme In kuyruk.Indirmeler
                        DirectCast(KuyrukYonetici.mapDownloadToItem(indir), DataGridViewRow).Visible = True
                    Next
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub Panel1_Resize(sender As Object, e As EventArgs) Handles Panel1.Resize
        İndirmelerListe.Height = Panel1.Height \ 2
    End Sub
End Class

Public Class TextAndImageColumn
    Inherits DataGridViewTextBoxColumn
    Private imageValue As Image

    Public Sub New()
        Me.CellTemplate = New TextAndImageCell()
    End Sub

    Public Overrides Function Clone() As Object
        Dim c As TextAndImageColumn = TryCast(MyBase.Clone(), TextAndImageColumn)
        c.imageValue = Me.imageValue
        Return c
    End Function

    Public Property Image() As Image
        Get
            Return Me.imageValue
        End Get
        Set(value As Image)
            If Me.Image IsNot value Then
                Me.imageValue = value
                If Me.InheritedStyle IsNot Nothing Then
                    Dim inheritedPadding As Padding = Me.InheritedStyle.Padding
                    Me.DefaultCellStyle.Padding = New Padding(value.Size.Width, inheritedPadding.Top, inheritedPadding.Right, inheritedPadding.Bottom)
                End If
            End If
        End Set
    End Property
    Private ReadOnly Property TextAndImageCellTemplate() As TextAndImageCell
        Get
            Return TryCast(Me.CellTemplate, TextAndImageCell)
        End Get
    End Property
End Class

Public Class TextAndImageCell
    Inherits DataGridViewTextBoxCell
    Private imageValue As Image
    Private imageSize As Size

    Public Overrides Function Clone() As Object
        Dim c As TextAndImageCell = TryCast(MyBase.Clone(), TextAndImageCell)
        c.imageValue = Me.imageValue
        c.imageSize = Me.imageSize
        Return c
    End Function

    Public Property Image() As Image
        Get
            If Me.OwningColumn Is Nothing OrElse Me.OwningTextAndImageColumn Is Nothing Then

                Return imageValue
            ElseIf Me.imageValue IsNot Nothing Then
                Return Me.imageValue
            Else
                Return Me.OwningTextAndImageColumn.Image
            End If
        End Get
        Set(value As Image)
            If Me.imageValue IsNot value Then
                Me.imageValue = value
                Me.imageSize = value.Size

                Dim inheritedPadding As Padding = Me.InheritedStyle.Padding
                Me.Style.Padding = New Padding(imageSize.Width, inheritedPadding.Top, inheritedPadding.Right, inheritedPadding.Bottom)
            End If
        End Set
    End Property

    Protected Overrides Sub Paint(graphics As Graphics, clipBounds As Rectangle, cellBounds As Rectangle, rowIndex As Integer, cellState As DataGridViewElementStates, value As Object, _
        formattedValue As Object, errorText As String, cellStyle As DataGridViewCellStyle, advancedBorderStyle As DataGridViewAdvancedBorderStyle, paintParts As DataGridViewPaintParts)
        ' Paint the base content
        MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value,
            formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts)
        If Me.Image IsNot Nothing Then
            ' Draw the image clipped to the cell.
            Dim container As System.Drawing.Drawing2D.GraphicsContainer = graphics.BeginContainer()
            graphics.SetClip(cellBounds)
            graphics.DrawImageUnscaled(Me.Image, cellBounds.X, cellBounds.Y + (cellBounds.Height - imageSize.Height) / 2)
            graphics.EndContainer(container)
        End If
    End Sub

    Private ReadOnly Property OwningTextAndImageColumn() As TextAndImageColumn
        Get
            Return TryCast(Me.OwningColumn, TextAndImageColumn)
        End Get
    End Property
End Class