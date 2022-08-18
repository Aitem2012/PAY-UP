using PAY_UP.Domain.Common;

namespace PAY_UP.Application.Dtos
{
    public class SmSDto
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public Schedule ScheduleType { get; set; }
        public bool IsSmsActive { get; set; }
        public string AppUserId { get; set; }
    }
}
