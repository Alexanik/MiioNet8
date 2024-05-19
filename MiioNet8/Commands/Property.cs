using System.Text.Json.Serialization;

namespace MiioNet8.Commands
{
    public class Property
    {
        [JsonPropertyName("did")]
        public string Did { get; set; }

        [JsonPropertyName("siid")]
        public int Sid { get; set; }

        [JsonPropertyName("piid")]
        public int Pid { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }

        public Property(string did, int sid, int pid)
        {
            Did = did;
            Sid = sid;
            Pid = pid;
        }
    }
}
