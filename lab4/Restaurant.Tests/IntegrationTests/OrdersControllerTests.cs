using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Restaurant.Common.DTOs.Ingredient;
using Restaurant.DAL.Context;
using Restaurant.WebAPI;
using System.Net;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Entities;
using System.Text;
using Restaurant.Common.DTOs.Order;
using Restaurant.Common.DTOs.OrderItem;

namespace Restaurant.Tests.IntegrationTests
{
    [TestFixture]
    public class OrdersControllerTests
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
        public async Task AddOrderAsync_WithOrderItems_ReturnsOkResult()
        {
            var newOrderItemDto = new NewOrderItemDto
            {
                Quantity = 2,
                PortionId = 1
            };

            var newOrderDto = new NewOrderDto
            {
                OrderItems = new List<NewOrderItemDto> { newOrderItemDto }
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(newOrderDto), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/orders", jsonContent);
            response.EnsureSuccessStatusCode();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OrderDto>(content);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.OrderItems, Is.Not.Null);
                Assert.That(result.OrderItems, Has.Count.EqualTo(1));
                Assert.That(result.OrderItems.First().Quantity, Is.EqualTo(newOrderItemDto.Quantity));
                Assert.That(result.OrderItems.First().PortionId, Is.EqualTo(newOrderItemDto.PortionId));
            });
        }

        [Test]
        public async Task UpdateOrderStatusAsync_ValidIdAndData_ReturnsOkResult()
        {
            var orderId = 1;
            var updateOrderStatusDto = new UpdateOrderStatusDto
            {
                Status = OrderStatusDto.Fulfilled
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(updateOrderStatusDto), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"/api/orders/{orderId}", jsonContent);
            response.EnsureSuccessStatusCode();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task AddAndUpdateOrderAsync_WithOrderItems_ReturnsUpdatedOrder()
        {
            var newOrderItemDto = new NewOrderItemDto
            {
                Quantity = 2,
                PortionId = 1
            };

            var newOrderDto = new NewOrderDto
            {
                OrderItems = new List<NewOrderItemDto> { newOrderItemDto }
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(newOrderDto), Encoding.UTF8, "application/json");

            var addResponse = await _client.PostAsync("/api/orders", jsonContent);
            addResponse.EnsureSuccessStatusCode();

            var addedOrder = JsonConvert.DeserializeObject<OrderDto>(await addResponse.Content.ReadAsStringAsync());

            var updateOrderStatusDto = new UpdateOrderStatusDto
            {
                Status = OrderStatusDto.Fulfilled
            };

            var updateJsonContent = new StringContent(JsonConvert.SerializeObject(updateOrderStatusDto), Encoding.UTF8, "application/json");

            var updateResponse = await _client.PutAsync($"/api/orders/{addedOrder.Id}", updateJsonContent);
            updateResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync($"/api/orders/{addedOrder.Id}");
            getResponse.EnsureSuccessStatusCode();

            var content = await getResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OrderDto>(content);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.OrderItems, Is.Not.Null);
                Assert.That(result.OrderItems.Count, Is.EqualTo(1));
                Assert.That(result.OrderItems.First().Quantity, Is.EqualTo(newOrderItemDto.Quantity));
                Assert.That(result.OrderItems.First().PortionId, Is.EqualTo(newOrderItemDto.PortionId));
                Assert.That(result.Status, Is.EqualTo(OrderStatusDto.Fulfilled));
            });
        }

        [Test]
        public async Task AddAndDeleteOrderAsync_WithOrderItems_ReturnsNoContentResult()
        {
            var newOrderItemDto = new NewOrderItemDto
            {
                Quantity = 2,
                PortionId = 1
            };

            var newOrderDto = new NewOrderDto
            {
                OrderItems = new List<NewOrderItemDto> { newOrderItemDto }
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(newOrderDto), Encoding.UTF8, "application/json");

            var addResponse = await _client.PostAsync("/api/orders", jsonContent);
            addResponse.EnsureSuccessStatusCode();

            var addedOrder = JsonConvert.DeserializeObject<OrderDto>(await addResponse.Content.ReadAsStringAsync());

            var deleteResponse = await _client.DeleteAsync($"/api/orders/{addedOrder.Id}");
            deleteResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync($"/api/orders/{addedOrder.Id}");
            Assert.Multiple(() =>
            {
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            });
        }
    }
}
