using Restaurant.Common.DTOs.OrderItem;

namespace Restaurant.Common.DTOs.Order
{
    public class OrderDto
    {
        public OrderStatusDto Status { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItemDto>? OrderItems { get; set; }
    }
}
