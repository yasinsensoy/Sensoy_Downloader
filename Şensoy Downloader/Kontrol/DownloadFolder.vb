Imports System.IO
Imports Microsoft.WindowsAPICodePack.Dialogs

Partial Public Class DownloadFolder
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
        Text = "Directory"
        txtSaveTo.Text = If(IndirmeAyar.Default.DownloadFolder = "", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), IndirmeAyar.Default.DownloadFolder)
    End Sub

    Public Property LabelText() As String
        Get
            Return lblText.Text
        End Get
        Set(value As String)
            lblText.Text = value
        End Set
    End Property

    Public ReadOnly Property Folder() As String
        Get
            Return txtSaveTo.Text
        End Get
    End Property

    Private Sub btnSelAV_Click(sender As Object, e As EventArgs) Handles btnSelAV.Click
        Dim folderBrowser As CommonOpenFileDialog = New CommonOpenFileDialog()
        folderBrowser.IsFolderPicker = True
        folderBrowser.InitialDirectory = txtSaveTo.Text
        If folderBrowser.ShowDialog() = DialogResult.OK Then
            txtSaveTo.Text = folderBrowser.FileName
            IndirmeAyar.Default.DownloadFolder = folderBrowser.FileName
            IndirmeAyar.Default.Save()
        End If
    End Sub
End Class