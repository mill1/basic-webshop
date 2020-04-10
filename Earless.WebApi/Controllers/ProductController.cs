using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Earless.WebApi.Models;
using Microsoft.Extensions.Logging;
using Earless.WebApi.Interfaces;

namespace Earless.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly Mapper mapper;
        private readonly ILogger<ProductController> logger;

        public ProductController(IProductService productService, Mapper mapper, ILogger<ProductController> logger)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(mapper.MapModelToDto(productService.GetProducts()));
        }

        [HttpGet("{id}")]
        public IActionResult GetProductByProductNumber(int id)
        {
            try
            {
                Product product = productService.GetProduct(id);

                if (product == null)
                    return NotFound($"The product was not found. Requested product id = {id}.");
            
                return Ok(mapper.MapModelToDto(product));
            }
            catch (Exception e)
            {
                string message = $"Getting the product failed. Requested product id = {id}.";
                logger.LogError($"{message}\r\n{e.Message}", e);
                return BadRequest(message);
            }
        }

        [HttpGet("Categories")]
        public IActionResult GetProductCategories()
        {
            return Ok(mapper.MapModelToDto(productService.GetProductCategories()));
        }
    }
}
