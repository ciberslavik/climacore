using Clima.Services.Configuration;

namespace Clima.AgavaModBusIO.Configuration
{
    public class ModbusConfig:ConfigItemBase
    {
        public static ModbusConfig CreateDefault()
        {
            return new ModbusConfig()
            {
                PortName = "/dev/ttyUSB1",
                Baudrate = 115200,
                ResponseTimeout = 300
            };
        }
        public ModbusConfig()
        {
        }
        public string PortName { get; set; }
        public int Baudrate { get; set; }
        public int ResponseTimeout { get; set; }
    }
}