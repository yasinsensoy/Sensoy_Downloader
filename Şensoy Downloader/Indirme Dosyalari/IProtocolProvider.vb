Imports System.IO
Imports System.Net

Public Interface IProtocolProvider
    Sub Initialize(downloader As Indirme)

    Function CreateStream(ByRef rl As IndirmeAdresi, initialPosition As Long, endPosition As Long, Optional cookies As CookieCollection = Nothing, Optional ByRef header As WebHeaderCollection = Nothing) As BinaryReader

    Function GetFileInfo(ByRef rl As IndirmeAdresi, ByRef stream As BinaryReader, Optional cookies As CookieCollection = Nothing, Optional ByRef header As WebHeaderCollection = Nothing) As SunucuDosyaBilgisi

    Function GetFileSize(ByRef rl As IndirmeAdresi, Optional cookies As CookieCollection = Nothing, Optional ByRef header As WebHeaderCollection = Nothing) As Long
End Interface
