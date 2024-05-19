using System.Text.Json.Serialization;

namespace MiioNet8.Miot
{
    internal class Instance
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("ts")]
        public int Ts { get; set; }
    }
}
