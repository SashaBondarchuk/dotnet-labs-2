using AutoMapper;
using Restaurant.BLL.Interfaces;
using Restaurant.BLL.Services.Abstract;
using Restaurant.Common.DTOs.Portion;
using Restaurant.Common.Exceptions;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.BLL.Services
{
    public class PortionsService : BaseService, IPortionsService
    {
        private readonly IPortionsRepository _portionsRepository;

        public PortionsService(
            IMapper mapper,
            IPortionsRepository portionsRepository) : base(mapper)
        {
            _portionsRepository = portionsRepository;
        }

        public async Task<IEnumerable<PortionDto>> GetAllPortionsAsync()
        {
            var portions = await _portionsRepository.GetAllPortionsAsync();

            return _mapper.Map<IEnumerable<PortionDto>>(portions);
        }

        public async Task<IEnumerable<PortionDto>> GetDishPortionsById(int dishId)
        {
            var portions = await _portionsRepository.GetDishPortionsByIdAsync(dishId);

            return _mapper.Map<IEnumerable<PortionDto>>(portions);
        }

        public async Task<PortionDto> AddPortionAsync(NewPortionDto newPortion)
        {
            var portion = _mapper.Map<Portion>(newPortion);

            await _portionsRepository.AddAsync(portion);
            await _portionsRepository.SaveAsync();

            return _mapper.Map<PortionDto>(portion);
        }

        public async Task UpdatePortionAsync(int id, UpdatePortionDto portionToUpdate)
        {
            var portion = await FindPortionByIdOrThrowAsync(id);

            portion.Description = portionToUpdate.Description;
            portion.Price = portionToUpdate.Price;
            portion.Amount = portionToUpdate.Amount;

            await _portionsRepository.SaveAsync();
        }

        public async Task DeletePortionAsync(int id)
        {
            var portion = await FindPortionByIdOrThrowAsync(id);

            _portionsRepository.Delete(portion);
            await _portionsRepository.SaveAsync();
        }

        private async Task<Portion> FindPortionByIdOrThrowAsync(int id)
        {
            var portion = await _portionsRepository.FindByIdAsync(id)
                ?? throw new NotFoundException(nameof(Portion));

            return portion;
        }
    }
}
