using Microsoft.AspNetCore.Identity;

namespace ProductCbrAssignment.Domain.Entities.Identity
{
    public class AppRole: IdentityRole<string>
    {
        public virtual ICollection<AppUserRole>? UserRoles { get; set; }
    }
}