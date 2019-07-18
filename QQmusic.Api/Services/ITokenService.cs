using System.Collections.Generic;
using System.Security.Claims;

namespace QQmusic.Api.Services
{
    public interface ITokenService
    {
        int GetDurationInMinutes();
        int GetRefreshDurationInMinutes();
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}