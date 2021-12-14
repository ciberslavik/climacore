using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Clima.Basics.Configuration;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Configuration
{
    public class VentilationControllerConfig:IConfigurationItem
    {
        public VentilationControllerConfig()
        {
        }
        
        public Dictionary<string, FanInfo> FanInfos { get; set; } = new Dictionary<string, FanInfo>();
        public string ValveServoName { get; set; }
        public string MineServoName { get; set; }
        [IgnoreDataMember]
        public string ConfigurationName => FileName;
        [IgnoreDataMember]
        public const string FileName = "VentilationConfig";
        
        public string GetNewFanInfoKey()
        {
            var prfix = "FAN:";
            for(int i = 0; i < Int32.MaxValue; i++)
            {
                var key = prfix + i;
                if (!FanInfos.ContainsKey(key))
                    return key;
            }
            return "";
        }
        public static VentilationControllerConfig CreateDefault()
        {
            return new VentilationControllerConfig();
        }
    }
}