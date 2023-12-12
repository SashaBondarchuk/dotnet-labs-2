using Restaurant.Common.DTOs.Ingredient;
using Restaurant.Common.DTOs.Portion;

namespace Restaurant.Common.DTOs.Dish
{
    public class DishDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<IngredientDto>? Ingredients { get; set; }

        public ICollection<PortionDto>? Portions { get; set; }
    }
}
