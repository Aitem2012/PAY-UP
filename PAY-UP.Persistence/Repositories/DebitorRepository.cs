using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PAY_UP.Application.Abstracts.Persistence;
using PAY_UP.Application.Abstracts.Repositories;
using PAY_UP.Domain.Debtors;

namespace PAY_UP.Persistence.Repositories{
    public class DebitorRepository : IDebitorRepository
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<DebitorRepository> _logger;
        public DebitorRepository(IAppDbContext context, ILogger<DebitorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Debtor> CreateDebtorAsync(Debtor debitor)
        {
            _context.Debtors.Add(debitor);
            if(await _context.SaveChangesAsync(CancellationToken.None) > 0){
                return debitor;
            }
            return null;
        }

        public async Task<bool> DeleteDebtorAsync(Guid Id)
        {
            var debtor = await _context.Debtors.SingleOrDefaultAsync(x => x.Id == Id);
            if(debtor == null){
                return false;
            }
            _context.Debtors.Remove(debtor);
            if(await _context.SaveChangesAsync(CancellationToken.None) >0){
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Debtor>> GetAllDebtorsAsync()
        {
            return await _context.Debtors.ToListAsync();
        }

        public async Task<IEnumerable<Debtor>> GetDebitorsForUserAsync(string userId)
        {
            return await _context.Debtors.Where(x => x.AppUserId == userId).ToListAsync();
        }

        public async Task<Debtor> GetDebtorAsync(Guid Id)
        {
            return await _context.Debtors.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Debtor> UpdateDebtorAsync(Debtor debitor)
        {
            try
            {
                _context.Debtors.Attach(debitor);
                if(await _context.SaveChangesAsync(CancellationToken.None) > 0){
                    return debitor;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Debtor could not be updated: {ex.Message} || {ex.StackTrace}");
            }
            return null;
        }
    }
}