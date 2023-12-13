using Bogus;
using Restaurant.DAL.Context.Seeding;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Enums;

namespace Restaurant.Tests
{
    public static class TestUtilities
    {
        public static ICollection<Portion> GeneratePortions(ICollection<Dish> dishesEntities)
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

        public static ICollection<OrderItem> GenerateOrderItems(ICollection<Order> orders)
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

        public static ICollection<Order> GenerateOrders(int count = 15)
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

        public static ICollection<DishIngredient> GenerateDishIngredients(ICollection<Dish> dishes, ICollection<Ingredient> ingredients)
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

        public static ICollection<Dish> GenerateDishes(int count = 5)
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
                .RuleFor(e => e.Id, f => f.UniqueIndex)
                .Generate(count);
        }

        public static ICollection<Ingredient> GenerateIngredients(int count = 10)
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
