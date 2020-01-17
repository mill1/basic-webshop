using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
