using System;

namespace Clima.Core.Devices
{
    public interface IFan : IComparable<IFan>
    {
        void Start();
        void Stop();
        FanState State { get; }
    }
}