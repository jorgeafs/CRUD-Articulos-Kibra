--Crear procedimiento para insertar, modificar y eliminar en la tabla departamento. 
--***********************************************************************************************************************************************************
--Insertar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han insertado datos o 0 si no se han insertado
Llamada: EXECUTE insertarDepartamentos @nombreDep,@descDep,@telefono,@res OUTPUT
*/
CREATE PROCEDURE insertarDepartamentos(@nombreDep VARCHAR(80),@descDep VARCHAR(150),@telefono VARCHAR(12), @res BIT OUTPUT) AS
BEGIN
	--NO DEBERIA DE HABER VALIDACIONES??
	INSERT INTO Departamentos(nombreDep,descDep,telefono) VALUES (@nombreDep,@descDep,@telefono)
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Modificar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han modificados datos o 0 si no
Llamada: EXECUTE modificarDepartamentos @idDepartamento,@nombreDep,@descDep,@telefono,@res OUTPUT
*/
CREATE PROCEDURE modificarDepartamentos (@idDepartamento INT, @nombreDep VARCHAR(80),@descDep VARCHAR(150),@telefono VARCHAR(12), @res BIT OUTPUT) AS
BEGIN
	UPDATE Departamentos SET nombreDep=@nombreDep,descDep=@descDep,telefono=@telefono WHERE idDepartamento=@idDepartamento
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO
--Eliminar
/*
Precondiciones: Ninguna
PostCondiciones: Devuelve 1 si se han eliminado datos o 0 si no 
Llamada: EXECUTE eliminarDepartamentos @idDepartamento,@res OUTPUT
*/

CREATE PROCEDURE eliminarDepartamentos (@idDepartamento INT, @res BIT OUTPUT) AS
BEGIN
	DELETE FROM Departamentos WHERE idDepartamento=@idDepartamento
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO


--Crear procedimiento para insertar, modificar y eliminar en la tabla Puestos.
--***************************************************************************************************************************************************************************
--Insertar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han insertado datos o 0 si no se han insertado
Llamada: EXECUTE insertarPuesto @nombrePue,@descPue ,@salario, @res OUTPUT
		EXECUTE insertarPuesto @nombrePue=valor,@descPue=valor, @res=variable OUTPUT
*/
CREATE PROCEDURE insertarPuesto(@nombrePue VARCHAR(80),@descPue VARCHAR(150),@salario MONEY = 0, @res BIT OUTPUT) AS
BEGIN
	
	INSERT INTO Puestos(nombrePue,descPue,salario) VALUES (@nombrePue,@descPue,@salario)
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Modificar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han modificados datos o 0 si no
Llamada: EXECUTE modificarPuesto @idPuesto,@nombrePue,@descPue ,@salario, @res OUTPUT
		EXECUTE modificarPuesto @idPuesto=valor, @nombrePue=valor,@descPue=valor, @res=variable OUTPUT
*/
CREATE PROCEDURE modificarPuesto (@idPuesto INT, @nombrePue VARCHAR(80),@descPue VARCHAR(150),@salario MONEY=NULL, @res BIT OUTPUT) AS
BEGIN
	
	UPDATE Puestos SET nombrePue=@nombrePue,salario=ISNULL(@salario,salario),descPue=@descPue WHERE idPuesto=@idPuesto
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO
--Eliminar
/*
Precondiciones: Ninguna
PostCondiciones: Devuelve 1 si se han eliminado datos o 0 si no 
Llamada: EXECUTE eliminarPuesto @idPuesto,@res OUTPUT
*/

CREATE PROCEDURE eliminarPuesto (@idPuesto INT, @res BIT OUTPUT) AS
BEGIN
	DELETE FROM Puestos WHERE idPuesto=@idPuesto
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Crear procedimiento para insertar, modificar y eliminar en la tabla empleados. 
--******************************************************************************************************************************************************************
--Insertar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han insertado datos o 0 si no se han insertado
*/
CREATE PROCEDURE insertarEmpleado(@dniEmpl VARCHAR(9),@nombreEmp VARCHAR(80),@apellidoEmp VARCHAR(100),@fechaNac DATE, @direccionEmp VARCHAR(150)
	,@telefonoEmp VARCHAR(12),@emailEmp VARCHAR(150),@departamentoEmp INT = NULL, @puestoEmp INT =NULL,@ciudadEmp INT =NULL ,@res BIT OUTPUT) AS
