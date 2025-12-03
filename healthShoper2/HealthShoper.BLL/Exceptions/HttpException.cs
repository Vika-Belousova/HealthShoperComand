using System.Net;
using System.Text.Json;
using HealthShoper.BLL.Exceptions.Models.Enums;
using HealthShoper.BLL.Extensions;

namespace HealthShoper.BLL.Exceptions;

public class HttpException : Exception
{
    public int Status { get; init; } = 200;
    public ErrorCode Type { get; init; } = ErrorCode.AppException;
    public IDictionary<string, string[]> Errors { get; init; } = new Dictionary<string, string[]>();

    public HttpException() { }

    public HttpException(string errorMessage)
        : base(
            nameof(ErrorCode.InternalServerError),
            new Exception(
                JsonSerializer.Serialize(new[] { errorMessage }, ConstantsJson.JsonLogOptions)
            )
        )
    {
        Status = 500;
        Type = ErrorCode.InternalServerError;
        Errors = new Dictionary<string, string[]>
        {
            { nameof(ErrorCode.InternalServerError), new[] { errorMessage } }
        };
    }

    // Конструктор для одной ошибки
    public HttpException(
        HttpStatusCode httpStatusCode,
        ErrorCode errorCode,
        string errorType = "default",
        string errorMessage = ""
    )
        : base(
            errorCode.ToString(),
            new Exception(
                JsonSerializer.Serialize(new[] { errorMessage }, ConstantsJson.JsonLogOptions)
            )
        )
    {
        Status = (int)httpStatusCode;
        Type = errorCode;
        Errors = new Dictionary<string, string[]> { { errorType, new[] { errorMessage } } };
    }
    // Конструктор для нескольких ошибок
    public HttpException(
        HttpStatusCode httpStatusCode,
        ErrorCode errorCode,
        string errorType = "default",
        string[]? errorMessages = null
    )
        : base(
            errorCode.ToString(),
            new Exception(
                JsonSerializer.Serialize(new[] { errorMessages }, ConstantsJson.JsonLogOptions)
            )
        )
    {
        Status = (int)httpStatusCode;
        Type = errorCode;
        Errors = new Dictionary<string, string[]> { { errorType, errorMessages } };
    }
}
