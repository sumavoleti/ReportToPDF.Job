Imports System.IO

Public Class Log

    Public Shared Sub Write(ByVal text As String)

        Dim oStreamWriter As StreamWriter
        Dim sb As New System.Text.StringBuilder

        'Dim szLogPath As String = Directory.GetCurrentDirectory.ToString & "\log"
        Dim szLogPath As String = AppDomain.CurrentDomain.BaseDirectory & "\log"
        If Not Directory.Exists(szLogPath) Then

            Directory.CreateDirectory(szLogPath)

        End If

        With sb
            .Append(szLogPath)

            If Not szLogPath.EndsWith("\") Then
                .Append("\")
            End If

            .Append(Date.Today.Year.ToString & "_")
            .Append(Date.Today.Month.ToString("00") & "_")
            .Append(Date.Today.Day.ToString("00"))
            .Append(".log")

        End With

        Dim szLogFile As String = sb.ToString

        If Not File.Exists(szLogFile) Then
            oStreamWriter = File.CreateText(szLogFile)

        Else
            oStreamWriter = File.AppendText(szLogFile)
        End If

        oStreamWriter.WriteLine(Now.Hour.ToString("00") & ":" & Now.Minute.ToString("00") & ":" & Now.Second.ToString("00") & " | " & text)

        oStreamWriter.Close()

    End Sub

End Class
