using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.Interfaces;
using Restaurant.Common.DTOs.Ingredient;

namespace Restaurant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsService _ingredientsService;

        public IngredientsController(IIngredientsService ingredientsService)
        {
            _ingredientsService = ingredientsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredientsAsync()
        {
            var ingredients = await _ingredientsService.GetAllIngredientsAsync();
            return Ok(ingredients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientByIdAsync(int id)
        {
            var ingredient = await _ingredientsService.GetIngredientByIdAsync(id);
            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredientAsync(NewIngredientDto newIngredient)
        {
            var ingredient = await _ingredientsService.AddIngredientAsync(newIngredient);
            return Ok(ingredient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredientAsync(int id, UpdateIngredientDto ingredientToUpdate)
        {
            await _ingredientsService.UpdateIngredientAsync(id, ingredientToUpdate);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientAsync(int id)
        {
            await _ingredientsService.DeleteIngredientAsync(id);
            return NoContent();
        }
    }
}
