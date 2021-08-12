namespace Clima.Core.IO
{
    public interface IAnalogInput : IPin
    {
        event AnalogPinValueChangedEventHandler ValueChanged;
        IAnalogValueConverter ValueConverter { get; set; }

        float Value { get; }
        double RawValue { get; }
    }
}