Imports System.Xml.Linq

Public Class XmlShape
    Implements IToXml

    Public Property ID() As Integer
    Public Property Type() As String
    Public Property Name() As String
    Public Property Text() As String
    Public Property PinX() As Double
    Public Property PinY() As Double

    Public Sub New(ByVal id As Integer, ByVal type As String, ByVal name As String, ByVal text As String, ByVal pinX As Double, ByVal pinY As Double)
        Me.ID = id
        Me.Type = type
        Me.Name = name
        Me.Text = text
        Me.PinX = pinX
        Me.PinY = pinY
    End Sub

    Public Function ToXml() As String Implements IToXml.ToXml
        Dim xml As XElement = <Shape>
                                  <ID><%= ID %></ID>
                                  <Type><%= Type %></Type>
                                  <Name><%= Name %></Name>
                                  <Text><%= Text %></Text>
                                  <PinX><%= PinX %></PinX>
                                  <PinY><%= PinY %></PinY>
                              </Shape>
        Return xml.ToString()
    End Function
End Class