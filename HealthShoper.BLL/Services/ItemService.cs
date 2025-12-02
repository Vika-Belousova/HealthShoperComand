using System.Text.Json;
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

namespace HealthShoper.BLL.Services;

public class ItemService(IApplicationDbContext dbContext) : IItemService
{
    public async Task<ItemDto> GetItemAsync(int itemId)
    {
        var item = await dbContext.Set<Item>().FirstOrDefaultAsync(p => p.Id == itemId);

        if (item is null)
        {
            throw ApiResultExtensions.NotFound(ErrorCode.Item_NotFound, "Item not found");
        }

        return item.MapToItemDto();
    }

    public async Task<IEnumerable<ItemDto>> GetItemsAsync(QueryFilter queryFilter)
    {
        IQueryable<Item> query = dbContext.Set<Item>();

        if (!string.IsNullOrEmpty(queryFilter.Search))
            query = query.Where(p => EF.Functions.ILike(p.Name, $"%{queryFilter.Search}%"));

        if (queryFilter.MinPrice.HasValue)
            query = query.Where(p => p.Price >= queryFilter.MinPrice.Value);

        if (queryFilter.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= queryFilter.MaxPrice.Value);

        if (queryFilter.Category is { Count: > 0 } && !queryFilter.Category.Contains(CategoryType.All))
        {
            query = query
                .AsEnumerable()
                .Where(p => p.Category.Any(c => queryFilter.Category.Contains(c)))
                .AsQueryable();
        }


        // сортировка
        query = queryFilter.SortBy switch
        {
            SortBy.Price => queryFilter.SortDirection == SortDirection.Asc
                ? query.OrderBy(p => p.Price)
                : query.OrderByDescending(p => p.Price),

            SortBy.Date => queryFilter.SortDirection == SortDirection.Asc
                ? query.OrderBy(p => p.CreatedAt)
                : query.OrderByDescending(p => p.CreatedAt),

            _ => query
        };

        var items = query.ToList();
        return items.MapToItemDtos();
    }


    public async Task<IEnumerable<ItemDto>> GetDiscountItemsAsync()
    {
        var items = dbContext.Set<Item>().Where(p => p.Count > 0 && p.DiscountPercents.HasValue).AsEnumerable();
        return items.MapToItemDtos();
    }
}