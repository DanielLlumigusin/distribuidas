
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientTest.ServiceProduct;

namespace ClientTest
{
    public class TestProducts
    {
        public void AgregarProducto()
        {
            Product result = null;
            try
            {
                using (var r = new ServiceProduct.Service1Client())
                {
                    Product product = new Product();
                    product.ProductName = "Prueba 12";
                    product.UnitPrice = 10;
                    product.CategoryID = 1;
                    product.UnitsInStock = 10;
                    result = r.GetData(product);
                }
                Console.WriteLine("Producto Ingresado con Exito");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ingresar un producto" + ex);
            }
        }

        public void EditarProducto()
        {
            bool result = false;
            try
            {
                using (var r = new ServiceProduct.Service1Client())
                {
                    Product product = new Product();
                    product.ProductID = 8;
                    product.ProductName = "Producto Editado Prueba";
                    product.UnitPrice = 69;
                    product.CategoryID = 1;
                    product.UnitsInStock = 96;
                    result = r.UpdateData(product);
                }
                Console.WriteLine("Producto Editado con Exito");
            }
            catch (Exception)
            {
            }
        }

        public  void EliminarProducto()
        {
            bool result = false;
            try
            {
                using (var r = new ServiceProduct.Service1Client())
                {
                    Product product = new Product();
                    product.ProductID = 8;
                    product.ProductName = "Producto Editado";
                    product.UnitPrice = 99;
                    product.CategoryID = 1;
                    product.UnitsInStock = 99;
                    result = r.DeleteData(product);
                }
                Console.WriteLine("Producto eliminado con Exito");
            }
            catch (Exception)
            {
                Console.WriteLine("Error al eliminar el producto");
            }
        }

        public void FiltrarProducto()
        {
            Product[] result = null;
            try
            {
                using (var r = new ServiceProduct.Service1Client())
                {
                    Product product = new Product();
                    product.ProductID = 1;
                    product.ProductName = "ProductoActualizado";
                    product.UnitPrice = 99;
                    product.CategoryID = 1;
                    product.UnitsInStock = 99;
                    result = r.FilterProduct(product);
                }
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se puede buscar el producto" + ex);
            }
        }
    }
}
