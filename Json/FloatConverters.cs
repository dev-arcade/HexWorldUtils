using System;
using Newtonsoft.Json;
using Unity.Mathematics;

namespace MoonPlotsCartographerShared.Json
{
    public class Float2Converter : JsonConverter<float2>
    {
        public override void WriteJson(JsonWriter writer, float2 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            writer.WriteEndObject();
        }

        public override float2 ReadJson(JsonReader reader, Type objectType, float2 existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            float x = 0f, y = 0f;
            while (reader.Read())
            {
                if (reader.Value != null && reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString();
                    reader.Read();
                    if (prop == "x")
                        x = Convert.ToSingle(reader.Value);
                    else if (prop == "y")
                        y = Convert.ToSingle(reader.Value);
                }
                else if (reader.TokenType == JsonToken.EndObject)
                    break;
            }

            return new float2(x, y);
        }
    }

    public class Float3Converter : JsonConverter<float3>
    {
        public override void WriteJson(JsonWriter writer, float3 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            writer.WritePropertyName("z");
            writer.WriteValue(value.z);
            writer.WriteEndObject();
        }

        public override float3 ReadJson(JsonReader reader, Type objectType, float3 existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            float x = 0f, y = 0f, z = 0f;
            while (reader.Read())
            {
                if (reader.Value != null && reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString();
                    reader.Read();
                    if (prop == "x")
                        x = Convert.ToSingle(reader.Value);
                    else if (prop == "y")
                        y = Convert.ToSingle(reader.Value);
                    else if (prop == "z")
                        z = Convert.ToSingle(reader.Value);
                }
                else if (reader.TokenType == JsonToken.EndObject)
                    break;
            }

            return new float3(x, y, z);
        }
    }
}
