using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TomasosPizzeriaAPI.Data.Entities;
using TomasosPizzeriaAPI.Data.Interfaces;
using TomasosPizzeriaAPI.DTOs;

namespace TomasosPizzeriaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repo;

        public OrdersController(IOrderRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder(PlaceOrderDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var (success, order) = await _repo.PlaceOrderAsync(userId, dto.DishIds);

                if (!success || order == null)
                    return BadRequest("Invalid dish IDs or request.");

                return Ok(new { order.Id, order.TotalPrice });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> MyOrders()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var orders = await _repo.GetUserOrdersAsync(userId);

                var result = orders.Select(o => new OrderResponseDto
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    TotalPrice = o.TotalPrice,
                    Dishes = o.OrderDishes.Select(d => d.Dish.Name).ToList()
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllOrders()
        {
            try
            {
                var orders = await _repo.GetAllOrdersAsync();

                var result = orders.Select(o => new
                {
                    o.Id,
                    o.OrderDate,
                    o.TotalPrice,
                    Customer = o.User.Username,
                    Dishes = o.OrderDishes.Select(d => d.Dish.Name).ToList()
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, UpdateOrderDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var role = User.FindFirstValue(ClaimTypes.Role)!;

                var result = await _repo.UpdateOrderAsync(id, dto.DishIds, userId, role);
                return result ? NoContent() : Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var role = User.FindFirstValue(ClaimTypes.Role)!;

                var result = await _repo.DeleteOrderAsync(id, userId, role);
                return result ? NoContent() : Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }
    }
}
