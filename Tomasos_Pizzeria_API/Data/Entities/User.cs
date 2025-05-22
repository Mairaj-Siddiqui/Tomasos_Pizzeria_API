namespace TomasosPizzeriaAPI.Data.Entities
{
    public class User
    {
        public int Id { get; set; }  // Primary Key
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // Store hashed password
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // Default role is User
    }
}
