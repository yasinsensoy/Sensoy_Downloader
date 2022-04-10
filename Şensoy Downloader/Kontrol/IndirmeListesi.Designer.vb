<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IndirmeListesi
    Inherits System.Windows.Forms.UserControl

    'UserControl, bileşen listesini temizlemeyi bırakmayı geçersiz kılar.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                If timer IsNot Nothing Then timer.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form Tasarımcısı tarafından gerektirilir
    Private components As System.ComponentModel.IContainer

    'NOT: Aşağıdaki yordam Windows Form Tasarımcısı tarafından gerektirilir
    'Windows Form Tasarımcısı kullanılarak değiştirilebilir.  
    'Kod düzenleyicisini kullanarak değiştirmeyin.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Tüm İndirmeler")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Tamamlananlar")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Tamamlanmayanlar")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Kuyruklar")
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.ParçalarListe = New MetroFramework.Controls.MetroGrid()
        Me.Parçaİd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParçaBoyut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Parçaİndirilen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BaşlangıçBoyutu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BitişBoyutu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParçaİndirmeHızı = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParçaKalanSüre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParçaDurum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParçaHataSayısı = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParçaSonHataTarihi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParçaİndirmeYeri = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.BlockedProgressBar1 = New Sensoy_Downloader.BlockedProgressBar()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.İndirmelerListe = New MetroFramework.Controls.MetroGrid()
        Me.DosyaAdı = New Sensoy_Downloader.TextAndImageColumn()
        Me.Boyut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.İndirilen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.İndirmeHızı = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KalanSüre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Durum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DevamEdebilme = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Parçalar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HataSayısı = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EklenmeTarihi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SonHataTarihi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KayıtYeri = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.İndirmeYeri = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.İndirmelerListeMenü = New MetroFramework.Controls.MetroContextMenu(Me.components)
        Me.AçMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BirlikteAçMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KlasörüAçMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.DevamEtMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DurdurMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SilMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.KuyruğaTaşıMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.TextAndImageColumn1 = New Sensoy_Downloader.TextAndImageColumn()
        Me.ımageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TextAndImageColumn2 = New Sensoy_Downloader.TextAndImageColumn()
        Me.TextAndImageColumn3 = New Sensoy_Downloader.TextAndImageColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.ParçalarListe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.İndirmelerListe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.İndirmelerListeMenü.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(839, 606)
        Me.TableLayoutPanel1.TabIndex = 71
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.Splitter1)
        Me.Panel2.Controls.Add(Me.TreeView1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(4, 4)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(831, 598)
        Me.Panel2.TabIndex = 72
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Splitter2)
        Me.Panel1.Controls.Add(Me.İndirmelerListe)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(230, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(601, 598)
        Me.Panel1.TabIndex = 73
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.ParçalarListe)
        Me.Panel3.Controls.Add(Me.BlockedProgressBar1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 300)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(601, 298)
        Me.Panel3.TabIndex = 71
        '
        'ParçalarListe
        '
        Me.ParçalarListe.AllowUserToAddRows = False
        Me.ParçalarListe.AllowUserToDeleteRows = False
        Me.ParçalarListe.AllowUserToOrderColumns = True
        Me.ParçalarListe.AllowUserToResizeRows = False
        Me.ParçalarListe.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ParçalarListe.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ParçalarListe.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.ParçalarListe.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.ParçalarListe.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ParçalarListe.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.ParçalarListe.ColumnHeadersHeight = 28
        Me.ParçalarListe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.ParçalarListe.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Parçaİd, Me.ParçaBoyut, Me.Parçaİndirilen, Me.BaşlangıçBoyutu, Me.BitişBoyutu, Me.ParçaİndirmeHızı, Me.ParçaKalanSüre, Me.ParçaDurum, Me.ParçaHataSayısı, Me.ParçaSonHataTarihi, Me.ParçaİndirmeYeri})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ParçalarListe.DefaultCellStyle = DataGridViewCellStyle3
        Me.ParçalarListe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ParçalarListe.EnableHeadersVisualStyles = False
        Me.ParçalarListe.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.ParçalarListe.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ParçalarListe.Location = New System.Drawing.Point(0, 26)
        Me.ParçalarListe.Name = "ParçalarListe"
        Me.ParçalarListe.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ParçalarListe.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.ParçalarListe.RowHeadersVisible = False
        Me.ParçalarListe.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.ParçalarListe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ParçalarListe.Size = New System.Drawing.Size(601, 272)
        Me.ParçalarListe.TabIndex = 70
        '
        'Parçaİd
        '
        Me.Parçaİd.HeaderText = "İd"
        Me.Parçaİd.Name = "Parçaİd"
        Me.Parçaİd.ReadOnly = True
        Me.Parçaİd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ParçaBoyut
        '
        Me.ParçaBoyut.HeaderText = "Boyut"
        Me.ParçaBoyut.Name = "ParçaBoyut"
        Me.ParçaBoyut.ReadOnly = True
        Me.ParçaBoyut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Parçaİndirilen
        '
        Me.Parçaİndirilen.HeaderText = "İndirilen"
        Me.Parçaİndirilen.Name = "Parçaİndirilen"
        Me.Parçaİndirilen.ReadOnly = True
        Me.Parçaİndirilen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'BaşlangıçBoyutu
        '
        Me.BaşlangıçBoyutu.HeaderText = "Başlangıç Boyutu"
        Me.BaşlangıçBoyutu.Name = "BaşlangıçBoyutu"
        Me.BaşlangıçBoyutu.ReadOnly = True
        Me.BaşlangıçBoyutu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'BitişBoyutu
        '
        Me.BitişBoyutu.HeaderText = "Bitiş Boyutu"
        Me.BitişBoyutu.Name = "BitişBoyutu"
        Me.BitişBoyutu.ReadOnly = True
        Me.BitişBoyutu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ParçaİndirmeHızı
        '
        Me.ParçaİndirmeHızı.HeaderText = "İndirme Hızı"
        Me.ParçaİndirmeHızı.Name = "ParçaİndirmeHızı"
        Me.ParçaİndirmeHızı.ReadOnly = True
        Me.ParçaİndirmeHızı.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ParçaKalanSüre
        '
        Me.ParçaKalanSüre.HeaderText = "Kalan Süre"
        Me.ParçaKalanSüre.Name = "ParçaKalanSüre"
        Me.ParçaKalanSüre.ReadOnly = True
        Me.ParçaKalanSüre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ParçaDurum
        '
        Me.ParçaDurum.HeaderText = "Durum"
        Me.ParçaDurum.Name = "ParçaDurum"
        Me.ParçaDurum.ReadOnly = True
        Me.ParçaDurum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ParçaHataSayısı
        '
        Me.ParçaHataSayısı.HeaderText = "Hata Sayısı"
        Me.ParçaHataSayısı.Name = "ParçaHataSayısı"
        Me.ParçaHataSayısı.ReadOnly = True
        Me.ParçaHataSayısı.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ParçaSonHataTarihi
        '
        DataGridViewCellStyle2.Format = "F"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.ParçaSonHataTarihi.DefaultCellStyle = DataGridViewCellStyle2
        Me.ParçaSonHataTarihi.HeaderText = "Son Hata Tarihi"
        Me.ParçaSonHataTarihi.Name = "ParçaSonHataTarihi"
        Me.ParçaSonHataTarihi.ReadOnly = True
        Me.ParçaSonHataTarihi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ParçaİndirmeYeri
        '
        Me.ParçaİndirmeYeri.HeaderText = "İndirm Yeri"
        Me.ParçaİndirmeYeri.Name = "ParçaİndirmeYeri"
        Me.ParçaİndirmeYeri.ReadOnly = True
        '
        'BlockedProgressBar1
        '
        Me.BlockedProgressBar1.BackColor = System.Drawing.SystemColors.Window
        Me.BlockedProgressBar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BlockedProgressBar1.ForeColor = System.Drawing.Color.DarkGreen
        Me.BlockedProgressBar1.Location = New System.Drawing.Point(0, 0)
        Me.BlockedProgressBar1.Name = "BlockedProgressBar1"
        Me.BlockedProgressBar1.Size = New System.Drawing.Size(601, 26)
        Me.BlockedProgressBar1.TabIndex = 0
        Me.BlockedProgressBar1.Text = "BlockedProgressBar1"
        '
        'Splitter2
        '
        Me.Splitter2.Cursor = System.Windows.Forms.Cursors.SizeNS
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 296)
        Me.Splitter2.MinExtra = 70
        Me.Splitter2.MinSize = 70
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(601, 4)
        Me.Splitter2.TabIndex = 68
        Me.Splitter2.TabStop = False
        '
        'İndirmelerListe
        '
        Me.İndirmelerListe.AllowUserToAddRows = False
        Me.İndirmelerListe.AllowUserToDeleteRows = False
        Me.İndirmelerListe.AllowUserToOrderColumns = True
        Me.İndirmelerListe.AllowUserToResizeRows = False
        Me.İndirmelerListe.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.İndirmelerListe.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.İndirmelerListe.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.İndirmelerListe.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.İndirmelerListe.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.İndirmelerListe.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.İndirmelerListe.ColumnHeadersHeight = 28
        Me.İndirmelerListe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.İndirmelerListe.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DosyaAdı, Me.Boyut, Me.İndirilen, Me.İndirmeHızı, Me.KalanSüre, Me.Durum, Me.DevamEdebilme, Me.Parçalar, Me.HataSayısı, Me.EklenmeTarihi, Me.SonHataTarihi, Me.KayıtYeri, Me.İndirmeYeri})
        Me.İndirmelerListe.ContextMenuStrip = Me.İndirmelerListeMenü
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.İndirmelerListe.DefaultCellStyle = DataGridViewCellStyle8
        Me.İndirmelerListe.Dock = System.Windows.Forms.DockStyle.Top
        Me.İndirmelerListe.EnableHeadersVisualStyles = False
        Me.İndirmelerListe.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.İndirmelerListe.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.İndirmelerListe.Location = New System.Drawing.Point(0, 0)
        Me.İndirmelerListe.Name = "İndirmelerListe"
        Me.İndirmelerListe.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.İndirmelerListe.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.İndirmelerListe.RowHeadersVisible = False
        Me.İndirmelerListe.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.İndirmelerListe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.İndirmelerListe.Size = New System.Drawing.Size(601, 296)
        Me.İndirmelerListe.TabIndex = 69
        '
        'DosyaAdı
        '
        Me.DosyaAdı.HeaderText = "Dosya Adı"
        Me.DosyaAdı.Image = Nothing
        Me.DosyaAdı.Name = "DosyaAdı"
        Me.DosyaAdı.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DosyaAdı.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Boyut
        '
        Me.Boyut.HeaderText = "Boyut"
        Me.Boyut.Name = "Boyut"
        Me.Boyut.ReadOnly = True
        '
        'İndirilen
        '
        Me.İndirilen.HeaderText = "İndirilen"
        Me.İndirilen.Name = "İndirilen"
        Me.İndirilen.ReadOnly = True
        '
        'İndirmeHızı
        '
        Me.İndirmeHızı.HeaderText = "İndirme Hızı"
        Me.İndirmeHızı.Name = "İndirmeHızı"
        Me.İndirmeHızı.ReadOnly = True
        '
        'KalanSüre
        '
        Me.KalanSüre.HeaderText = "Kalan Süre"
        Me.KalanSüre.Name = "KalanSüre"
        Me.KalanSüre.ReadOnly = True
        '
        'Durum
        '
        Me.Durum.HeaderText = "Durum"
        Me.Durum.Name = "Durum"
        Me.Durum.ReadOnly = True
        '
        'DevamEdebilme
        '
        Me.DevamEdebilme.HeaderText = "DevamEdebilme"
        Me.DevamEdebilme.Name = "DevamEdebilme"
        Me.DevamEdebilme.ReadOnly = True
        '
        'Parçalar
        '
        Me.Parçalar.HeaderText = "Parçalar"
        Me.Parçalar.Name = "Parçalar"
        Me.Parçalar.ReadOnly = True
        '
        'HataSayısı
        '
        Me.HataSayısı.HeaderText = "Hata Sayısı"
        Me.HataSayısı.Name = "HataSayısı"
        Me.HataSayısı.ReadOnly = True
        '
        'EklenmeTarihi
        '
        DataGridViewCellStyle6.Format = "F"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.EklenmeTarihi.DefaultCellStyle = DataGridViewCellStyle6
        Me.EklenmeTarihi.HeaderText = "Eklenme Tarihi"
        Me.EklenmeTarihi.Name = "EklenmeTarihi"
        Me.EklenmeTarihi.ReadOnly = True
        '
        'SonHataTarihi
        '
        DataGridViewCellStyle7.Format = "F"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.SonHataTarihi.DefaultCellStyle = DataGridViewCellStyle7
        Me.SonHataTarihi.HeaderText = "Son Hata Tarihi"
        Me.SonHataTarihi.Name = "SonHataTarihi"
        Me.SonHataTarihi.ReadOnly = True
        '
        'KayıtYeri
        '
        Me.KayıtYeri.HeaderText = "Kayıt Yeri"
        Me.KayıtYeri.Name = "KayıtYeri"
        Me.KayıtYeri.ReadOnly = True
        '
        'İndirmeYeri
        '
        Me.İndirmeYeri.HeaderText = "İndirme Yeri"
        Me.İndirmeYeri.Name = "İndirmeYeri"
        Me.İndirmeYeri.ReadOnly = True
        '
        'İndirmelerListeMenü
        '
        Me.İndirmelerListeMenü.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AçMenuItem, Me.BirlikteAçMenuItem, Me.KlasörüAçMenuItem, Me.ToolStripSeparator3, Me.DevamEtMenuItem, Me.DurdurMenuItem, Me.SilMenuItem, Me.ToolStripSeparator4, Me.KuyruğaTaşıMenuItem})
        Me.İndirmelerListeMenü.Name = "İndirmelerListeMenü"
        Me.İndirmelerListeMenü.Size = New System.Drawing.Size(140, 170)
        '
        'AçMenuItem
        '
        Me.AçMenuItem.Name = "AçMenuItem"
        Me.AçMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.AçMenuItem.Text = "Aç"
        '
        'BirlikteAçMenuItem
        '
        Me.BirlikteAçMenuItem.Name = "BirlikteAçMenuItem"
        Me.BirlikteAçMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.BirlikteAçMenuItem.Text = "Birlikte aç..."
        '
        'KlasörüAçMenuItem
        '
        Me.KlasörüAçMenuItem.Name = "KlasörüAçMenuItem"
        Me.KlasörüAçMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.KlasörüAçMenuItem.Text = "Klasörü aç"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(136, 6)
        '
        'DevamEtMenuItem
        '
        Me.DevamEtMenuItem.Name = "DevamEtMenuItem"
        Me.DevamEtMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.DevamEtMenuItem.Text = "Devam et"
        '
        'DurdurMenuItem
        '
        Me.DurdurMenuItem.Name = "DurdurMenuItem"
        Me.DurdurMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.DurdurMenuItem.Text = "Durdur"
        '
        'SilMenuItem
        '
        Me.SilMenuItem.Name = "SilMenuItem"
        Me.SilMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.SilMenuItem.Text = "Sil"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(136, 6)
        '
        'KuyruğaTaşıMenuItem
        '
        Me.KuyruğaTaşıMenuItem.Name = "KuyruğaTaşıMenuItem"
        Me.KuyruğaTaşıMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.KuyruğaTaşıMenuItem.Text = "Kuyruğa taşı"
        '
        'Splitter1
        '
        Me.Splitter1.Cursor = System.Windows.Forms.Cursors.SizeWE
        Me.Splitter1.Location = New System.Drawing.Point(226, 0)
        Me.Splitter1.MinExtra = 300
        Me.Splitter1.MinSize = 80
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 598)
        Me.Splitter1.TabIndex = 71
        Me.Splitter1.TabStop = False
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Left
        Me.TreeView1.FullRowSelect = True
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        TreeNode1.Name = "Tüm İndirmeler"
        TreeNode1.Tag = "Tüm İndirmeler"
        TreeNode1.Text = "Tüm İndirmeler"
        TreeNode2.Name = "Tamamlananlar"
        TreeNode2.Tag = "Tamamlananlar"
        TreeNode2.Text = "Tamamlananlar"
        TreeNode3.Name = "Tamamlanmayanlar"
        TreeNode3.Tag = "Tamamlanmayanlar"
        TreeNode3.Text = "Tamamlanmayanlar"
        TreeNode4.Name = "Kuyruklar"
        TreeNode4.Tag = "Kuyruklar"
        TreeNode4.Text = "Kuyruklar"
        Me.TreeView1.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4})
        Me.TreeView1.Size = New System.Drawing.Size(226, 598)
        Me.TreeView1.TabIndex = 72
        '
        'TextAndImageColumn1
        '
        Me.TextAndImageColumn1.HeaderText = "Dosya Adı"
        Me.TextAndImageColumn1.Image = Nothing
        Me.TextAndImageColumn1.Name = "TextAndImageColumn1"
        Me.TextAndImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'ımageList1
        '
        Me.ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ımageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ımageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'TextAndImageColumn2
        '
        Me.TextAndImageColumn2.HeaderText = "Dosya Adı"
        Me.TextAndImageColumn2.Image = Nothing
        Me.TextAndImageColumn2.Name = "TextAndImageColumn2"
        Me.TextAndImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'TextAndImageColumn3
        '
        Me.TextAndImageColumn3.HeaderText = "Dosya Adı"
        Me.TextAndImageColumn3.Image = Nothing
        Me.TextAndImageColumn3.Name = "TextAndImageColumn3"
        Me.TextAndImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'DataGridViewTextBoxColumn7
        '
        DataGridViewCellStyle10.Format = "F"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn7.HeaderText = "Eklenme Tarihi"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        DataGridViewCellStyle11.Format = "F"
        DataGridViewCellStyle11.NullValue = Nothing
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn8.HeaderText = "Parça İd"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Boyut"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "İndirilen"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Başlangıç Boyutu"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        DataGridViewCellStyle12.Format = "F"
        DataGridViewCellStyle12.NullValue = Nothing
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridViewTextBoxColumn12.HeaderText = "Bitiş Boyutu"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        '
        'DataGridViewTextBoxColumn13
        '
        DataGridViewCellStyle13.Format = "F"
        DataGridViewCellStyle13.NullValue = Nothing
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridViewTextBoxColumn13.HeaderText = "İndirme Hızı"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.HeaderText = "Kalan Süre"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.HeaderText = "Durum"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.HeaderText = "Hata Sayısı"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.HeaderText = "Hata Sayısı"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        '
        'IndirmeListesi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Name = "IndirmeListesi"
        Me.Size = New System.Drawing.Size(839, 606)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.ParçalarListe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.İndirmelerListe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.İndirmelerListeMenü.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents TextAndImageColumn1 As Sensoy_Downloader.TextAndImageColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ımageList1 As System.Windows.Forms.ImageList
    Friend WithEvents TextAndImageColumn2 As Sensoy_Downloader.TextAndImageColumn
    Friend WithEvents TextAndImageColumn3 As Sensoy_Downloader.TextAndImageColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents İndirmelerListe As MetroFramework.Controls.MetroGrid
    Friend WithEvents DosyaAdı As Sensoy_Downloader.TextAndImageColumn
    Friend WithEvents Boyut As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents İndirilen As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents İndirmeHızı As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KalanSüre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Durum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DevamEdebilme As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Parçalar As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HataSayısı As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EklenmeTarihi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SonHataTarihi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KayıtYeri As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents İndirmeYeri As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents ParçalarListe As MetroFramework.Controls.MetroGrid
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Parçaİd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParçaBoyut As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Parçaİndirilen As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BaşlangıçBoyutu As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BitişBoyutu As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParçaİndirmeHızı As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParçaKalanSüre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParçaDurum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParçaHataSayısı As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParçaSonHataTarihi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParçaİndirmeYeri As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents BlockedProgressBar1 As Sensoy_Downloader.BlockedProgressBar
    Friend WithEvents İndirmelerListeMenü As MetroFramework.Controls.MetroContextMenu
    Friend WithEvents AçMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BirlikteAçMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KlasörüAçMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DevamEtMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DurdurMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SilMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents KuyruğaTaşıMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
