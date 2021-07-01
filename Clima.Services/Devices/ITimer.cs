using System.Timers;

namespace Clima.Services.Devices
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