using System;
using System.Runtime.Serialization;

namespace Clima.Core.DataModel
{
    public class FanInfo:IComparable<FanInfo>
    {
        public FanInfo(string key = "", string name = "", string relayName = "")
        {
            Key = key;
            FanName = name;
            RelayName = relayName;
        }

        public bool IsAnalog { get; set; } = false;
        public string Key { get; set; }
        public string FanName { get; set; }
        public string RelayName { get; set; }
        public int Performance { get; set; }
        public int FanCount { get; set; }
        public int Priority { get; set; }
        public bool Hermetise { get; set; }
        public bool IsManual { get; set; }

        public float StartValue { get; set; }
        public float StopValue { get; set; }
        
        [IgnoreDataMember] public int TotalPerformance => Performance * FanCount;
        public int CompareTo(FanInfo? other)
        {
            if (other is null)
                return -1;
            
            return Priority - other.Priority;
        }
    }
}