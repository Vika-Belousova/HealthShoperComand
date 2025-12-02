using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthShoper.BLL.Extensions.JsonConverters;

public class IntNullConverter : JsonConverter<int?>
{
    public override void Write(Utf8JsonWriter writer, int? number, JsonSerializerOptions options)
    {
        if (number.HasValue)
        {
            writer.WriteNumberValue(number.Value);
        }
        else
        {
            writer.WriteNullValue();
        }
    }

    public override int? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    ) =>
        reader.TokenType switch
        {
            JsonTokenType.String => int.TryParse(reader.GetString(), out var i) ? i : null,
            JsonTokenType.Number => reader.TryGetInt32(out var i) ? i : null,
            _ => null,
        };
}
