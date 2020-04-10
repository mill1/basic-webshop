using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Earless.WebApi.Data;
using Earless.WebApi.Models;
using MoreLinq;
using Earless.WebApi.Interfaces;

namespace Earless.WebApi.Services
{
    public class OrderLineService: IOrderLineService
    {
        private readonly EarlessContext context;

        public OrderLineService(EarlessContext context)
        {
            this.context = context;
        }

        public OrderLine GetOrderLine(int id)
        {
            return context.OrderLines
                .Include(ol => ol.Product)
                .ThenInclude(p => p.ProductCategory)
                .Where(o => o.Id == id).FirstOrDefault();
        }

        public void UpdateOrderLines(Order databaseOrder, IEnumerable<OrderLine> orderLines)
        {
            var fullJoin = databaseOrder.OrderLines
                .FullJoin(
                    orderLines,
                    dol => dol.Id,
                    ol => ol.Id,
                    dol => new { dol, ol = (OrderLine)null },
                    ol => new { dol = (OrderLine)null, ol },
                    (dol, ol) => new { dol, ol }).ToList();

            foreach (var item in fullJoin)
            {
                if (item.dol == null)
                    databaseOrder.OrderLines.Add(MapValues(item.ol, new OrderLine()));
                else if (item.ol == null)
                    databaseOrder.OrderLines.Remove(item.dol);
                else
                    context.OrderLines.Update(MapValues(item.ol, item.dol));
            }
        }

        private OrderLine MapValues(OrderLine source, OrderLine target)
        {
            target.Product = context.Products.Where(p => p.Id == source.Product.Id).FirstOrDefault();
            
            if (target.Product == null)
                throw new Exception($"Product id {source.Product.Id} in orderline {source.Id} could not be found.");

            target.Quantity = source.Quantity;
            target.Fulfilled = source.Fulfilled;

            return target;
        }
    }
}
