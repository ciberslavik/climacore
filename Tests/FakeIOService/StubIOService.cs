using Clima.Services.IO;

namespace FakeIOService
{
    public class StubIOService:IIOService
    {
        private bool _isInit;
        private bool _isRunning;
        private IOPinCollection _pins;

        public StubIOService()
        {
            _pins = new IOPinCollection();
        }
        public void Init()
        {
            //Pins to relay test
            for (int i = 1; i <= 32; i++)
            {
                _pins.AddDiscreteOutput($"DO:1:{i}", new DiscreteOutput()
                {
                    PinName = $"DO:1:{i}"
                });

                _pins.AddDiscreteInput($"DI:1:{i}", new DiscreteInput()
                {
                    PinName = $"DI:1:{i}"
                });
            }

            for (int i = 0; i <= 3; i++)
            {
                _pins.AddAnalogOutput($"AO:1:{i}",new AnalogOutput()
                {
                    PinName = $"AO:1:{i}"
                });
            }
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public bool IsInit => _isInit;

        public bool IsRunning => _isRunning;

        public IOPinCollection Pins => _pins;
    }
}