using PAY_UP.Application.Dtos.Email;

namespace PAY_UP.Application.Abstracts.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequestDto request, string senderEmail);
        Task<bool> SendEmailAsync(string recipientEmail, string subject, string htmlContent, string plainContent = "");
    }
}
