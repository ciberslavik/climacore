using System;

namespace Clima.Services.IO
{
    public class DiscreteOutput:PinBase
    {
        public override PinType PinType
        {
            get
            {
                return PinType.Discrete;
            }
        }

        public override PinDir Direction
        {
            get
            {
                return PinDir.Output;
            }
        }
    }
}
