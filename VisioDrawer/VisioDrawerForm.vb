
Public Class VisioDrawerForm

    Private Sub btnGenerateDefault_Click(sender As Object, e As EventArgs) Handles btnGenerateDefault.Click
        Try
            Dim visioDrawer As VisioDrawer = New VisioDrawer()
            visioDrawer.DropShape("sample1", "sample1", 4, 7, ShapeTypes.RoundedRectangle)
            visioDrawer.DropShape("sample2", "sample2", 4, 7, ShapeTypes.Square)
            visioDrawer.DropShape("sample3", "sample3", 4, 7, ShapeTypes.Square)
            visioDrawer.DropShape("sample4", "sample4", 4, 7, ShapeTypes.Square)
            visioDrawer.DropShape("sample5", "sample5", 4, 7, ShapeTypes.Triangle)
            visioDrawer.DropShape("sample6", "sample6", 4, 7, ShapeTypes.Triangle)
            visioDrawer.DropShape("sample7", "sample7", 4, 7, ShapeTypes.Hexagon)

            Dim Sample1 = visioDrawer.GetShapeByName("sample1")
            Dim Sample2 = visioDrawer.GetShapeByName("sample2")
            Dim Sample3 = visioDrawer.GetShapeByName("sample3")
            Dim Sample4 = visioDrawer.GetShapeByName("sample4")
            Dim Sample5 = visioDrawer.GetShapeByName("sample5")
            Dim Sample6 = visioDrawer.GetShapeByName("sample6")
            Dim Sample7 = visioDrawer.GetShapeByName("sample7")

            visioDrawer.ConnectShapes(Sample1, Sample2, Microsoft.Office.Interop.Visio.VisAutoConnectDir.visAutoConnectDirDown)
            visioDrawer.ConnectShapes(Sample1, Sample3, Microsoft.Office.Interop.Visio.VisAutoConnectDir.visAutoConnectDirDown)
            visioDrawer.ConnectShapes(Sample1, Sample4, Microsoft.Office.Interop.Visio.VisAutoConnectDir.visAutoConnectDirDown)
            visioDrawer.ConnectShapes(Sample3, Sample5, Microsoft.Office.Interop.Visio.VisAutoConnectDir.visAutoConnectDirDown)
            visioDrawer.ConnectShapes(Sample3, Sample6, Microsoft.Office.Interop.Visio.VisAutoConnectDir.visAutoConnectDirDown)
            visioDrawer.ConnectShapes(Sample4, Sample7, Microsoft.Office.Interop.Visio.VisAutoConnectDir.visAutoConnectDirDown)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        Try
            Dim openFileName = GetOpenFileDialogFileName("Visio Files (*.vsd; *.vsdx)|*.vsd;*.vsdx")
            If openFileName = "" Then Return
            Dim saveFileName = GetSaveFileDialogFileName("XML Files (*.xml)|*.xml")
            If saveFileName = "" Then Return

            Using visioDrawer As VisioDrawer = New VisioDrawer(openFileName)
                visioDrawer.Hide()
                Dim visioSerializer As VisioSerializer = New VisioSerializer(visioDrawer)
                Dim serializedXml = visioSerializer.Serialize()

                My.Computer.FileSystem.WriteAllText(saveFileName, serializedXml, False)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnReadXml_Click(sender As Object, e As EventArgs) Handles btnReadXml.Click
        Try
            Dim fileName = GetOpenFileDialogFileName("XML Files (*.xml)|*.xml")
            If fileName = "" Then Return

            Dim visioDeserializer As VisioDeserializer = New VisioDeserializer(fileName)
            visioDeserializer.Deserialize()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Shared Function GetOpenFileDialogFileName(ByVal filter As String) As String
        Using ofd As New OpenFileDialog
            ofd.InitialDirectory = System.Windows.Forms.Application.ExecutablePath
            ofd.Filter = filter
            ofd.FilterIndex = 1
            ofd.RestoreDirectory = True
            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Return ofd.FileName
            End If
        End Using

        Return ""
    End Function
    Private Shared Function GetSaveFileDialogFileName(ByVal filter As String) As String
        Using sfd As New SaveFileDialog
            sfd.Filter = "XML Files (*.xml)|*.xml"
            sfd.FilterIndex = 1
            sfd.RestoreDirectory = True
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Return sfd.FileName
            End If
        End Using

        Return ""
    End Function
End Class