using Clima.Basics.Services.Communication;
using Clima.Core.Controllers.Network.Messages;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.DataModel;
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
            _ventController.CreateOrUpdateFan(request.Info);
            return new DefaultResponse();
        }

        [ServiceMethod]
        public FanStateResponse GetFanState(FanStateRequest request)
        {
            FanState st = _ventController.FanStates[request.RequestFanKey];
            if (st is not null)
                return new FanStateResponse() {State = st};
            
            return new FanStateResponse();
        }

        [ServiceMethod]
        public FanStateResponse SetFanState(FanStateRequest request)
        {
            _ventController.UpdateFanState(request.State);
            return new FanStateResponse()
            {
                State = _ventController.FanStates[request.State.Info.Key]
            };

        }

        [ServiceMethod]
        public FanStateListResponse GetFanStateList(DefaultRequest request)
        {
            var response = new FanStateListResponse();
            foreach (var fanState in _ventController.FanStates.Values)
            {
                response.States.Add(fanState);
            }

            return response;
        }

        [ServiceMethod]
        public ServoStateResponse UpdateValveState(UpdateServoStateRequest request)
        {
            _ventController.ValveIsManual = request.IsManual;
            if (request.IsManual)
                _ventController.SetValvePosition(request.SetPoint);

            return new ServoStateResponse()
            {
                CurrentPosition = _ventController.ValveCurrentPos,
                IsManual = _ventController.ValveIsManual,
                SetPoint = _ventController.ValveSetPoint
            };
        }
        [ServiceMethod]
        public ServoStateResponse GetValveState(DefaultRequest request)
        {
            return new ServoStateResponse()
            {
                CurrentPosition = _ventController.ValveCurrentPos,
                IsManual = _ventController.ValveIsManual,
                SetPoint = _ventController.ValveSetPoint
            };
        }

        [ServiceMethod]
        public ServoStateResponse UpdateMineState(UpdateServoStateRequest request)
        {
            _ventController.MineIsManual = request.IsManual;
            if (request.IsManual)
                _ventController.SetMinePosition(request.SetPoint);
           
            return new ServoStateResponse()
            {
                CurrentPosition = _ventController.MineCurrentPos,
                IsManual = _ventController.MineIsManual,
                SetPoint = _ventController.MineSetPoint
            };
        }

        [ServiceMethod]
        public ServoStateResponse GetMineState(DefaultRequest request)
        {
                ClimaContext.Logger.Debug($"Servo curent pos:{_ventController.MineCurrentPos}");
            return new ServoStateResponse()
            {
                CurrentPosition = _ventController.MineCurrentPos,
                IsManual = _ventController.MineIsManual,
                SetPoint = _ventController.MineSetPoint
            };
        }
    }
}