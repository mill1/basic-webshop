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
    public class OrderServiceTest : IDisposable
    {
        private readonly EarlessContext context;
        private readonly IOrderService orderService;
        private readonly IOrderLineService orderLineService;
        private readonly MockFactory mockFactory = new MockFactory();

        public OrderServiceTest()
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
                Order order = mockFactory.CreateOrder();
                context.Add(order);
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
