using Restaurant.Common.DTOs.Portion;

namespace Restaurant.Common.DTOs.OrderItem
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public int PortionId { get; set; }

        public PortionDto? Portion { get; set; }
    }
}
