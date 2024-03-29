﻿using System;
using System.Runtime.Serialization;
using Clima.Basics;

namespace Clima.Core.DataModel
{
    public class FanInfo:ObservableObject, IComparable<FanInfo>
    {
        private bool _isAnalog = false;
        private float _analogPower;
        private string _key;
        private string _fanName;
        private string _relayName;
        private int _performance;
        private int _fanCount;
        private int _priority;
        private FanModeEnum _mode;
        private FanStateEnum _state;
        private float _startValue;
        private float _stopValue;
        private bool _isAlarm;

        public FanInfo(string key = "", string name = "", string relayName = "")
        {
            _key = key;
            _fanName = name;
            _relayName = relayName;
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

        public FanModeEnum Mode
        {
            get => _mode;
            set => _mode = value;
        }

        public FanStateEnum State
        {
            get => _state;
            set => _state = value;
        }

        public float AnalogPower
        {
            get => _analogPower;
            set => _analogPower = value;
        }

        public bool IsAlarm
        {
            get => _isAlarm;
            set => _isAlarm = value;
        }

        public int CompareTo(FanInfo? other)
        {
            if (other is null)
                return -1;
            
            return Priority - other.Priority;
        }
    }
    public enum FanStateEnum : int
    {
        [EnumMember]
        Stopped = 0,
        [EnumMember]
        Running = 1
    }

    public enum FanModeEnum : int
    {
        [EnumMember]
        Auto = 0,
        [EnumMember]
        Manual = 1,
        [EnumMember]
        Disabled = 2
    }
}