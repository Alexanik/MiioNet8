using MiioNet8.Interfaces;
using System.Text.Json.Serialization;

namespace MiioNet8.Responses
{
    public class BaseResponse : IResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("exe_time")]
        public int ExeTime { get; set; }
    }
}
