namespace MiioNet8.Interfaces
{
    public interface IFanPowerControlDevice
    {
        Task<int> GetCurrentPower();
        Task SetCurrentPower(int power);
    }
}
