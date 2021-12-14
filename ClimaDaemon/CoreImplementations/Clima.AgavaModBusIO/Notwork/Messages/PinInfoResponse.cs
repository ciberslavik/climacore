using System.Collections.Generic;

namespace Clima.AgavaModBusIO.Notwork.Messages
{
    public class PinInfoResponse
    {
        public List<PinInfo> Infos { get; set; } = new List<PinInfo>();
    }
}