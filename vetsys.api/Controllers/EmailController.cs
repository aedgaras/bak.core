using Microsoft.AspNetCore.Mvc;
using vetsys.contracts;
using vetsys.entities.Dtos;

namespace vetsys.api.Controllers;

[Route("[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailService emailService;
    private readonly ILogger<EmailController> logger;

    public EmailController(IEmailService emailService, ILogger<EmailController> logger)
    {
        this.logger = logger;
        this.emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromForm] EmailRequestDto requestDto)
    {
        await emailService.SendEmailAsync(requestDto);
        return Ok();
    }
}