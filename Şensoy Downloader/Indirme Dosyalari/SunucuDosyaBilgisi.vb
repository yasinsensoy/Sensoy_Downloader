Imports System.Collections.Generic
Imports System.Text
Imports System.Xml.Serialization

<Serializable> Public Class SunucuDosyaBilgisi
    <XmlAttribute("sdb_mt")> Public Property MimeType() As String
    <XmlAttribute("sdb_de")> Public Property DevamEdebilme() As Boolean
    <XmlAttribute("sdb_db")> Public Property DosyaBoyutu() As Long
    <XmlAttribute("sdb_sdt")> Public Property SonDuzenlemeTarihi() As DateTime
End Class