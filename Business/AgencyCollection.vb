Public Class AgencyCollection
    Inherits ArrayList

    Public Shadows Sub Add(ByVal rt As Agency)
        MyBase.Add(rt)
    End Sub

    Public Shadows Function Item(ByVal index As Integer) As Agency
        Return DirectCast(MyBase.Item(index), Agency)
    End Function

End Class
