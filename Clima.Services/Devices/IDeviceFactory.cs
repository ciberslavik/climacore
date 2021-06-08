namespace Clima.Services.Devices
{
    public interface IDeviceFactory
    {
        FrequencyConverter CreateFrequencyConverter(int number);
        DiscreteFanController CreateFanController();
        ServoController CreateServoController(int number);
        HeaterController CreateHeaterController(int number);
        
    }
}