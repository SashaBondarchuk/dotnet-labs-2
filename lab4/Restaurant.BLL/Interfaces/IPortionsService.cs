using Restaurant.Common.DTOs.Portion;

namespace Restaurant.BLL.Interfaces
{
    public interface IPortionsService
    {
        Task<IEnumerable<PortionDto>> GetAllPortionsAsync();

        Task<PortionDto> AddPortionAsync(NewPortionDto newPortion);

        Task UpdatePortionAsync(int id, UpdatePortionDto portionToUpdate);

        Task DeletePortionAsync(int id);

        Task<IEnumerable<PortionDto>> GetDishPortionsById(int dishId);
    }
}
