namespace Obras.Data.Enums
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public enum TypePhoto
    {
        FrontCover = 'F',
        Others = 'O'
    }

    public class TypePhotoConverter : JsonConverter<TypePhoto>
    {
        public override TypePhoto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value switch
            {
                "F" => TypePhoto.FrontCover,
                "O" => TypePhoto.Others,
                _ => throw new JsonException("Invalid value for TypePhoto")
            };
        }

        public override void Write(Utf8JsonWriter writer, TypePhoto value, JsonSerializerOptions options)
        {
            var stringValue = value switch
            {
                TypePhoto.FrontCover => "F",
                TypePhoto.Others => "O",
                _ => throw new JsonException("Invalid value for TypePhoto")
            };
            writer.WriteStringValue(stringValue);
        }
    }
}
