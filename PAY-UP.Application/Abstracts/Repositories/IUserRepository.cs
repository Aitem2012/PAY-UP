using PAY_UP.Application.Dtos.Users;
using PAY_UP.Domain.AppUsers;

namespace PAY_UP.Application.Abstracts.Repositories
{
    public interface IUserRepository
    {
        public Task<AppUser> CreateAsync(CreateUserDto entity, CancellationToken cancellationToken, string role = "user");
        public Task<AppUser> UpdateAsync(UpdateUserDto entity, CancellationToken cancellationToken);
        public Task<IEnumerable<AppUser>> GetAllAsync();
        public Task<AppUser> GetByIdAsync(string id);
        public Task<AppUser> GetByEmailAsync(string email);
        public Task<bool> DeleteUserAsync(string id, CancellationToken cancellationToken);
        public Task<IEnumerable<AppUser>> GetActiveUsersAsync();
    }
}
