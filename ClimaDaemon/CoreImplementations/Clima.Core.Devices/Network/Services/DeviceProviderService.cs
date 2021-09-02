using System.Collections.Generic;
using Clima.Basics.Services.Communication;
using Clima.Core.DataModel;
using Clima.Core.Devices.Network.Messages;
using Clima.Core.Network.Messages;

namespace Clima.Core.Devices.Network.Services
{
    public class DeviceProviderService:INetworkService
    {
        private readonly IDeviceProvider _deviceProvider;

        public DeviceProviderService(IDeviceProvider deviceProvider)
        {
            _deviceProvider = deviceProvider;
        }

        [ServiceMethod]
        public RelayInfosResponse GetRelayInfos(DefaultRequest request)
        {
            return new RelayInfosResponse()
            {
                Infos = _deviceProvider.GetRelayInfos()
            };
        }
    }
}