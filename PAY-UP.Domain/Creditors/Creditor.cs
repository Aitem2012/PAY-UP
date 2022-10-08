using PAY_UP.Domain.AppUsers;
using PAY_UP.Domain.Common;

namespace PAY_UP.Domain.Creditors
{
    public class Creditor : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal AmountOwed { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }       
        public int Installment { get; set; }
        public DateTime DateCreditWasCollected { get; set; }
        public DateTime DateForRepayment { get; set; }
        public ReminderType ReminderType { get; set; }
        public virtual AppUser User { get; set; }
        public string AppUserId { get; set; }
    }
}
