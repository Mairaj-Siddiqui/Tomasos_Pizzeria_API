using Microsoft.EntityFrameworkCore;
using TomasosPizzeriaAPI.Data.Entities;

namespace Tomasos_Pizzeria_API.Context
{
    public class TomasosDbContext : DbContext
    {
        public TomasosDbContext(DbContextOptions options) : base(options)
        {           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDish> OrderDishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // ✅ Keep base call

            // Composite key for OrderDish (many-to-many)
            modelBuilder.Entity<OrderDish>()
                .HasKey(od => new { od.OrderId, od.DishId });

            modelBuilder.Entity<OrderDish>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDishes)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDish>()
                .HasOne(od => od.Dish)
                .WithMany()
                .HasForeignKey(od => od.DishId);
        }



    }
}
