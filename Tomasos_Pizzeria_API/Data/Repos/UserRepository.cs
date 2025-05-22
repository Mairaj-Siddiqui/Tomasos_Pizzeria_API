using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Tomasos_Pizzeria_API.Context;
using Tomasos_Pizzeria_API.Data.Interfaces;
using TomasosPizzeriaAPI.Data.Entities;
using TomasosPizzeriaAPI.Helper;

namespace TomasosPizzeriaAPI.Data.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly TomasosDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public UserRepository(TomasosDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            user.PasswordHash = HashPassword(password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            var hash = HashPassword(password);
            return user.PasswordHash == hash ? user : null;
        }

        public Task<string> GenerateJwtTokenAsync(User user)
        {
            return _jwtHelper.CreateTokenAsync(user);
        }

        public async Task<bool> UpdateUserAsync(int id, User update, int requesterId, string role)
        {
            if (id != requesterId && role != "Admin")
                return false;

            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.Email = update.Email;
            user.Phone = update.Phone;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id, int requesterId, string role)
        {
            if (id != requesterId && role != "Admin")
                return false;

            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
