using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Repository;

namespace Restaurant.Tests.UnitTests.DAL
{
    [TestFixture]
    public class RepositoryTests
    {
        private DbContextOptions<TestDbContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Test]
        public async Task FindAsync_ReturnsEntity_WhenPredicateMatches()
        {
            using (var context = new TestDbContext(_options))
            {
                var repository = new Repository<TestEntity>(context);
                var entity = new TestEntity { Id = 1, Name = "TestEntity" };
                await repository.AddAsync(entity);
                await repository.SaveAsync();
            }

            using (var context = new TestDbContext(_options))
            {
                var repository = new Repository<TestEntity>(context);
                var result = await repository.FindAsync(e => e.Id == 1);

                Assert.That(result, Is.Not.Null);
                Assert.That(result.Name, Is.EqualTo("TestEntity"));
            }
        }

        [Test]
        public async Task FindByIdAsync_ReturnsEntity_WhenIdExists()
        {
            using (var context = new TestDbContext(_options))
            {
                var repository = new Repository<TestEntity>(context);
                var entity = new TestEntity { Id = 1, Name = "TestEntity" };
                await repository.AddAsync(entity);
                await repository.SaveAsync();
            }

            using (var context = new TestDbContext(_options))
            {
                var repository = new Repository<TestEntity>(context);
                var result = await repository.FindByIdAsync(1);

                Assert.That(result, Is.Not.Null);
                Assert.That(result.Name, Is.EqualTo("TestEntity"));
            }
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            using (var context = new TestDbContext(_options))
            {
                var repository = new Repository<TestEntity>(context);
                var entities = new List<TestEntity>
                {
                    new() { Id = 1, Name = "TestEntity1" },
                    new() { Id = 2, Name = "TestEntity2" }
                };
                await repository.AddRangeAsync(entities);
                await repository.SaveAsync();
            }

            using (var context = new TestDbContext(_options))
            {
                var repository = new Repository<TestEntity>(context);
                var result = await repository.GetAllAsync();

                Assert.That(result, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(result.Count(), Is.EqualTo(2));
                    Assert.That(result.Any(e => e.Name == "TestEntity1"), Is.True);
                });
                Assert.That(result.Any(e => e.Name == "TestEntity2"), Is.True);
            }
        }

        [Test]
        public async Task Delete_RemovesEntity()
        {
            using (var context = new TestDbContext(_options))
            {
                var repository = new Repository<TestEntity>(context);
                var entity = new TestEntity { Id = 1, Name = "TestEntity" };
                await repository.AddAsync(entity);
                await repository.SaveAsync();
            }

            using (var context = new TestDbContext(_options))
            {
                var repository = new Repository<TestEntity>(context);
                var entity = await repository.FindByIdAsync(1);
                repository.Delete(entity);
                await repository.SaveAsync();
            }

            using (var context = new TestDbContext(_options))
            {
                var repository = new Repository<TestEntity>(context);
                var result = await repository.FindByIdAsync(1);
                Assert.That(result, Is.Null);
            }
        }

        public class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class TestDbContext : DbContext
        {
            public TestDbContext(DbContextOptions<TestDbContext> options)
                : base(options)
            {
            }

            public DbSet<TestEntity> TestEntities { get; set; }
        }
    }
}
