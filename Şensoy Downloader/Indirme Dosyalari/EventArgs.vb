#Region "ResolvingProtocolProviderEventArgs"
Public Class ResolvingProtocolProviderEventArgs
    Inherits EventArgs
#Region "Fields"

    Private provider As IProtocolProvider
    Private m_url As String

#End Region

#Region "Constructor"

    Public Sub New(provider As IProtocolProvider, url As String)
        m_url = url
        Me.provider = provider
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property URL() As String
        Get
            Return m_url
        End Get
    End Property

    Public Property ProtocolProvider() As IProtocolProvider
        Get
            Return provider
        End Get
        Set(value As IProtocolProvider)
            provider = Value
        End Set
    End Property

#End Region
End Class
#End Region

#Region "DownloaderEventArgs"
Public Class DownloaderEventArgs
    Inherits EventArgs
#Region "Fields"

    Private m_downloader As Indirme
    Private m_willStart As Boolean

#End Region

#Region "Constructor"

    Public Sub New(download As Indirme)
        m_downloader = download
    End Sub

    Public Sub New(download As Indirme, willStart As Boolean)
        Me.New(download)
        m_willStart = willStart
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property Downloader() As Indirme
        Get
            Return m_downloader
        End Get
    End Property

    Public ReadOnly Property WillStart() As Boolean
        Get
            Return m_willStart
        End Get
    End Property

#End Region
End Class
#End Region

#Region "SegmentEventArgs"
Public Class SegmentEventArgs
    Inherits DownloaderEventArgs
#Region "Fields"

    Private m_segment As Parca

#End Region

#Region "Constructor"

    Public Sub New(d As Indirme, segment As Parca)
        MyBase.New(d)
        m_segment = segment
    End Sub

#End Region

#Region "Properties"

    Public Property Segment() As Parca
        Get
            Return m_segment
        End Get
        Set(value As Parca)
            m_segment = Value
        End Set
    End Property

#End Region
End Class
#End Region
