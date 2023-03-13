Public Class FilenameParts

#Region " Private variables "

    Private m_szFilename As String
    Private m_szPrefix As String
    Private m_szPeriodChar As String

    Private m_nYear As Integer
    Private m_nMonth As Integer
    Private m_nDay As Integer

    Private m_bIsCorrect As Boolean

    Private m_dReportDate As Date

#End Region

#Region " Public properties "

    Public ReadOnly Property IsInCorrectFormat() As Boolean
        Get
            Return m_bIsCorrect
        End Get
    End Property

    Public ReadOnly Property Prefix() As String
        Get
            Return m_szPrefix
        End Get
    End Property

    Public ReadOnly Property Filename() As String
        Get
            Return m_szFilename
        End Get
    End Property

    Public ReadOnly Property PeriodChar() As String
        Get
            Return m_szPeriodChar
        End Get
    End Property

    Public ReadOnly Property ReportYear() As Integer
        Get
            Return m_nYear
        End Get
    End Property

    Public ReadOnly Property ReportMonth() As Integer
        Get
            Return m_nMonth
        End Get
    End Property

    Public ReadOnly Property ReportDay() As Integer
        Get
            Return m_nDay
        End Get
    End Property

    Public ReadOnly Property ReportDate() As Date
        Get
            Return m_dReportDate
        End Get
    End Property

#End Region

#Region " Constructor "

    Private Sub New()

    End Sub

    Public Sub New(ByVal filename As String)

        ' Validate and parse the filename into it's parts.

        ' The report must be in the format ZZZZZZ_YYYY_MM_DD_P.RPT
        '
        ' Where:   ZZZZZZ = is the file prefix from the report_pdf_types table that indicates what the report is.
        '          YYYY   = The year for the report.
        '          MM     = The month for the report (00 means that this is not a monthly report)
        '          DD     = Day of the month (00 means that this is not a daily/weekly report)
        '          P      = Period. D=Daily, W=weekly, M=monthly, S=semi-annual, Y=annual.
        '

        m_bIsCorrect = False

        m_szFilename = filename

        ' Make sure there is a value
        If filename = String.Empty Then Exit Sub

        ' Check the length of the filename. Should be 23
        If filename.Length <> 23 Then Exit Sub

        ' Should end with .RPT
        If Not filename.ToUpper.EndsWith(".RPT") Then Exit Sub

        ' Get the prefix and store.
        m_szPrefix = filename.ToUpper.Substring(0, 6)

        ' Get the year/month/day.
        Dim szYear As String = filename.Substring(7, 4)
        Dim szMonth As String = filename.Substring(12, 2)
        Dim szDay As String = filename.Substring(15, 2)

        ' Get the Period
        m_szPeriodChar = filename.Substring(18, 1)


        ' Validate the Year/Month/Day and store
        Try
            m_nYear = Integer.Parse(szYear)
        Catch ex As FormatException
            m_nYear = 0
            Exit Sub
        End Try

        Try
            m_nMonth = Integer.Parse(szMonth)
        Catch ex As FormatException
            m_nMonth = 0
            Exit Sub
        End Try

        Try
            m_nDay = Integer.Parse(szDay)
        Catch ex As FormatException
            m_nDay = 0
            Exit Sub
        End Try


        If m_nDay > 0 AndAlso m_nMonth > 0 Then

            m_dReportDate = New Date(m_nYear, m_nMonth, m_nDay)

        Else

            m_dReportDate = New Date

        End If


        m_bIsCorrect = True

    End Sub

#End Region

End Class
