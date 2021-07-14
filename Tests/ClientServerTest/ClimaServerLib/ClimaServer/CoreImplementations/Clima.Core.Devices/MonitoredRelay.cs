using System;

namespace Clima.Core.Devices
{
    public class MonitoredRelay:IRelay
    {
        public void SetState(bool state)
        {
            throw new NotImplementedException();
        }

        public bool State { get; }
    }
}