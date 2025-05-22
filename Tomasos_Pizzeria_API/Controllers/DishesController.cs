using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tomasos_Pizzeria_API.Data.Interfaces;
using TomasosPizzeriaAPI.Data.Entities;
using TomasosPizzeriaAPI.Data.Interfaces;
using TomasosPizzeriaAPI.DTOs;

namespace TomasosPizzeriaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly IDishRepository _repo;

        public DishesController(IDishRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var dishes = await _repo.GetAllAsync();
                return Ok(dishes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            try
            {
                var dishes = await _repo.GetByCategoryAsync(categoryId);
                return Ok(dishes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateDishDto dto)
        {
            try
            {
                var dish = new Dish
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Description = dto.Description,
                    Ingredients = dto.Ingredients,
                    CategoryId = dto.CategoryId
                };

                var created = await _repo.CreateAsync(dish);
                return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, CreateDishDto dto)
        {
            try
            {
                var updated = new Dish
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Description = dto.Description,
                    Ingredients = dto.Ingredients,
                    CategoryId = dto.CategoryId
                };

                var result = await _repo.UpdateAsync(id, updated);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _repo.DeleteAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }
    }
}
