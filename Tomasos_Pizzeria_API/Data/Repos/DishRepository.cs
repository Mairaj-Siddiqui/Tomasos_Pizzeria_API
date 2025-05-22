using Microsoft.EntityFrameworkCore;
using Tomasos_Pizzeria_API.Context;
using Tomasos_Pizzeria_API.Data.Interfaces;
using TomasosPizzeriaAPI.Data.Entities;

namespace TomasosPizzeriaAPI.Data.Repos
{
    public class DishRepository : IDishRepository
    {
        private readonly TomasosDbContext _context;

        public DishRepository(TomasosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dish>> GetAllAsync()
        {
            return await _context.Dishes
                .Include(d => d.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Dish>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Dishes
                .Where(d => d.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Dish?> GetByIdAsync(int id)
        {
            return await _context.Dishes.FindAsync(id);
        }

        public async Task<Dish> CreateAsync(Dish dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
            return dish;
        }

        public async Task<bool> UpdateAsync(int id, Dish updated)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null) return false;

            dish.Name = updated.Name;
            dish.Price = updated.Price;
            dish.Description = updated.Description;
            dish.Ingredients = updated.Ingredients;
            dish.CategoryId = updated.CategoryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null) return false;

            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
