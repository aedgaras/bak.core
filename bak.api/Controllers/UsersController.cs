using bak.api.Context;
using bak.api.Dtos;
using bak.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : Controller
{
    private readonly ApplicationDbContext context;
    private ILogger<UsersController> logger;

    public UsersController(ILogger<UsersController> logger, ApplicationDbContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await context.Users.Include(u => u.Cases).ToListAsync());
    }

    [HttpGet("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUser(int userId)
    {
        if (userId <= 0) return BadRequest("UserId cannot be lower than 1.");
        var user = await context.Users.Where(user => user.Id == userId).Include(u => u.Cases).FirstOrDefaultAsync();
        if (user == null) return BadRequest("Such user doesnt exist.");

        return Ok(user);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddUser([FromBody] UserDto user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userExists = await context.Users.FirstOrDefaultAsync(x => x.Username == user.Username);

        if (userExists != null) return BadRequest("Such user exists.");

        await context.Users.AddAsync(new User { Username = user.Username, Password = user.Password, Role = user.Role });
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{userId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        if (userId <= 0) return BadRequest("UserId cannot be lower than 1.");

        var userExists = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (userExists == null) return BadRequest("Such user doesn't exist.");

        context.Users.Remove(userExists);

        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("{userId:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto user, int userId)
    {
        if (userId <= 0) return BadRequest("UserId cannot be lower than 1.");

        if (!ModelState.IsValid) return BadRequest(ModelState);

        return Ok();
    }
}