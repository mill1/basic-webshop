using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Earless.WebApi.Data;
using Earless.WebApi.Models;
using Earless.WebApi.Services;
using Earless.WebApi.Test.Mocks;
using Earless.WebApi.Interfaces;

namespace Earless.WebApi.Test.Services
{
    [Collection("Realm tests")]
    public class OrderLineServiceTest : IDisposable
    {
        private readonly EarlessContext context;
        private readonly IOrderService orderService;
        private readonly IOrderLineService orderLineService;
        private readonly MockFactory mockFactory = new MockFactory();

        public OrderLineServiceTest()
        {
            context = mockFactory.InitializeContext();
            orderLineService = new OrderLineService(context);
            orderService = new OrderService(context, orderLineService);            
        }

        public void Dispose()
        {
            context.Dispose();
        }

        [Fact]
        public void GetOrderLineTestFound()
        {
            orderService.AddOrder(mockFactory.CreateOrder());

            int Id = context.OrderLines.First().Id;

            OrderLine orderLine = orderLineService.GetOrderLine(Id);

            Assert.NotNull(orderLine);
        }

        [Fact]
        public void GetOrderLineTestNotFound()
        {
            orderService.AddOrder(mockFactory.CreateOrder());

            int Id = context.OrderLines.Max(ol => ol.Id);

            OrderLine orderLine = orderLineService.GetOrderLine(Id + 1);

            Assert.Null(orderLine);
        }

        [Fact]
        public void UpdateOrderTestAddedOrderlines()
        {
            Order order = orderService.AddOrder(mockFactory.CreateOrder());

            order.OrderLines.Add(mockFactory.CreateOrderLine(0, 2, 1, 0));
            context.SaveChanges();

            Order updatedOrder = orderService.UpdateOrder(order);

            Assert.Equal(order.OrderLines.Count, updatedOrder.OrderLines.Count);
        }

        [Fact]
        public void UpdateOrderTestUpdatedOrderlines()
        {
            Order order = orderService.AddOrder(mockFactory.CreateOrder());

            order.OrderLines.First().Quantity = order.OrderLines.First().Quantity + 1;
            context.SaveChanges();

            Order updatedOrder = orderService.UpdateOrder(order);

            Assert.Equal(order.OrderLines.First().Quantity, updatedOrder.OrderLines.First().Quantity);
        }

        [Fact]
        public void UpdateOrderTestDeletedOrderlines()
        {
            Order order = orderService.AddOrder(mockFactory.CreateOrder());

            order.OrderLines.Remove(order.OrderLines.ElementAt(1));
            context.SaveChanges();

            Order updatedOrder = orderService.UpdateOrder(order);

            Assert.Equal(order.OrderLines.Count, updatedOrder.OrderLines.Count);
        }
    }
}
