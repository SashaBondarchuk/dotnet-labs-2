using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Context;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository;

namespace Restaurant.Tests.UnitTests.DAL
{
    [TestFixture]
    public class DishesRepositoryTests
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
        public async Task GetDishesWithAllInfoAsync_ReturnsDishesWithRelatedInfo()
        {
            using var context = new RestaurantDbContext(_options);
            var repository = new DishesRepository(context);

            var sampleDish = new Dish("Sample Dish", "Sample Description");
            var sampleIngredient = new Ingredient("Sample Ingredient", "Sample Ingredient Description");
            
            sampleDish.Ingredients.Add(sampleIngredient);
            
            await repository.AddAsync(sampleDish);
            await repository.SaveAsync();

            var result = await repository.GetDishesWithAllInfoAsync();

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.GreaterThan(0));
                Assert.That(result.Any(d => d.Ingredients.Any()), Is.True);
            });
        }

        [Test]
        public async Task GetDishWithAllInfoByIdAsync_ReturnsDishWithRelatedInfo()
        {
            using var context = new RestaurantDbContext(_options);
            var repository = new DishesRepository(context);

            var sampleDish = new Dish("Sample Dish", "Sample Description");
            var sampleIngredient = new Ingredient("Sample Ingredient", "Sample Ingredient Description");
            
            sampleDish.Ingredients.Add(sampleIngredient);
            
            await repository.AddAsync(sampleDish);
            await repository.SaveAsync();

            var result = await repository.GetDishWithAllInfoByIdAsync(sampleDish.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Ingredients, Is.Not.Null);
            Assert.That(result.Ingredients, Is.Not.Empty);
        }
    }
}
