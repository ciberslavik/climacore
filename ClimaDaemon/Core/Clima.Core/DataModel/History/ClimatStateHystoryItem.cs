using System;

namespace Clima.Core.DataModel.History
{
    public class ClimatStateHystoryItem
    {
        public DateTime PointDate { get; set; }
        public float Front { get; set; }
        public float Rear { get; set; }
        public float Outdoor { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
        public float SetPoint { get; set; }
    }
}