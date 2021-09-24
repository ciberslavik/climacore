using System.Linq;
using Clima.Basics.Services.Communication;
using Clima.Core.Controllers.Heater;
using Clima.Core.Controllers.Network.Messages;
using Clima.Core.Network.Messages;

namespace Clima.Core.Controllers.Network.Services
{
    public class HeaterControllerService:INetworkService
    {
        private readonly IHeaterController _heaterController;

        public HeaterControllerService(IHeaterController heaterController)
        {
            _heaterController = heaterController;
        }

        [ServiceMethod]
        public HeaterStateResponse GetHeaterState(HeaterStateRequest request)
        {
            return new HeaterStateResponse()
            {
                State = _heaterController.GetHeaterState(request.Key)
            };
        }
        
        [ServiceMethod]
        public HeaterStateResponse SetHeaterState(HeaterStateRequest request)
        {
            _heaterController.SetHeaterState(request.State);
            return new HeaterStateResponse()
            {
                State = _heaterController.GetHeaterState(request.Key)
            };
        }

        [ServiceMethod]
        public HeaterStateListResponse GetStateList(DefaultRequest request)
        {
            var list = new HeaterStateListResponse();
            list.States = _heaterController.States.Values.ToList();
            return list;
        }

        [ServiceMethod]
        public HeaterInfoResponse UpdateHeaterInfo(HeaterInfoRequest request)
        {
            return new HeaterInfoResponse() {Info = _heaterController.UpdateHeaterInfo(request.Info)};
        }
    }
}