using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using nexus_bilding_api.infrastructure.identity.Context;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using nexus_bilding_api.core.application.Interfaces;
using nexus_bilding_api.core.application.Wrappers;
using nexus_bilding_api.core.domain.Settings;
using nexus_bilding_api.infrastructure.identity.Entities;
using nexus_bilding_api.infrastructure.identity.Services;

namespace nexus_bilding_api.infrastructure.identity;

public static class ServiceExtensionIoC
{
    public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("IdentityConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("IdentityConnection")),
                b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));

        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        });

        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JWTSettings:Issuer"],
                ValidAudience = configuration["JWTSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:SecretKey"]!))
            };

            o.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.StatusCode = 500;
                    c.Response.ContentType = "text/plain";
                    return c.Response.WriteAsync(c.Exception.ToString());
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    var result = Newtonsoft.Json.JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                    return context.Response.WriteAsync(result);
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";
                    var result = Newtonsoft.Json.JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                    return context.Response.WriteAsync(result);
                },
            };
        });

        services.AddScoped<IAccountService, AccountService>();
    }
}
