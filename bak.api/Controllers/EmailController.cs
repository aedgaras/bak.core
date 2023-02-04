using bak.api.Dtos;
using bak.api.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bak.api.Controllers;

[Route(("[controller]"))]
[ApiController]
public class EmailController: ControllerBase
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
        try
        {
            await emailService.SendEmailAsync(requestDto);
            return Ok();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}