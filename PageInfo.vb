Public Class PageInfo

    Private m_nFromPage As Integer
    Private m_nToPage As Integer

    Public Property FromPage() As Integer
        Get
            Return m_nFromPage
        End Get
        Set(ByVal Value As Integer)
            m_nFromPage = Value
        End Set
    End Property

    Public Property ToPage() As Integer
        Get
            Return m_nToPage
        End Get
        Set(ByVal Value As Integer)
            m_nToPage = Value
        End Set
    End Property

    Public Sub New()
        Me.ToPage = -1
        Me.FromPage = -1
    End Sub

End Class
