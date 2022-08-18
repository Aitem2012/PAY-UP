using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PAY_UP.Application.Abstracts.Infrastructure;
using PAY_UP.Application.Dtos.Email;
using SendGrid;
using SendGrid.Extensions.DependencyInjection;
using SendGrid.Helpers.Mail;

namespace PAY_UP.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private static IConfiguration Configuration { get; set; }
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }
        public async Task SendEmailAsync(EmailRequestDto request, string senderEmail)
        {
            var services = ConfigureServices(new ServiceCollection()).BuildServiceProvider();
            var client = services.GetRequiredService<ISendGridClient>();
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail),
                Subject = request.Subject
            };
            msg.AddContent(MimeType.Text, request.Message);
            msg.AddTo(new EmailAddress(request.RecipientEmail));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            _logger.LogInformation($"Mail sending returns: {response.StatusCode}");
        }
        //Todo: send another email

        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSendGrid(options => { options.ApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY", EnvironmentVariableTarget.User) ?? Configuration["SendGrid:ApiKey"]; });

            return services;
        }
    }
}
