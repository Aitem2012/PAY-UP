using PAY_UP.Domain.AppUsers;

namespace PAY_UP.Application.Abstracts.Repositories
{
    public interface IUserRepository : IBaseRepository<AppUser>
    {
        public Task<AppUser> GetByIdAsync(string id);
        public Task<AppUser> GetByEmailAsync(string email);
        public Task<bool> DeleteUserAsync(string id, CancellationToken cancellationToken);
        public Task<IEnumerable<AppUser>> GetActiveUsersAsync();
    }
}
