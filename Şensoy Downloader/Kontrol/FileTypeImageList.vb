
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing

Public Class FileTypeImageList
    Private Const OpenFolderKey As String = "OpenFolderKey"
    Private Const CloseFolderKey As String = "OpenFolderKey"
    Private Shared instance As ImageList

    Public Shared Function GetSharedInstance() As ImageList
        If instance Is Nothing Then
            instance = New ImageList()
            instance.TransparentColor = Color.Black
            instance.TransparentColor = Color.Transparent
            instance.ColorDepth = ColorDepth.Depth32Bit
            instance.ImageSize = New Size(16, 16)
        End If
        Return instance
    End Function

    Public Shared Function GetImageIndexByExtention(ext As String) As Integer
        GetSharedInstance()
        ext = ext.ToLower()
        If Not instance.Images.ContainsKey(ext) Then
            Dim iconForFile As Icon = IconReader.GetFileIconByExt(ext, IconReader.EnumIconSize.Small, False)
            ext = If(ext = "", "boþ", ext)
            instance.Images.Add(ext, iconForFile)
        End If
        Return instance.Images.IndexOfKey(ext)
    End Function

    Public Shared Function GetImageIndexFromFolder(open As Boolean) As Integer
        Dim key As String
        GetSharedInstance()
        If open Then
            key = OpenFolderKey
        Else
            key = CloseFolderKey
        End If
        If Not instance.Images.ContainsKey(key) Then
            Dim iconForFile As Icon = IconReader.GetFolderIcon(IconReader.EnumIconSize.Small, (If(open, IconReader.EnumFolderType.Open, IconReader.EnumFolderType.Closed)))
            instance.Images.Add(key, iconForFile)
        End If
        Return instance.Images.IndexOfKey(key)
    End Function
End Class
