using PAY_UP.Domain.Common;

namespace PAY_UP.Application.Dtos.Creditors{
    public class GetCreditorDto{
        public Guid Id { get; set; }       
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public decimal AmountOwed { get; set; }
        public decimal AmountPaid { get; set; }       
        public int Installment { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreditWasCollected { get; set; }
        public DateTime DateForRepayment { get; set; }
        public ReminderType ReminderType { get; set; }
    }
}