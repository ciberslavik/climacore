using System;

namespace Clima.Services.IO
{
    public class AnalogOutput : PinBase
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
                return PinDir.Output;
            }
        }
        public virtual double Value { get; set; }
    }
}
