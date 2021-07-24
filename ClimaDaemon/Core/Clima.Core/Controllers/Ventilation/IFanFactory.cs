namespace Clima.Core.Controllers.Ventilation
{
    public interface IFanFactory
    {
        IAnalogFan GetAnalogFan();
    }
}