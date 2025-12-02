using HealthShoper.DAL.Models.Base;
using HealthShoper.DAL.Models.Enums;

namespace HealthShoper.DAL.Models;

/// <summary>
/// Предметы
/// </summary>
public class Item : BaseData
{
    /// <summary>
    /// Наименование товара
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание товара
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Категория товара
    /// </summary>
    public IEnumerable<CategoryType> Category { get; set; } = [CategoryType.All];

    /// <summary>
    /// Цена товара
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Количество товара.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Скидка (опциональная)
    /// </summary>
    public int? DiscountPercents { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}