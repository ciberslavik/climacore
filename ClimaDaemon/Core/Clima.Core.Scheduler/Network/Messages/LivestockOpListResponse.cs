using System.Collections.Generic;

namespace Clima.Core.Scheduler.Network.Messages
{
    public class LivestockOpListResponse
    {
        public List<LivestockOperation> Operations { get; set; } = new List<LivestockOperation>();
    }
}