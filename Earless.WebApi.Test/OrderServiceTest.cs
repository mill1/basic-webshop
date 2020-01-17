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
    public class OrderServiceTest : IDisposable
    {
        private readonly EarlessContext context;
        private readonly OrderService orderService;
        private readonly OrderLineService orderLineService;
        private readonly MockFactory mockFactory;

        public OrderServiceTest()
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
        public void GetOrderTestFound()
        {
            orderService.AddOrder(mockFactory.CreateOrder());

            Order order = orderService.GetOrder(1);
            Assert.Equal(1, order.Id);
        }

        [Fact]
        public void GetOrderTestNotFound()
        {
            Order order = orderService.GetOrder(1);
            Assert.Null(order);
        }

        [Fact]
        public void AddOrderTest()
        {
            Order order = orderService.AddOrder(mockFactory.CreateOrder());

            Assert.Equal(1, order.Id);
        }


        [Fact]
        public void GetOrdersTestCount()
        {
            int orderCount = 3;

            for (int i = 0; i < orderCount; i++)
            {
                context.Add(mockFactory.CreateOrder());
                context.SaveChanges();
            }
            
            var orders = orderService.GetOrders();
            Assert.Equal(orderCount, orders.Count());
        }

        [Fact]
        public void UpdateOrderTest()
        {
            Order order = orderService.AddOrder(mockFactory.CreateOrder());

            order.Date = order.Date.AddDays(1);

            Order updatedOrder = orderService.UpdateOrder(order);

            Assert.Equal(order.Date, updatedOrder.Date);
        }

        [Fact]
        public void UpdateOrderTestException()
        {
            Order order = new Order(){ Id = 1, Date = DateTime.Now };

            Assert.Throws<ArgumentNullException>(() => orderService.UpdateOrder(order));
        }

        [Fact]
        public void DeleteOrderTest()
        {
            Order order = orderService.AddOrder(mockFactory.CreateOrder());
            orderService.DeleteOrder(order.Id);

            Assert.Null(orderService.GetOrder(order.Id));
        }

        [Fact]
        public void DeleteOrderTestException()
        {
            Assert.Throws<ArgumentNullException>(() => orderService.DeleteOrder(1));
        }
    }
}
