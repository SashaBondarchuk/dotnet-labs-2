using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.Interfaces;
using Restaurant.Common.DTOs.Portion;

namespace Restaurant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortionsController : ControllerBase
    {
        private readonly IPortionsService _portionsService;

        public PortionsController(IPortionsService portionsService)
        {
            _portionsService = portionsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPortionsAsync()
        {
            var portions = await _portionsService.GetAllPortionsAsync();
            return Ok(portions);
        }

        [HttpGet("{dishId}")]
        public async Task<IActionResult> GetDishPortionsByIdAsync(int dishId)
        {
            var portions = await _portionsService.GetDishPortionsById(dishId);
            return Ok(portions);
        }

        [HttpPost]
        public async Task<IActionResult> AddPortionAsync(NewPortionDto newPortionDto)
        {
            var portion = await _portionsService.AddPortionAsync(newPortionDto);
            return Ok(portion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePortionAsync(int id, UpdatePortionDto updatePortionDto)
        {
            await _portionsService.UpdatePortionAsync(id, updatePortionDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortionAsync(int id)
        {
            await _portionsService.DeletePortionAsync(id);
            return NoContent();
        }
    }
}
