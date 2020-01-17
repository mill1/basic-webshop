using System;
using System.Collections.Generic;
using System.Text;
using Earless.WebApi.Data;
using Earless.WebApi.Models;

namespace Earless.WebApi.Test
{
    public class MockFactory
    {
        private readonly EarlessContext context;
        private List<ProductCategory> productCategories;
        private List<Product> products;

        public MockFactory(EarlessContext context)
        {
            this.context = context;

            productCategories = new List<ProductCategory>
            {
                new ProductCategory{ Name = "Gehoorapparaten" },
                new ProductCategory{ Name = "Accessoires en schoonmaakartikelen" },
                new ProductCategory{ Name = "Batterijen en accu's" },
                new ProductCategory{ Name = "Gehoorbescherming" },
                new ProductCategory{ Name = "Advies en hulp" },
            };

            products = new List<Product>
            {
                new Product{ ProductCategory = productCategories[0], Name = "Phonak Audéo M90-R – oplaadbaar", Description = "Phonak Audéo M90-R – oplaadbaar", Price = 1599.00 },
                new Product{ ProductCategory = productCategories[1], Name = "Oticon ProWax miniFit filters", Description = "Oticon ProWax miniFit filters", Price = 8.20 },
                new Product{ ProductCategory = productCategories[2], Name = "PowerOne p312", Description = "PowerOne p312", Price = 1.49 },
                new Product{ ProductCategory = productCategories[3], Name = "Alpine SleepSoft", Description = "Alpine SleepSoft", Price = 11.55 },
                new Product{ ProductCategory = productCategories[4], Name = "Consult", Description = "Consult", Price = 49.00 },
            };

            context.AddRange(productCategories);
            context.AddRange(products);
            context.SaveChanges();
        }

        public Order CreateOrder()
        {
            return(new Order()
            {
                Date = new DateTime(2020, 1, 14),
                Remark = "Twee keer bellen.",
                OrderLines = new List<OrderLine>()
                    {
                        new OrderLine { Product = products[0], Quantity = 1, Fulfilled = 1},
                        new OrderLine { Product = products[2], Quantity = 4, Fulfilled = 2},
                        new OrderLine { Product = products[3], Quantity = 2, Fulfilled = 2}
                    }
            });
        }
    }
}
