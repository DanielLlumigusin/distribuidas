from fastapi import FastAPI
from Model.productModel import Product
from Controllers.ControllerProducts import read_all, read_one, write_product, edit_product, delete_product
app = FastAPI()


@app.get("/")
def home():
    return {
        "Inicio": {
            "mensaje": "Hola, estamos en el microservicio del grupo 5",
            "integrantes": ["Richard Alban","Víctor Camacho", "Daniel Llumigusín", "Michelle Yánez"],
            "endpoints": {
                "GET-ALL": "/products",
                "GET-ONE": "/products/{id}",
                "POST": "/create-product/",
                "DELETE": "/delete-product/{id}",
                "UPDATE": "/update-product/{id}"
            }
        }
    }

@app.get("/products")
def show_all():
    return read_all()

@app.get("/product/{id}")
def get_product(id: int):
    return read_one(id)

@app.post("/create-product/")
def create_product(product: Product):
    return write_product(product.dict())

@app.put("/update-product/{id}")
def update_product(id: int, product: Product):
    return edit_product(id, product.dict())

@app.delete("/delete-product/{id}")
def del_product(id: int):
    return delete_product(id)
