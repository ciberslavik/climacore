using System.Collections.Generic;
using Clima.AgavaModBusIO.Notwork.Messages;
using Clima.Basics.Services.Communication;
using Clima.Core.IO;
using Clima.Core.Network.Messages;

namespace Clima.AgavaModBusIO.Notwork
{
    public class IONetworkService:INetworkService
    {
        private readonly IIOService _ioService;

        public IONetworkService(IIOService _ioService)
        {
            this._ioService = _ioService;
        }
        [ServiceMethod]
        public PinInfoResponse GetPinInfos(DefaultRequest request)
        {
            var pins = new List<PinInfo>();

            foreach (var di in _ioService.Pins.DiscreteInputs.Values)
            {
                pins.Add(new PinInfo()
                {
                    PinName = di.PinName,
                    PinState = di.State ? 1 : 0,
                    PinType = 0
                });
            }
            foreach (var di in _ioService.Pins.DiscreteOutputs.Values)
            {
                pins.Add(new PinInfo()
                {
                    PinName = di.PinName,
                    PinState = di.State ? 1 : 0,
                    PinType = 1
                });
            }
            foreach (var di in _ioService.Pins.AnalogInputs.Values)
            {
                pins.Add(new PinInfo()
                {
                    PinName = di.PinName,
                    PinState = di.Value,
                    PinType = 2
                });
            }
            foreach (var di in _ioService.Pins.AnalogOutputs.Values)
            {
                pins.Add(new PinInfo()
                {
                    PinName = di.PinName,
                    PinState = di.Value,
                    PinType = 3
                });
            }

            return new PinInfoResponse() {Infos = pins};
        }
    }
}