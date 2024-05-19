using MiioNet8.Interfaces;

namespace MiioNet8.Commands
{
    internal class GetPropertiesCommand : BaseCommand
    {
        public GetPropertiesCommand(List<ISpecServiceProperty> properties) : base("get_properties") =>
            Params.AddRange(properties.Select(p => new Property($"{p.Service.Description.ToLower()}:{p.Description.ToLower()}", p.Service.Id, p.Id)));
    }
}
