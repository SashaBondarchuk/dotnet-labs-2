using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Context;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository;

namespace Restaurant.Tests.UnitTests.DAL
{
    [TestFixture]
    public class PortionsRepositoryTests
    {
        private DbContextOptions<RestaurantDbContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new RestaurantDbContext(_options);
            var units = new List<Unit>
            {
                new("Unit 1"),
                new("Unit 2")
            };
            context.Units.AddRange(units);
            context.SaveChanges();
        }

        [Test]
        public async Task GetAllPortionsAsync_ReturnsAllPortions()
        {
            using (var context = new RestaurantDbContext(_options))
            {
                var repository = new PortionsRepository(context);
                var portions = new List<Portion>
                {
                    new("Portion 1") { Id = 1, DishId = 1, UnitId = 1, Amount = 100, Price = 10 },
                    new("Portion 2") { Id = 2, DishId = 1, UnitId = 2, Amount = 150, Price = 15 }
                };
                await repository.AddRangeAsync(portions);
                await repository.SaveAsync();
            }

            using (var context = new RestaurantDbContext(_options))
            {
                var repository = new PortionsRepository(context);
                var result = await repository.GetAllPortionsAsync();

                Assert.That(result, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(result.Count(), Is.EqualTo(2));
                    Assert.That(result.Any(p => p.Description == "Portion 1"), Is.True);
                    Assert.That(result.Any(p => p.Description == "Portion 2"), Is.True);
                });
            }
        }

        [Test]
        public async Task GetDishPortionsByIdAsync_ReturnsPortionsForDish()
        {
            using (var context = new RestaurantDbContext(_options))
            {
                var repository = new PortionsRepository(context);
                var portions = new List<Portion>
                {
                    new("Portion 3") { Id = 3, DishId = 1, UnitId = 1, Amount = 100, Price = 10 },
                    new("Portion 4") { Id = 4, DishId = 1, UnitId = 2, Amount = 150, Price = 15 }
                };
                await repository.AddRangeAsync(portions);
                await repository.SaveAsync();
            }

            using (var context = new RestaurantDbContext(_options))
            {
                var repository = new PortionsRepository(context);
                var result = await repository.GetDishPortionsByIdAsync(1);

                Assert.That(result, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(result.Count(), Is.EqualTo(2));
                    Assert.That(result.First().Description, Is.EqualTo("Portion 3"));
                });
            }
        }

        [TearDown]
        public void TearDown()
        {
            using var context = new RestaurantDbContext(_options);
            context.Database.EnsureDeleted();
        }
    }
}
