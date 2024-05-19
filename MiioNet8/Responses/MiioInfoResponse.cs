using System.Text.Json.Serialization;

namespace MiioNet8.Responses
{
    public class MiioInfoResponse : BaseResponse
    {
        public class ResultModel
        {
            [JsonPropertyName("model")]
            public string Model { get; set; }
            [JsonPropertyName("token")]
            public string Token { get; set; }
            [JsonPropertyName("fw_ver")]
            public string FwVer { get; set; }
            [JsonPropertyName("mcu_fw_ver")]
            public string McuFwVer { get; set; }
            [JsonPropertyName("miio_ver")]
            public string MiioVer { get; set; }
            [JsonPropertyName("hw_ver")]
            public string HwVer { get; set; }
            [JsonPropertyName("mac")]
            public string MAC { get; set; }
        }

        [JsonPropertyName("result")]
        public ResultModel Result { get; set; }
    }
}
