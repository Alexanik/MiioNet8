using MiioNet8.Interfaces;
using System.Text.Json.Serialization;

namespace MiioNet8.Miot
{
    public class ServiceProperty : ISpecServiceProperty
    {
        [JsonPropertyName("iid")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("access")]
        public string[] Access { get; set; }

        [JsonIgnore]
        public ISpecService Service { get; set; }

        public override string ToString() => Description;
    }
}
