namespace OptiBid.Microservices.Accounts.Services.Utility
{
    public static class PasswordExtensions
    {
        public static string GenerateHash(this string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool ValidatePassword(this string password,string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
