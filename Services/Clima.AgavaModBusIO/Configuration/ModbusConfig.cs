using Clima.Services.Configuration;

namespace Clima.AgavaModBusIO.Configuration
{
    public class ModbusConfig:ConfigItemBase
    {
        
        public ModbusConfig()
        {
        }
        public string PortName { get; set; }
        public int Baudrate { get; set; }
        public int ResponseTimeout { get; set; }
    }
}