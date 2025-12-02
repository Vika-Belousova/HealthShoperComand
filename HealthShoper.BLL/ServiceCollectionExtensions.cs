using System.Text;
using System.Text.Json;
using HealthShoper.BLL.Exceptions.Models;
using HealthShoper.BLL.Interfaces;
using HealthShoper.BLL.Services;
using HealthShoper.DAL;
using HealthShoper.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HealthShoper.BLL;

public static class ServiceCollectionExtensions
{
    public static void AddBllServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDalServiceCollection(configuration);
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IBucketService, BucketService>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(p =>
            {
                p.SaveToken = true;
                p.RequireHttpsMetadata = false;
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["ApplicationSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["ApplicationSettings:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApplicationSettings:Secret"]!)),
                    ValidateIssuerSigningKey = true
                };
                p.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception is not SecurityTokenExpiredException) return Task.CompletedTask;

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var result = JsonSerializer.Serialize(new ErrorResponseModel
                        {
                            Messages = new Dictionary<string, string>()
                            {
                                { MessageKeys.AccessToken, "token_expired" }
                            },
                            Code = StatusCodes.Status401Unauthorized
                        });
                        return context.Response.WriteAsync(result);
                    }
                };
            });

        services.AddScoped<IPasswordHasher<Client>, PasswordHasher<Client>>();
    }
}