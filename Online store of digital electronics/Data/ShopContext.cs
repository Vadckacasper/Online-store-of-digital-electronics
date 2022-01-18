using Microsoft.EntityFrameworkCore;
using Online_store_of_digital_electronics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Online_store_of_digital_electronics.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) {   }
        public DbSet<Products> products { get; set; }
        public DbSet<Buyers> buyers { get; set; }
        public DbSet<Manufacturers> manufacturers { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }
        public DbSet<RelationshipProductOrder> relationshipProductOrders { get; set; }
        public DbSet<RelationshipProductCategory> relationshipProductCategories { get; set; }
        public DbSet<ProductImage> ProductsImag { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().ToTable("Products");
            modelBuilder.Entity<Buyers>().ToTable("Buyers");
            modelBuilder.Entity<Manufacturers>().ToTable("Manufacturers");
            modelBuilder.Entity<Orders>().ToTable("Orders");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
            modelBuilder.Entity<RelationshipProductOrder>().ToTable("RelationshipProductOrder");
            modelBuilder.Entity<RelationshipProductCategory>().ToTable("RelationshipProductCategory");
            modelBuilder.Entity<ProductImage>().ToTable("ProductsImage");
        }
    }
}
