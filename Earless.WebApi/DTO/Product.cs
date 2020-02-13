using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Earless.WebApi.DTO
{
    public class Product
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
