using Microsoft.AspNetCore.Identity;
using ProductCbrAssignment.Domain.Entities.Identity;

namespace ProductCbrAssignment.Domain.Abstractions.Users
{
    public interface IUserService
    {
      
        Task<IdentityResult> CreateManagerAsync(string email);

        Task<IdentityResult> CreateCustomerAsync(string email, string password);

        Task<IdentityResult> UpdateUserAsync(string userId, string newEmail);

        Task<IdentityResult> DeleteUserAsync(string userId);

        Task<List<AppUser>> GetManagersAsync();

        Task<List<AppUser>> GetCustomersAsync();

    }
}
