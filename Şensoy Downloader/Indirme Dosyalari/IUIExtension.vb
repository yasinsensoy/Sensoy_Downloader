Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms

Public Interface IUIExtension
    Function CreateSettingsView() As Control()

    Sub PersistSettings(settingsView As Control())
End Interface
