using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PAY_UP.Application.Abstracts.Infrastructure;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos.Authentication;
using PAY_UP.Application.Dtos.Token;
using PAY_UP.Application.Dtos.Users;
using PAY_UP.Common.Config;
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
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IOptions<JWTData> _options;
        private readonly IOptions<WebAppConfig> _config;
        public AuthenticationService(IUserService userService, UserManager<AppUser> userManager, IMapper mapper, IEmailService emailService, IHttpContextAccessor httpContext, ILogger<AuthenticationService> logger, SignInManager<AppUser> signInManager, ITokenService tokenService, IOptions<JWTData> options, IOptions<WebAppConfig> config)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
            _httpContext = httpContext;
            _logger = logger;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _options = options;
            _config = config;
        }

        public async Task<ResponseObject<bool>> ChangePasswordAsync(ChangePasswordDto changePasswordRequest)
        {
            var user = await _userManager.FindByIdAsync(changePasswordRequest.UserId);
            if (user.IsNull())
            {
                return new ResponseObject<bool>().CreateResponse($"User with Id: {changePasswordRequest.UserId} does not exits", false, false);
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
            var user = await FindByEmailAsync(confirmEmailReques.Email); ;
            if (user.IsNull())
            {
                return new ResponseObject<bool>().CreateResponse($"User with email: {confirmEmailReques.Email} does not exist", false, false);
            }
            var emailConfirmed = await _userManager.ConfirmEmailAsync(user, confirmEmailReques.Token);
            if (!emailConfirmed.Succeeded)
            {
                foreach (var err in emailConfirmed.Errors)
                {
                    _logger.LogError($"{err.Code} :{err.Description}");
                }
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
            var user = await FindByEmailAsync(forgotPasswordRequest.Email);
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

            var template = new NotificationHelper(_config).EmailHtmlStringTemplate($"{user.FirstName} {user.LastName}", "Account/ResetPassword", queryParams, "ResetPasswordTemplate.html", _httpContext.HttpContext);
            await _emailService.SendEmailAsync(user.Email, "Password Reset", template);
            return new ResponseObject<bool>().CreateResponse("Password reset link has been sent to your email", true, true);
        }

        public async Task<ResponseObject<LoginResponseDto>> LoginAsync(LoginDto loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user.IsNull())
            {
                return new ResponseObject<LoginResponseDto>().CreateResponse($"No user with email: {loginRequest.Email}", false, null);
            }
            if (!user.IsActive)
            {
                return new ResponseObject<LoginResponseDto>().CreateResponse($"Please verify your email: {loginRequest.Email}", false, null);
            }
            var signIn = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);
            if (!signIn.Succeeded)
            {
                return new ResponseObject<LoginResponseDto>().CreateResponse("Incorrect password. Try Again!", false, null);
            }
            var userRoles = await _userManager.GetRolesAsync(user) as List<string>;
            var token = _tokenService.GenerateToken(user, userRoles, _options);
            var userToReturn = _mapper.Map<LoginResponseDto>(user);
            userToReturn.Token = token;
            return new ResponseObject<LoginResponseDto>().CreateResponse("Login successfully", true, userToReturn);
        }

        public async Task<ResponseObject<bool>> ResetPasswordAsync(ResetPasswordDto resetPasswordRequest)
        {
            var user = await FindByEmailAsync(resetPasswordRequest.Email);
            if (user.IsNull())
            {
                return new ResponseObject<bool>().CreateResponse($"No user with email: {resetPasswordRequest.Email}", false, false);
            }
            var res = await _userManager.ResetPasswordAsync(user, resetPasswordRequest.Token, resetPasswordRequest.Password);
            if (!res.Succeeded)
            {
                foreach (var err in res.Errors)
                {
                    _logger.LogError($"{err.Code} :{err.Description}");
                }
                return new ResponseObject<bool>().CreateResponse("Password could not be reset. Try again!", false, false);
            }
            return new ResponseObject<bool>().CreateResponse("Password reset successfully", true, true);
        }

        public async Task<ResponseObject<Task>> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            _httpContext.HttpContext.Session.Clear();
            return new ResponseObject<Task>().CreateResponse("Signout successfully", true, Task.CompletedTask);
        }

        private async Task<AppUser> FindByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);
    }
}
