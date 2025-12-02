using HealthShoper.DAL.Models.Base;

namespace HealthShoper.DAL.Models;

/// <summary>
/// Корзина
/// </summary>
public class Basket : BaseData
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Товары.
    /// </summary>
    public List<BasketItem> Items { get; set; } = [];
}

public class BasketItem : BaseData
{
    public int BasketId { get; set; }
    public Basket Basket { get; set; }

    public int ItemId { get; set; }
    public Item Item { get; set; }

    /// <summary>
    /// Количество товара в корзине
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Цена товара на момент добавления в корзину.
    /// </summary>
    public decimal Price { get; set; }
}