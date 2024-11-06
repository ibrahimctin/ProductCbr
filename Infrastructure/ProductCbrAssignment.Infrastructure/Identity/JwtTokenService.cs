using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductCbrAssignment.Common.ExceptionModels;
using ProductCbrAssignment.Domain.Abstractions.Identity;
using ProductCbrAssignment.Domain.Entities.Identity;
using ProductCbrAssignment.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductCbrAssignment.Infrastructure.Identity.Impl
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<JwtOptions> _jwtSettings;

        public JwtTokenService(UserManager<AppUser> userManager, IOptions<JwtOptions> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }


        public async Task<JwtSecurityToken> GenerateJsonWebToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Secret));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Audience.ToString(),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Value.AccessTokenExpireTime),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
