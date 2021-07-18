using Clima.Basics.Configuration;
using System.Runtime.InteropServices;

namespace Clima.AgavaModBusIO.Configuration
{
    public class ModbusConfig:IConfigurationItem
    {
        public static ModbusConfig CreateDefault()
        {
            string portName = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                portName = "/dev/ttyUSB1";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                portName = "COM2";
            }

                return new ModbusConfig()
            {
                PortName = portName,
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
        
        public string ConfigurationName { get; set; }
    }
}