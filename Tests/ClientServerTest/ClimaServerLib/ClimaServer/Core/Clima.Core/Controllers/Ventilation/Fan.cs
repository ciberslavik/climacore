namespace Clima.Core.Controllers.Ventilation
{
    public abstract class Fan
    {
        protected Fan()
        {
        }
        public string FanName { get; set; }
        public bool Enabled { get; set; }
        public bool Sealed { get; set; }
        public int Performance { get; set; }
        public int Priority { get; set; }
    }
}