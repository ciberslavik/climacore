using Clima.Core.IO;

namespace Clima.Core.Tests.IOService
{
    public class StubAnalogInput : IAnalogInput
    {
        public PinType PinType => PinType.Analog;
        public PinDir Direction => PinDir.Input;

        public string Description { get; set; }
        public string PinName { get; set; }
        public bool IsModified { get; }
        public event AnalogPinValueChangedEventHandler ValueChanged;
        public IAnalogValueConverter ValueConverter { get; set; }
        public double Value { get; set; }
        public double RawValue { get; }
    }
}