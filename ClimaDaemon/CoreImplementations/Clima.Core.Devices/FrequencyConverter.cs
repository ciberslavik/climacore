﻿using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class FrequencyConverter : IFrequencyConverter
    {
        public FrequencyConverter()
        {
        }


        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void SetPower(float power)
        {
            throw new System.NotImplementedException();
        }

        public float Power { get; }
        internal IDiscreteOutput EnablePin { get; set; }
        internal IDiscreteInput AlarmPin { get; set; }
        internal IAnalogOutput AnalogPin { get; set; }
    }
}