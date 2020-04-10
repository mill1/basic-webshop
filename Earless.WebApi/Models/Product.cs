using System;
using System.Collections.Generic;

namespace Earless.WebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
