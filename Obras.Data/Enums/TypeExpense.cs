namespace Obras.Data.Enums
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public enum TypeExpense
    {
        DespesaFinal = 0,
        DespesaDiversa = 1
    }

    public class TypeExpenseConverter : JsonConverter<TypeExpense>
    {
        public override TypeExpense Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value switch
            {
                "0" => TypeExpense.DespesaFinal,
                "1" => TypeExpense.DespesaDiversa,
                _ => throw new JsonException("Invalid value for TypeExpense")
            };
        }

        public override void Write(Utf8JsonWriter writer, TypeExpense value, JsonSerializerOptions options)
        {
            var stringValue = value switch
            {
                TypeExpense.DespesaDiversa => 1,
                TypeExpense.DespesaFinal => 0,
                _ => throw new JsonException("Invalid value for TypeExpense")
            };
            writer.WriteNumberValue(stringValue);
        }
    }
}
