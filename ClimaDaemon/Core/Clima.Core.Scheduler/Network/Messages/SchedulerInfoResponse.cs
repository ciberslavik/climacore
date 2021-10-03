using Clima.Core.DataModel;

namespace Clima.Core.Scheduler.Network.Messages
{
    public class SchedulerInfoResponse
    {
        public SchedulerInfoResponse()
        {
            
        }

        public SchedulerProcessInfo ProcessInfo { get; set; } = new SchedulerProcessInfo();
    }
}