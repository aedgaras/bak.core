using AutoMapper;
using bak.context;
using bak.models.Dtos;
using bak.models.Enums;
using bak.models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : Controller
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private ILogger<UsersController> logger;

    public UsersController(ILogger<UsersController> logger, ApplicationDbContext context, IMapper mapper)
    {
        this.logger = logger;
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsers()
    {
        var users = mapper.Map<List<UserDto>>(await context.Users.ToListAsync());

        return Ok(users);
    }

    [HttpGet("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUser(int userId)
    {
        if (userId <= 0) return BadRequest("UserId cannot be lower than 1.");
        var user = await context.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
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

        if (!Enum.TryParse<Role>(user.Role, true, out var role)) return BadRequest("Such role doesnt exist.");

        var userToAdd = mapper.Map<User>(user);

        await context.Users.AddAsync(userToAdd);
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

        var userToUpdate = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user.Role != null && !Enum.TryParse<Role>(user.Role, true, out var role)) userToUpdate.Role = role;

        context.Users.Update(userToUpdate);

        return Ok();
    }
}