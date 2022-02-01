using System.Collections.Generic;
using Clima.Basics.Configuration;
using System.Runtime.InteropServices;
using Clima.AgavaModBusIO.Model;

namespace Clima.AgavaModBusIO.Configuration
{
    public class ModbusConfig : IConfigurationItem
    {
        public static ModbusConfig CreateDefault()
        {
            var portName = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                portName = "/dev/ttyUSB1";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) portName = "COM2";

            return new ModbusConfig()
            {
                PortName = portName,
                BaudRate = 115200,
                ResponseTimeout = 300,
                IOProcessorCycleTime = 100,
                DiscreteReadCycleDevider = 10,
                AnalogReadCycleDevider = 11
            };
        }

        public ModbusConfig()
        {
            AnalogInputsTypes = new Dictionary<string, AgavaAnalogInType>();
            AnalogOutputTypes = new Dictionary<string, AgavaAnalogOutType>();
        }

        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public int ResponseTimeout { get; set; }
        public int IOProcessorCycleTime { get; set; }
        public int DiscreteReadCycleDevider { get; set; }
        public int AnalogReadCycleDevider { get; set; }

        public int BusStartAddress { get; set; }
        public int BusEndAddress { get; set; }

        public string ConfigurationName { get; set; }
        
        public Dictionary<string, AgavaAnalogInType> AnalogInputsTypes { get; set; }
        public Dictionary<string, AgavaAnalogOutType> AnalogOutputTypes { get; set; }
    }
}