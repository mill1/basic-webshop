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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly Mapper mapper;
        private readonly ILogger<OrderController> logger;

        public OrderController(IOrderService orderService, Mapper mapper, ILogger<OrderController> logger)
        {
            this.orderService = orderService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(mapper.MapModelToDto(orderService.GetOrders()));
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderByOrderNumber(int id)
        {
            try
            {
                Order order = orderService.GetOrder(id);

                if (order == null)
                    return NotFound($"The order was not found. Requested order id = {id}.");

                return Ok(mapper.MapModelToDto(order));
            }
            catch (Exception e)
            {
                string message = $"Getting the order failed. Requested order id = {id}.";
                logger.LogError($"{message}\r\n{e.Message}", e);
                return BadRequest(message);
            }
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] DTO.Order orderDto)
        {
            if (orderDto == null)
                return BadRequest("Order object to add is null.");

            try
            {
                Order order = orderService.AddOrder(mapper.MapDtoToModel(orderDto));

                return Ok(mapper.MapModelToDto(order));
            }
            catch (Exception e)
            {
                string message = "Adding the order failed.";
                logger.LogError($"{message}\r\n{e.Message}", e);
                return BadRequest(message);
            }
        }

        [HttpPut]
        public IActionResult UpdateOrder([FromBody] DTO.Order orderDto)
        {
            if (orderDto == null)
                return BadRequest("Order object to update is null.");

            try
            {
                Order order = orderService.UpdateOrder(mapper.MapDtoToModel(orderDto));
                return Ok(mapper.MapModelToDto(order));
            }
            catch (Exception e)
            {
                string message = $"Updating the order failed. Order id = {orderDto.Id}.";
                logger.LogError($"{message}\r\n{e.Message}", e);
                return BadRequest(message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                orderService.DeleteOrder(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                string message = $"Deleting the order failed. Requested order id = {id}.";
                logger.LogError($"{message}\r\n{e.Message}", e);
                return BadRequest(message);
            }
        }
    }
}
