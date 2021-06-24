namespace Clima.Services.Devices
{
    public interface IDeviceFactory
    {
        FrequencyConverter CreateFrequencyConverter(int number);
        Relay CreateRelay(int relayNumber);
        ServoController CreateServoController(int number);
        HeaterController CreateHeaterController(int number);
        
    }
}