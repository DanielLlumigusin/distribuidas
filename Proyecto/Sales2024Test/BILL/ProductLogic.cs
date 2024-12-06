using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILL
{
    public class ProductLogic
    {
        public Product Create(Product product)
        {
            Product res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                Product result = r.Retrieve<Product>(p => p.ProductName == product.ProductName);
                if (result == null)
                {
                    res = r.Create(product);
                }
            }
                return res;
        }

        public Product RetrieveById(int id) {
            Product res = null;
            using (var r = RepositoryFactory.CreateRepository()) { 
                res = r.Retrieve<Product>(p=>p.ProductID == id);
            }
            return res;
        }

        public bool Update(Product product) {
            bool res = false;
            using (var r = RepositoryFactory.CreateRepository()) {
                Product temp = r.Retrieve<Product>
                    (p => p.ProductName == product.ProductName && p.ProductID != product.ProductID);
                if (temp == null)
                {
                    res = r.Update(product);
                }
            }
            return res;
        }

        public bool Delete(int id)
        {
            bool res = false;
            var product = RetrieveById(id);
            if (product != null)
            {
                if (product.UnitsInStock == 0)
                {
                    using (var r = RepositoryFactory.CreateRepository())
                    {
                        res = r.Delete(product);
                    }
                }
                else
                {

                }
            }
            else
            {

            }
            return res;
        }

        public List<Product> Filter(string name) {
            List<Product> res = null;
            using (var r = RepositoryFactory.CreateRepository()) {
                res = r.Filter<Product>(
                    p => p.ProductName == name);
            }
            return res;
        }

    }
}
