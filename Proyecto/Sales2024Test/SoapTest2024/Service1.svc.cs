using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.UI.WebControls;

namespace SoapTest2024
{
      public class Service1 : IService1
    {
        public bool DeleteData(Product product)
        {
            bool result = false;
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    result = r.Delete<Product>(product);
                }
            }
            catch (Exception) {
                throw;
            }
            return result;
        }

        public List<Product> FilterProduct(Product product)
        {
            List<Product> products = null;
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    products = r.Filter<Product>(p => p.ProductName == product.ProductName).ToList();
                }
            }
            catch (Exception)
            {
                throw;    
            }
            return products;
        }

        public Product GetData(Product product)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    return r.Create(product);
                }
            }
            catch (Exception) {
                throw;

            }
        }

        

        public bool UpdateData(Product product)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    return r.Update(product);
                }
            }
            catch (Exception)
            {
                throw;   
            }
        }

        
    }
}
