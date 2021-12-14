using System;
using System.Collections.Generic;
using Clima.Basics.Configuration;
using Clima.Core.Controllers.Light;

namespace Clima.Core.Controllers.Configuration
{
    public class LightControllerConfig:IConfigurationItem
    {
        private string _configurationName;

        public LightControllerConfig()
        {
            _configurationName = "LightControllerConfig";
        }
        public string ConfigurationName => _configurationName;

        public string ControlPinName { get; set; }
        public string CurrentProfileKey { get; set; }
        
        public Dictionary<string, LightTimerProfile> Profiles { get; set; } =
            new Dictionary<string, LightTimerProfile>();

        public static LightControllerConfig CreateDefault()
        {
            var config = new LightControllerConfig();
            config.ControlPinName = "DO:3:7";
            config.CurrentProfileKey = "PROFILE:0";
            config.Profiles.Add("PROFILE:0",new LightTimerProfile()
            {
                Key = "PROFILE:0",
                Name = "Поумолчанию",
                Days =
                {
                    new LightTimerDay()
                    {
                        Day = 0,
                        Timers =
                        {
                            new LightTimerItem(DateTime.Parse("00:00"),DateTime.Parse("01:00")),
                            new LightTimerItem(DateTime.Parse("11:00"),DateTime.Parse("12:00"))
                        }
                    }
                }
            });
            return config;
        }
    }
}