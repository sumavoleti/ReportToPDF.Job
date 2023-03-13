Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Public Class dbsprocEmailNotifierAddRecipient

    Public Shared Function Execute(ByVal messageId As Integer, ByVal toUserId As Integer, ByVal emailAddress As String, ByVal toType As Byte) As Boolean

        Dim db As Database = New DatabaseProviderFactory().Create("UserCommon")

        Dim cmd As DbCommand = db.GetStoredProcCommand("dbo.EmailNotifierAddRecipient")

        db.AddInParameter(cmd, "@pMessageID", DbType.Int32, messageId)
        db.AddInParameter(cmd, "@pToUserID", DbType.Int32, toUserId)
        db.AddInParameter(cmd, "@pEmailAddress", DbType.String, emailAddress)
        db.AddInParameter(cmd, "@pToType", DbType.Byte, toType)

        Dim nCount As Integer = db.ExecuteNonQuery(cmd)

        Return nCount > 0

    End Function


End Class
