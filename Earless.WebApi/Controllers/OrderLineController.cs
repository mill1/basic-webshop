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
    public class OrderLineController : ControllerBase
    {
        private readonly IOrderLineService orderLineService;
        private readonly Mapper mapper;
        private readonly ILogger<OrderLineController> logger;

        public OrderLineController(IOrderLineService orderLineService, Mapper mapper, ILogger<OrderLineController> logger)
        {
            this.orderLineService = orderLineService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderLineByOrderLineNumber(int id)
        {
            try
            {
                OrderLine orderLine = orderLineService.GetOrderLine(id);

                if (orderLine == null)
                    return NotFound($"The orderline was not found. Requested orderline id = {id}.");

                return Ok(mapper.MapModelToDto(orderLine));
            }
            catch (Exception e)
            {
                string message = $"Getting the orderline failed. Requested orderline id = {id}.";
                logger.LogError($"{message}\r\n{e.Message}", e);
                return BadRequest(message);
            }
        }
    }
}
