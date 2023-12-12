using AutoMapper;
using Restaurant.BLL.Interfaces;
using Restaurant.BLL.Services.Abstract;
using Restaurant.Common.DTOs.Order;
using Restaurant.Common.Exceptions;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Enums;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.BLL.Services
{
    public class OrdersService : BaseService, IOrdersService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IPortionsRepository _portionsRepository;

        public OrdersService(
            IMapper mapper,
            IOrdersRepository ordersRepository,
            IOrderItemsRepository orderItemsRepository,
            IPortionsRepository portionsRepository) : base(mapper)
        {
            _orderItemsRepository = orderItemsRepository;
            _ordersRepository = ordersRepository;
            _portionsRepository = portionsRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _ordersRepository.GetOrdersAsync();

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _ordersRepository.GetOrderByIdAsync(id)
                ?? throw new NotFoundException(nameof(Order));

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(NewOrderDto newOrder)
        {
            if (newOrder.OrderItems is null || !newOrder.OrderItems.Any())
            {
                throw new BadOperationException("Order items count should be more than 0");
            }

            decimal orderTotalAmount = await GetOrderTotalAmount(newOrder);

            var order = new Order
            {
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                TotalAmount = orderTotalAmount
            };

            await _ordersRepository.AddAsync(order);
            await _ordersRepository.SaveAsync();

            var orderItems = _mapper.Map<IEnumerable<OrderItem>>(newOrder.OrderItems);

            foreach (var item in orderItems)
            {
                item.OrderId = order.Id;
            }

            await _orderItemsRepository.AddRangeAsync(orderItems);
            await _orderItemsRepository.SaveAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task UpdateOrderStatusAsync(int id, UpdateOrderStatusDto orderToUpdate)
        {
            var order = await FindOrderByIdOrThrowAsync(id);

            if (orderToUpdate.Status is OrderStatusDto.Cancelled)
            {
                await DeleteOrderAsync(id);
            }

            order.Status = (OrderStatus)orderToUpdate.Status;
            await _ordersRepository.SaveAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await FindOrderByIdOrThrowAsync(id);

            _ordersRepository.Delete(order);
            await _ordersRepository.SaveAsync();
        }

        private async Task<decimal> GetOrderTotalAmount(NewOrderDto newOrder)
        {
            decimal orderTotalAmount = 0;
            foreach (var orderItem in newOrder.OrderItems!)
            {
                var portionPrice = await GetPortionPriceByIdAsync(orderItem.PortionId);
                orderTotalAmount += orderItem.Quantity * portionPrice;
            }

            return orderTotalAmount;
        }

        private async Task<Order> FindOrderByIdOrThrowAsync(int id)
        {
            var order = await _ordersRepository.FindByIdAsync(id)
                ?? throw new NotFoundException(nameof(Order));

            return order;
        }

        private async Task<decimal> GetPortionPriceByIdAsync(int portionId)
        {
            var portion = await _portionsRepository.FindByIdAsync(portionId)
                ?? throw new NotFoundException(nameof(Portion));

            return portion.Price;
        }
    }
}
