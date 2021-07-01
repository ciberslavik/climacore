namespace Clima.Services.Devices
{
    public interface IDeviceFactory
    {
        FrequencyConverter GetFrequencyConverter(string converterName);
        Relay GetRelay(string relayName);
        ServoController CreateServoController(int number);
        HeaterController CreateHeaterController(int number);
        
    }
}