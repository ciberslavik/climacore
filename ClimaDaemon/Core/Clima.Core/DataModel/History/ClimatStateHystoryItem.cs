using System;

namespace Clima.Core.DataModel.History
{
    public class ClimatStateHystoryItem
    {
        public DateTime PointDate { get; set; }
        public float FrontTemperature { get; set; }
        public float RearTemperature { get; set; }
        public float OutdoorTemperature { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
        public float TemperatureSetPoint { get; set; }
        public float VentilationSetPoint { get; set; }
        
        public float ValveSetPoint { get; set; }
        public float ValvePosition { get; set; }
        
        public float MineSetPoint { get; set; }
        public float MinePosition { get; set; }
    }
}