using System.Text.Json.Serialization;
using System.Text.Json;

namespace MauiCamMqttClient.Utility
{
    public static class Utility
    {
        public static T DeepCopy<T>(T obj)
        {
            if (obj == null)
                return default;

            var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve });
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve });
        }
    }
}
