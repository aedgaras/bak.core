using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using bak.api.Context;
using bak.api.Dtos;
using bak.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace bak.api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IConfiguration configuration;
    private readonly ApplicationDbContext context;
    private readonly ILogger<AuthController> logger;

    public AuthController(ILogger<AuthController> logger, ApplicationDbContext context, IConfiguration configuration)
    {
        this.logger = logger;
        this.context = context;
        this.configuration = configuration;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] AuthDto login)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = AuthenticateUser(login);

        if (user == null) return Unauthorized("Such user doesn't exist.");

        var tokenString = GenerateJSONWebToken(user);

        return Ok(new { token = tokenString });
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

        var tokenString = GenerateJSONWebToken(register);

        return Ok(new { token = tokenString });
    }

    private string GenerateJSONWebToken(AuthDto userInfo)
    {
        ;
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

    private AuthDto AuthenticateUser(AuthDto login)
    {
        var user = context.Users.FirstOrDefault(x => x.Username == login.Username);

        if (user == null) return null;

        return new AuthDto { Username = user.Username, Password = user.Password };
    }
}