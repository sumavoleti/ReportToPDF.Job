Public Class ReportPeriod

#Region " Variables "

    Private m_nReportPeriodID As Byte
    Private m_szDescription As String
    Private m_szPeriodChar As String
    Private m_nRententionDaysMax As Integer

#End Region

#Region " Properties "

    Public Property ReportPeriodID() As Byte
        Get
            Return m_nReportPeriodID
        End Get
        Set(ByVal Value As Byte)
            m_nReportPeriodID = Value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return m_szDescription
        End Get
        Set(ByVal Value As String)
            m_szDescription = Value
        End Set
    End Property

    Public Property PeriodChar() As String
        Get
            Return m_szPeriodChar
        End Get
        Set(ByVal Value As String)
            m_szPeriodChar = Value
        End Set
    End Property

    Public Property RententionDaysMax() As Integer
        Get
            Return m_nRententionDaysMax
        End Get
        Set(ByVal Value As Integer)
            m_nRententionDaysMax = Value
        End Set
    End Property

#End Region

End Class
