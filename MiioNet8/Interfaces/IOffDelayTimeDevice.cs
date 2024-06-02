namespace MiioNet8.Interfaces
{
    public interface IOffDelayTimeDevice
    {
        Task<int> GetOffDelayTime();
        Task SetOffDelayTime(int min);
    }
}
