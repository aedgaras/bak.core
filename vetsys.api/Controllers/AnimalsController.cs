using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vetsys.context;
using vetsys.entities.Dtos;

namespace vetsys.api.Controllers;

[Route("[controller]")]
[ApiController]
public class AnimalsController : Controller
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public AnimalsController(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAnimals()
    {
        var animals = mapper.Map<List<AnimalDto>>(await context.Animals.ToListAsync());

        return Ok(animals);
    }
}