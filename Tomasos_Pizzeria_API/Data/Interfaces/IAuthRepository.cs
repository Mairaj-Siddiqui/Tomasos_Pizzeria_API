using TomasosPizzeriaAPI.Data.Entities;

namespace Tomasos_Pizzeria_API.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(User user, string password);
        Task<string> GenerateJwtTokenAsync(User user);
        Task<User?> GetByUsernameAsync(string username);

        Task<User?> ValidateCredentialsAsync(string username, string password);

    }
}
