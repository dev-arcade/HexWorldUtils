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
            try
            {
                if (reader.TokenType != JsonToken.StartObject)
                    throw new JsonSerializationException($"Expected StartObject token, got {reader.TokenType}");

                int x = 0, y = 0;
                bool hasX = false, hasY = false;

                while (reader.Read())
                {
                    if (reader.Value != null && reader.TokenType == JsonToken.PropertyName)
                    {
                        string prop = reader.Value.ToString();
                        if (!reader.Read())
                            throw new JsonSerializationException($"Expected value after property '{prop}'");

                        if (prop == "x")
                        {
                            x = Convert.ToInt32(reader.Value);
                            hasX = true;
                        }
                        else if (prop == "y")
                        {
                            y = Convert.ToInt32(reader.Value);
                            hasY = true;
                        }
                    }
                    else if (reader.TokenType == JsonToken.EndObject)
                        break;
                }

                if (!hasX || !hasY)
                    throw new JsonSerializationException($"Missing required properties. Has x: {hasX}, Has y: {hasY}");

                return new int2(x, y);
            }
            catch (FormatException ex)
            {
                throw new JsonSerializationException("Invalid number format in int2 JSON data", ex);
            }
            catch (InvalidCastException ex)
            {
                throw new JsonSerializationException("Invalid data type in int2 JSON data", ex);
            }
            catch (OverflowException ex)
            {
                throw new JsonSerializationException("Number out of range in int2 JSON data", ex);
            }
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
            try
            {
                if (reader.TokenType != JsonToken.StartObject)
                    throw new JsonSerializationException($"Expected StartObject token, got {reader.TokenType}");

                int x = 0, y = 0, z = 0;
                bool hasX = false, hasY = false, hasZ = false;

                while (reader.Read())
                {
                    if (reader.Value != null && reader.TokenType == JsonToken.PropertyName)
                    {
                        string prop = reader.Value.ToString();
                        if (!reader.Read())
                            throw new JsonSerializationException($"Expected value after property '{prop}'");

                        if (prop == "x")
                        {
                            x = Convert.ToInt32(reader.Value);
                            hasX = true;
                        }
                        else if (prop == "y")
                        {
                            y = Convert.ToInt32(reader.Value);
                            hasY = true;
                        }
                        else if (prop == "z")
                        {
                            z = Convert.ToInt32(reader.Value);
                            hasZ = true;
                        }
                    }
                    else if (reader.TokenType == JsonToken.EndObject)
                        break;
                }

                if (!hasX || !hasY || !hasZ)
                    throw new JsonSerializationException($"Missing required properties. Has x: {hasX}, Has y: {hasY}, Has z: {hasZ}");

                return new int3(x, y, z);
            }
            catch (FormatException ex)
            {
                throw new JsonSerializationException("Invalid number format in int3 JSON data", ex);
            }
            catch (InvalidCastException ex)
            {
                throw new JsonSerializationException("Invalid data type in int3 JSON data", ex);
            }
            catch (OverflowException ex)
            {
                throw new JsonSerializationException("Number out of range in int3 JSON data", ex);
            }
        }
    }
}
