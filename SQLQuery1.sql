CREATE TABLE Usuario(
	IdUsuario INT PRIMARY KEY IDENTITY,
	Nombre VARCHAR(50),
	Correo VARCHAR(50),
	Clave VARCHAR(100)
);

CREATE TABLE Producto(
	IdProducto INT PRIMARY KEY IDENTITY,
	Nombre VARCHAR(50),
	Marca VARCHAR(50),
	Precio DECIMAL(10,2)
)

INSERT INTO Producto(Nombre,Marca,Precio) VALUES ('Laptop gamer 1001','HP',3500),('Monitor curvo HD','Samsung',2000);