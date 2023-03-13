Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Public Class dbsprocAgencyListGet

#Region " Executes "

    Public Shared Function Execute() As AgencyCollection

        Dim a As Agency
        Dim ac As New AgencyCollection

        Dim szSP As String = "dbo.AgencyListGet"

        Dim drdr As New SqlDataReaderHelper(SqlHelper.ExecuteReader(ConfigHelper.GetConnectionString, CommandType.StoredProcedure, szSP))

        With drdr

            While .Read

                a = New Agency

                a.AgencyID = .GetString("agency_id")

                ac.Add(a)

            End While

            .Close()

        End With

        Return ac

    End Function

#End Region

End Class
