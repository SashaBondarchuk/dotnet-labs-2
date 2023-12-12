using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.Interfaces;
using Restaurant.Common.DTOs.Dish;

namespace Restaurant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IDishesService _dishService;

        public DishesController(IDishesService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDishesAsync()
        {
            var dishes = await _dishService.GetAllDishesAsync();
            return Ok(dishes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDishByIdAsync(int id)
        {
            var dish = await _dishService.GetDishByIdAsync(id);
            return Ok(dish);
        }

        [HttpPost]
        public async Task<IActionResult> AddDishAsync(NewDishDto newDishDto)
        {
            var dish = await _dishService.AddDishAsync(newDishDto);
            return Ok(dish);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredientAsync(int id, UpdateDishDto updateDishDto)
        {
            await _dishService.UpdateDishAsync(id, updateDishDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDishAsync(int id)
        {
            await _dishService.DeleteDishAsync(id);
            return NoContent();
        }
    }
}
