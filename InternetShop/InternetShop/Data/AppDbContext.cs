using InternetShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;

namespace InternetShop.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Carts> Carts { get; set; }
        public DbSet<CartsProducts> CartsProducts { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersProducts> UsersProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .Property(e => e.Price)
                .HasColumnType("decimal(10,2)");
            
            modelBuilder.Entity<Orders>()
                .Property(e => e.TotalPrice)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Users>()
                .HasOne(t => t.Carts)
                .WithOne(s => s.Users)
                .HasForeignKey<Carts>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
