using Clima.Services.IO;

namespace Clima.AgavaModBusIO.Model
{
    public class AgavaDInput : DiscreteInput
    {
        private int _pinNumberInModule;
        private ushort _regAddress;
        private byte _moduleId;

        public AgavaDInput(byte moduleId, int pinNumberInModule)
        {
            _pinNumberInModule = pinNumberInModule;
            _regAddress = (ushort)(10000 + (_pinNumberInModule / 16));
            _moduleId = moduleId;
        }
        internal int PinNumberInModule => _pinNumberInModule;
        internal ushort RegAddress => _regAddress;
        internal byte ModuleId => _moduleId;
    }
}
