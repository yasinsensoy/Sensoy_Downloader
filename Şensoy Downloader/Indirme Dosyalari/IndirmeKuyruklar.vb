Imports System.Xml.Serialization

<Serializable> Public Class IndirmeKuyruklar
    <NonSerialized> Private downloadListSync As New ReaderWriterObjectLocker()
    <XmlAttribute("ik_ka")> Public Property Adi() As String
    Public Property Indirmeler() As List(Of Indirme)

    Public Function LockDownloadList(lockForWrite As Boolean) As IDisposable
        If lockForWrite Then
            Return downloadListSync.LockForWrite()
        Else
            Return downloadListSync.LockForRead()
        End If
    End Function

    Public Sub New()
    End Sub

    Public Sub New(downloader As Indirme, kuyrukadi As String)
        If IsNothing(Indirmeler) = True Then Indirmeler = New List(Of Indirme)
        Using LockDownloadList(True)
            Indirmeler.Add(downloader)
        End Using
        Adi = kuyrukadi
    End Sub
End Class
