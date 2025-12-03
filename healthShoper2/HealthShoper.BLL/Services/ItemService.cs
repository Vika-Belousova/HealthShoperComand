using HealthShoper.BLL.Exceptions.Models.Enums;
using HealthShoper.BLL.Extensions;
using HealthShoper.BLL.Interfaces;
using HealthShoper.BLL.Mappers;
using HealthShoper.BLL.Models.Dtos;
using HealthShoper.BLL.Models.ViewModels;
using HealthShoper.DAL.Interfaces;
using HealthShoper.DAL.Models;
using HealthShoper.DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthShoper.BLL.Services;

public class ItemService(IApplicationDbContext dbContext) : IItemService
{
    public async Task<ItemDto> GetItemAsync(int itemId)
    {
        // 1. Находим товар
        var item = await dbContext.Set<Item>()
            .FirstOrDefaultAsync(p => p.Id == itemId);

        // 2. Если не нашли → ошибка 404
        if (item is null)
            throw ApiResultExtensions.NotFound(ErrorCode.Item_NotFound, "Item not found");

        // 3. Преобразуем Entity → DTO
        return item.MapToItemDto();
    }

    public async Task<IEnumerable<ItemDto>> GetItemsAsync(QueryFilter queryFilter)
    {
        // 1. Начинаем с базового запроса
        IQueryable<Item> query = dbContext.Set<Item>();

        // 2. Применяем фильтры по очереди

        // Поиск по названию (регистронезависимый)
        if (!string.IsNullOrEmpty(queryFilter.Search))
            query = query.Where(p => EF.Functions.ILike(p.Name, $"%{queryFilter.Search}%"));

        // Фильтр по минимальной цене
        if (queryFilter.MinPrice.HasValue)
            query = query.Where(p => p.Price >= queryFilter.MinPrice.Value);

        // Фильтр по максимальной цене
        if (queryFilter.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= queryFilter.MaxPrice.Value);

        // Фильтр по категории (если выбрана не "All")
        if (queryFilter.Category is { Count: > 0 } && !queryFilter.Category.Contains(CategoryType.All))
        {
            // Сначала загружаем в память, т.к. сложная фильтрация
            query = query
                .AsEnumerable()  // В память
                .Where(p => p.Category.Any(c => queryFilter.Category.Contains(c)))
                .AsQueryable();  // Обратно в IQueryable
        }

        // 3. Применяем сортировку
        query = queryFilter.SortBy switch
        {
            SortBy.Price => queryFilter.SortDirection == SortDirection.Asc
                ? query.OrderBy(p => p.Price)          // По возрастанию цены
                : query.OrderByDescending(p => p.Price), // По убыванию цены

            SortBy.Date => queryFilter.SortDirection == SortDirection.Asc
                ? query.OrderBy(p => p.CreatedAt)      // По возрастанию даты
                : query.OrderByDescending(p => p.CreatedAt),

            _ => query  // Без сортировки
        };

        // 4. Выполняем запрос и преобразуем в DTO
        var items = query.ToList();
        return items.MapToItemDtos();
    }


    public async Task<IEnumerable<ItemDto>> GetDiscountItemsAsync()
    {
        // Товары со скидкой и в наличии
        var items = dbContext.Set<Item>()
            .Where(p => p.Count > 0 && p.DiscountPercents.HasValue)
            .AsEnumerable();

        return items.MapToItemDtos();
    }
}