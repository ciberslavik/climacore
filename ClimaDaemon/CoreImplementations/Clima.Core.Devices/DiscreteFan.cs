namespace Clima.Core.Devices
{
    public class DiscreteFan:IDiscreteFan
    {
        public DiscreteFan()
        {
        }


        public void Start()
        {
            if(!FanRelay.RelayIsOn)
                FanRelay.On();
        }

        public void Stop()
        {
            FanRelay.Off();
        }

        public FanState State { get; }


        public IRelay FanRelay { get; set; }
    }
}