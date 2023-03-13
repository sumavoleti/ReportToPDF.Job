Public Class ReportPeriodCollection
    Inherits ArrayList

    Public Shadows Sub Add(ByVal rt As ReportPeriod)
        MyBase.Add(rt)
    End Sub

    Public Shadows Function Item(ByVal index As Integer) As ReportPeriod
        Return DirectCast(MyBase.Item(index), ReportPeriod)
    End Function

    Public Function ItemByPeriodChar(ByVal periodChar As String) As ReportPeriod

        For Each rt As ReportPeriod In Me
            If rt.PeriodChar = periodChar.ToUpper Then
                Return rt
            End If
        Next

        Return Nothing

    End Function

End Class
