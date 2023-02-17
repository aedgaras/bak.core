using System.Security.Claims;
using bak.api.Dtos;

namespace bak.api.Interface;

public interface ITokenService
{
    string GenerateJSONWebToken(AuthDto userInfo);
    AuthDto AuthenticateUser(AuthDto login);

    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}