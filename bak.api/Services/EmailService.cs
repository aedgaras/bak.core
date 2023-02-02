using bak.api.Configurations;
using bak.api.Interface;
using bak.api.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace bak.api.Services;

public class EmailService: IEmailService
{
    private readonly EmailConfiguration emailConfig;
    
    public EmailService(EmailConfiguration emailConfig)
    {
        this.emailConfig = emailConfig;
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(emailConfig.Mail);
        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;
        var builder = new BodyBuilder();
        if (mailRequest.Attachments != null)
        {
            foreach (var file in mailRequest.Attachments.Where(file => file.Length > 0))
            {
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }
                builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            }
        }
        builder.HtmlBody = mailRequest.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(emailConfig.Host, emailConfig.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(emailConfig.Mail, emailConfig.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}