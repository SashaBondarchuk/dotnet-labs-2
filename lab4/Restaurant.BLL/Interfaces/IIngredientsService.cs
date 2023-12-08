using Restaurant.Common.DTOs.Ingredient;

namespace Restaurant.BLL.Interfaces
{
    public interface IIngredientsService
    {
        Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync();

        Task<IngredientDto> GetIngredientByIdAsync(int id);

        Task<IngredientDto> AddIngredientAsync(NewIngredientDto newIngredient);

        Task UpdateIngredientAsync(int id, UpdateIngredientDto ingredientToUpdate);

        Task DeleteIngredientAsync(int id);
    }
}