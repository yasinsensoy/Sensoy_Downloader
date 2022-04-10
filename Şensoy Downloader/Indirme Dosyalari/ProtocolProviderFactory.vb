Imports System.Collections.Generic
Imports System.Text
Imports System.Collections

Public NotInheritable Class ProtocolProviderFactory
    Private Shared protocolHandlers As New Hashtable()
    Public Shared Event ResolvingProtocolProvider As EventHandler(Of ResolvingProtocolProviderEventArgs)

    Public Shared Sub RegisterProtocolHandler(prefix As String, protocolProvider As Type)
        protocolHandlers(prefix) = protocolProvider
    End Sub

    Public Shared Function CreateProvider(uri As String, downloader As Indirme) As IProtocolProvider
        Dim provider As IProtocolProvider = InternalGetProvider(uri)
        If downloader IsNot Nothing Then
            provider.Initialize(downloader)
        End If
        Return provider
    End Function

    Public Shared Function GetProvider(uri As String) As IProtocolProvider
        Return InternalGetProvider(uri)
    End Function

    Public Shared Function GetProviderType(uri As String) As Type
        Dim index As Integer = uri.IndexOf("://")
        If index > 0 Then
            Dim prefix As String = uri.Substring(0, index)
            Dim type As Type = TryCast(protocolHandlers(prefix), Type)
            Return type
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function CreateProvider(providerType As Type, downloader As Indirme) As IProtocolProvider
        Dim provider As IProtocolProvider = CreateFromType(providerType)
        Dim e As New ResolvingProtocolProviderEventArgs(provider, Nothing)
        RaiseEvent ResolvingProtocolProvider(Nothing, e)
        provider = e.ProtocolProvider
        If downloader IsNot Nothing Then
            provider.Initialize(downloader)
        End If
        Return provider
    End Function

    Private Shared Function InternalGetProvider(uri As String) As IProtocolProvider
        Dim type As Type = GetProviderType(uri)
        Dim provider As IProtocolProvider = CreateFromType(type)
        Dim e As New ResolvingProtocolProviderEventArgs(provider, uri)
        RaiseEvent ResolvingProtocolProvider(Nothing, e)
        provider = e.ProtocolProvider
        Return provider
    End Function

    Private Shared Function CreateFromType(type As Type) As IProtocolProvider
        Dim provider As IProtocolProvider = DirectCast(Activator.CreateInstance(type), IProtocolProvider)
        Return provider
    End Function
End Class