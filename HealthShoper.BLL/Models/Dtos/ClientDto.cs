namespace HealthShoper.BLL.Models.Dtos;

public class ClientDto
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
    /// Номер телефона пользователя.
    /// </summary>
    public string? PhoneNumber { get; set; }
}