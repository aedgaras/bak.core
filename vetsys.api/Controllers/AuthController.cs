using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vetsys.context;
using vetsys.contracts;
using vetsys.entities.Dtos;
using vetsys.entities.Models;

namespace vetsys.api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IConfiguration configuration;
    private readonly ApplicationDbContext context;
    private readonly ITokenService tokenService;

    public AuthController(ApplicationDbContext context, IConfiguration configuration,
        ITokenService tokenService)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] AuthDto login)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = tokenService.AuthenticateUser(login);

        if (user == null) return Unauthorized("Such user doesn't exist.");

        var userModel = context.Users.FirstOrDefault(x => x.Username == login.Username);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim("role", userModel.Role.ToString()),
            new Claim("classification", userModel.Classification.ToString())
        };

        var accessToken = tokenService.GenerateAccessToken(claims);
        var refreshToken = tokenService.GenerateRefreshToken();

        context.Users.Update(userModel).Entity.RefreshToken = refreshToken;
        context.Users.Update(userModel).Entity.RefreshTokenExpiryTime = DateTime.Now.AddDays(14);

        await context.SaveChangesAsync();

        return Ok(new AuthTokenDto { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync([FromBody] AuthDto register)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userExists = context.Users.FirstOrDefault(x => x.Username == register.Username);

        if (userExists != null) return BadRequest("Such user already exists.");

        await context.Users.AddAsync(new User { Username = register.Username, Password = register.Password });
        await context.SaveChangesAsync();

        var userModel = context.Users.FirstOrDefault(x => x.Username == register.Username);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userModel.Username),
            new Claim("role", userModel.Role.ToString()),
            new Claim("classification", userModel.Classification.ToString())
        };

        var accessToken = tokenService.GenerateAccessToken(claims);
        var refreshToken = tokenService.GenerateRefreshToken();

        context.Users.Update(userModel).Entity.RefreshToken = refreshToken;
        context.Users.Update(userModel).Entity.RefreshTokenExpiryTime = DateTime.Now.AddDays(14);

        await context.SaveChangesAsync();

        return Ok(new AuthTokenDto { AccessToken = accessToken, RefreshToken = refreshToken });
    }
}