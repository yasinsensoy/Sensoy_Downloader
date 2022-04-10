Imports System.Collections.Generic
Imports System.Text
Imports System.Diagnostics

Public Class MyStopwatch
    Implements IDisposable

    Private internalStopwatch As Stopwatch
    Private name As String
    Private disposedValue As Boolean

    Public Sub New(name As String)
#If DEBUG Then
        Me.name = name
        internalStopwatch = New Stopwatch()
#End If
        internalStopwatch.Start()
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
#If DEBUG Then
                internalStopwatch.[Stop]()
                Console.WriteLine((name & ": " & internalStopwatch.Elapsed.ToString))
#End If
            End If
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
