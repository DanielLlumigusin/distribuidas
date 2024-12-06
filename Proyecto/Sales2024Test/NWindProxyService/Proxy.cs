using System;
using System.Collections.Generic;
using System.Linq;
using SLC;
using Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace NWindProxyService
{
    public class Proxy : IService
    {
        string BaseAddress = "http://localhost:50508";
    }

    public async Task<T> SendPost<T, PostData>(string requestURI, PostData data) {
            T Result = default(T);
            using (var Client = new HttpClient())
            {
                try
                {
                    //URL Absoluto
                    requestURI = BaseAddress + requestURI;
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add
                        (new MediaTypeWithQualityHeaderValue("application/json"));

                    var JSONData = JsonConvert.SerializeObject(data);
                    HttpResponseMessage Reponse =
                        await Client.PostAsync(requestURI,
                        new StringContent(JSONData.ToString(),
                        Encoding.UTF8, "application/json"));

                    var ResultWebAPI = await Response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<T>(ResultWebAPI);
                }
                catch (Exception ex) { }
            }
            return Result;
        }

        public async Task<T> SendGet<T>(string requestURI)
        {
            T Result = default(T);
            using (var Client = new HttpClient())
            {
                try
                {
                    requestURI = BaseAddress + requestURI;
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var ResultJSON = await Client.GetStringAsync(requestURI);
                    Result = JsonConvert.DeserializeObject<T>(ResultJSON);
                } catch (Exception ex) { }
            }
            return Result; 
        }

        public async Task<Product> CreateProductAsync(Product newProduct)
        {
            return await SendPost<Product, Product>
                ("/api/nwind/createproduct", newProduct);
        }

        public Product CreateProduct(Product newProduct)
        {
            Product Result = null;
            //Ejecutar la tarea en un nuevo hilo
            //para que no se bloquee el hilo sincrono
            //con wait esperamos la iperacion asincrona
            Task.Run(async () => Result =
            await CreateProductAsync(newProduct)).Wait();
            return Result;
        }

        public async Task<Product> RetrieveProductByIDAsync(int ID)
        {
            return await SendGet<Product>($"/api/nwind/RetrieveProductByID/{ID}");
        }

        public Product RetrieveProductByID(int ID)
        {
            Product result = null;
            Task.Run(async () =>
                result = await RetrieveProductByIDAsync(ID)).Wait();
            return result;
        }

        public async Task<bool> UpdateProductAsync(Product productToUpdate)
        {
            return await SendPost<bool, Product>
                ("/api/nwind/UpdateProduct", productToUpdate);
        }

        public bool UpdateProduct(Product productToUpdate)
        {
            bool Result = false;
            Task.Run(async () => Result = await
            UpdateProductAsync(productToUpdate)).Wait();
            return Result;
        }

        public async Task<List<Product>> FilterProductsByCategoryIDAsync(int ID)
        {
            return await SendGet<List<Product>>
                ($"/api/nwind/FilterProductsByCategoryID/{ID}");
        }

        public List<Product> FilterProductByCategoryID(int ID) { 
            List<Product> Result = null;
            Task.Run(async () => Result = await
            FilterProductsByCategoryIDAsync (ID)).Wait();
            return Result;
        }

        public async Task<Category> CreateCategoryAsync(Category newCategory)
        {
            return await SendPost<Category, Category>
                ("/api/nwind/CreateCategory",newCategory);
        }

        public Category CreateCategory(Category newCategory) 
        { 
            Category Result = null;
            Task.Run(async() => Result = await CreateCategoryAsync(newCategory)).Wait();
            return Result;
        }
    }
}
