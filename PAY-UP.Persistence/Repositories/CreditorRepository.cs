using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PAY_UP.Application.Abstracts.Persistence;
using PAY_UP.Application.Abstracts.Repositories;
using PAY_UP.Application.Dtos.Creditors;
using PAY_UP.Domain.Creditors;

namespace PAY_UP.Persistence.Repositories{
    public class CreditorRepository : ICreditorRepository
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<CreditorRepository> _logger;

        public CreditorRepository(IAppDbContext context, ILogger<CreditorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Creditor> CreateCreditorAsync(Creditor creditor)
        {
            _context.Creditors.Add(creditor);
            if(await _context.SaveChangesAsync(CancellationToken.None) > 0){
                return creditor;
            }
            _logger.LogInformation($"Creditor could not be saved =====>>>>> ");
            return null;
        }

        public async Task<bool> DeleteCreditorAsync(Guid Id)
        {
            var creditorToDelete = await _context.Creditors.SingleOrDefaultAsync(c => c.Id == Id);
            if(creditorToDelete == null){
                _logger.LogInformation($"Creditor with Id: {Id} is not found");
                return false;
            }
            _context.Creditors.Remove(creditorToDelete);
            if(await _context.SaveChangesAsync(CancellationToken.None) > 0){
                return true;
            }
            _logger.LogInformation($"Creditor could not be deleted");
            return false;
        }

        public async Task<Creditor> GetCreditorAsync(Guid Id)
        {
            return await _context.Creditors.SingleOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<IEnumerable<Creditor>> GetCreditorsAsync()
        {
            return await _context.Creditors.ToListAsync();
        }

        public async Task<IEnumerable<Creditor>> GetCreditorsForUserAsync(string userId)
        {
            return await _context.Creditors.Where(c => c.AppUserId == userId).ToListAsync();
        }

        public async Task<Creditor> UpdateCreditorAsync(Creditor creditor)
        {
            try{
                _context.Creditors.Attach(creditor);
                if(await _context.SaveChangesAsync(CancellationToken.None) > 0){
                    return creditor;
                }
            }catch(Exception ex){
                _logger.LogInformation($"Creditor could not be updated {ex.StackTrace}");
            }
            return null;
        }
    }
}