BEGIN
	
	INSERT INTO Empleados(dniEmp,nombreEmp,apellidoEmp,fechaNac,direccionEmp,telefonoEmp,emailEmp,departamentoEmp,puestoEmp,ciudadEmp) 
			VALUES (@dniEmpl,@nombreEmp,@apellidoEmp,@fechaNac,@direccionEmp,@telefonoEmp,@emailEmp,@departamentoEmp,@puestoEmp,@ciudadEmp)
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Modificar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han modificados datos o 0 si no
*/
CREATE PROCEDURE modificarEmpleado (@idEmpleado INT, @dniEmpl VARCHAR(9),@nombreEmp VARCHAR(80),@apellidoEmp VARCHAR(100),@fechaNac DATE, @direccionEmp VARCHAR(150)
	,@telefonoEmp VARCHAR(12),@emailEmp VARCHAR(150),@departamentoEmp INT = NULL, @puestoEmp INT =NULL,@ciudadEmp INT =NULL ,@res BIT OUTPUT) AS
BEGIN

	UPDATE Empleados SET dniEmp=@dniEmpl,nombreEmp=@nombreEmp,apellidoEmp=@apellidoEmp,fechaNac=@fechaNac,direccionEmp=@direccionEmp,
		telefonoEmp=@telefonoEmp,emailEmp=@emailEmp,departamentoEmp=ISNULL(@departamentoEmp,departamentoEmp),
		puestoEmp=ISNULL(@puestoEmp,puestoEmp),ciudadEmp=ISNULL(@ciudadEmp,ciudadEmp) WHERE idEmpleado=@idEmpleado

	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO
--Eliminar
/*
Precondiciones: Ninguna
PostCondiciones: Devuelve 1 si se han eliminado datos o 0 si no 
Llamada: EXECUTE eliminarEmpleado @idEmpleado,@res OUTPUT
*/

CREATE PROCEDURE eliminarEmpleado (@idEmpleado INT, @res BIT OUTPUT) AS
BEGIN
	DELETE FROM Empleados WHERE idEmpleado=@idEmpleado
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Crear procedimiento para insertar, modificar y eliminar en la tabla Logins.
--****************************************************************************************************************************************************************
--Insertar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han insertado datos o 0 si no se han insertado
NOTAS: MODIFICAR LA PASS CON HASHBYTE
*/
CREATE PROCEDURE insertarLogin(@usuario VARCHAR(30),@passwd VARCHAR(30),@ultimoAcceso DATE='01-01-0001',@empleado INT=NULL, @res BIT OUTPUT) AS
BEGIN

	INSERT INTO Login(usuario,passwd,ultimoAcceso,empleado) VALUES (@usuario,CONVERT(NVARCHAR(30),HASHBYTES('MD5',@passwd),2),@ultimoAcceso,@empleado)
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Modificar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han modificados datos o 0 si no
*/
CREATE PROCEDURE modificarLogin (@idLogin INT, @usuario VARCHAR(30),@passwd VARCHAR(30),@ultimoAcceso DATE=NULL,@empleado INT=NULL, @res BIT OUTPUT) AS
BEGIN
	UPDATE Login SET usuario=@usuario,passwd=@passwd,ultimoAcceso=ISNULL(@ultimoAcceso,ultimoAcceso),empleado=ISNULL(@empleado,empleado) WHERE idLogin=@idLogin
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO
--Eliminar
/*
Precondiciones: Ninguna
PostCondiciones: Devuelve 1 si se han eliminado datos o 0 si no 
*/

CREATE PROCEDURE eliminarLogin (@idLogin INT, @res BIT OUTPUT) AS
BEGIN
	DELETE FROM Login WHERE idLogin=@idLogin
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO


