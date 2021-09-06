using Clima.Basics.Services.Communication;
using Clima.Core.Controllers.Network.Messages;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Network.Messages;

namespace Clima.Core.Controllers.Network.Services
{
    public class VentilationControllerService : INetworkService
    {
        private readonly IVentilationController _ventController;

        public VentilationControllerService(IVentilationController ventController)
        {
            _ventController = ventController;
        }

        [ServiceMethod]
        public FanInfosResponse GetFanInfoList(DefaultRequest request)
        {
            var response = new FanInfosResponse();

            foreach (var fanState in _ventController.FanStates.Values)
            {
                response.Infos.Add(fanState.Info);
            }

            return response;
        }
        
        [ServiceMethod]
        public DefaultResponse CreateOrUpdateFan(FanInfoRequest request)
        {
            _ventController.CreateOrUpdate(request.Info);
            return new DefaultResponse();
        }

        [ServiceMethod]
        public FanStateResponse GetFanState(FanStateRequest request)
        {
            return new FanStateResponse();
        }

        [ServiceMethod]
        public FanStateResponse SetFanState(FanStateRequest request)
        {
            _ventController.UpdateState(request.State);
            return new FanStateResponse();
        }

        [ServiceMethod]
        public FanStateListResponse GetFanStateList(DefaultRequest request)
        {
            var response = new FanStateListResponse();
            foreach(var fanState in _ventController.FanStates.Values)
            {
                response.States.Add(fanState);
            }
            return response;
        }
    }
}