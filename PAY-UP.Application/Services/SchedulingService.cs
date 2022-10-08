using Hangfire;
using PAY_UP.Application.Abstracts.Infrastructure;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos.Debtors;
using PAY_UP.Application.Dtos.Email;
using PAY_UP.Domain.Common;

namespace PAY_UP.Application.Services{
    public class SchedulingService : ISchedulingService
    {
        private readonly IDebitorService _debitorService;
        private readonly IEmailService _emailService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IUserService _userService;
        public SchedulingService(IDebitorService debitorService, IEmailService emailService, IBackgroundJobClient backgroundJobClient, IUserService userService)
        {
            _debitorService = debitorService;
            _emailService = emailService;
            _backgroundJobClient = backgroundJobClient;
            _userService = userService;
        }

        public async Task<bool> ScheduleEmail(ScheduleEmailDto email)
        {
            var debtor = await _debitorService.GetDebtorAsync(email.DebtorId);
            var user = await _userService.GetByIdAsync(email.AppUserId);
            if(debtor.Data.Balance == 0){
                return false;
            }
            if(!user.IsSuccessfull){
                return false;
            }
            if (!debtor.IsSuccessfull)
            {
                return false;
            }
            var content =  EmailContent(debtor.Data, email.Message);
            switch(email.ReminderType){
                case ReminderType.Hourly:
                    RecurringJob.RemoveIfExists(email.DebtorId.ToString());
                    RecurringJob.AddOrUpdate<IEmailService>(email.DebtorId.ToString(), mail => mail.SendEmailAsync(debtor.Data.Email, "Debt Repayment", content, ""), Cron.Hourly);
                    break;
                
                case ReminderType.Daily:
                    RecurringJob.RemoveIfExists(email.DebtorId.ToString());
                    RecurringJob.AddOrUpdate<IEmailService>(email.DebtorId.ToString(), mail => mail.SendEmailAsync(debtor.Data.Email, "Debt Repayment", content, ""), Cron.Daily);
                    break;

                case ReminderType.BiMonthly:
                    RecurringJob.RemoveIfExists(email.DebtorId.ToString());
                    RecurringJob.AddOrUpdate<IEmailService>(email.DebtorId.ToString(), mail => mail.SendEmailAsync(debtor.Data.Email, "Debt Repayment", content, ""), Cron.DayInterval(15));
                    break;
                
                case ReminderType.Monthly:
                    RecurringJob.RemoveIfExists(email.DebtorId.ToString());
                    RecurringJob.AddOrUpdate<IEmailService>(email.DebtorId.ToString(), mail => mail.SendEmailAsync(debtor.Data.Email, "Debt Repayment", content, ""), Cron.Monthly);
                    break;
                
                case ReminderType.Weekly:
                    RecurringJob.RemoveIfExists(email.DebtorId.ToString());
                    RecurringJob.AddOrUpdate<IEmailService>(email.DebtorId.ToString(), mail => mail.SendEmailAsync(debtor.Data.Email, "Debt Repayment", content, ""), Cron.Weekly);
                    break;
                default: break;
            }
            return true;
        }

        public void StopEmail(Guid debtorId)
        {
            RecurringJob.RemoveIfExists(debtorId.ToString());
        }

        private string EmailContent(GetDebtorDto debtor, string message){
            return $"{message}" +
                    $"Amount owed: {debtor.AmountOwed} " +
                    $"Amount paid: {debtor.AmountPaid} " +
                    $"Amount to balance: {debtor.Balance} ";
        }
    }
}