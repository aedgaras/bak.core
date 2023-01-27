using bak.api.Context;
using bak.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> logger;
        private readonly ApplicationDbContext context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var list = await context.WeatherForecasts.ToListAsync();

            if(list.Count == 0) 
            {
                var forecast = new WeatherForecast
                {
                    TemperatureC = 24
                };

                await context.WeatherForecasts.AddAsync(forecast);
                await context.SaveChangesAsync();
            }


            return Ok(await context.WeatherForecasts.ToListAsync());
        }
    }
}