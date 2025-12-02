using HealthShoper.BLL.Models.Dtos;

namespace HealthShoper.BLL.Interfaces;

public interface IBucketService
{
    Task AddBucket(int itemId, int userId);
    Task DeleteBucket(int itemId, int userId);
    Task<IEnumerable<ItemDto>?> GetFromBucket(int userId);
}