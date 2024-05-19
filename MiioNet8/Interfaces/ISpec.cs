using MiioNet8.Miot;

namespace MiioNet8.Interfaces
{
    public interface ISpec
    {
        string Type { get; set; }
        string Description { get; set; }
        Service[] Services { get; set; }
        IEnumerable<ServiceProperty> Properties { get; }
    }
}
