using System.Text.Encodings.Web;
using System.Text.Json;
using HealthShoper.BLL.Extensions.JsonConverters;

namespace HealthShoper.BLL.Extensions;

public class ConstantsJson
{
    // Для логов: разрешает неэкранированные символы, красивый вывод
    public static readonly JsonSerializerOptions JsonLogOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    // Для десериализации API: с конвертерами и без учёта регистра
    public static readonly JsonSerializerOptions JsonDeserializeApiOptions = new()
    {
        Converters =  // кастомные конвертеры
        {
            new BoolConverter(),
            new BoolNullConverter(),
            new IntConverter(),
            new IntNullConverter()
        },
        PropertyNameCaseInsensitive = true  
    };

    // Для сериализации API: camelCase 
    public static readonly JsonSerializerOptions JsonSerializeApiOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}