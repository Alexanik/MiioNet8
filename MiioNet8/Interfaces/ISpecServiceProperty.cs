namespace MiioNet8.Interfaces
{
    public interface ISpecServiceProperty
    {
        int Id { get; set; }
        string Type { get; set; }
        string Description { get; set; }
        string Format { get; set; }
        string[] Access { get; set; }
        ISpecService Service { get; set; }
    }
}
