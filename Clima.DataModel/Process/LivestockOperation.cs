using System;

namespace Clima.DataModel.Process
{
    public class LivestockOperation
    {
        public LivestockOperation()
        {
        }

        public LivestockOperationType OperationType { get; set; }
        public DateTime OperationDate { get; set; }
        public int HeadsCount { get; set; }
    }
}