using MiioNet8.Interfaces;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MiioNet8.Miot
{
    public class Service : ISpecService, IJsonOnDeserialized
    {
        [JsonPropertyName("iid")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("properties")]
        public IList<ServiceProperty> Properties { get; set; } = new List<ServiceProperty>();

        public void OnDeserialized()
        {
            foreach (var property in Properties)
                property.Service = this;
        }

        public override string ToString() => Description;

    }
}
