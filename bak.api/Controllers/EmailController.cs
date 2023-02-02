using bak.api.Interface;
using bak.api.Models;
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
    public async Task<IActionResult> SendMail([FromForm]MailRequest request)
    {
        try
        {
            await emailService.SendEmailAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}