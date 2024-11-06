using ProductCbrAssignment.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ProductCbrAssignment.Domain.Abstractions.Identity
{
    public interface IJwtTokenService
    {
        Task<JwtSecurityToken> GenerateJsonWebToken(AppUser user);
    }
}
