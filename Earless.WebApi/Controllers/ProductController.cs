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
        public IEnumerable<DTO.Product> Get()
        {
            return MapModelToDTO(productService.GetProducts());
        }

        [HttpGet("{id}")]
        public DTO.Product GetProductByProductNumber(int id)
        {
            if (id < 1)
                throw new Exception($"Product id's below 1 are not supported. Requested product id = {id}");

            DTO.Product product = MapModelToDTO(productService.GetProduct(id));

            return product;
        }

        /*
         * Obsolete. See products.component.ts.getProductsPerCategory()
         */
        //[HttpGet("GetByCategory/{productCategoryId}")]
        //public IEnumerable<DTO.Product> GetProductByProductCategory(int productCategoryId)
        //{
        //    if (productCategoryId < 1)
        //        throw new Exception($"Productcategory id's below 1 are not supported. Requested productcategory id = {productCategoryId}");

        //    return MapModelToDTO(productService.GetProducts(productCategoryId));
        //}

        [HttpGet("Categories")]
        public IEnumerable<DTO.ProductCategory> GetProductCategories()
        {
            return MapModelToDTO(productService.GetProductCategories());
        }

        private IEnumerable<DTO.ProductCategory> MapModelToDTO(IEnumerable<ProductCategory> productCategories)
        {
            return productCategories.Select(pc => new DTO.ProductCategory { Id = pc.Id, Name = pc.Name });
        }

        private DTO.Product MapModelToDTO(Product product)
        {
            return new DTO.Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ProductCategoryId = product.ProductCategory.Id
            };
        }

        private IEnumerable<DTO.Product> MapModelToDTO(IEnumerable<Product> products)
        {
            return products.Select(p => MapModelToDTO(p));
        }
    }
}
