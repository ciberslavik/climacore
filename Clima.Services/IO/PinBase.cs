using System;

namespace Clima.Services.IO
{
    public enum PinType
    {
        Discrete,
        Analog
    }
    public enum PinDir
    {
        Input,
        Output
    }
    public abstract class PinBase
    {
        private string _pinName;
        private string _description;
        protected int _index;
        protected bool _isModified;
        public abstract PinType PinType{get;}
        public abstract PinDir Direction{get;}
        public string Description { get => _description; set => _description = value; }
        public string PinName { get => _pinName; set => _pinName = value; }
        public int Index => _index;

        public bool IsModified
        {
            get => _isModified;
        }
    }
}
