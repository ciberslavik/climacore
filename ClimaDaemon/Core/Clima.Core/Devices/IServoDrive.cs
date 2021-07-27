namespace Clima.Core.Devices
{
    public interface IServoDrive
    {
        void Open();
        void Close();
        void SetPosition(double position);
        double CurrentPosition { get; }
    }
}