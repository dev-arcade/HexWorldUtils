using Newtonsoft.Json;

namespace MoonPlotsCartographerShared.Json
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
            },
            Formatting = Formatting.Indented
        };
    }
}
