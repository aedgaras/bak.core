using bak.api.Dtos;

namespace bak.api.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequestDto mailRequestDto);
}