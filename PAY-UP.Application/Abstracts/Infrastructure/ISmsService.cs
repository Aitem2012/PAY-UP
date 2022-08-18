using PAY_UP.Application.Dtos.SmS;

namespace PAY_UP.Application.Abstracts.Infrastructure
{
    public interface ISmsService
    {
        void SendSms(SmSDto smsRequest, string senderNumber);
    }
}
