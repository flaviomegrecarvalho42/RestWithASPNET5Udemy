using System.Collections.Generic;
using System.Security.Claims;

namespace RestWithAspNet5Udemy.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string refreshToken);
    }
}
