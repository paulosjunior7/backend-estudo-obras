namespace Obras.Data.Enums
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public enum StatusConstruction
    {
        CONSTRUCAO = 0,
        FINALIZADA = 1,
        VENDIDA = 2
    }

    public class StatusConstructionConverter : JsonConverter<StatusConstruction>
    {
        public override StatusConstruction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetUInt32();
            return value switch
            {
                0 => StatusConstruction.CONSTRUCAO,
                1 => StatusConstruction.FINALIZADA,
                2 => StatusConstruction.VENDIDA,
                _ => throw new JsonException("Invalid value for StatusConstruction")
            };
        }

        public override void Write(Utf8JsonWriter writer, StatusConstruction value, JsonSerializerOptions options)
        {
            var stringValue = value switch
            {
                StatusConstruction.CONSTRUCAO => 0,
                StatusConstruction.FINALIZADA => 1,
                StatusConstruction.VENDIDA => 2,
                _ => throw new JsonException("Invalid value for StatusConstruction")
            };
            writer.WriteNumberValue(stringValue);
        }
    }
}
