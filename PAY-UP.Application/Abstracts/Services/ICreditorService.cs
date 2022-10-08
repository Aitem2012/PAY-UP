using PAY_UP.Application.Dtos;
using PAY_UP.Application.Dtos.Common;
using PAY_UP.Application.Dtos.Creditors;
using PAY_UP.Common.Helpers;

namespace PAY_UP.Application.Abstracts.Services{
    public interface ICreditorService{
        Task<ResponseObject<GetCreditorDto>> CreateCreditorAsync(CreateCreditorDto creditor);
        Task<ResponseObject<GetCreditorDto>> UpdateCreditorAsync(UpdateCreditorDto creditor);
        Task<ResponseObject<bool>> DeleteCreditorAsync(Guid id);
        Task<ResponseObject<IEnumerable<GetCreditorDto>>> GetAllCreditorsAsync();
        Task<ResponseObject<IEnumerable<GetCreditorDto>>> GetCreditorsForUserAsync(string userId);
        Task<ResponseObject<GetCreditorDto>> GetCreditorAsync(Guid id);
        Task<ResponseObject<GetCreditorDto>> MakePayment(PaymentDto payment);
    }
}