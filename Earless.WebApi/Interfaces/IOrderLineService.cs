using Earless.WebApi.Models;
using System;
using System.Collections.Generic;

namespace Earless.WebApi.Interfaces
{
    public interface IOrderLineService
    {
        public OrderLine GetOrderLine(int id);
        public void UpdateOrderLines(Order databaseOrder, IEnumerable<OrderLine> orderLines);
    }
}
