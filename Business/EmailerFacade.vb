Public Class EmailerFacade
    Public Shared Function ExecuteEmailNotifierAddMessage(ByVal userId As Integer, ByVal subject As String, ByVal body As String, ByVal fromUserId As Integer, ByVal fromEmailAddress As String, ByVal fromEmailDisplayName As String, ByVal formatHtml As Boolean, ByVal holdUntilDate As Date, ByVal batchCount As Integer) As Integer
        Return dbsprocEmailNotifierAddMessage.Execute(userId, subject, body, fromUserId, fromEmailAddress, fromEmailDisplayName, formatHtml, holdUntilDate, batchCount)
    End Function

    Public Shared Function ExecuteEmailNotifierAddRecipient(ByVal messageId As Integer, ByVal toUserId As Integer, ByVal emailAddress As String, ByVal toType As Byte) As Boolean
        Return dbsprocEmailNotifierAddRecipient.Execute(messageId, toUserId, emailAddress, toType)
    End Function
End Class
