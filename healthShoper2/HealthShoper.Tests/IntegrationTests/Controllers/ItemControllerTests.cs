using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using FluentAssertions;
using HealthShoper.BLL.Extensions;
using HealthShoper.BLL.Interfaces;
using HealthShoper.BLL.Models.Dtos;
using HealthShoper.BLL.Models.ViewModels;
using HealthShoper.DAL.Context;
using HealthShoper.DAL.Models.Enums;
using HealthShoper.DAL.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace HealthShoper.Tests.IntegrationTests.Controllers;

//Это класс тестов для проверки работы с товарами.
//factory — это объект, который умеет запускать тестовую копию приложения.
//IClassFixture говорит, что фабрика будет создана один раз для всех тестов.
public class ItemControllerTests(CustomWebApplicationFactory factory)
    : IClassFixture<CustomWebApplicationFactory>
{
    // HttpClient, создаваемый фабрикой. Он направляет запросы на тестовый хост
    private readonly HttpClient _client = factory.CreateClient();
    private const string Email = "test@example.com";


    // Тест для проверки получения всех товаров
    [Fact]
    public async Task GetAllItems_ShouldReturnAllItems()
    {
        //чистим базу и инициализируем данные
        await ClearDatabase();
        await InitData();

        // получаем токен для авторизации
        var token = await GetTokenDto();
        // Добавляем токен в заголовки запросов
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token.AccessToken);

        // Act
        // Отправляем запрос на получение всех товаров
        var response = await _client.GetAsync("/api/Item/GetAll");

        // Assert
        // Проверяем, что ответ успешный
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        // Читаем список товаров из ответа
        var items = await response.Content.ReadFromJsonAsync<IEnumerable<ItemDto>>(
            //Превращаем полученный текст в список товаров. игнорируем различие в больших маленьких буквах,
            //разрешаем читать текстовые значения перечислений.
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });
        // Проверяем, что список есть и не пустой, что в нём есть хотя бы один товар
        items.Should().NotBeNull();
        items.Should().HaveCountGreaterThan(0);
        // Проверяем, что у товара со скидкой вычисляется цена со скидкой.
        items.First(p => p.DiscountPercents.HasValue).PriceWithDiscount.HasValue.Should().BeTrue();
    }

    // Тест для проверки получения всех товаров с фильтром по категории
    [Fact]
    public async Task GetAllItems_WithFilter_ByCategoryType_OnlyDiscounts_ShouldReturnItems()
    {
        //чистим базу и инициализируем данные
        await ClearDatabase();
        await InitData();
        // получаем токен для авторизации
        var token = await GetTokenDto();
        // Добавляем токен в заголовки запросов
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token.AccessToken);
        // Получаем товары с фильтром по выбранным категориям.
        // В данном случае фильтр по категории BackHurts.
        var categories = new List<CategoryType>();
        categories.Add(CategoryType.BackHurts);
        // создаём объект фильтра с выбранными категориями
        var queryFilters = new QueryFilter
        {
            Category = categories
        };

        // Формируем строку запроса с выбранными категориями
        var query = HttpUtility.ParseQueryString(string.Empty);
        foreach (var categoryType in categories)
        {
            query.Add("Category", categoryType.ToString());
        }

        // Act
        // Отправляем запрос на получение всех товаров с фильтром
        var response = await _client.GetAsync("/api/Item/GetAll" + queryFilters.ToQueryString());

        // Assert
        // Проверяем, что ответ успешный
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        // Читаем список товаров из ответа
        var items = await response.Content.ReadFromJsonAsync<IEnumerable<ItemDto>>(
            //Превращаем полученный текст в список товаров. игнорируем различие в больших маленьких буквах
            //разрешаем читать текстовые значения перечислений.
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });
        // Проверяем, что список есть и не пустой, что в нём есть хотя бы один товар
        items.Should().NotBeNull();
        items.Should().HaveCountGreaterThan(0);
        // Проверяем, что у товара со скидкой вычисляется цена со скидкой.
        items.First(p => p.DiscountPercents.HasValue).PriceWithDiscount.HasValue.Should().BeTrue();
    }


    // Тест для проверки получения товара по его идентификатору
    [Fact]
    public async Task GetItemById_ShouldReturnItem()
    {
        //чистим базу и инициализируем данные
        await ClearDatabase();
        await InitData();
        // получаем токен для авторизации
        var token = await GetTokenDto();
        // Добавляем токен в заголовки запросов
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token.AccessToken);

        // Act
        // Отправляем запрос на получение товара по идентификатору 1
        var response = await _client.GetAsync($"/api/Item/GetBy/1");

        // Assert
        // Проверяем, что ответ успешный
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        // Читаем товар из ответа
        var items = await response.Content.ReadFromJsonAsync<ItemDto>(
            //Превращаем полученный текст в товар. игнорируем различие в больших маленьких буквах,
            //разрешаем читать текстовые значения перечислений.
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });
        // Проверяем, что товар есть и не пустой
        items.Should().NotBeNull();
    }

    // Тест для проверки получения всех товаров со скидками
    [Fact]
    public async Task GetAllDiscounts_ShouldReturnAllItemWithDiscounts()
    {
        //чистим базу и инициализируем данные
        await ClearDatabase();
        await InitData();
        // получаем токен для авторизации
        var token = await GetTokenDto();
        // Добавляем токен в заголовки запросов
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token.AccessToken);

        // Act
        // Отправляем запрос на получение всех товаров со скидками
        var response = await _client.GetAsync($"/api/Item/GetAllDiscounts");

        // Assert
        // Проверяем, что ответ успешный
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var items = await response.Content.ReadFromJsonAsync<IEnumerable<ItemDto>>(
            //Превращаем полученный текст в список товаров. игнорируем различие в больших маленьких буквах
            //разрешаем читать текстовые значения перечислений
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });
        // Проверяем, что список есть
        items.Should().NotBeNull();
        // Проверяем, что в списке все товары со скидками
        items.All(p => p.DiscountPercents.HasValue).Should().BeTrue();
    }


    // Вспомогательный метод для очистки базы данных
    private async Task ClearDatabase()
    {
        //создаём область видимости для получения сервисов
        using var scope = factory.Services.CreateScope();
        // получаем контекст базы данных
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // удаляем базу данных
        await dbContext.Database.EnsureDeletedAsync();
    }

    // Вспомогательный метод для регистрации клиента и получения токена
    private async Task<TokenDto> GetTokenDto()
    {
        //создаём область видимости для получения сервисов
        using var scope = factory.Services.CreateScope();
        // получаем сервис аутентификации
        var clientService = scope.ServiceProvider.GetRequiredService<IClientService>();

        // создаём модель клиента для регистрации
        var clientViewModel = new ClientViewModel
        {
            Password = "testpassword",
            Email = Email,
            FirstName = "Test",
            LastName = "Test last name",
            PhoneNumber = "+99363194681"
        };
        // регистрируем клиента и получаем токен
        return await clientService.CreateClient(clientViewModel);
    }

    // Вспомогательный метод для инициализации данных в базе
    private async Task InitData()
    {
        //создаём область видимости для получения сервисов
        using var scope = factory.Services.CreateScope();
        // получаем сервис для инициализации данных
        var dataSeed = scope.ServiceProvider.GetRequiredService<DataSeed>();
        // заполняем базу начальными данными
        await dataSeed.Record();
    }
}