using System.Collections.Generic;

namespace Clima.Services.Devices.Configs
{
    public class DiscreteFanConfig
    {
        public DiscreteFanConfig()
        {
            Fans = new List<DiscreteFanConfigItem>();
        }
        
        public List<DiscreteFanConfigItem> Fans { get; set; }
    }
}