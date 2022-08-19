using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PAY_UP.Application.Abstracts.Infrastructure;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos.Authentication;
using PAY_UP.Application.Dtos.Users;
using PAY_UP.Common.Extensions;
using PAY_UP.Common.Helpers;
using PAY_UP.Domain.AppUsers;

namespace PAY_UP.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly HttpContext _httpContext;
        public AuthenticationService(IUserService userService, UserManager<AppUser> userManager, IMapper mapper, IEmailService emailService, HttpContext httpContext)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
            _httpContext = httpContext;
        }

        public async Task<ResponseObject<bool>> ChangePasswordAsync(ChangePasswordDto changePasswordRequest, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user.IsNull())
            {
                return new ResponseObject<bool>().CreateResponse($"User with Id: {userId} does not exits", false, false);
            }
            var passwordExist = await _userManager.CheckPasswordAsync(user, changePasswordRequest.OldPassword);
            if (!passwordExist)
            {
                return new ResponseObject<bool>().CreateResponse($"Old password is incorrect", false, passwordExist);
            }
            var res = await _userManager.ChangePasswordAsync(user, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);
            if (!res.Succeeded)
            {
                return new ResponseObject<bool>().CreateResponse("Password could not be changed", false, false);
            }
            return new ResponseObject<bool>().CreateResponse("Password Changed Successfully", true, true);
        }

        public async Task<ResponseObject<bool>> ConfirmEmailAsync(ConfirmEmailDto confirmEmailReques)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailReques.Email);
            if (user.IsNull())
            {
                return new ResponseObject<bool>().CreateResponse($"User with email: {confirmEmailReques.Email} does not exist", false, false);
            }
            var emailConfirmed = await _userManager.ConfirmEmailAsync(user, confirmEmailReques.Token);
            if (!emailConfirmed.Succeeded)
            {
                return new ResponseObject<bool>().CreateResponse("Email could not be confirmed", false, false);
            }
            user.IsActive = true;
            var res = await _userManager.UpdateAsync(user);
            if (!res.Succeeded)
            {
                return new ResponseObject<bool>().CreateResponse("User could not be activated", false, false);
            }
            return new ResponseObject<bool>().CreateResponse("Email Confirmed successfully", true, true);
        }

        public async Task<ResponseObject<GetUserDto>> CreateAsync(CreateUserDto entity, string role = "user")
        {
            return await _userService.CreateAsync(entity, role);

        }

        public async Task<ResponseObject<bool>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordRequest)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordRequest.Email);
            if (user.IsNull())
            {
                return new ResponseObject<bool>().CreateResponse($"No user with email: {forgotPasswordRequest.Email}", false, false);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var queryParams = new Dictionary<string, string>()
            {
                ["email"] = user.Email,
                ["token"] = token
            };

            var template = NotificationHelper.EmailHtmlStringTemplate($"{user.FirstName} {user.LastName}", "auth/reset-password", queryParams, "ResetPasswordTemplated.html", _httpContext);
            //TODO: Send forgot password email

            return new ResponseObject<bool>().CreateResponse("Password reset link has been sent to your email", true, true);
        }

        public Task<ResponseObject<LoginResponseDto>> LoginAsync(LoginDto loginRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObject<bool>> ResetPasswordAsync(ResetPasswordDto resetPasswordRequest)
        {
            throw new NotImplementedException();
        }
    }
}
