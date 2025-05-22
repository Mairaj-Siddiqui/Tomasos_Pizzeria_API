using TomasosPizzeriaAPI.Data.Entities;

namespace TomasosPizzeriaAPI.Data.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task<bool> DeleteAsync(int id);
    }
}
