using Restaurant.Common.DTOs.Order;

namespace Restaurant.BLL.Interfaces
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();

        Task<OrderDto> GetOrderByIdAsync(int id);

        Task<OrderDto> CreateOrderAsync(NewOrderDto newOrder);

        Task UpdateOrderStatusAsync(int id, UpdateOrderStatusDto orderToUpdate);

        Task DeleteOrderAsync(int id);
    }
}
