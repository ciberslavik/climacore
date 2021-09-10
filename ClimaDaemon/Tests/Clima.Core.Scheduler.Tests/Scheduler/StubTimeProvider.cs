using System;

namespace Clima.Core.Scheduler.Tests
{
    public class StubTimeProvider:ITimeProvider
    {
        private DateTime _time;
        public StubTimeProvider()
        {
        }

        public void SetTime(DateTime time)
        {
            _time = time;
        }

        public DateTime Now => _time;
        public DateTime GetNetworkTime()
        {
            throw new NotImplementedException();
        }

        public void SetLocalTime(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}