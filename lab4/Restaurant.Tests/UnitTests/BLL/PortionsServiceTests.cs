using AutoMapper;
using Moq;
using Restaurant.BLL.Interfaces;
using Restaurant.BLL.Services;
using Restaurant.Common.DTOs.Portion;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.Tests.UnitTests.BLL
{
    [TestFixture]
    public class PortionsServiceTests
    {
        private Mock<IPortionsRepository> _mockPortionsRepository;
        private Mock<IMapper> _mockMapper;

        private IPortionsService _portionsService;

        [SetUp]
        public void Setup()
        {
            _mockPortionsRepository = new Mock<IPortionsRepository>();
            _mockMapper = new Mock<IMapper>();

            _portionsService = new PortionsService(
                _mockMapper.Object,
                _mockPortionsRepository.Object);
        }

        [Test]
        public async Task GetAllPortionsAsync_ShouldReturnListOfPortionDto()
        {
            var dishes = TestUtilities.GenerateDishes();
            var portions = TestUtilities.GeneratePortions(dishes);
            var expectedPortionDtos = _mockMapper.Object.Map<IEnumerable<PortionDto>>(portions);
            _mockPortionsRepository.Setup(r => r.GetAllPortionsAsync()).ReturnsAsync(portions);

            var result = await _portionsService.GetAllPortionsAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<PortionDto>>());
            Assert.That(result, Is.EqualTo(expectedPortionDtos));
        }

        [Test]
        public async Task GetDishPortionsById_ShouldReturnListOfPortionDto()
        {
            var dishId = 1;
            var dishes = TestUtilities.GenerateDishes();
            var portions = TestUtilities.GeneratePortions(dishes);
            var expectedPortionDtos = _mockMapper.Object.Map<IEnumerable<PortionDto>>(portions);
            _mockPortionsRepository.Setup(r => r.GetDishPortionsByIdAsync(dishId)).ReturnsAsync(portions);

            var result = await _portionsService.GetDishPortionsById(dishId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<PortionDto>>());
            Assert.That(result, Is.EqualTo(expectedPortionDtos));
        }
    }
}
