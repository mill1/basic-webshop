using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Earless.WebApi.DTO
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; }
        public ICollection<DTO.OrderLine> OrderLines { get; set; }
    }
}
