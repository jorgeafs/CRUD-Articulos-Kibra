Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel

Public Class Proveedores
#Region "Atributos"
    Private _idProveedor As Integer
    Private _empresaPro As String
#End Region
#Region "Constructor"
    Public Sub New()
        _idProveedor = 0
        _empresaPro = Nothing
    End Sub
    Public Sub New(ByVal nombreEmpresa)
        _idProveedor = 0
        _empresaPro = nombreEmpresa
    End Sub
#End Region
#Region "Propiedades"
    Public Property idProveedor() As Integer
        Get
            Return _idProveedor
        End Get
        Set(value As Integer)
            _idProveedor = value
        End Set
    End Property
    <Required(ErrorMessage:="El nombre de la empresa proveedora")>
    <StringLength(100, ErrorMessage:="El nombre de la empresa es demasiado largo")>
    Public Property empresaPro() As String
        Get
            Return _empresaPro
        End Get
        Set(value As String)
            _empresaPro = value
        End Set
    End Property
#End Region
End Class
