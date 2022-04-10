
Imports System.Collections.Generic
Imports System.Text

Public Interface IExtension
    ReadOnly Property Name() As String

    ReadOnly Property UIExtension() As IUIExtension
End Interface
