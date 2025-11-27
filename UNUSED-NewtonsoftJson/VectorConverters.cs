/*
using System;
using Newtonsoft.Json;
using UnityEngine;

namespace HexWorldUtils.Json
{
    public class Vector2Converter : JsonConverter<Vector2>
    {
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            writer.WriteEndObject();
        }

        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue,
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

                return new Vector2(x, y);
            }
            catch (FormatException ex)
            {
                throw new JsonSerializationException("Invalid number format in Vector2 JSON data", ex);
            }
            catch (InvalidCastException ex)
            {
                throw new JsonSerializationException("Invalid data type in Vector2 JSON data", ex);
            }
            catch (OverflowException ex)
            {
                throw new JsonSerializationException("Number out of range in Vector2 JSON data", ex);
            }
        }
    }

    public class Vector3Converter : JsonConverter<Vector3>
    {
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
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

        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue,
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

                return new Vector3(x, y, z);
            }
            catch (FormatException ex)
            {
                throw new JsonSerializationException("Invalid number format in Vector3 JSON data", ex);
            }
            catch (InvalidCastException ex)
            {
                throw new JsonSerializationException("Invalid data type in Vector3 JSON data", ex);
            }
            catch (OverflowException ex)
            {
                throw new JsonSerializationException("Number out of range in Vector3 JSON data", ex);
            }
        }
    }

    public class Vector2IntConverter : JsonConverter<Vector2Int>
    {
        public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            writer.WriteEndObject();
        }

        public override Vector2Int ReadJson(JsonReader reader, Type objectType, Vector2Int existingValue,
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

                return new Vector2Int(x, y);
            }
            catch (FormatException ex)
            {
                throw new JsonSerializationException("Invalid number format in Vector2Int JSON data", ex);
            }
            catch (InvalidCastException ex)
            {
                throw new JsonSerializationException("Invalid data type in Vector2Int JSON data", ex);
            }
            catch (OverflowException ex)
            {
                throw new JsonSerializationException("Number out of range in Vector2Int JSON data", ex);
            }
        }
    }

    public class Vector3IntConverter : JsonConverter<Vector3Int>
    {
        public override void WriteJson(JsonWriter writer, Vector3Int value, JsonSerializer serializer)
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

        public override Vector3Int ReadJson(JsonReader reader, Type objectType, Vector3Int existingValue,
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

                return new Vector3Int(x, y, z);
            }
            catch (FormatException ex)
            {
                throw new JsonSerializationException("Invalid number format in Vector3Int JSON data", ex);
            }
            catch (InvalidCastException ex)
            {
                throw new JsonSerializationException("Invalid data type in Vector3Int JSON data", ex);
            }
            catch (OverflowException ex)
            {
                throw new JsonSerializationException("Number out of range in Vector3Int JSON data", ex);
            }
        }
    }
}
*/
