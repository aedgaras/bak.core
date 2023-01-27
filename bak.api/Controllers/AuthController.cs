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
        public IActionResult Login([FromBody] UserDto login)
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
        public async Task<IActionResult> RegisterAsync([FromBody] UserDto register) 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExists = context.Users.FirstOrDefault(x => x.Username == register.Username);

            if (userExists != null) 
            {
                return BadRequest("Such user already exists.");
            }

            await context.Users.AddAsync(new User { Username = register.Username, Password = register.Password });
            await context.SaveChangesAsync();

            var tokenString = GenerateJSONWebToken(register);

            return Ok(new { token = tokenString });
        }

        private string GenerateJSONWebToken(UserDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var user = context.Users.FirstOrDefault(x => x.Username == userInfo.Username);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim("role", user.Role.ToString()),
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserDto AuthenticateUser(UserDto login)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == login.Username);

            if (user == null)
            {
                return null;
            }

            return new UserDto { Username = user.Username, Password = user.Password };
        }

        public class UserDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
