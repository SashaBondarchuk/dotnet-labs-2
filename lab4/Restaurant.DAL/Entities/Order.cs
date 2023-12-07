using Restaurant.DAL.Entities.Abstract;
using Restaurant.DAL.Enums;

namespace Restaurant.DAL.Entities
{
    public class Order : BaseEntity
    {
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
    }
}
