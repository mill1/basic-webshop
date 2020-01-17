﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Earless.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Earless.WebApi.Data
{
    public class DbInitializer
    {
        private EarlessContext context;
        private InitializationOptions options;

        public DbInitializer(EarlessContext context, IOptionsMonitor<InitializationOptions> subOptionsAccessor)
        {
            this.context = context;
            options = subOptionsAccessor.CurrentValue;
        }

        public void Initialize()
        {
            // Clear all tables
            if (options.ClearTables)
            {
                List<string> tableNames = new List<string>(new string[] { 
                    "OrderLine", 
                    "Orders", 
                    "Product", 
                    "ProductCategory",
                });

                tableNames.ForEach(t => context.Database.ExecuteSqlCommand(string.Format("DELETE FROM {0}", t)));

                context.SaveChanges();
            }

            // Seed the database
            if (options.SeedDatabase)
            {
                DbSeeder dbSeeder = new DbSeeder(context);
                dbSeeder.Seed();
            }

            if (options.ClearTables || options.SeedDatabase)
                context.SaveChanges();
        }
    }
}
