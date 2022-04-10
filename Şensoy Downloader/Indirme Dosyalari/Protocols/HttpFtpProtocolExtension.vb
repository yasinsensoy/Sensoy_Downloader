Imports System.Collections.Generic
Imports System.Text
Imports System.IO

Public Class HttpFtpProtocolExtension
    Implements IExtension

    Friend Shared parameters As IHttpFtpProtocolParameters

    Public ReadOnly Property Name() As String Implements IExtension.Name
        Get
            Return "HTTP/FTP"
        End Get
    End Property

    Public ReadOnly Property UIExtension() As IUIExtension Implements IExtension.UIExtension
        Get
            Return New HttpFtpProtocolUIExtension()
        End Get
    End Property

    Public Sub New()
        Me.New(New HttpFtpProtocolParametersSettingsProxy())
    End Sub

    Public Sub New(parameters As IHttpFtpProtocolParameters)
        If parameters Is Nothing Then
            Throw New ArgumentNullException("parameters")
        End If
        If HttpFtpProtocolExtension.parameters IsNot Nothing Then
            Throw New InvalidOperationException("The type HttpFtpProtocolExtension is already initialized.")
        End If
        HttpFtpProtocolExtension.parameters = parameters
        ProtocolProviderFactory.RegisterProtocolHandler("http", GetType(HttpProtocolProvider))
        ProtocolProviderFactory.RegisterProtocolHandler("https", GetType(HttpProtocolProvider))
        ProtocolProviderFactory.RegisterProtocolHandler("ftp", GetType(FtpProtocolProvider))
    End Sub
End Class
