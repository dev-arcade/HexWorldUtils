using Newtonsoft.Json;

namespace HexWorldUtils.Json
{
    public static class JsonSettingsRef
    {
        public static readonly JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
        {
            Converters =
            {
                new Float2Converter(),
                new Float3Converter(),
                new Int2Converter(),
                new Int3Converter(),
                new Vector2Converter(),
                new Vector3Converter(),
                new Vector2IntConverter(),
                new Vector3IntConverter(),
            },
            Formatting = Formatting.Indented
        };
    }
}
