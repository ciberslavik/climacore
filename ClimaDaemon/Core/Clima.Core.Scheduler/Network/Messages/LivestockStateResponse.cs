using Clima.Core.DataModel;

namespace Clima.Core.Scheduler.Network.Messages
{
    public class LivestockStateResponse
    {
        public LivestockStateResponse()
        {
            
        }

        public LivestockState State { get; set; } = new LivestockState();
    }
}