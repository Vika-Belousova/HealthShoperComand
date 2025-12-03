using Microsoft.Extensions.Configuration;

namespace HealthShoper.Tests;
/// <summary>
/// Фасад для загрузки конфигурации в тестах.
///
/// Зачем нужен этот класс:
/// - Позволяет единообразно загрузить конфигурацию из appsettings.json.
/// - Автоматически подгружает appsettings.ci.json, если тесты запускаются в CI (например, GitHub Actions).
/// - Передаёт строки подключения в ConnectionStringsManager, чтобы тесты могли их использовать.
/// 
/// Итог:
/// При запуске тестов конфигурация и строки подключения загружаются один раз,
/// а затем доступны через ConfigurationFacade.Configuration.
/// </summary>
public static class ConfigurationFacade
    
{
    // Статическое свойство для доступа к конфигурации
    public static IConfiguration Configuration { get; }

    // Статический конструктор для инициализации конфигурации
    static ConfigurationFacade()
    {
        // Проверка, запущены ли тесты в CI(автоматическая система, которая проверяет код после каждого пуша? но у нас такого нет) 
        var isCi = Environment.GetEnvironmentVariable("CI") == "true";

        // начинаем создавать объект конфигурации
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");// передаем базовый файл настроек

       
        // Если мы в CI, загружаем дополнительный файл appsettings.ci.json (если есть)
        if (isCi) builder.AddJsonFile("appsettings.ci.json", optional: true);

        // Строим конфигурацию
        Configuration = builder.Build();
        // Чтение строк подключения из конфигурации
        ConnectionStringsManager.ReadFromConfiguration(Configuration);
    }
}
