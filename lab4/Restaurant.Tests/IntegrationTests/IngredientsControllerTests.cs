using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Restaurant.Common.DTOs.Ingredient;
using Restaurant.DAL.Context;
using Restaurant.DAL.Entities;
using Restaurant.WebAPI;
using System.Net;
using System.Text;

namespace Restaurant.Tests.IntegrationTests
{
    [TestFixture]
    public class IngredientsControllerTests
    {
        private HttpClient _client;
        private IServiceScope _scope;
        private RestaurantDbContext _dbContext;

        [OneTimeSetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.RemoveAll(typeof(DbContextOptions<RestaurantDbContext>));
                        services.AddDbContext<RestaurantDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDatabase");
                        });
                    });
                });

            _client = factory.CreateClient();
            _scope = factory.Services.CreateScope();
            _dbContext = _scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();

            _dbContext.Database.EnsureCreated();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _scope.Dispose();
        }

        [Test]
        public async Task AddUpdateAndGetIngredientAsync_ValidData_ReturnsUpdatedIngredient()
        {
            var newIngredient = new Ingredient("Salt", "Descr") { Id = 102 };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newIngredient), Encoding.UTF8, "application/json");

            var addResponse = await _client.PostAsync("/api/ingredients", jsonContent);
            addResponse.EnsureSuccessStatusCode();

            var addedIngredient = JsonConvert.DeserializeObject<IngredientDto>(await addResponse.Content.ReadAsStringAsync());

            addedIngredient.Name = "Updated Ingredient";
            addedIngredient.Description = "Updated Description";
            var updateJsonContent = new StringContent(JsonConvert.SerializeObject(addedIngredient), Encoding.UTF8, "application/json");
            var updateResponse = await _client.PutAsync($"/api/ingredients/{addedIngredient.Id}", updateJsonContent);
            updateResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync($"/api/ingredients/{addedIngredient.Id}");
            getResponse.EnsureSuccessStatusCode();

            var content = await getResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IngredientDto>(content);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo("Updated Ingredient"));
                Assert.That(result.Description, Is.EqualTo("Updated Description"));
            });
        }

        [Test]
        public async Task GetAllIngredientsAsync_ReturnsOkResult()
        {
            var response = await _client.GetAsync("/api/ingredients");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<IngredientDto>>(content);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<IngredientDto>>());
        }

        [Test]
        public async Task GetIngredientByIdAsync_ValidId_ReturnsOkResult()
        {
            var newIngredient = new Ingredient("Salt", "Descr") { Id = 101 };
            _dbContext.Ingredients.Add(newIngredient);
            _dbContext.SaveChanges();

            var response = await _client.GetAsync($"/api/ingredients/{newIngredient.Id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IngredientDto>(content);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
        }

        [Test]
        public async Task DeleteIngredientAsync_ValidId_ReturnsNoContentResult()
        {
            var newIngredient = new Ingredient("Salt", "Descr") { Id = 100 };
            _dbContext.Ingredients.Add(newIngredient);
            _dbContext.SaveChanges();

            var response = await _client.DeleteAsync($"/api/ingredients/{newIngredient.Id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.Multiple(() =>
            {
                Assert.That(content, Is.Empty);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            });
        }
    }
}
