using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Earless.WebApi.Data;
using Earless.WebApi.Models;
using Earless.WebApi.Services;

namespace Earless.WebApi.Test
{
    public class OrderLineServiceTest : IDisposable
    {
        private readonly EarlessContext context;
        private readonly OrderService orderService;
        private readonly OrderLineService orderLineService;
        private readonly MockFactory mockFactory;

        public OrderLineServiceTest()
        {
            context = TestDbGenerator.CreateContext();
            orderLineService = new OrderLineService(context);
            orderService = new OrderService(context, orderLineService);
            TestDbGenerator.Initialize(context);
            mockFactory = new MockFactory(context);
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

            order.OrderLines.Add(new OrderLine 
                                 { 
                                    Product = new Product
                                            {
                                                Name = "Hoorbatterijen 10 geel",
                                                Description = "Tien gele hoorbatterijen",
                                                Price = 1.49,
                                            },           
                                    Quantity = 2 });
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
