namespace MiioNet8.Interfaces
{
    public interface IHorizontalSwingDevice
    {
        Task SetHorizontalSwing(bool value);
        Task<int> GetHorizontalSwingAngle();
        Task SetHorizontalSwingAngle(int angle);
    }
}
