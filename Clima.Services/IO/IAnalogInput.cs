using System;

namespace Clima.Services.IO
{
    public interface IAnalogInput : IPin
    {
        event AnalogPinValueChangedEventHandler ValueChanged;
        IAnalogValueConverter ValueConverter { get; set; }
        
        double Value { get; }
        double RawValue { get; }
    }
}
