using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nodsoft.WowsUnpack.Common.Data.Serialization;

public class IntToBoolConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt32() == 1;
        }
        
        return reader.GetBoolean();
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteBooleanValue(value);
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(bool);
    }
}