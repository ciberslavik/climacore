using System.Runtime.Serialization;

namespace Clima.Core.DataModel.Graphs
{
    [DataContract]
    public class VentilationMinMaxGraphPoint:GraphPointBase
    {
        [DataMember(Name = "Day")]
        public int DayNumber { get; set; }
        [DataMember(Name =  "Max")]
        public double MaxPerformance { get; set; }
        [DataMember(Name = "Min")]
        public double MinPerformance { get; set; }


        public override object X => DayNumber;
        public override object Y => MinPerformance;

    }
}