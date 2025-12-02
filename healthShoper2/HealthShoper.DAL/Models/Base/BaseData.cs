namespace HealthShoper.DAL.Models.Base;

/// <summary>
/// Базовый класс для всех моделей. От данного класса будут наследоваться все остальные.
/// </summary>
public class BaseData
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }
}