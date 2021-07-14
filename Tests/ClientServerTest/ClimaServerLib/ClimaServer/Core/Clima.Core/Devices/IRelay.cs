using System;
using System.Runtime.CompilerServices;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public interface IRelay
    {
        void SetState(bool state);
        bool State { get; }
    }
}