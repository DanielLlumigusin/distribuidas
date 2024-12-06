﻿using BILL;
using Entities;
using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Service.Controllers
{
    public class ProductController : ApiController, IProductService
    {
        [HttpPost]
        public Product CreateProduct(Product products)  
        {
            //var productLogic = new ProductLogic();
            //var product = productLogic.Create(product);
            //return product;
            return products;
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var productLogic = new ProductLogic();
            var product = productLogic.Delete(id);
            return product;
        }
    }
}