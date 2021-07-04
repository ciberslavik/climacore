using System.Timers;

namespace Clima.Services.Devices
{
    public class DefaultTimer:ITimer
    {
        private Timer _timer;

        public DefaultTimer()
        {
            _timer = new Timer();
            _timer.AutoReset = false;
            _timer.Elapsed += OnTimerElapsed;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnElapsed();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public double Interval
        {
            get => _timer.Interval;
            set => _timer.Interval = value;
        }

        public event TimerElapsedEventHandler Elapsed;

        protected virtual void OnElapsed()
        {
            Elapsed?.Invoke(this);
        }
    }
}