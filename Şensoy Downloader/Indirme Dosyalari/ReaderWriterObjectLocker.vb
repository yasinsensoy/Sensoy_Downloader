Imports System.Collections.Generic
Imports System.Text
Imports System.Threading

Public Class ReaderWriterObjectLocker
    Private Class BaseReleaser
        Protected locker As ReaderWriterObjectLocker

        Public Sub New(locker As ReaderWriterObjectLocker)
            Me.locker = locker
        End Sub
    End Class

    Private Class ReaderReleaser
        Inherits BaseReleaser
        Implements IDisposable

        Public Sub New(locker As ReaderWriterObjectLocker)
            MyBase.New(locker)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            locker.locker.ReleaseReaderLock()
        End Sub
    End Class

    Private Class WriterReleaser
        Inherits BaseReleaser
        Implements IDisposable

        Public Sub New(locker As ReaderWriterObjectLocker)
            MyBase.New(locker)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            locker.locker.ReleaseWriterLock()
        End Sub
    End Class

#Region "Fields"
    Private locker As ReaderWriterLock
    Private _writerReleaser As IDisposable
    Private _readerReleaser As IDisposable
#End Region

#Region "Constructor"
    Public Sub New()
        ' TODO: update to ReaderWriterLockSlim on .net 3.5
        locker = New ReaderWriterLock()
        _writerReleaser = New WriterReleaser(Me)
        _readerReleaser = New ReaderReleaser(Me)
    End Sub
#End Region

#Region "Methods"
    Public Function LockForRead() As IDisposable
        locker.AcquireReaderLock(-1)
        Return _readerReleaser
    End Function

    Public Function LockForWrite() As IDisposable
        locker.AcquireWriterLock(-1)
        Return _writerReleaser
    End Function
#End Region
End Class
