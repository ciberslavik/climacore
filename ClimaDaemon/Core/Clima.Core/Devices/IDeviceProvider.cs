using System.Collections.Generic;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.DataModel;

namespace Clima.Core.Devices
{
    public interface IDeviceProvider
    {
        IRelay GetRelay(string relayName);
        List<RelayInfo> GetRelayInfos();
        IFrequencyConverter GetFrequencyConverter(string converterName);
        IDiscreteFan GetDiscreteFan(string fanName);
        IAnalogFan GetAnalogFan(string fanName);
        IServoDrive GetServo(string servoName);
        ISensors GetSensors();
    }
}