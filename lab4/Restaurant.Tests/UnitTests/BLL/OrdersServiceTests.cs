using AutoMapper;
using Moq;
using Restaurant.BLL.Interfaces;
using Restaurant.BLL.Services;
using Restaurant.Common.DTOs.Order;
using Restaurant.Common.Exceptions;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Enums;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.Tests.UnitTests.BLL
{
    [TestFixture]
    public class OrdersServiceTests
    {
        private Mock<IOrdersRepository> _mockOrdersRepository;
        private Mock<IOrderItemsRepository> _mockOrderItemsRepository;
        private Mock<IPortionsRepository> _mockPortionsRepository;
        private Mock<IMapper> _mockMapper;

        private IOrdersService _ordersService;

        [SetUp]
        public void Setup()
        {
            _mockOrdersRepository = new Mock<IOrdersRepository>();
            _mockOrderItemsRepository = new Mock<IOrderItemsRepository>();
            _mockPortionsRepository = new Mock<IPortionsRepository>();
            _mockMapper = new Mock<IMapper>();

            _ordersService = new OrdersService(
                _mockMapper.Object,
                _mockOrdersRepository.Object,
                _mockOrderItemsRepository.Object,
                _mockPortionsRepository.Object);
        }

        [Test]
        public void GetOrderByIdAsync_InvalidId_ThrowsNotFoundException()
        {
            var invalidId = 999;
            _mockOrdersRepository.Setup(r => r.GetOrderByIdAsync(invalidId)).ReturnsAsync((Order)null);

            Assert.ThrowsAsync<NotFoundException>(async () => await _ordersService.GetOrderByIdAsync(invalidId));
        }

        [Test]
        public async Task UpdateOrderStatusAsync_ValidIdAndStatus_UpdatesOrderStatus()
        {
            var orderId = 1;
            var order = new Order { Id = orderId, Status = OrderStatus.Pending };
            _mockOrdersRepository.Setup(r => r.FindByIdAsync(orderId)).ReturnsAsync(order);

            var updateDto = new UpdateOrderStatusDto { Status = OrderStatusDto.Pending };
            await _ordersService.UpdateOrderStatusAsync(orderId, updateDto);

            Assert.That(order.Status, Is.EqualTo(OrderStatus.Pending));
            _mockOrdersRepository.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateOrderStatusAsync_CancelledStatus_DeletesOrder()
        {
            var orderId = 1;
            var order = new Order { Id = orderId, Status = OrderStatus.Pending };
            _mockOrdersRepository.Setup(r => r.FindByIdAsync(orderId)).ReturnsAsync(order);

            var updateDto = new UpdateOrderStatusDto { Status = OrderStatusDto.Cancelled };

            await _ordersService.UpdateOrderStatusAsync(orderId, updateDto);

            _mockOrdersRepository.Verify(r => r.Delete(It.IsAny<Order>()), Times.Once);
            _mockOrdersRepository.Verify(r => r.Delete(It.Is<Order>(o => o.Id == orderId)), Times.Once);
        }

        [Test]
        public async Task DeleteOrderAsync_ValidId_DeletesOrder()
        {
            var orderId = 1;
            var order = new Order { Id = orderId, Status = OrderStatus.Pending };
            _mockOrdersRepository.Setup(r => r.FindByIdAsync(orderId)).ReturnsAsync(order);

            await _ordersService.DeleteOrderAsync(orderId);

            _mockOrdersRepository.Verify(r => r.Delete(order), Times.Once);
            _mockOrdersRepository.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Test]
        public void DeleteOrderAsync_InvalidId_ThrowsNotFoundException()
        {
            var invalidId = 999;
            _mockOrdersRepository.Setup(r => r.FindByIdAsync(invalidId)).ReturnsAsync((Order)null);

            Assert.ThrowsAsync<NotFoundException>(async () => await _ordersService.DeleteOrderAsync(invalidId));
        }
    }
}
