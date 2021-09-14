namespace Clima.Core.Devices
{
    public interface IServoDrive
    {
        void Open();
        void Close();
        void SetPosition(float target);
        float CurrentPosition { get; }
        float SetPoint { get; }
    }
}