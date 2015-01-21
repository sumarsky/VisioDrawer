Imports System.Xml.Linq

Public Class XmlConnector
    Implements IToXml

    Public Property FromShape() As String
    Public Property ToShape() As String

    Public Sub New(ByVal fromShape As String, ByVal toShape As String)
        Me.FromShape = fromShape
        Me.ToShape = toShape
    End Sub

    Public Function ToXml() As String Implements IToXml.ToXml
        Dim xml As XElement = <Connector>
                                  <FromShape><%= FromShape %></FromShape>
                                  <ToShape><%= ToShape %></ToShape>
                              </Connector>
        Return xml.ToString()
    End Function
End Class