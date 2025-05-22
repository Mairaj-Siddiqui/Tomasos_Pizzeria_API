using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Tomasos_Pizzeria_API.Context;
using Tomasos_Pizzeria_API.Data.Interfaces;
using TomasosPizzeriaAPI.Data.Entities;
using TomasosPizzeriaAPI.Helper;

namespace Tomasos_Pizzeria_API.Data.Repos
{
    public class AuthRepository : IAuthRepository
    {
        private readonly TomasosDbContext _context;
       private readonly JwtHelper _jwtHelper;

        public AuthRepository(TomasosDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            return await _jwtHelper.CreateTokenAsync(user);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            try
            {
                user.PasswordHash = HashPassword(password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error while registering user: " + ex.Message);
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }


        public async Task<User?> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            var inputHash = HashPassword(password);
            return user.PasswordHash == inputHash ? user : null;
        }        

    }
}
