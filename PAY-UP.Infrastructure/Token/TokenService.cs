using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PAY_UP.Application.Abstracts.Infrastructure;
using PAY_UP.Application.Dtos.Token;
using PAY_UP.Domain.AppUsers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PAY_UP.Infrastructure.Token
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(AppUser user, List<string> userRoles, IOptions<JWTData> options)
        {
            var jWTData = options.Value;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTData.SecretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now + jWTData.TokenLifeTime,
                Audience = jWTData.Audience,
                Issuer = jWTData.Issuer,
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
