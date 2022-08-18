using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PAY_UP.Application.Abstracts.Persistence;
using PAY_UP.Application.Abstracts.Repositories;
using PAY_UP.Application.Dtos.Users;
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
        public UserRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IAppDbContext context, IMapper mapper, ILogger<UserRepository> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _mapper = mapper;
            _logger = logger;
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

                return user;
            }

            return null;
        }

        public Task<bool> DeleteUserAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppUser>> GetActiveUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> UpdateAsync(UpdateUserDto entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
