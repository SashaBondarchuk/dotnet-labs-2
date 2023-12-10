using Restaurant.DAL.Entities.Abstract;

namespace Restaurant.DAL.Entities
{
    public class Dish : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Ingredient> Ingredients { get; } = new List<Ingredient>();
        public ICollection<Portion> Portions { get; } = new List<Portion>();

        public Dish(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
