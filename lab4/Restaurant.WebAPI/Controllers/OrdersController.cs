using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.Interfaces;
using Restaurant.Common.DTOs.Order;

namespace Restaurant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var orders = await _ordersService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var order = await _ordersService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> AddDishAsync(NewOrderDto newOrderDto)
        {
            var order = await _ordersService.CreateOrderAsync(newOrderDto);
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItemsAsync(int id, UpdateOrderStatusDto updateOrderStatusDto)
        {
            await _ordersService.UpdateOrderStatusAsync(id, updateOrderStatusDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            await _ordersService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
