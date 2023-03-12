using vetsys.models.Dtos;

namespace vetsys.contracts;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequestDto mailRequestDto);
}