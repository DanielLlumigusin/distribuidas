from Database.db import cursor, connection

# Leer todos los productos
def read_all():
    cursor.execute("SELECT * FROM productos")
    rows = cursor.fetchall()
    return [{"id": row[0], "nombre": row[1], "cantidad": row[2], "precio": row[3]} for row in rows]


def exist_product(id):
    query = "SELECT * FROM productos WHERE id = %s"
    cursor.execute(query, (id,))
    return cursor.fetchone() is not None

# Leer un solo producto por ID
def read_one(id):
    query = "SELECT * FROM productos WHERE id = %s"
    cursor.execute(query, (id,))
    row = cursor.fetchone()
    if row is None:
        return "No existe el producto con el id especificado"
    else:
        return {"id": row[0], "nombre": row[1], "cantidad": row[2], "precio": row[3]}

# Insertar un producto
def write_product(product):
    try:
        query = "INSERT INTO productos (nombre, cantidad, precio) VALUES (%s, %s, %s)"
        cursor.execute(query, (product['nombre'], product['cantidad'], product['precio']))
        connection.commit() 
        return "Producto guardado exitosamente"
    except Exception as e:
        connection.rollback()
        return f"Error al guardar el producto: {e}"

# Editar un producto
def edit_product(id, product):
    if exist_product(id):    
        try:
            query = """
                UPDATE productos
                SET nombre = %s, cantidad = %s, precio = %s
                WHERE id = %s
            """
            cursor.execute(query, (product['nombre'], product['cantidad'], product['precio'], id))
            connection.commit()
            return f"Producto con id {id} editado exitosamente"
        except Exception as e:
            connection.rollback()
            return f"Error al editar el producto: {e}"
    else:
        return f"No existe el producto con id {id} para editar"
    
# Eliminar un producto
def delete_product(id):
    if exist_product(id):
        try:
            query = "DELETE FROM productos WHERE id = %s"
            cursor.execute(query, (id,))
            connection.commit()
            return f"Producto con id {id} borrado exitosamente"
        except Exception as e:
            connection.rollback()
            return f"Error al eliminar el producto: {e}"
    else:
        return f"No existe el producto con id {id} para eliminar"
