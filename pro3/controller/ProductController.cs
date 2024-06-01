using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using pro3.model;
using pro3.service;

namespace pro3.controller
{
    public class ProductController
    {
        //Instance of model
        private IProductService productService;
        public ProductController(IProductService proService){
            productService = proService;
        }
        public void AddProduct(Product product){
            productService.AddProduct(product);;
        }
    }
}