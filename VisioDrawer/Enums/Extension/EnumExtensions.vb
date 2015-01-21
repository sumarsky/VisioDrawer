Imports System.Linq

Public Module EnumExtensions

    <Runtime.CompilerServices.Extension()>
    Public Function GetAttribute(Of T As Attribute)(enumValue As [Enum]) As T
        Dim attribute As T

        Dim memberInfo As Reflection.MemberInfo = enumValue.[GetType]().GetMember(enumValue.ToString()).FirstOrDefault()

        If memberInfo IsNot Nothing Then
            attribute = DirectCast(memberInfo.GetCustomAttributes(GetType(T), False).FirstOrDefault(), T)
            Return attribute
        End If
        Throw New ArgumentException("Not found.", "enumValue")
    End Function

    <Runtime.CompilerServices.Extension()>
    Public Function GetValueFromDescription(Of T)(ByVal description As String) As T
        Dim type = GetType(T)
        If Not type.IsEnum Then Throw New InvalidOperationException()
        For Each field As Reflection.FieldInfo In type.GetFields()
            Dim attribute As System.ComponentModel.DescriptionAttribute = System.Attribute.GetCustomAttribute(field, GetType(System.ComponentModel.DescriptionAttribute))
            If attribute IsNot Nothing Then
                If attribute.Description = description Then
                    Return DirectCast(field.GetValue(Nothing), T)
                Else
                    If field.Name = description Then
                        Return DirectCast(field.GetValue(Nothing), T)
                    End If
                End If
            End If
        Next
        Throw New ArgumentException("Not found.", "description")
    End Function

End Module