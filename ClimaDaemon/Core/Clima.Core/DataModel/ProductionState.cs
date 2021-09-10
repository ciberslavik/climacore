using System;

namespace Clima.Core.DataModel
{
    public class ProductionState
    {
        public ProductionState()
        {
        }
        public int State { get; set; }
        public DateTime StartDate { get; set; }
        public int CurrentDay { get; set; }
        public int CurrentHeads { get; set; }
    }
}