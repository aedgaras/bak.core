using bak.api.Context;
using bak.api.Enums;
using bak.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Controllers;

[ApiController]
[Route("[controller]")]
public class CasesController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private ILogger<CasesController> logger;

    public CasesController(ILogger<CasesController> logger, ApplicationDbContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCases()
    {
        return Ok(await context.Cases.ToListAsync());
    }

    [HttpGet("{caseId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCase(int caseId)
    {
        if (caseId <= 0) return BadRequest("CaseId cannot be lower than 1.");
        var caseObj = await context.Cases.FirstOrDefaultAsync(x => x.Id == caseId);
        if (caseObj == null) return BadRequest("Such case doesnt exist.");

        return Ok(caseObj);
    }

    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserCases(int userId)
    {
        if (userId <= 0) return BadRequest("UserId cannot be lower than 1.");
        var user = await context.Users.Include(u => u.Cases).ToListAsync();
        if (user == null) return BadRequest("Such user doesnt exist.");
    
        return Ok(user);
    }

    [HttpPost("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddUserCase(int userId)
    {
        if (userId <= 0) return BadRequest("UserId cannot be lower than 1.");
        var user = await context.Users.Include(u => u.Cases).FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) return BadRequest("Such user doesnt exist.");

        var caseToAdd = new Case { Status = CaseStatus.Filled };

        user.Cases.Add(caseToAdd);

        await context.SaveChangesAsync();

        return Ok();
    }
}