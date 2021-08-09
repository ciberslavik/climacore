using System;
using System.Runtime.CompilerServices;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public interface IRelay
    {
        void On();
        void Off();
        bool RelayIsOn { get; }
    }
}