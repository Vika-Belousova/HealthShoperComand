using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthShoper.BLL.Extensions.JsonConverters;

public class IntConverter : JsonConverter<int>
{
    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value);

    public override int Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    ) =>
        reader.TokenType switch
        {
            JsonTokenType.String => int.TryParse(reader.GetString(), out var b) ? b : 0,
            JsonTokenType.Number => reader.TryGetInt32(out var l) ? l : 0,
            _ => 0,
        };
}
