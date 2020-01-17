using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Earless.WebApi.Models;

namespace Earless.WebApi.Data
{
    public class EarlessContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public EarlessContext(DbContextOptions<EarlessContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderLines)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>().Property(o => o.Date).HasColumnType("date");

            modelBuilder.Entity<Order>().Property(o => o.Date).IsRequired(true);
            modelBuilder.Entity<OrderLine>().Property(ol => ol.Quantity).IsRequired(true);
            modelBuilder.Entity<OrderLine>().Property(ol => ol.Fulfilled).IsRequired(true);
            modelBuilder.Entity<Product>().Property(ol => ol.Name).IsRequired(true);
            modelBuilder.Entity<Product>().Property(ol => ol.Description).IsRequired(true);
            modelBuilder.Entity<Product>().Property(ol => ol.Price).IsRequired(true);
            modelBuilder.Entity<ProductCategory>().Property(ol => ol.Name).IsRequired(true);
        }
    }
}