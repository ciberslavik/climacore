using System.Linq;
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

            response.Infos = _ventController.FanInfos.Values.ToList();
            
            return response;
        }

        [ServiceMethod]
        public DefaultResponse CreateOrUpdateFan(FanInfoRequest request)
        {
            _ventController.CreateOrUpdateFan(request.Info);
            return new DefaultResponse();
        }
        
        [ServiceMethod]
        public FanStateResponse SetFanState(FanStateRequest request)
        {
            _ventController.SetFanState(request.Key, request.State);
            return new FanStateResponse()
            {
                Key = request.Key,
                State = _ventController.FanInfos[request.Key].State
            };
        }

        [ServiceMethod]
        public FanModeResponse SetFanMode(FanModeRequest request)
        {
            _ventController.SetFanMode(request.Key, request.Mode);
            return new FanModeResponse()
            {
                Key = request.Key,
                Mode = _ventController.FanInfos[request.Key].Mode
            };
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

        [ServiceMethod]
        public VentilationStatusResponse GetVentilationStatus(DefaultRequest request)
        {
            return new VentilationStatusResponse()
            {
                MineCurrentPos = _ventController.MineCurrentPos,
                MineSetPoint = _ventController.MineSetPoint,
                ValveCurrentPos = _ventController.ValveCurrentPos,
                ValveSetPoint = _ventController.ValveCurrentPos,
                VentSetPoint = _ventController.CurrentPerformance
            };
        }
        
    }
}