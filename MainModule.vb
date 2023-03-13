Imports System.IO
Imports System.Text
Imports Microsoft.ApplicationBlocks.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Module MainModule

#Region " Main "

    Sub Main()

        '
        ' Entry point for the Application.
        '

        '
        ' This Console application takes a Crystal Report and creates individual PDF files.
        ' The report must be in the format ZZZZZZ_YYYY_MM_DD_P.RPT
        '
        ' Where:   ZZZZZZ = is the file prefix from the report_pdf_types table that indicates what the report is.
        '          YYYY   = The year for the report.
        '          MM     = The month for the report (00 means that this is not a monthly report)
        '          DD     = Day of the month (00 means that this is not a daily/weekly report)
        '          P      = Period. D=Daily, W=weekly, M=monthly, S=semi-annual, Y=annual.
        '


        ' Get the path to where the RPT file should be located. If the value is blank, use the current directory.
        Dim szSourcePath As String = ConfigHelper.GetStringValue("SourcePath")
        If szSourcePath = String.Empty Then
            szSourcePath = AppDomain.CurrentDomain.BaseDirectory 'Directory.GetCurrentDirectory.ToString
        End If

        If Not szSourcePath.EndsWith("\") Then
            szSourcePath &= "\"
        End If

        'TESTING
        'Dim fp As New FilenameParts("AM0026_2006_05_00_M.rpt")
        'TESTING

        ' Load all the Report Types from the database
        Dim coReportTypes As ReportTypeCollection = dbtableReportPDFTypes.GetAll
        Dim coReportPeriods As ReportPeriodCollection = dbtableReportPDFPeriods.GetAll


        ' Load all files from the source folder that have the RPT file extention
        Dim oFileinfo As FileInfo() = New DirectoryInfo(szSourcePath).GetFiles("*.rpt")

        Log.Write("------------------------------------------------------------------------------")
        Log.Write("Process Started")
        Log.Write("")

        Dim upperBound As Integer = oFileinfo.GetUpperBound(0)
        If upperBound < 0 Then
            Log.Write("No RPT file found in source folder.")
        End If
        ' Process all *.RPT files found in the folder.
        For i As Integer = 0 To upperBound

            ' Parse and identify the file
            Dim fp As New FilenameParts(oFileinfo(i).ToString)

            ' If the file is being processed, begin:
            If fp.IsInCorrectFormat Then

                ' Find the ReportType for the file.
                Dim rt As ReportType = coReportTypes.ItemByPrefix(fp.Prefix)
                Dim rp As ReportPeriod = coReportPeriods.ItemByPeriodChar(fp.PeriodChar)

                If rt Is Nothing OrElse rp Is Nothing Then

                    ' The report type was not found. No processing can occur. Log and Exit.
                    Log.Write("No matching report type for prefix [" & fp.Prefix & "] nor period [" & fp.PeriodChar & "]. File Ignored")

                Else

                    Try
                        ProcessCrystalReport(fp, rt, rp, szSourcePath)
                    Catch ex As Exception
                        Log.Write("ERROR :: Message: " & ex.Message & " Report: " & fp.Filename)
                        Dim FileName As String = "[" & fp.Filename & "] " & rt.DisplayName
                        SendEmail(False, FileName)
                    End Try

                End If

            Else

                ' The file was not in the correct format. Ignore:

                Log.Write("File [" & fp.Filename & "]. Not in the correct format of [ZZZZZZ_YYYY_MM_DD_P.RPT]. File Ignored")

            End If


        Next

        Log.Write("")
        Log.Write("Process Ended")
        Log.Write("------------------------------------------------------------------------------")

    End Sub

#End Region

#Region " ProcessCrystalReport "

    Private Sub ProcessCrystalReport(ByVal fp As FilenameParts, ByVal rt As ReportType, ByVal rp As ReportPeriod, ByVal filePath As String)

        ' Performs the Processing for all Agents within the Report.

        Log.Write("Processing file [" & fp.Filename & "] " & rt.DisplayName)

        ' Verify that the destiniation folder is in the config file.
        Dim szDestPath As String = rt.FolderLocation
        If szDestPath = String.Empty Then
            Throw New ApplicationException("Target folder not found in the report_df_types table for the prefix [" & fp.Prefix & "]")
        End If

        ' Make sure the folder Exists
        If Not Directory.Exists(szDestPath) Then
            Throw New ApplicationException("Target folder [" & szDestPath & "] could not be found")
        End If

        ' Tack on the Year. Create the folder as needed.
        If szDestPath.EndsWith("\") Then
            szDestPath &= fp.ReportYear
        Else
            szDestPath &= "\" & fp.ReportYear
        End If

        If Not Directory.Exists(szDestPath) Then
            Directory.CreateDirectory(szDestPath)
        End If

        ' Tack on the Month. Create the folder as needed
        szDestPath &= "\" & fp.ReportMonth.ToString("00")
        If Not Directory.Exists(szDestPath) Then
            Directory.CreateDirectory(szDestPath)
        End If


        ' Check to see of the RPT file is already in the folder. If it's there, then stop the process. This will
        ' prevent PDFs from being overwritten.
        Dim oFileinfo As FileInfo() = New DirectoryInfo(szDestPath).GetFiles(fp.Filename)
        If oFileinfo.Length > 0 Then
            Throw New ApplicationException("Folder [" & szDestPath & "] already exists with the file [" & fp.Filename & "]. Cannot continue with file. Prior data must be removed before re-processing.")
        End If


        szDestPath &= "\"

        Log.Write("Writing to folder [" & szDestPath & "]")


        '
        ' Folders Exist and is ready to Process the file
        '

        ' Load all Agencies to Process
        Dim coAgencies As AgencyCollection = GetAgencies()


        ' Move the file to the destination folder
        File.Move(filePath & fp.Filename, szDestPath & fp.Filename)

        Dim nCtr As Integer = 0

        If coAgencies.Count > 0 Then

            ' Load the Crystal Report Document and Viewer

            Dim oRptViewer As New CrystalReportViewer
            Dim oRpt As New ReportDocument

            oRpt.Load(szDestPath & fp.Filename)

            oRptViewer.ReportSource = oRpt
            oRptViewer.ShowFirstPage()

            ' Loop through each Agency.
            For Each a As Agency In coAgencies

                ' Find the Page location in the Crystal Report.
                Dim oPageInfo As PageInfo = FindAgencyPageInfo(oRptViewer, a, fp, rt)

                If Not oPageInfo Is Nothing Then

                    Dim szFileID As String = String.Empty

                    If ConfigHelper.GetBooleanValue("ObfuscatePDFs") Then
                        szFileID = System.Guid.NewGuid.ToString
                    Else
                        szFileID = a.AgencyID
                    End If

                    ' Export the PDF
                    ExportPDF(oRpt, szFileID & ".pdf", szDestPath, oPageInfo)

                    ' Save the Record to the Database
                    dbtableReportPDF.Save(a, fp, rt, rp, szFileID)

                    Log.Write("Processed Agency [" & a.AgencyID & "]")
                    nCtr += 1

                End If

            Next

            Log.Write(" ")
            Log.Write("Generated " & nCtr.ToString & " Agency PDFs")
            Log.Write(" ")
            oRpt.Close()

            ' Send success mail
            Dim FileName As String = "[" & fp.Filename & "] " & rt.DisplayName
            SendEmail(True, FileName)


        End If

    End Sub

#End Region

#Region " Load Agency List "

    Private Function GetAgencies() As AgencyCollection

        '***************************************************
        'Purpose:    Get's a list of agencies for the current reporting period
        '             from the Agency database
        '***************************************************

        Return dbsprocAgencyListGet.Execute()

    End Function

#End Region

#Region " FindAgencyPageInfo "

    Private Function FindAgencyPageInfo(ByVal rpt As CrystalDecisions.Windows.Forms.CrystalReportViewer, ByVal a As Agency, ByVal fp As FilenameParts, ByVal rt As ReportType) As PageInfo

        '***************************************************
        'Purpose:    Find a page range of a crystal report that is associated with a specific agency and/or sort
        '****************************************************

        Dim szSearchText As String = rt.SearchVerbiage
        Dim oPage As New PageInfo


        szSearchText = szSearchText.Replace("[AGTID]", a.AgencyID)


        While rpt.SearchForText(szSearchText)
            If oPage.FromPage = -1 Then
                oPage.FromPage = rpt.GetCurrentPageNumber
            End If
        End While

        If oPage.FromPage = -1 Then Exit Function

        oPage.ToPage = rpt.GetCurrentPageNumber

        Return oPage

    End Function

#End Region

#Region " Export PDF "

    Private Sub ExportPDF(ByVal rptDocument As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal rptName As String, ByVal rptDirectory As String, ByVal pi As PageInfo)

        '***************************************************
        'Purpose:    Export a page range of a crystal report to a pdf file
        '             Uses the export functionality of crystal
        '
        'Inputs:     agencyCode = Agent Id
        '             fromPage = Starting page to print from
        '             toPage = Page to print to
        '
        'Output:     Creates a pdf file based on agency code and page range in the directory specified by m_szDirectory
        '
        '****************************************************
        Dim diskOpts As DiskFileDestinationOptions = ExportOptions.CreateDiskFileDestinationOptions()
        Dim pdfOpts As PdfRtfWordFormatOptions = ExportOptions.CreatePdfRtfWordFormatOptions
        Dim exportOpts As ExportOptions = New ExportOptions
        Try

            With pdfOpts
                .FirstPageNumber = pi.FromPage
                .LastPageNumber = pi.ToPage
                .UsePageRange = (pi.FromPage > 0)

            End With
            ' set the export format
            With exportOpts
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .ExportFormatOptions = pdfOpts
                .ExportDestinationType = ExportDestinationType.DiskFile
            End With
            ' set the disk file options
            diskOpts.DiskFileName = Path.Combine(rptDirectory, rptName)

            exportOpts.ExportDestinationOptions = diskOpts

            rptDocument.Export(exportOpts)

        Catch ex As System.Exception
            Throw
        End Try

    End Sub

#End Region

#Region " Send Mail "
    Private Sub SendEmail(Success As Boolean, FileName As String)

        Dim szFrom As String = "noreply@HarfordMutual.com"
        Dim szTo As String = String.Empty
        Dim sbBody As StringBuilder
        Dim szSubject As String = String.Empty
        Dim szMessage As String = String.Empty
        Dim nMessageId As Integer = 0
        Dim byToType As Byte = 1
        Dim UserId As Integer = 0

        szTo = ConfigHelper.GetStringValue("To_EmailAddress")
        UserId = ConfigHelper.GetIntegerValue("UserId")
        If Success Then
            szSubject = "Success: Report To PDF - " & FileName
            szMessage = "Report to PDF " & FileName & " generated successfully."
        Else
            szSubject = "Failure: Report To PDF - " & FileName
            szMessage = "Report to PDF " & FileName & " generation failed."
        End If

        sbBody = New StringBuilder

        With sbBody
            .Append("<span style='font-family:Calibri; font-size:18; color:DarkOliveGreen;'>")
            .Append(szMessage)
            .Append("<br/>")
            .Append("<br/>")
            .Append("DO NOT REPLY. This message was automatically generated by the Report to PDF application.")
            .Append("</span>")
        End With

        nMessageId = EmailerFacade.ExecuteEmailNotifierAddMessage(UserId, szSubject, sbBody.ToString, 0, szFrom, "Harford Mutual", True, Nothing, 0)
        EmailerFacade.ExecuteEmailNotifierAddRecipient(nMessageId, 0, szTo, byToType)


    End Sub
#End Region
End Module
