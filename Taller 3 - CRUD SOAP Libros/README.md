
Book Server

1. Se debe crear una base de datos y una tabla en Sql Server.

CREATE DATABASE Books;

use Books;

CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(50),
    Description NVARCHAR(100),
    Author NVARCHAR(50)
);

2. Modificar la conexion en el archivo Web.config segun el servidor del Sql Server entra por Autenticacion de Windows.

    <connectionStrings>
		<add name="SqlConnection"
			 connectionString="Server=(Aqui va el nombre del Servidor);Database=Books;Integrated Security=True"
			 providerName="System.Data.SqlClient" />
    </connectionStrings>

3. Ejecutar


Book Client
1. Ejecutar

Se desplega un menu en consola en que se puede navegar por teclado.

(1) Para visualizar todos los registros de la tabla Books.
(2) Buscar registro por Id.
(3) Agregar nuevo registro de libro.
(4) Editar algun registro por Id y completando todos los campos de nuevo.
(5) Eliminar algun registro por Id.
(6) Salir de la consola.
