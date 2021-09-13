using Clima.Core.DataModel;

namespace Clima.Core.Scheduler.Network.Messages
{
    public class SchedulerInfoResponse
    {
        public SchedulerInfoResponse()
        {
            
        }

        public SchedulerInfo Info { get; set; } = new SchedulerInfo();
    }
}