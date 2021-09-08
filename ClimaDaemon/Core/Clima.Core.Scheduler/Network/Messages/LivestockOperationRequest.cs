using System;

namespace Clima.Core.Scheduler.Network.Messages
{
    public class LivestockOperationRequest
    {
        public int HeadsCount { get; set; }
        public DateTime OperationDate { get; set; }
    }
}