using vetsys.api.Configurations;
using vetsys.contracts;
using vetsys.models.Dtos;

namespace vetsys.api.Services;

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