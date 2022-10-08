using PAY_UP.Domain.Debtors;

namespace PAY_UP.Application.Abstracts.Repositories{
    public interface IDebitorRepository{
        Task<Debtor> CreateDebtorAsync(Debtor debitor);
        Task<Debtor> UpdateDebtorAsync(Debtor debitor);
        Task<bool> DeleteDebtorAsync(Guid Id);
        Task<Debtor> GetDebtorAsync(Guid Id);
        Task<IEnumerable<Debtor>> GetAllDebtorsAsync();
        Task<IEnumerable<Debtor>> GetDebitorsForUserAsync(string userId);
    }
}