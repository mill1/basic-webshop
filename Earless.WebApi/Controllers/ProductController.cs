using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Earless.WebApi.Models;
using Earless.WebApi.Services;

namespace Earless.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return productService.GetProducts();
        }

        [HttpGet("{id}")]
        public Product GetProductByProductNumber(int id)
        {
            if (id < 1)
                throw new Exception($"Product id's below 1 are not supported. Requested product id = {id}");

            Product product = productService.GetProduct(id);

            return product;
        }

        [HttpGet("GetByCategory/{productCategoryId}")]
        public IEnumerable<Product> GetProductByProductCategory(int productCategoryId)
        {
            if (productCategoryId < 1)
                throw new Exception($"Productcategory id's below 1 are not supported. Requested productcategory id = {productCategoryId}");

            return productService.GetProducts(productCategoryId); ;
        }

        [HttpGet("Categories")]
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return productService.GetProductCategories();
        }


    }
}
