namespace Clima.Core.Devices
{
    public interface ISensors
    {
        float FrontTemperature { get; }
        float RearTemperature { get; }
        float OutdoorTemperature { get; }
        float Humidity { get; }
        float Pressure { get; }
        float Valve1 { get; }
        float Valve2 { get; }
    }
}