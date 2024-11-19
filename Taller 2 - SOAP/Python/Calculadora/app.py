from zeep import Client
from zeep.exceptions import Fault

# Get WSDL location
wsdl_doc = "http://www.dneonline.com/calculator.asmx?WSDL"

# Create a SOAP client
client = Client(wsdl_doc)

# suma
try:
    result = client.service.Add(5, 8)
    print(f"Resultado de la suma (5 + 8): {result}")
except Fault as e:
    print(f"Error en la suma: {e}")

# resta
try:
    result = client.service.Subtract(10, 8)
    print(f"Resultado de la resta (10 - 8): {result}")
except Fault as e:
    print(f"Error en la resta: {e}")

# multiplicación
try:
    result = client.service.Multiply(5, 8)
    print(f"Resultado de la multiplicación (5 * 8): {result}")
except Fault as e:
    print(f"Error en la multiplicación: {e}")

# división
try:
    result = client.service.Divide(10, 2)
    print(f"Resultado de la división (10 / 2): {result}")
except Fault as e:
    print(f"Error en la división: {e}")
