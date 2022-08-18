using Microsoft.Extensions.Logging;
using PAY_UP.Application.Abstracts.Infrastructure;
using PAY_UP.Application.Dtos.SmS;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PAY_UP.Infrastructure.Sms
{
    public class SmsService : ISmsService
    {
        private readonly ILogger<SmsService> _logger;

        public SmsService(ILogger<SmsService> logger)
        {
            _logger = logger;
        }

        public void SendSms(SmSDto smsRequest, string senderNumber)
        {
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: smsRequest.Message,
                from: new Twilio.Types.PhoneNumber(senderNumber),
                to: new Twilio.Types.PhoneNumber(smsRequest.PhoneNumber)
            );

            _logger.LogInformation($"The message sid: {message.Sid}");
        }
    }
}
