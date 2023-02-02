using bak.api.Models;

namespace bak.api.Interface;

public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}