using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PAY_UP.Application.Abstracts.Infrastructure;
using PAY_UP.Application.Abstracts.Persistence;
using PAY_UP.Application.Abstracts.Repositories;
using PAY_UP.Application.Dtos.Users;
using PAY_UP.Common.Extensions;
using PAY_UP.Common.Helpers;
using PAY_UP.Domain.AppUsers;
using System.Security.Claims;

namespace PAY_UP.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContext;
        public UserRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IAppDbContext context, IMapper mapper, ILogger<UserRepository> logger, IEmailService emailService, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
            _httpContext = httpContext;
        }

        public async Task<AppUser> CreateAsync(CreateUserDto entity, CancellationToken cancellationToken, string role = "user")
        {
            if (!await _userManager.Users.AnyAsync(x => x.Email.Equals(entity.Email)))
            {
                var userRole = new IdentityResult();
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    userRole = await _roleManager.CreateAsync(new IdentityRole(role));
                }
                AppUser user = _mapper.Map<AppUser>(entity);
                user.IsActive = false;
                user.EmailConfirmed = false;
                user.UserName = entity.Email;

                var result = _userManager.CreateAsync(user, entity.Password).Result;
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError($"{error.Code}: {error.Description}");
                    }
                    return null;
                }
                await _userManager.AddToRoleAsync(user, role);
                await _userManager.AddClaimsAsync(user, new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Email, user.Email),
                });

                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var queryParams = new Dictionary<string, string>()
                {
                    ["email"] = user.Email,
                    ["token"] = confirmationToken
                };

                //TODO: Send confirmation email
                var template = NotificationHelper.EmailHtmlStringTemplate($"{user.FirstName} {user.LastName}", "api/auth/confirm-email ", queryParams, "ConfirmationEmail.html", _httpContext.HttpContext);
                var res = await _emailService.SendEmailAsync(user.Email, "Email Confirmation", template);

                return user;
            }

            return null;
        }

        public async Task<bool> DeleteUserAsync(string id, CancellationToken cancellationToken)
        {
            var userToDisable = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (userToDisable.IsNull())
            {
                return false;
            }
            userToDisable.IsActive = false;
            _context.Users.Attach(userToDisable);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync(bool isActive = false)
        {
            var users = new List<AppUser>();
            if (isActive)
            {
                users = await _context.Users.Where(x => x.IsActive).ToListAsync();
                return users;
            }
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == email);
        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<AppUser> UpdateAsync(UpdateUserDto entity, CancellationToken cancellationToken)
        {
            var userInDb = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(entity.AppUserId));
            if (userInDb.IsNull())
            {
                return null;
            }
            var user = _mapper.Map(entity, userInDb);
            _context.Users.Attach(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
