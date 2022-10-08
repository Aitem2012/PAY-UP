using PAY_UP.Domain.Common;

namespace PAY_UP.Application.Dtos.Email{
    public class ScheduleEmailDto{
        public Guid DebtorId { get; set; }
        public string Message { get; set; }
        public ReminderType ReminderType { get; set; }      
        public string AppUserId { get; set; }
        
        
    }
}