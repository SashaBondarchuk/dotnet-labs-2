using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Context;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository;

namespace Restaurant.Tests.UnitTests.DAL
{
    [TestFixture]
    public class DishIngredientsRepositoryTests
    {
        private DbContextOptions<RestaurantDbContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Test]
        public async Task GetDishIngredientsByDishIdAsync_ReturnsDishIngredientsForDish()
        {
            using var context = new RestaurantDbContext(_options);
            var repository = new DishIngredientsRepository(context);

            var dishId = 10;
            var sampleDishIngredient = new DishIngredient { DishId = dishId, IngredientId = 1 };
            await repository.AddAsync(sampleDishIngredient);
            await repository.SaveAsync();

            var result = await repository.GetDishIngredientsByDishIdAsync(dishId);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.GreaterThan(0));
                Assert.That(result.Any(di => di.DishId == dishId), Is.True);
            });
        }

        [TearDown]
        public void TearDown()
        {
            using var context = new RestaurantDbContext(_options);
            context.Database.EnsureDeleted();
        }
    }
}
