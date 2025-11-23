namespace HealthShoper.Middlewears;

public class HostMiddleware : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (HttpException ex)
        {
            await WriteHttpExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            var httpEx = new HttpException(
                HttpStatusCode.InternalServerError,
                ErrorCode.InternalServerError,
                "error",
                ex.Message
            );

            await WriteHttpExceptionAsync(context, httpEx);
        }
    }


    /// <summary>
    /// Записывает кастомный HttpException в JSON-ответ.
    /// </summary>
    private static async Task WriteHttpExceptionAsync(HttpContext context, HttpException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex.Status;

        var result = new
        {
            status = ex.Status,
            type = ex.Type.ToString(),
            errors = ex.Errors
        };

        var json = JsonSerializer.Serialize(result, ConstantsJson.JsonLogOptions);
        await context.Response.WriteAsync(json);
    }
}