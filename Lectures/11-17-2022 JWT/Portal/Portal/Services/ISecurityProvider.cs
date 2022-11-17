using System.Security.Claims;

namespace Portal.Services
{
    public interface ISecurityProvider
    {
        bool ValidateToken(string token);

        string GetToken(List<Claim> claims);
    }
}
