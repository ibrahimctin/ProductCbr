using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProductCbrAssignment.Domain.Abstractions.Identity;
using ProductCbrAssignment.Domain.Entities.Identity;
using ProductCbrAssignment.Infrastructure.DbContext;
using ProductCbrAssignment.Infrastructure.Identity.Impl;
using ProductCbrAssignment.Infrastructure.Repositories.Abstractions;
using ProductCbrAssignment.Infrastructure.Repositories.Implementations;
using ProductCbrAssignment.Infrastructure.UnitOfWorks;

namespace ProductCbrAssignment.Infrastructure.Configurators
{
    public static class InfrastructureConfigurator
    {
        public static IServiceCollection LoadInfrastructureConf(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISupportFormRepository, SupportFormRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();




            var builder = services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
            }).AddRoleManager<RoleManager<AppRole>>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

            return services;
        }
    }
}
