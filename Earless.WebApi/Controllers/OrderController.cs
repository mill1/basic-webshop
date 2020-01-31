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
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;
        private readonly ProductService productService;

        public OrderController(OrderService orderService, ProductService productService)
        {
            this.orderService = orderService;
            this.productService = productService;
        }

        [HttpGet]
        public IEnumerable<DTO.Order> Get()
        {
            IEnumerable<Order> orders =  orderService.GetOrders();

            return MapModelToDto(orders);
        }

        [HttpGet("{id}")]
        public DTO.Order GetOrderByOrderNumber(int id)
        {
            if (id < 1)
                throw new Exception($"Order id's below 1 are not supported. Requested order id = {id}");

            Order order = orderService.GetOrder(id);

            return MapModelToDto(order);
        }

        [HttpPost]
        public DTO.Order AddOrder([FromBody] DTO.Order orderDto)
        {
            if (orderDto == null)
                throw new Exception("Order object to add cannot be null.");

            Order order = orderService.AddOrder(MapDtoToModel(orderDto));

            return MapModelToDto(order);
        }

        [HttpPut]
        public DTO.Order UpdateOrder([FromBody] DTO.Order orderDto)
        {
            if (orderDto == null)
                throw new Exception("Order object to update cannot be null.");

            Order order =  orderService.UpdateOrder(MapDtoToModel(orderDto));
            return MapModelToDto(order);
        }

        [HttpDelete("{id}")]
        public int DeleteOrder(int id)
        {
            if (id < 1)
                throw new Exception($"Order id's below 1 are not supported. Requested order id = {id}");

            orderService.DeleteOrder(id);
            return id;
        }

        private IEnumerable<DTO.Order> MapModelToDto(IEnumerable<Order> orders)
        {
            return orders.Select(o => MapModelToDto(o));
        }

        private DTO.Order MapModelToDto(Order order)
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

        private Order MapDtoToModel(DTO.Order orderDto)
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
    }
}
