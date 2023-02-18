

namespace OptiBid.Microservices.Contracts.Services
{
    public interface IJwtManager
    {
        string GenerateToken(string username, string roleName);
    }
}
