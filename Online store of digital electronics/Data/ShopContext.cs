using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_store_of_digital_electronics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Online_store_of_digital_electronics.Data
{
    public class ShopContext : IdentityDbContext<Buyers>
    {
        public ShopContext()
        {
        }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options) {   }
        public DbSet<Products> products { get; set; }
        public DbSet<Buyers> buyers { get; set; }
        public DbSet<Manufacturers> manufacturers { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }
        public DbSet<ProductOrder> productOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().ToTable("Products");
            modelBuilder.Entity<Buyers>().ToTable("Buyers");
            modelBuilder.Entity<Manufacturers>().ToTable("Manufacturers");
            modelBuilder.Entity<Orders>().ToTable("Orders");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
            //modelBuilder.Entity<ProductOrder>().ToTable("ProductOrder");
            modelBuilder.Entity<ProductOrder>().ToTable("ProductOrder");
            base.OnModelCreating(modelBuilder);
        }
    }
}
