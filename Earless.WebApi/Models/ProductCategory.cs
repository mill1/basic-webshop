using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Earless.WebApi.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
