namespace Clima.Core.Devices
{
    public delegate void TimerElapsedEventHandler(object sender);
    public interface ITimer
    {
        void Start();
        void Stop();
        double Interval { get; set; }
        event TimerElapsedEventHandler Elapsed;
    }
}