using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProductCbrAssignment.Domain.Abstractions.Identity;
using ProductCbrAssignment.Domain.Abstractions.Users;
using ProductCbrAssignment.Infrastructure.Identity.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCbrAssignment.Infrastructure.Configurators
{
    public static class IdentityServiceLayerConfigurator
    {
        public static IServiceCollection LoadIdentityServices(this IServiceCollection services)
        {
          
          
           
            return services;
        }

    }
}
