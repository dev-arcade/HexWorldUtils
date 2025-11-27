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

            return new Vector2(x, y);
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

            return new Vector3(x, y, z);
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

            return new Vector2Int(x, y);
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

            return new Vector3Int(x, y, z);
        }
    }
}
