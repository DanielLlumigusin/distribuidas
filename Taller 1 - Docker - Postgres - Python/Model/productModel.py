from pydantic import BaseModel

class Product(BaseModel):
    nombre: str
    cantidad: int
    precio: float