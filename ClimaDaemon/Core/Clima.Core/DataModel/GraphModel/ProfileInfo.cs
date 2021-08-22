using System;

namespace Clima.Core.DataModel.GraphModel
{
    public class ProfileInfo
    {
        public ProfileInfo()
        {
        }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}