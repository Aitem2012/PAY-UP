using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PAY_UP.Common.Helpers;
using PAY_UP.Web.Models.Accounts;
using PAY_UP.Web.Models.Common;

namespace PAY_UP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IOptions<ApiConfig> _options;
        private readonly string _baseUrl = string.Empty;
        public AccountController(ILogger<AccountController> logger, IOptions<ApiConfig> options)
        {
            _logger = logger;
            _options = options;
            _baseUrl = _options.Value.BaseUrl;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.LoginErrMsg = false;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginErrMsg = false;

            var (responseBody, response) = await HttpHelper.PostContentAsync(_baseUrl, model, $"/api/Auth/login");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.LoginErrMsg = true;
                var errorResponse = JsonConvert.DeserializeObject<ResponseObject<string>>(responseBody);
                ModelState.AddModelError(errorResponse.Message, errorResponse.Message);
                return View(model);
            }

            var successfulResponse = JsonConvert.DeserializeObject<ResponseObject<LoginResponseViewModel>>(responseBody);
            var fullname = successfulResponse.Data.Fullname;
            var token = successfulResponse.Data.Token;

            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetString("Username", fullname);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UserRegistration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserRegistration(UserRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var (responseBody, response) = await HttpHelper.PostContentAsync<UserRegistrationViewModel>(_baseUrl, model, "/api/Auth/signup");
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ResponseObject<string>>(responseBody);
                ModelState.AddModelError(errorResponse.Message, errorResponse.Message);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var (responseBody, response) = await HttpHelper.PostContentAsync<ForgotPasswordViewModel>(_baseUrl, model, "/api/Auth/forgot-password");
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ResponseObject<bool>>(responseBody);
                ModelState.AddModelError(errorResponse.Message, errorResponse?.Message);
                return View(model);
            }
            return RedirectToAction("ForgotPasswordConfirmation", "Account");
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public IActionResult ResetPassword(string email, string token)
        {
            ViewBag.Email = email;
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var (responseBody, response) = await HttpHelper.PostContentAsync<ResetPasswordViewModel>(_baseUrl, model, "/api/Auth/reset-password");
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<ResponseObject<bool>>(responseBody);
                ModelState.AddModelError(errorResponse.Message, errorResponse.Message);
                return View();
            }
            return RedirectToAction("ResetPasswordConfirmation", "Account");
        }
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var (responseBody, response) = await HttpHelper.PostContentAsync(_baseUrl, new { }, "api/Auth/signout");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
