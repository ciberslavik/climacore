using System.Runtime.Serialization;

namespace Clima.Core.DataModel.Graphs
{
    [DataContract]
    public class TemperatureGraphPiont:GraphPointBase
    {
        private int _dayNumber;
        private double _temperature;
        
        public override object X => (double)_dayNumber;
        public override object Y => _temperature;
        [DataMember(Name = "Day")]
        public int DayNumber
        {
            get => _dayNumber;
            set => _dayNumber = value;
        }
        [DataMember(Name = "Temperature")]
        public double Temperature
        {
            get => _temperature;
            set => _temperature = value;
        }
    }
}