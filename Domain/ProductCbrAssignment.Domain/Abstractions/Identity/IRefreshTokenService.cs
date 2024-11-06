using ProductCbrAssignment.Domain.DTOs.Identity;
using ProductCbrAssignment.Domain.Entities.Identity;

namespace ProductCbrAssignment.Domain.Abstractions.Identity
{
    public interface IRefreshTokenService
    {
        RefreshToken CreateRefreshToken(string token);
        Task<VerifiedUserResponse> RefreshToken(string token);
        void RemoveOldRefreshTokens(AppUser user);
        Task RevokeToken(string token);
    }
}
