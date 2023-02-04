using AutoMapper;
using bak.api.Context;
using bak.api.Dtos;
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
    private readonly IMapper mapper;

    public CasesController(ILogger<CasesController> logger, ApplicationDbContext context, IMapper mapper)
    {
        this.logger = logger;
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCases()
    {
        var cases = mapper.Map<List<CaseDto>>(await context.Cases.ToListAsync());

        return Ok(cases);
    }

    [HttpGet("{caseId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCase(int caseId)
    {
        if (caseId <= 0) return BadRequest("CaseId cannot be lower than 1.");
        var caseObj = await context.Cases.FirstOrDefaultAsync(x => x.Id == caseId);
        if (caseObj == null) return BadRequest("Such case doesnt exist.");

        var caseDto = mapper.Map<CaseDto>(caseObj);


        return Ok(caseDto);
    }

    [HttpGet("user/{userId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserCases(int userId)
    {
        if (userId <= 0) return BadRequest("UserId cannot be lower than 1.");
        var userCases = await context.Cases.Where(c => c.UserId == userId).ToListAsync();

        var casesDto = mapper.Map<List<CaseDto>>(userCases);

        return Ok(casesDto);
    }

    [HttpPost("user/{userId:int}")]
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

    [HttpPost("user/{userId:int}/case/{caseId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUserCase([FromBody] CaseDto caseDto, int userId, int caseId)
    {
        if (userId <= 0 || caseId <= 0) return BadRequest("Id cannot be lower than 1.");
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) return BadRequest("Such user doesnt exist.");

        var caseToUpdate = await context.Cases.Where(c => c.UserId == userId).FirstOrDefaultAsync(x => x.Id == caseId);

        if (caseToUpdate == null || !Enum.TryParse<CaseStatus>(caseDto.Status, true, out var status))
            return BadRequest("Such case doesnt exist.");

        caseToUpdate.Status = status;

        context.Cases.Update(caseToUpdate);

        await context.SaveChangesAsync();

        return Ok();
    }
}