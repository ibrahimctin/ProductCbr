using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
namespace ProductCbrAssignment.Infrastructure.Configurators
{
    public static class IdentityConfigurator
    {
        public static IServiceCollection LoadIdentityConf(this IServiceCollection services)
        {

     
            services.Configure<IdentityOptions>(options =>
            {
                // password options
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                // user settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            return services;
        }
    }
}

