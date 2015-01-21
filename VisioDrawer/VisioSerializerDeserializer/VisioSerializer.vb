Imports System.Linq

Public Interface IVisioSerializer
    Function Serialize() As String
End Interface
Public Class VisioSerializer
    Implements IVisioSerializer

    ReadOnly visioDrawer As IVisioDrawer

    Public Sub New(ByVal visioDrawer As VisioDrawer)
        Me.visioDrawer = visioDrawer
    End Sub

    Public Function Serialize() As String Implements IVisioSerializer.Serialize
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("<Root>")
        For Each shape As Microsoft.Office.Interop.Visio.Shape In visioDrawer.GetActivePageShapes()
            Dim shapeType As ShapeTypes = shape.Master.NameU.GetValueFromDescription(Of ShapeTypes)()
            If shapeType <> ShapeTypes.DynamicConnector Then
                sb.AppendLine(New XmlShape(shape.ID,
                                           shape.Master.NameU,
                                           shape.Name,
                                           shape.Text,
                                           shape.Cells("PinX").ResultIU,
                                           shape.Cells("PinY").ResultIU).ToXml())

                For Each referencedShapeId As Integer In shape.ConnectedShapes(Microsoft.Office.Interop.Visio.VisConnectedShapesFlags.visConnectedShapesOutgoingNodes, "").Cast(Of Integer)()
                    sb.AppendLine(New XmlConnector(visioDrawer.GetShapeById(shape.ID).Name,
                                                   visioDrawer.GetShapeById(referencedShapeId).Name).ToXml())
                Next
            End If
        Next
        sb.AppendLine("</Root>")
        Return sb.ToString()
    End Function
End Class