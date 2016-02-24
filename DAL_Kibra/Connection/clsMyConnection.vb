Imports System.Data.SqlClient

' CLASE: clsMyConnection    Author:@PedroSerrano , Modified   License: OpenSource
'
' Esta clase contiene los métodos necesarios para trabajar con el acceso a una base de datos SQL desde una aplicación de Visual Basic
'
' PROPIEDADES
'   _database: cadena, básica. Consultable/modificable.
'   _user: cadena, básica. Consultable/modificable.
'   _pass: cadena, básica. Consultable/modificable.
'
' MÉTODOS
'   Function getConnection() As SqlConnection
'       Este método abre una conexión con la base de datos. Lanza excepciones de tipo: SqlExcepion, InvalidOperationException y Exception.
'   
'   Sub closeConnection(ByRef connection As SqlConnection)
'       Este método cierra una conexión con la base de datos. Lanza excepciones de tipo: SqlExcepion, InvalidOperationException y Exception.
'
'   Sub closeDataReader(ByRef myDataReader As SqlDataReader)
'       Este método cierra un objeto de la clase SqlDataReader. Lanza excepciones de tipo: SqlExcepion, InvalidOperationException y Exception.
'

Public Class clsMyConnection

#Region "Atributos"
    Private _database As String
    Private _user As String
    Private _pass As String
#End Region

#Region "Constructores"

    'Constructor por defecto
    Public Sub New()
        Me._database = "Proyecto_kibra"
        Me._user = "Prueba"
        Me._pass = "123"
    End Sub

    'Constructor con parámetros
    Public Sub New(ByVal database As String, ByVal user As String, ByVal pass As String)
        Me._database = database
        Me._user = user
        Me._pass = pass
    End Sub

#End Region

#Region "Consultores y modificadores"

    Public Property Database() As String
        Get
            Return _database
        End Get
        Set(ByVal value As String)
            _database = value
        End Set
    End Property

    Public Property User() As String
        Get
            Return _user
        End Get
        Set(ByVal value As String)
            _user = value
        End Set
    End Property

    Public Property Pass() As String
        Get
            Return _pass
        End Get
        Set(ByVal value As String)
            _pass = value
        End Set
    End Property


#End Region

#Region "Métodos"

    ''' <summary>
    ''' This method open a connection with the database.
    ''' </summary>
    ''' <pre>Nothing.</pre>
    ''' <post>The connection is opened.</post>
    ''' <returns>A connection with the database.</returns>
    ''' <remarks></remarks>
    Public Function getConnection() As SqlConnection

        Dim connection As New SqlConnection()

        Try
            'connection.ConnectionString = "Data Source=" & My.Computer.Name & "Initial Catalog=" & _database & ";uid=" & _user & ";pwd=" & _user & ";"
            connection.ConnectionString = "server=(local);database=" & _database & ";uid=" & _user & ";pwd=" & _pass & ";"

            connection.Open()
        Catch exSql As SqlException
            Throw exSql
        Catch exInOp As InvalidOperationException
            Throw exInOp
        Catch ex As Exception
            Throw ex
        End Try

        Return connection

    End Function

    ''' <summary>
    ''' This method close a connection with the database.
    ''' </summary>
    ''' <pre>Nothing.</pre>
    ''' <post>The connection is closed.</post>
    ''' <param name="connection">The input parameter is the connection to close.</param>
    ''' <remarks></remarks>
    Public Sub closeConnection(ByRef connection As SqlConnection)

        Try
            connection.Close()
        Catch exSql As SqlException
            Throw exSql
        Catch exInOp As InvalidOperationException
            Throw exInOp
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region

End Class
