using System;
using System.Collections.Generic;

namespace Earless.WebApi.DTO
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
