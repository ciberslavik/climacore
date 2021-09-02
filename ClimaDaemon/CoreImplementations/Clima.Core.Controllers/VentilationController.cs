using System;
using System.Collections.Generic;
using System.Linq;
using Clima.Basics;
using Clima.Basics.Services;
using Clima.Core.Conrollers.Ventilation.DataModel;
using Clima.Core.Controllers.Configuration;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.DataModel;
using Clima.Core.Devices;

namespace Clima.Core.Conrollers.Ventilation
{
    public class VentilationController : IVentilationController
    {
        private readonly IDeviceProvider _devProvider;
        private FanControllerTable _fanTable;
        private VentilationControllerConfig _config;

        public VentilationController(IDeviceProvider devProvider)
        {
            _devProvider = devProvider;
            _fanTable = new FanControllerTable();

            ServiceState = ServiceState.NotInitialized;
        }


        public void Start()
        {
            if(ServiceState == ServiceState.Running)
                return;
            
            if (ServiceState == ServiceState.Initialized)
            {
                ServiceState = ServiceState.Running;
            }
        }

        public void Stop()
        {
            if (ServiceState == ServiceState.Running)
            {
                ServiceState = ServiceState.Stopped;
            }
        }

        public void Init(object config)
        {
            if (config is VentilationControllerConfig cfg)
            {
                _config = cfg;
                
                CreateFans();
            }
        }

        public Type ConfigType => typeof(VentilationControllerConfig);
        public ServiceState ServiceState { get; private set; }
        
        public Dictionary<string, FanState> FanStates { get; } 
            = new Dictionary<string, FanState>();
        
        public string CreateOrUpdate(FanInfo fanInfo)
        {
            if (!string.IsNullOrEmpty(fanInfo.Key))    //Key not empty
            {
                if (_config.FanInfos.ContainsKey(fanInfo.Key)) //Update existing
                {
                    _config.FanInfos[fanInfo.Key] = fanInfo;
                }
                else //Create new for info key
                {
                    _config.FanInfos.Add(fanInfo.Key, fanInfo);
                }
            }
            else     //Create new key and record
            {
                fanInfo.Key = _config.GetNewFanInfoKey();
                _config.FanInfos.Add(fanInfo.Key, fanInfo);
            }
            return fanInfo.Key;
        }

        public void RemoveFan(string fanKey)
        {
            if (_config.FanInfos.ContainsKey(fanKey))
            {
                _config.FanInfos.Remove(fanKey);
            }
        }

        public void SetPerformance(int performance)
        {
            throw new NotImplementedException();
        }
        public int TotalPerformance => 0;
        public int CurrentPerformance => 0;


        private void CreateFans()
        {
            
        }
    }
}