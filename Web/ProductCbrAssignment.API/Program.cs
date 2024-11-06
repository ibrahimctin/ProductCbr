using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProductCbrAssignment.Application.Products;
using ProductCbrAssignment.Application.SupportForms;
using ProductCbrAssignment.Application.Users;
using ProductCbrAssignment.Common.AutoMapper;
using ProductCbrAssignment.Domain.Abstractions.Identity;
using ProductCbrAssignment.Domain.Abstractions.Products;
using ProductCbrAssignment.Domain.Abstractions.SupportForms;
using ProductCbrAssignment.Domain.Abstractions.Users;
using ProductCbrAssignment.Domain.DTOs.Identity;
using ProductCbrAssignment.Domain.Entities.Identity;
using ProductCbrAssignment.Infrastructure.Configurators;
using ProductCbrAssignment.Infrastructure.Identity;
using ProductCbrAssignment.Infrastructure.Identity.Impl;

namespace ProductCbrAssignment.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.TryAddScoped<IUserAuthenticationService, UserAuthenticationService>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.LoadSqlConf(builder.Configuration);
            builder.Services.LoadJwtConf(builder.Configuration);


            builder.Services.LoadInfrastructureConf();
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddMvc();
                  


            // Register application services
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<ISupportFormService, SupportFormService>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<IUserService, UserService>();
      


            builder.Services.AddHttpContextAccessor();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

           


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();


            app.MapControllers();

            app.Run();
        }
    }
}
