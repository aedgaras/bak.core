using bak.api.Dtos;

namespace bak.api.Interface;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequestDto mailRequestDto);
}