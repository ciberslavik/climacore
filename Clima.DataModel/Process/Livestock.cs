using System.Collections.Generic;

namespace Clima.DataModel.Process
{
    public class Livestock
    {
        private List<LivestockOperation> _operations = new List<LivestockOperation>();
        public Livestock()
        {
        }

        public void AddOperation(LivestockOperation op)
        {
            
        }
        
        public int TotalHeads { get; private set; }
        public int CurrentHeads { get; private set; }
        
    }
}