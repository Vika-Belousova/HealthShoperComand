using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HealthShoper.BLL.Interfaces;
using HealthShoper.BLL.Models.Dtos;
using HealthShoper.BLL.Models.ViewModels;
using HealthShoper.DAL.Context;
using HealthShoper.DAL.Seeds;
using Microsoft.Extensions.DependencyInjection;

namespace HealthShoper.Tests.IntegrationTests.Controllers;

// Это тесты для  аутентификации
//создаём класс тестов для проверки работы контроллера авторизации.
//factory — это объект, который умеет запускать тестовую копию приложения.
//IClassFixture говорит, что фабрика будет создана один раз для всех тестов.

public class AuthControllerTests(CustomWebApplicationFactory factory)
    : IClassFixture<CustomWebApplicationFactory>
{

    // HttpClient, создаваемый фабрикой. Он направляет запросы на тестовый хост.
    private readonly HttpClient _client = factory.CreateClient();
    private const string EMAIL = "test@example.com";

    // Тест для проверки регистрации нового пользователя
    [Fact]
    public async Task Registration_ShouldReturnOk()
    {
        // Очистка базы данных и инициализация данных
        await ClearDatabase();
        await InitData();

        // Arrange
        // Создаём модель клиента с тестовыми данными
        var clientViewModel = new ClientViewModel
        {
            Password = "testpassword",
            Email = EMAIL,
            FirstName = "Test"
        };

        // Act
        // Отправляем запрос на регистрацию
        var response = await _client.PostAsJsonAsync("/api/Auth/Registration", clientViewModel);

        // Assert
        // Проверяем, что ответ успешный
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var tokenDto = await response.Content.ReadFromJsonAsync<TokenDto>();
        // Проверяем, что токен пришел и не пустой
        tokenDto.Should().NotBeNull();
        tokenDto.AccessToken.Should().NotBeNullOrEmpty();
        //чистим базу
        await ClearDatabase();
    }

    // Тест для проверки входа пользователя
    [Fact]
    public async Task LogIn_ShouldReturnOk()
    {
        // Очистка базы данных и инициализация данных
        await ClearDatabase();
        await InitData();

        // Регистрируем пользователя, чтобы потом войти
        await GetTokenDto();

        // Arrange
        // Создаём модель для входа с тестовыми данными
        var logInViewModel = new LogInViewModel
        {
            Email = EMAIL,
            Password = "testpassword",
        };

        // Act
        // Отправляем запрос на вход
        var response = await _client.PostAsJsonAsync("/api/Auth/LogIn", logInViewModel);

        // Assert
        // Проверяем, что ответ успешный
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Проверяем, что токены доступа и обновления пришли и не пустые
        var tokenDto = await response.Content.ReadFromJsonAsync<TokenDto>(); 
        tokenDto.Should().NotBeNull();
        tokenDto.Should().NotBeNull();
        tokenDto.AccessToken.Should().NotBeNullOrEmpty();
        tokenDto.RefreshToken.Should().NotBeNullOrEmpty();
        //чистим базу
        await ClearDatabase();
    }

    // Тест для проверки входа с неверными данными
    [Fact]
    public async Task LogIn_ShouldReturnUnauthorized()
    {
        // Очистка базы данных и инициализация данных
        await ClearDatabase();
        await InitData();

        // Arrange
        // Создаём модель для входа
        var logInViewModel = new LogInViewModel
        {
            Email = EMAIL,
            Password = "testpassword",
        };

        // Act
        // Отправляем запрос на вход без предварительной регистрации
        var response = await _client.PostAsJsonAsync("/api/Auth/LogIn", logInViewModel);

        // Assert
        // Проверяем, что ответ - Unauthorized
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        //чистим базу
        await ClearDatabase();
    }

    // Вспомогательный метод для очистки базы данных
    private async Task ClearDatabase()
    {
        // Создаём область видимости для получения сервисов
        using var scope = factory.Services.CreateScope();
        // Получаем контекст базы данных
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Удаляем базу данных
        await dbContext.Database.EnsureDeletedAsync();
    }

    // Вспомогательный метод для регистрации пользователя и получения токена
    private async Task<TokenDto> GetTokenDto()
    {
        // Создаём область видимости для получения сервисов
        using var scope = factory.Services.CreateScope();
        // Получаем сервис для работы с клиентами
        var clientService = scope.ServiceProvider.GetRequiredService<IClientService>();
        // Создаём модель клиента с тестовыми данными
        var clientViewModel = new ClientViewModel
        {
            Password = "testpassword",
            Email = EMAIL,
            FirstName = "Test",
            LastName = "Test last name",
            PhoneNumber = "+99363194681"
        };
        // Регистрируем клиента и за одно получаем токен 
        return await clientService.CreateClient(clientViewModel);
    }

    // Вспомогательный метод для инициализации данных в базе
    private async Task InitData()
    {
        // Создаём область видимости для получения сервисов
        using var scope = factory.Services.CreateScope();
        // Получаем сервис для заполнения начальными данными
        var dataSeed = scope.ServiceProvider.GetRequiredService<DataSeed>();
        // Заполняем данные
        await dataSeed.Record();
    }
}