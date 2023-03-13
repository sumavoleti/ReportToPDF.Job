Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Text

Public Class dbtableReportPDF

#Region " Save "

    Public Shared Sub Save(ByVal a As Agency, ByVal fp As FilenameParts, ByVal rt As ReportType, ByVal rp As ReportPeriod, ByVal ID As String)

        Dim oParam1 As New SqlParameter("@TypeID", rt.ReportTypeID)
        Dim oParam2 As New SqlParameter("@AgencyID", a.AgencyID)
        Dim oParam3 As New SqlParameter("@Year", fp.ReportYear)
        Dim oParam4 As New SqlParameter("@Month", IIf(fp.ReportMonth >= 1 AndAlso fp.ReportMonth <= 12, fp.ReportMonth, DBNull.Value))
        Dim oParam5 As New SqlParameter("@Date", IIf(fp.ReportDate = New Date, DBNull.Value, fp.ReportDate))
        Dim oParam6 As New SqlParameter("@ID", ID & ".pdf")


        Dim sb As New StringBuilder

        sb.Append("INSERT INTO dbo.report_pdf ")
        sb.Append("(report_pdf_type_id, agency_id, process_year, process_month, process_date, filename)")
        sb.Append("VALUES")
        sb.Append("(@TypeID,@AgencyID,@Year,@Month,@Date,@ID)")


        ' Next, Do the insert
        Dim nCtr As Integer = SqlHelper.ExecuteNonQuery(ConfigHelper.GetConnectionString, CommandType.Text, sb.ToString, oParam1, oParam2, oParam3, oParam4, oParam5, oParam6)

        ' Should be one record!


    End Sub


#End Region

End Class
