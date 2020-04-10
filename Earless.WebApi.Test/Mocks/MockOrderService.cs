using Earless.WebApi.Interfaces;
using Earless.WebApi.Models;
using System;
using System.Collections.Generic;
using Moq;
using System.Linq;

namespace Earless.WebApi.Test.Mocks
{
    public static class MockOrderService
    {
        public static readonly Order NewOrder = GetOrder(0);
        public static readonly Order FirstOrder = GetOrder(Constants.ORDER_ID_FOUND);
        public static readonly DTO.Order NewOrderDto = GetNewOrderDtoMock();

        public static IOrderService GetOrderService()
        {
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();

            Order nullOrder = null;

            mockOrderService.Setup(s => s.GetOrders()).Returns(GetOrders());
            mockOrderService.Setup(s => s.GetOrder(Constants.ORDER_ID_NOT_FOUND)).Returns(nullOrder);
            mockOrderService.Setup(s => s.GetOrder(Constants.ORDER_ID_FOUND)).Returns(FirstOrder);
            mockOrderService.Setup(s => s.GetOrder(Constants.ORDER_ID_THROWS_EXC)).Throws(new Exception("Mock exception: GetOrder(int id)"));
            mockOrderService.Setup(s => s.AddOrder(NewOrder)).Returns(FirstOrder); // Does not work: mapper returns a new object.
            mockOrderService.Setup(s => s.DeleteOrder(Constants.ORDER_ID_THROWS_EXC)).Throws(new Exception("Mock exception: DeleteOrder(int id)"));

            return mockOrderService.Object;
        }

        private static DTO.Order GetNewOrderDtoMock()
        {
            IProductService productService = Mocks.MockProductService.GetProductService();
            Mapper mapper = new Mapper(productService);

            var order = GetOrder(0);

            return mapper.MapModelToDto(order);
        }

        private static IEnumerable<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            
            for (int i = 0; i < 3; i++)
            {
                var order = GetOrder(i + 1);
                orders.Add(order);
            }
            return orders;
        }

        private static Order GetOrder(int Id)
        {
            var products = MockFactory.GetProducts(true, MockFactory.GetProductCategories(true));

            var order = CreateOrder(products);

            order.Id = Id;

            // Update the orderline id's with test values
            order.OrderLines = order.OrderLines.Select((ol, i) => {ol.Id = (i + 1) * 2 - 1;  return ol; }).ToList();

            return order;
        }

        public static Order CreateOrder(IEnumerable<Product> products)
        {
            return (new Order()
            {
                Date = new DateTime(2020, 1, 14),
                Remark = "Twee keer bellen.",
                OrderLines = new List<OrderLine>()
                {
                    MockOrderLineService.CreateOrderLine(products, 0, 1, 1, 1),
                    MockOrderLineService.CreateOrderLine(products, 0, 3, 4, 2),
                    MockOrderLineService.CreateOrderLine(products, 0, 4, 2, 2)
                }
            });
        }
    }
}
