using AutoMapper;
using bak.api.Context;
using bak.api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthRecordsController : Controller
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public HealthRecordsController(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetHealthRecords()
    {
        var healthRecords = mapper.Map<List<HealthRecordDto>>(await context.HealthRecords.ToListAsync());

        return Ok(healthRecords);
    }
}