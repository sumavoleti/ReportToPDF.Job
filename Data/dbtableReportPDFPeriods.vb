Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Public Class dbtableReportPDFPeriods

#Region " Selects "

    Public Shared Function GetAll() As ReportPeriodCollection

        Dim szCmd As String = "SELECT * FROM dbo.report_pdf_periods"

        Return ProcessDataReader(New SqlDataReaderHelper(SqlHelper.ExecuteReader(ConfigHelper.GetConnectionString, CommandType.Text, szCmd)))

    End Function

#End Region

#Region " ProcessDataReader "

    Private Shared Function ProcessDataReader(ByVal drdr As SqlDataReaderHelper) As ReportPeriodCollection

        Dim rp As ReportPeriod
        Dim rpc As New ReportPeriodCollection

        With drdr

            While .Read

                rp = New ReportPeriod

                rp.ReportPeriodID = .GetByte("report_pdf_period_id")
                rp.Description = .GetString("description")
                rp.PeriodChar = .GetString("period_char")
                rp.RententionDaysMax = .GetInteger("retention_max_days")

                rpc.Add(rp)

            End While

            .Close()

        End With

        Return rpc

    End Function

#End Region

End Class
