using System.Net;
using HealthShoper.BLL.Exceptions;
using HealthShoper.BLL.Exceptions.Models.Enums;
using HealthShoper.DAL.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HealthShoper.BLL.Extensions;

public static class ApiResultExtensions
{
    public static HttpException Unauthorized(
        ErrorCode errorCode,
        string errorMessage
    )
    {
        return new HttpException(
            HttpStatusCode.Unauthorized,
            errorCode,
            "error",
            errorMessage
        );
    }
    
    public static HttpException BadRequest(
        
        ErrorCode errorCode,
        string errorKey,
        string errorMessage
    )
    {
        return new HttpException(
            HttpStatusCode.BadRequest,
            errorCode,
            errorKey,
            errorMessage
        );
    }

    public static HttpException BadRequest(
        ErrorCode errorCode,
        string errorKey,
        string[] errorMessage
    )
    {
        return new HttpException(
            HttpStatusCode.BadRequest,
            errorCode,
            errorKey,
            errorMessage
        );
    }

    public static HttpException NotFound(
        
        ErrorCode errorCode,
        string message = "NotFound"
    )
    {
        return new HttpException(
            HttpStatusCode.NotFound,
            errorCode,
            "error",
            message
        );
    }

    public static Exception ValidationError(
        string errorCode,
        string errorMessage
    )
    {
        return BadRequest(
            Enum.TryParse<ErrorCode>(errorCode, out var code)
                ? code
                : ErrorCode.NotValidModel,
            errorCode,
            errorMessage
        );
    }

    public static HttpException ValidationError( string message)
    {
        return new HttpException(
            HttpStatusCode.BadRequest,
            ErrorCode.NotValidModel,
            "error",
            message
        );
    }

    public static HttpException ValidationError(
        
        string format,
        object arg0
    )
    {
        return new HttpException(
            HttpStatusCode.BadRequest,
            ErrorCode.NotValidModel,
            "error",
            string.Format(format, arg0)
        );
    }

    public static HttpException ModelError(
        
        ModelStateDictionary modelState
    )
    {
        var errorMessages = modelState.ToDictionary(
            kvp => string.IsNullOrEmpty(kvp.Key) ? "default" : kvp.Key.ToCamelCase(),
            kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? [],
            StringComparer.Ordinal
        );

        return new HttpException
        {
            Status = (int)HttpStatusCode.BadRequest,
            Type = ErrorCode.NotValidModel,
            Errors = errorMessages,
        };
    }
}