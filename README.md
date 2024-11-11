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

### Requisitos previos

- **Python 3.8 o superior**
- **Docker**

