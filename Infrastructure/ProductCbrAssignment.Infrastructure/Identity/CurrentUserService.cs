using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProductCbrAssignment.Domain.Abstractions.Identity;
using ProductCbrAssignment.Domain.Entities.Identity;
using System.Security.Claims;

namespace ProductCbrAssignment.Infrastructure.Identity.Impl
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId { get; set; } = default!;

        public async Task<AppUser> GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException();
            }
            var emailClaim = user.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name));
            if (emailClaim is null)
            {
                throw new UnauthorizedAccessException();
            }
            var email = emailClaim.Value;
            var userManager = _httpContextAccessor.HttpContext.RequestServices
                .GetService<UserManager<AppUser>>();

            return await userManager.FindByNameAsync(email);

        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            var currentUser = await GetCurrentUser();
            return currentUser is null ? default : currentUser.Id;
        }
    }
}
