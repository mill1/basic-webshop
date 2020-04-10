using System;
using System.Collections.Generic;
using System.Linq;
using Earless.WebApi.Data;
using Earless.WebApi.Models;

namespace Earless.WebApi.Test.Mocks
{
    public class MockFactory
    {
        private static List<ProductCategory> productCategories;
        private static List<Product> products;

        public EarlessContext InitializeContext()
        {
            EarlessContext context =  TestDbGenerator.CreateContext();
            TestDbGenerator.Initialize(context);

            productCategories = GetProductCategories(false);
            context.AddRange();

            products = GetProducts(false, productCategories);

            context.AddRange(products);
            context.SaveChanges();

            return context;
        }

        public static List<ProductCategory> GetProductCategories(bool setId)
        {
            var productCategories = new List<ProductCategory>
            {
                new ProductCategory{ Name = "Gehoorapparaten" },
                new ProductCategory{ Name = "Accessoires en schoonmaakartikelen" },
                new ProductCategory{ Name = "Batterijen en accu's" },
                new ProductCategory{ Name = "Gehoorbescherming" },
                new ProductCategory{ Name = "Advies en hulp" },
            };

            if (setId)
                productCategories = productCategories.Select((pc, i) => { pc.Id = i + 1; return pc; }).ToList();

            return productCategories;
        }

        public static List<Product> GetProducts(bool setId, List<ProductCategory> productCategories)
        {
            var products =  new List<Product>
            {
                new Product{ ProductCategory = productCategories[0], Name = "Phonak Audéo M90-R – oplaadbaar", Description = "Phonak Audéo M90-R – oplaadbaar", Price = 1599.00 },
                new Product{ ProductCategory = productCategories[1], Name = "Oticon ProWax miniFit filters", Description = "Oticon ProWax miniFit filters", Price = 8.20 },
                new Product{ ProductCategory = productCategories[2], Name = "PowerOne p312", Description = "PowerOne p312", Price = 1.49 },
                new Product{ ProductCategory = productCategories[3], Name = "Alpine SleepSoft", Description = "Alpine SleepSoft", Price = 11.55 },
                new Product{ ProductCategory = productCategories[4], Name = "Consult", Description = "Consult", Price = 49.00 },
            };

            if (setId)
                products = products.Select((p, i) => { p.Id = i + 1; return p; }).ToList();

            return products;
        }

        public Order CreateOrder()
        {
            return MockOrderService.CreateOrder(products);
        }

        public OrderLine CreateOrderLine(int Id, int productId, int quantity, int fulfilled)
        {
            return MockOrderLineService.CreateOrderLine(products, Id, productId, quantity, fulfilled);
        }
    }
}
