using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TomasosPizzeriaAPI.Data.Entities;

namespace TomasosPizzeriaAPI.Helper
{
    public class JwtHelper
    {
        private readonly IConfiguration _config;

        public JwtHelper(IConfiguration config)
        {
            _config = config;
        }

        public Task<string> CreateTokenAsync(User user)
        {
            //Retrieve the JWT secret key from Key Vault via IConfiguration
            var jwtKey = _config["JwtSecret"];

            if (string.IsNullOrWhiteSpace(jwtKey))
                throw new Exception("JWT secret is missing or empty in JwtHelper. Check your Azure Key Vault or configuration.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Create claims for user
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            //Create JWT token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            //Return token as string
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
