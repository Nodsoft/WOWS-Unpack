using System.Text.Json;

namespace Nodsoft.WowsUnpack.Common.Data.Serialization;

public static class JsonHelper
{
    public static readonly JsonSerializerOptions DeserializationOptions = new()
    {
        PropertyNamingPolicy = new DeserializationNamingPolicy(),
        PropertyNameCaseInsensitive = true,
        Converters = { new IntToBoolConverter() },
    };
}