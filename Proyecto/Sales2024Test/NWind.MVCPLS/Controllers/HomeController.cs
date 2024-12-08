using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NWindProxyService;
using Entities;
using System.Linq;

namespace NWind.MVCPLS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpPost]
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return View(); // Muestra el formulario para ingresar el ID de categoría.
            }

            var Proxy = new Proxy();
            var Products = Proxy.FilterProductByCategoryID(id.Value);

            if (Products == null || !Products.Any())
            {
                ViewBag.Message = "No se encontraron productos para esta categoría.";
                return View("ProductList", new List<Product>()); // Envía una lista vacía.
            }

            return View("ProductList", Products);
        }


        [HttpGet]
        public ActionResult Detail(int id)
        {
            var Proxy = new Proxy();
            var Model = Proxy.RetrieveProductByID(id);
            return View(Model);
        }

        public ActionResult CUD(int? id)
        {
            var proxy = new Proxy();
            var model = new Product();

            if (id.HasValue)
            {
                model = proxy.RetrieveProductByID(id.Value);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult CUD(Product newProduct, string CreateBtn, string UpdateBtn, string DeleteBtn)
        {
            var proxy = new Proxy();
            ActionResult result = View();
            Product product;

            if (CreateBtn != null)
            {
                product = proxy.CreateProduct(newProduct);
                if (product != null)
                {
                    result = RedirectToAction("CUD", new { id = product.ProductID });
                }
            }
            else if (UpdateBtn != null)
            {
                bool isUpdated = proxy.UpdateProduct(newProduct);
                if (isUpdated)
                {
                    result = Content("El producto se ha actualizado correctamente.");
                }
            }
            else if (DeleteBtn != null)
            {
                bool isDeleted = proxy.Delete(newProduct.ProductID);
                if (isDeleted)
                {
                    result = Content("El producto se ha eliminado correctamente.");
                }
            }

            return result;
        }

    }
}
