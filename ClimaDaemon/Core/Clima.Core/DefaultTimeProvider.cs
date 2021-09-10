using System;

namespace Clima.Core
{
    public class DefaultTimeProvider:ITimeProvider
    {
        
        public DateTime Now => DateTime.Now;
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