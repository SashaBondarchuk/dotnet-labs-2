using Restaurant.Common.DTOs.Dish;

namespace Restaurant.BLL.Interfaces
{
    public interface IDishesService
    {
        Task<IEnumerable<DishDto>> GetAllDishesAsync();

        Task<DishDto> GetDishByIdAsync(int id);

        Task<DishDto> AddDishAsync(NewDishDto newDish);

        Task UpdateDishAsync(int id, UpdateDishDto dishToUpdate);

        Task DeleteDishAsync(int id);
    }
}
