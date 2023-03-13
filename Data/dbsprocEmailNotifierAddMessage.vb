Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Public Class dbsprocEmailNotifierAddMessage

    Public Shared Function Execute(ByVal userId As Integer, ByVal subject As String, ByVal body As String, ByVal fromUserId As Integer, ByVal fromEmailAddress As String, ByVal fromEmailDisplayName As String, ByVal formatHtml As Boolean, ByVal holdUntilDate As Date, ByVal batchCount As Integer) As Integer
        Dim db As Database = New DatabaseProviderFactory().Create("UserCommon")

        Dim cmd As DbCommand = db.GetStoredProcCommand("dbo.EmailNotifierAddMessage")

        db.AddInParameter(cmd, "@pUserID", DbType.Int32, userId)
        db.AddInParameter(cmd, "@pSubject", DbType.String, subject)
        db.AddInParameter(cmd, "@pBody", DbType.String, body)
        db.AddInParameter(cmd, "@pFromUserID", DbType.Int32, IIf(fromUserId = 0, DBNull.Value, fromUserId))
        db.AddInParameter(cmd, "@pFromEmailAddress", DbType.String, IIf(fromEmailAddress = String.Empty, DBNull.Value, fromEmailAddress))
        db.AddInParameter(cmd, "@pFromEmailDisplayName", DbType.String, IIf(fromEmailDisplayName = String.Empty, DBNull.Value, fromEmailDisplayName))
        db.AddInParameter(cmd, "@pFormatHtml", DbType.Boolean, formatHtml)
        db.AddInParameter(cmd, "@pHoldUntilDate", DbType.Date, IIf(holdUntilDate = Nothing, DBNull.Value, holdUntilDate))
        db.AddInParameter(cmd, "@pBatchCount", DbType.Int32, IIf(batchCount = 0, DBNull.Value, batchCount))

        db.AddParameter(cmd, "@Result", DbType.Int32, ParameterDirection.ReturnValue, "@Result", DataRowVersion.Default, Nothing)

        db.ExecuteNonQuery(cmd)

        Dim oResult As Object = db.GetParameterValue(cmd, "@Result")

        If oResult IsNot Nothing Then
            Return CInt(oResult)
        Else
            Return 0
        End If

    End Function

End Class
