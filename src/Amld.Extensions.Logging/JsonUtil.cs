using System.Text.Json;

namespace Amld.Extensions.Logging
{
    internal static class JsonUtil
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true };


        public static string ToJson(this object obj, JsonSerializerOptions? jsonOptions  = null)
        {
            return JsonSerializer.Serialize(obj, jsonOptions);
        }

        public static string ToIndentedJson(this object obj, JsonSerializerOptions? jsonOptions = null)
        {
            jsonOptions ??= _jsonOptions;
            return JsonSerializer.Serialize(obj, jsonOptions);
        }
    }
}
