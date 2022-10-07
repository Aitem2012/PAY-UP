using PAY_UP.Domain.Common;

namespace PAY_UP.Domain.Creditor
{
    public class Creditor : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreditWasCollected { get; set; }
        public DateTime DateForRepayment { get; set; }
        public ReminderType ReminderType { get; set; }
    }
}
