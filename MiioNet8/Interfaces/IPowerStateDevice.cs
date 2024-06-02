namespace MiioNet8.Interfaces
{
    public interface IPowerStateDevice
    {
        Task On();
        Task Off();
        Task<bool> PowerState();
    }
}
