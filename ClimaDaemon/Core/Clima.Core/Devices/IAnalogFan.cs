namespace Clima.Core.Devices
{
    public interface IAnalogFan:IFan
    {
        double Power { get; set; }
        
        IFrequencyConverter FrequencyConverter { get; set; }
    }
}