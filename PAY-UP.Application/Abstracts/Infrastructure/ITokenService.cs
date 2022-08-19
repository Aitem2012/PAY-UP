using Microsoft.Extensions.Options;
using PAY_UP.Application.Dtos.Token;
using PAY_UP.Domain.AppUsers;

namespace PAY_UP.Application.Abstracts.Infrastructure
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user, List<string> userRoles, IOptions<JWTData> options);
    }
}
