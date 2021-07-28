using Clima.Core.Controllers.Ventilation;

namespace Clima.Core.Devices
{
    public interface IDeviceProvider
    {
        IRelay GetRelay(string relayName);
        IFrequencyConverter GetFrequencyConverter(string converterName);
        IDiscreteFan GetDiscreteFan(string fanName);
        IAnalogFan GetAnalogFan(string fanName);
        IServoDrive GetServo(string servoName);
    }
}