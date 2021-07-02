using Clima.DataModel.Configurations;
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
                ResponseTimeout = 300,
                IOProcessorCycleTime = 100,
                DiscreteReadCycleDevider = 10,
                AnalogReadCycleDevider = 11
            };
        }
        public ModbusConfig()
        {
        }
        public string PortName { get; set; }
        public int Baudrate { get; set; }
        public int ResponseTimeout { get; set; }
        public int IOProcessorCycleTime { get; set; }
        public int DiscreteReadCycleDevider { get; set; }
        public int AnalogReadCycleDevider { get; set; }
    }
}