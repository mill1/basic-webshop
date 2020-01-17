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

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return orderService.GetOrders();
        }

        [HttpGet("{id}")]
        public Order GetOrderByOrderNumber(int id)
        {
            if (id < 1)
                throw new Exception($"Order id's below 1 are not supported. Requested order id = {id}");

            Order order = orderService.GetOrder(id);

            return order;
        }

        [HttpPost]
        public Order AddOrder([FromBody] Order order)
        {
            if (order == null)
                throw new Exception("Order object to add cannot be null.");

            return orderService.AddOrder(order);
        }

        [HttpPut]
        public Order UpdateOrder([FromBody] Order order)
        {
            if (order == null)
                throw new Exception("Order object to update cannot be null.");

            orderService.UpdateOrder(order);
            return order;
        }

        [HttpDelete("{id}")]
        public int DeleteOrder(int id)
        {
            if (id < 1)
                throw new Exception($"Order id's below 1 are not supported. Requested order id = {id}");

            orderService.DeleteOrder(id);
            return id;
        }
    }
}
