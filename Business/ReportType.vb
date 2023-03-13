Public Class ReportType

#Region " Variables "

    Private m_nReportTypeID As Integer
    Private m_byReportPeriodID As Byte
    Private m_szFilePrefix As String
    Private m_szSearchVerbiage As String
    Private m_szDescription As String
    Private m_szDisplayName As String
    Private m_nDisplayOrder As Integer
    Private m_szHttpLocation As String
    Private m_szHttpOnlineURL As String
    Private m_szFolderLocation As String
    Private m_byContactType As Byte

#End Region

#Region " Properties "

    Public Property HttpLocation() As String
        Get
            Return m_szHttpLocation
        End Get
        Set(ByVal Value As String)
            m_szHttpLocation = Value
        End Set
    End Property

    Public Property HttpOnlineURL() As String
        Get
            Return m_szHttpOnlineURL
        End Get
        Set(ByVal Value As String)
            m_szHttpOnlineURL = Value
        End Set
    End Property

    Public Property ReportPeriodID() As Byte
        Get
            Return m_byReportPeriodID
        End Get
        Set(ByVal Value As Byte)
            m_byReportPeriodID = Value
        End Set
    End Property

    Public Property ReportTypeID() As Integer
        Get
            Return m_nReportTypeID
        End Get
        Set(ByVal Value As Integer)
            m_nReportTypeID = Value
        End Set
    End Property

    Public Property DisplayOrder() As Integer
        Get
            Return m_nDisplayOrder
        End Get
        Set(ByVal Value As Integer)
            m_nDisplayOrder = Value
        End Set
    End Property

    Public Property ContactType() As Byte
        Get
            Return m_byContactType
        End Get
        Set(ByVal Value As Byte)
            m_byContactType = Value
        End Set
    End Property

    Public Property FilePrefix() As String
        Get
            Return m_szFilePrefix
        End Get
        Set(ByVal Value As String)
            m_szFilePrefix = Value
        End Set
    End Property

    Public Property SearchVerbiage() As String
        Get
            Return m_szSearchVerbiage
        End Get
        Set(ByVal Value As String)
            m_szSearchVerbiage = Value
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

    Public Property DisplayName() As String
        Get
            Return m_szDisplayName
        End Get
        Set(ByVal Value As String)
            m_szDisplayName = Value
        End Set
    End Property

    Public Property FolderLocation() As String
        Get
            Return m_szFolderLocation
        End Get
        Set(ByVal Value As String)
            m_szFolderLocation = Value
        End Set
    End Property

#End Region

End Class
