using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantGenerated
{
    public class MobileAppEventData
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("webhook_id")]
        public string? WebhookId { get; set; }

        [JsonPropertyName("server_id")]
        public string? ServerId { get; set; }

        [JsonPropertyName("device_id")]
        public string? DeviceId { get; set; }

        [JsonPropertyName("action")]
        public string? Action { get; set; }

        // Alle overige velden (zoals action_1_title, action_2_key, etc.)
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? AdditionalData { get; set; }

        // Helper: verzamel alle action_X_title en action_X_key waarden
        public IEnumerable<(string Title, string Key)> GetActions()
        {
            if (AdditionalData == null)
                yield break;

            var titles = new Dictionary<int, string>();
            var keys = new Dictionary<int, string>();

            foreach (var kvp in AdditionalData)
            {
                if (kvp.Key.StartsWith("action_", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = kvp.Key.Split('_', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 3 && int.TryParse(parts[1], out int index))
                    {
                        if (parts[2].Equals("title", StringComparison.OrdinalIgnoreCase))
                            titles[index] = kvp.Value.GetString() ?? string.Empty;
                        else if (parts[2].Equals("key", StringComparison.OrdinalIgnoreCase))
                            keys[index] = kvp.Value.GetString() ?? string.Empty;
                    }
                }
            }

            foreach (var index in titles.Keys)
            {
                yield return (titles[index], keys.GetValueOrDefault(index, string.Empty));
            }
        }
    }
}
