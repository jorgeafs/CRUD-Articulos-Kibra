Imports Entity_Kibra
Imports DAL_Kibra
Public Class ManejadoraBL
#Region "Atributos"
    Private listasDAL As ListadoArticulosProveedor
#End Region
#Region "Constructor"
    Public Sub New()
        Try
            listasDAL = New ListadoArticulosProveedor
        Catch ex As Exception
            Throw New Exception("Error en la conexion", ex)
        End Try
    End Sub
#End Region
#Region "Metodos"
    Public Function listaArticulosBL() As List(Of Articulo)
        Dim listaArt As New List(Of Articulo)
        Try
            listaArt = listasDAL.listaArticulosDAL()
            Return listaArt
        Catch ex As Exception
            Throw New Exception("Error al listar los articulos" & ex.Message)
        End Try
    End Function

    Public Function listaProveedoresBL() As List(Of Proveedores)
        Dim listaPro As New List(Of Proveedores)

        Try
            listaPro = listasDAL.listaProveedoresDAL
            Return listaPro
        Catch ex As Exception
            Throw New Exception("Error al listar los proveedores" & ex.Message)
        End Try
    End Function

    Public Function insertArticuloBL(articuloMod As Articulo) As Integer
        Dim resultado As Integer

        Try
            resultado = listasDAL.insertArticuloDAL(articuloMod)
            Return resultado
        Catch ex As Exception
            Throw New Exception("Error al insertar" & ex.Message)
        End Try
    End Function

    Public Function borrarArticuloBL(articulo As Articulo) As Integer
        Dim resultado As Integer

        Try
            resultado = listasDAL.borrarArticuloDAL(articulo)
            Return resultado
        Catch ex As Exception
            Throw New Exception("Error al insertar" & ex.Message)
        End Try
    End Function

    Public Function actualizarArticuloBL(articulo As Articulo) As Integer
        Dim resultado As Integer

        Try
            resultado = listasDAL.actualizarArticuloDAL(articulo)
            Return resultado
        Catch ex As Exception
            Throw New Exception("Error al insertar" & ex.Message)
        End Try
    End Function

    Public Function seleccionaArticuloBL(id As Integer) As Articulo
        Dim resultado As Articulo

        Try
            resultado = listasDAL.seleccionaArticuloDAL(id)
            Return resultado
        Catch ex As Exception
            Throw New Exception("Error al obtener la persona con id " & id & " " & ex.Message)
        End Try

    End Function

    Public Function seleccionaProveedorBL(id As Integer) As Proveedores
        Dim resultado As Proveedores

        Try
            resultado = listasDAL.seleccionaProveedorDAL(id)
            Return resultado
        Catch ex As Exception
            Throw New Exception("Error al obtener el proveedor con id " & id & " " & ex.Message)
        End Try
    End Function

    Public Function seleccionaProveedorBL(nombre As String) As Proveedores
        Dim resultado As Proveedores = Nothing

        Try
            For Each proveedor As Proveedores In listaProveedoresBL()
                If (nombre.Equals(proveedor.empresaPro)) Then
                    resultado = New Proveedores(proveedor.empresaPro)
                    resultado.idProveedor = proveedor.idProveedor
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw New Exception("Error al obtener el proveedor con nombre " & nombre & " " & ex.Message)
        End Try
    End Function

#End Region
End Class
