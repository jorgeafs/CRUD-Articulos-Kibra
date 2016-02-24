drop database proyecto_kibra
go

CREATE DATABASE Proyecto_kibra
ON 
( NAME = kibra_data,
    FILENAME = 'C:\Proyecto_Kibra\kibra_data.mdf',
    SIZE = 10MB,
    MAXSIZE = 250MB,
    FILEGROWTH = 5MB )
LOG ON
( NAME = kibra_log,
    FILENAME = 'C:\Proyecto_Kibra\kibra_log.ldf',
    SIZE = 5MB,
    MAXSIZE = 250MB,
    FILEGROWTH = 5MB ) ;
GO

use Proyecto_kibra
go

	------------------------------------
	--------- CREADO POR PABLO ---------
	------------------------------------ 



--------------------------------------
---------- TABLA PROVINCIAS ----------
--------------------------------------
--Almacenará las provincias de españa.
Create Table Provincias(
	idProvincia integer,
	nombreProv varchar(80),
	CONSTRAINT PK_Provincias PRIMARY KEY(idProvincia)
)
go

--------------------------------------
----------- TABLA CIUDADES -----------
--------------------------------------
--Almacenará las ciudades de españa.
Create Table Ciudades(
	idCiudad integer,
	nombreCiu varchar(80),
	provincia Integer,
	CONSTRAINT PK_Ciudades PRIMARY KEY(idCiudad),	
	constraint FK_Ciudades_Provincias foreign key (provincia) references provincias(idProvincia)
)
go

----------------------------------------
---------- TABLA DEPARTAMENTOS ---------
----------------------------------------
--Almacenará los departamentos dentro de la empresa. 
Create table Departamentos(
	idDepartamento integer identity,
	nombreDep varchar(80) not null,
	descDep varchar(150) not null,
	telefono varchar(12) not null,
	CONSTRAINT PK_Departamentos PRIMARY KEY (idDepartamento)
)
go

------------------------------------
----------- TABLA PUESTOS ----------
------------------------------------
--Esta tabla recogerá los distintos cargos que tienen los empleados dentro de la empresa. 
create table Puestos(
	idPuesto integer identity,
	nombrePue varchar(80) not null,
	descPue varchar(150) not null,
	salario money,
	constraint PK_Puestos primary key(idPuesto)
)
go

--------------------------------------
----------- TABLA EMPLEADOS ----------
--------------------------------------
--Esta tabla recogerá los datos de los empleados que pertenecen a la empresa.
Create table Empleados(
	idEmpleado integer identity,
	dniEmp varchar(9) Not null,
	nombreEmp varchar(80) not null,
	apellidoEmp varchar(100) not null,
	fechaNac   date not null,
	direccionEmp varchar(150) not null,
	telefonoEmp varchar(12) not null,
	emailEmp varchar(150) not null,
	departamentoEmp integer,
	puestoEmp integer, 
	ciudadEmp integer,
	constraint PK_Empleados primary key(idEmpleado),
	constraint FK_Empleados_Ciudades foreign key (ciudadEmp) references ciudades(idCiudad),
	constraint FK_Empleados_Departamentos foreign key (departamentoEmp) references departamentos(idDepartamento),
	constraint FK_Empleados_Puestos foreign key (puestoEmp) references Puestos(idPuesto)
)
go



-----------------------------------
----------- TABLA LOGINS ----------
-----------------------------------
--Esta tabla almacenara los logins de los empleados que tengan permiso para acceder al "ERP"
Create table [Login](
	idLogin integer identity,
	usuario varchar(30) not null,
	passwd varchar(30) not null,
	ultimoAcceso date,
	empleado integer,
	CONSTRAINT PK_Login PRIMARY KEY (idLogin),
	constraint FK_login_empleados foreign key (empleado) references empleados(idEmpleado)
)
go

----------------------------------------
----------- TABLA PRIVILEGIOS ----------
----------------------------------------
--En esta tabla se almacenaran los privilegios disponibles dentro del "ERP"
Create table Privilegios (
	idPrivilegio integer identity,
	nombrePriv varchar(80) not null,
	descPriv varchar(150) not null,
	valorPriv integer not null,
	CONSTRAINT PK_Privilegios PRIMARY KEY (idPrivilegio)
)
go


