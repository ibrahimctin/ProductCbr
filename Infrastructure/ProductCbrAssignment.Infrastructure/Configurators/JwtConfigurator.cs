using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProductCbrAssignment.Infrastructure.Options;
using System.Text;

namespace ProductCbrAssignment.Infrastructure.Configurators
{
    public static class JwtConfigurator
    {
        public static IServiceCollection LoadJwtConf(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(JwtOptions.JWT)
            .Get<JwtOptions>();

            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.JWT));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.RequireHttpsMetadata = false;
                  options.SaveToken = true;

                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateAudience = true,
                      ValidateIssuer = true,
                      RequireExpirationTime = true,
                      ValidIssuer = jwtOptions.Issuer,
                      ValidAudience = jwtOptions.Audience,
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                      ClockSkew = TimeSpan.Zero
                  };

                  options.Events = new JwtBearerEvents
                  {
                      OnMessageReceived = context =>
                      {
                          context.Token = context.Request.Cookies["X-Access-Token"];
                          return Task.CompletedTask;
                      },
                  };
              });
                 return services;

        }
    }
}

