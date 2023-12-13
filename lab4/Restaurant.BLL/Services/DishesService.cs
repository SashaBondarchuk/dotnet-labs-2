using AutoMapper;
using Restaurant.BLL.Interfaces;
using Restaurant.BLL.Services.Abstract;
using Restaurant.Common.DTOs.Dish;
using Restaurant.Common.Exceptions;
using Restaurant.DAL.Entities;
using Restaurant.DAL.Repository.Interfaces;

namespace Restaurant.BLL.Services
{
    public class DishesService : BaseService, IDishesService
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IDishIngredientsRepository _dishIngredientsRepository;

        public DishesService(
            IMapper mapper,
            IDishesRepository dishesRepository,
            IDishIngredientsRepository dishIngredientsRepository) : base(mapper)
        {
            _dishesRepository = dishesRepository;
            _dishIngredientsRepository = dishIngredientsRepository;
        }

        public async Task<IEnumerable<DishDto>> GetAllDishesAsync()
        {
            var dishes = await _dishesRepository.GetDishesWithAllInfoAsync();

            return _mapper.Map<IEnumerable<DishDto>>(dishes);
        }

        public async Task<DishDto> GetDishByIdAsync(int id)
        {
            var dish = await _dishesRepository.GetDishWithAllInfoByIdAsync(id);

            return dish is null
                ? throw new NotFoundException(nameof(Dish))
                : _mapper.Map<DishDto>(dish);
        }

        public async Task<DishDto> AddDishAsync(NewDishDto newDish)
        {
            var dish = _mapper.Map<Dish>(newDish);

            await _dishesRepository.AddAsync(dish);
            await _dishesRepository.SaveAsync();

            if (newDish.Ingredients is not null)
            {
                var dishIngredients = _mapper.Map<IEnumerable<Ingredient>>(newDish.Ingredients)
                .Select(ing => new DishIngredient { DishId = dish.Id, IngredientId = ing.Id }).ToList();

                await _dishIngredientsRepository.AddRangeAsync(dishIngredients);
                await _dishIngredientsRepository.SaveAsync();
            }

            return _mapper.Map<DishDto>(dish);
        }

        public async Task UpdateDishAsync(int id, UpdateDishDto dishToUpdate)
        {
            var dish = await FindDishByIdOrThrowAsync(id);

            dish.Description = dishToUpdate.Description;
            dish.Name = dishToUpdate.Name;

            await _dishesRepository.SaveAsync();

            if (dishToUpdate.Ingredients is null)
            {
                throw new BadOperationException("All fields are required");
            }

            var newIngredientIds = dishToUpdate.Ingredients.Select(i => i.Id).ToList();

            var existingDishIngredients = await _dishIngredientsRepository.GetDishIngredientsByDishIdAsync(id);

            var dishIngredientsToRemove = existingDishIngredients
                .Where(di => !newIngredientIds.Contains(di.IngredientId))
                .ToList();

            _dishIngredientsRepository.DeleteRange(dishIngredientsToRemove);

            var currentIngredientIds = existingDishIngredients.Select(di => di.IngredientId).ToList();
            var newDishIngredients = newIngredientIds
                .Where(ingredientId => !currentIngredientIds.Contains(ingredientId))
                .Select(ingredientId => new DishIngredient { DishId = id, IngredientId = ingredientId })
                .ToList();

            await _dishIngredientsRepository.AddRangeAsync(newDishIngredients);
            await _dishIngredientsRepository.SaveAsync();
        }

        public async Task DeleteDishAsync(int id)
        {
            var dish = await FindDishByIdOrThrowAsync(id);

            _dishesRepository.Delete(dish);
            await _dishesRepository.SaveAsync();
        }

        private async Task<Dish> FindDishByIdOrThrowAsync(int id)
        {
            var dish = await _dishesRepository.FindByIdAsync(id)
                ?? throw new NotFoundException(nameof(Dish));

            return dish;
        }
    }
}
