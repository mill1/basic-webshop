using Earless.WebApi.Models;
using System;
using System.Collections.Generic;

namespace Earless.WebApi.Interfaces
{
    public interface IOrderService
    {
        public IEnumerable<Order> GetOrders();
        public Order GetOrder(int id);
        public Order AddOrder(Order order);
        public Order UpdateOrder(Order order);
        public void DeleteOrder(int id);
    }
}
