using TomasosPizzeriaAPI.Data.Entities;

namespace Tomasos_Pizzeria_API.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User> RegisterAsync(User user, string password);
        Task<User?> ValidateCredentialsAsync(string username, string password);
        Task<string> GenerateJwtTokenAsync(User user);
        Task<bool> UpdateUserAsync(int id, User update, int requesterId, string role);
        Task<bool> DeleteUserAsync(int id, int requesterId, string role);
    }
}
