using Restaurant.DAL.Entities.Abstract;

namespace Restaurant.DAL.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int PortionId { get; set; }

        public Order? Order { get; set; }
        public Portion? Portion { get; set; }
    }
}
