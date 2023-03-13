Public Class Agency

#Region " Variables "

    Private m_szAgencyID As String
    Private m_szSortName As String
    Private m_nSortID As Integer

#End Region

#Region " Properties "

    Public Property SortID() As Integer
        Get
            Return m_nSortID
        End Get
        Set(ByVal Value As Integer)
            m_nSortID = Value
        End Set
    End Property

    Public Property AgencyID() As String
        Get
            Return m_szAgencyID
        End Get
        Set(ByVal Value As String)
            m_szAgencyID = Value
        End Set
    End Property

    Public Property SortName() As String
        Get
            Return m_szSortName
        End Get
        Set(ByVal Value As String)
            m_szSortName = Value
        End Set
    End Property

#End Region

End Class
