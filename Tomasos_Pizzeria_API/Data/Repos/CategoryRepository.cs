using Microsoft.EntityFrameworkCore;
using Tomasos_Pizzeria_API.Context;
using TomasosPizzeriaAPI.Data.Entities;
using TomasosPizzeriaAPI.Data.Interfaces;

namespace TomasosPizzeriaAPI.Data.Repos
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TomasosDbContext _context;

        public CategoryRepository(TomasosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) return false;

            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
