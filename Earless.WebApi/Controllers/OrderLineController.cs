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
    public class OrderLineController : ControllerBase
    {
        private readonly OrderLineService orderLineService;

        public OrderLineController(OrderLineService orderLineService)
        {
            this.orderLineService = orderLineService;
        }

        [HttpGet("{id}")]
        public OrderLine GetOrderLineByOrderLineNumber(int id)
        {
            if (id < 1)
                throw new Exception($"Orderline id's below 1 are not supported. Requested orderline id = {id}");

            OrderLine orderLine = orderLineService.GetOrderLine(id);

            return orderLine;
        }
    }
}
