using Bogus;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Context.Seeding;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Enums;

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

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>().HasData(SeedDefaults.Units);

            var ingredientsEntities = GenerateIngredients();
            modelBuilder.Entity<Ingredient>().HasData(ingredientsEntities);

            var dishesEntities = GenerateDishes();
            modelBuilder.Entity<Dish>().HasData(dishesEntities);

            var dishesIngredients = GenerateDishIngredients(dishesEntities, ingredientsEntities);
            modelBuilder.Entity<DishIngredient>().HasData(dishesIngredients);

            var ordersEntities = GenerateOrders();
            modelBuilder.Entity<Order>().HasData(ordersEntities);

            var portionsEntities = GeneratePortions(dishesEntities);
            modelBuilder.Entity<Portion>().HasData(portionsEntities);

            var ordersItemsEntities = GenerateOrderItems(ordersEntities);
            modelBuilder.Entity<OrderItem>().HasData(ordersItemsEntities);
        }

        private static ICollection<Portion> GeneratePortions(ICollection<Dish> dishesEntities)
        {
            Faker.GlobalUniqueIndex = 0;

            var count = dishesEntities.Count * 3;

            return new Faker<Portion>()
                .CustomInstantiator(f => new Portion(f.Lorem.Sentence(5)))
                .UseSeed(SeedDefaults.Seed)
                .RuleFor(e => e.Id, f => f.IndexGlobal)
                .RuleFor(e => e.UnitId, 1)
                .RuleFor(e => e.Amount, f => f.Random.Decimal(min: 200, max: 600))
                .RuleFor(e => e.DishId, f => f.Random.Number(1, dishesEntities.Count))
                .RuleFor(e => e.Price, f => f.Random.Decimal(min: 200, max: 1000))
                .Generate(count);
        }

        private static ICollection<OrderItem> GenerateOrderItems(ICollection<Order> orders)
        {
            Faker.GlobalUniqueIndex = 0;

            var count = orders.Count * 4;

            return new Faker<OrderItem>()
                .CustomInstantiator(f => new OrderItem())
                .UseSeed(SeedDefaults.Seed)
                .RuleFor(e => e.Id, f => f.IndexGlobal)
                .RuleFor(e => e.Quantity, f => f.Random.Number(1, 5))
                .RuleFor(e => e.OrderId, f => f.Random.Number(1, orders.Count))
                .RuleFor(e => e.PortionId, f => f.Random.Number(1, orders.Count))
                .Generate(count);
        }

        private static ICollection<Order> GenerateOrders(int count = 15)
        {
            Faker.GlobalUniqueIndex = 0;

            return new Faker<Order>()
                .CustomInstantiator(f => new Order())
                .UseSeed(SeedDefaults.Seed)
                .RuleFor(e => e.Id, f => f.IndexGlobal)
                .RuleFor(e => e.TotalAmount, f => f.Random.Decimal(min: 20, max: 1500))
                .RuleFor(e => e.Status, f => f.PickRandom<OrderStatus>())
                .RuleFor(e => e.OrderDate, f => f.Date.Between(new DateTime(2022, 12, 31, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 11, 30, 0, 0, 0, DateTimeKind.Utc)))
                .Generate(count);
        }

        private static ICollection<DishIngredient> GenerateDishIngredients(ICollection<Dish> dishes, ICollection<Ingredient> ingredients)
        {
            Faker.GlobalUniqueIndex = 0;

            var dishIngredients = new Faker<DishIngredient>()
                .CustomInstantiator(f => new DishIngredient())
                .UseSeed(SeedDefaults.Seed)
                .RuleFor(e => e.DishId, f => f.PickRandom(dishes).Id)
                .RuleFor(e => e.IngredientId, f => f.Random.Number(1, ingredients.Count))
                .Generate(dishes.Count * 5);

            return dishIngredients.DistinctBy(di => new { di.IngredientId, di.DishId }).ToList();
        }

        private static ICollection<Dish> GenerateDishes(int count = 5)
        {
            Faker.GlobalUniqueIndex = 0;

            var dishNames = new HashSet<string>();

            return new Faker<Dish>()
                .CustomInstantiator(f =>
                {
                    string name;
                    do
                    {
                        name = f.PickRandom(SeedDefaults.DishesNames);
                    } while (!dishNames.Add(name));

                    return new Dish(name, f.Lorem.Sentence(7));
                })
                .UseSeed(SeedDefaults.Seed)
                .RuleFor(e => e.Id, f => f.IndexGlobal)
                .Generate(count);
        }

        private static ICollection<Ingredient> GenerateIngredients(int count = 10)
        {
            Faker.GlobalUniqueIndex = 0;

            var ingredientNames = new HashSet<string>();

            return new Faker<Ingredient>()
                .CustomInstantiator(f =>
                {
                    string name;
                    do
                    {
                        name = f.PickRandom(SeedDefaults.IngredientsNames);
                    } while (!ingredientNames.Add(name));

                    return new Ingredient(name, f.Lorem.Sentence(5));
                })
                .UseSeed(SeedDefaults.Seed)
                .RuleFor(e => e.Id, f => f.IndexGlobal)
                .Generate(count);
        }
    }
}
