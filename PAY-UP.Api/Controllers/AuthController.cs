using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos.Authentication;
using PAY_UP.Application.Dtos.Users;

namespace PAY_UP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("signup", Name = nameof(Signup)), ProducesResponseType(typeof(GetUserDto), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> Signup([FromBody] CreateUserDto model)
        {
            return Ok(await _authService.CreateAsync(model));
        }

        /// <summary>
        /// Register a new admin user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("signup-admin", Name = nameof(SignupAdmin)), ProducesResponseType(typeof(GetUserDto), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> SignupAdmin([FromForm] CreateUserDto model)
        {
            return Ok(await _authService.CreateAsync(model, "admin"));
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login", Name = nameof(Login)), ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            return Ok(await _authService.LoginAsync(model));
        }

        /// <summary>
        /// Forgot password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("forgot-password", Name = nameof(ForgotPassword)), ProducesResponseType(typeof(string), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto request)
        {
            return Ok(await _authService.ForgotPasswordAsync(request));
        }

        /// <summary>
        /// Forgot password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("reset-password", Name = nameof(ResetPassword)), ProducesResponseType(typeof(bool), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            return Ok(await _authService.ResetPasswordAsync(model));
        }

        [HttpPost("confirm-email", Name = nameof(ConfirmEmail)), ProducesResponseType(typeof(bool), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto model)
        {
            return Ok(await _authService.ConfirmEmailAsync(model));
        }

        /// <summary>
        /// SignOut
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("signout", Name = nameof(Logout)), ProducesResponseType(typeof(bool), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> Logout()
        {
            //TODO: Properly implement signout
            return Ok(await _authService.SignOutAsync());
        }
    }
}
