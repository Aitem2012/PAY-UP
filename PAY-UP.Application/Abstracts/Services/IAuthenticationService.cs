using PAY_UP.Application.Dtos.Authentication;
using PAY_UP.Application.Dtos.Users;
using PAY_UP.Common.Helpers;

namespace PAY_UP.Application.Abstracts.Services
{
    public interface IAuthenticationService
    {
        public Task<ResponseObject<GetUserDto>> CreateAsync(CreateUserDto entity, string role = "user");
        public Task<ResponseObject<bool>> ChangePasswordAsync(ChangePasswordDto changePasswordRequest);
        public Task<ResponseObject<bool>> ConfirmEmailAsync(ConfirmEmailDto confirmEmailReques);
        public Task<ResponseObject<bool>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordRequest);
        public Task<ResponseObject<LoginResponseDto>> LoginAsync(LoginDto loginRequest);
        public Task<ResponseObject<bool>> ResetPasswordAsync(ResetPasswordDto resetPasswordRequest);
    }
}
