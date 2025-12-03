using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace HealthShoper.Tests;

/// <summary>
/// Кастомная фабрика веб-приложения для интеграционных тестов.
/// 
/// Зачем создаём:
/// - Чтобы запускать наше ASP.NET Core приложение в памяти без реального веб-сервера.
/// - Чтобы можно было подменять сервисы, например, использовать тестовую базу данных или заглушки.
/// - Позволяет создавать HttpClient для тестирования API.
/// 
/// Что такое WebApplicationFactory:
/// - Это встроенный инструмент для интеграционных тестов ASP.NET Core.
/// - Создаёт экземпляр приложения так, как если бы оно запускалось на сервере, но в памяти.
/// - Можно переопределять настройки, зависимости и конфигурацию.
/// </summary>

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
        });
        // Создаём хост приложения с обычными настройками
        return base.CreateHost(builder);
    }
}