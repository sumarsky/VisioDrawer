Imports System.Linq
Imports System.Reflection

Module DataTableExtensions

    <Runtime.CompilerServices.Extension()>
    Public Function ToList(Of T As New)(ByVal dataTable As DataTable) As IList(Of T)
        Dim properties = GetType(T).GetProperties().ToList()

        Return (From dataRow In dataTable.Select()
                Select CreateItemFromRow(Of T)(dataRow, properties)).ToList()
    End Function
    Private Function CreateItemFromRow(Of T As New)(ByVal dataRow As DataRow, ByVal properties As IList(Of PropertyInfo)) As T
        Dim item As T = New T()
        For Each prop As PropertyInfo In properties
            prop.SetValue(item,
                          If(IsNothing(dataRow(prop.Name)) Or IsDBNull(dataRow(prop.Name)),
                            Nothing,
                            dataRow(prop.Name)),
                          Nothing)
        Next
        Return item
    End Function

End Module