--Crear procedimiento para insertar, modificar y eliminar en la tabla OrdenesVentas.
--*********************************************************************************************************************************
--Insertar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han insertado datos o 0 si no se han insertado
*/
CREATE PROCEDURE insertarOrdenesVentas(@fecha DATE ,@empleado INT=NULL,@cliente INT=NULL, @res BIT OUTPUT) AS
BEGIN

	INSERT INTO OrdenesVentas(fecha,empleado,cliente) VALUES (@fecha,@empleado,@cliente)
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Modificar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han modificados datos o 0 si no
*/
CREATE PROCEDURE modificarOrdenesVentas (@idOrdenVenta INT,@fecha DATE ,@empleado INT=NULL,@cliente INT=NULL, @res BIT OUTPUT) AS
BEGIN
	UPDATE OrdenesVentas SET fecha=@fecha,empleado=ISNULL(@empleado,empleado),cliente=ISNULL(@cliente,cliente) WHERE idOrdenVenta=@idOrdenVenta
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Eliminar
/*
Precondiciones: Ninguna
PostCondiciones: Devuelve 1 si se han eliminado datos o 0 si no 
*/

CREATE PROCEDURE eliminarOrdenesVentas (@idOrdenVenta INT, @res BIT OUTPUT) AS
BEGIN
	DELETE FROM OrdenesVentas WHERE idOrdenVenta=@idOrdenVenta
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Crear procedimiento para insertar, modificar y eliminar en la tabla OrdenesCompras.
--*********************************************************************************************************************************
--Insertar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han insertado datos o 0 si no se han insertado
*/
CREATE PROCEDURE insertarOrdenesCompras(@fecha DATE ,@empleado INT=NULL,@proveedor INT=NULL, @res BIT OUTPUT) AS
BEGIN

	INSERT INTO OrdenesCompras(fecha,empleado,proveedor) VALUES (@fecha,@empleado,@proveedor)
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Modificar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han modificados datos o 0 si no
*/
CREATE PROCEDURE modificarOrdenesCompras (@idOrdenCompra INT,@fecha DATE ,@empleado INT=NULL,@proveedor INT=NULL, @res BIT OUTPUT) AS
BEGIN
	UPDATE OrdenesCompras SET fecha=@fecha,empleado=ISNULL(@empleado,empleado),proveedor=ISNULL(@proveedor,proveedor) WHERE idOrdenCompra=@idOrdenCompra
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Eliminar
/*
Precondiciones: Ninguna
PostCondiciones: Devuelve 1 si se han eliminado datos o 0 si no 
*/

CREATE PROCEDURE eliminarOrdenesCompras (@idOrdenCompra INT, @res BIT OUTPUT) AS
BEGIN
	DELETE FROM OrdenesCompras WHERE idOrdenCompra=@idOrdenCompra
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Crear procedimiento para insertar, modificar y eliminar en la tabla Facturas.
--*********************************************************************************************************************************
--Insertar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han insertado datos o 0 si no se han insertado
*/
CREATE PROCEDURE insertarFacturas(@fechaGenerada DATE ,@ordenVenta INT=NULL, @res BIT OUTPUT) AS
BEGIN

	INSERT INTO Facturas(fechaGenerada,ordenVenta) VALUES (@fechaGenerada,@ordenVenta)
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Modificar
/*
Precondiciones: Los datos deberian de estar ya validados
PostCondiciones: Devuelve 1 si se han modificados datos o 0 si no
*/
CREATE PROCEDURE modificarFacturas (@idFactura INT,@fechaGenerada DATE ,@ordenVenta INT=NULL, @res BIT OUTPUT) AS
BEGIN
	UPDATE Facturas SET fechaGenerada=@fechaGenerada,ordenVenta=ISNULL(@ordenVenta,ordenVenta) WHERE idFactura=@idFactura
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Eliminar
/*
Precondiciones: Ninguna
PostCondiciones: Devuelve 1 si se han eliminado datos o 0 si no 
*/

CREATE PROCEDURE eliminarFacturas (@idFactura INT, @res BIT OUTPUT) AS
BEGIN
	DELETE FROM Facturas WHERE idFactura=@idFactura
	IF(@@ROWCOUNT!=0)
	BEGIN
		SET @res=1
	END ELSE
	BEGIN
		SET @res=0
	END
END
GO

--Crear procedimiento para insertar, modificar y eliminar en la tabla Articulos.
--*********************************************************************************************************************************
--Insertar
Create procedure insertarArticulos (@nombreArt Varchar(80),@descArt Varchar(150),@precioArt money,@stock int,@stockMinimo int,@idProveedor int,@res bit output) as
begin
	insert into Articulos(nombreArt,descArt,precioArt,stock,stockMinimo,idProveedor) values (@nombreArt,@descArt,@precioArt,@stock,@stockMinimo,@idProveedor)
	if (@@ROWCOUNT!=0)
	begin
		set @res=1
	end else
	begin
		set @res=0
	end
