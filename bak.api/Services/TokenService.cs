using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using bak.context;
using bak.contracts;
using bak.models.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace bak.api.Services;

public class TokenService : ITokenService
{
    private readonly ApplicationDbContext context;

    public TokenService(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public string GenerateJSONWebToken(AuthDto userInfo)
    {
        var securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var user = context.Users.FirstOrDefault(x => x.Username == userInfo.Username);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
            new Claim("role", user.Role.ToString()),
            new Claim("classification", user.Classification.ToString())
        };

        var token = new JwtSecurityToken(Environment.GetEnvironmentVariable("JWT_ISSUER"),
            Environment.GetEnvironmentVariable("JWT_ISSUER"),
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public AuthDto AuthenticateUser(AuthDto login)
    {
        var user = context.Users.FirstOrDefault(x => x.Username == login.Username);

        return user == null ? null : new AuthDto { Username = user.Username, Password = user.Password };
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            Environment.GetEnvironmentVariable("JWT_ISSUER"),
            Environment.GetEnvironmentVariable("JWT_ISSUER"),
            claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return tokenString;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"))),
            ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }
}