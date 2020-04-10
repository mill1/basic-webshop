using Earless.WebApi.Models;
using System;
using System.Collections.Generic;

namespace Earless.WebApi.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();
        public IEnumerable<Product> GetProducts(int productCategoryId);
        public IEnumerable<ProductCategory> GetProductCategories();
        public Product GetProduct(int id);
    }
}
