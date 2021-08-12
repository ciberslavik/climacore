using System;
using Clima.Basics;

namespace Clima.Core.Devices
{
    public class FanState : ObservableObject
    {
        private int _fanId;
        private string _fanName;
        private bool _disabled;
        private bool _hermetise;
        private int _performance;
        private int _fansCount;
        private int _priority;
        private float _startValue;
        private float _stopValue;

        public FanState()
        {
        }

        public int FanId
        {
            get => _fanId;
            set => Update(ref _fanId, value);
        }

        public string FanName
        {
            get => _fanName;
            set => Update(ref _fanName, value);
        }

        public bool Disabled
        {
            get => _disabled;
            set =>  Update(ref _disabled, value);
        }

        public bool Hermetise
        {
            get => _hermetise;
            set =>  Update(ref _hermetise, value);
        }

        public int Performance
        {
            get => _performance;
            set =>  Update(ref _performance,  value);
        }

        public int FansCount
        {
            get => _fansCount;
            set =>  Update(ref _fansCount,  value);
        }

        public int Priority
        {
            get => _priority;
            set =>  Update(ref _priority, value);
        }

        public float StartValue
        {
            get => _startValue;
            set =>  Update(ref _startValue, value);
        }

        public float StopValue
        {
            get => _stopValue;
            set =>  Update(ref _stopValue, value);
        }
    }
}