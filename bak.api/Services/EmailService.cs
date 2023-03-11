using bak.api.Configurations;
using bak.contracts;
using bak.models.Dtos;

namespace bak.api.Services;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration emailConfig;

    public EmailService(EmailConfiguration emailConfig)
    {
        this.emailConfig = emailConfig;
    }

    public async Task SendEmailAsync(EmailRequestDto mailRequestDto)
    {
    }
}