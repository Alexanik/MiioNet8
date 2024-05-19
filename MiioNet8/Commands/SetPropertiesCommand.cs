using MiioNet8.Interfaces;
using MiioNet8.Miot;
using System.Text.Json.Serialization;

namespace MiioNet8.Commands
{
    internal class SetPropertiesCommand : BaseCommand
    {
        private class PropertyModel
        {
            [JsonPropertyName("did")]
            public string Did { get; set; }

            [JsonPropertyName("siid")]
            public int Siid { get; set; }

            [JsonPropertyName("piid")]
            public int Piid { get; set; }

            [JsonPropertyName("value")]
            public object Value { get; set; }

            public PropertyModel(string did, int siid, int piid, object value)
            {
                Did = did;
                Siid = siid;
                Piid = piid;
                Value = value;
            }
        }

        public SetPropertiesCommand(List<(ISpecServiceProperty, object)> parameters) : base("set_properties") => 
            Params.AddRange(parameters.Select(p => new PropertyModel($"{p.Item1.Service.Description.ToLower()}:{p.Item1.Description.ToLower()}", p.Item1.Service.Id, p.Item1.Id, p.Item2)));
    }
}
