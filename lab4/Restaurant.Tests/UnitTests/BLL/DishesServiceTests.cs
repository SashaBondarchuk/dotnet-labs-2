using AutoMapper;
using Moq;
using Restaurant.BLL.Interfaces;
using Restaurant.BLL.Services;
using Restaurant.Common.DTOs.Dish;
using Restaurant.Common.Exceptions;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.Tests.UnitTests.BLL
{
    [TestFixture]
    public class DishesServiceTests
    {
        private Mock<IDishesRepository> _mockDishesRepository;
        private Mock<IDishIngredientsRepository> _mockDishIngredientsRepository;
        private Mock<IMapper> _mockMapper;
        private IDishesService _dishesService;

        [SetUp]
        public void Setup()
        {
            _mockDishesRepository = new Mock<IDishesRepository>();
            _mockDishIngredientsRepository = new Mock<IDishIngredientsRepository>();
            _mockMapper = new Mock<IMapper>();

            _dishesService = new DishesService(
                _mockMapper.Object,
                _mockDishesRepository.Object,
                _mockDishIngredientsRepository.Object);
        }

        [Test]
        public async Task GetAllDishesAsync_ShouldReturnDishDtos()
        {
            var dishes = TestUtilities.GenerateDishes();
            var dishDtos = _mockMapper.Object.Map<IEnumerable<DishDto>>(dishes);

            _mockDishesRepository.Setup(r => r.GetDishesWithAllInfoAsync()).ReturnsAsync(dishes);
            _mockMapper.Setup(m => m.Map<IEnumerable<DishDto>>(dishes)).Returns(dishDtos);

            var result = await _dishesService.GetAllDishesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<DishDto>>());
            Assert.That(result, Is.EqualTo(dishDtos));
        }

        [Test]
        public void GetDishByIdAsync_NonExistingId_ShouldThrowNotFoundException()
        {
            var dishId = 1;
            _mockDishesRepository.Setup(r => r.GetDishWithAllInfoByIdAsync(dishId)).ReturnsAsync((Dish)null);

            Assert.ThrowsAsync<NotFoundException>(async () => await _dishesService.GetDishByIdAsync(dishId));
        }

        [Test]
        public async Task GetAllDishesAsync_ShouldReturnListOfDishDto()
        {
            var dishes = TestUtilities.GenerateDishes(3);
            var expectedDishDtos = _mockMapper.Object.Map<IEnumerable<DishDto>>(dishes);
            _mockDishesRepository.Setup(r => r.GetDishesWithAllInfoAsync()).ReturnsAsync(dishes);

            var result = await _dishesService.GetAllDishesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<DishDto>>());
            Assert.That(result, Is.EqualTo(expectedDishDtos));
        }

        [Test]
        public void AddDishAsync_NullIngredients_ShouldNotThrowException()
        {
            var newDishDto = new NewDishDto
            {
                Name = "New Dish",
                Description = "Description",
                Ingredients = null
            };

            Assert.DoesNotThrowAsync(async () => await _dishesService.AddDishAsync(newDishDto));
        }
    }
}
