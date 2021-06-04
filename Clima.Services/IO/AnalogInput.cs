using System;

namespace Clima.Services.IO
{
    public class AnalogInput : PinBase
    {
        public override PinType PinType
        {
            get
            {
                return PinType.Analog;
            }
        }

        public override PinDir Direction
        {
            get
            {
                return PinDir.Input;
            }
        }
    }
}
