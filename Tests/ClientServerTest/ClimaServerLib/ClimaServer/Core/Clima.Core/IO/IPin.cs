namespace Clima.Core.IO
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
    public interface IPin
    {
        PinType PinType{get;}
        PinDir Direction{get;}
        string Description { get; set; }
        string PinName { get; set; }
        public bool IsModified { get; }
    }
}
