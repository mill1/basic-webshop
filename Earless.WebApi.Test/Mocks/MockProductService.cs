using Earless.WebApi.Interfaces;
using Earless.WebApi.Models;
using System;
using System.Collections.Generic;
using Moq;
using System.Linq;

namespace Earless.WebApi.Test.Mocks
{
    public static class MockProductService
    {
        public static IProductService GetProductService()
        {
            Mock<IProductService> mockProductService = new Mock<IProductService>();

            Product nullProduct = null;

            mockProductService.Setup(s => s.GetProducts()).Returns(GetProducts());
            mockProductService.Setup(s => s.GetProductCategories()).Returns(GetProductCategories());
            mockProductService.Setup(s => s.GetProduct(Constants.PRODUCT_ID_NOT_FOUND)).Returns(nullProduct);
            mockProductService.Setup(s => s.GetProduct(Constants.PRODUCT_ID_FOUND)).Returns(GetExistingProduct(Constants.PRODUCT_ID_FOUND));
            mockProductService.Setup(s => s.GetProduct(Constants.PRODUCT_ID_THROWS_EXC)).Throws(new Exception("Mock exception: GetProduct(int id)"));

            return mockProductService.Object;
        }

        private static Product GetExistingProduct(int Id)
        {
            var productCategories = MockFactory.GetProductCategories(true);

            IEnumerable<Product> products = MockFactory.GetProducts(true, productCategories);

            return products.Where(p => p.Id == Id).First();
        }

        private static IEnumerable<Product> GetProducts()
        {
            var productCategories = MockFactory.GetProductCategories(true);

            return MockFactory.GetProducts(true, productCategories);
        }

        private static IEnumerable<ProductCategory> GetProductCategories()
        {
            return MockFactory.GetProductCategories(true);
        }
    }
}
