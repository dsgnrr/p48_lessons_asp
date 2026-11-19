using System;
using System.Security.Claims;

namespace p48_lessons_asp.Services.Token;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GeenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
