using System.Collections.Generic;

namespace Clima.AgavaModBusIO.Transport
{
    public class AgavaQueue
    {
        private Queue<AgavaRequest> _queue;

        private static object _queueLocker = new object();
        
        public void EnqueueRequest(AgavaRequest request)
        {
            lock (_queueLocker)
            {
                _queue.Enqueue(request);
            }
        }

        private void ProcessQueue()
        {
            
        }
    }
}