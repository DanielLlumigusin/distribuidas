using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SoapTest2024Category
{
       public class Service1 : IService1
    {
        public bool DeleteDataCategory(Category category)
        {
            bool result = false;
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    result = r.Delete<Category>(category);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public List<Category> FilterCategory(Category category)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    return r.Filter<Category>(p => p.CategoryName == category.CategoryName).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Category GetDataCategory(Category category)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    return r.Create(category);
                }
            }
            catch (Exception)
            {
                throw;

            }
        }

        public bool UpdateDataCategory(Category category)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    return r.Update(category);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
