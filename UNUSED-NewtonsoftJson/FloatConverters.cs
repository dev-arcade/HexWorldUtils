
/*
using System;
using Newtonsoft.Json;
using Unity.Mathematics;

namespace HexWorldUtils.Json
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
            try
            {
                if (reader.TokenType != JsonToken.StartObject)
                    throw new JsonSerializationException($"Expected StartObject token, got {reader.TokenType}");

                float x = 0f, y = 0f;
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
                            x = Convert.ToSingle(reader.Value);
                            hasX = true;
                        }
                        else if (prop == "y")
                        {
                            y = Convert.ToSingle(reader.Value);
                            hasY = true;
                        }
                    }
                    else if (reader.TokenType == JsonToken.EndObject)
                        break;
                }

                if (!hasX || !hasY)
                    throw new JsonSerializationException($"Missing required properties. Has x: {hasX}, Has y: {hasY}");

                return new float2(x, y);
            }
            catch (FormatException ex)
            {
                throw new JsonSerializationException("Invalid number format in float2 JSON data", ex);
            }
            catch (InvalidCastException ex)
            {
                throw new JsonSerializationException("Invalid data type in float2 JSON data", ex);
            }
            catch (OverflowException ex)
            {
                throw new JsonSerializationException("Number out of range in float2 JSON data", ex);
            }
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
            try
            {
                if (reader.TokenType != JsonToken.StartObject)
                    throw new JsonSerializationException($"Expected StartObject token, got {reader.TokenType}");

                float x = 0f, y = 0f, z = 0f;
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
                            x = Convert.ToSingle(reader.Value);
                            hasX = true;
                        }
                        else if (prop == "y")
                        {
                            y = Convert.ToSingle(reader.Value);
                            hasY = true;
                        }
                        else if (prop == "z")
                        {
                            z = Convert.ToSingle(reader.Value);
                            hasZ = true;
                        }
                    }
                    else if (reader.TokenType == JsonToken.EndObject)
                        break;
                }

                if (!hasX || !hasY || !hasZ)
                    throw new JsonSerializationException($"Missing required properties. Has x: {hasX}, Has y: {hasY}, Has z: {hasZ}");

                return new float3(x, y, z);
            }
            catch (FormatException ex)
            {
                throw new JsonSerializationException("Invalid number format in float3 JSON data", ex);
            }
            catch (InvalidCastException ex)
            {
                throw new JsonSerializationException("Invalid data type in float3 JSON data", ex);
            }
            catch (OverflowException ex)
            {
                throw new JsonSerializationException("Number out of range in float3 JSON data", ex);
            }
        }
    }
}
*/
