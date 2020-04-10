using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Earless.WebApi.Data;
using System;
using System.Collections.Generic;

namespace Earless.WebApi.Test
{
    public static class TestDbGenerator
    {
        public static void Initialize(EarlessContext context)
        {
            context.Database.EnsureCreated();
        }

        public static EarlessContext CreateContext()
        {
            EarlessContext context = new EarlessContext(CreateNewEntityFrameworkContextOptions());
            context.Database.EnsureCreated();
            return context;
        }

        // Create a EntityFrameWork in memory database contex with the name of the calling method or with a specified name.
        public static DbContextOptions<EarlessContext> CreateNewEntityFrameworkContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<EarlessContext>();
            builder.UseInMemoryDatabase(typeof(TestDbGenerator).Name)
                .EnableSensitiveDataLogging()
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}


