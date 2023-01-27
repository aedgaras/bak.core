
using bak.api.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext context;

        public UsersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await context.Users.ToListAsync());
        }

        [HttpGet("{userId}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(user=> user.Id == userId);
            if (user == null)
            {
                return BadRequest("Such user doesnt exist.");
            }

            return Ok(user);
        }
    }
}
