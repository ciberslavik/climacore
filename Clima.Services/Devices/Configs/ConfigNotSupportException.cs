using System;

namespace Clima.Services.Devices.Configs
{
    public class ConfigNotSupportException:Exception
    {
        public ConfigNotSupportException()
        {
        }

        public ConfigNotSupportException(Type expected, Type received)
        {
            ExpectedTypeName = expected.FullName;
            ReceivedTypeName = received.FullName;
        }
        public string ExpectedTypeName { get; set; }
        public string ReceivedTypeName { get; set; }
    }
}