using System;

namespace Clima.Services.IO
{
    public interface IAnalogOutput : IPin
    {
        public event AnalogPinValueChangedEventHandler ValueChanged;
        
        IAnalogValueConverter ValueConverter { get; set; }
        double Value { get; }
        void SetValue(double value);

    }
}