end
go
--Modificar
Create Procedure modificarArticulos (@nombreArt Varchar(80),@descArt Varchar(150),@precioArt money,@stock int,@stockMinimo int,@idProveedor int,@res bit output) as
begin
	update Articulos set nombreArt=@nombreArt,descArt=@descArt,precioArt=@precioArt,stock=@stock,stockMinimo=@stockMinimo,idProveedor=@idProveedor 
	if(@@ROWCOUNT!=0)
	begin
		set @res=1
	end else
	begin
		set @res = 0
	end
end
go
--Eliminar
Create procedure eliminarArticulos(@idArticulo int,@res bit output) as
begin
	delete from Articulos where idArticulo = @idArticulo
	if(@@ROWCOUNT!=0)
	begin
		set @res=1
	end else
	begin
		set @res = 0
	end
end
go




--Crear procedimiento para insertar, modificar y eliminar en la tabla Clientes. --CONSIDERANDO LA PK DE Clientes COMO IDENTITY
--*********************************************************************************************************************************
--Insertar
Create procedure insertarClientes (@nombreCli varchar(80),@apellidoCli varchar(80),@direccionCli varchar(150),@telefonoCli Varchar(12),@emailCli varchar(150),@ciudad int,@res bit output) as
begin
	insert into Clientes (nombreCli,apellidoCli,direccionCli,telefonoCli,emailCli,ciudad) values (@nombreCli,@apellidoCli,@direccionCli,@telefonoCli,@emailCli,@ciudad)
	if (@@ROWCOUNT!=0)
	begin
		set @res=1
	end else
	begin
		set @res=0
	end
end
go
--Modificar
Create procedure modificarClientes (@nombreCli varchar(80),@apellidoCli varchar(80),@direccionCli varchar(150),@telefonoCli Varchar(12),@emailCli varchar(150),@ciudad int,@res bit output) as
begin
	update Clientes set nombreCli=@nombreCli,apellidoCli=@apellidoCli,direccion=@direccionCli,telefono=@telefonoCli,emailCli=@emailCli,ciudad=@ciudad
	if(@@ROWCOUNT!=0)
	begin
		set @res=1
	end else
	begin
		set @res = 0
	end
end
go
--Eliminar
Create procedure eliminarClientes (@idCliente int,@res bit output) as 
begin
	delete from Clientes where idCliente=@idCliente
	if(@@ROWCOUNT!=0)
	begin
		set @res=1
	end else
	begin
		set @res = 0
	end
end
go

--Crear procedimiento para insertar, modificar y eliminar en la tabla Proveedores.
--*********************************************************************************************************************************
--Insertar
Create procedure insertarProveedores (@empresaPro varchar (100),@nombrePro varchar(80),@direccionPro varchar(150),@telefonoPro varchar(12),@emailPro varchar(150),@ciudadPro int,@res bit output) as 
begin
	insert into Proveedores(empresaPro,nombrePro,direccionPro,telefonoPro,emailPro,ciudadPro)values(@empresaPro,@nombrePro,@direccionPro,@telefonoPro,@emailPro,@ciudadPro)
	if (@@ROWCOUNT!=0)
	begin
		set @res=1
	end else
	begin
		set @res=0
	end
end
go
--Modificar
Create procedure modificarProveedores(@empresaPro varchar (100),@nombrePro varchar(80),@direccionPro varchar(150),@telefonoPro varchar(12),@emailPro varchar(150),@ciudadPro int,@res bit output) as 
begin
	update Proveedores set empresaPro=@empresaPro,nombrePro=@nombrePro,direccionPro=@direccionPro,telefonoPro=@telefonoPro,emailPro=@emailPro,ciudadPro=@ciudadPro
	if(@@ROWCOUNT!=0)
	begin
		set @res=1
	end else
	begin
		set @res = 0
	end
end
go
--Eliminar
Create procedure eliminarProveedores(@idProveedor int,@res bit output) as 
begin
	delete from Proveedores where idProveedor=@idProveedor
	if(@@ROWCOUNT!=0)
	begin
		set @res=1
	end else
	begin
		set @res = 0
	end
end
go


