using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClothesSupplyApi.Models
{
    public partial class ProductCatalogDbContext : DbContext
    {
        public ProductCatalogDbContext()
        {
        }

        public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });
        }
    }
}
