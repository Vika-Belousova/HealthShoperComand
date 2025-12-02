using Microsoft.AspNetCore.Http;

namespace HealthShoper.BLL.Exceptions.Models;

public class ErrorResponseModel
{
    public IDictionary<string, string> Messages { get; set; } = new Dictionary<string, string>();
    public int Code { get; set; } = StatusCodes.Status400BadRequest;
}