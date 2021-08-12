namespace Clima.Core.IO
{
    public interface IAnalogOutput : IPin
    {
        public event AnalogPinValueChangedEventHandler ValueChanged;

        IAnalogValueConverter ValueConverter { get; set; }
        double Value { get; }
        void SetValue(float value);
    }
}