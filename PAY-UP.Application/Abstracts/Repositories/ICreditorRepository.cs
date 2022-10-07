using PAY_UP.Application.Dtos.Creditors;
using PAY_UP.Domain.Creditors;

namespace PAY_UP.Application.Abstracts.Repositories{
    public interface ICreditorRepository{
        Task<Creditor> CreateCreditorAsync(Creditor creditor);
        Task<Creditor> UpdateCreditorAsync(Creditor creditor);
        Task<bool> DeleteCreditorAsync(Guid Id);
        Task<Creditor> GetCreditorAsync(Guid Id);
        Task<IEnumerable<Creditor>> GetCreditorsAsync();
        Task<IEnumerable<Creditor>> GetCreditorsForUserAsync(string userId);
    }
}