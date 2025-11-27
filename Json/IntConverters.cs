using System;
using Newtonsoft.Json;
using Unity.Mathematics;

namespace HexWorldUtils.Json
{
    public class Int2Converter : JsonConverter<int2>
    {
        public override void WriteJson(JsonWriter writer, int2 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            writer.WriteEndObject();
        }

        public override int2 ReadJson(JsonReader reader, Type objectType, int2 existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            int x = 0, y = 0;
            while (reader.Read())
            {
                if (reader.Value != null && reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString();
                    reader.Read();
                    if (prop == "x")
                        x = Convert.ToInt32(reader.Value);
                    else if (prop == "y")
                        y = Convert.ToInt32(reader.Value);
                }
                else if (reader.TokenType == JsonToken.EndObject)
                    break;
            }

            return new int2(x, y);
        }
    }

    public class Int3Converter : JsonConverter<int3>
    {
        public override void WriteJson(JsonWriter writer, int3 value, JsonSerializer serializer)
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

        public override int3 ReadJson(JsonReader reader, Type objectType, int3 existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            int x = 0, y = 0, z = 0;
            while (reader.Read())
            {
                if (reader.Value != null && reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString();
                    reader.Read();
                    if (prop == "x")
                        x = Convert.ToInt32(reader.Value);
                    else if (prop == "y")
                        y = Convert.ToInt32(reader.Value);
                    else if (prop == "z")
                        z = Convert.ToInt32(reader.Value);
                }
                else if (reader.TokenType == JsonToken.EndObject)
                    break;
            }

            return new int3(x, y, z);
        }
    }
}
