using Clima.Core.DataModel.Graphs;

namespace Clima.Core.Devices
{
    public class DiscreteFan:IDiscreteFan
    {
        private bool _isRunning;
        
        public void Start()
        {
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        
        public int FanPerformance { get; }
        public int FansCount { get; }
        public double StartPoint { get; }
        public double StopPoint { get; }
        public FanState State { get; }
    }
}