using Microsoft.AspNetCore.Identity;
using ProductCbrAssignment.Domain.DTOs.Identity;

namespace ProductCbrAssignment.Domain.Abstractions.Identity
{
    public interface IUserAuthenticationService
    {
        Task<VerifiedUserResponse> LoginUserAsync(LoginRequest loginRequest);

        Task<IdentityResult> RegisterAsync(RegisterRequest request);
    }
}
