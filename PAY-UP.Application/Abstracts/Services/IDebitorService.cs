using PAY_UP.Application.Dtos.Common;
using PAY_UP.Application.Dtos.Debtors;
using PAY_UP.Common.Helpers;

namespace PAY_UP.Application.Abstracts.Services{
    public interface IDebitorService{
        Task<ResponseObject<GetDebtorDto>> CreateDebtorAsync(CreateDebtorDto debtor);
        Task<ResponseObject<GetDebtorDto>> UpdateDebtorAsync(UpdateDebtorDto debtor);
        Task<ResponseObject<bool>> DeleteDebtorAsync(Guid id);
        Task<ResponseObject<IEnumerable<GetDebtorDto>>> GetAllDebtorsAsync();
        Task<ResponseObject<IEnumerable<GetDebtorDto>>> GetDebtorsForUserAsync(string userId);
        Task<ResponseObject<GetDebtorDto>> GetDebtorAsync(Guid id);
        Task<ResponseObject<GetDebtorDto>> MakePayment(PaymentDto payment);
    }
}