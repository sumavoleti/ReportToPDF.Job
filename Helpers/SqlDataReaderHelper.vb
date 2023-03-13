'
' SqlDataReaderHelper
'
' This is a generic 'Helper' class that should be used when processing a SqlClient.SqlDatReader.
' It will encapsulate all access to the functions provided inside the datareader.
'
' Programmers note:
'   - When you use this class in your application, it is always copied into your project. If needed,
'     you can make changes that are custom to your application.
'
Public Class SqlDataReaderHelper

#Region " Private variables "

    Private m_drdrData As SqlClient.SqlDataReader

#End Region

#Region " Constructor "

    Private Sub New()
    End Sub

    Public Sub New(ByVal dr As SqlClient.SqlDataReader)
        m_drdrData = dr
    End Sub

#End Region

#Region " Properties and Methods "

    Public ReadOnly Property FieldCount() As Integer
        Get
            If m_drdrData Is Nothing Then Return 0
            Return m_drdrData.FieldCount
        End Get
    End Property

    Public Function Read() As Boolean

        If m_drdrData Is Nothing Then Return False
        Return m_drdrData.Read()

    End Function

    Public Function IsClosed() As Boolean

        If m_drdrData Is Nothing Then Return True
        Return m_drdrData.IsClosed

    End Function

    Public Sub Close()

        If Not m_drdrData Is Nothing Then
            m_drdrData.Close()
        End If

    End Sub

    Public Function NextResult() As Boolean

        If m_drdrData Is Nothing Then Return False
        Return m_drdrData.NextResult()

    End Function

    Public ReadOnly Property RecordsAffected() As Integer
        Get
            If m_drdrData Is Nothing Then Return 0
            Return m_drdrData.RecordsAffected
        End Get
    End Property

    Public Function GetDatabaseSchema() As DataTable

        If m_drdrData Is Nothing Then Return Nothing
        Return m_drdrData.GetSchemaTable

    End Function

#End Region

#Region " GetString "

    Public Overloads Function GetString(ByVal fieldName As String) As String

        If m_drdrData Is Nothing Then Return String.Empty

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return String.Empty
            Else
                Return .GetString(.GetOrdinal(fieldName)).TrimEnd
            End If

        End With

    End Function

    Public Overloads Function GetString(ByVal index As Integer) As String

        If m_drdrData Is Nothing Then Return String.Empty

        With m_drdrData

            If .IsDBNull(index) Then
                Return String.Empty
            Else
                Return .GetString(index).TrimEnd
            End If

        End With

    End Function

#End Region

#Region " GetGUID "

    Public Overloads Function GetGUID(ByVal fieldName As String) As String

        If m_drdrData Is Nothing Then Return Nothing

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return Nothing
            Else
                Return .GetSqlGuid(.GetOrdinal(fieldName)).ToString
            End If

        End With

    End Function

    Public Overloads Function GetGUID(ByVal index As Integer) As String

        If m_drdrData Is Nothing Then Return Nothing

        With m_drdrData

            If .IsDBNull(index) Then
                Return Nothing
            Else
                Return .GetSqlGuid(index).ToString
            End If

        End With

    End Function

#End Region

#Region " GetDataTypeName "

    Public Overloads Function GetDataTypeName(ByVal fieldName As String) As String

        If m_drdrData Is Nothing Then Return Nothing

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return Nothing
            Else
                Return .GetDataTypeName(.GetOrdinal(fieldName)).TrimEnd
            End If

        End With

    End Function

    Public Overloads Function GetDataTypeName(ByVal index As Integer) As String

        If m_drdrData Is Nothing Then Return Nothing

        With m_drdrData

            If .IsDBNull(index) Then
                Return Nothing
            Else
                Return .GetDataTypeName(index).TrimEnd
            End If

        End With

    End Function

#End Region

#Region " GetChar "

    Public Overloads Function GetChar(ByVal fieldName As String) As Char

        If m_drdrData Is Nothing Then Return Nothing

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return Nothing
            Else
                Return .GetChar(.GetOrdinal(fieldName))
            End If

        End With

    End Function

    Public Overloads Function GetChar(ByVal index As Integer) As Char

        If m_drdrData Is Nothing Then Return Nothing

        With m_drdrData

            If .IsDBNull(index) Then
                Return Nothing
            Else
                Return .GetChar(index)
            End If

        End With

    End Function

#End Region

#Region " GetInteger "

    Public Overloads Function GetInteger(ByVal fieldName As String) As Integer

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return 0
            Else
                Return .GetInt32(.GetOrdinal(fieldName))
            End If

        End With

    End Function

    Public Overloads Function GetInteger(ByVal index As Integer) As Integer

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(index) Then
                Return 0
            Else
                Return .GetInt32(index)
            End If

        End With

    End Function

