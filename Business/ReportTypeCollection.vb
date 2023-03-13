Public Class ReportTypeCollection
    Inherits ArrayList

    Public Shadows Sub Add(ByVal rt As ReportType)
        MyBase.Add(rt)
    End Sub

    Public Shadows Function Item(ByVal index As Integer) As ReportType
        Return DirectCast(MyBase.Item(index), ReportType)
    End Function

    Public Function ItemByPrefix(ByVal reportPrefix As String) As ReportType

        For Each rt As ReportType In Me
            If rt.FilePrefix = reportPrefix.ToUpper Then
                Return rt
            End If
        Next

        Return Nothing

    End Function

End Class
