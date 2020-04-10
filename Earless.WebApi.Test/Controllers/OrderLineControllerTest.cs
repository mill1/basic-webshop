using Xunit;
using Earless.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Earless.WebApi.Interfaces;

namespace Earless.WebApi.Test.Controllers
{
    [Collection("Realm tests")]
    public class OrderLineControllerTest
    {
        private readonly OrderLineController orderLineController;
        private readonly IOrderLineService orderLineService;

        public OrderLineControllerTest()
        {
            orderLineService = Mocks.MockOrderLineService.GetOrderLineService();
            var logger = new Mocks.MockLogger<OrderLineController>().CreateLogger();
            orderLineController = new OrderLineController(orderLineService, 
                                  new Mapper(Mocks.MockProductService.GetProductService()),
                                  logger);            
        }

        [Fact]
        private void GetOrderLineByOrderLineNumberNotFoundTest()
        {
            IActionResult result = orderLineController.GetOrderLineByOrderLineNumber(Constants.ORDERLINE_ID_NOT_FOUND);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        private void GetOrderLineByOrderLineNumberOk()
        {
            IActionResult result = orderLineController.GetOrderLineByOrderLineNumber(Constants.ORDERLINE_ID_FOUND);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void GetOrderLineByOrderLineNumberExceptionTest()
        {
            IActionResult result = orderLineController.GetOrderLineByOrderLineNumber(Constants.ORDERLINE_ID_THROWS_EXC);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
