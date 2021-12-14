using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class SimpleRelay:IRelay
    {
        private string _name;

        public SimpleRelay()
        {
        }


        public string Name => _name;

        public void On()
        {
            RelayPin?.SetState(true);
        }

        public void Off()
        {
            RelayPin?.SetState(false);
        }

        public bool RelayIsOn => RelayPin is not null && RelayPin.State;

        internal IDiscreteOutput RelayPin { get; set; }
    }
}