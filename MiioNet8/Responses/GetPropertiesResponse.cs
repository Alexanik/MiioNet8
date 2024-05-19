using MiioNet8.Commands;
using System.Text.Json.Serialization;

namespace MiioNet8.Responses
{
    internal class GetPropertiesResponse : BaseResponse
    {
        [JsonPropertyName("result")]
        public List<Property> Result { get; set; } = new List<Property>();
    }
}
