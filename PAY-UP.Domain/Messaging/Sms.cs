using PAY_UP.Domain.AppUsers;
using PAY_UP.Domain.Common;

namespace PAY_UP.Domain.Messaging
{
    public class Sms : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public Schedule ScheduleType { get; set; }
        public bool IsSmsActive { get; set; }
        public string AppUserId { get; set; }
        public AppUser User { get; set; }
    }
}
