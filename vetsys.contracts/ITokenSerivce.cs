using System.Security.Claims;
using vetsys.models.Dtos;

namespace vetsys.contracts;

public interface ITokenService
{
    string GenerateJSONWebToken(AuthDto userInfo);
    AuthDto AuthenticateUser(AuthDto login);

    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}