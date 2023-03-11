using bak.api.Dtos;
using bak.api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bak.api.Controllers;

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