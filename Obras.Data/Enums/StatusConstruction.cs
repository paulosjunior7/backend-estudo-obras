namespace Obras.Data.Enums
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public enum StatusConstruction
    {
        CONSTRUCAO = 'C',
        FINALIZADA = 'F',
        VENDIDA = 'V'
    }

    public class StatusConstructionConverter : JsonConverter<StatusConstruction>
    {
        public override StatusConstruction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value switch
            {
                "C" => StatusConstruction.CONSTRUCAO,
                "F" => StatusConstruction.FINALIZADA,
                "V" => StatusConstruction.VENDIDA,
                _ => throw new JsonException("Invalid value for StatusConstruction")
            };
        }

        public override void Write(Utf8JsonWriter writer, StatusConstruction value, JsonSerializerOptions options)
        {
            var stringValue = value switch
            {
                StatusConstruction.CONSTRUCAO => "C",
                StatusConstruction.FINALIZADA => "F",
                StatusConstruction.VENDIDA => "V",
                _ => throw new JsonException("Invalid value for StatusConstruction")
            };
            writer.WriteStringValue(stringValue);
        }
    }
}
