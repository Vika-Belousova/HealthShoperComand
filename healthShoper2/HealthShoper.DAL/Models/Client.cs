using HealthShoper.DAL.Models.Base;
using HealthShoper.DAL.Models.Enums;

namespace HealthShoper.DAL.Models;

public class Client : BaseData
{
    /// <summary>
    /// Имя пользователя будет являться обязательным.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Данное поле будет опциональным.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Уникальная почта пользователя.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Хеш пароля
    /// </summary>
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Номер телефона пользователя.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Дата создания профиля.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Дата удаления профиля пользователя.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Удален ли профиль пользователя.
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Токен для обновления
    /// </summary>
    public string? RefreshToken { get; set; }
    /// <summary>
    /// Роль пользователя в системе.
    /// </summary>
    public Role Role { get; set; } = Role.Client;
}