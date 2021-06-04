using System;
using System.Collections.Generic;
using Clima.Services.IO;

namespace Clima.AgavaModBusIO.Model
{
    internal class AgavaIOModule
    {
        Dictionary<string, PinBase> _pins;

        public Dictionary<string, PinBase> Pins { get => _pins; set => _pins = value; }
    }
}
