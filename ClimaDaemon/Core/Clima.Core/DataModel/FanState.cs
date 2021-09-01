using System.Runtime.Serialization;
using Clima.Basics;

namespace Clima.Core.DataModel
{
    public class FanState : ObservableObject
    {
        public FanState()
        {
        }
        public FanStateEnum State { get; set; }
        public FanModeEnum Mode { get; set; }
        public FanInfo Info { get; set; }
    }

    [DataContract(Name = "FanStateEnum")]
    public enum FanStateEnum : int
    {
        [EnumMember]
        Stopped = 0,
        [EnumMember]
        Running = 1,
        [EnumMember]
        Alarm = 2
    }

    public enum FanModeEnum : int
    {
        Auto = 0,
        Manual = 1,
        Hermetise = 2
    }
}