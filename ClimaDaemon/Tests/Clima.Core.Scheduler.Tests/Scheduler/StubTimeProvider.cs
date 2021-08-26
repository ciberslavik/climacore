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
    }
}