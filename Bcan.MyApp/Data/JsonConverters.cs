using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bcan.MyApp.Data;

public class AppSettingValueTypeConverter : JsonConverter<Type>
{
    public override Type? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Type.GetType(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString()); // or value.FullName or value.AssemblyQualifiedName
    }
}