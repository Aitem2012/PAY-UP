using PAY_UP.Domain.Common;

namespace PAY_UP.Application.Dtos.Email
{
    public class EmailRequestDto
    {
        public string Subject { get; set; }
        public string RecipientEmail { get; set; }
        public string Message { get; set; }
        public Schedule ScheduleType { get; set; }
        public string AppUserId { get; set; }
    }
}
