using System;
using System.Collections.Generic;

namespace Earless.WebApi.Models
{
    public class OrderLine
    {        
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int Fulfilled { get; set; }
    }
}