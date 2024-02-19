using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using InternetShop.Models;

namespace InternetShop.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<UsersProducts> UsersProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .Property(e => e.Price)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<UsersProducts>()
                  .HasKey(pc => new { pc.UserId, pc.ProductId });
            modelBuilder.Entity<UsersProducts>()
                    .HasOne(p => p.Products)
                    .WithMany(pc => pc.UsersProducts)
                    .HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<UsersProducts>()
                    .HasOne(p => p.Users)
                    .WithMany(pc => pc.UsersProducts)
                    .HasForeignKey(c => c.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
