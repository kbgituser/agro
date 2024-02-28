using PlatF.Model.WebApi.Models;
using System.Security.Claims;

namespace WebApi.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<AuthenticatedResponse> Login(LoginModel loginModel);
    }
}
