using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Earless.WebApi.Data;
using Earless.WebApi.Models;
using MoreLinq;

namespace Earless.WebApi.Services
{
    public class OrderService
    {
        private readonly EarlessContext context;
        private readonly OrderLineService orderLineService;

        public OrderService(EarlessContext context, OrderLineService orderLineService)
        {
            this.context = context;
            this.orderLineService = orderLineService;
        }

        public IEnumerable<Order> GetOrders()
        {
            return context.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .ThenInclude(p => p.ProductCategory)
                .ToList();
        }

        public Order GetOrder(int id)
        {
            return GetOrders().Where(o => o.Id == id).FirstOrDefault();
        }

        public Order AddOrder(Order order)
        {
            IEnumerable<(bool productFound, OrderLine orderLine)> orderlines = order.OrderLines.LeftJoin(
                    context.Products,
                    ol => ol.Product.Id,
                    p => p.Id,
                    ol => (false, ol),
                    (ol, p) => (true, new OrderLine()
                    {
                        Product = p,
                        Quantity = ol.Quantity,
                        Fulfilled = ol.Fulfilled
                    })).ToArray();

            var faultyLines = orderlines.Where(ol => !ol.productFound).Select(ol => ol.orderLine);

            if (faultyLines.Any())
                throw new Exception($"Product id's [{string.Join(", ", faultyLines.Select(ol => ol.Product.Id))}] from orderlines could not be found.");

            Order newOrder = new Order()
            {
                Date = order.Date,
                Remark = order.Remark,
                OrderLines = orderlines.Where(ol => ol.Item1).Select(ol => ol.Item2).ToList()
            };

            context.Orders.Add(newOrder);

            context.SaveChanges();

            return newOrder;
        }

        public Order UpdateOrder(Order order)
        {
            Order databaseOrder = GetOrder(order.Id);

            if (databaseOrder == null)
                throw new ArgumentNullException();

            databaseOrder.Date = order.Date;
            databaseOrder.Remark = order.Remark;
            orderLineService.UpdateOrderLines(databaseOrder, order.OrderLines);

            context.SaveChanges();

            return databaseOrder;
        }

        public void DeleteOrder(int id)
        {
            Order order = GetOrder(id);

            if (order == null)
                throw new ArgumentNullException();

            context.Orders.Remove(order);
            context.SaveChanges();
        }
    }
}
