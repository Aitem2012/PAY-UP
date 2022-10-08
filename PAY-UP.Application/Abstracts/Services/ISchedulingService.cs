using PAY_UP.Application.Dtos.Email;

namespace PAY_UP.Application.Abstracts.Services{
    public interface ISchedulingService{
        Task<bool> ScheduleEmail(ScheduleEmailDto email);
        void StopEmail(Guid debtorId);
    }
}