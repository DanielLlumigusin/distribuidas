using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientTest.ServiceCategory;

namespace ClientTest
{
    public class TestCategory
    {
        public void AgregarCategoria()
        {
            Category result = null;
            try
            {
                using (var r = new ServiceCategory.Service1Client())
                {
                    Category category = new Category();
                    category.CategoryName = "Categoria Prueba";
                    category.Description = "Descripcion Prueba";
                    result = r.GetDataCategory(category);
                }
                Console.WriteLine("Prueba Exitosa");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Prueba Fallida" + ex);
            }
        }

        public void EditarCategoria()
        {
            bool result = false;
            try
            {
                using (var r = new ServiceCategory.Service1Client())
                {
                    Category category = new Category();
                    category.CategoryID = 3;
                    category.CategoryName = "Categoria Editada Feli";
                    result = r.UpdateDataCategory(category);
                }
                Console.WriteLine("Categoria Editado con Exito");
            }
            catch (Exception)
            {
            }
        }

        public void EliminarCategoria()
        {
            bool result = false;
            try
            {
                using (var r = new ServiceCategory.Service1Client())
                {
                    Category category = new Category();
                    category.CategoryID = 3;
                    category.CategoryName = "Categoria Editado";
                    category.Description = "Description1";
                    result = r.DeleteDataCategory(category);
                }
                Console.WriteLine("Categoria eliminado con Exito");
            }
            catch (Exception)
            {
                Console.WriteLine("Error al eliminar el categoria");
            }
        }

        public void FiltrarCategoria()
        {
            Category[] result = null;
            try
            {
                using (var r = new ServiceCategory.Service1Client())
                {
                    Category category = new Category();
                    category.CategoryID = 1;
                    category.CategoryName = "CategoriaActualizado";
                    result = r.FilterCategory(category);
                }
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se puede buscar la categoria" + ex);
            }
        }
    }
}