----------------------------------------------
----------- TABLA LOGIN_PRIVILEGIOS ----------
----------------------------------------------
--En esta tabla se recogeran los privilegios que tiene cada usuario sobre el sistema.
Create table Login_Privilegios(
	idLogin integer,
	idPrivilegio integer,
	fechaAsignado date,
	CONSTRAINT PK_Login_Privilegios PRIMARY KEY (idLogin,idPrivilegio),
	constraint FK_Login_Empleados_Login foreign key (idLogin) references [login](idLogin),
	constraint FK_login_Privilegios_Privilegios foreign key (idPrivilegio) references privilegios(idPrivilegio)
)
go
	------------------------------------
	------------ FIN  PABLO ------------
	------------------------------------

--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	-----------------------------------------
	--------- CREADO POR JOSE MARÍA ---------
	----------------------------------------- 


----------------------------------------
----------- TABLA PROVEEDORES ----------
----------------------------------------
--Tabla donde se almacenaran los proveedores con los que trabaja la empresa.
CREATE TABLE Proveedores(
	idProveedor int IDENTITY
	,empresaPro Varchar(100) not null
	,nombrePro Varchar(80) not null
	,direccionPro Varchar(150) not null
	,telefonoPro Varchar(12) not null
	,emailPro Varchar(150) not null
	,ciudadPro int not null
	,CONSTRAINT PK_Proveedores PRIMARY KEY (idProveedor)
	,CONSTRAINT FK_Ciudades_Proveedores FOREIGN KEY (ciudadPro) REFERENCES Ciudades(idCiudad)
) 
go


----------------------------------------
------------ TABLA ARTÍCULOS -----------
----------------------------------------
--Tabla donde se almacenara la informacion de los articulos con los que trabaja la empresa.
CREATE TABLE Articulos(
	idArticulo int IDENTITY 
	,nombreArt Varchar(80) NOT NULL
	,imagenArt Binary NOT NULL
	,descArt varchar(150) NOT NULL
	,precioArt money NOT NULL
	,stock int NOT NULL
	,stockMinimo int NOT NULL
	,idProveedor int NOT NULL
	,CONSTRAINT PK_Articulos PRIMARY KEY(idArticulo) 
	,CONSTRAINT FK_Proveedores_Articulos FOREIGN KEY (idProveedor) REFERENCES Proveedores(idProveedor)
)
GO
	----------------------------------
	--------- FIN JOSE MARÍA ---------
	----------------------------------

--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	--------------------------------------
	--------- CREADO POR JUAN S. ---------
	--------------------------------------

---------------------------------------
------------ TABLA CLIENTES -----------
---------------------------------------
--Tabla donde se almacenaran los clientes con los que trabaja la empresa.
CREATE TABLE Clientes(
	idCliente INT NOT NULL,
	nombreCli VARCHAR(80) not null,
	apellidoCli VARCHAR(80) not null,
	direccionCli VARCHAR(150) not null,
	telefonoCli VARCHAR(12) not null,
	emailCli VARCHAR(150) not null,
	ciudad INT,
	CONSTRAINT PK_idCliente PRIMARY KEY (idCliente),
	CONSTRAINT FK_Ciudades_Clientes FOREIGN KEY(ciudad) REFERENCES Ciudades(idCiudad)
)
go

--------------------------------------------
------------ TABLA ORDENESVENTAS -----------
--------------------------------------------
--Esta tabla almacenará las ordenes de venta que se realicen en la empresa.
CREATE TABLE OrdenesVentas(
	idOrdenVenta INT IDENTITY NOT NULL,
	fecha DATE not null,
	empleado INT not null,
	cliente INT not null,
	CONSTRAINT PK_idOrdenVenta PRIMARY KEY (idOrdenVenta),
	CONSTRAINT FK_Empleado_OredenesVentas FOREIGN KEY (empleado) REFERENCES Empleados(idEmpleado),
	CONSTRAINT FK_Clientes_OrdenesVentas FOREIGN KEY (cliente) REFERENCES Clientes(idCliente)	
)
go

