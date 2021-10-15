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
        public HeaterStateResponse UpdateHeaterState(HeaterStateRequest request)
        {
            _heaterController.UpdateHeaterState(request.Key, request.State);
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
            list.SetPoint = _heaterController.SetPoint;
            list.Front = ClimaContext.Current.Sensors.FrontTemperature;
            list.Rear = ClimaContext.Current.Sensors.RearTemperature;
            return list;
        }

        [ServiceMethod]
        public HeaterParamsListResponse UpdateHeaterParamsList(HeaterParamsRequest request)
        {
            return new HeaterParamsListResponse() {ParamsList = _heaterController.UpdateHeaterParams(request.ParamsList)};
        }

        [ServiceMethod]
        public HeaterParamsListResponse GetHeaterParamsList(DefaultRequest request)
        {
            return new HeaterParamsListResponse() {ParamsList = _heaterController.Params.Values.ToList()};
        }
    }
}