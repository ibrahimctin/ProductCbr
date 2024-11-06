using ProductCbrAssignment.Domain.Entities.Identity;

namespace ProductCbrAssignment.Domain.Abstractions.Identity
{
    public interface ICurrentUserService
    {
        string UserId { get; set; }
        Task<AppUser> GetCurrentUser();
        Task<string> GetCurrentUserIdAsync();
    }
}
