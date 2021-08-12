﻿using Clima.Core.IO;

namespace Clima.Core.Tests.IOService
{
    public class StubAnalogOutput : IAnalogOutput
    {
        public PinType PinType => PinType.Analog;
        public PinDir Direction => PinDir.Output;

        public string Description { get; set; }
        public string PinName { get; set; }
        public bool IsModified { get; }
        public event AnalogPinValueChangedEventHandler ValueChanged;
        public IAnalogValueConverter ValueConverter { get; set; }
        public double Value { get; }

        public void SetValue(float value)
        {
            throw new System.NotImplementedException();
        }
    }
}