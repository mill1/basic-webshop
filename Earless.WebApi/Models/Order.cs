using System;
using System.Collections.Generic;

namespace Earless.WebApi.Models
{
    public class Order
    {
        public Order()
        {
            OrderLines = new List<OrderLine>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; }
        
        public ICollection<OrderLine> OrderLines { get; set; }

        public override bool Equals(Object obj) {
            Order order = obj as Order;
            if (order.Id.Equals(this.Id))
                return true;
            else return false;
        }

    }
}
