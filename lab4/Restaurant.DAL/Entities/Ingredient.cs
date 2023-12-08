using Restaurant.DAL.Entities.Abstract;

namespace Restaurant.DAL.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<DishIngredient> DishIngredients { get; } = new List<DishIngredient>();
        public ICollection<Dish> Dishes { get; } = new List<Dish>();

        public Ingredient(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
