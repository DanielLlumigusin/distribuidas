# Usa la imagen oficial de Python
FROM python:3.11-slim

# Establece el directorio de trabajo en el contenedor
WORKDIR /app

# Copia los archivos del proyecto al contenedor
COPY . /app

# Instala las dependencias de Python
RUN pip install --no-cache-dir fastapi uvicorn psycopg2-binary

# Expone el puerto en el que correrá FastAPI
EXPOSE 8000

# Comando para iniciar el servidor de FastAPI
CMD ["uvicorn", "app:app", "--host", "0.0.0.0", "--port", "8000", "--reload"]
