using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using pro3.model;


namespace pro3.service{
    public interface IProductService
    {
        void AddProduct(Product product);
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}