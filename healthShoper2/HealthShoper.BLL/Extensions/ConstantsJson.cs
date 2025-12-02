using System.Text.Encodings.Web;
using System.Text.Json;
using HealthShoper.BLL.Extensions.JsonConverters;

namespace HealthShoper.BLL.Extensions;

public class ConstantsJson
{
    public static readonly JsonSerializerOptions JsonLogOptions =
        new() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, WriteIndented = true };

    public static readonly JsonSerializerOptions JsonDeserializeApiOptions =
        new()
        {
            Converters =
            {
                new BoolConverter(),
                new BoolNullConverter(),
                new IntConverter(),
                new IntNullConverter()
            },
            PropertyNameCaseInsensitive = true
        };

    public static readonly JsonSerializerOptions JsonSerializeApiOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
}