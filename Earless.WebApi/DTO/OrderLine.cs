using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Earless.WebApi.DTO
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Fulfilled { get; set; }
    }
}
