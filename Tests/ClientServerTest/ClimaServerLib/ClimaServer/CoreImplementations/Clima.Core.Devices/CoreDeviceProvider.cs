using System;
using System.Collections.Generic;
using Clima.Basics.Configuration;
using Clima.Core.Devices.Configuration;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class CoreDeviceProvider:IDeviceProvider
    {
        private readonly IConfigurationStorage _configStorage;
        private readonly IIOService _ioService;
        private DeviceProviderConfig _config;
        
        private Dictionary<string, IRelay> _relays;
        private Dictionary<string, IFrequencyConverter> _fcs;
        public CoreDeviceProvider(IConfigurationStorage configStorage, IIOService ioService)
        {
            _configStorage = configStorage ?? throw new ArgumentNullException(nameof(configStorage));
            _ioService = ioService ?? throw new ArgumentNullException(nameof(ioService));

            _config = _configStorage.GetConfig<DeviceProviderConfig>("DeviceProvider");
            
            _relays = new Dictionary<string, IRelay>();
            _fcs = new Dictionary<string, IFrequencyConverter>();
        }


        public IRelay GetRelay(string relayName)
        {
            throw new System.NotImplementedException();
        }

        public IFrequencyConverter GetFrequencyConverter(string converterName)
        {
            throw new System.NotImplementedException();
        }
    }
}