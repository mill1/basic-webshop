using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Earless.WebApi.Data;
using Earless.WebApi.Models;
using MoreLinq;
using Earless.WebApi.Interfaces;

namespace Earless.WebApi.Services
{
    public class ProductService: IProductService
    {
        private readonly EarlessContext context;

        public ProductService(EarlessContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.Products
                .Include(p => p.ProductCategory);
        }

        public IEnumerable<Product> GetProducts(int productCategoryId)
        {
            return GetProducts().Where(p => p.ProductCategory.Id == productCategoryId);
        }

        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return context.ProductCategories;
        }

        public Product GetProduct(int id)
        {
            return GetProducts().Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
