namespace AutoTechAPI.Services
{
    public class PasswordService {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyHashPassword(string initialPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(initialPassword, hashedPassword);
        }
    }
}