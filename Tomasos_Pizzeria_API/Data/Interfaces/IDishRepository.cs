using TomasosPizzeriaAPI.Data.Entities;

namespace Tomasos_Pizzeria_API.Data.Interfaces
{
    public interface IDishRepository
    {
        Task<List<Dish>> GetAllAsync();
        Task<List<Dish>> GetByCategoryAsync(int categoryId);
        Task<Dish?> GetByIdAsync(int id);
        Task<Dish> CreateAsync(Dish dish);
        Task<bool> UpdateAsync(int id, Dish updatedDish);
        Task<bool> DeleteAsync(int id);
    }
}
