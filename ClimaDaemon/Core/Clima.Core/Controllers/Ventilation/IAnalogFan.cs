using Clima.Core.Devices;

namespace Clima.Core.Controllers.Ventilation
{
    public interface IAnalogFan:IFan
    {
        double Power { get; set; }
        
        IFrequencyConverter FrequencyConverter { get; set; }
    }
}