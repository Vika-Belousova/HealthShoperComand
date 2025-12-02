using HealthShoper.DAL.Context;
using HealthShoper.DAL.Interfaces;
using HealthShoper.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthShoper.DAL;

public static class ServiceCollectionExtensions
{
    public static void AddDalServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(p =>
        {
            p.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IApplicationDbContext>(p => p.GetService<AppDbContext>()!);
        services.AddScoped<DataSeed>();
    }
}