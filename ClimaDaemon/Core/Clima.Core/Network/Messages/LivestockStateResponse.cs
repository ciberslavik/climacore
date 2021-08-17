using System;

namespace Clima.Core.Network.Messages
{
    public class LivestockStateResponse
    {
        public LivestockStateResponse()
        {}
        public int CurrentHeads { get; set; }
        public int PlacementHeads { get; set; }
        public DateTime PlacementDate { get; set; }
    }
}