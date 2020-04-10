using Earless.WebApi.Interfaces;
using Earless.WebApi.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Moq;

namespace Earless.WebApi.Test.Mocks
{
    public static class MockOrderLineService
    {
        public static IOrderLineService GetOrderLineService()
        {
            Mock<IOrderLineService> mockOrderLineService = new Mock<IOrderLineService>();

            OrderLine nullOrderLine = null;            
            
            mockOrderLineService.Setup(s => s.GetOrderLine(Constants.ORDERLINE_ID_NOT_FOUND)).Returns(nullOrderLine);
            mockOrderLineService.Setup(s => s.GetOrderLine(Constants.ORDERLINE_ID_FOUND)).Returns(GetExistingOrderLine(Constants.ORDERLINE_ID_FOUND));
            mockOrderLineService.Setup(s => s.GetOrderLine(Constants.ORDERLINE_ID_THROWS_EXC)).Throws(new Exception("Mock exception: GetOrderLine(int id)"));

            return mockOrderLineService.Object;
        }

        public static OrderLine GetExistingOrderLine(int Id)
        {
            var products = MockFactory.GetProducts(true, MockFactory.GetProductCategories(true));

            return CreateOrderLine(products, Id, Constants.PRODUCT_ID_FOUND, 1, 1);
        }

        public static OrderLine CreateOrderLine(IEnumerable<Product> products, int Id, int productId, int quantity, int fulfilled)
        {
            return new OrderLine()
            {
                Id = Id,
                Quantity = quantity,
                Fulfilled = fulfilled,
                Product = products.Where(p => p.Id == productId).First()
            };
        }
    }
}
