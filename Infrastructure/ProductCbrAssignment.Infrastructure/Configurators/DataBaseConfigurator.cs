using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProductCbrAssignment.Domain.Abstractions.Identity;
using ProductCbrAssignment.Infrastructure.DbContext;
using ProductCbrAssignment.Infrastructure.Identity.Impl;

namespace ProductCbrAssignment.Infrastructure.Configurators
{
    public static class DataBaseConfigurator
    {
        public static IServiceCollection LoadSqlConf(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("ProductCbrConn")
                   ));
          
            return services;
            
        }
    }
}
