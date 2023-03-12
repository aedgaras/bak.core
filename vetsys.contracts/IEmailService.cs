using vetsys.entities.Dtos;

namespace vetsys.contracts;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequestDto mailRequestDto);
}