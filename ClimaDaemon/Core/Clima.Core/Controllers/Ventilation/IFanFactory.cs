using Clima.Core.Devices;

namespace Clima.Core.Controllers.Ventilation
{
    public interface IFanFactory
    {
        IAnalogFan GetAnalogFan(int fanId);
        IDiscreteFan GetDiscreteFan(int fanId);
    }
}