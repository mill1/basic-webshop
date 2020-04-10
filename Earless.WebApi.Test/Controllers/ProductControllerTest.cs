using Xunit;
using Earless.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Earless.WebApi.Interfaces;

namespace Earless.WebApi.Test.Controllers
{
    [Collection("Realm tests")]
    public class ProductControllerTest
    {
        private readonly ProductController productController;

        public ProductControllerTest()
        {
            IProductService productService = Mocks.MockProductService.GetProductService();
            var logger = new Mocks.MockLogger<ProductController>().CreateLogger();
            productController = new ProductController(productService, new Mapper(productService),logger);
        }

        [Fact]
        private void GetProductsTest()
        {
            IActionResult result = productController.Get();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void GetProductCategoriesTest()
        {
            IActionResult result = productController.GetProductCategories();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void GetProductByProductNumberNotFoundTest()
        {
            IActionResult result = productController.GetProductByProductNumber(Constants.PRODUCT_ID_NOT_FOUND);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        private void GetProductByProductNumberOk()
        {
            IActionResult result = productController.GetProductByProductNumber(Constants.PRODUCT_ID_FOUND);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void GetProductByProductNumberExceptionTest()
        {
            IActionResult result = productController.GetProductByProductNumber(Constants.PRODUCT_ID_THROWS_EXC);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
