Public Interface IVisioDrawer
    Inherits IDrawer
    Sub Hide()
    Sub DropShape(ByVal shapeName As String, ByVal shapeText As String, ByVal x As Double, ByVal y As Double, ByVal shapeType As ShapeTypes)
    Sub ConnectShapes(ByVal fromShape As Microsoft.Office.Interop.Visio.Shape, ByVal toShape As Microsoft.Office.Interop.Visio.Shape, ByVal visAutoConnectDir As Microsoft.Office.Interop.Visio.VisAutoConnectDir)
    Function GetShapeById(Id As Integer) As Microsoft.Office.Interop.Visio.Shape
    Function GetShapeByName(ByVal shapeName As String) As Microsoft.Office.Interop.Visio.Shape
    Function GetActivePageShapes() As Microsoft.Office.Interop.Visio.Shapes
End Interface

Public Class VisioDrawer
    Implements IVisioDrawer, IDisposable

    ReadOnly VisioApp As Microsoft.Office.Interop.Visio.Application
    ReadOnly MastersDocuments As Microsoft.Office.Interop.Visio.Documents
    ReadOnly MasterDoc As Microsoft.Office.Interop.Visio.Document
    ReadOnly Masters As Microsoft.Office.Interop.Visio.Masters
    ReadOnly ActiveDocument As Microsoft.Office.Interop.Visio.Document
    ReadOnly ActivePage As Microsoft.Office.Interop.Visio.Page

    Public Sub New()
        VisioApp = New Microsoft.Office.Interop.Visio.Application
        VisioApp.Documents.Add("")
        MastersDocuments = VisioApp.Documents
        MasterDoc = MastersDocuments.OpenEx("Basic_U.vss", Microsoft.Office.Interop.Visio.VisOpenSaveArgs.visOpenDocked)
        Masters = MasterDoc.Masters
        ActiveDocument = VisioApp.ActiveDocument
        ActivePage = ActiveDocument.Pages.Add()
    End Sub
    Public Sub New(ByVal visioFilePath As String)
        VisioApp = New Microsoft.Office.Interop.Visio.Application
        VisioApp.Documents.Add(visioFilePath)
        MastersDocuments = VisioApp.Documents
        MasterDoc = MastersDocuments.OpenEx("Basic_U.vss", Microsoft.Office.Interop.Visio.VisOpenSaveArgs.visOpenDocked)
        Masters = MasterDoc.Masters
        ActiveDocument = VisioApp.ActiveDocument
        ActivePage = VisioApp.ActivePage
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ActiveDocument.Close()
        VisioApp.Quit()
    End Sub

    Public Sub Hide() Implements IVisioDrawer.Hide
        VisioApp.Visible = False
    End Sub

    Public Sub DropShape(ByVal shapeName As String, ByVal shapeText As String, ByVal x As Double, ByVal y As Double, ByVal shapeType As ShapeTypes) Implements IVisioDrawer.DropShape
        Dim shapeTypeAttribute As System.ComponentModel.DescriptionAttribute = _
            shapeType.GetAttribute(Of System.ComponentModel.DescriptionAttribute)()

        Dim objectToDrop As Microsoft.Office.Interop.Visio.Master = GetMaster(shapeTypeAttribute.Description)
        Dim shape As Microsoft.Office.Interop.Visio.Shape = ActivePage.Drop(objectToDrop, x, y)
        shape.Name = shapeName
        shape.Text = shapeText
    End Sub
    Public Sub ConnectShapes(ByVal fromShape As Microsoft.Office.Interop.Visio.Shape, ByVal toShape As Microsoft.Office.Interop.Visio.Shape,
                             ByVal visAutoConnectDir As Microsoft.Office.Interop.Visio.VisAutoConnectDir) Implements IVisioDrawer.ConnectShapes
        fromShape.AutoConnect(toShape, visAutoConnectDir)
    End Sub

    Public Function GetShapeById(ByVal Id As Integer) As Microsoft.Office.Interop.Visio.Shape Implements IVisioDrawer.GetShapeById
        For Each shape As Microsoft.Office.Interop.Visio.Shape In ActivePage.Shapes
            If Id = shape.ID Then Return shape
        Next
        Throw New ArgumentException("Not found.", "Id")
    End Function
    Public Function GetShapeByName(ByVal shapeName As String) As Microsoft.Office.Interop.Visio.Shape Implements IVisioDrawer.GetShapeByName
        For Each shape As Microsoft.Office.Interop.Visio.Shape In ActivePage.Shapes
            If shapeName = shape.Name Then Return shape
        Next
        Throw New ArgumentException("Not found.", "shapeName")
    End Function

    Public Function GetActivePageShapes() As Microsoft.Office.Interop.Visio.Shapes Implements IVisioDrawer.GetActivePageShapes
        Return ActivePage.Shapes
    End Function

    Private Function GetMaster(ByVal mastername As String) As Microsoft.Office.Interop.Visio.Master
        Return Masters.ItemU(mastername)
    End Function
    
End Class