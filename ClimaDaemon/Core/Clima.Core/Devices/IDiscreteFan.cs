namespace Clima.Core.Devices
{
    public interface IDiscreteFan:IFan
    {
        IRelay FanRelay { get; set; }
    }
}