namespace Clima.Core.Ventelation
{
    public interface IAnalogFan
    {
        void Start();
        void Stop();
        void SetValue(double value);
        double Value { get; }
        bool IsRunning { get; }
        AnalogFanConfig Config { get; }
    }
}