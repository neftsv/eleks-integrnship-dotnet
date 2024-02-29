using InternetShop.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Carts> Carts { get; set; }
        public DbSet<CartsProducts> CartsProducts { get; set; }
        public DbSet<Categories> Categories { get; set; }
		public DbSet<Delivery> Delivery { get; set; }
		public DbSet<Images> Images { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersProducts> UsersProducts { get; set; }
        public DbSet<Roles> Roles { get; set; }

        public DbSet<BlogPost> BlogPosts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .Property(e => e.Price)
                .HasColumnType("decimal(10,2)");
            
            modelBuilder.Entity<Orders>()
                .Property(e => e.TotalPrice)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Carts>(c => c.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
