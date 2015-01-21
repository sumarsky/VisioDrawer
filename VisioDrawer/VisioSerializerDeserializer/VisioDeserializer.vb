Imports System.Xml.Linq
Imports System.Linq

Public Interface IVisioDeserializer
    Sub Deserialize()
End Interface

Public Class VisioDeserializer
    Implements IVisioDeserializer

    ReadOnly path As String
    ReadOnly visioDrawer As IVisioDrawer

    Public Sub New(ByVal path As String)
        Me.path = path
        visioDrawer = New VisioDrawer()
    End Sub

    Public Sub Deserialize() Implements IVisioDeserializer.Deserialize
        Dim xml = XDocument.Load(path)
        Dim listShape = xml.Root.Elements("Shape").Select(Function(x) New XmlShape(x.Element("ID"),
                                                                                   x.Element("Type"),
                                                                                   x.Element("Name"),
                                                                                   x.Element("Text"),
                                                                                   x.Element("PinX"),
                                                                                   x.Element("PinY")))
        Dim listConnector = xml.Root.Elements("Connector").Select(Function(x) New XmlConnector(x.Element("FromShape"),
                                                                                               x.Element("ToShape")))
        DrawShapes(listShape)
        DrawConnectors(listConnector)
    End Sub

    Private Sub DrawShapes(ByVal listShape As IEnumerable(Of XmlShape))
        For Each xmlShape As XmlShape In listShape
            Dim shapeType As ShapeTypes = xmlShape.Type.GetValueFromDescription(Of ShapeTypes)()
            visioDrawer.DropShape(xmlShape.Name, xmlShape.Text, xmlShape.PinX, xmlShape.PinY, shapeType)
        Next
    End Sub
    Private Sub DrawConnectors(ByVal listConnector As IEnumerable(Of XmlConnector))
        For Each xmlConnector As XmlConnector In listConnector
            visioDrawer.ConnectShapes(visioDrawer.GetShapeByName(xmlConnector.FromShape),
                                      visioDrawer.GetShapeByName(xmlConnector.ToShape),
                                      Microsoft.Office.Interop.Visio.VisAutoConnectDir.visAutoConnectDirDown)
        Next
    End Sub
    
End Class