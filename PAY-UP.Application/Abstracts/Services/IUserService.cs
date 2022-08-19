using PAY_UP.Application.Dtos.Users;
using PAY_UP.Common.Helpers;

namespace PAY_UP.Application.Abstracts.Services
{
    public interface IUserService
    {
        public Task<ResponseObject<GetUserDto>> CreateAsync(CreateUserDto entity, string role = "user");
        public Task<ResponseObject<GetUserDto>> UpdateAsync(UpdateUserDto entity);
        public Task<ResponseObject<IEnumerable<GetUserDto>>> GetAllAsync(bool isActive = false);
        public Task<ResponseObject<GetUserDto>> GetByIdAsync(string id);
        public Task<ResponseObject<GetUserDto>> GetByEmailAsync(string email);
        public Task<ResponseObject<bool>> DeleteUserAsync(string id);
    }
}
