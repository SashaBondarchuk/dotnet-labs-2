using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Entities;

namespace Restaurant.DAL.Context.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishIngredient>()
                .HasKey(e => new { e.DishId, e.IngredientId }).HasName("PK_DishIngredient");

            modelBuilder.Entity<Dish>()
                .HasMany(e => e.Ingredients)
                .WithMany(e => e.Dishes)
                .UsingEntity<DishIngredient>();

            modelBuilder.Entity<Portion>()
                .HasOne(e => e.Dish)
                .WithMany(e => e.Portions);

            modelBuilder.Entity<Portion>()
                .HasOne(e => e.Unit)
                .WithMany(e => e.Portions);

            modelBuilder.Entity<OrderItem>()
                .HasOne(e => e.Portion)
                .WithMany(e => e.OrderItems);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderItems)
                .WithOne(e => e.Order);
        }
    }
}
