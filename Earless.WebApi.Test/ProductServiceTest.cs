using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Earless.WebApi.Data;
using Earless.WebApi.Models;
using Earless.WebApi.Services;

namespace Earless.WebApi.Test
{
    public class ProductServiceTest : IDisposable
    {
        private readonly EarlessContext context;
        private readonly ProductService productService;
        private readonly MockFactory mockFactory;
        private readonly int productCount;

        public ProductServiceTest()
        {
            context = TestDbGenerator.CreateContext();
            productService = new ProductService(context);
            TestDbGenerator.Initialize(context);
            mockFactory = new MockFactory(context);
            productCount = context.Products.Count();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        [Fact]
        public void GetProductTestFound()
        {
            const int PRODUCT_ID = 1;
            Product product = productService.GetProduct(PRODUCT_ID);
            Assert.Equal(PRODUCT_ID, product.Id);
        }

        [Fact]
        public void GetProductTestNotFound()
        {
            Product product = productService.GetProduct(productCount + 1);
            Assert.Null(product);
        }

        [Fact]
        public void GetProductsTestCount()
        {
            var products = productService.GetProducts();
            Assert.Equal(productCount, products.Count());
        }

        [Fact]
        public void GetProductsByCategoryTestCount()
        {
            const int PRODUCTCATEGORY_ID = 1;
            int count = 0;

            var products = productService.GetProducts();

            foreach (Product product in products)
            {
                if (product.ProductCategory.Id == PRODUCTCATEGORY_ID)
                    count++;
            }

            Assert.Equal(count, productService.GetProducts(PRODUCTCATEGORY_ID).ToList().Count);
        }

        [Fact]
        public void GetProductFromOrderLineTestFound()
        {
            Product product = mockFactory.CreateOrder().OrderLines.First().Product;
            Assert.NotNull(product);
        }
    }
}
