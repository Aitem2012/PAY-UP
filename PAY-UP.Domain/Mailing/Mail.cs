using PAY_UP.Domain.AppUsers;
using PAY_UP.Domain.Common;

namespace PAY_UP.Domain.Mailing
{
    public class Mail : BaseEntity
    {
        public string RecipientEmail { get; set; }
        public string Message { get; set; }
        public Schedule ScheduleType { get; set; }
        public string AppUserId { get; set; }
        public AppUser User { get; set; }
    }
}
