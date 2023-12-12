using Restaurant.Common.DTOs.Ingredient;

namespace Restaurant.Common.DTOs.Dish
{
    public class NewDishDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<IngredientDto>? Ingredients { get; set; }
    }
}
