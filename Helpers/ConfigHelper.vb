'
' ConfigHelper
'
' This is a generic 'Helper' class that should be used in all ASP.NET applications. It provides a
' common interface method for retrieving data from the Web.config. The structure of the Web.config that
' these methods use is as follow:
'
' <configuration>
'     <appSettings>
'         <add key="PolicyNumber" value="1234567" /> 
'     </appSettings>
' </configuration>
'
' The above sample setting would be retrieved by using the GetStringValue method below:
'
'   Dim szPolicyNumber as String = ConfigHelper.GetStringValue("PolicyNumber")
'
'
' Things to keep in mind:
'   - Web.config keyvalues are case-insensative
'
' Programmers note:
'   - When you use this class in your ASP.NET application, it is always copied into your project. If needed,
'     you can make changes that are custom to your application. For example, the GetIntegerValue function will
'     return a -1 when the integer cannot be detemined. If your application considers a -1 to be a valid value,
'     then you may need to change it so that the functions returns another value instead.
'

Imports System.Configuration

Public NotInheritable Class ConfigHelper

    '
    ' GetStringValue: Returns a String value from the Web.config. If the value has not been
    '       provided, then 'String.Empty' is returned.
    Public Shared Function GetStringValue(ByVal key As String) As String

        ' Retrieve the value from the Web.config
        Dim szValue As String = ConfigurationSettings.AppSettings(key)

        If szValue Is Nothing Then
            Return String.Empty
        Else
            Return szValue
        End If

    End Function

    '
    ' GetIntegerValue: Returns an Integer value from the Web.config. If the value is not 
    '       found or if the value is not an integer, then a -1 is returned.
    Public Shared Function GetIntegerValue(ByVal key As String) As Integer

        Dim result As Integer = -1
        Dim szValue As String = GetStringValue(key)

        If String.Empty <> szValue Then
            Try
                result = Integer.Parse(szValue)
            Catch
                'Ignore format exceptions.
            End Try
        End If

        Return result

    End Function


    '
    ' GetBooleanValue: Returns a Boolean value from the Web.config. If the value has not been
    '       found or if the value is not 'true' or '1', then a false is returned.
    Public Shared Function GetBooleanValue(ByVal key As String) As Boolean

        ' Initialize the return value to False
        Dim bReturn As Boolean = False

        ' Retrieve the value from the Web.Config as a string.
        Dim szValue As String = GetStringValue(key)

        If String.Empty <> szValue Then

            ' For boolean values, allow the word 'true' or the number '1' to indicate
            ' a boolean true. Otherwise, the value will be false.
            If szValue.ToLower.Equals("true") Or szValue.Equals("1") Then

                bReturn = True

            End If

        End If

        Return bReturn

    End Function

    '
    ' GetConnectionString: Returns a String Sql Connection String from the Web.config. If the value 
    '       has not been provided, then 'String.Empty' is returned.
    Public Shared Function GetConnectionString() As String

        ' Retrieve the value from the Web.config
        Dim szValue As String = ConfigurationSettings.AppSettings("connectionString")

        If szValue Is Nothing Then
            Return String.Empty
        Else
            Return szValue
        End If

    End Function

End Class

