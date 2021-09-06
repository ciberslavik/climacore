using Clima.Basics.Services.Communication;
using Clima.Core.Network.Messages;

namespace Clima.Core.Controllers.Network.Services
{
    public class SchedulerService : INetworkService
    {
        public SchedulerService()
        {
        }

        [ServiceMethod]
        public DefaultResponse StartScheduler()
        {
            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse StopScheduler()
        {
            return new DefaultResponse();
        }
    }
}