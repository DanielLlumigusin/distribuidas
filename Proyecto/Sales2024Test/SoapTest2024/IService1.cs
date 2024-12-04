using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SoapTest2024
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        List<Product> FilterProduct(Product product);

        [OperationContract]
        Product GetData(Product product);
        [OperationContract]

        bool UpdateData(Product product);
        [OperationContract]
        bool DeleteData(Product product);

    }
}