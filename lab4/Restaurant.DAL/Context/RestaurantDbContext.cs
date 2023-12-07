using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Context.Extensions;
using Restaurant.DAL.Entities;

namespace Restaurant.DAL.Context
{
    public class RestaurantDbContext : DbContext
    {
        public DbSet<Dish> Dishes => Set<Dish>();
        public DbSet<DishIngredient> DishIngredients => Set<DishIngredient>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Portion> Portions => Set<Portion>();
        public DbSet<Unit> Units => Set<Unit>();

        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
        }
    }
}
