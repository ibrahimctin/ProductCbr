using Microsoft.AspNetCore.Identity;

namespace ProductCbrAssignment.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {

        public virtual ICollection<AppUserRole> UserRoles { get; set; } = default!;
        public virtual List<RefreshToken>? RefreshTokens { get; set; }
        public DateTime AccountCreatedDate { get; set; }
    }
}