using Xunit;
using Earless.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Earless.WebApi.Interfaces;
using Earless.WebApi.Test.Mocks;

namespace Earless.WebApi.Test.Controllers
{
    [Collection("Realm tests")]
    public class OrderControllerTest
    {
        private readonly OrderController orderController;
        private readonly IOrderService orderService;
        private readonly Mapper mapper;

        public OrderControllerTest()
        {
            orderService = MockOrderService.GetOrderService();
            mapper = new Mapper(MockProductService.GetProductService());
            var logger = new MockLogger<OrderController>().CreateLogger();
            orderController = new OrderController(orderService, mapper, logger);
        }

        [Fact]
        private void GetOrdersTest()
        {
            IActionResult result = orderController.Get();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void GetOrderByOrderNumberNotFoundTest()
        {
            IActionResult result = orderController.GetOrderByOrderNumber(Constants.ORDER_ID_NOT_FOUND);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        private void GetOrderByOrderNumberOk()
        {
            IActionResult result = orderController.GetOrderByOrderNumber(Constants.ORDER_ID_FOUND);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void GetOrderByOrderNumberExceptionTest()
        {
            IActionResult result = orderController.GetOrderByOrderNumber(Constants.ORDER_ID_THROWS_EXC);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        private void AddOrderDtoOrderIsNullTest()
        {
            IActionResult result = orderController.AddOrder(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        private void UpdateOrderDtoOrderIsNullTest()
        {
            IActionResult result = orderController.UpdateOrder(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        private void DeleteOrderOkTest()
        {
            IActionResult result = orderController.DeleteOrder(Constants.ORDER_ID_FOUND);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        private void DeleteOrderExceptionTest()
        {
            IActionResult result = orderController.DeleteOrder(Constants.ORDER_ID_THROWS_EXC);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