---------------------------------------
------------ TABLA FACTURAS -----------
---------------------------------------
--En esta tabla se almacenaran los datos de la cabecera de las facturas asociadas a las ordenes de venta dentro de la empresa.
CREATE TABLE facturas(
	idFactura INT IDENTITY NOT NULL,
	fechaGenerada DATE not null,
	ordenVenta INT not null,
	CONSTRAINT PK_Factura PRIMARY KEY (idFactura),
	CONSTRAINT FK_OrdenesVentas_Facturas FOREIGN KEY (ordenventa) REFERENCES OrdenesVentas (idOrdenVenta)	
)
go

	-------------------------------
	--------- FIN JUAN S. ---------
	-------------------------------
	
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
	
	------------------------------------
	--------- CREADO POR ELIAS ---------
	------------------------------------	

--------------------------------------------
------------ TABLA ORDENESCOMPRA -----------
--------------------------------------------
--Esta tabla almacenará las ordenes de compra que se realicen en la empresa.
CREATE TABLE OrdenesCompras
(
	idOrdenCompra INTEGER IDENTITY,
	fecha DATE NOT NULL,
	empleado INTEGER NOT NULL,
	proveedor INTEGER NOT NULL,
	CONSTRAINT PK_OrdenesCompras PRIMARY KEY (idOrdenCompra),
	CONSTRAINT FK_Empleados_OrdenesCompras FOREIGN KEY (empleado) REFERENCES dbo.Empleados (idEmpleado),
	CONSTRAINT FK_Proveedores_OrdenesCompras FOREIGN KEY (proveedor) REFERENCES dbo.Proveedores (idProveedor)
)
GO

---------------------------------------------
------------ TABLA DETALLESCOMPRA -----------
---------------------------------------------
--Esta tabla almacenara cada una de las lineas de las ordenes de compra.

--Creo estas dos tablas para hacer referencia a OrdenesCompras y OrdenesVentas, ya que en el diagrama la
--columna orden(FK) de la tabla Detalles no puede hacer referencia a ambas columnas de dos tablas distintas.
CREATE TABLE DetallesCompras
(
	idDetalles INTEGER IDENTITY,
	precioUnidad MONEY NOT NULL,
	cantidad INTEGER NOT NULL,
	iva INTEGER NOT NULL,
	ordenCompra INTEGER NOT NULL,
	articulo INTEGER NOT NULL,
	CONSTRAINT PK_DetallesCompras PRIMARY KEY (idDetalles),
	CONSTRAINT FK_OrdenesCompras_Detalles FOREIGN KEY (ordenCompra) REFERENCES dbo.OrdenesCompras (idOrdenCompra),
	CONSTRAINT FK_ARTICULOS_DETALLESC FOREIGN KEY (articulo) REFERENCES dbo.Articulos (idArticulo)
)
GO


--------------------------------------------
------------ TABLA DETALLESVENTA -----------
--------------------------------------------
--Esta tabla almacenara cada una de las lineas de las ordenes de venta.
CREATE TABLE DetallesVentas
(
	idDetalles INTEGER IDENTITY,
	precioUnidad MONEY NOT NULL,
	cantidad INTEGER NOT NULL,
	iva INTEGER NOT NULL,
	ordenVenta INTEGER NOT NULL,
	articulo INTEGER NOT NULL,
	CONSTRAINT PK_DetallesVentas PRIMARY KEY (idDetalles),
	CONSTRAINT FK_OrdenesVentas_Detalles FOREIGN KEY (ordenVenta) REFERENCES dbo.OrdenesVentas (idOrdenVenta),
	CONSTRAINT FK_ARTICULOS_DETALLESV FOREIGN KEY (articulo) REFERENCES dbo.Articulos (idArticulo)
)
GO

	-----------------------------
	--------- FIN ELIAS ---------
	-----------------------------