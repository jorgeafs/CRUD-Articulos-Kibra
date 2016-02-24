Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel
Imports System.Windows.Media.Imaging


Public Class Articulo
#Region "Atributos"
    Private _idArticulo As Integer
    Private _nombreArt As String
    Private _imagenArt As Byte()
    Private _descArt As String
    Private _precioArt As Decimal
    Private _stock As Integer
    Private _stockMinimo As Integer
    Private _idProveedor As Integer
    Private _proveedor As Proveedores
#End Region

#Region "Contructores"
    'Default
    Public Sub New()
        _idArticulo = 0
        _nombreArt = Nothing
        _imagenArt = Nothing
        _descArt = Nothing
        _precioArt = 0.0
        _stock = 0
        _stockMinimo = 0
        _idProveedor = 0
        _proveedor = Nothing
    End Sub

    'Con parametros
    Public Sub New(ByVal nombreArt As String, ByVal imagenArt As Byte(), ByVal descArt As String, ByVal precioArt As Decimal, ByVal stock As Integer, ByVal stockMinimo As Integer, ByVal idProveedor As Integer)
        _idArticulo = 0
        _nombreArt = nombreArt
        _imagenArt = imagenArt   ' convierteArrayDeBytesAimagen(imagenArt) ByVal imagenArt As Byte(), 
        _descArt = descArt
        _precioArt = precioArt
        _stock = stock
        _stockMinimo = stockMinimo
        _idProveedor = idProveedor
        _proveedor = Nothing
    End Sub
#End Region

#Region "Propiedades"
    'Esta propiedad nunca sera accesible al usuario, en la BD es Identity, cuando actualizemos la BD usaremos el valor 0 como control para insertar o actualizar (en caso de ser distinto de 0)
    Public Property idArticulo() As Integer
        Get
            Return _idArticulo
        End Get
        Set(value As Integer)
            _idArticulo = value
        End Set
    End Property
    <Required(ErrorMessage:="El articulo debe tener un nombre")>
    <MaxLength(80, ErrorMessage:="No puede ser de mas de 80 caracteres")>
    <Display(Name:="Nombre del articulo")>
    Public Property nombreArt() As String
        Get
            Return _nombreArt
        End Get
        Set(value As String)
            _nombreArt = value
        End Set
    End Property
    'en la base de datos no hay imagenes... es un binario en la base de datos
    <Required(ErrorMessage:="Se esperaba una imagen")>
    Public Property imagenArt() As Byte()
        Get
            Return _imagenArt
        End Get
        Set(value As Byte())
            _imagenArt = value
        End Set
    End Property
    <Required(ErrorMessage:="La descripcion es obligatoria")>
    <Display(Name:="Descripcion del articulo")>
    <MaxLength(150, ErrorMessage:="Descripcion demasiado larga")>
    Public Property descArt() As String
        Get
            Return _descArt
        End Get
        Set(value As String)
            _descArt = value
        End Set
    End Property
    <Required(ErrorMessage:="Se esperaba un precio")>
    <Range(0.0, 922337203685477.62, ErrorMessage:="Precio fuera de rango")>
    Public Property precioArt() As Decimal
        Get
            Return _precioArt
        End Get
        Set(value As Decimal)
            _precioArt = value
        End Set
    End Property
    <Required(ErrorMessage:="Es necesaria una cantidad en stock")>
    <Range(0.0, 922337203685477.5807, ErrorMessage:="Cantidad fuera de rango")>
    Public Property stock() As Integer
        Get
            Return _stock
        End Get
        Set(value As Integer)
            _stock = value
        End Set
    End Property
    <Required(ErrorMessage:="Es necesaria una cantidad minima en stock")>
    <Range(0.0, 922337203685477.62, ErrorMessage:="Cantidad fuera de rango")>
    Public Property stockMinimo() As Integer
        Get
            Return _stockMinimo
        End Get
        Set(value As Integer)
            _stockMinimo = value
        End Set
    End Property

    Public Property idProveedor() As Integer
        Get
            Return _idProveedor
        End Get
        Set(value As Integer)
            _idProveedor = value
        End Set
    End Property

    Public Property proveedor() As Proveedores
        Get
            Return _proveedor
        End Get
        Set(value As Proveedores)
            _proveedor = value
        End Set
    End Property
#End Region

#Region "Metodos privados"
    Private Function convierteArrayDeBytesAimagen(arrayImagen As Byte()) As BitmapImage
        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(arrayImagen)
        Dim miImagen As New BitmapImage

        Try
            miImagen.BeginInit()
            miImagen.StreamSource = ms
            miImagen.EndInit()
        Catch ex As Exception
            Throw New Exception(ex.InnerException.Message)
        End Try
        Return miImagen
    End Function
#End Region

End Class
