using System;
using System.Runtime.Serialization;
using Clima.Basics;

namespace Clima.Core.DataModel
{
    public class FanInfo:ObservableObject, IComparable<FanInfo>
    {
        private bool _isAnalog = false;
        private string _key;
        private string _fanName;
        private string _relayName;
        private int _performance;
        private int _fanCount;
        private int _priority;
        private bool _hermetise;
        private bool _isManual;
        private float _startValue;
        private float _stopValue;

        public FanInfo(string key = "", string name = "", string relayName = "")
        {
            Key = key;
            FanName = name;
            RelayName = relayName;
        }

        public bool IsAnalog
        {
            get => _isAnalog;
            set => Update(ref _isAnalog, value);
        }

        public string Key
        {
            get => _key;
            set => Update(ref _key, value);
        }

        public string FanName
        {
            get => _fanName;
            set => Update(ref _fanName, value);
        }

        public string RelayName
        {
            get => _relayName;
            set => Update(ref _relayName, value);
        }

        public int Performance
        {
            get => _performance;
            set => Update(ref _performance, value);
        }

        public int FanCount
        {
            get => _fanCount;
            set => Update(ref _fanCount, value);
        }

        public int Priority
        {
            get => _priority;
            set => Update(ref _priority, value);
        }

        public bool Hermetise
        {
            get => _hermetise;
            set => Update(ref _hermetise, value);
        }

        public bool IsManual
        {
            get => _isManual;
            set => Update(ref _isManual, value);
        }

        public float StartValue
        {
            get => _startValue;
            set => Update(ref _startValue, value);
        }

        public float StopValue
        {
            get => _stopValue;
            set => Update(ref _stopValue, value);
        }

        [IgnoreDataMember] public int TotalPerformance => Performance * FanCount;
        public int CompareTo(FanInfo? other)
        {
            if (other is null)
                return -1;
            
            return Priority - other.Priority;
        }
    }
}