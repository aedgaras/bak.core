using bak.models.Dtos;

namespace bak.contracts;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequestDto mailRequestDto);
}