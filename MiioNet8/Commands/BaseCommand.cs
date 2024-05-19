using MiioNet8.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiioNet8.Commands
{
    internal class BaseCommand : ICommand
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("method")]
        public string Method { get; private set; } = "";

        [JsonPropertyName("params")]
        public List<object> Params { get; private set; } = new List<object>();

        public BaseCommand(string method) 
        {
            Method = method;
        }

        public async Task<string> ToJson()
        {
            using var memoryStream = new MemoryStream();

            await JsonSerializer.SerializeAsync(memoryStream, this);

            memoryStream.Position = 0;
            using var reader = new StreamReader(memoryStream);

            return await reader.ReadToEndAsync();
        }
    }
}
