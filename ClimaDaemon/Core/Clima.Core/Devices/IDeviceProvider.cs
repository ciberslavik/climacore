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
        IServoDrive GetServo(string servoName);
        IHeater GetHeater(string heaterName);
        ISensors GetSensors();
    }
}