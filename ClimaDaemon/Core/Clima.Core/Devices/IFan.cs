using System;
using Clima.Core.DataModel;

namespace Clima.Core.Devices
{
    public interface IFan : IComparable<IFan>
    {
        void Start();
        void Stop();
        FanState State { get; }
    }
}