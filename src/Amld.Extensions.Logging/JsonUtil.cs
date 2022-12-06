using System.Text.Json;

namespace Amld.Extensions.Logging
{
    public static class JsonUtil
    {
        private static readonly JsonSerializerOptions _IndentedJsonOptions = new JsonSerializerOptions { 
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping 
        };

        public static string ToJson(this object obj, JsonSerializerOptions? jsonOptions  = null)
        {
            if (jsonOptions == null)
            {
                jsonOptions = _jsonOptions;
            }
            return JsonSerializer.Serialize(obj, jsonOptions);
        }

        public static string ToIndentedJson(this object obj, JsonSerializerOptions? jsonOptions = null)
        {
            jsonOptions ??= _IndentedJsonOptions;
            return JsonSerializer.Serialize(obj, jsonOptions);
        }
    }
}
