using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vetsys.context;

namespace vetsys.api.Controllers;

[ApiController]
[Route("[controller]")]
public class ViewHistoryController: Controller
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private ILogger<UsersController> logger;

    public ViewHistoryController(ILogger<UsersController> logger, ApplicationDbContext context, IMapper mapper)
    {
        this.logger = logger;
        this.context = context;
        this.mapper = mapper;
    }
}