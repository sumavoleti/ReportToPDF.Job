Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Public Class dbtableReportPDFTypes

#Region " Selects "

    Public Shared Function GetAll() As ReportTypeCollection

        Dim szCmd As String = "SELECT * FROM dbo.report_pdf_types"

        Return ProcessDataReader(New SqlDataReaderHelper(SqlHelper.ExecuteReader(ConfigHelper.GetConnectionString, CommandType.Text, szCmd)))

    End Function

#End Region

#Region " ProcessDataReader "

    Private Shared Function ProcessDataReader(ByVal drdr As SqlDataReaderHelper) As ReportTypeCollection

        Dim rt As ReportType
        Dim rtc As New ReportTypeCollection

        With drdr

            While .Read

                rt = New ReportType

                rt.ReportTypeID = .GetInteger("report_pdf_type_id")
                rt.ReportPeriodID = .GetByte("report_pdf_period_id")
                rt.FilePrefix = .GetString("file_prefix")
                rt.SearchVerbiage = .GetString("search_verbiage")
                rt.Description = .GetString("description")
                rt.DisplayName = .GetString("display_name")
                rt.DisplayOrder = .GetInteger("display_order")
                rt.HttpLocation = .GetString("http_location")
                rt.HttpOnlineURL = .GetString("http_online_url")
                rt.FolderLocation = .GetString("folder_location")
                rt.ContactType = .GetByte("contact_type")

                rtc.Add(rt)

            End While

            .Close()

        End With

        Return rtc

    End Function

#End Region

End Class
