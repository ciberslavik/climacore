using System.Collections.Generic;

namespace Clima.Core.Controllers.Network.Messages
{
    public class LightPresetListResponse
    {
        public LightPresetListResponse()
        {
            
        }
        public int Count { get; set; }
        public List<string> Presets { get; set; } = new List<string>();
    }
}