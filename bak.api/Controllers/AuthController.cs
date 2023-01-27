using bak.api.Context;
using bak.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bak.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ILogger<WeatherForecastController> logger;
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public AuthController(ILogger<WeatherForecastController> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            this.logger = logger;
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost("login"), AllowAnonymous]
        public IActionResult Login([FromBody] User login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = AuthenticateUser(login);

            if (user == null)
            {
                return Unauthorized("Such user doesn't exist.");
            }

            var tokenString = GenerateJSONWebToken(user);

            return Ok(new { token = tokenString });
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] User register) 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExists = context.Users.FirstOrDefault(x => x.Username== register.Username);

            if (userExists != null) 
            {
                return BadRequest("Such user already exists.");
            }

            await context.Users.AddAsync(register);
            await context.SaveChangesAsync();

            var tokenString = GenerateJSONWebToken(register);

            return Ok(new { token = tokenString });
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User AuthenticateUser(User login)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == login.Username);

            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}
