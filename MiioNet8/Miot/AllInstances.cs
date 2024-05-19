using System.Text.Json.Serialization;

namespace MiioNet8.Miot
{
    internal class AllInstances
    {
        [JsonPropertyName("instances")]
        public Instance[] Instances { get; set; }
    }
}