#End Region

#Region " GetDecimal "

    Public Overloads Function GetDecimal(ByVal fieldName As String) As Decimal

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return 0
            Else
                Return .GetDecimal(.GetOrdinal(fieldName))
            End If

        End With

    End Function

    Public Overloads Function GetDecimal(ByVal index As Integer) As Decimal

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(index) Then
                Return 0
            Else
                Return .GetDecimal(index)
            End If

        End With

    End Function

#End Region

#Region " GetFloat "

    Public Overloads Function GetFloat(ByVal fieldName As String) As Single

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return 0
            Else
                Return .GetFloat(.GetOrdinal(fieldName))
            End If

        End With

    End Function

    Public Overloads Function GetFloat(ByVal index As Integer) As Single

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(index) Then
                Return 0
            Else
                Return .GetFloat(index)
            End If

        End With

    End Function

#End Region

#Region " GetDouble "

    Public Overloads Function GetDouble(ByVal fieldName As String) As Double

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return 0
            Else
                Return .GetDouble(.GetOrdinal(fieldName))
            End If

        End With

    End Function

    Public Overloads Function GetDouble(ByVal index As Integer) As Double

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(index) Then
                Return 0
            Else
                Return .GetDouble(index)
            End If

        End With

    End Function

#End Region

#Region " GetMoney "

    Public Overloads Function GetMoney(ByVal fieldName As String) As Double

        If m_drdrData Is Nothing Then Return 0

        Return GetMoney(m_drdrData.GetOrdinal(fieldName))

    End Function

    Public Overloads Function GetMoney(ByVal index As Integer) As Double

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(index) Then
                Return 0
            Else
                Return Convert.ToDouble(.GetDecimal(index))
            End If

        End With

    End Function

#End Region

#Region " GetShort "

    Public Overloads Function GetShort(ByVal fieldName As String) As Short

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return 0
            Else
                Return .GetInt16(.GetOrdinal(fieldName))
            End If

        End With

    End Function

    Public Overloads Function GetShort(ByVal index As Integer) As Short

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(index) Then
                Return 0
            Else
                Return .GetInt16(index)
            End If

        End With

    End Function

#End Region

#Region " GetLong "

    Public Overloads Function GetLong(ByVal fieldName As String) As Long

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return 0
            Else
                Return .GetInt64(.GetOrdinal(fieldName))
            End If

        End With

    End Function

    Public Overloads Function GetLong(ByVal index As Integer) As Long

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(index) Then
                Return 0
            Else
                Return .GetInt64(index)
            End If

        End With

    End Function

#End Region

#Region " GetByte "

    Public Overloads Function GetByte(ByVal fieldName As String) As Byte

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return 0
            Else
                Return .GetByte(.GetOrdinal(fieldName))
            End If

        End With

    End Function

    Public Overloads Function GetByte(ByVal index As Integer) As Byte

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(index) Then
                Return 0
            Else
                Return .GetByte(index)
            End If

        End With

    End Function

#End Region

#Region " GetBytes "

    Public Overloads Function GetBytes(ByVal i As Integer, ByVal fieldOffet As Long, ByVal buffer() As Byte, ByVal bufferOffset As Integer, ByVal length As Integer) As Long

        If m_drdrData Is Nothing Then Return 0

        With m_drdrData

            If .IsDBNull(i) Then
                Return 0
            Else
                Return .GetBytes(i, fieldOffet, buffer, bufferOffset, length)
            End If

        End With

    End Function

#End Region

#Region " GetDateTime "

    Public Overloads Function GetDateTime(ByVal fieldName As String) As DateTime

        If m_drdrData Is Nothing Then Return New Date

        With m_drdrData

            If .IsDBNull(.GetOrdinal(fieldName)) Then
                Return New Date
            Else
                Return .GetDateTime(.GetOrdinal(fieldName))
            End If

        End With

    End Function

    Public Overloads Function GetDateTime(ByVal index As Integer) As DateTime

        If m_drdrData Is Nothing Then Return New Date

        With m_drdrData

            If .IsDBNull(index) Then
                Return New Date
            Else
                Return .GetDateTime(index)
            End If

        End With

    End Function

#End Region

#Region " GetBoolean "

    Public Overloads Function GetBoolean(ByVal fieldName As String) As Boolean

        If m_drdrData Is Nothing Then Return False

        Return m_drdrData.GetBoolean(m_drdrData.GetOrdinal(fieldName))

    End Function

    Public Overloads Function GetBoolean(ByVal index As Integer) As Boolean

        If m_drdrData Is Nothing Then Return False

        Return m_drdrData.GetBoolean(index)

    End Function

#End Region

End Class
