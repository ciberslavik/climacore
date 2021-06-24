using System.Collections.Generic;
using Clima.Services.Configuration;

namespace Clima.Services.Devices.Configs
{
    public class DeviceFactoryConfig:ConfigItemBase
    {
        public DeviceFactoryConfig()
        {
            FcConfigItems = new List<FCConfig>();
            RelayConfigItems = new List<RelayConfig>();
        }
        public List<FCConfig> FcConfigItems { get; set; }
        public List<RelayConfig> RelayConfigItems { get; set; }

        public static DeviceFactoryConfig CreateDefault()
        {
            var cfg = new DeviceFactoryConfig();
            cfg.FcConfigItems.Add(new FCConfig());
            cfg.FcConfigItems[0].EnablePinName = "DO:0";
            cfg.FcConfigItems[0].AlarmPinName = "DI:0";
            cfg.FcConfigItems[0].AnalogPinName = "AO:0";
            cfg.FcConfigItems[0].FCName = "FC:0";
            cfg.FcConfigItems[0].StartUpTime = 1000;
            
            cfg.FcConfigItems.Add(new FCConfig());
            cfg.FcConfigItems[1].EnablePinName = "DO:1";
            cfg.FcConfigItems[1].AlarmPinName = "DI:1";
            cfg.FcConfigItems[1].AnalogPinName = "AO:1";
            cfg.FcConfigItems[1].FCName = "FC:1";
            cfg.FcConfigItems[1].StartUpTime = 1000;

            //Create default relays
            for (int i = 0; i < 8; i++)
            {
                var rel = new RelayConfig();
                rel.MonitorEdge = ActiveEdge.Falling;
                rel.RelayEdge = ActiveEdge.Rising;
                rel.RelayPinName = $"DO:{i + 2}";
                rel.MonitorPinName = $"DI:{i + 2}";
                rel.MonitorTimeout = 1000;
                rel.RelayName = $"REL:{i}";
                cfg.RelayConfigItems.Add(rel);
            }
            
            return cfg;
        }
    }
}