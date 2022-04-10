Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms

Public Class HttpFtpProtocolUIExtension
    Implements IUIExtension

    Public Function CreateSettingsView() As System.Windows.Forms.Control() Implements IUIExtension.CreateSettingsView
        Return New Control() {New Proxy()}
    End Function

    Public Sub PersistSettings(settingsView As System.Windows.Forms.Control()) Implements IUIExtension.PersistSettings
        Dim proxy As Proxy = DirectCast(settingsView(0), Proxy)
        Protocols.Default.UseProxy = proxy.UseProxy
        Protocols.Default.ProxyAddress = proxy.ProxyAddress
        Protocols.Default.ProxyPort = proxy.ProxyPort
        Protocols.Default.ProxyByPassOnLocal = proxy.ProxyByPassOnLocal
        Protocols.Default.ProxyUserName = proxy.ProxyUserName
        Protocols.Default.ProxyPassword = proxy.ProxyPassword
        Protocols.Default.ProxyDomain = proxy.ProxyDomain
        Protocols.Default.Save()
    End Sub
End Class
