namespace Clima.Core.Devices
{
    public interface IServoDrive
    {
        void Open();
        void Close();
        void SetPosition(double target);
        double CurrentPosition { get; }
    }
}