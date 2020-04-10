using System;
using System.Collections.Generic;
using System.Linq;
using Earless.WebApi.Interfaces;
using Earless.WebApi.Models;

namespace Earless.WebApi
{
    public class Mapper
    {
        private readonly IProductService productService;

        public Mapper(IProductService productService)
        {
            this.productService = productService;
        }

        #region Order mappings

        public IEnumerable<DTO.Order> MapModelToDto(IEnumerable<Order> orders)
        {
            return orders.Select(o => MapModelToDto(o));
        }

        public DTO.Order MapModelToDto(Order order)
        {
            return new DTO.Order
            {
                Id = order.Id,
                Date = order.Date,
                Remark = order.Remark,
                OrderLines = order.OrderLines.Select(ol =>
                {
                    return new DTO.OrderLine
                    {
                        Id = ol.Id,
                        ProductId = ol.Product.Id,
                        Quantity = ol.Quantity,
                        Fulfilled = ol.Fulfilled
                    };
                }).ToList()
            };
        }

        public Order MapDtoToModel(DTO.Order orderDto)
        {
            return new Order
            {
                Id = orderDto.Id,
                Date = orderDto.Date,
                Remark = orderDto.Remark,
                OrderLines = orderDto.OrderLines.Select(ol =>
                {
                    return new OrderLine
                    {
                        Id = ol.Id,
                        Product = productService.GetProduct(ol.ProductId),
                        Quantity = ol.Quantity,
                        Fulfilled = ol.Fulfilled
                    };
                }).ToList()
            };
        }
        #endregion

        #region OrderLine mappings

        public DTO.OrderLine MapModelToDto(OrderLine orderLine)
        {
            return new DTO.OrderLine
            {
                Id = orderLine.Id,
                ProductId = orderLine.Product.Id,
                Quantity = orderLine.Quantity,
                Fulfilled = orderLine.Fulfilled
            };
        }
        #endregion

        #region Product mappings

        public DTO.Product MapModelToDto(Product product)
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

        public IEnumerable<DTO.Product> MapModelToDto(IEnumerable<Product> products)
        {
            return products.Select(p => MapModelToDto(p));
        }

        public IEnumerable<DTO.ProductCategory> MapModelToDto(IEnumerable<ProductCategory> productCategories)
        {
            return productCategories.Select(pc => new DTO.ProductCategory { Id = pc.Id, Name = pc.Name });
        }
        #endregion
    }
}