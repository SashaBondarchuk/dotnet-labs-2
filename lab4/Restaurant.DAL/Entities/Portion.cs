using Restaurant.DAL.Entities.Abstract;

namespace Restaurant.DAL.Entities
{
    public class Portion : BaseEntity
    {
        public int DishId { get; set; }
        public int UnitId { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Dish? Dish { get; set; }
        public Unit? Unit { get; set; }

        public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

        public Portion(string description)
        {
            Description = description;
        }
    }
}
