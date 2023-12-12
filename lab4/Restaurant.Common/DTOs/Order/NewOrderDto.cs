using Restaurant.Common.DTOs.OrderItem;

namespace Restaurant.Common.DTOs.Order
{
    public class NewOrderDto
    {
        public ICollection<NewOrderItemDto>? OrderItems { get; set; }
    }
}
