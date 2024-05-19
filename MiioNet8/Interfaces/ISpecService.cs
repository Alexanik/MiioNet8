using MiioNet8.Miot;

namespace MiioNet8.Interfaces
{
    public interface ISpecService
    {
        int Id { get; set; }
        string Type { get; set; }
        string Description { get; set; }
        IList<ServiceProperty> Properties { get; set; }
    }
}
