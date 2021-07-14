﻿namespace Clima.Core.Devices
{
    public interface IFrequencyConverter
    {
        void Start();
        void Stop();
        void SetPower(double power);
        double Power { get; }
    }
}