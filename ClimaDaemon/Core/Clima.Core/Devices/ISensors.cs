namespace Clima.Core.Devices
{
    public interface ISensors
    {
        double FrontTemperature { get; }
        double RearTemperature { get; }
        double OutdoorTemperature { get; }
        double Humidity { get; }
        double Pressure { get; }
    }
}