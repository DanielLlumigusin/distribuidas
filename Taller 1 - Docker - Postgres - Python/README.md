# API de Productos con FastAPI

Este proyecto proporciona una API RESTful para gestionar productos en una base de datos PostgreSQL. Permite realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) para productos.

## Funcionalidades

- **Crear un producto**: Permite agregar un nuevo producto con un nombre, cantidad y precio.
- **Leer todos los productos**: Devuelve una lista de todos los productos almacenados en la base de datos.
- **Leer un producto**: Devuelve la información de un producto específico mediante su ID.
- **Actualizar un producto**: Permite actualizar los detalles de un producto utilizando su ID.
- **Eliminar un producto**: Permite eliminar un producto específico mediante su ID.

## Tecnologías Utilizadas

- **FastAPI**: Framework web para construir la API RESTful.
- **PostgreSQL**: Sistema de gestión de bases de datos utilizado para almacenar los productos.
- **Pydantic**: Para la validación de los datos de entrada (modelo de producto).
- **Docker**: Utilizado para crear el contenedor de la base de datos PostgreSQL.

## Instalación
- Se debe crear una red

docker network create distribuidas

- Se descarga la imagen de Postgres

docker run -d --name db -p 5432:5432 -e POSTGRES_PASSWORD=P@SSWORD123 -e POSTGRES_USER=postgres -e POSTGRES_DB=productos -v postgres_data:/var/lib/postgresql/data --network distribuidas postgres:15

## Creación de la Base de Datos
- Se debe ingresar al bash de postgres

docker exec -it db bash

- Ingresar los siguientes comandos de datos

psql -U postgres

CREATE TABLE productos (
	id SERIAL PRIMARY KEY,
	nombre VARCHAR(255),
	cantidad INT,
	precio FLOAT
);
	
INSERT INTO productos (nombre, cantidad, precio) VALUES ('Arroz', 10, 20.5), ('Papas', 5, 15.75), ('Sopa', 3, 50.99), ('Atún', 5, 11.99), ('Fideos', 7, 13.99);

## Se descarga la imagen de API
docker run -d --name distribuidas-api  -e DATABASE_URL=postgres://postgres:P@SSWORD123@host.docker.internal:5432/productos --network distribuidas -p 8000:8000 danielllumigusin/distribuidas:latest

## Hacer pruebas de funcionamiento
- Se debe probar con las siguientes url
Para mostrar el inicio
127.0.0.1:8000/

Para mostrar todos los productos
127.0.0.1:8000/products/

Para mostrar un producto según el id
127.0.0.1:8000/product/{id}

Para crear un producto enviando los datos en el body.
127.0.0.1:8000/create-product/ + body {nombre, cantidad, precio}

Para actualizar los productos según el id enviando los nuevos datos en el body.
127.0.0.1:8000/update-product/{id} + body{nombre, cantidad, precio}

Para borrar el producto según el id.
127.0.0.1:8000/delete-product/{id}

