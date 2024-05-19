using MiioNet8.Interfaces;
using System.Text.Json.Serialization;

namespace MiioNet8.Miot
{
    public class Spec : ISpec
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("services")]
        public Service[] Services { get; set; }

        [JsonIgnore]
        public IEnumerable<ServiceProperty> Properties => Services.SelectMany(s => s.Properties);
    }
}
