using HealthShoper.BLL.Interfaces;
using HealthShoper.BLL.Mappers;
using HealthShoper.BLL.Models.Dtos;
using HealthShoper.DAL.Interfaces;
using HealthShoper.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthShoper.BLL.Services;

public class BucketService(IApplicationDbContext dbContext) : IBucketService
{
    public async Task AddBucket(int itemId, int userId)
    {
        var item = await dbContext.Set<Item>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == itemId);

        if (item == null)
            throw new Exception("Товар не найден");

        var basket = await dbContext.Set<Basket>()
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.ClientId == userId);

        // Создаём корзину, если её нет
        if (basket == null)
        {
            basket = new Basket
            {
                ClientId = userId,
                Items = new List<BasketItem>
                {
                    new BasketItem
                    {
                        ItemId = itemId,
                        Quantity = 1,
                        Price = item.Price // фиксируем цену
                    }
                }
            };

            await dbContext.Set<Basket>().AddAsync(basket);
        }
        else
        {
            var basketItem = basket.Items.FirstOrDefault(i => i.ItemId == itemId);

            // Уже есть товар → увеличиваем количество
            if (basketItem != null)
            {
                basketItem.Quantity++;
            }
            else
            {
                // Добавляем новый товар
                basket.Items.Add(new BasketItem
                {
                    ItemId = itemId,
                    Quantity = 1,
                    Price = item.Price
                });
            }
        }

        await dbContext.SaveChangesAsync();
    }


    public async Task DeleteBucket(int itemId, int userId)
    {
        var basket = await dbContext.Set<Basket>()
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.ClientId == userId);

        if (basket == null)
            return;

        var basketItem = basket.Items.FirstOrDefault(i => i.ItemId == itemId);
        if (basketItem == null)
            return;

        if (basketItem.Quantity > 1)
        {
            basketItem.Quantity--;
        }
        else
        {
            basket.Items.Remove(basketItem);
        }

        await dbContext.SaveChangesAsync();
    }


    public async Task<IEnumerable<ItemDto>> GetFromBucket(int userId)
    {
        var basket = await dbContext.Set<Basket>()
            .Include(b => b.Items)
            .ThenInclude(i => i.Item)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ClientId == userId);

        if (basket == null)
            return Enumerable.Empty<ItemDto>();

        return basket.Items.Select(i => new ItemDto
        {
            Id = i.ItemId,
            Name = i.Item.Name,
            Description = i.Item.Description,
            Price = i.Price,
            Count = i.Quantity
        });
    }
}