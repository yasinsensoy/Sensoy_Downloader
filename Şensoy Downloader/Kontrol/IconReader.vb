Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices

Public Class IconReader

    <System.FlagsAttribute> _
    Private Enum EnumFileInfoFlags As UInteger
        ''' get large icon
        LARGEICON = &H0
        ''' get small icon
        SMALLICON = &H1
        ''' get open icon
        OPENICON = &H2
        ''' get shell size icon
        SHELLICONSIZE = &H4
        ''' pszPath is a pidl
        PIDL = &H8
        ''' use passed dwFileAttribute
        USEFILEATTRIBUTES = &H10
        ''' apply the appropriate overlays
        ADDOVERLAYS = &H20
        ''' get the index of the overlay
        OVERLAYINDEX = &H40
        ''' get icon
        ICON = &H100
        ''' get display name
        DISPLAYNAME = &H200
        ''' get type name
        TYPENAME = &H400
        ''' get attributes
        ATTRIBUTES = &H800
        ''' get icon location
        ICONLOCATION = &H1000
        ''' return exe type
        EXETYPE = &H2000
        ''' get system icon index
        SYSICONINDEX = &H4000
        ''' put a link overlay on icon
        LINKOVERLAY = &H8000
        ''' show icon in selected state
        SELECTED = &H10000
        ''' get only specified attributes
        ATTR_SPECIFIED = &H20000
    End Enum

    '''
    ''' maxumum length of path
    '''
    Private Const conMAX_PATH As Integer = 260

    '''
    ''' looking for folder
    '''
    Private Const conFILE_ATTRIBUTE_DIRECTORY As UInteger = &H10

    '''
    ''' looking for file
    '''
    Private Const conFILE_ATTRIBUTE_NORMAL As UInteger = &H80

    '''
    ''' size of the icon
    '''
    Public Enum EnumIconSize
        ''' 32x32
        Large = 0
        ''' 16x16
        Small = 1
    End Enum

    '''
    ''' state of the folder
    '''
    Public Enum EnumFolderType
        ''' open folder
        Open = 0
        ''' closed folder
        Closed = 1
    End Enum

    '''
    ''' hold file/icon information
    ''' see platformSDK SHFILEINFO
    '''
    '''
    ''' be sure to call DestroyIcon [hIcon] when done
    '''
    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Private Structure ShellFileInfo

        Public Const conNameSize As Integer = 80
        Public hIcon As System.IntPtr
        ' note to call DestroyIcon
        Public iIndex As Integer
        Public dwAttributes As UInteger

        <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=conMAX_PATH)> _
        Public szDisplayName As String

        <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=conNameSize)> _
        Public szTypeName As String
    End Structure

    '''
    ''' used to free a windows icon handle
    '''
    ''' "hIcon">icon handle.
    <System.Runtime.InteropServices.DllImport("User32.dll")> _
    Private Shared Function DestroyIcon(hIcon As System.IntPtr) As Integer
    End Function

    '''
    ''' gets file information
    ''' see platformSDK
    '''
    <DllImport("shell32.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function SHGetFileInfo(pszPath As String, dwFileAttributes As UInteger, ByRef psfi As ShellFileInfo, cbFileInfo As UInteger, uFlags As UInteger) As System.IntPtr
    End Function

    Public Shared Function GetFileIcon(filePath As String, size As EnumIconSize, addLinkOverlay As Boolean) As System.Drawing.Icon
        Dim flags As EnumFileInfoFlags = EnumFileInfoFlags.ICON

        ' add link overlay if requested
        If addLinkOverlay Then
            flags = flags Or EnumFileInfoFlags.LINKOVERLAY
        End If

        ' set size
        If size = EnumIconSize.Small Then
            flags = flags Or EnumFileInfoFlags.SMALLICON
        Else
            flags = flags Or EnumFileInfoFlags.LARGEICON
        End If

        Dim shellFileInfo As New ShellFileInfo()

        SHGetFileInfo(filePath, conFILE_ATTRIBUTE_NORMAL, shellFileInfo, CUInt(System.Runtime.InteropServices.Marshal.SizeOf(shellFileInfo)), CUInt(flags))

        ' deep copy
        Dim icon As System.Drawing.Icon = DirectCast(System.Drawing.Icon.FromHandle(shellFileInfo.hIcon).Clone(), System.Drawing.Icon)

        ' release handle
        DestroyIcon(shellFileInfo.hIcon)

        Return icon
    End Function
    '''
    ''' lookup and return an icon from windows shell
    '''
    ''' "name">path to the file
    ''' "size">large or small
    ''' "linkOverlay">true to include the overlay link iconlet
    ''' requested icon
    Public Shared Function GetFileIconByExt(fileExt As String, size As EnumIconSize, addLinkOverlay As Boolean) As System.Drawing.Icon
        Dim tempFile As String = Path.GetTempPath() + Guid.NewGuid().ToString("N") + fileExt

        Try
            File.WriteAllBytes(tempFile, New Byte(0) {0})

            Return GetFileIcon(tempFile, size, addLinkOverlay)
        Finally
            Try
                File.Delete(tempFile)
            Catch generatedExceptionName As Exception
            End Try
        End Try
    End Function

    '''
    '''  lookup and return an icon from windows shell
    '''
    ''' "size">large or small
    ''' "folderType">open or closed
    ''' requested icon
    Public Shared Function GetFolderIcon(size As EnumIconSize, folderType As EnumFolderType) As System.Drawing.Icon
        Return GetFolderIcon(Nothing, size, folderType)
    End Function

    '''
    ''' lookup and return an icon from windows shell
    '''
    ''' "folderPath">path to folder    
    ''' "size">large or small
    ''' "folderType">open or closed
    ''' requested icon
    Public Shared Function GetFolderIcon(folderPath As String, size As EnumIconSize, folderType As EnumFolderType) As System.Drawing.Icon

        Dim flags As EnumFileInfoFlags = EnumFileInfoFlags.ICON Or EnumFileInfoFlags.USEFILEATTRIBUTES

        If folderType = EnumFolderType.Open Then
            flags = flags Or EnumFileInfoFlags.OPENICON
        End If

        If EnumIconSize.Small = size Then
            flags = flags Or EnumFileInfoFlags.SMALLICON
        Else
            flags = flags Or EnumFileInfoFlags.LARGEICON
        End If

        Dim shellFileInfo As New ShellFileInfo()
        SHGetFileInfo(folderPath, conFILE_ATTRIBUTE_DIRECTORY, shellFileInfo, CUInt(System.Runtime.InteropServices.Marshal.SizeOf(shellFileInfo)), CUInt(flags))

        ' deep copy
        Dim icon As System.Drawing.Icon = DirectCast(System.Drawing.Icon.FromHandle(shellFileInfo.hIcon).Clone(), System.Drawing.Icon)
        ' release handle
        DestroyIcon(shellFileInfo.hIcon)
        Return icon
    End Function
End Class
