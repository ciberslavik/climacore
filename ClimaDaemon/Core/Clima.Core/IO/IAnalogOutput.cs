namespace Clima.Core.IO
{
    public interface IAnalogOutput : IPin
    {
        public event AnalogPinValueChangedEventHandler ValueChanged;

        IAnalogValueConverter ValueConverter { get; set; }
        float Value { get; }
        void SetValue(float value);
    }
}