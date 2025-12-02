using HealthShoper.DAL.Context;
using HealthShoper.DAL.Extensions;
using HealthShoper.DAL.Models;
using HealthShoper.DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HealthShoper.DAL.Seeds;

public class DataSeed(AppDbContext dbContext, ILogger<DataSeed> logger)
{
    public async Task Record()
    {
        try
        {
            logger.LogInformation("Started operation for migrations data for DB");
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Finished migrations data for DB");

            var items = new List<Item>()
            {
                new()
                {
                    Name = "Подспинная Подушка",
                    Count = 10,
                    Price = 10900,
                    DiscountPercents = 18,
                    Category = [CategoryType.All, CategoryType.DiscountedProducts, CategoryType.BackHurts],
                    Description = "Черная подспинная подушка",
                },
                new()
                {
                    Name = "Шейный полувалик.",
                    Count = 10,
                    Price = 7100,
                    DiscountPercents = 30,
                    Category = [CategoryType.All, CategoryType.DiscountedProducts, CategoryType.NeckHurts],
                    Description = "Шейный полувалик Detensor (рост от 150 см.)",
                },
                new()
                {
                    Name = "Лечебный мат.",
                    Count = 18,
                    Price = 59800,
                    DiscountPercents = 7,
                    Category = [CategoryType.All, CategoryType.DiscountedProducts, CategoryType.NeckHurts],
                    Description = "Мат Detensor 18% (от 65 до 120 кг) жесткость 2+",
                },
                new()
                {
                    Name = "Лечебный матрас",
                    Count = 2,
                    Price = 45800,
                    DiscountPercents = 44,
                    Category = [CategoryType.All, CategoryType.DiscountedProducts, CategoryType.NeckHurts],
                    Description = "Матрас-топпер FIBROTOP Classic (80x200x9 см)",
                },
                new()
                {
                    Name = "Тестовый товар без скидки",
                    Count = 4,
                    Price = 87000,
                    Category = [CategoryType.All, CategoryType.DiscountedProducts, CategoryType.NeckHurts],
                    Description = "Матрас-топпер FIBROTOP Classic (80x200x9 см)",
                },
            };
            if (dbContext.Set<Item>().AsNoTracking().All(p => items.Contains(p)))
            {
                await dbContext.Set<Item>().AddRangeAsync(items);
                await dbContext.SaveChangesAsync();
            }

            var defaultUser = new Client()
            {
                PasswordHash = "test".HashSecretKey(),
                Email = "test@gmail.com",
                FirstName = "Test"
            };
            if (!dbContext.Set<Client>().Any(p => p.Email == defaultUser.Email))
            {
                await dbContext.Set<Client>().AddAsync(defaultUser);
                await dbContext.SaveChangesAsync();
            }

            logger.LogInformation("Finished migrations data for DB");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}