using HealthShoper.BLL.Models.Dtos;
using HealthShoper.DAL.Models;
using Riok.Mapperly.Abstractions;

namespace HealthShoper.BLL.Mappers;

[Mapper]
public static partial class ItemMapper
{
    public static IEnumerable<ItemDto> MapToItemDtos(this IEnumerable<Item> items)
    {
        return items.Select(MapToItemDto);
    }

    public static ItemDto MapToItemDto(this Item item)
    {
        var dto = ToDto(item);
        if (dto.DiscountPercents.HasValue)
        {
            dto.PriceWithDiscount = dto.Price * (1 - dto.DiscountPercents.Value / 100m);
        }

        return dto;
    }

    private static partial ItemDto ToDto(this Item item);
}