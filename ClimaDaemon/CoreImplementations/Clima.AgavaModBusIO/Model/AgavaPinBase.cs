using Clima.Core.IO;


namespace Clima.AgavaModBusIO.Model
{
    public abstract class AgavaPinBase : IPin
    {
        private string _description;
        private string _pinName;
        protected bool _isModified;

        protected byte _moduleId;
        protected ushort _regAddress;
        protected int _pinNumberInModule;

        protected AgavaPinBase()
        {
        }


        public abstract PinType PinType { get; }

        public abstract PinDir Direction { get; }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public string PinName
        {
            get => _pinName;
            set => _pinName = value;
        }


        public virtual bool IsModified => _isModified;

        public byte ModuleId
        {
            get => _moduleId;
            set => _moduleId = value;
        }

        public ushort RegAddress
        {
            get => _regAddress;
            set => _regAddress = value;
        }

        public int PinNumberInModule
        {
            get => _pinNumberInModule;
            set => _pinNumberInModule = value;
        }
    }
}