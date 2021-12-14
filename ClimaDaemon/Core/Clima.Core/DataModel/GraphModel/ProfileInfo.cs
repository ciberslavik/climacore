using System;
using Clima.Core.Controllers.Light;

namespace Clima.Core.DataModel.GraphModel
{
    public class ProfileInfo
    {
        public ProfileInfo()
        {
        }

        public ProfileInfo(LightTimerProfile profile)
        {
            Key = profile.Key;
            Name = profile.Name;
            Description = profile.Description;
        }
        public string Key { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime CreationTime { get; set; } = DateTime.MinValue;
        public DateTime ModifiedTime { get; set; } = DateTime.MinValue;
    }
}