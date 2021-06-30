namespace Clima.Services.Devices
{
    public interface IDeviceFactory
    {
        FrequencyConverter CreateFrequencyConverter(int number);
        Relay GetRelay(string relayName);
        ServoController CreateServoController(int number);
        HeaterController CreateHeaterController(int number);
        
    }
}