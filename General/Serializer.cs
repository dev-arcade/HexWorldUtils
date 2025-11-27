using System;
using Leguar.TotalJSON;
using UnityEngine;

namespace HexWorldUtils.General
{
    public class Serializer
    {
        public string SerializeData<T>(T data)
        {
            var serialized = JSON.Serialize(data);
            var jsonString = serialized.CreatePrettyString();
            return jsonString;
        }

        public bool DeserializeData<T>(string content, out T data, out string error)
        {
            var settings = new DeserializeSettings();
            settings.AllowDeserializeIntsToEnums = true;
            try
            {
                var json = JSON.ParseString(content);
                data = json.Deserialize<T>(settings);
                error = "";
                return true;
            }

            catch (Exception e)
            {
                data = default;
                error = $"Serialization failed: {e.Message}";
                Debug.Log(error);
                return false;
            }
        }
    }
}
