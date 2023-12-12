using AutoMapper;
using Restaurant.BLL.Interfaces;
using Restaurant.BLL.Services.Abstract;
using Restaurant.Common.DTOs.Ingredient;
using Restaurant.Common.Exceptions;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.BLL.Services
{
    public class IngredientsService : BaseService, IIngredientsService
    {
        private readonly IIngredientsRepository _ingredientsRepository;

        public IngredientsService(IMapper mapper, IIngredientsRepository ingredientsRepository) : base(mapper)
        {
            _ingredientsRepository = ingredientsRepository;
        }

        public async Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync()
        {
            var ingredients = await _ingredientsRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
        }

        public async Task<IngredientDto> GetIngredientByIdAsync(int id)
        {
            var ingredient = await FindIngredientByIdOrThrowAsync(id);

            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task<IngredientDto> AddIngredientAsync(NewIngredientDto newIngredient)
        {
            var ingredient = _mapper.Map<Ingredient>(newIngredient);

            await _ingredientsRepository.AddAsync(ingredient);
            await _ingredientsRepository.SaveAsync();

            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task UpdateIngredientAsync(int id, UpdateIngredientDto ingredientToUpdate)
        {
            var ingredient = await FindIngredientByIdOrThrowAsync(id);

            ingredient.Description = ingredientToUpdate.Description;
            ingredient.Name = ingredientToUpdate.Name;

            await _ingredientsRepository.SaveAsync();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredient = await FindIngredientByIdOrThrowAsync(id);

            _ingredientsRepository.Delete(ingredient);
            await _ingredientsRepository.SaveAsync();
        }

        private async Task<Ingredient> FindIngredientByIdOrThrowAsync(int id)
        {
            var ingredient = await _ingredientsRepository.FindByIdAsync(id)
                ?? throw new NotFoundException(nameof(Ingredient));

            return ingredient;
        }
    }
}
