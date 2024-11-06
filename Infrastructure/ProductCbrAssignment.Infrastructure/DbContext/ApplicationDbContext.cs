using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProductCbrAssignment.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductCbrAssignment.Domain.Entities.SupportForms;
using ProductCbrAssignment.Domain.Entities.Products;

namespace ProductCbrAssignment.Infrastructure.DbContext
{
    public class ApplicationDbContext: IdentityDbContext<AppUser, AppRole, string,
        IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<SupportForm> SupportForms { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var keysProperties = builder.Model.GetEntityTypes().Select(x => x.FindPrimaryKey()).SelectMany(x => x.Properties);
            foreach (var property in keysProperties)
            {
                property.ValueGenerated = ValueGenerated.OnAdd;
            }
            builder.Entity<AppUser>()
                   .HasMany(e => e.UserRoles)
                   .WithOne(e => e.User)
                   .HasForeignKey(e => e.UserId)
                   .IsRequired();

            builder.Entity<AppRole>()
                   .HasMany(e => e.UserRoles)
                   .WithOne(e => e.Role)
                   .HasForeignKey(e => e.RoleId)
            .IsRequired();

            builder.Entity<Product>()
                   .Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            // Seeding Roles
            builder.Entity<AppRole>().HasData(
                new IdentityRole
                {
                    Id = "33333a6e-f7bb-4125-baaf-1add431ccbbf",
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN"
                },
                new IdentityRole
                {
                    Id = "44447c87-3fd7-4123-bc34-0e15db1fc803",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new IdentityRole
                {
                    Id = "5555e7b-62b0-4930-8321-3754785fc71e",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
            );

            // Seeding Users (Make sure the password hash matches the format expected by ASP.NET Identity)
            var hasher = new PasswordHasher<AppUser>();

            builder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "b9999e2d-a33b-45e6-8329-1958b32252bbd",
                    UserName = "admin@example.nl",
                    NormalizedUserName = "ADMIN@EXAMPLE.NL",
                    Email = "admin@example.nl",
                    NormalizedEmail = "ADMIN@EXAMPLE.NL",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                },
                new AppUser
                {
                    Id = "d88881ff-c746-cbrr-a83f-c333fa673dcf",
                    UserName = "manager@example.nl",
                    NormalizedUserName = "MANAGER@EXAMPLE.NL",
                    Email = "manager@example.nl",
                    NormalizedEmail = "MANAGER@EXAMPLE.NL",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                },
                new AppUser
                {
                    Id = "ab7777e2d-45fg-6ibohd-8132-1000klsr82bb",
                    UserName = "customer@example.nl",
                    NormalizedUserName = "CUSTOMER@EXAMPLE.NL",
                    Email = "customer@example.nl",
                    NormalizedEmail = "CUSTOMER@EXAMPLE.NL",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                }
            );
        }
    }
}