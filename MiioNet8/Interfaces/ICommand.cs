namespace MiioNet8.Interfaces
{
    public interface ICommand
    {
        int Id { get; set; }
        string Method { get; }
        List<object> Params { get; }

        Task<string> ToJson();
    }
}
