using System;

namespace Clima.Core
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
        DateTime GetNetworkTime();
        void SetLocalTime(DateTime dateTime);
    }
}