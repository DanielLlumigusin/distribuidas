using ClientTest.ServiceProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    public class Program 
    {
        static void Main(string[] args)
        {
            //Instanciar Clases con los metodos
            TestProducts testProducts = new TestProducts();
            TestCategory testCategory = new TestCategory();
            
            //CRUD PRODUCTOS
            //testProducts.AgregarProducto();
            //testProducts.EditarProducto();
            //testProducts.EliminarProducto();
            //testProducts.FiltrarProducto();
            //CRUD CATEGORIA
            //testCategory.AgregarCategoria();
            //testCategory.EditarCategoria();
            //testCategory.EliminarCategoria();
            //testCategory.FiltrarCategoria();
        }



    }
}
