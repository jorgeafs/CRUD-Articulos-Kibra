Imports Entity_Kibra
Imports System.Data.SqlClient

Public Class ListadoArticulosProveedor
#Region "Atributos"
    Private myConnection As clsMyConnection
    Private ReadOnly Articulos As String = "Articulos"
    Private ReadOnly Proveedores As String = "Proveedores"
#End Region
#Region "Constructor"
    Public Sub New()
        Try
            myConnection = New clsMyConnection()
        Catch ex As Exception
            Throw New Exception("Error en la conexion", ex)
        End Try
    End Sub
#End Region
#Region "Metodos"
    Public Function listaArticulosDAL() As List(Of Articulo)
        Dim listaArt As New List(Of Articulo)
        Dim miCommand As New SqlCommand
        'Igualamos a Nothing para evitar warnings
        Dim miConexion As SqlConnection = Nothing
        Dim articulosReader As SqlDataReader = Nothing
        Dim oArticulo As Articulo

        Try
            miConexion = myConnection.getConnection
            'ten en cuenta como hallas llamado a la tabla en la base de datos
            miCommand.CommandText = "Select * FROM " & Articulos
            miCommand.Connection = miConexion
            articulosReader = miCommand.ExecuteReader()

            'rellenamos la lista
            If articulosReader.HasRows Then
                While articulosReader.Read
                    oArticulo = New Articulo(articulosReader.Item("nombreArt"), articulosReader.Item("imagenArt"), articulosReader.Item("descArt"), articulosReader.Item("precioArt"), articulosReader.Item("stock"), articulosReader.Item("stockMinimo"), articulosReader.Item("idProveedor"))
                    oArticulo.idArticulo = articulosReader.Item("idArticulo")  ' articulosReader.Item("imagenArt"),
                    listaArt.Add(oArticulo)
                End While
            End If

            Return listaArt
        Catch ex As Exception
            Throw New Exception("Error al listar los articulos" & ex.Message)
        Finally
            miConexion.Close()
        End Try
    End Function

    Public Function listaProveedoresDAL() As List(Of Proveedores)
        Dim listaPro As New List(Of Proveedores)
        Dim miCommand As New SqlCommand
        'Igualamos a Nothing para evitar warnings
        Dim miConexion As SqlConnection = Nothing
        Dim proveedoresReader As SqlDataReader = Nothing
        Dim oProveedores As Proveedores

        Try
            miConexion = myConnection.getConnection
            'ten en cuenta como hallas llamado a la tabla en la base de datos
            miCommand.CommandText = "Select * FROM " & Proveedores
            miCommand.Connection = miConexion
            proveedoresReader = miCommand.ExecuteReader()

            'rellenamos la lista
            If proveedoresReader.HasRows Then
                While proveedoresReader.Read
                    oProveedores = New Proveedores
                    With oProveedores
                        'tomamos los datos del articulo del reader y los almacenamos en oArticulo
                        .empresaPro = proveedoresReader.Item("empresaPro")
                        .idProveedor = proveedoresReader.Item("idProveedor")
                    End With
                    listaPro.Add(oProveedores)
                End While
            End If

            Return listaPro
        Catch ex As Exception
            Throw New Exception("Error al listar los proveedores" & ex.Message)
        Finally
            miConexion.Close()
        End Try
    End Function

    Public Function insertArticuloDAL(articuloMod As Articulo) As Integer
        Dim añadirArticulo As String
        Dim miCommand As New SqlCommand
        Dim resultado As Integer
        'Igualamos a Nothing para evitar warnings
        Dim miConexion As SqlConnection = Nothing
        'Dim imagen As Byte() = New Byte() {1}

        añadirArticulo = "INSERT INTO Articulos (nombreArt ,imagenArt ,descArt ,precioArt,stock,stockMinimo,idProveedor) VALUES (@nombreArt , @imagenArt , @descArt , @precioArt , @stock , @stockMinimo , @idProveedor)"
        Try
            miConexion = myConnection.getConnection
            miCommand.CommandText = añadirArticulo
            miCommand.Connection = miConexion
            miCommand.Parameters.Add(New SqlParameter("@nombreArt", SqlDbType.NVarChar))
            miCommand.Parameters("@nombreArt").Value = articuloMod.nombreArt
            miCommand.Parameters.Add(New SqlParameter("@imagenArt", SqlDbType.Binary))
            miCommand.Parameters("@imagenArt").Value = articuloMod.imagenArt
            miCommand.Parameters.Add(New SqlParameter("@descArt", SqlDbType.NVarChar))
            miCommand.Parameters("@descArt").Value = articuloMod.descArt
            miCommand.Parameters.Add(New SqlParameter("@precioArt", SqlDbType.Money))
            miCommand.Parameters("@precioArt").Value = articuloMod.precioArt
            miCommand.Parameters.Add(New SqlParameter("@stock", SqlDbType.Int))
            miCommand.Parameters("@stock").Value = articuloMod.stock
            miCommand.Parameters.Add(New SqlParameter("@stockMinimo", SqlDbType.Int))
            miCommand.Parameters("@stockMinimo").Value = articuloMod.stockMinimo
            miCommand.Parameters.Add(New SqlParameter("@idProveedor", SqlDbType.Int))
            miCommand.Parameters("@idProveedor").Value = articuloMod.idProveedor
            resultado = miCommand.ExecuteNonQuery()

            Return resultado
        Catch ex As Exception
            Throw New Exception("Error al insertar" & ex.Message)
        Finally
            miConexion.Close()
        End Try
    End Function

    ''' <summary>
    ''' it erase a given object Articulo from the database
    ''' </summary>
    ''' <param name="articulo" ></param>
    Public Function borrarArticuloDAL(articulo As Articulo) As Integer
        Dim borrarArticulo As String
        Dim miCommand As New SqlCommand
        Dim resultado As Integer
        'Igualamos a Nothing para evitar warnings
        Dim miConexion As SqlConnection = Nothing

        borrarArticulo = "DELETE FROM " & Articulos & " WHERE idArticulo = @id"
        Try
            miConexion = myConnection.getConnection
            miCommand.CommandText = borrarArticulo
            miCommand.Connection = miConexion
            miCommand.Parameters.Add(New SqlParameter("@id", SqlDbType.Int))
            miCommand.Parameters("@id").Value = articulo.idArticulo
            resultado = miCommand.ExecuteNonQuery()
            Return resultado
        Catch ex As Exception
            Throw New Exception("Error al insertar" & ex.Message)
        Finally
            miConexion.Close()
        End Try
    End Function

    ''' <summary>
    ''' actualiza la base de datos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function actualizarArticuloDAL(articulo As Articulo) As Integer
        Dim borrarArticulo As String
        Dim miCommand As New SqlCommand
        Dim resultado As Integer
        'Igualamos a Nothing para evitar warnings
        Dim miConexion As SqlConnection = Nothing
        'temporalmente usaremos esta variable para las imagenes
        'Dim imagen As Byte() = New Byte() {1}

        borrarArticulo = "UPDATE Articulos SET nombreArt = @nombreArt ,imagenArt = @imagenArt ,descArt = @descArt ,precioArt = @precioArt ,stock = @stock ,stockMinimo = @stockMinimo ,idProveedor = @idProveedor WHERE idArticulo = @idArticulo"

        Try
            miConexion = myConnection.getConnection
            miCommand.CommandText = borrarArticulo
            miCommand.Connection = miConexion
            miCommand.Parameters.Add(New SqlParameter("@nombreArt", SqlDbType.NVarChar))
            miCommand.Parameters("@nombreArt").Value = articulo.nombreArt
            miCommand.Parameters.Add(New SqlParameter("@imagenArt", SqlDbType.Binary))
            miCommand.Parameters("@imagenArt").Value = articulo.imagenArt
            miCommand.Parameters.Add(New SqlParameter("@descArt", SqlDbType.NVarChar))
            miCommand.Parameters("@descArt").Value = articulo.descArt
            miCommand.Parameters.Add(New SqlParameter("@precioArt", SqlDbType.Money))
            miCommand.Parameters("@precioArt").Value = articulo.precioArt
            miCommand.Parameters.Add(New SqlParameter("@stock", SqlDbType.Int))
            miCommand.Parameters("@stock").Value = articulo.stock
            miCommand.Parameters.Add(New SqlParameter("@stockMinimo", SqlDbType.Int))
            miCommand.Parameters("@stockMinimo").Value = articulo.stockMinimo
            miCommand.Parameters.Add(New SqlParameter("@idProveedor", SqlDbType.Int))
            miCommand.Parameters("@idProveedor").Value = articulo.idProveedor

            miCommand.Parameters.Add(New SqlParameter("@idArticulo", SqlDbType.Int))
            miCommand.Parameters("@idArticulo").Value = articulo.idArticulo
            resultado = miCommand.ExecuteNonQuery()
            Return resultado
        Catch ex As Exception
            Throw New Exception("Error al insertar" & ex.Message)
        Finally
            miConexion.Close()
        End Try
    End Function

    Public Function seleccionaArticuloDAL(id As Integer) As Articulo
        Dim miCommand As New SqlCommand
        'Igualamos a Nothing para evitar warnings
        Dim miConexion As SqlConnection = Nothing
        Dim articuloReader As SqlDataReader = Nothing
        Dim oArticulo As Articulo = Nothing

        Try
            miConexion = myConnection.getConnection
            'ten en cuenta como hallas llamado a la tabla en la base de datos
            miCommand.CommandText = "Select * FROM " & Articulos & " WHERE idArticulo = @idArticulo"
            miCommand.Parameters.Add(New SqlParameter("@idArticulo", SqlDbType.Int))
            miCommand.Parameters("@idArticulo").Value = id
            miCommand.Connection = miConexion
            articuloReader = miCommand.ExecuteReader()

            'rellenamos la lista
            If articuloReader.HasRows Then
                While articuloReader.Read
                        'tomamos los datos del articulo del reader y los almacenamos en oArticulo
                    oArticulo = New Articulo(articuloReader.Item("nombreArt"), articuloReader.Item("imagenArt"), articuloReader.Item("descArt"), articuloReader.Item("precioArt"), articuloReader.Item("stock"), articuloReader.Item("stockMinimo"), articuloReader.Item("idProveedor"))
                    oArticulo.idArticulo = articuloReader("idArticulo")
                End While
            End If

            Return oArticulo
        Catch ex As Exception
            Throw New Exception("Error al listar los proveedores" & ex.Message)
        Finally
            miConexion.Close()
        End Try
    End Function

    Public Function seleccionaProveedorDAL(id As Integer) As Entity_Kibra.Proveedores
        Dim miCommand As New SqlCommand
        'Igualamos a Nothing para evitar warnings
        Dim miConexion As SqlConnection = Nothing
        Dim proveedorReader As SqlDataReader = Nothing
        Dim oProveedor As Proveedores = Nothing

        Try
            miConexion = myConnection.getConnection
            'ten en cuenta como hallas llamado a la tabla en la base de datos
            miCommand.CommandText = "Select * FROM " & Proveedores & " WHERE idProveedor = @idProveedor"
            miCommand.Parameters.Add(New SqlParameter("@idProveedor", SqlDbType.Int))
            miCommand.Parameters("@idProveedor").Value = id
            miCommand.Connection = miConexion
            proveedorReader = miCommand.ExecuteReader()

            'rellenamos la lista
            If proveedorReader.HasRows Then
                oProveedor = New Proveedores()
                While proveedorReader.Read
                    'tomamos los datos del articulo del reader y los almacenamos en oArticulo
                    oProveedor.idProveedor = proveedorReader.Item("idProveedor")
                    oProveedor.empresaPro = proveedorReader.Item("empresaPro")
                End While
            End If

            Return oProveedor
        Catch ex As Exception
            Throw New Exception("Error al listar los proveedores" & ex.Message)
        Finally
            miConexion.Close()
        End Try
    End Function
#End Region



End Class
