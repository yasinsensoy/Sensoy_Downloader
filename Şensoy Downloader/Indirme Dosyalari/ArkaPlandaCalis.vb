Imports System.Threading

Public Class ArkaPlandaCalis
    Implements IDisposable

    Public Event Calis(işlenecekparça As Parca, arkaplan As ArkaPlandaCalis)
    Public Event CalismaBitti(arkaplan As ArkaPlandaCalis)
    Public Event BilgiGuncelle()
    Dim calismaaraci As Thread
    Dim _islenecekparca As Parca

    Public Sub New(işlenecekparça As Parca, başlat As Boolean)
        _islenecekparca = işlenecekparça
        If başlat = True Then CalismayiBaslat()
    End Sub

    Public Sub Guncelle()
        RaiseEvent BilgiGuncelle()
    End Sub

    Public Sub CalismayiBaslat()
        SyncLock "kitle"
            Dim Thread As Object = calismaaraci
            If Thread Is Nothing Then
                calismaaraci = New Thread(New ThreadStart(AddressOf _Calis))
            ElseIf calismaaraci.ThreadState <> ThreadState.Background Then
                calismaaraci = New Thread(New ThreadStart(AddressOf _Calis))
            End If
            If calismaaraci.IsAlive = False Then
                calismaaraci.SetApartmentState(ApartmentState.MTA)
                calismaaraci.IsBackground = True
                calismaaraci.Start()
            End If
        End SyncLock
    End Sub

    Private Sub _Calis()
        Try
            RaiseEvent Calis(_islenecekparca, Me)
            RaiseEvent CalismaBitti(Me)
        Catch generatedExceptionName As ThreadAbortException
            RaiseEvent CalismaBitti(Me)
        End Try
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                calismaaraci.Abort()
                calismaaraci = Nothing
            End If
        End If
        Me.disposedValue = True
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class


