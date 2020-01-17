using Earless.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Earless.WebApi.Data
{
    public class DbSeeder
    {
        private readonly EarlessContext context;

        public DbSeeder(EarlessContext context)
        {
             this.context = context;   
        }

        public void Seed()
        {
            if (context.ProductCategories.Any())
                return;   // DB has been seeded

            List < ProductCategory > productCategories =  new List<ProductCategory>
            {
                /*  0 */ new ProductCategory{ Name = "Gehoorapparaten" },
                /*  1 */ new ProductCategory{ Name = "Accessoires en schoonmaakartikelen" },
                /*  2 */ new ProductCategory{ Name = "Batterijen en accu's" },
                /*  3 */ new ProductCategory{ Name = "Gehoorbescherming" },
                /*  4 */ new ProductCategory{ Name = "Advies en hulp" },
            };

            List<Product> products = new List<Product>
            {
                /*  0 */ new Product{ ProductCategory = productCategories[0], Name = "Phonak Audéo M90-R – oplaadbaar", Description = "Phonak Audéo M90-R – oplaadbaar", Price = 1599.00 },
                /*  1 */ new Product{ ProductCategory = productCategories[0], Name = "Widex EVOKE 440 Fusion 2 direct streaming", Description = "Widex EVOKE 440 Fusion 2 direct streaming", Price = 1492.22 },
                /*  2 */ new Product{ ProductCategory = productCategories[0], Name = "Starkey Livio AI 2400 RIC R – oplaadbaar", Description = "Starkey Livio AI 2400 RIC R – oplaadbaar", Price = 1649.00 },
                /*  3 */ new Product{ ProductCategory = productCategories[0], Name = "Oticon Siya 1 85", Description = "Oticon Siya 1 85", Price = 718.32 },
                /*  4 */ new Product{ ProductCategory = productCategories[0], Name = "Phonak Audéo M90-312", Description = "Phonak Audéo M90-312", Price = 1449.00 },
                /*  5 */ new Product{ ProductCategory = productCategories[0], Name = "Phonak Lyric", Description = "Phonak Lyric", Price = 1600.00 },

                /*  6 */ new Product{ ProductCategory = productCategories[1], Name = "Oticon ProWax miniFit filters", Description = "Oticon ProWax miniFit filters", Price = 8.20 },
                /*  7 */ new Product{ ProductCategory = productCategories[1], Name = "Phonak CeruStop Filters", Description = "Phonak CeruStop Filters", Price = 4.45 },
                /*  8 */ new Product{ ProductCategory = productCategories[1], Name = "Widex Nanocare Filters", Description = "Widex Nanocare Filters", Price = 4.95 },
                /*  9 */ new Product{ ProductCategory = productCategories[1], Name = "TakeCare Cleaning tissues refill", Description = "TakeCare Cleaning tissues refill", Price = 3.50 },
                /* 10 */ new Product{ ProductCategory = productCategories[1], Name = "Oticon Dome", Description = "Oticon Dome", Price = 5.55 },
                /* 11 */ new Product{ ProductCategory = productCategories[1], Name = "Perfect Clean Refill", Description = "Perfect Clean Refill", Price = 15.75 },
                /* 12 */ new Product{ ProductCategory = productCategories[1], Name = "Cedis Reinigingstabletten", Description = "Cedis Reinigingstabletten", Price = 3.75 },

                /* 13 */ new Product{ ProductCategory = productCategories[2], Name = "PowerOne p312", Description = "PowerOne p312", Price = 1.49 },
                /* 14 */ new Product{ ProductCategory = productCategories[2], Name = "Rayovac 312", Description = "Rayovac 312", Price = 1.55 },
                /* 15 */ new Product{ ProductCategory = productCategories[2], Name = "TakeCare 312", Description = "TakeCare 312", Price = 1.29 },
                /* 16 */ new Product{ ProductCategory = productCategories[2], Name = "PowerOne p10", Description = "PowerOne p10", Price = 1.49 },
                /* 17 */ new Product{ ProductCategory = productCategories[2], Name = "Rayovac 13", Description = "Rayovac 13", Price = 1.55 },
                /* 18 */ new Product{ ProductCategory = productCategories[2], Name = "Rayovac 10", Description = "Rayovac 10", Price = 1.55 },
                /* 19 */ new Product{ ProductCategory = productCategories[2], Name = "PowerOne p675", Description = "PowerOne p675", Price = 1.49 },
                /* 20 */ new Product{ ProductCategory = productCategories[2], Name = "PowerOne p13", Description = "PowerOne p13", Price = 1.49 },
                /* 21 */ new Product{ ProductCategory = productCategories[2], Name = "TakeCare 10", Description = "TakeCare 10", Price = 1.29 },

                /* 22 */ new Product{ ProductCategory = productCategories[3], Name = "Alpine SleepSoft", Description = "Alpine SleepSoft", Price = 11.55 },
                /* 23 */ new Product{ ProductCategory = productCategories[3], Name = "KNOPS", Description = "KNOPS", Price = 85.00 },
                /* 24 */ new Product{ ProductCategory = productCategories[3], Name = "Alpine PartyPlug", Description = "Alpine PartyPlug", Price = 11.55 },
                /* 25 */ new Product{ ProductCategory = productCategories[3], Name = "KNOPS knurled", Description = "KNOPS knurled", Price = 99.00 },
                /* 26 */ new Product{ ProductCategory = productCategories[3], Name = "Alpine WorkSafe", Description = "Alpine WorkSafe", Price = 11.55 },
                /* 27 */ new Product{ ProductCategory = productCategories[3], Name = "EARS2U Gehoorbescherming", Description = "EARS2U Gehoorbescherming", Price = 8.65 },
                /* 28 */ new Product{ ProductCategory = productCategories[3], Name = "Alpine Muffy oorkappen", Description = "Alpine Muffy oorkappen", Price = 21.65 },
                /* 29 */ new Product{ ProductCategory = productCategories[3], Name = "EarPlanes", Description = "EarPlanes", Price = 11.45 },
                /* 30 */ new Product{ ProductCategory = productCategories[3], Name = "Alpine MotorSafe PRO", Description = "Alpine MotorSafe PRO", Price = 22.55 },

                /* 31 */ new Product{ ProductCategory = productCategories[4], Name = "Consult", Description = "Consult", Price = 49.00 },
                /* 32 */ new Product{ ProductCategory = productCategories[4], Name = "Extra afstelling hoortoestel", Description = "Extra afstelling hoortoestel", Price = 99.00 },
                /* 33 */ new Product{ ProductCategory = productCategories[4], Name = "Hoortest", Description = "Hoortest", Price = 49.00 },
                /* 34 */ new Product{ ProductCategory = productCategories[4], Name = "Reparatie", Description = "Reparatie", Price = 145.00 },
                /* 35 */ new Product{ ProductCategory = productCategories[4], Name = "Extra instelling op afstand (online)", Description = "Extra instelling op afstand (online)", Price = 35.00 },
                /* 36 */ new Product{ ProductCategory = productCategories[4], Name = "Huisbezoek", Description = "Huisbezoek", Price = 149.00 },
            };

            context.AddRange(productCategories);
            context.AddRange(products);

            context.Add(new Order()
            {
                Date = new DateTime(2019, 11, 1),
                Remark = "Niet afleveren bij de buren!",
                OrderLines = new List<OrderLine>()
                    {
                        new OrderLine { Product = products[0] , Quantity = 1, Fulfilled = 1},
                        new OrderLine { Product = products[16], Quantity = 5, Fulfilled = 5}
                    }
            });

            context.Add(new Order()
            {
                Date = new DateTime(2019, 11, 1),
                Remark = null,
                OrderLines = new List<OrderLine>()
                    {
                        new OrderLine { Product = products[1], Quantity = 1, Fulfilled = 1},
                        new OrderLine { Product = products[12], Quantity = 2, Fulfilled = 2},
                        new OrderLine { Product = products[21], Quantity = 3, Fulfilled = 3}
                    }
            });

            context.Add(new Order()
            {
                Date = new DateTime(2019, 11, 2),
                Remark = "Niet bezorgen voor donderdag as.",
                OrderLines = new List<OrderLine>()
                    {
                        new OrderLine { Product = products[2], Quantity = 1, Fulfilled = 0},
                        new OrderLine { Product = products[9], Quantity = 3, Fulfilled = 3},
                        new OrderLine { Product = products[18], Quantity = 4, Fulfilled  = 4},
                    }
            });

            context.Add(new Order()
            {
                Date = new DateTime(2019, 11, 2),
                Remark = null,
                OrderLines = new List<OrderLine>()
                    {
                        new OrderLine { Product = products[3], Quantity = 1, Fulfilled = 0},
                        new OrderLine { Product = products[27], Quantity = 1, Fulfilled = 0},
                        new OrderLine { Product = products[33], Quantity = 1, Fulfilled = 0}
                    }
            });
        }
    }
}
