using HealthShoper.BLL.Models.Dtos;
using HealthShoper.BLL.Models.ViewModels;

namespace HealthShoper.BLL.Interfaces;

public interface IItemService
{
    Task<ItemDto> GetItemAsync(int itemId);
    Task<IEnumerable<ItemDto>> GetItemsAsync(QueryFilter queryFilter);
    Task<IEnumerable<ItemDto>> GetDiscountItemsAsync();
    
}