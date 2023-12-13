using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Restaurant.Common.DTOs.Dish;
using Restaurant.DAL.Context;
using Restaurant.WebAPI;

namespace Restaurant.Tests.IntegrationTests
{
    [TestFixture]
    public class DishesControllerIntegrationTests
    {
        private HttpClient _client;
        private IServiceScope _scope;

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

            var context = _scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
            context.Database.EnsureCreated();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _scope.Dispose();
        }

        [Test]
        public async Task AddUpdateAndGetDishAsync_ValidData_ReturnsUpdatedDish()
        {
            var newDish = new NewDishDto { Name = "Pizza", Description = "Delicious pizza" };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newDish), Encoding.UTF8, "application/json");
            var addResponse = await _client.PostAsync("/api/dishes", jsonContent);
            addResponse.EnsureSuccessStatusCode();
            var addedDish = JsonConvert.DeserializeObject<DishDto>(await addResponse.Content.ReadAsStringAsync());

            addedDish.Name = "Updated Pizza";
            addedDish.Description = "Even more delicious pizza";
            var updateJsonContent = new StringContent(JsonConvert.SerializeObject(addedDish), Encoding.UTF8, "application/json");
            var updateResponse = await _client.PutAsync($"/api/dishes/{addedDish.Id}", updateJsonContent);
            updateResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync($"/api/dishes/{addedDish.Id}");
            getResponse.EnsureSuccessStatusCode();

            var content = await getResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DishDto>(content);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo("Updated Pizza"));
                Assert.That(result.Description, Is.EqualTo("Even more delicious pizza"));
            });
        }

        [Test]
        public async Task DeleteDishAsync_ValidId_ReturnsNoContentResult()
        {
            var newDish = new NewDishDto { Name = "Burger", Description = "Tasty burger" };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newDish), Encoding.UTF8, "application/json");
            var addResponse = await _client.PostAsync("/api/dishes", jsonContent);
            addResponse.EnsureSuccessStatusCode();
            var addedDish = JsonConvert.DeserializeObject<DishDto>(await addResponse.Content.ReadAsStringAsync());

            var deleteResponse = await _client.DeleteAsync($"/api/dishes/{addedDish.Id}");
            deleteResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync($"/api/dishes/{addedDish.Id}");
            Assert.Multiple(() =>
            {
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            });
        }

        [Test]
        public async Task GetAllDishesAsync_ReturnsOkResult()
        {
            var response = await _client.GetAsync("/api/dishes");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<DishDto>>(content);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<DishDto>>());
        }

        [Test]
        public async Task GetDishByIdAsync_ValidId_ReturnsOkResult()
        {
            var response = await _client.GetAsync("/api/dishes/1");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DishDto>(content);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
        }

        [Test]
        public async Task AddDishAsync_ValidData_ReturnsOkResult()
        {
            var newDish = TestUtilities.GenerateDishes(1).First();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newDish), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/dishes", jsonContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DishDto>(content);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
        }

        [Test]
        public async Task UpdateIngredientAsync_ValidIdAndData_ReturnsNoContentResult()
        {
            var updateDish = TestUtilities.GenerateDishes(1).First();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updateDish), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/dishes/1", jsonContent);
            response.EnsureSuccessStatusCode();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
