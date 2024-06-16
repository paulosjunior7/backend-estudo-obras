namespace Obras.Data.Enums
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public enum TypePeople
    {
        FISICA = 'F',
        JURIDICA = 'J'
    }

    public class TypePeopleConverter : JsonConverter<TypePeople>
    {
        public override TypePeople Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value switch
            {
                "F" => TypePeople.FISICA,
                "J" => TypePeople.JURIDICA,
                _ => throw new JsonException("Invalid value for TypePeople")
            };
        }

        public override void Write(Utf8JsonWriter writer, TypePeople value, JsonSerializerOptions options)
        {
            var stringValue = value switch
            {
                TypePeople.FISICA => "F",
                TypePeople.JURIDICA => "J",
                _ => throw new JsonException("Invalid value for TypePeople")
            };
            writer.WriteStringValue(stringValue);
        }
    }
}
